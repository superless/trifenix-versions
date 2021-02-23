using System;
using System.Collections.Generic;

namespace trifenix.git.interfaces
{

    /// <summary>
    /// Operaciones de github tipada, que permitirá poder persistir elementos tipados en un github
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGithubRepo<T>
    {
        /// <summary>
        /// Guarda un elemento en github.
        /// </summary>
        /// <param name="path">ruta del repositorio, donde será guardado</param>
        /// <param name="message">mensaje (normalmente sería la versión)</param>
        /// <param name="element">elemento a guardar</param>
        void SaveFile(string path, string message, T element);

        /// <summary>
        /// Obtiene un elemento tipado desde una ruta
        /// </summary>
        /// <param name="path">ruta del elemento a obtener</param>
        /// <param name="force">Forza un Clone</param>
        /// <param name="branch">Rama de la que obtendrá el elemento (por defecto es master)</param>
        /// <returns>Elemento deserializado</returns>
        T GetElement(string path);

        

        

    }

    /// <summary>
    /// Operaciones sobre github
    /// </summary>
    public interface IGithubRepo {

        /// <summary>
        /// Guarda un string en un archivo, en un repositorio.
        /// </summary>
        /// <param name="path">la ruta del archivo a guardar.</param>
        /// <param name="message">mensaje en el commit</param>
        /// <param name="element">elemento en string que será guardado</param>
        void SaveFile(string path, string message, string element);

        /// <summary>
        /// Obtiene el elemento en string de una ruta definida.
        /// </summary>
        /// <param name="path">ruta del archivo a buscar</param>
        /// <returns>Elemento en string</returns>
        string Get(string path);

        /// <summary>
        /// Commits a una rama
        /// el mensaje quedará como tag.
        /// </summary>        
        /// <param name="commitMessageFileOperations">colección de key value, donde el key es el mensaje del commit y el value una acción que cambia archivos del repositorio</param>
        /// <param name="message">mensaje en el commit</param>
        void Commit(IEnumerable<Func<bool>> commitMessageFileOperations, string message);


        /// <summary>
        /// Clona el proyecto de la rama que mantiene el objeto github
        /// generando una nueva carpeta temporal y retornandola al finalizar el clonado.        
        /// </summary>
        /// <returns>Carpeta temporal con el repositorio</returns>
        string Clone();
    }
}
