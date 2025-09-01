using Infrastructure.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Users
{
    public class UserDto
    {
        public int UserID { get; set; }


        public string FullName { get; set; } = null!;

        public string Username { get; set; } = null!;

        //public string Password { get; set; } = null!;


        public int? DoctorID { get; set; }


        public string? Status { get; set; }

        public DateTime? LastLogin { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
        public  DoctorDto? Doctor { get; set; }



    }
}
