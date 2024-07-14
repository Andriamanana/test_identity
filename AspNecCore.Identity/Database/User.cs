using Microsoft.AspNetCore.Identity;

namespace AspNecCore.Identity.Database
{
    public class User:IdentityUser // to make User compatible with identity system => implement IdentityUser base class
    {
        public string? Initials { get; set; }    // it will be added when we make migration  (mila Ef core database context) 
                                                 // mila Npgsql.EntityFrqmeworkCore.PostgreSql  (raha postgreSql)
                                                 //Microsoft.EntityFrameworkCore.SqlServer (raha SqlServer)
                                                 // mila EntityFramework.Tools thqnt going to allow us to create our migrations
                                                 // mila Microsoft.AspNetCore.Identity.EntityFrameworkCore ahafahana milalao Identity (contain some abstraction that we can use to define our identity)
    }
}
