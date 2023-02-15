using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MasterDataManagement.Client.Models;

namespace MasterDataManagement.Client.Data
{
    public class MasterDataManagementClientContext : DbContext
    {
        public MasterDataManagementClientContext (DbContextOptions<MasterDataManagementClientContext> options)
            : base(options)
        {
        }

        public DbSet<MasterDataManagement.Client.Models.SysOwnerDto> SysOwnerDto { get; set; } = default!;
    }
}
