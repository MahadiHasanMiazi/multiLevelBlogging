namespace Multi_Level_Blogging_System.Extensions;

public class AppException : Exception
{
    private AppException(AppExceptionKey? myExceptionKey, ErrorCode errorCode, string message, Exception innerException)
        : base(message, innerException)
    {
       ErrorCode = errorCode;
       Key = myExceptionKey;
    }
    
    public AppExceptionKey? Key { get; }
    

    public ErrorCode ErrorCode { get; }
    
    public static AppException Validation(string message) => new AppException(null, ErrorCode.ValidationError, message, null);
    public static AppException Supported(string message, Exception innerException = null) => new AppException(AppExceptionKey.SupportedError, ErrorCode.ValidationError, message, innerException);
    public static AppException Unsupported(string message, Exception innerException = null) => new AppException(null, ErrorCode.InternalError, message, innerException);

    
}