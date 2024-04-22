using CVManagerapp.Models;

namespace CVManagerapp.ViewModels
{
    public class CVListVM
    {
        public IList<CV> Cvs { get; set; }

        public CVListVM(IList<CV> cvs)
        {
            Cvs = cvs;
        }
    }
}
