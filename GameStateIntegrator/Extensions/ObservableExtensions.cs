using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateIntegrator.Extensions
{
    public static class ObservableExtensions
    {
        public static IObservable<T> Only<T>(this IObservable<object> source)
        {
            return source.Where(x => x is T).Cast<T>();
        }
    }
}
