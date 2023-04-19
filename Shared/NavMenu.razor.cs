using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Portfolio.Shared;


public partial class NavMenu
{
    [Inject] private IJSRuntime Js { get; set; }
    
    private bool _lightDarkMode;

    public bool LightDarkMode
    {
        get => _lightDarkMode;
        set
        {
            _lightDarkMode = value;
            var mode = value ? "dark" : "light";
            Js.InvokeVoidAsync("SetTheme", mode);
            Js.InvokeVoidAsync("AddToStorage", "theme", mode);
            StateHasChanged();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var localStorageTheme = await Js.InvokeAsync<string>("ReadStorage", "theme");

            if (!string.IsNullOrEmpty(localStorageTheme))
            {
                LightDarkMode = localStorageTheme != "light";
            }
            else
            {
                var systemTheme = await Js.InvokeAsync<string>("GetSystemTheme");
                LightDarkMode = systemTheme != "light";
            } 
        }
    }
}