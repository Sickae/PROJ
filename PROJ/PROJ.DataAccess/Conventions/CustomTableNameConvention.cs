using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using PROJ.DataAccess.Helpers;
using System.Globalization;

namespace PROJ.DataAccess.Conventions
{
    public class CustomTableNameConvention : IClassConvention
    {
        public void Apply(IClassInstance instance)
        {
            instance.Table(string.Format(CultureInfo.InvariantCulture, "{0}", NameConverter.ConvertName(instance.EntityType.Name)));
        }
    }
}
