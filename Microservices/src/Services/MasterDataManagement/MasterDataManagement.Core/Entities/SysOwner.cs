using System.Numerics;

namespace MasterDataManagement.Core.Entities
{
    public class SysOwner : BaseEntity
    {
        public string OwnerName { get; set; }
        public string OwnerCode { get; set; }
        public string Description { get; set; }
        public long ParentOwnerId { get; set; }
    }
}
