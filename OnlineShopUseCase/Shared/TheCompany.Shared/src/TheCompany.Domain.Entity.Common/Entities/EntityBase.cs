
namespace TheCompany.Domain.Entity.Common.Entities
{
    public abstract class EntityBase
    {
        public DateTime CreateTime { get; set; }
        public Guid? CreateUserId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public Guid? ModifyUserId { get; set; }
        public byte IsDeleted { get; set; }
    }
}