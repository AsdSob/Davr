
namespace Davr.Vash.Entities.Abstracts
{
    public abstract class EntityBase : IEntity<int>
    {
        public int Id { get; set; }

        //public DateTime CreatedDateUtc { get; set; }
        //public DateTime DeleteDateUtc { get; set; }

        public virtual bool IsNew => Id <= 0;
    }
}
