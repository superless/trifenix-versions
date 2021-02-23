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

        /// <summary>
        /// Listado de dependencias a ser usada en la aplicación.
        /// </summary>
        public static VersionStructure[] Packages { get; set; } = new VersionStructure[] {

            new VersionStructure {
                PackageName="trifenix.connect.mdm.ts_model",
                GithubHttp="https://github.com/trifenix/trifenix-cosmos-db.git",
                GithubSsh="git@github.com:trifenix/trifenix-cosmos-db.git",
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
                Dependencies = new List<Dependency>{

                    new Dependency{
                        PackageName="trifenix.connect.db.cosmos",
                        GithubHttp="https://github.com/trifenix/trifenix-cosmos-db.git",
                        GithubSsh="git@github.com:trifenix/trifenix-cosmos-db.git",
                        pathPackageSettings="trifenix.connect.db.cosmos.csproj"
                    }


                }

            },

            new VersionStructure {
                PackageName="trifenix.connect.interfaces.db.cosmos",
                GithubHttp="https://github.com/trifenix/trifenix-cosmos-db.git",
                GithubSsh="git@github.com:trifenix/trifenix-cosmos-db.git",
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
                Dependencies = new List<Dependency>{

                    new Dependency{
                        PackageName="trifenix.connect.db.cosmos",
                        GithubHttp="https://github.com/trifenix/trifenix-cosmos-db.git",
                        GithubSsh="git@github.com:trifenix/trifenix-cosmos-db.git",
                        pathPackageSettings="trifenix.connect.db.cosmos.csproj"
                    }


                }

            },
            new VersionStructure {
                PackageName="trifenix.connect.entities.cosmos",
                GithubHttp="https://github.com/trifenix/entities-cosmos.git",
                GithubSsh="git@github.com:trifenix/entities-cosmos.git",
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
                            Patch= 59
                        }
                    }
                },
                Dependencies = new List<Dependency>{

                    new Dependency{
                        PackageName="trifenix.connect.interfaces.db.cosmos",
                        GithubHttp="https://github.com/trifenix/trifenix-cosmos-db.git",
                        GithubSsh="git@github.com:trifenix/trifenix-cosmos-db.git",
                        pathPackageSettings="trifenix.connect.interfaces.db.cosmos.csproj"
                    }


                }

            },
            new VersionStructure {
                PackageName="trifenix.connect.arguments",
                GithubHttp="https://github.com/trifenix/trifenix-connect-arguments.git",
                GithubSsh="git@github.com:trifenix/trifenix-connect-arguments.git",
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
                            Patch= 46
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

            },
            new VersionStructure {
                PackageName="trifenix.connect",
                GithubHttp="https://github.com/trifenix/connect.git",
                GithubSsh="git@github.com:trifenix/connect.git",
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
                            Patch= 77
                        }
                    }
                },
                Dependencies = new List<Dependency>{
                    new Dependency{
                        PackageName="trifenix.connect.mdm.search.model",
                        GithubHttp="https://github.com/trifenix/connect-azr-search-model.git",
                        GithubSsh="git@github.com:trifenix/connect-azr-search-model.git",
                        pathPackageSettings="trifenix.connect.mdm.search.model.csproj"
                    },
                    new Dependency{
                        PackageName="trifenix.connect.entities.cosmos",
                        GithubHttp="https://github.com/trifenix/entities-cosmos.git",
                        GithubSsh="git@github.com:trifenix/entities-cosmos.git",
                        pathPackageSettings="trifenix.connect.entities.cosmos.csproj"
                    },
                    new Dependency{
                        PackageName="trifenix.connect.bus",
                        GithubHttp="https://github.com/trifenix/trifenix-connect-bus.git",
                        GithubSsh="git@github.com:trifenix/trifenix-connect-bus.git",
                        pathPackageSettings="trifenix.connect.bus.csproj"
                    },


                }

            },
            new VersionStructure {
                PackageName="trifenix.connect.interfaces",
                GithubHttp="https://github.com/trifenix/interfaces-connect.git",
                GithubSsh="git@github.com:trifenix/interfaces-connect.git",
                PackageType= PackageType.nuget,
                Versions = new List<CommitVersion>{ 
                    new CommitVersion{ 
                        Branch="master",
                        DependantRelease=false,
                        IsFeature=false,
                        PreReleaseLabel=string.Empty,
                        Preview=0,
                        SemanticBaseVersion=new Semantic{ // actual versión
                            Major = 0,
                            Minor = 8,
                            Patch=13
                        }
                    }
                },
                Dependencies = new List<Dependency>{ 
                    new Dependency{ 
                        PackageName="trifenix.connect.aad.auth",
                        GithubHttp="https://github.com/trifenix/connect-azr-auth.git",
                        GithubSsh="git@github.com:trifenix/connect-azr-auth.git",
                        pathPackageSettings="trifenix.connect.aad.auth.csproj"
                    },
                    new Dependency{
                        PackageName="trifenix.connect",
                        GithubHttp="https://github.com/trifenix/connect.git",
                        GithubSsh="git@github.com:trifenix/connect.git",
                        pathPackageSettings="connect/trifenix.connect.csproj"
                    },
                    new Dependency{
                        PackageName="trifenix.connect.translate",
                        GithubHttp="https://github.com/trifenix/connect-translate.git",
                        GithubSsh="git@github.com:trifenix/connect-translate.git",
                        pathPackageSettings="trifenix.connect.translate.csproj"
                    },
                    new Dependency{
                        PackageName="trifenix.connect.graph",
                        GithubHttp="https://github.com/trifenix/graph.git",
                        GithubSsh="git@github.com:trifenix/graph.git",
                        pathPackageSettings="trifenix.connect.graph.csproj"
                    },
                    new Dependency{
                        PackageName="trifenix.connect.storage.azure",
                        GithubHttp="https://github.com/trifenix/connect-storage-azure.git",
                        GithubSsh="git@github.com:trifenix/connect-storage-azure.git",
                        pathPackageSettings="trifenix.connect.storage.azure.csproj"
                    }
                }

            }, 
            new VersionStructure{
                PackageName="trifenix.connect.mdm",
                GithubHttp="https://github.com/trifenix/mdm.git",
                GithubSsh="git@github.com:trifenix/mdm.git",
                PackageType= PackageType.nuget,
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
                            Patch=110
                        }
                    }
                },
                Dependencies = new List<Dependency>{
                    new Dependency{
                        PackageName="trifenix.connect",
                        GithubHttp="https://github.com/trifenix/connect.git",
                        GithubSsh="git@github.com:trifenix/connect.git",
                        pathPackageSettings="connect/trifenix.connect.csproj"
                    }
                }
            },
             new VersionStructure{
                PackageName="trifenix@mdm", // los paquetes de npm usan trifenix al principio
                GithubHttp="https://github.com/trifenix/mdm-auto-npm-model.git",
                GithubSsh="git@github.com:trifenix/mdm-auto-npm-model.git",
                PackageType= PackageType.npm,
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
            },

        };

       




    }
}
