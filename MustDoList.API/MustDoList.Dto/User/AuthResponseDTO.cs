using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MustDoList.Dto.User
{
    public class AuthResponseDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
