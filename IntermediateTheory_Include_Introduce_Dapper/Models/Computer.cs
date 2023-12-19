

using System.Text.Json.Serialization;

namespace IntermediateTheory_Include_Introduce_Dapper.Models
{
    public class Computer
    {
        [JsonPropertyName(nameof(ComputerSnake.computer_id))]
        public int ComputerId { get; set; }

        [JsonPropertyName(nameof(ComputerSnake.motherboard))]
        public string Motherboard { get; set; } = string.Empty;

        [JsonPropertyName(nameof(ComputerSnake.cpu_cores))]
        public int? CPUCores { get; set; }

        [JsonPropertyName(nameof(ComputerSnake.has_wifi))]
        public bool HasWifi { get; set; }

        [JsonPropertyName(nameof(ComputerSnake.has_lte))]
        public bool HasLTE { get; set; }

        [JsonPropertyName(nameof(ComputerSnake.release_date))]
        public DateTime? ReleaseDate { get; set; }

        [JsonPropertyName(nameof(ComputerSnake.price))]
        public decimal Price { get; set; }

        [JsonPropertyName(nameof(ComputerSnake.video_card))]
        public string VideoCard { get; set; } = string.Empty;
    }
}
