using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.DTOs
{
    public class PaginateList<T>
    {
        public PaginateList()
        {
            Lista = new List<T>();
        }

        public List<T> Lista { get; set; }
        public int RegistroInicial { get; set; }
        public int RegistroFinal { get; set; }
        public int TotalRegistros { get; set; }
        public int NumeroPaginas { get; set; }
        public int Pagina { get; set; }
        public int RegistrosPorPagina { get; set; }
        public object InformacoesAdicionais { get; set; }

        public void CalcularRegistros()
        {
            if (Pagina <= 0)
            {
                Pagina = 1;
            }
            RegistroInicial = ((Pagina - 1) * RegistrosPorPagina) + 1;
            RegistroFinal = (RegistroInicial + RegistrosPorPagina) - 1;
            if (RegistroFinal == 0)
            {
                RegistroFinal = TotalRegistros;
            }
            else if (RegistroFinal > TotalRegistros)
            {
                RegistroFinal = TotalRegistros;
            }
            NumeroPaginas = (int)Math.Ceiling((double)TotalRegistros / RegistrosPorPagina);
            if (NumeroPaginas <= 0)
            {
                NumeroPaginas = 1;
            }
            if (Pagina > NumeroPaginas)
            {
                Pagina = NumeroPaginas;
            }
            if (RegistrosPorPagina == 0)
            {
                RegistrosPorPagina = TotalRegistros;
            }
            if (TotalRegistros == 0)
            {
                RegistroInicial = 0;
                NumeroPaginas = 0;
                Pagina = 0;
            }
        }
    }
