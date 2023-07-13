using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Enumerators;
using System.Text.Json.Serialization;

namespace Questao5.Application.Commands.Responses
{
    public class CreateMovementResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? IdMovimento { get; set; }
        // public MovementErrorType TypeError { get; set; }
        public string Description { get; set; }
        public int Result { get; set; }
    }
}
