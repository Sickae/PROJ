using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using PROJ.Shared.Attributes;

namespace PROJ.DataAccess.Conventions
{
    public class NotNullConvention : AttributePropertyConvention<NotNullAttribute>
    {
        protected override void Apply(NotNullAttribute attribute, IPropertyInstance instance)
        {
            instance.Not.Nullable();
        }
    }
}
