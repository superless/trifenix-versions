using System.IO;
using System.Reflection;
using trifenix.versions.model;
using trifenix.versions.tests.mock;
using Xunit;

namespace trifenix.versions.tests
{

    public partial class VersionSpecTest {

        
        public class SetCsProjNugetVersion {

            [Fact]
            public void GetNewVersionFromFileCsProj() {

                var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var spec = TestData.Instances.Default;

                var result = spec.SetCsProjNugetVersion(new Dependency
                {
                    pathPackageSettings = "Files/file.csproj"
                },
                new CommitVersion
                {
                    SemanticBaseVersion = new Semantic
                    {
                        Major = 1,
                        Minor = 2,
                        Patch = 2
                    },
                    Branch = "master",

                },
                directoryName

                );

                var expected = StringUtils.ReadAllText(Path.Combine(directoryName, "Files/file.result.csproj"));

                


                Assert.True(VersionSpec.CompareXml(result, expected));





            }

        }


    }
}
