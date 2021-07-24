namespace UrlShortener.Services
{
    public interface IIntEncoder
    {
        string Encode(int num);
        int Decode(string str);
    }
}