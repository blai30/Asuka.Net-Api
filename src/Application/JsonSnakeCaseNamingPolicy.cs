using System.Text.Json;
using Humanizer;

namespace AsukaApi.Application;

public sealed class JsonSnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name) => name.Underscore();
}
