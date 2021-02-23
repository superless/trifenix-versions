using System;
using System.Collections.Generic;
using System.Text;
using trifenix.versions.model;

namespace trifenix.versions.interfaces
{
    public interface IStringUtils
    {
        /// <summary>
        /// Crea el string repositorio formado por el repositorio original, el usuario y el token.
        /// </summary>
        /// <param name="repository">repositorio original</param>
        /// <param name="token">token a incluir en el repositorio</param>
        /// <param name="username">usuario a incluir en el repositorio</param>
        /// <returns>repositorio de github con token de usuario y usuario</returns>
        string SetGithubToken(string repository, string token, string username);


        /// <summary>
        /// Retorna la versión de una rama release.
        /// </summary>
        /// <param name="branch">nombre de la rama release</param>
        /// <returns>Versión semantica del release</returns>
        Semantic GetSemanticVersionFromRelease(string branch);

        /// <summary>
        /// Obtiene el nombre del feature de una branch.
        /// </summary>
        /// <param name="branch">Branch a evaluar para obtener el feature</param>
        /// <returns>nombre de la feature si la encuentra, sino el nombre de la rama</returns>
        string GetFeatureName(string branch);


        


        /// <summary>
        /// Obtiene la ruta completa de un archivo a persistir en github.
        /// </summary>
        /// <param name="folder">carpeta donde estará el archivo</param>
        /// <param name="packageName">nombre del paquete</param>
        /// <param name="type">tipo de paquete</param>
        /// <returns></returns>
        string GetPackageFullPath(string folder, string packageName, PackageType type);


    }

    
}
