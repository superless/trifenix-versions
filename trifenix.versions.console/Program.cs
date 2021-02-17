using Colorful;
using CommandLine;
using System;
using System.Drawing;
using System.IO;
using trifenix.arguments;

namespace trifenix.versions.console
{

    /// <summary>
    /// # trifenix-versions
    /// Aplicación para la actualización de github de paquetes dependientes de una versión
    /// y para generar nuevas versiones de acuerdo a la rama.
    /// </summary>
    class Program
    {

        /// <summary>
        /// Punto de arranque
        /// </summary>
        /// <param name="args">usamos CommandLineParser para parsear las opciones</param>
        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<UpdateVersionArgs, object>(args);

            var currentDitectory = AppDomain.CurrentDomain.BaseDirectory;
            var fontPath = Path.Combine(currentDitectory, "figlet/small");
            var fontTitle = new Figlet(Colorful.FigletFont.Load(fontPath));

            if (result.Tag == ParserResultType.Parsed)
            {
                Colorful.Console.WriteLine("Hello Arguments", Color.Red);
                
                result.WithParsed<UpdateVersionArgs>(s => Colorful.Console.WriteLine(s, Color.Blue));
            }


            
        }
    }
}
