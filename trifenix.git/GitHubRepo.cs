using System;
using System.Collections.Generic;
using trifenix.git.interfaces;

namespace trifenix.git
{
    public class GitHubRepo : IGithubRepo
    {
        public string Clone()
        {
            throw new NotImplementedException();
        }

        public void Commit(string branch, Dictionary<string, Func<bool>> commitMessageFileOperations)
        {
            throw new NotImplementedException();
        }

        public string Get(string path, string branch)
        {
            throw new NotImplementedException();
        }

        public void SaveFile(string path, string branch, string message, string element)
        {
            throw new NotImplementedException();
        }
    }

    public class GitHubRepo<T> : IGithubRepo<T>
    {
        public T GetElement(string path, bool force = false, string branch = "master")
        {
            throw new NotImplementedException();
        }

        public void SaveFile(string path, string message, T element, string branch = "master")
        {
            throw new NotImplementedException();
        }
    }
}
