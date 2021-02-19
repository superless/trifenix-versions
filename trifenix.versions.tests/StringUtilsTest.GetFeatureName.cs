using System;
using Xunit;

namespace trifenix.versions.tests
{

    /// <summary>
    /// Test de StringUtil
    /// </summary>
    public partial class StringUtilsTest
    {

        /// <summary>
        /// Colección de tests para featureName
        /// </summary>
        public class GetFeatureName {


            [Fact]
            public void AssignIncorrectFeatureBranchAndReturnTheBranch() {
                // assign
                var utils = new StringUtils();
                // action
                var rama = utils.GetFeatureName("rama");
                // assert

                Assert.Equal("rama", rama);
            }


            [Fact]
            public void GetCorrectFeatureFromWellFormBranch() {
                // assign
                var utils = new StringUtils();
                // action
                var rama = utils.GetFeatureName("feature/rama");
                // assert

                Assert.Equal("rama", rama);
            }

        }
    }
}
