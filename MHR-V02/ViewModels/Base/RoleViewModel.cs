namespace MHR_V02.ViewModels.Base
{
    public class RoleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsSelected { get; set; } // آیا نقش انتخاب شده است؟

    }
}
