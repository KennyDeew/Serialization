namespace Serialization.StringSerializers
{
    public interface IStringSerializer
    {
        string ConvertToString(object obj);
    }
}
