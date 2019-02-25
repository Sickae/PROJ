using System;

namespace PROJ.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotNullAttribute : Attribute
    { }
}
