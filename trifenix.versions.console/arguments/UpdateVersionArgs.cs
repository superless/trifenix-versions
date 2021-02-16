using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace trifenix.arguments
{


    /// <summary>
    /// Argumentos para generar una nueva versión del paquete.
    /// </summary>
    [Verb("update", HelpText = "Actualiza el modelo de datos en github, con la nueva versión a generar y la retorna")]
    public class UpdateVersionArgs
    {
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
        /// 
        /// </summary>
        [Option('t', "token", Required = true, HelpText = "token de github")]
        public string Token { get; set; }



    }
}
