using System.Text.Json.Serialization;

namespace TFLAssessment.Domain.Entities
{
    /// <summary>
    /// To capture the Error object from the TFL Api 
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Error Message
        /// </summary>
        [JsonPropertyName("message")]
        public string message { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("$type")]
        public string Type { get; set; }

        /// <summary>
        /// Timestamp UTC
        /// </summary>
        [JsonPropertyName("timestampUtc")]
        public string TimestampUtc { get; set; }

        /// <summary>
        /// Exception Type
        /// </summary>
        [JsonPropertyName("exceptionType")]
        public string ExceptionType { get; set; }

        /// <summary>
        /// HTTP Status Code
        /// </summary>
        [JsonPropertyName("httpStatusCode")]
        public int HttpStatusCode { get; set; }

        /// <summary>
        /// Http Status
        /// </summary>
        [JsonPropertyName("httpStatus")]
        public string HttpStatus { get; set; }
    }
}
