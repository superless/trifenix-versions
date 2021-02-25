using System;

namespace trifenix.versions.model
{
    /// <summary>
    /// Detalles de un commit y su versión
    /// </summary>
    public class CommitVersion {

        /// <summary>
        /// Rama del commit
        /// </summary>
        public string Branch { get; set; }


        /// <summary>
        /// Fecha en que se actualizó.
        /// </summary>
        public DateTime LastUpdate { get; set; }


        /// <summary>
        /// versión semántica.
        /// </summary>
        public Semantic SemanticBaseVersion { get; set; } = new Semantic();

        /// <summary>
        /// Label de la versión
        /// </summary>
        public string PreReleaseLabel { get; set; }

        /// <summary>
        /// autonumérico que se incrementa, se usa en ramas que no son master.
        /// </summary>
        public int Preview { get; set; }

        /// <summary>
        /// Si una rama a modificar es dependant release, usará la versión del release.
        /// </summary>
        public bool DependantRelease { get; set; }

        /// <summary>
        /// Indica si una rama es feature.
        /// </summary>
        public bool IsFeature { get; set; }

        /// <summary>
        /// Build de azure devops
        /// </summary>
        public string Build { get; set; }

        /// <summary>
        /// genera la versión en string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Branch.ToLower().Equals("main") || Branch.ToLower().Equals("master"))
            {
                return $"{SemanticBaseVersion.Major}.{SemanticBaseVersion.Minor}.{SemanticBaseVersion.Patch}";
            }

            return $"{SemanticBaseVersion.Major}.{SemanticBaseVersion.Minor}.{SemanticBaseVersion.Patch}-{PreReleaseLabel}.{Preview}";
        }
    }


    public class CommitPackageVersion : CommitVersion {
        public string PackageName { get; set; }

    }

    public class Semantic : IComparable<Semantic>, IEquatable<Semantic> {
        /// <summary>
        /// Sección Major de la versión semántica
        /// </summary>
        public int Major { get; set; }

        /// <summary>
        /// Sección Minor de la versión semántica
        /// </summary>
        public int Minor { get; set; }

        /// <summary>
        /// Sección Patch de la versión semántica
        /// </summary>
        public int Patch { get; set; }

        public int CompareTo(Semantic other)
        {
            if (Equals(other)) return 0;
            if (Major >= other.Major) return 1;
            if (Major >= other.Major && Minor >= other.Minor) return 1;
            if( Patch > other.Patch) return 1;
            return -1;

        }

        public bool Equals(Semantic other)
        {
            return Major == other.Major && Minor == other.Minor && Patch == other.Patch;
        }
    }


}
