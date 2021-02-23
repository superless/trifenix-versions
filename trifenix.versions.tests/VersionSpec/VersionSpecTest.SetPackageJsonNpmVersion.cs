using System.IO;
using System.Reflection;
using trifenix.versions.model;
using trifenix.versions.tests.mock;
using Xunit;

namespace trifenix.versions.tests
{

    public partial class VersionSpecTest {


        public class SetPackageJsonNpmVersion {

            [Fact]
            public void SetPackageVersionInFileForCheck() {
                var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var spec = TestData.Instances.NpmDevelopNoRelease;

                var fileStr = spec.SetPackageJsonNpmVersion(new Dependency
                {
                    pathPackageSettings = "Files/file.json"
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

                var resultStr = StringUtils.ReadAllText(Path.Combine(directoryName, "Files/file.result.json"));

                var hashResultExpect = Hash.ComputeSha256Hash(resultStr);

                var hashResult = Hash.ComputeSha256Hash(fileStr);

                Assert.Equal(hashResultExpect, hashResult);

            }


            [Fact]
            public void SetPackageVersionInFileForCheckPeerDependencies()
            {
                var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var spec = TestData.Instances.NpmDevelopNoRelease;

                var fileStr = spec.SetPackageJsonNpmVersion(new Dependency
                {
                    pathPackageSettings = "Files/file.peerDependency.json"
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

                var resultStr = StringUtils.ReadAllText(Path.Combine(directoryName, "Files/file.peerDependency.result.json"));

                var hashResultExpect = Hash.ComputeSha256Hash(resultStr);

                var hashResult = Hash.ComputeSha256Hash(fileStr);

                Assert.Equal(hashResultExpect, hashResult);

            }

            [Fact]
            public void SetPackageVersionInFileForCheckDevDependencies()
            {
                var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var spec = TestData.Instances.NpmDevelopNoRelease;

                var fileStr = spec.SetPackageJsonNpmVersion(new Dependency
                {
                    pathPackageSettings = "Files/file.devDependency.json"
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

                var resultStr = StringUtils.ReadAllText(Path.Combine(directoryName, "Files/file.devDependency.result.json"));

                var hashResultExpect = Hash.ComputeSha256Hash(resultStr);

                var hashResult = Hash.ComputeSha256Hash(fileStr);

                Assert.Equal(hashResultExpect, hashResult);

            }

        }


    }
}
