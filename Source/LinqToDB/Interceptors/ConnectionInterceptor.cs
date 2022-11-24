using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace LinqToDB.Interceptors
{
	public abstract class ConnectionInterceptor : IConnectionInterceptor
	{
		public virtual void ConnectionOpened(ConnectionEventData eventData, DbConnection connection)
		{
		}

		public virtual Task ConnectionOpenedAsync(ConnectionEventData eventData, DbConnection connection, CancellationToken cancellationToken)
		{
#if THE_RAOT_CORE
			return TaskExEx.CompletedTask;
#else
			return TaskEx.CompletedTask;
#endif
		}

		public virtual void ConnectionOpening(ConnectionEventData eventData, DbConnection connection)
		{
		}

		public virtual Task ConnectionOpeningAsync(ConnectionEventData eventData, DbConnection connection, CancellationToken cancellationToken)
		{
#if THE_RAOT_CORE
			return TaskExEx.CompletedTask;
#else
			return TaskEx.CompletedTask;
#endif
		}
	}
}
