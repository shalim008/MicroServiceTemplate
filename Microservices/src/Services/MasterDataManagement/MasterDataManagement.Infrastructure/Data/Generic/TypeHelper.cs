using System.Data;

namespace MasterDataManagement.Infrastructure.Data.Generic
{
    public class TypeHelper
    {
        public static DateTimeOffset GetDateTimeOffset(DateTime dt)
        {
            return new DateTimeOffset(dt, TimeZoneInfo.Local.GetUtcOffset(dt));
        }

        public static List<T> BindToList<T>(DataTable dt)
        {
            // Example 1:
            // Get private fields + non properties
            //var fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            // Example 2: Your case
            // Get all public fields
            var fields = typeof(T).GetFields();

            List<T> lst = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                // Create the object of T
                var ob = Activator.CreateInstance<T>();

                foreach (var fieldInfo in fields)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        // Matching the columns with fields
                        if (fieldInfo.Name == dc.ColumnName)
                        {
                            // Get the value from the datatable cell
                            object value = dr[dc.ColumnName];

                            // Set the value into the object
                            fieldInfo.SetValue(ob, value);
                            break;
                        }
                    }
                }

                lst.Add(ob);
            }

            return lst;
        }
    }
}
