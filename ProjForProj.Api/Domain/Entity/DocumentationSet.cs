namespace ProjForProj.Api.Domain.Entity
{
    public class DocumentationSet
    {
        public Guid Id { get; set; }
        public Guid MarkId { get; set; }
        public Mark Mark { get; set; }
        public int Number { get; set; }
        public Guid DesignObjectId { get; set; }
        public DesignObject DesignObject { get; set; }
        public string FullCode { get; set; }
    }

}
