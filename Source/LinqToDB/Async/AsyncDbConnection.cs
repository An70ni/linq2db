using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

namespace LinqToDB.Async
{
	/// <summary>
	/// Implements <see cref="IAsyncDbConnection"/> wrapper over <see cref="DbConnection"/> instance with
	/// synchronous implementation of asynchronous methods.
	/// Providers with async operations support could override its methods with asynchronous implementations.
	/// </summary>
	[PublicAPI]
	public class AsyncDbConnection : IAsyncDbConnection
	{
		protected internal AsyncDbConnection(DbConnection connection)
		{
			Connection = connection ?? throw new ArgumentNullException(nameof(connection));
		}

		public virtual DbConnection Connection { get; }

		public virtual DbConnection? TryClone()
		{
			try
			{
				return Connection is ICloneable cloneable
					? (DbConnection)cloneable.Clone()
					: null;
			}
			catch
			{
				// this try-catch added to handle errors like this one from MiniProfiler's ProfiledDbConnection
				// "NotSupportedException : Underlying SqliteConnection is not cloneable"
				// because wrapper implements ICloneable but wrapped connection doesn't
				// exception-less solution will be always return null for wrapped connections which is also meh
				return null;
			}
		}

		[AllowNull]
		public virtual string ConnectionString
		{
			get => Connection.ConnectionString;
			set => Connection.ConnectionString = value;
		}

		public virtual ConnectionState State => Connection.State;

		public virtual DbCommand CreateCommand() => Connection.CreateCommand();

		public virtual void Open     ()                                    => Connection.Open();

		public virtual Task OpenAsync(CancellationToken cancellationToken)
#if NO_ASYNC && THE_RAOT_CORE
		{
			Open();
			return TaskExEx.CompletedTask; 
		}
#else
			=> Connection.OpenAsync(cancellationToken);
#endif

		public virtual void Close     () => Connection.Close();
		public virtual Task CloseAsync()
		{
#if NETSTANDARD2_1PLUS
			return Connection.CloseAsync();
#else
			Close();
#if THE_RAOT_CORE
			return TaskExEx.CompletedTask;
#else
			return TaskEx.CompletedTask;
#endif
#endif
		}

		public virtual IAsyncDbTransaction BeginTransaction() => AsyncFactory.Create(Connection.BeginTransaction());
		public virtual IAsyncDbTransaction BeginTransaction(IsolationLevel isolationLevel) => AsyncFactory.Create(Connection.BeginTransaction(isolationLevel));

#if !NATIVE_ASYNC
				public virtual Task<IAsyncDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
#if THE_RAOT_CORE
					=> TaskEx.FromResult(BeginTransaction());
#else
					=> Task.FromResult(BeginTransaction());
#endif
#elif !NETSTANDARD2_1PLUS
		public virtual ValueTask<IAsyncDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
			=> new(BeginTransaction());
#else
		public virtual async ValueTask<IAsyncDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
		{
			var transaction = await Connection.BeginTransactionAsync(cancellationToken)
				.ConfigureAwait(Common.Configuration.ContinueOnCapturedContext);
			return AsyncFactory.Create(transaction);
		}
#endif

#if !NATIVE_ASYNC
		public virtual Task<IAsyncDbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken)
#if THE_RAOT_CORE
				=> TaskEx.FromResult(BeginTransaction(isolationLevel));
#else
				=> Task.FromResult(BeginTransaction(isolationLevel));
#endif
#elif !NETSTANDARD2_1PLUS
		public virtual ValueTask<IAsyncDbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken)
			=> new(BeginTransaction(isolationLevel));
#else
		public virtual async ValueTask<IAsyncDbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken)
		{
			var transaction = await Connection.BeginTransactionAsync(isolationLevel, cancellationToken)
				.ConfigureAwait(Common.Configuration.ContinueOnCapturedContext);
			return AsyncFactory.Create(transaction);
		}
#endif

#region IDisposable
		public virtual void Dispose() => Connection.Dispose();
#endregion

#region IAsyncDisposable
#if !NATIVE_ASYNC && !THE_RAOT_CORE
		public virtual Task DisposeAsync()
		{
			Dispose();
			return TaskEx.CompletedTask;
		}
#else
		public virtual ValueTask DisposeAsync()
		{
#if NATIVE_ASYNC
			if (Connection is IAsyncDisposable asyncDisposable)
				return asyncDisposable.DisposeAsync();
#endif

			Dispose();
			return default;
		}
#endif
#endregion
}
}
