using Avalonia.Controls;
using MaterialColorUtilities.Schemes;

namespace Sy.Avalonia.Material.Utils;

public static class ResourceExtensionsExt
{
    public static ResourceDictionary CreateResourceDictionary(Scheme<uint> scheme)
    {
        Scheme<string> schemeString = scheme.Convert(x => "#" + x.ToString("X")[2..]);
        var rd = new ResourceDictionary
        {
            ["md.sys.color.primary"] = schemeString.Primary.ParseColor(),
            ["md.sys.color.on-primary"] = schemeString.OnPrimary.ParseColor(),
            ["md.sys.color.primary-container"] = schemeString.PrimaryContainer.ParseColor(),
            ["md.sys.color.on-primary-container"] = schemeString.OnPrimaryContainer.ParseColor(),
            ["md.sys.color.secondary"] = schemeString.Secondary.ParseColor(),
            ["md.sys.color.on-secondary"] = schemeString.OnSecondary.ParseColor(),
            ["md.sys.color.secondary-container"] = schemeString.SecondaryContainer.ParseColor(),
            ["md.sys.color.on-secondary-container"] = schemeString.OnSecondaryContainer.ParseColor(),
            ["md.sys.color.tertiary"] = schemeString.Tertiary.ParseColor(),
            ["md.sys.color.on-tertiary"] = schemeString.OnTertiary.ParseColor(),
            ["md.sys.color.tertiary-container"] = schemeString.TertiaryContainer.ParseColor(),
            ["md.sys.color.on-tertiary-container"] = schemeString.OnTertiaryContainer.ParseColor(),
            ["md.sys.color.error"] = schemeString.OnError.ParseColor(),
            ["md.sys.color.on-error"] = schemeString.OnError.ParseColor(),
            ["md.sys.color.error-container"] = schemeString.ErrorContainer.ParseColor(),
            ["md.sys.color.on-error-container"] = schemeString.OnErrorContainer.ParseColor(),
            ["md.sys.color.background"] = schemeString.Background.ParseColor(),
            ["md.sys.color.on-background"] = schemeString.OnBackground.ParseColor(),
            ["md.sys.color.surface"] = schemeString.Surface.ParseColor(),
            ["md.sys.color.on-surface"] = schemeString.OnSurface.ParseColor(),
            ["md.sys.color.surface-variant"] = schemeString.SurfaceVariant.ParseColor(),
            ["md.sys.color.on-surface-variant"] = schemeString.OnSurfaceVariant.ParseColor(),
            ["md.sys.color.outline"] = schemeString.Outline.ParseColor(),
            ["md.sys.color.outline-variant"] = schemeString.OutlineVariant.ParseColor(),
            ["md.sys.color.shadow"] = schemeString.Shadow.ParseColor(),
            ["md.sys.color.inverse-surface"] = schemeString.Surface.ParseColor(),
            ["md.sys.color.inverse-on-surface"] = schemeString.OnSurface.ParseColor(),
            ["md.sys.color.inverse-primary"] = schemeString.InversePrimary.ParseColor(),
            ["md.sys.color.surface-dim"] = schemeString.SurfaceDim.ParseColor(),
            ["md.sys.color.surface-bright"] = schemeString.SurfaceBright.ParseColor(),
            ["md.sys.color.surface-container-low"] = schemeString.SurfaceContainerLow.ParseColor(),
            ["md.sys.color.surface-container-lowest"] = schemeString.SurfaceContainerLowest.ParseColor(),
            ["md.sys.color.surface-container"] = schemeString.SurfaceContainer.ParseColor(),
            ["md.sys.color.surface-container-high"] = schemeString.SurfaceContainerHigh.ParseColor(),
            ["md.sys.color.surface-container-highest"] = schemeString.SurfaceContainerHighest.ParseColor()
        };

        return rd;
    }
}