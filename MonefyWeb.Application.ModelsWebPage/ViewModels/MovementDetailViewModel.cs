namespace MonefyWeb.Application.ModelsWebPage.ViewModels
{
    public class MovementDetailViewModel
    {
        public List<MovementSectionViewModel> Sections { get; set; }
    }

    public class MovementSectionViewModel
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public List<MovementViewModel> Movements { get; set; }
    }

    public class MovementViewModel
    {
        public string DetailInfo { get; set; }
    }
}