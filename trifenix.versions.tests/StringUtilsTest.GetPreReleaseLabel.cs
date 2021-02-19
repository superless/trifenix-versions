using System;
using trifenix.versions.model;
using Xunit;

namespace trifenix.versions.tests
{

    /// <summary>
    /// Test de StringUtil
    /// </summary>
    public partial class StringUtilsTest
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
                var utils = new StringUtils();
                // action
                var label = utils.GetPreReleaseLabel("master", true);
                // assert

                Assert.Equal(string.Empty, label);
            }

            [Fact]
            public void ReleasePreLeaseLabel()
            {
                // assign
                var utils = new StringUtils();
                // action
                var label = utils.GetPreReleaseLabel("release", true);
                // assert

                Assert.Equal("RC", label);
            }

            [Fact]
            public void DevelopReleasePreLeaseLabel()
            {
                // assign
                var utils = new StringUtils();
                // action
                var label = utils.GetPreReleaseLabel("develop", true);
                // assert

                Assert.Equal("preview-release", label);
            }

            [Fact]
            public void DevelopMasterReleasePreLeaseLabel()
            {
                // assign
                var utils = new StringUtils();
                // action
                var label = utils.GetPreReleaseLabel("develop", false);
                // assert

                Assert.Equal("preview", label);
            }

            [Fact]
            public void FeatureMasterReleasePreLeaseLabel()
            {
                // assign
                var utils = new StringUtils();
                // action
                var label = utils.GetPreReleaseLabel("feature/test", false);
                // assert

                Assert.Equal("alpha.test", label);
            }






        }
    }
}
