
namespace Davr.Gumon.Entities.Abstracts
{
    public interface IEntity<T> where T: struct
    {
        T Id { get; set; }
    }
}
