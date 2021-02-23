using System;
using trifenix.versions.model;
using trifenix.versions.tests.mock;
using Xunit;

namespace trifenix.versions.tests
{

    /// <summary>
    /// Test de StringUtil
    /// </summary>
    public partial class VersionSpecTest
    {

        /// <summary>
        /// Colección de tests para GetPackageFullPath
        /// encargado de crear la ruta para el github.
        /// </summary>
        public class GetPreReleaseLabel
        {


            [Fact]
            public void MasterPreLeaseLabel() {
                // assign
                var spec = TestData.Instances.Default;
                // action
                var label = spec.GetPreReleaseLabel();
                // assert

                Assert.Equal(string.Empty, label);
            }

            [Fact]
            public void ReleasePreLeaseLabel()
            {
                // assign
                var spec = TestData.Instances.DevelopRelease;
                // action
                var label = spec.GetPreReleaseLabel();
                // assert

                Assert.Equal("preview-release", label);
            }

            [Fact]
            public void DevelopReleaseFalsePreLeaseLabel()
            {
                // assign
                var spec = TestData.Instances.NpmDevelopNoRelease;
                // action
                var label = spec.GetPreReleaseLabel();
                // assert

                Assert.Equal("preview", label);
            }

            [Fact]
            public void AlphaEveryBranch()
            {
                // assign
                var spec = TestData.Instances.NpmDevelopNoReleaseCustomBranchRelease;
                // action
                var label = spec.GetPreReleaseLabel();
                // assert

                Assert.Equal("alpha-test-branch-release", label);
            }

            [Fact]
            public void FeatureMasterReleasePreLeaseLabel()
            {
                // assign
                var spec = TestData.Instances.NpmDevelopNoReleaseBranch;
                // action
                var label = spec.GetPreReleaseLabel();
                // assert

                Assert.Equal("RC", label);
            }






        }
    }
}
