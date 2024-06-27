using System.Text.Json.Serialization;

namespace BunnySurfers.API.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        None = 0,  // Default; unauthenticated/unauthorized
        Student,
        Teacher,
        Admin
    }
}
