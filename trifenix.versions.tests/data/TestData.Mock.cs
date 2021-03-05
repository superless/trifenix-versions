using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using trifenix.git.interfaces;
using trifenix.versions.model;

namespace trifenix.versions.tests.mock
{
    public static partial class TestData
    {

        public static class Mock {
            public static IGithubRepo GitRepo() {
                var mock = new Mock<IGithubRepo>();
                
                Action<string, string, string> saveFile = (path, message, element) => Log.AppendLine($"saveFile => path : {path} , message : {message}, element : {element}");
                Func<string, string> get = (path) =>
                {
                    Log.AppendLine($"get => path : {path}");
                    return null;
                };
                Action <IEnumerable<Func<bool>>, string> commit = (operations, branch) =>
                {
                    var operationsStr = string.Join(',', operations.ToList());
                    Log.AppendLine($"commit => branch : {branch}, operations : {operationsStr}");
                };
                mock.Setup(s => s.Clone()).Returns("folder");
                mock.Setup(s => s.Commit(It.IsAny<IEnumerable<Func<bool>>>(), It.IsAny<string>())).Callback(commit);
                mock.Setup(s => s.SaveFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Callback(saveFile);
                mock.Setup(s => s.Get(It.IsAny<string>())).Returns(get);
                
                return mock.Object;
            }

            public static IGithubRepo<VersionStructure> GitRepoVersionStructure(VersionStructure defaultVersionStructure)
            {
                var mock = new Mock<IGithubRepo<VersionStructure>>();

                Action<string, string, VersionStructure> saveFile = (path, message, element) => Log.AppendLine($"saveFile<VersionStructure> => path : {path} , message : {message}, element : {element.PackageName}");
                Func<string, VersionStructure> get = (path) =>
                {
                    Log.AppendLine($"getElement => path : {path}");
                    return defaultVersionStructure;
                };

                mock.Setup(s => s.SaveFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<VersionStructure>())).Callback(saveFile);
                mock.Setup(s => s.GetElement(It.IsAny<string>())).Returns(get);
                return mock.Object;
            }

        };
    }
}
