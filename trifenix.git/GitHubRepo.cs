using LibGit2Sharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using trifenix.git.interfaces;

namespace trifenix.git
{
    public class GitHubRepo : IGithubRepo
    {
        private readonly string GitHubUrl;
        private readonly string Branch;
        private readonly string UserName;
        private readonly string Email;

        public GitHubRepo(string gitHubUrl, string branch, string userName, string email)
        {
            GitHubUrl = gitHubUrl;
            Branch = branch;
            UserName = userName;
            Email = email;
        }

        public string Cloned { get; set; }

        public string Clone()
        {
            if (string.IsNullOrWhiteSpace(Cloned))
            {
                var folder = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), $"mdm-data-{Guid.NewGuid()}")).FullName;
                Cloned = Repository.Clone(GitHubUrl, folder, new CloneOptions
                {
                    BranchName = Branch
                });
                return Cloned;
            }
            return Cloned;

        }

        public void Commit(IEnumerable<Func<bool>> commitMessageFileOperations, string message)
        {

            var folder = Clone();
            using (var repo = new Repository(folder))
            {
                // obtiene los tags desde el repositorio
                var listTags = repo.Tags;

                var lastCommit = repo.Commits.QueryBy(new CommitFilter {
                    FirstParentOnly = true,
                    SortBy = CommitSortStrategies.Time
                }).Last();

                

                // aplicamos el tag/
                var tg = repo.ApplyTag($"prev-{message}",lastCommit.Id.Sha);

               



                // enviamos el tag al servidor 
                repo.Network.Push(repo.Network.Remotes["origin"], tg.CanonicalName, new PushOptions { });

                var tgNew = repo.ApplyTag($"{message}");

                

                // carpeta de código fuente.
                var srcFolder = folder;


                //ejecutamos los commits del parámetro.
                foreach (var action in commitMessageFileOperations)
                {
                    if (action.Invoke())
                    {
                        Commands.Stage(repo, "*");
                    }
                    

                }

                repo.Commit(message, new Signature(UserName, this.Email, DateTimeOffset.Now), new Signature(this.UserName, this.Email, DateTimeOffset.Now));


                repo.Network.Push(repo.Network.Remotes["origin"], $@"refs/heads/{Branch}", new PushOptions { });

                repo.Network.Push(repo.Network.Remotes["origin"], tgNew.CanonicalName, new PushOptions { });
            }
        }

        public static string ReadAllText(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }
            if (path.Length == 0)
            {
                throw new ArgumentException("no existe ruta");
            }
            return InternalReadAllText(path, Encoding.UTF8);
        }


        private static string InternalReadAllText(string path, Encoding encoding)
        {
            string result;
            using (StreamReader streamReader = new StreamReader(path, encoding))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }

        public string Get(string path)
        {
            var tempFolder = Clone();

            return ReadAllText(Path.Combine(tempFolder, path));



        }

        public void SaveFile(string path, string message, string element)
        {
            var tempFolder = Clone();
            Commit(new List<Func<bool>> {
                ()=>{
                    File.WriteAllText(Path.Combine(tempFolder, path), element);
                    return true;
                }
            }, message);
        }
    }

    public class GitHubRepo<T> : IGithubRepo<T>
    {
        private readonly IGithubRepo githubRepo;

        public GitHubRepo(IGithubRepo githubRepo)
        {
            this.githubRepo = githubRepo;
        }

        public GitHubRepo(string gitHubUrl, string branch, string userName, string email):this(new GitHubRepo(gitHubUrl, branch, userName, email))
        {

        }

        public T GetElement(string path)
        {
            var element = githubRepo.Get(path);
            return JsonConvert.DeserializeObject<T>(element, new JavaScriptDateTimeConverter());
        }

        public void SaveFile(string path, string message, T element)
        {
            var serialize = JsonConvert.SerializeObject(element, new JavaScriptDateTimeConverter());
            githubRepo.SaveFile(path, message, serialize);
        }
    }
}
