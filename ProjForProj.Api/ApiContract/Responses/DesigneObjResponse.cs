using ProjForProj.Api.Domain.Entity;

namespace ProjForProj.Api.ApiContract.Responses;


//При выборе в дереве проекта или объекта проектирования в правой части должна появиться таблица комплектов со столбцами
//Шифр проекта
//Код объекта проектирования(полный код объекта формируется как "Полный код родительского объекта.Собственный код")
//Марка
//Номер(нумеруются комплекты с нуля). Марка + Номер
//Полный шифр комплекта(формируется как "Шифр проекта-Полный код объекта-МаркаНомер")

/// <summary>
/// Для получения дочерних объектов, нужно будет посылать отдельные запросы по полученным id
/// </summary>
public class DesigneObjResponse
{
    public Guid Id { get; set; }
    /// <summary>
    /// Код объекта проектирования(полный код объекта формируется как "Полный код родительского объекта.Собственный код")
    /// </summary>
    public string FullCode { get; set; }
    public string Name { get; set; }
    /// <summary>
    /// Шифр проекта
    /// </summary>
    public string ProjectName { get; set; }
    public Guid? ParentObjectId { get; set; } 
    public ICollection<Guid> ChildObjectsIds { get; set; }
    public ICollection<Guid> DocumentationSetsIds { get; set; }
}

