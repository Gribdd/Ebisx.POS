using System.Text.Json.Serialization;

namespace Ebisx.POS.Presentation.Models;

public partial class BaseModel : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("id")]
    public partial Guid Id { get; set; }
}
