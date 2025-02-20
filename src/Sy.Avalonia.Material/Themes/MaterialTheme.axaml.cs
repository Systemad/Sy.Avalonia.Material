using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using System.Diagnostics;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using MaterialColorUtilities.Palettes;
using MaterialColorUtilities.Schemes;
using MaterialColorUtilities.Utils;
using Sy.Avalonia.Material.Utils;
using Style = MaterialColorUtilities.Palettes.Style;

namespace Sy.Avalonia.Material.Themes;



public partial class MaterialTheme : Styles, IResourceNode
{
    private readonly Application _app;

    private const string DefaultVariantKey = "Light";

    private readonly ResourceDictionary _defaultSchemeDark;
    private readonly ResourceDictionary _defaultSchemeLight;

    private CorePalette _corePalette = CorePalette.Of(11);

    private Style _style = Style.Vibrant;

    public Action<Scheme<uint>>? OnColorThemeChanged { get; set; }
    public Action<ThemeVariant>? OnBaseThemeChanged { get; set; }

    public MaterialTheme()
    {
        AvaloniaXamlLoader.Load(this);
        _app = Application.Current!;
        _app.ActualThemeVariantChanged += (_, e) => OnBaseThemeChanged?.Invoke(_app.ActualThemeVariant);
        _defaultSchemeDark = GetAndRemove<ResourceDictionary>("DefaultSchemeTokensDark");
        _defaultSchemeLight = GetAndRemove<ResourceDictionary>("DefaultSchemeTokensLight");
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


    public void ChangeTheme(Color seed)
    {
        var hey = ColorUtils.ArgbFromRgb(seed.R, seed.G, seed.B);
        ApplySchemes(hey);
    }

    public void SwitchBaseTheme()
    {
        if (Application.Current is null) return;
        var newBase = Application.Current.ActualThemeVariant == ThemeVariant.Dark
            ? ThemeVariant.Light
            : ThemeVariant.Dark;
        Application.Current.RequestedThemeVariant = newBase;
    }

    public void ApplySchemes(ResourceDictionary lightResourceDictionary, ResourceDictionary darkResourceDictionary)
    {
        Resources.Remove("DefaultSchemeTokensLight");
        Resources.Remove("DefaultSchemeTokensDark");

        Resources.MergedDictionaries.Add(lightResourceDictionary);
        Resources.MergedDictionaries.Add(darkResourceDictionary);
    }

    private void ApplySchemes(uint seed)
    {
        /*
        var newBase = Application.Current.ActualThemeVariant == ThemeVariant.Dark
            ? ThemeVariant.Light
            : ThemeVariant.Dark;
        uint uintColor = Convert.ToUInt32(seed.Replace("#", ""), 16);
        */
        CorePalette corePalette = CorePalette.Of(seed /*0x0000FF 11 */);
        Scheme<uint> lightScheme = new LightSchemeMapper().Map(corePalette);
        Scheme<uint> darkScheme = new DarkSchemeMapper().Map(corePalette);

        //Scheme<Color> lightSchemeColor = lightScheme.Convert(x => Color.FromArgb((byte)x));
        //Scheme<string> lightSchemeString = lightScheme.Convert(x => "#" + x.ToString("X")[2..]);
        //ChangeLightPalette(lightSchemeColor);

        //var scheme = newBase == ThemeVariant.Dark ? _defaultSchemeDark : _defaultSchemeLight;

        var newLightRd = ResourceExtensionsExt.CreateResourceDictionary(lightScheme);
        var newDarkRd = ResourceExtensionsExt.CreateResourceDictionary(darkScheme);
        Resources.Remove("DefaultSchemeTokensLight");
        Resources.Remove("DefaultSchemeTokensDark");

        /*
        var newDict = new ResourceDictionary();
        foreach (var kvp in scheme)
        {
            newDict.Add(kvp.Key, kvp.Value);
        }
        */

        Resources.Add("DefaultSchemeTokensLight", newLightRd); //MergedDictionaries.Add(newLightRd);
        Resources.MergedDictionaries.Add(newDarkRd);
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
        return variantKey switch
        {
            "Light" or "Dark" => true,
            _ => false
        };
    }
}