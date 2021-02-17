namespace trifenix.versions.model
{
    /// <summary>
    /// Dependencia de un paquete
    /// (no usa el tipo de paquete, dado que se asume por la dependencia de VersionStructure)
    /// </summary>
    public class Dependency {

        /// <summary>
        /// Nombre del paquete dependiente
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// Repositorio del paquete en http
        /// </summary>
        public string GithubHttp { get; set; }

        /// <summary>
        /// Repositorio del paquete en ssh.
        /// </summary>
        public string GithubSsh { get; set; }

        /// <summary>
        /// Ruta del archivo donde se actualizará la versión.
        /// </summary>
        public string pathPackageSettings { get; set; }

    }


}
