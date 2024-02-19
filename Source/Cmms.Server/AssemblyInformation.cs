namespace Cmms.Server;

using System.Reflection;

/// <summary>
/// 
/// </summary>
/// <param name="Product"></param>
/// <param name="Description"></param>
/// <param name="Version"></param>
public record AssemblyInformation(string Product, string Description, string Version)
{
    /// <summary>
    /// 
    /// </summary>
    public static readonly AssemblyInformation Current = new(typeof(AssemblyInformation).Assembly);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="assembly"></param>
    public AssemblyInformation(Assembly assembly)
        : this(
            assembly.GetCustomAttribute<AssemblyProductAttribute>()!.Product,
            assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()!.Description,
            assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()!.Version)
    {
    }
}
