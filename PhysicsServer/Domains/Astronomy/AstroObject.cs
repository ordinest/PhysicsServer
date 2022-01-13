namespace PhysicsServer.Domains.Astronomy
{
    public record AstroObject
    {
        public enum Catalogue
        {
            NGC,
            Messier,
            Caldwell,
        }
        public enum Type
        {
            None,
            Planet,
            Galaxy,
            Cluster,
            Nebula,
            DoubleStar,
            Asterism,
            StarCloud
        }
        public enum Subtype
        {
            None,
            // Galaxies
            Spiral,
            Elliptical,
            Irregular,
            Lenticular,
            // Clusters
            Globular,
            Open,
            // Nebulae
            SupernovaRemnant,
            Diffuse,
            PlanetaryNebula,
        }
        public record AstroObjectClassification(
            IDictionary<Catalogue, int> CatalogueId,
            Type Type,
            Subtype Subtype,
            string SubtypeClassification)
        {
            /// <summary>
            /// Mapping between astronomical <see cref="Catalogue"/>s
            /// to catalogue numbers for this astronomical object.
            /// </summary>
            public IDictionary<Catalogue, int> CatalogueId { get; init; } = CatalogueId;

            /// <summary>
            /// The <see cref="Type"/> of astronomical object.
            /// </summary>
            public Type Type { get; init; } = Type;

            /// <summary>
            /// The <see cref="Subtype"/> of astronomical object, in the context of the
            /// object's <see cref="Type"/>.
            /// </summary>
            public Subtype Subtype { get; init; } = Subtype;

            /// <summary>
            /// Type-scoped classification of the astronomical object.
            /// </summary>
            /// <remarks>
            /// Some types of astronomical objects have their own internal classifications.
            /// For example, Galaxy types have several subtypes (elliptical, spiral, etc.),
            /// which have further classification that describes structure and appearance,
            /// such as those given by the 
            /// <see href="https://en.wikipedia.org/wiki/Hubble_sequence">Hubble sequence</see>.
            /// </remarks>
            public string SubtypeClassification { get; init; } = SubtypeClassification;
        }

        public string Id { get; init; } = "Id";
        public string Name { get; init; } = "Astro Object";
        public AstroObjectClassification? Classification { get; init; }
        public double RightAscension { get; init; }
        public double Declination { get; init; }


        public static readonly Dictionary<Subtype, Type> SubtypeToType = new Dictionary<Subtype, Type>
        {
            { Subtype.None, Type.None },
            // Galaxies
            { Subtype.Spiral, Type.Galaxy },
            { Subtype.Elliptical, Type.Galaxy },
            { Subtype.Irregular , Type.Galaxy },
            { Subtype.Lenticular, Type.Galaxy },
            // Clusters,
            { Subtype.Globular, Type.Cluster },
            { Subtype.Open,Type.Cluster },
            // Nebulae
            { Subtype.SupernovaRemnant, Type.Nebula },
            { Subtype.Diffuse, Type.Nebula },
            { Subtype.PlanetaryNebula, Type.Nebula },
        };
    }
}
