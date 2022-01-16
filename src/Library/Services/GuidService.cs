namespace Library.Services;

public class GuidService : IGuidService
{
    public Guid NewGuid()
    {
        return Guid.NewGuid();
    }
}