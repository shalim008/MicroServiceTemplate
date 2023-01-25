using MasterDataManagement.Core.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using System.Reflection;

namespace MasterDataManagement.Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext()
        {
        }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<SysOwner> SysOwner { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }
                }
            }
        }

        public IEnumerable<dynamic> GetDynamicResult(string commandText, params SqlParameter[] parameters)
        {
            var result = new ExpandoObject() as IDictionary<string, object>;
            using (var connection = Database.GetDbConnection())
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = commandText;

                if (parameters?.Length > 0)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                connection.Open();

                using (var dataReader = command.ExecuteReader())
                {
                    var names = new List<string>();

                    if (dataReader.HasRows)
                    {
                        for (var i = 0; i < dataReader.VisibleFieldCount; i++)
                        {
                            names.Add(dataReader.GetName(i));
                        }

                        while (dataReader.Read())
                        {
                            foreach (var name in names)
                            {
                                result.Add(name, dataReader[name]);
                            }
                        }
                    }
                }

                connection.Close();
            }

            yield return result;
        }
    }
}



