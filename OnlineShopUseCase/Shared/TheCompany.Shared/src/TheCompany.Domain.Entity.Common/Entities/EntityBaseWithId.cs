
namespace TheCompany.Domain.Entity.Common.Entities
{
    public abstract class EntityBaseWithId: EntityBase
    {
        public Guid Id { get; set; }    
    }
}