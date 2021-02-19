using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using trifenix.git.interfaces;
using trifenix.versions.model;

namespace trifenix.versions.tests
{
    public static partial   class TestData
    {

        public static StringBuilder Log { get; set; } = new StringBuilder();

        public static class Mock {
            public static IGithubRepo GitRepo() {
                var mock = new Mock<IGithubRepo>();
                
                Action<string, string, string, string> saveFile = (path, branch, message, element) => Log.AppendLine($"saveFile => path : {path} , brach : {branch}, message : {message}, element : {element}");
                Action<string, string> get = (path, branch) => Log.AppendLine($"get => path : {path}, branch : {branch}");
                Action<string, Dictionary<string, Func<bool>>> commit = (branch, operations) =>
                {
                    var operationsStr = string.Join(',', operations.Select(s => s.Key).ToArray());
                    Log.AppendLine($"commit => branch : {branch}, operations : {operationsStr}");
                };
                mock.Setup(s => s.Clone()).Callback(() => Log.AppendLine("clone => executed"));
                mock.Setup(s => s.SaveFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Callback(saveFile);
                mock.Setup(s => s.Get(It.IsAny<string>(), It.IsAny<string>())).Returns(get);
                mock.Setup(s => s.Commit(It.IsAny<string>(), It.IsAny<Dictionary<string, Func<bool>>>())).Callback(commit);
                return mock.Object;
            }

            public static IGithubRepo<VersionStructure> GitRepoVersionStructure()
            {
                var mock = new Mock<IGithubRepo<VersionStructure>>();

                Action<string, string, VersionStructure, string> saveFile = (path, message, element, branch) => Log.AppendLine($"saveFile<VersionStructure> => path : {path} , brach : {branch}, message : {message}, element : {element.PackageName}");
                Action<string, bool, string> get = (path, force,  branch) => Log.AppendLine($"getElement => path : {path}, branch : {branch}, force : {force}");
                mock.Setup(s => s.SaveFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<VersionStructure>(), It.IsAny<string>())).Callback(saveFile);
                mock.Setup(s => s.GetElement(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<string>())).Callback(get);
                return mock.Object;
            }

        };
    }
}
