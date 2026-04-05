namespace Inflow.UnitTests.UI.Common.Extensions;

public static class BunitJsInteropExtensions
{
    public static void SetupMudBlazorJsInterop(this BunitJSInterop bunitJsInterop)
    {
        bunitJsInterop.SetupVoid("mudElementRef.addOnBlurEvent", _ => true);
        bunitJsInterop.SetupVoid("mudKeyInterceptor.connect", _ => true);
        bunitJsInterop.Setup<int>("mudpopoverHelper.countProviders");
        bunitJsInterop.SetupVoid("mudThemeProvider.watchDarkMode", _ => true);
        bunitJsInterop
            .SetupVoid("mudKeyInterceptor.disconnect", _ => true)
            .SetVoidResult();

        bunitJsInterop.SetupVoid("mudPopover.dispose").SetVoidResult();
    }
}