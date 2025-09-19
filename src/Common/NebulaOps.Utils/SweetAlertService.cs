using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using static NebulaOps.Utils.DefaultResponseHelper;

namespace NebulaOps.Utils;
public class SweetAlertService(IJSRuntime _jsRuntime, NavigationManager _navigation, ICustomAuthenticationStateProvider _authenticationStateProvider) : ISweetAlertService
{
    public async Task Show(string title, string message, SweetAlertType type) => await _jsRuntime.InvokeVoidAsync(
        "window.sweetalertInterop.showSweetAlert",
        title,
        message,
        type.ToString().ToLower());

    public async Task ShowResponse(DefaultResponse response)
    {
        SweetAlertType type = SweetAlertType.Error;

        if (response.HttpStatus.IsSuccessStatusCode())
        {
            type = SweetAlertType.Success;
        }
        else if (response.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
        {
            type = SweetAlertType.Warning;
            await _authenticationStateProvider.MarkUserAsLoggedOut().ConfigureAwait(false);
            _navigation.NavigateTo("/login");
        }
        else if (response.HttpStatus.IsClientErrorStatusCode() || response.HttpStatus.IsServerErrorStatusCode())
        {
            type = SweetAlertType.Error;
        }

        await Show(string.Empty, response.Message, type);
    }
}

