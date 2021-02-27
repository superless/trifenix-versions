using System;
using trifenix.versions.model;

namespace trifenix.versions.interfaces
{

    /// <summary>
    /// Interface principal del proyecto trifenix-versions,
    /// Usará un repositorio de almacenamiento de datos, inicialmente github.
    /// </summary>
    public interface IVersionSpec
    {
        /// <summary>
        /// Obtiene el label de acuerdo a la rama.
        /// </summary>
        /// <returns>release label</returns>
        string GetPreReleaseLabel();

        /// <summary>
        /// Obtiene una estructura de dependencias de un paquete desde un repositorio,
        /// devuelve uno por defecto, sino lo encuentra
        /// </summary>
        /// <returns>Estructura del paquete y sus dependenicias</returns>
        VersionStructure GetVersionStructure(Action<string> message);


        /// <summary>
        /// Actualiza la versión del paquete dentro de la estructura.
        /// </summary>
        /// <param name="versionStructure">estructura previa</param>
        /// <returns>VersionStructure con la nueva versión aplicada</returns>
        VersionStructure SetMainVersion(VersionStructure versionStructure);


        /// <summary>
        /// Toma los valores del constructor para configurar una nueva versión
        /// y persistirla en un repositorio.
        /// </summary>
        /// <returns>Nueva versión generada</returns>
        string SetVersion(Action<string> message);


        /// <summary>
        /// Actualiza el paquete en cada github de cada dependiente.
        /// </summary>
        /// <param name="eventMessage">evento que captura los mensajes de la propagación.</param>
        void SetVersionToDependant(Action<string> eventMessage);

        /// <summary>
        /// Función personalizada que permite guardar o actualizar en github un versionStructure
        /// Se usa para asignar las últimas versiones de los paquetes, de las cuales un componente depende
        /// Se guarda una herencia de CommitVersion que incluye el PackageName
        /// </summary>
        /// <param name="eventMessage">evento que captura los mensajes de la propagación.</param>
        void SaveVersionStructure(VersionStructure versionStructure);


        /// <summary>
        /// Método que permitirá obtener el csproj con la nueva versión para ser persistida.
        /// La rama y lo necesario para persistir vendrá desde el constructor.
        /// </summary>
        /// <param name="dependency">la dependencia que se debe actualizar</param>
        /// <param name="commit">Parámetros del último commit</param>
        /// <param name="folder">carpeta donde buscar el archivo</param>
        /// <returns>csproj en memoria con la nueva versión</returns>
        string SetCsProjNugetVersion(Dependency dependency, CommitVersion commit, string folder);

        /// <summary>
        /// Actualiza el package json con la nueva versión.
        /// </summary>
        /// <param name="dependency">dependencia a actualizar</param>
        /// <param name="commit">datos de la última versión</param>
        /// <returns></returns>
        string SetPackageJsonNpmVersion(Dependency dependency, CommitVersion commit, string folder);


       

    }


}
