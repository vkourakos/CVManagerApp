using CVManagerapp.Models;
using X.PagedList;

namespace CVManagerapp.ViewModels
{
    public class CVListVM
    {
        public IPagedList<CV>? Cvs { get; set; }
        public string? SearchString { get; set; }

        public CVListVM(IPagedList<CV>? cvs, string? searchString)
        {
            Cvs = cvs;
            SearchString = searchString;
        }
    }
}
