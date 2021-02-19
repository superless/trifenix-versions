using System;
using trifenix.versions.interfaces;
using trifenix.versions.model;

namespace trifenix.versions
{
    public class StringUtils : IStringUtils
    {
        public string GetFeatureName(string branch)
        {
            throw new NotImplementedException();
        }

        public string GetPackageFullPath(string folder, string packageName, PackageType type)
        {
            throw new NotImplementedException();
        }

        public string GetPreReleaseLabel(string branch, bool isReleaseDependant)
        {
            throw new NotImplementedException();
        }

        public Semantic GetSemanticVersionFromRelease(string branch)
        {
            throw new NotImplementedException();
        }

        public string SetGithubToken(string repository, string token, string username)
        {
            throw new NotImplementedException();
        }
    }
}
