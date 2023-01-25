using MasterDataManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterDataManagement.Infrastructure.Data.Configuration
{
    public class SysOwnerConfiguration
    {
        public void Configure(EntityTypeBuilder<SysOwner> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.OwnerCode).IsRequired(true).HasMaxLength(10);
            builder.Property(p => p.OwnerName).IsRequired(true).HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired(true).HasMaxLength(500);
            builder.Property(p => p.DataStatus).IsRequired(true).HasDefaultValue(1);

            //builder.HasMany(p => p.DcmCaseListCustomer)
            //      .WithOne()
            //      .HasForeignKey(fk => fk.DcmCaseListId)
            //      .OnDelete(DeleteBehavior.Restrict)
            //      .IsRequired();
        }
    }
}
