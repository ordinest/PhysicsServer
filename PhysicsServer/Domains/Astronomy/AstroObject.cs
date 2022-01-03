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
        public enum Subtype
        {
            None,
            Spiral,
            Ellip
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
    }
}
