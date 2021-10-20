using System.Text.Json.Serialization;

namespace TFLAssessment.Domain.Entities
{
    /// <summary>
    /// Road entity
    /// </summary>
    public class Road
    {
        /// <summary>
        /// Comma-separated list of road identifiers e.g. "A406, A2"
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Display Name
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Status Serverity
        /// </summary>
        [JsonPropertyName("statusSeverity")]
        public string StatusSeverity { get; set; }

        /// <summary>
        /// Status Severity Description
        /// </summary>
        [JsonPropertyName("statusSeverityDescription")]
        public string StatusSeverityDescription { get; set; }

        [JsonPropertyName("bounds")]
        public string Bounds { get; set; }

        [JsonPropertyName("envelope")]
        public string Envelope { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}