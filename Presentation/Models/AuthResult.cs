namespace Presentation.Models;

/* Return result for Auth */
public class AuthResult : ServiceResult
{
}

public class AuthResult<T> : ServiceResult
{
    public T? Result { get; set; }
}

