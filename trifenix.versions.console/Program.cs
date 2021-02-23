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
            

            var currentDitectory = AppDomain.CurrentDomain.BaseDirectory;
            var fontPath = Path.Combine(currentDitectory, "figlet/small");
            var fontTitle = new Figlet(Colorful.FigletFont.Load(fontPath));

            Parser.Default.ParseArguments<UpdateVersionArgs, Propagation>(args).MapResult(
                (UpdateVersionArgs s)=> {
                    Update(s);
                    return 1;
                }, 
                (Propagation s) => {
                    ProgragationMethod(s);
                    return 1;
                },
                err=>1
                );


            

        }


        /// <summary>
        /// Actualización del paquete en los distintas dependencias.
        /// </summary>
        /// <param name="args"></param>
        public static void ProgragationMethod(Propagation args) {
            var versionSpec = new VersionSpec(args.GitAddress, args.branch, args.Token, args.packageName, args.packageType, args.DependantRelease, args.username, args.email);

            versionSpec.SetVersionToDependant();
        }



        public static void Update(UpdateVersionArgs args) {

            var versionSpec = new VersionSpec(args.GitAddress, args.branch, args.Token, args.packageName, args.packageType, args.DependantRelease, args.username, args.email, args.Build);

            var version = versionSpec.SetVersion();

            System.Console.WriteLine(version);
            
        }
    }
}
