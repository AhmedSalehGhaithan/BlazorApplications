namespace BaseLibrary.Responses
{
    public record GeneralResponse (bool Flag,string Message=null!);
    public record LoginResponse(bool Flag,string Message=null!,string Token=null!,string RefreshToken=null!);
}
