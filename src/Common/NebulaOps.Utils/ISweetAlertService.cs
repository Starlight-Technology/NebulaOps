using System.Net;

using static NebulaOps.Utils.DefaultResponseHelper;

namespace NebulaOps.Utils;
public interface ISweetAlertService
{
    Task Show(string title, string message, SweetAlertType type);

    Task ShowResponse(DefaultResponse response);
}
