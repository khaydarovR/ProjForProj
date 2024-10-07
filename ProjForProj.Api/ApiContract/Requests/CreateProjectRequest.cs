namespace ProjForProj.Api.ApiContract.Requests
{
    public class CreateProjectRequest
    {
        /// <summary>
        /// Код проекта
        /// </summary>
        /// <example>ADS534</example>
        public string Code { get; set; }
        /// <summary>
        /// Название проекта
        /// </summary>
        /// <example>Get from 1С</example>
        public string Name { get; set; }
    }

}