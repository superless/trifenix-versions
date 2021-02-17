using System;
using System.Collections.Generic;
using System.Text;

namespace trifenix.versions.model
{

    /// <summary>
    /// Estructura de un paquete y sus versiones, a ser almacenada
    /// en en repositorio
    /// </summary>
    public class VersionStructure
    {
        /// <summary>
        /// Nombre del paquete.
        /// </summary>
        public string PackageName { get; set; }


        /// <summary>
        /// Tipo del paquete (json / nuget).
        /// </summary>
        public PackageType PackageType { get; set; }

        /// <summary>
        /// repositorio de github del paquete en https.
        /// </summary>
        public string GithubHttp { get; set; }

        /// <summary>
        /// Repositori del github del paquete en ssh.
        /// </summary>
        public string GithubSsh { get; set; }


        /// <summary>
        /// todas las versiones que se han generado.
        /// </summary>
        public List<CommitVersion> Versions { get; set; } = new List<CommitVersion>();


        /// <summary>
        /// listado con los paquetes dependientes del paquete principal.
        /// </summary>
        public List<Dependency> Dependencies { get; set; }


    }


}
