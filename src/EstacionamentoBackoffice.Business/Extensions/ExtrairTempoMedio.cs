using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Extensions
{
    public static class ExtrairTempoMedio
    {
        public static TimeSpan Extrair(this IEnumerable<TimeSpan> spans) => TimeSpan.FromSeconds(spans.Select(s => s.TotalSeconds).Average());

    }
}
