using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiSecuritytokenProvider.ModelForAuth
{
    public class UserApiAuthntication
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int Age { get; set; }
        public  Role RoleType { get; set; }
    }
    public  enum Role
    {
        Admin=1,Viewer,Salesman
    }
}
