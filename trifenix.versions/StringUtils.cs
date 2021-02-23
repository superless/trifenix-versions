using System;
using System.IO;
using System.Text;
using trifenix.versions.interfaces;
using trifenix.versions.model;

namespace trifenix.versions
{
    public class StringUtils : IStringUtils
    {
        public string GetFeatureName(string branch)
        {
            if (branch.ToLower().StartsWith("feature/"))
            {
                return branch.Split('/')[1];
            }

            return branch;
        }

        public string GetPackageFullPath(string folder, string packageName, PackageType type)
        {
            if (string.IsNullOrWhiteSpace(folder))
            {
                return $"{packageName}.{type}.json";
            }
            return Path.Combine(folder, $"{packageName}.{type}.json");
        }




        public Semantic GetSemanticVersionFromRelease(string branch)
        {
            if (!branch.ToLower().StartsWith("release/"))
            {
                return null;
            }

            var errorMessage = "es un release, pero el formato no obedece a una versión semántica";

            var versionRelease = branch.Split(new char[] { '/' }, 2)[1];

            var numbers = versionRelease.Split(new char[] { '.' }, 3);

            if (!int.TryParse(numbers[0],  out var major))
            {
                throw new Exception(errorMessage);
            }

            if (!int.TryParse(numbers[1], out var minor))
            {
                throw new Exception(errorMessage);
            }

            if (!int.TryParse(numbers[2], out var patch))
            {
                throw new Exception(errorMessage);
            }


            return new Semantic {
                Major = major,
                Minor = minor,
                Patch = patch
            };


        }

        public string SetGithubToken(string repository, string token, string username)
        {
            var uriRepo = new Uri(repository);

            return $"https://{username}:{token}@{uriRepo.DnsSafeHost}{uriRepo.LocalPath}";
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
    }
}
