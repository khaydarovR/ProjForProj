namespace ProjForProj.Api.Domain.Entity
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<DesignObject> DesignObjects { get; set; }
    }
}
