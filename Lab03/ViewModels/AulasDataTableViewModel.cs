using System.Data;
using Lab03.Data;

namespace Lab03.ViewModels
{
    public class AulasDataTableViewModel : ViewModelBase
    {
        private readonly AulaRepository _repo = new();
        public DataTable AulasTabla { get; set; }

        public AulasDataTableViewModel()
        {
            AulasTabla = _repo.ObtenerAulasDataTable();
        }
    }
}