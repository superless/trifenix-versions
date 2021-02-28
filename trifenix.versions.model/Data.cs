using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using trifenix.versions.model;

namespace trifenix.versions.model
{
    /// <summary>
    /// Información usada en los tests
    /// </summary>
    public static class Data
    {


        public static VersionStructure Interfaces = new VersionStructure
        {
            PackageName = "trifenix.connect.interfaces",
            GithubHttp = "https://github.com/trifenix/interfaces-connect.git",
            GithubSsh = "git@github.com:trifenix/interfaces-connect.git",
            PackageType = PackageType.nuget,
            Versions = new List<CommitVersion>{
                    new CommitVersion{
                    Branch="master",
                    DependantRelease=false,
                    IsFeature=false,
                    PreReleaseLabel=string.Empty,
                    Preview=0,
                    SemanticBaseVersion=new Semantic { // actual versión
                        Major = 0,
                        Minor = 8,
                        Patch=13
                    }
                }
            },
            Dependencies = new List<Dependency>{
                new Dependency{
                     PackageName = "trifenix.connect.graph",
                     GithubHttp = "https://github.com/trifenix/graph.git",
                     GithubSsh = "git@github.com:trifenix/graph.git",
                    pathPackageSettings="trifenix.connect.graph.csproj"
                },
                new Dependency{
                    PackageName = "trifenix.connect.aad.auth",
                    GithubHttp = "https://github.com/trifenix/connect-azr-auth.git",
                    GithubSsh = "git@github.com:trifenix/connect-azr-auth.git",
                    pathPackageSettings="trifenix.connect.aad.auth.csproj"
                },
                new Dependency{
                    PackageName = "trifenix.connect.email",
                    GithubHttp = "https://github.com/trifenix/trifenix-connect-email.git",
                    GithubSsh = "git@github.com:trifenix/trifenix-connect-email.git",
                    pathPackageSettings="trifenix.connect.email.csproj"
                },
                new Dependency{
                    PackageName = "trifenix.connect.translate",
                    GithubHttp = "https://github.com/trifenix/connect-translate.git",
                    GithubSsh = "git@github.com:trifenix/connect-translate.git",
                    pathPackageSettings="trifenix.connect.translate.csproj"
                },
                new Dependency{
                        PackageName="trifenix.connect.db.cosmos",
                        GithubHttp="https://github.com/trifenix/trifenix-cosmos-db.git",
                        GithubSsh="git@github.com:trifenix/trifenix-cosmos-db.git",
                        pathPackageSettings="trifenix.connect.db.cosmos.csproj"
                    },
                new Dependency{
                        PackageName = "trifenix.connect.search", // los paquetes de npm usan trifenix al principio
                        GithubHttp = "https://github.com/trifenix/connect-search.git",
                        GithubSsh = "git@github.com:trifenix/connect-search.git",
                        pathPackageSettings="trifenix.connect.search.csproj"
                    }
            }
        };

        public static VersionStructure Translate => new VersionStructure
        {
            PackageName = "trifenix.connect.translate",
            GithubHttp = "https://github.com/trifenix/connect-translate.git",
            GithubSsh = "git@github.com:trifenix/connect-translate.git",
            PackageType = PackageType.nuget,
            Versions = new List<CommitVersion>{
                    new CommitVersion {
                        Branch="master",
                        DependantRelease=false,
                        IsFeature=false,
                        PreReleaseLabel=string.Empty,
                        Preview=0,
                        SemanticBaseVersion=new Semantic{ // actual versión
                            Major = 0,
                            Minor = 8,
                            Patch= 52
                        }
                    }
                },
        };

        public static VersionStructure Email => new VersionStructure
        {
            PackageName = "trifenix.connect.email",
            GithubHttp = "https://github.com/trifenix/trifenix-connect-email.git",
            GithubSsh = "git@github.com:trifenix/trifenix-connect-email.git",
            PackageType = PackageType.nuget,
            Versions = new List<CommitVersion>{
                    new CommitVersion {
                        Branch="master",
                        DependantRelease=false,
                        IsFeature=false,
                        PreReleaseLabel=string.Empty,
                        Preview=0,
                        SemanticBaseVersion=new Semantic{ // actual versión
                            Major = 0,
                            Minor = 8,
                            Patch= 52
                        }
                    }
                },
        };


        public static VersionStructure Graph => new VersionStructure
        {
            PackageName = "trifenix.connect.graph",
            GithubHttp = "https://github.com/trifenix/graph.git",
            GithubSsh = "git@github.com:trifenix/graph.git",
            PackageType = PackageType.nuget,
            Versions = new List<CommitVersion>{
                    new CommitVersion {
                        Branch="master",
                        DependantRelease=false,
                        IsFeature=false,
                        PreReleaseLabel=string.Empty,
                        Preview=0,
                        SemanticBaseVersion=new Semantic{ // actual versión
                            Major = 0,
                            Minor = 8,
                            Patch= 53
                        }
                    }
                },
        };

        public static VersionStructure Auth => new VersionStructure
        {
            PackageName = "trifenix.connect.aad.auth",
            GithubHttp = "https://github.com/trifenix/connect-azr-auth.git",
            GithubSsh = "git@github.com:trifenix/connect-azr-auth.git",
            PackageType = PackageType.nuget,
            Versions = new List<CommitVersion>{
                    new CommitVersion {
                        Branch="master",
                        DependantRelease=false,
                        IsFeature=false,
                        PreReleaseLabel=string.Empty,
                        Preview=0,
                        SemanticBaseVersion=new Semantic{ // actual versión
                            Major = 0,
                            Minor = 8,
                            Patch= 22
                        }
                    }
                },
        };

        public static VersionStructure Arguments => new VersionStructure
        {
            PackageName = "trifenix.connect.arguments",
            GithubHttp = "https://github.com/trifenix/trifenix-connect-arguments.git",
            GithubSsh = "git@github.com:trifenix/trifenix-connect-arguments.git",
            PackageType = PackageType.nuget,
            Versions = new List<CommitVersion>{
                    new CommitVersion {
                        Branch="master",
                        DependantRelease=false,
                        IsFeature=false,
                        PreReleaseLabel=string.Empty,
                        Preview=0,
                        SemanticBaseVersion=new Semantic{ // actual versión
                            Major = 0,
                            Minor = 8,
                            Patch= 48
                        }
                    }
                },
            Dependencies = new List<Dependency>{

                    new Dependency{
                        PackageName="trifenix.connect.db.cosmos",
                        GithubHttp="https://github.com/trifenix/trifenix-cosmos-db.git",
                        GithubSsh="git@github.com:trifenix/trifenix-cosmos-db.git",
                        pathPackageSettings="trifenix.connect.db.cosmos.csproj"
                    }


                }

        };

        public static VersionStructure Cosmos => new VersionStructure
        {
            PackageName = "trifenix.connect.db.cosmos",
            GithubHttp = "https://github.com/trifenix/trifenix-cosmos-db.git",
            GithubSsh = "git@github.com:trifenix/trifenix-cosmos-db.git",
            PackageType = PackageType.nuget,
            Versions = new List<CommitVersion>{
                    new CommitVersion {
                        Branch="master",
                        DependantRelease=false,
                        IsFeature=false,
                        PreReleaseLabel=string.Empty,
                        Preview=0,
                        SemanticBaseVersion=new Semantic{ // actual versión
                            Major = 0,
                            Minor = 8,
                            Patch= 54
                        }
                    }
                }
        };



        public static VersionStructure Exceptions => new VersionStructure
        {
            PackageName = "trifenix.exception",
            GithubHttp = "https://github.com/trifenix/trifenix.exception.git",
            GithubSsh = "git@github.com:trifenix/trifenix.exception.git",
            PackageType = PackageType.nuget,
            Versions = new List<CommitVersion>{
                    new CommitVersion {
                        Branch="master",
                        DependantRelease=false,
                        IsFeature=false,
                        PreReleaseLabel=string.Empty,
                        Preview=0,
                        SemanticBaseVersion=new Semantic{ // actual versión
                            Major = 0,
                            Minor = 1,
                            Patch= 31
                        }
                    }
                },
            Dependencies = new List<Dependency>{

                    new Dependency{
                         PackageName = "trifenix.connect",
                        GithubHttp = "https://github.com/trifenix/connect.git",
                        GithubSsh = "git@github.com:trifenix/connect.git",
                        pathPackageSettings="trifenix.connect.csproj"
                    }


                }

        };





        public static VersionStructure Connect => new VersionStructure
        {
            PackageName = "trifenix.connect",
            GithubHttp = "https://github.com/trifenix/connect.git",
            GithubSsh = "git@github.com:trifenix/connect.git",
            PackageType = PackageType.nuget,
            Versions = new List<CommitVersion>{
                    new CommitVersion {
                        Branch="master",
                        DependantRelease=false,
                        IsFeature=false,
                        PreReleaseLabel=string.Empty,
                        Preview=0,
                        SemanticBaseVersion=new Semantic{ // actual versión
                            Major = 0,
                            Minor = 8,
                            Patch= 86
                        }
                    }
                },
            Dependencies = new List<Dependency>{
                  
                    new Dependency{
                        PackageName="trifenix.connect.bus",
                        GithubHttp="https://github.com/trifenix/trifenix-connect-bus.git",
                        GithubSsh="git@github.com:trifenix/trifenix-connect-bus.git",
                        pathPackageSettings="trifenix.connect.bus.csproj"
                    },

                }

        };


        public static VersionStructure SearchModel => new VersionStructure {
                PackageName="trifenix.connect.mdm.search.model",
                GithubHttp="https://github.com/trifenix/connect-azr-search-model.git",
                GithubSsh="git@github.com:trifenix/connect-azr-search-model.git",
                PackageType= PackageType.nuget,
                Versions = new List<CommitVersion>{
                    new CommitVersion {
                        Branch="master",
                        DependantRelease=false,
                        IsFeature=false,
                        PreReleaseLabel=string.Empty,
                        Preview=0,
                        SemanticBaseVersion=new Semantic{ // actual versión
                            Major = 0,
                            Minor = 8,
                            Patch= 50
                        }
                    }
                },
                Dependencies = new List<Dependency> {
                    
                    new Dependency{
                        PackageName = "trifenix.connect.search", // los paquetes de npm usan trifenix al principio
                        GithubHttp = "https://github.com/trifenix/connect-search.git",
                        GithubSsh = "git@github.com:trifenix/connect-search.git",
                        pathPackageSettings="trifenix.connect.search.csproj"
                    }
                }
            };

        public static VersionStructure Bus => new VersionStructure
        {
            PackageName = "trifenix.connect.bus",
            GithubHttp = "https://github.com/trifenix/trifenix-connect-bus.git",
            GithubSsh = "git@github.com:trifenix/trifenix-connect-bus.git",
            PackageType = PackageType.nuget,
            Versions = new List<CommitVersion>{
                    new CommitVersion {
                        Branch="master",
                        DependantRelease=false,
                        IsFeature=false,
                        PreReleaseLabel=string.Empty,
                        Preview=0,
                        SemanticBaseVersion=new Semantic{ // actual versión
                            Major = 0,
                            Minor = 8,
                            Patch= 47
                        }
                    }
                },
        };

        


        public static VersionStructure Mdm = new VersionStructure
        {
            PackageName = "trifenix.connect.mdm",
            GithubHttp = "https://github.com/trifenix/mdm.git",
            GithubSsh = "git@github.com:trifenix/mdm.git",
            PackageType = PackageType.nuget,
            Versions = new List<CommitVersion>{
                    new CommitVersion{
                    Branch="master",
                    DependantRelease=false,
                    IsFeature=false,
                    PreReleaseLabel=string.Empty,
                    Preview=0,
                    SemanticBaseVersion=new Semantic { // actual versión
                        Major = 0,
                        Minor = 8,
                        Patch=149
                    }
                }
            },
            Dependencies = new List<Dependency>{
                new Dependency{
                    PackageName="trifenix.connect",
                    GithubHttp="https://github.com/trifenix/connect.git",
                    GithubSsh="git@github.com:trifenix/connect.git",
                    pathPackageSettings="connect/trifenix.connect.csproj"
                },
                 new Dependency{
                    PackageName = "trifenix.connect.interfaces",
                    GithubHttp = "https://github.com/trifenix/interfaces-connect.git",
                    GithubSsh = "git@github.com:trifenix/interfaces-connect.git",
                    pathPackageSettings="trifenix.connect.interfaces.csproj"
                },
                new Dependency{
                    PackageName="trifenix.connect.mdm.search.model",
                    GithubHttp="https://github.com/trifenix/connect-azr-search-model.git",
                    GithubSsh="git@github.com:trifenix/connect-azr-search-model.git",
                    pathPackageSettings="trifenix.connect.mdm.search.model.csproj"
                },
            }
        };

        public static VersionStructure MdmNpm = new VersionStructure
        {
            PackageName = "trifenix@mdm", // los paquetes de npm usan trifenix al principio
            GithubHttp = "https://github.com/trifenix/mdm-auto-npm-model.git",
            GithubSsh = "git@github.com:trifenix/mdm-auto-npm-model.git",
            PackageType = PackageType.npm,
            Versions = new List<CommitVersion>{
                    new CommitVersion{
                        Branch="master",
                        DependantRelease=false,
                        IsFeature=false,
                        PreReleaseLabel=string.Empty,
                        Preview=0,
                        SemanticBaseVersion=new Semantic { // actual versión
                            Major = 1,
                            Minor = 6,
                            Patch=24
                        }
}
                },
            Dependencies = new List<Dependency>{
                    new Dependency{
                        PackageName="@trifenix/agro-data",
                        GithubHttp="https://github.com/trifenix/agro-data.git",
                        GithubSsh="git@github.com:trifenix/agro-data.git",
                        pathPackageSettings="package.json"
                    }
                }
        };


        public static VersionStructure Connectsearch = new VersionStructure
        {
            PackageName = "trifenix.connect.search", // los paquetes de npm usan trifenix al principio
            GithubHttp = "https://github.com/trifenix/connect-search.git",
            GithubSsh = "git@github.com:trifenix/connect-search.git",
            PackageType = PackageType.nuget,
            Versions = new List<CommitVersion>{
                    new CommitVersion{
                        Branch="master",
                        DependantRelease=false,
                        IsFeature=false,
                        PreReleaseLabel=string.Empty,
                        Preview=0,
                        SemanticBaseVersion=new Semantic { // actual versión
                            Major = 0,
                            Minor = 8,
                            Patch=47
                        }
}
                }
        };


        /// <summary>
        /// Listado de dependencias a ser usada en la aplicación.
        /// </summary>
        public static VersionStructure[] Packages { get; set; } = new VersionStructure[] {

            Arguments, Connect,SearchModel, Bus, Mdm,MdmNpm, Graph, Auth, Email, Interfaces, Translate, Exceptions, Cosmos


         };

        public static MapperConfiguration Configuration<T1,T2>() {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T1, T2>();
            });

        }

      

        public static IMapper Mapper => Configuration<VersionStructure, VersionStructure>().CreateMapper();

        public static IMapper MapperCommitVersion => Configuration<CommitVersion, CommitPackageVersion>().CreateMapper();





    }
}
