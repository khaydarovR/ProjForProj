namespace ProjForProj.Api.ApiContract.Requests
{
    public class CreateDesigneObjRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid ProjectId { get; set; }
        /// <summary>
        /// Возможен null если является корнем в проекте
        /// </summary>
        public Guid? ParentObjectId { get; set; } 
    }

}