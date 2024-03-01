namespace Cmms.Api.Common.ViewModels;

using System.Text.Json.Serialization;
using Cmms.Api.Assets;

/// <summary>
/// Enables faster serialization and de-serialization with fewer allocations by generating source code.
/// </summary>
[JsonSerializable(typeof(Asset[]))]
[JsonSerializable(typeof(Connection<Asset>[]))]
//[JsonSerializable(typeof(SaveAsset[]))]
internal sealed partial class CustomJsonSerializerContext : JsonSerializerContext
{
}
