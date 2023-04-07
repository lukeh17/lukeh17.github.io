using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Portfolio.Shared;


public partial class NavMenu
{
    [Inject] private IJSRuntime? Js { get; set; }
    
    private string _mode = "light";
    private bool _isChecked = false;

    private async Task SwitchMode()
    {
        _mode = _mode == "light" ? "dark" : "light";
        _isChecked = !_isChecked;

        await Js.InvokeVoidAsync("SetTheme", _mode);
        await Js.InvokeVoidAsync("AddToStorage", "theme", _mode);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        var localStorageTheme = await Js.InvokeAsync<string>("ReadStorage", "theme");

        if (!string.IsNullOrEmpty(localStorageTheme))
        {
            _mode = localStorageTheme;
            _isChecked = _mode != "dark";
        }
        else
        {
            var systemTheme = await Js.InvokeAsync<string>("GetSystemTheme");
            _mode = systemTheme;
            _isChecked = _mode != "dark";
        }
    }
}