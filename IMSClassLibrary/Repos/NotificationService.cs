using Microsoft.JSInterop;

namespace IMSClassLibrary.Repos
{
   
    public class NotificationService
    {
      
        private readonly IJSRuntime js;

        public NotificationService(IJSRuntime jsRuntime)
        {
            js = jsRuntime;
        }

        public void ShowSuccess(string message)
        {
            this.js.InvokeVoidAsync("success_noti", message);
        }
        
        public void ShowWarning(string message)
        {
            this.js.InvokeVoidAsync("warning_noti", message);
        }

        public void ShowError(string message)
        {
            this.js.InvokeVoidAsync("error_noti", message);
        }

        public void ShowInfo(string message)
        {
            this.js.InvokeVoidAsync("info_noti", message);
        }

        public void ShowMessage(string message)
        {
            this.js.InvokeVoidAsync("default_noti", message);
        }


    }
}
