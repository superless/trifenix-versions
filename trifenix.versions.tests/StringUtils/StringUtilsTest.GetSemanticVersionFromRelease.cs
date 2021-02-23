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
        public class GetSemanticVersionFromRelease
        {


            [Fact]
            public void CorrectSemantic() {
                // assign
                var utils = new StringUtils();
                // action
                var repo = utils.GetSemanticVersionFromRelease("Release/1.0.1");
                // assert

                Assert.True(repo.Major == 1 && repo.Minor == 0 && repo.Patch == 1);
            }

            [Fact]
            public void InCorrectSemanticThrowException()
            {
                // assign
                var utils = new StringUtils();

                var errorMessage = string.Empty;
                try
                {
                    // action
                    utils.GetSemanticVersionFromRelease("Release/1.a.1");
                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                    
                }

                Assert.Contains("release", errorMessage);

                // assert



                
            }





        }
    }
}
