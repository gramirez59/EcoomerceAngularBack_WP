using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceCore.Core.Domain.Entities.Security
{
    public class RegisterUserModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MinLength(6)]
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password), Compare("Password")]
        public string Confirm { get; set; }
    }
}