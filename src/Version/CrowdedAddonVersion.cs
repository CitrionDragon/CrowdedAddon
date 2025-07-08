using Hazel;

namespace CrowdedAddon.Version;


/// <summary>
/// Version Representing this Addon
/// </summary>
public class CrowdedAddonVersion: VentLib.Version.Version
{
    public override VentLib.Version.Version Read(MessageReader reader)
    {
        return new CrowdedAddonVersion();
    }

    protected override void WriteInfo(MessageWriter writer)
    {
    }

    public override string ToSimpleName()
    {
        return "Crowded Addon Version v1.0.0";
    }

    public override string ToString() => "CrowdedAddonVersion";
}