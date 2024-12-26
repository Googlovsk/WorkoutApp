using System.Text.Json.Serialization;

namespace WorkoutAppApi.Models.DTOs
{
    public class RegistrationModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [JsonPropertyName("fullName")]
        public string FullName { get; set; }
        /// <summary>
        /// Email пользователя
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; }
        /// <summary>
        /// Телефон пользователя
        /// </summary>
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        /// <summary>
        /// Роль пользователя
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; }
        /// <summary>
        /// Пол пользователя
        /// </summary>
        [JsonPropertyName("gender")]
        public string Gender { get; set; }
    }

    public class LoginModel
    {
        /// <summary>
        /// Email пользователя
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }
    }
}
