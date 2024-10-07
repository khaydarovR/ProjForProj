namespace ProjForProj.Api.Domain.Entity
{
    public class DesignObject
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid? ParentObjectId { get; set; } //null если нет родителя (корень)
        public DesignObject ParentObject { get; set; }
        public ICollection<DesignObject> ChildObjects { get; set; } //может быть пустой если нет дочерних объектов
        public ICollection<DocumentationSet> DocumentationSets { get; set; }
    }
}
