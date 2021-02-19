using System;
using trifenix.git.interfaces;
using trifenix.versions.interfaces;
using trifenix.versions.model;

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
        private readonly bool releaseDependant;
        private readonly IGithubRepo repo;
        private readonly IGithubRepo<VersionStructure> repoVersion;


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
        public VersionSpec(string githubRepo, string branch, string build, string token, string packageName, PackageType packageType, bool releaseDependant, IGithubRepo repo, IGithubRepo<VersionStructure> repoVersion)
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
        }
        public string GetFileStringFromPath(string path)
        {
            throw new NotImplementedException();
        }

        public VersionStructure GetVersionStructure()
        {
            throw new NotImplementedException();
        }

        public string SetCsProjNugetVersion(Dependency dependency, CommitVersion commit)
        {
            throw new NotImplementedException();
        }

        public VersionStructure SetMainVersion(VersionStructure versionStructure)
        {
            throw new NotImplementedException();
        }

        public string SetPackageJsonNpmVersion(Dependency dependency, CommitVersion commit)
        {
            throw new NotImplementedException();
        }

        public string SetVersion()
        {
            // considerar que el preview no sea infinito.
            throw new NotImplementedException();
        }

        public void SetVersionToDependant()
        {
            throw new NotImplementedException();
        }
    }
}
