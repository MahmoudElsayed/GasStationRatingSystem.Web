using GasStationRatingSystem.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GasStationRatingSystem.DTO
{
    public class UserParameters
    {
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.UsernameRequired))]
        public string Username { get; set; }
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.PasswordRequired))]

        public string Password { get; set; }
    }
    public class UserChangePasswordParameters
    {
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.PasswordRequired))]

        public string NewPassword { get; set; }
    }
    public class UserSendCodeParameters
    {
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.UsernameRequired))]

        public string Username { get; set; }
    }
    public class UserForgetPasswordParameters
    {
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.UsernameRequired))]

        public string Username { get; set; }

        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.PasswordRequired))]

        public string NewPassword { get; set; }
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.ValidationCodeRequired))]

        public string Code { get; set; }
    }
    public class UserResultDTO
    {
        public Guid Id { get; set; }
        public string BearerToken { get; set; }
        public string Username { get; set; }
        public bool UseDefaultPassword { get; set; } = true;
    }
}
