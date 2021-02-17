using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using trifenix.versions.model;

namespace trifenix.arguments
{


    /// <summary>
    /// Argumentos para generar una nueva versión del paquete.
    /// </summary>
    [Verb("update", HelpText = "Actualiza el modelo de datos en github, con la nueva versión a generar y la retorna")]
    public class UpdateVersionArgs
    {
        /// <summary>
        /// github donde se almacenará la estructura de versiones.
        /// </summary>
        [Value(0, Required = true, HelpText = "Url o ssh de la url de git del proyecto, la gestión de versiones se hará en este repositorio")]
        public string GitAddress { get; set; }

        /// <summary>
        /// Usuario que registra el cambio en el repositorio del componente
        /// </summary>

        [Option('u', "user", Required = true, HelpText = "Usuario que registra el cambio en el repositorio del componente", Default = "devops")]
        public string username { get; set; }


        /// <summary>
        /// correo que registra el cambio en el repositorio del componente
        /// </summary>
        [Option('e', "email", Required = true, HelpText = "correo que registra el cambio en el repositorio del componente", Default = "devops@trifenix.io")]
        public string email { get; set; }

        /// <summary>
        /// Rama que será sobrescrita en el github
        /// </summary>
        [Option('b', "branch", Required = false, HelpText = "rama que generará la versión", Default = "master")]
        public string branch { get; set; }

        /// <summary>
        /// Token del github.
        /// </summary>
        [Option('t', "token", Required = true, HelpText = "token de github")]
        public string Token { get; set; }


        /// <summary>
        /// build de azure devops.
        /// </summary>
        [Option('b', "build", Required = false, HelpText = "Build, no requerido para la rama master.")]
        public string packageName { get; set; }


        /// <summary>
        /// nombre del paquete a actualizar.
        /// </summary>
        [Option('n', "name", Required = true, HelpText = "nombre del paquete")]
        public string Build { get; set; }

        /// <summary>
        /// Tipo del paquete
        /// </summary>
        [Option('k', "ktype", Required = true, HelpText = "tipo del paquete")]
        public PackageType packageType { get; set; }

        /// <summary>
        /// si la rama depende del release, se usará release para la nueva versión, sino la master.
        /// </summary>
        [Option('d', "dependant", Required = false, Default = false, HelpText = "si la actualización depende del release, se usará release para la nueva versión, sino la master")]
        public bool DependantRelease { get; set; }


        /// <summary>
        /// muestra de valores de la clase
        /// </summary>
        /// <returns>valores de la clase, solo por debug.</returns>
        public override string ToString()
        {
            return $@"gitaddress = {GitAddress} \n username = {username} \n email = {email} \n branch = {branch} \n token = {Token} \n package-name={packageName} \n package-type:{packageType} \n build:{Build} \n dependant = {DependantRelease}";
        }
    }
}
