using System;

namespace PROJ.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DeletableEntityAttribute : Attribute
    { }
}
