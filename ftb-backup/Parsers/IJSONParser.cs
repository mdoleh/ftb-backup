namespace ftb_backup.Parsers
{
    public interface IJSONParser<T>
    {
        T ParseFile(string path);
    }
}
