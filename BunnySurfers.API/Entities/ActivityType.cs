using System.Text.Json.Serialization;

namespace BunnySurfers.API.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ActivityType
    {
        None = 0,
        Lecture,
        Assignment,
        ELearning,
        Practice,
        Other
    }
}
