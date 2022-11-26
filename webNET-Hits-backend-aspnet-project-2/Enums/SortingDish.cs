using System.Text.Json.Serialization;

namespace webNET_Hits_backend_aspnet_project_2.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortingDish
{
    NameAsk,
    NameDesk,
    PriceAsk,
    PriceDesk,
    RatingAsk,
    RatingDesk
}
