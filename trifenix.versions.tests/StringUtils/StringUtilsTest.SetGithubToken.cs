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
        public class SetGithubToken {


            [Fact]
            public void CorrectGithub() {
                // assign
                var utils = new StringUtils();
                // action
                var repo = utils.SetGithubToken(
                        @"https://github.com/trifenix/docs-trifenix-connect.git", 
                        "59403fbd47076ac163d62cdfd352367288db414a", 
                        "devops");
                // assert

                Assert.Equal("https://devops:59403fbd47076ac163d62cdfd352367288db414a@github.com/trifenix/docs-trifenix-connect.git", repo);
            }


            

        }
    }
}
