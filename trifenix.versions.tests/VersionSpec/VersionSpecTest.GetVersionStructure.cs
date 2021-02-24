using System.Linq;
using trifenix.versions.model;
using trifenix.versions.tests.mock;
using Xunit;

namespace trifenix.versions.tests
{
    public partial class VersionSpecTest
    {
        /// <summary>
        /// Obtiene versionStructure desde git o desde los valores por defecto
        /// </summary>
        public class GetVersionStructure {


            /// <summary>
            /// no existe el archivo en git por tanto asumirá el por defecto.
            /// Los valores por defecto están en las instancias.
            /// </summary>
            [Fact]
            public void GetVersionWithoutGit() {

                // assign
                var spec = TestData.Instances.DefaultNullGithub;

                // action
                var result = spec.GetVersionStructure();

                // assert
                Assert.Equal(Data.Packages.First(s=>s.PackageName.Equals("trifenix.connect")), result);

            }

            [Fact]
            public void GetVersionFromGit() {
                // assign
                var defaultPackage = Data.Packages.First(s => s.PackageName.Equals("trifenix.connect"));

                var local = Data.Mapper.Map<VersionStructure>(defaultPackage);

                local.Versions.Add(new CommitVersion {
                    Branch = "develop",
                    Build = "build",
                    IsFeature = false

                });
                var spec = TestData.Instances.SetVersionStructureGithub(local);

                // action
                var result = spec.GetVersionStructure();

                // assert
                Assert.Equal(local, result);

            }

            [Fact]
            public void MasterNotFound() {
                // assign sin valores por defecto, ni elementos en github.
                var spec = TestData.Instances.SetVersionStructureWithoutDefaultGithub(null);

                // action
                var result = spec.GetVersionStructure();

                // assert
                Assert.Null(result);
            }



        }
    }
}
