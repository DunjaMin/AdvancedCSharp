namespace Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportClass : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class ExportPropery : Attribute { }

    [AttributeUsage(AttributeTargets.Constructor)]
    public class ImportingConstructorAttribute : Attribute { }
}