using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using trifenix.git.interfaces;
using trifenix.versions.interfaces;
using trifenix.versions.model;
using Microsoft.XmlDiffPatch;
using trifenix.git;

namespace trifenix.versions
{

    /// <summary>
    /// componente principal, en el se ejecutarán todos los procesos que realiza el programa.
    /// </summary>
    public class VersionSpec : IVersionSpec
    {
        private readonly string githubRepo;
        private readonly string branch;
        private readonly string build;
        private readonly string token;
        private readonly string packageName;
        private readonly PackageType packageType;
        private bool releaseDependant;
        private readonly IGithubRepo repo;
        private readonly IGithubRepo<VersionStructure> repoVersion;
        private readonly List<VersionStructure> defaultVersions;
        private readonly string mail;
        private readonly StringUtils utils;
        private readonly string userGithub;


        /// <summary>
        /// Constructor de la clase principal de trifenix-versions.
        /// </summary>
        /// <param name="githubRepo">repositorio de github de las versiones</param>
        /// <param name="branch">rama de la que se quiere obtener la versión</param>
        /// <param name="build">build de la integración</param>
        /// <param name="token">token de github</param>
        /// <param name="packageName">Nombre del paquete a actualizar</param>
        /// <param name="packageType">tipo de paquete json o nuget</param>
        /// <param name="releaseDependant">define si la versión es para un nuevo release</param>
        /// <param name="repo">Interface para operaciones de github</param>
        /// <param name="repoVersion">Interface para operaciones de github tipadas</param>
        /// <param name="defaultVersions">Interface para operaciones de github tipadas</param>
        public VersionSpec(string githubRepo, string branch, string build, string token, string packageName, PackageType packageType, bool releaseDependant, string email, string user, IGithubRepo repo, IGithubRepo<VersionStructure> repoVersion, List<VersionStructure> defaultVersions)
        {
            this.githubRepo = githubRepo;
            this.branch = branch;
            this.build = build;
            this.token = token;
            this.packageName = packageName;
            this.packageType = packageType;
            this.releaseDependant = releaseDependant;
            this.repo = repo;
            this.repoVersion = repoVersion;
            this.defaultVersions = defaultVersions;
            this.mail = email;
            this.userGithub = user;
            utils = new StringUtils();
        }

        public VersionSpec(string githubRepo, string branch, string token, string packageName, PackageType packageType, bool releaseDependant, string userGithub, string mailGithub, string build = null) :
            this(githubRepo, 
                branch, 
                build, 
                token, 
                packageName, 
                packageType, 
                releaseDependant, 
                userGithub,
                mailGithub,
                new GitHubRepo(new StringUtils().SetGithubToken(githubRepo, token, userGithub), branch, userGithub, mailGithub), 
                new GitHubRepo<VersionStructure>(new GitHubRepo(new StringUtils().SetGithubToken(githubRepo, token, userGithub), "master", userGithub, mailGithub)), 
                Data.Packages.ToList())
        {
            
        }
        

        public VersionStructure GetVersionStructure(string name, PackageType type, Action<string> message, int intentos = 0)
        {
            VersionStructure githubStructure;
            try
            {
                githubStructure = repoVersion.GetElement($"{name}.{type}.json");
            }
            catch (Exception e)
            {
                message.Invoke("------------------------------------------------------------------");
                message.Invoke($"error al obtener la estructura, nro : {intentos}");
                message.Invoke("------------------------------------------------------------------");
                message.Invoke($"{e.Message}");
                message.Invoke("------------------------------------------------------------------");
                if (intentos>=3)
                {
                    throw new Exception("error al obtener estructura después de 3 intentos fallidos");
                }
                return GetVersionStructure(name, type, message, intentos++);

            }
            if (githubStructure != null) return githubStructure;
            var defaultValue = defaultVersions.FirstOrDefault(s => s.PackageName.Equals(name));
            return defaultValue;
        }

        public VersionStructure GetVersionStructure(Action<string> message) => GetVersionStructure(packageName, packageType, message);



        public string SetPackageJsonNpmVersion(Dependency dependency, CommitVersion commit, string folder)
        {
            var pathPackageJson = Path.Combine(folder, dependency.pathPackageSettings);

            var jsn = StringUtils.ReadAllText(pathPackageJson);

            var version = commit.ToString();

            var jobject = JsonConvert.DeserializeObject(jsn) as JObject;

            var dependenciesArray = jobject["dependencies"];

            if (dependenciesArray!= null&&dependenciesArray.HasValues)
            {
                var packageExists = dependenciesArray.Any(s => ((JProperty)s).Name.Equals(packageName));
                if (packageExists)
                {
                    var tkn = (JProperty)dependenciesArray.First(s => ((JProperty)s).Name.Equals(packageName));
                    tkn.Value = $"^{version}";
                    

                }
            }

            var devDependenciesArray = jobject["devDependencies"];

            if (devDependenciesArray!=null && devDependenciesArray.HasValues)
            {
                var packageExists = devDependenciesArray.Any(s => ((JProperty)s).Name.Equals(packageName));
                if (packageExists)
                {
                    var tkn = (JProperty)devDependenciesArray.First(s => ((JProperty)s).Name.Equals(packageName));
                    tkn.Value = $"^{version}";


                }
            }

            var peerDependenciesArray = jobject["peerDependencies"];

            if (peerDependenciesArray!=null && peerDependenciesArray.HasValues)
            {
                var packageExists = peerDependenciesArray.Any(s => ((JProperty)s).Name.Equals(packageName));
                if (packageExists)
                {
                    var tkn = (JProperty)peerDependenciesArray.First(s => ((JProperty)s).Name.Equals(packageName));
                    tkn.Value = $"^{version}";


                }
            }


            return jobject.ToString();
        }

        

        public string GetPreReleaseLabel()
        {
            if (branch.ToLower().Equals("master") || branch.ToLower().Equals("main"))
            {
                return string.Empty;
            }

            if (branch.ToLower().Equals("develop") && releaseDependant) return "preview-release";
            if (branch.ToLower().Equals("develop") && !releaseDependant) return "preview";

            if (branch.ToLower().Contains("release")) return "RC";
            
            return $"alpha-{utils.GetFeatureName(branch)}{(releaseDependant?"-release":"")}";




        }


        public string SetCsProjNugetVersion(Dependency dependency, CommitVersion commit, string folder)
        {

            var pathCsproj = Path.Combine(folder, dependency.pathPackageSettings);

            var version = commit.ToString();
            var xmlDoc = new XmlDocument();
            
            xmlDoc.Load(pathCsproj);

            for (int i = 0; i < xmlDoc.DocumentElement.ChildNodes.Count; i++)
            {
                var node = xmlDoc.DocumentElement.ChildNodes.Item(i);
                if (node.Name.Equals("ItemGroup"))
                {
                    var nodes = node.ChildNodes;
                    for (int x = 0; x < nodes.Count; x++)
                    {
                        var nodePackage = nodes.Item(x);
                        if (nodePackage.Name.Equals("PackageReference"))
                        {
                            var attributes = nodePackage.Attributes;

                            var attrInclude = attributes["Include"];

                            if (attrInclude != null && attrInclude.Value.Equals(packageName))
                            {
                                attributes["Version"].Value = $"{version}";
                            }

                            
                        }
                    }
                }
            }


            return ToXmlString(xmlDoc);
        }

        public static string ToXmlString(XmlDocument xmlDoc) {
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                xmlDoc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }


        public static bool CompareXml(string originalFile, string finalFile)
        {
            using (var stringWriter = new StringWriter())
            using (var writter = XmlWriter.Create(stringWriter))
            {
                XmlDiff xmldiff = new XmlDiff(XmlDiffOptions.IgnoreChildOrder |
                                             XmlDiffOptions.IgnoreNamespaces |
                                             XmlDiffOptions.IgnorePrefixes);

                var xmlReaderOrigin = XmlReader.Create(new StringReader(originalFile));
                var xmlReaderDestiny = XmlReader.Create(new StringReader(finalFile));

                return xmldiff.Compare(xmlReaderOrigin, xmlReaderDestiny);
            }

            
        }

       

        public VersionStructure SetMainVersion(VersionStructure spec)
        {   
            var newCommitVersion = GetNewCommitVersion(spec);
            spec.Versions.Add(newCommitVersion);
            return spec;
        }


        private CommitVersion GetNewCommitVersion(VersionStructure vers) {


            var versions = vers.Versions.ToList();
            var versionMasters = versions.Where(MasterWhere).ToList();
            var versionReleases = versions.Where(ReleaseWhere).ToList();


            // master
            var maxMajor = versionMasters.Max(s => s.SemanticBaseVersion.Major);
            var versionMasterMajors = versionMasters.Where(s => s.SemanticBaseVersion.Major.Equals(maxMajor)).ToList();
            var maxMinor = versionMasterMajors.Max(s => s.SemanticBaseVersion.Minor);
            var versionMasterPatchs = versionMasterMajors.Where(s => s.SemanticBaseVersion.Minor.Equals(maxMinor)).ToList();
            var maxPatch = versionMasterPatchs.Max(s => s.SemanticBaseVersion.Patch);

            var semanticMaster = new Semantic
            {
                Major = maxMajor,
                Minor = maxMinor,
                Patch = maxPatch
            };

            var existsReleases = versionReleases.Any();


            // se debe checkear si existe un release con el misma versión.
            var maxMajorRelease = existsReleases?versionReleases.Max(s => s.SemanticBaseVersion.Major):0;
            var versionReleasesMajors = versionReleases.Where(s => s.SemanticBaseVersion.Major.Equals(maxMajorRelease)).ToList();
            var maxMinorRelease = existsReleases?versionReleasesMajors.Max(s => s.SemanticBaseVersion.Minor):0;
            var versionReleasesPatchs = versionReleasesMajors.Where(s => s.SemanticBaseVersion.Minor.Equals(maxMinorRelease)).ToList();
            var maxPatchRelease = existsReleases?versionReleasesPatchs.Max(s => s.SemanticBaseVersion.Patch):0;



            // releases
            var semanticRelease = branch.ToLower().Contains("release")?utils.GetSemanticVersionFromRelease(branch):new Semantic { 
                Major = maxMajorRelease,
                Minor = maxMinorRelease,
                Patch = maxPatchRelease
            };

            bool ReleaseIsGreaterThanMaster = false;

            if (semanticRelease.Major >= maxMajor && semanticRelease.Minor >= maxMinor && semanticRelease.Patch > maxPatch)
            {
                ReleaseIsGreaterThanMaster = releaseDependant;

            }
            else {
                releaseDependant = false;
            }
            if (!ReleaseIsGreaterThanMaster && branch.ToLower().Contains("release"))
            {
                throw new Exception("no se puede procesar una rama release menor a la master");
            }
            if (maxMajorRelease.Equals(semanticRelease.Major) && maxMinorRelease.Equals(semanticRelease.Minor) && maxPatchRelease.Equals(semanticRelease.Patch) && branch.ToLower().Contains("release"))
            {
                throw new Exception("Ya existe una versión igual en el repotorio de versiones, para release");
            }

            var branchLabel = GetPreReleaseLabel();

            if ((branch.ToLower().Equals("master") || branch.ToLower().Equals("main")) && ReleaseIsGreaterThanMaster)
            {
                return new CommitVersion
                {
                    Branch = branch,
                    Build = build,
                    DependantRelease = true,
                    IsFeature = false,
                    LastUpdate = DateTime.Now,
                    PreReleaseLabel = string.Empty,
                    Preview = 0,
                    SemanticBaseVersion = new Semantic
                    {
                        Major = semanticRelease.Major,
                        Minor = semanticRelease.Minor,
                        Patch = semanticRelease.Major
                    }
                };
            }

            if (branch.ToLower().Equals("master") || branch.ToLower().Equals("main"))
            {
                return new CommitVersion
                {
                    Branch = branch,
                    Build = build,
                    DependantRelease = false,
                    IsFeature = false,
                    LastUpdate = DateTime.Now,
                    PreReleaseLabel = string.Empty,
                    Preview = 0,
                    SemanticBaseVersion = new Semantic
                    {
                        Major = maxMajor,
                        Minor = maxMinor,
                        Patch = maxPatch + 1 // aquí suma.
                    }
                };
            }

            if (branch.ToLower().Contains("release"))
            {
               

                return new CommitVersion
                {
                    Branch = branch,
                    Build = build,
                    DependantRelease = true,
                    IsFeature = false,
                    LastUpdate = DateTime.Now,
                    PreReleaseLabel = branchLabel,
                    Preview = 0,
                    SemanticBaseVersion = semanticRelease
                };
            }

            if (branch.ToLower().Contains("develop"))
            {
                var versionsDevelop = versions.Where(DevelopWhere).Where(s => s.DependantRelease == ReleaseIsGreaterThanMaster).ToList();

                var sem = (ReleaseIsGreaterThanMaster) ? semanticRelease : semanticMaster;
                var versionSemanticDevelop = versionsDevelop.Where(s => s.SemanticBaseVersion.Equals(sem));

                var maxPreview = versionSemanticDevelop.Any()?versionSemanticDevelop.Max(s => s.Preview):0;

                return new CommitVersion
                {
                    Branch = branch,
                    Build = build,
                    DependantRelease = ReleaseIsGreaterThanMaster,
                    IsFeature = false,
                    LastUpdate = DateTime.Now,
                    PreReleaseLabel = GetPreReleaseLabel(),
                    Preview = maxPreview + 1,
                    SemanticBaseVersion = sem
                };
            }

            var otherVersions = versions.Where(s=>s.Branch.Equals(branch)).Where(s => s.DependantRelease == ReleaseIsGreaterThanMaster).ToList();
            var semOthers = (ReleaseIsGreaterThanMaster) ? semanticRelease : semanticMaster;
            var versionSemanticOThers = otherVersions.Where(s => s.SemanticBaseVersion.Equals(semOthers));

            var maxPreviewOThers = versionSemanticOThers.Any()?versionSemanticOThers.Max(s => s.Preview):0;

            return new CommitVersion
            {
                Branch = branch,
                Build = build,
                DependantRelease = ReleaseIsGreaterThanMaster,
                IsFeature = false,
                LastUpdate = DateTime.Now,
                PreReleaseLabel = GetPreReleaseLabel(),
                Preview = maxPreviewOThers + 1,
                SemanticBaseVersion = semOthers
            };






        }

        private Func<CommitVersion, bool> MasterWhere => s => s.Branch.ToLower().Equals("master") || s.Branch.ToLower().Equals("main");
        private Func<CommitVersion, bool> ReleaseWhere => s => s.Branch.ToLower().Contains("release");

        private Func<CommitVersion, bool> DevelopWhere => s => s.Branch.ToLower().Equals("develop") || s.Branch.ToLower().Equals("development");





        private static CommitVersion GetLastVersion(VersionStructure version, string branch) {

            var versions = version.Versions.Where(s => s.Branch.Equals(branch));
            if (!versions.Any()) return null;


            var maxSemantinc = versions.Max(s => s.SemanticBaseVersion);



            var maxSemanticVersions = version.Versions.Where(s => s.SemanticBaseVersion.Equals(maxSemantinc));

            var lastDate = maxSemanticVersions.Max(s => s.LastUpdate);

            return maxSemanticVersions.First(s => s.LastUpdate.Equals(lastDate));
        }



        public string SetVersion(Action<string> message)
        {
            //
            
            var version = SetMainVersion(GetVersionStructure(message));

            var versTag = GetLastVersion(version, branch).ToString();

            var tag = $"{packageName}.{versTag}";

            
            var fileFullpath = utils.GetPackageFullPath(string.Empty, packageName, packageType);

            repoVersion.SaveFile(fileFullpath, tag, version);

            return versTag;

        }

        

        public void SetVersionToDependant(Action<string> eventMessage)
        {
            eventMessage.Invoke("Obteniendo estuctura del paquete desde github");
            var structure = GetVersionStructure(eventMessage);
            eventMessage.Invoke($"{packageName} obtenido desde github");


            // última versión de la rama actual.
            var lastVersion = GetLastVersion(structure, branch);
            eventMessage.Invoke($"la última versión de {packageName} es {lastVersion}");

            foreach (var item in structure.Dependencies)
            {
                eventMessage.Invoke($"----------------------------------");
                eventMessage.Invoke($"Dependencia : {item.PackageName}");
                eventMessage.Invoke($"----------------------------------");

                var struc = GetVersionStructure(item.PackageName, packageType, eventMessage);

                

                var last = GetLastVersion(struc, branch);

                eventMessage.Invoke($"La version actual de la dependencia es {item.PackageName} es {(last==null?"[no registrada]":last.ToString())}");


                if (last == null || last.DependantRelease == releaseDependant)
                {
                    eventMessage.Invoke($"-------------------------------------------------------------------------------");
                    eventMessage.Invoke($"-------------------------------------------------------------------------------");
                    eventMessage.Invoke($"se actualizará {packageName} a {lastVersion} en {item.PackageName}");
                    eventMessage.Invoke($"-------------------------------------------------------------------------------");
                    eventMessage.Invoke($"-------------------------------------------------------------------------------");
                    var updated = UpdateGithub(item, lastVersion, eventMessage);
                    eventMessage.Invoke($"-------------------------------------------------------------------------------");
                    eventMessage.Invoke($"-------------------------------------------------------------------------------");
                    eventMessage.Invoke($"Guardando versión paquete, para llevar el rastro de las actualizaciones.");
                    if (!updated)
                    {   
                        continue;
                    }
                    var versionPackage = GetVersionStructure(item.PackageName, packageType, eventMessage);
                    var versionPackageVersion = Data.MapperCommitVersion.Map<CommitPackageVersion>(lastVersion);
                    versionPackageVersion.PackageName = packageName;
                    versionPackage.DependantVersions.Add(versionPackageVersion);
                    eventMessage.Invoke($"Se asigna la última versión de {packageName} a los paquetes dependientes de {item.PackageName} con la version {lastVersion}");
                    
                    SaveVersionStructure(versionPackage);



                    
                }
                else {
                    eventMessage.Invoke($"{item.PackageName} ya estableció su continuidad (master/release)  debe cambiar dependant o crear un nuevo release de  {packageName}");
                }   
                
            }
        }

        private bool UpdateGithub(Dependency dependency, CommitVersion version, Action<string> eventMessage) {
            var gh = new GitHubRepo(new StringUtils().SetGithubToken(dependency.GithubHttp, token, userGithub), this.branch, this.userGithub, this.mail);

            string folder;

            try
            {
                folder = gh.Clone();
            }
            catch (Exception e)
            {
                                
                eventMessage.Invoke($"{dependency.PackageName} fallo al clonar con la rama {branch}  desde {dependency.GithubHttp}");
                eventMessage.Invoke($"este es el mensaje de error:");
                eventMessage.Invoke($"{e.Message}");
                return false;
            }

            eventMessage.Invoke($"-------------------------------------------------------------------------------");
            eventMessage.Invoke($"{dependency.PackageName} es clonado en la carpeta temporal  desde {dependency.GithubHttp}");
            eventMessage.Invoke($"-------------------------------------------------------------------------------");
            eventMessage.Invoke($"{folder}");
            eventMessage.Invoke($"-------------------------------------------------------------------------------");
            eventMessage.Invoke($" Desde url Github : {dependency.GithubHttp}");
            eventMessage.Invoke($"-------------------------------------------------------------------------------");
           




            string contentFile;
            eventMessage.Invoke($"Se obtiene {dependency.pathPackageSettings}");
            eventMessage.Invoke($"-------------------------------------------------------------------------------");
            eventMessage.Invoke($"-------------------------------------------------------------------------------");
            if (packageType == PackageType.npm)
            {   
                contentFile = SetPackageJsonNpmVersion(dependency, version, folder);

            }
            else {
                contentFile = SetCsProjNugetVersion(dependency, version, folder);
            }
            eventMessage.Invoke(contentFile);
            eventMessage.Invoke($"-------------------------------------------------------------------------------");
            eventMessage.Invoke($"-------------------------------------------------------------------------------");

            gh.SaveFile(dependency.pathPackageSettings, $"{packageName}.{version}", contentFile);
            eventMessage.Invoke($"El paquete {dependency.PackageName} ha actualizado {packageName} a {version}");
            return true;
        }

        public void SaveVersionStructure(VersionStructure versionStructure)
        {
            var version = SetMainVersion(versionStructure);

            var versTag = GetLastVersion(version, branch).ToString();

            var tag = $"{packageName}.{versionStructure.PackageName}.{versTag}";


            var fileFullpath = utils.GetPackageFullPath(string.Empty, packageName, packageType);

            repoVersion.SaveFile(fileFullpath, tag, version);
        }
    }
}
