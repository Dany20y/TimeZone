using System.Web;
using Time_Zone.Domain.Entities.User;

namespace Time_Zone.Extension
{
    public static class HttpContextExtensions
    {
        public static UserMinimal GetMySessionObject(this HttpContextBase current)
        {
            return (UserMinimal)current?.Session["__SessionObject"];
        }

        public static void SetMySessionObject(this HttpContextBase current, UserMinimal profile)
        {
            current.Session["__SessionObject"] = profile; // Utilizăm indexator în loc de Add
        }
    }
}
