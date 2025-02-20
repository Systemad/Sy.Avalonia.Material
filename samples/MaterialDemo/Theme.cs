using Avalonia.Controls;
using MaterialColorUtilities.Palettes;
using MaterialColorUtilities.Schemes;

namespace MaterialDemo;

public static class ResourceExtensions
{
    public static ResourceDictionary CreateResourceDictionary(Scheme<uint> scheme)
    {
        var rd = new ResourceDictionary
        {
            ["md.sys.color.primary"] = scheme.Primary, //.ParseColor();
            ["md.sys.color.on-primary"] = scheme.OnPrimary, //.ParseColor();
            ["md.sys.color.primary-container"] = scheme.PrimaryContainer, //.ParseColor();
            ["md.sys.color.on-primary-container"] = scheme.OnPrimaryContainer, //.ParseColor();
            ["md.sys.color.secondary"] = scheme.Secondary, //.ParseColor();
            ["md.sys.color.on-secondary"] = scheme.OnSecondary, //.ParseColor();
            ["md.sys.color.secondary-container"] = scheme.SecondaryContainer, //.ParseColor();
            ["md.sys.color.on-secondary-container"] = scheme.OnSecondaryContainer, //.ParseColor();
            ["md.sys.color.tertiary"] = scheme.Tertiary, //.ParseColor();
            ["md.sys.color.on-tertiary"] = scheme.OnTertiary, //.ParseColor();
            ["md.sys.color.tertiary-container"] = scheme.TertiaryContainer, //.ParseColor();
            ["md.sys.color.on-tertiary-container"] = scheme.OnTertiaryContainer, //.ParseColor();
            ["md.sys.color.error"] = scheme.OnError, //.ParseColor();
            ["md.sys.color.on-error"] = scheme.OnError, //.ParseColor();
            ["md.sys.color.error-container"] = scheme.ErrorContainer, //.ParseColor();
            ["md.sys.color.on-error-container"] = scheme.OnErrorContainer, //.ParseColor();
            ["md.sys.color.background"] = scheme.Background, //.ParseColor();
            ["md.sys.color.on-background"] = scheme.OnBackground, //ParseColor();
            ["md.sys.color.surface"] = scheme.Surface, //.ParseColor();
            ["md.sys.color.on-surface"] = scheme.OnSurface, //.ParseColor();
            ["md.sys.color.surface-variant"] = scheme.SurfaceVariant, //.ParseColor();
            ["md.sys.color.on-surface-variant"] = scheme, //.OnSurfaceVariant.ParseColor();
            ["md.sys.color.outline"] = scheme.Outline, //.ParseColor();
            ["md.sys.color.outline-variant"] = scheme.OutlineVariant, //.ParseColor();
            ["md.sys.color.shadow"] = scheme.Shadow, //.ParseColor();
            ["md.sys.color.inverse-surface"] = scheme.Surface, //.ParseColor();
            ["md.sys.color.inverse-on-surface"] = scheme.OnSurface, //.ParseColor();
            ["md.sys.color.inverse-primary"] = scheme.InversePrimary, //.ParseColor();
            ["md.sys.color.surface-dim"] = scheme.SurfaceDim, //.ParseColor();
            ["md.sys.color.surface-bright"] = scheme.SurfaceBright, //.ParseColor();
            ["md.sys.color.surface-container-low"] = scheme.SurfaceContainerLow, //.ParseColor();
            ["md.sys.color.surface-container-lowest"] = scheme.SurfaceContainerLowest, //.ParseColor();
            ["md.sys.color.surface-container"] = scheme.SurfaceContainer, //.ParseColor();
            ["md.sys.color.surface-container-high"] = scheme.SurfaceContainerHigh, //ParseColor();
            ["md.sys.color.surface-container-highest"] = scheme.SurfaceContainerHighest //.ParseColor();
        };

        return rd;
    }
}

public class Theme
{
    public string Name { get; set; }
    public uint SeedColor { get; set; }

    private Scheme<uint> _lightScheme;
    private Scheme<uint> _darkScheme;
    private CorePalette _corePalette;

    public ResourceDictionary lightSchemeDictionary { get; }
    public ResourceDictionary darkSchemeDictionary { get; }
    
    public Theme(string name, uint seedColor)
    {
        Name = name;
        SeedColor = seedColor;
        _corePalette = CorePalette.Of(SeedColor);
        _lightScheme = new LightSchemeMapper().Map(_corePalette);
        _darkScheme = new DarkSchemeMapper().Map(_corePalette);
        lightSchemeDictionary = ResourceExtensions.CreateResourceDictionary(_lightScheme);
        darkSchemeDictionary = ResourceExtensions.CreateResourceDictionary(_darkScheme);
        
    }

}