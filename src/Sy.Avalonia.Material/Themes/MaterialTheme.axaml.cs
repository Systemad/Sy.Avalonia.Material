using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using System.Diagnostics;
using Avalonia.Media;
using MaterialColorUtilities.Palettes;
using MaterialColorUtilities.Schemes;
using Style = MaterialColorUtilities.Palettes.Style;

namespace Sy.Avalonia.Material.Themes;

public static class ColorExt
{
    public static Color ParseColor(this string hex) => Color.Parse(hex);
}

public partial class MaterialTheme : Styles, IResourceNode
{
    private readonly Application _app;

    private const string DefaultVariantKey = "Light";

    private readonly ResourceDictionary _defaultSchemeDark;
    private readonly ResourceDictionary _defaultSchemeLight;

    private CorePalette _corePalette = CorePalette.Of(11);
    Scheme<uint> _lightScheme;
    Scheme<uint> _darkScheme; // = new LightSchemeMapper().Map(_corePalette);
    private Style _style = Style.Vibrant;


    public MaterialTheme()
    {
        AvaloniaXamlLoader.Load(this);
        _app = Application.Current!;
        _defaultSchemeDark = GetAndRemove<ResourceDictionary>("DefaultSchemeTokensDark");
        _defaultSchemeLight = GetAndRemove<ResourceDictionary>("DefaultSchemeTokensLight");

        //lightScheme = new LightSchemeMapper().Map(_corePalette);
        //darkScheme = new DarkSchemeMapper().Map(_corePalette);

        //Scheme<string> lightSchemeString = lightScheme.Convert(x => "#" + x.ToString("X")[2..]);
        //ChangeTheme(lightSchemeString);
    }

    public static MaterialTheme GetInstance() => GetInstance(Application.Current!);

    public static MaterialTheme GetInstance(Application app)
    {
        var theme = app.Styles.FirstOrDefault(style => style is MaterialTheme);
        if (theme is not MaterialTheme mTheme)
            throw new InvalidOperationException("aa");

        return mTheme;
    }

    bool IResourceNode.TryGetResource(object key, ThemeVariant? theme, out object? value)
    {
        var baseSuccess = TryGetResource(key, theme, out value);
        if (baseSuccess) return true;

        var variantKey = GetCurrentVariantKey();
        if (!IsSupportedVariantKey(variantKey)) return false;

        var resources = variantKey switch
        {
            "Dark" => _defaultSchemeDark,
            _ => _defaultSchemeLight,
        };
        return resources.TryGetValue(key, out value);
    }

    public void ChangeTheme(string color)
    {
        CorePalette corePalette = CorePalette.Of(11);
        Scheme<uint> lightScheme = new LightSchemeMapper().Map(corePalette);
        Scheme<string> lightSchemeString = lightScheme.Convert(x => "#" + x.ToString("X")[2..]);
        ApplyTheme(lightSchemeString);
    }
    
    public void ChangeTheme2(string color)
    {
        CorePalette corePalette = CorePalette.Of(11);
        Scheme<uint> lightScheme = new LightSchemeMapper().Map(corePalette);
        Scheme<string> lightSchemeString = lightScheme.Convert(x => "#" + x.ToString("X")[2..]);
        ApplyTheme2(lightSchemeString);
    }

    private void ApplyTheme2(Scheme<string> schemeString)
    {
        Scheme<uint> lightScheme;
        var hey = _defaultSchemeLight["md.sys.color.primary"];

        _defaultSchemeLight["md.sys.color.primary"] = schemeString.Primary.ParseColor();
        _defaultSchemeLight["md.sys.color.on-primary"] = schemeString.OnPrimary.ParseColor();
        _defaultSchemeLight["md.sys.color.primary-container"] = schemeString.PrimaryContainer.ParseColor();
        _defaultSchemeLight["md.sys.color.on-primary-container"] = schemeString.OnPrimaryContainer.ParseColor();

        _defaultSchemeLight["md.sys.color.secondary"] = schemeString.Secondary.ParseColor();
        _defaultSchemeLight["md.sys.color.on-secondary"] = schemeString.OnSecondary.ParseColor();
        _defaultSchemeLight["md.sys.color.secondary-container"] = schemeString.SecondaryContainer.ParseColor();
        _defaultSchemeLight["md.sys.color.on-secondary-container"] = schemeString.OnSecondaryContainer.ParseColor();

        _defaultSchemeLight["md.sys.color.tertiary"] = schemeString.Tertiary.ParseColor();
        _defaultSchemeLight["md.sys.color.on-tertiary"] = schemeString.OnTertiary.ParseColor();
        _defaultSchemeLight["md.sys.color.tertiary-container"] = schemeString.TertiaryContainer.ParseColor();
        _defaultSchemeLight["md.sys.color.on-tertiary-container"] = schemeString.OnTertiaryContainer.ParseColor();

        _defaultSchemeLight["md.sys.color.error"] = schemeString.OnError.ParseColor();
        _defaultSchemeLight["md.sys.color.on-error"] = schemeString.OnError.ParseColor();
        _defaultSchemeLight["md.sys.color.error-container"] = schemeString.ErrorContainer.ParseColor();
        _defaultSchemeLight["md.sys.color.on-error-container"] = schemeString.OnErrorContainer.ParseColor();

        _defaultSchemeLight["md.sys.color.background"] = schemeString.Background.ParseColor();
        _defaultSchemeLight["md.sys.color.on-background"] = schemeString.OnBackground.ParseColor();

        _defaultSchemeLight["md.sys.color.surface"] = schemeString.Surface.ParseColor();
        _defaultSchemeLight["md.sys.color.on-surface"] = schemeString.OnSurface.ParseColor();

        _defaultSchemeLight["md.sys.color.surface-variant"] = schemeString.SurfaceVariant.ParseColor();
        _defaultSchemeLight["md.sys.color.on-surface-variant"] = schemeString.OnSurfaceVariant.ParseColor();

        _defaultSchemeLight["md.sys.color.outline"] = schemeString.Outline.ParseColor();
        _defaultSchemeLight["md.sys.color.outline-variant"] = schemeString.OutlineVariant.ParseColor();

        _defaultSchemeLight["md.sys.color.shadow"] = schemeString.Shadow.ParseColor();

        _defaultSchemeLight["md.sys.color.inverse-surface"] = schemeString.Surface.ParseColor();
        _defaultSchemeLight["md.sys.color.inverse-on-surface"] = schemeString.OnSurface.ParseColor();

        _defaultSchemeLight["md.sys.color.inverse-primary"] = schemeString.InversePrimary.ParseColor();

        _defaultSchemeLight["md.sys.color.surface-dim"] = schemeString.SurfaceDim.ParseColor();
        _defaultSchemeLight["md.sys.color.surface-bright"] = schemeString.SurfaceBright.ParseColor();
        _defaultSchemeLight["md.sys.color.surface-container-low"] = schemeString.SurfaceContainerLow.ParseColor();
        _defaultSchemeLight["md.sys.color.surface-container-lowest"] = schemeString.SurfaceContainerLowest.ParseColor();

        _defaultSchemeLight["md.sys.color.surface-container"] = schemeString.SurfaceContainer.ParseColor();
        _defaultSchemeLight["md.sys.color.surface-container-high"] = schemeString.SurfaceContainerHigh.ParseColor();
        _defaultSchemeLight["md.sys.color.surface-container-highest"] =
            schemeString.SurfaceContainerHighest.ParseColor();

        var hey2 = _defaultSchemeLight["md.sys.color.primary"];
    }

    private void ApplyTheme(Scheme<string> schemeString)
    {
        Scheme<uint> lightScheme;
        var hey = _app.Resources["md.sys.color.primary"];

        _app.Resources["md.sys.color.primary"] = schemeString.Primary.ParseColor();
        _app.Resources["md.sys.color.on-primary"] = schemeString.OnPrimary.ParseColor();
        _app.Resources["md.sys.color.primary-container"] = schemeString.PrimaryContainer.ParseColor();
        _app.Resources["md.sys.color.on-primary-container"] = schemeString.OnPrimaryContainer.ParseColor();

        _app.Resources["md.sys.color.secondary"] = schemeString.Secondary.ParseColor();
        _app.Resources["md.sys.color.on-secondary"] = schemeString.OnSecondary.ParseColor();
        _app.Resources["md.sys.color.secondary-container"] = schemeString.SecondaryContainer.ParseColor();
        _app.Resources["md.sys.color.on-secondary-container"] = schemeString.OnSecondaryContainer.ParseColor();

        _app.Resources["md.sys.color.tertiary"] = schemeString.Tertiary.ParseColor();
        _app.Resources["md.sys.color.on-tertiary"] = schemeString.OnTertiary.ParseColor();
        _app.Resources["md.sys.color.tertiary-container"] = schemeString.TertiaryContainer.ParseColor();
        _app.Resources["md.sys.color.on-tertiary-container"] = schemeString.OnTertiaryContainer.ParseColor();

        _app.Resources["md.sys.color.error"] = schemeString.OnError.ParseColor();
        _app.Resources["md.sys.color.on-error"] = schemeString.OnError.ParseColor();
        _app.Resources["md.sys.color.error-container"] = schemeString.ErrorContainer.ParseColor();
        _app.Resources["md.sys.color.on-error-container"] = schemeString.OnErrorContainer.ParseColor();

        _app.Resources["md.sys.color.background"] = schemeString.Background.ParseColor();
        _app.Resources["md.sys.color.on-background"] = schemeString.OnBackground.ParseColor();

        _app.Resources["md.sys.color.surface"] = schemeString.Surface.ParseColor();
        _app.Resources["md.sys.color.on-surface"] = schemeString.OnSurface.ParseColor();

        _app.Resources["md.sys.color.surface-variant"] = schemeString.SurfaceVariant.ParseColor();
        _app.Resources["md.sys.color.on-surface-variant"] = schemeString.OnSurfaceVariant.ParseColor();

        _app.Resources["md.sys.color.outline"] = schemeString.Outline.ParseColor();
        _app.Resources["md.sys.color.outline-variant"] = schemeString.OutlineVariant.ParseColor();

        _app.Resources["md.sys.color.shadow"] = schemeString.Shadow.ParseColor();

        _app.Resources["md.sys.color.inverse-surface"] = schemeString.Surface.ParseColor();
        _app.Resources["md.sys.color.inverse-on-surface"] = schemeString.OnSurface.ParseColor();

        _app.Resources["md.sys.color.inverse-primary"] = schemeString.InversePrimary.ParseColor();

        _app.Resources["md.sys.color.surface-dim"] = schemeString.SurfaceDim.ParseColor();
        _app.Resources["md.sys.color.surface-bright"] = schemeString.SurfaceBright.ParseColor();
        _app.Resources["md.sys.color.surface-container-low"] = schemeString.SurfaceContainerLow.ParseColor();
        _app.Resources["md.sys.color.surface-container-lowest"] = schemeString.SurfaceContainerLowest.ParseColor();

        _app.Resources["md.sys.color.surface-container"] = schemeString.SurfaceContainer.ParseColor();
        _app.Resources["md.sys.color.surface-container-high"] = schemeString.SurfaceContainerHigh.ParseColor();
        _app.Resources["md.sys.color.surface-container-highest"] = schemeString.SurfaceContainerHighest.ParseColor();

        var hey2 = _app.Resources["md.sys.color.primary"];
    }

    private T GetAndRemove<T>(string key)
    {
        var val = Resources[key] ?? throw new UnreachableException(key);
        if (val is not T t) throw new UnreachableException(key);
        Resources.Remove(key);
        return t;
    }

    private void SetResource(string name, Color color) =>
        _app.Resources[name] = color;

    private string GetCurrentVariantKey()
    {
        var actualVariantObj = Application.Current?.ActualThemeVariant.Key;
        if (actualVariantObj is string actualVariant)
        {
            return actualVariant;
        }

        return DefaultVariantKey;
    }

    private bool IsSupportedVariantKey(string variantKey)
    {
        if (variantKey == "Light") return true;
        if (variantKey == "Dark") return true;
        return false;
    }
}