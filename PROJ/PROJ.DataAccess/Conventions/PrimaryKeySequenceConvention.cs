﻿using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using PROJ.DataAccess.Helpers;
using System.Globalization;

namespace PROJ.DataAccess.Conventions
{
    public class PrimaryKeySequenceConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            var sequenceName = string.Format(CultureInfo.InvariantCulture, "{0}_id_seq", NameConverter.ConvertName(instance.EntityType.Name));
            var columnName = string.Format(CultureInfo.InvariantCulture, "{0}", NameConverter.ConvertName(instance.Property.Name));

            instance.GeneratedBy.Native(sequenceName);
            instance.Column(columnName);
        }
    }
}
