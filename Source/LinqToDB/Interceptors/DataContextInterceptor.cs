using System;
using System.Threading.Tasks;

namespace LinqToDB.Interceptors
{
	public abstract class DataContextInterceptor : IDataContextInterceptor
	{
		public virtual void OnClosed      (DataContextEventData eventData) { }
		public virtual void OnClosing     (DataContextEventData eventData) { }
		public virtual Task OnClosedAsync (DataContextEventData eventData) =>
#if THE_RAOT_CORE
			TaskExEx.CompletedTask;
#else
			TaskEx.CompletedTask;
#endif
		public virtual Task OnClosingAsync(DataContextEventData eventData) =>
#if THE_RAOT_CORE
			TaskExEx.CompletedTask;
#else
			TaskEx.CompletedTask;
#endif
	}
}
