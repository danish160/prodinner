using System.Collections.Generic;
using System.Linq;

namespace Omu.ProDinner.WebUI.Utils
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Page<T>(this IEnumerable<T> list, int page, int pageSize)
        {
            return list.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}