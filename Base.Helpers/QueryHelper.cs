using System.Linq;
using System.Reflection;

namespace Base.Helpers
{
    public static class Query
    {
        public static IQueryable<TEntity> Filtrar<TEntity>(this IQueryable<TEntity> consulta, int limite, int pular, string ordenacao) where TEntity : class
        {
            if (!string.IsNullOrEmpty(ordenacao))
            {
                if (ordenacao.Contains("_desc"))
                {
                    consulta = consulta.OrderByDescending(x => x.GetType().GetProperty(ordenacao.Replace("_desc", "").CapitalizeFirst()).GetValue(x));
                }
                else
                {
                    consulta = consulta.OrderBy(x => x.GetType().GetProperty(ordenacao.Replace("_asc", "").CapitalizeFirst()).GetValue(x));
                }
            }

            if (limite > 0)
            {
                consulta = consulta.Skip(pular).Take(limite);
            }

            return consulta;
        }

        public static IQueryable<TEntity> FiltrarPaginado<TEntity>(this IQueryable<TEntity> consulta, int pagina, int registrosPorPagina, string ordenacao) where TEntity : class
        {
            if (!string.IsNullOrEmpty(ordenacao))
            {
                if (ordenacao.Contains("_desc"))
                {
                    consulta = consulta.OrderByDescending(x => x.GetType().GetProperty(ordenacao.Replace("_desc", "").CapitalizeFirst()).GetValue(x));
                }
                else
                {
                    consulta = consulta.OrderBy(x => x.GetType().GetProperty(ordenacao.Replace("_asc", "").CapitalizeFirst()).GetValue(x));
                }
            }

            int pular = 0;
            if (pagina > 1)
            {
                pular = (pagina - 1) * registrosPorPagina;
            }

            if (registrosPorPagina > 0)
            {
                consulta = consulta.Skip(pular).Take(registrosPorPagina);
            }

            return consulta;
        }

        public static IQueryable<TEntity> FiltrarPaginado<TEntity>(this IQueryable<TEntity> consulta, int start, int length, string sortColumn = "", string sortColumnDirection = "asc") where TEntity : class
        {
            if (length == 0)
                length = 10;

            var properties = typeof(TEntity).GetProperties();
            PropertyInfo prop = null;
            foreach (var item in properties)
            {
                if (item.Name.ToLower().Equals(sortColumn.ToLower()))
                {
                    prop = item;
                    break;
                }
            }
            var campoOrdem = prop;
            if (sortColumnDirection == "asc")
            {
                consulta = consulta.OrderBy(campoOrdem.GetValue).AsQueryable().Skip(start).Take(length);
            }
            else
            {
                consulta = consulta.OrderByDescending(campoOrdem.GetValue).AsQueryable().Skip(start).Take(length);
            }
            return consulta;
        }
    }
}
