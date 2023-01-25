using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataManagement.Core.Entities
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTimeOffset SetOn { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset ModifiedOn { get; set; } = DateTimeOffset.Now;
        public int? SetBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int DataStatus { get; set; } = 1;
    }
}