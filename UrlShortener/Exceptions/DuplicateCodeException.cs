namespace UrlShortener.Exceptions;

public class DuplicateCodeException : Exception
{
    public DuplicateCodeException()
        : base($"It was not possible to generate a unique code for the URL. Try again.") { }
}
