namespace MHR_V02.ViewModels.Base
{
    public class ActionLogViewModel
    {
        public Guid Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string HttpMethod { get; set; }
        public bool IsSelected { get; set; } // آیا نقش انتخاب شده است؟

    }
}
