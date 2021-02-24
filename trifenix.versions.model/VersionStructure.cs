using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace trifenix.versions.model
{

    /// <summary>
    /// Estructura de un paquete y sus versiones, a ser almacenada
    /// en en repositorio
    /// </summary>
    public class VersionStructure : IEquatable<VersionStructure>
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
        /// Paquetes de los que depende.
        /// </summary>
        public List<CommitPackageVersion> DependantVersions { get; set; } = new List<CommitPackageVersion>();


        /// <summary>
        /// listado con los paquetes dependientes del paquete principal.
        /// </summary>
        public List<Dependency> Dependencies { get; set; }


        /// <summary>
        /// Crea un hash para comparar un objeto versionStructure con otro.
        /// </summary>
        /// <param name="other">versionStructure a comparar</param>
        /// <returns>true si es igual el hash.</returns>
        public bool Equals(VersionStructure other)
        {
            var origin = this;
            var destiny = other;
            var serializeOrigin = JsonConvert.SerializeObject(origin, new JavaScriptDateTimeConverter());
            var serializeDestiny = JsonConvert.SerializeObject(destiny, new JavaScriptDateTimeConverter());
            var hashOrigin = Hash.ComputeSha256Hash(serializeOrigin);
            var hashDestiny = Hash.ComputeSha256Hash(serializeDestiny);

            return hashOrigin.Equals(hashDestiny);

        }

        
    }


}
