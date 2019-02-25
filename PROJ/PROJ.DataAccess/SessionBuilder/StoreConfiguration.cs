using FluentNHibernate;
using FluentNHibernate.Automapping;
using PROJ.DataAccess.Entities;
using PROJ.DataAccess.Helpers;
using System;

namespace PROJ.DataAccess.SessionBuilder
{
    public class StoreConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.IsSubclassOf(typeof(Entity));
        }

        public override string GetComponentColumnPrefix(Member member)
        {
            var result = NameConverter.ConvertName(member.Name) + "_";
            return result;
        }
    }
}
