using System.Data;
using Lab03.Data;

namespace Lab03.ViewModels
{
    public class ReservasDataTableViewModel : ViewModelBase
    {
        private readonly ReservaRepository _repo = new();
        public DataTable ReservasTabla { get; set; }

        public ReservasDataTableViewModel()
        {
            ReservasTabla = _repo.ObtenerReservasDataTable();
        }
    }
}
