using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A_Winform_Library.Data
{
    public static class BindingSourceUtility
    {
        public static BindingSource BindingSource_Of<T>(this DataTable data)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = List_Of<T>(data);

            return bs;
        }

        public static BindingSource Of<T>(this BindingSource bs)
        {
            bs.DataSource = new List<T>();

            return bs;
        }

        public static BindingSource BindingSource_AddList<T>(this BindingSource bs, List<T> list)
        {
            foreach (var item in list)
            {
                bs.Add(item);
            }

            return bs;
        }

        public static BindingSource Append(this BindingSource bs, BindingSource bs1)
        {
            foreach (var item in bs1)
            {
                bs.Add(item);
            }

            return bs;
        }

        private static List<T> List_Of<T>(this DataTable data)
        {
            PropertyInfo property;
            Type type;

            MethodInfo method;
            object value;

            List<PropertyInfo> properties = typeof(T).GetProperties().AsEnumerable()
                .Where(prop => data.Columns.Contains(prop.Name) && prop.CanWrite)
                .ToList();

            List<T> result = new List<T>();

            string bool_true_conditions = "yes y ok true";

            foreach (DataRow row in data.Rows)
            {
                // Create the object of T
                var item = Activator.CreateInstance<T>();

                foreach (PropertyInfo prop in properties)
                {
                    try
                    {
                        type = prop.PropertyType;

                        if (type.Name.Equals(typeof(bool).Name))
                        {
                            if (!string.IsNullOrEmpty(row[prop.Name].ToString())
                                && bool_true_conditions.Contains(row[prop.Name].ToString().ToLower()))
                            {
                                prop.SetValue(item, true, null);
                            }
                            else
                            {
                                prop.SetValue(item, false, null);
                            }

                            continue;
                        }

                        method = typeof(BindingSourceUtility).GetMethod("GenericConvert_To")
                                .MakeGenericMethod(new Type[] { type });

                        value = method.Invoke(typeof(BindingSourceUtility), new object[] { row[prop.Name], null });

                        prop.SetValue(item, value, null);
                    }
                    catch
                    {
                        continue;
                    }
                }

                result.Add(item);
            }

            return result;
        }

        private static T GenericConvert_To<T>(this object value, object default_value = null)
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                try
                {
                    if (default_value != null)
                    {
                        return (T)default_value;
                    }

                    return default(T);
                }
                catch
                {
                    return default(T);
                }
            }
        }
    }
}
