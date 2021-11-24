
namespace Davr.Vash.Entities.Abstracts
{
    public interface IEntity<T> where T: struct
    {
        T Id { get; set; }
    }
}
