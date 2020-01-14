using System.ComponentModel.DataAnnotations;

namespace UserCRUD.Core.Model
{
    public abstract class IEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
