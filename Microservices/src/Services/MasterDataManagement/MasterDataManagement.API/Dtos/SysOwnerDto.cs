using System.ComponentModel.DataAnnotations;

namespace MasterDataManagement.API.Dtos
{
    public class SysOwnerDto
    {
        public Int64 Id { get; set; }

        [Required]
        public string OwnerName { get; set; }
        
        [Required]
        public string OwnerCode { get; set; }

        [MaxLength(500, ErrorMessage = "Description must be less than 500 character")]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Your Parent Owner Id is invalid")]
        public Int64 ParentOwnerId { get; set; }
    }
}
