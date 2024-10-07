namespace ProjForProj.Api.Domain.Entity
{
    public class Mark
    {
        public Guid Id { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public ICollection<DocumentationSet> DocumentationSets { get; set; }
    }
}
