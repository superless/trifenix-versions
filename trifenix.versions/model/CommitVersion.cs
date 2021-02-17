namespace trifenix.versions.model
{
    /// <summary>
    /// Detalles de un commit y su versión
    /// </summary>
    public class CommitVersion {

        /// <summary>
        /// Rama del commit
        /// </summary>
        public string branch { get; set; }

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
        /// genera la versión en string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Major}.{Minor}.{Patch}.{PreReleaseLabel}.{Preview}";
        }
    }


}
