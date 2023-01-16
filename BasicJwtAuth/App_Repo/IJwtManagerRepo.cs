using BasicJwtAuth.Models;

namespace BasicJwtAuth.App_Repo
{
    public interface IJwtManagerRepo
    {
        Tokens Authenticate(Users user, object JwtTokenDescriptor);
        object Authenticate(Users userdata);
        //object Authenticate(Users userdata);
    }
}
