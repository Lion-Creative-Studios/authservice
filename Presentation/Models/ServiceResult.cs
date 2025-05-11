namespace Presentation.Models;

/* Base result class */
public abstract class ServiceResult
{
    public bool Succeeded { get; set; }

    public int StatusCode { get; set; }

    public string? Error { get; set; }
}

