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
        public class GetPackageFullPath {


            [Fact]
            public void CorrectPath() {
                // assign
                var utils = new StringUtils();
                // action
                var fullpath = utils.GetPackageFullPath("", "test-package", PackageType.nuget);
                // assert

                Assert.Equal("./test-package.nuget.json", fullpath);
            }


            

        }
    }
}
