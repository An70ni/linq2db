using System.Data.Common;
using LinqToDB.Async;

namespace LinqToDB.Data
{
	/// <summary>
	/// Type of operation associated with specific trace event.
	/// </summary>
	/// <seealso cref="TraceInfo"/>
	public enum TraceOperation
	{
#if NET40
		/// <summary>
		/// <see cref="DbConnection.Open"/> operation.
		/// See also <seealso cref="TraceInfo.IsAsync"/>.
		/// </summary>
#else
		/// <summary>
		/// <see cref="DbCommand.ExecuteNonQuery"/> or <see cref="DbCommand.ExecuteNonQueryAsync(System.Threading.CancellationToken)"/> operation.
		/// See also <seealso cref="TraceInfo.IsAsync"/>.
		/// </summary>
#endif
		ExecuteNonQuery,
#if NET40
/// <summary>
/// <see cref="DbConnection.Open"/> operation.
/// See also <seealso cref="TraceInfo.IsAsync"/>.
/// </summary>
#else
		/// <summary>
		/// <see cref="DbCommand.ExecuteReader(System.Data.CommandBehavior)"/> or <see cref="DbCommand.ExecuteReaderAsync(System.Data.CommandBehavior, System.Threading.CancellationToken)"/> operation.
		/// See also <seealso cref="TraceInfo.IsAsync"/>.
		/// </summary>
#endif
		ExecuteReader,
#if NET40
/// <summary>
/// <see cref="DbConnection.Open"/> operation.
/// See also <seealso cref="TraceInfo.IsAsync"/>.
/// </summary>
#else
		/// <summary>
		/// <see cref="DbCommand.ExecuteScalar"/> or <see cref="DbCommand.ExecuteScalarAsync(System.Threading.CancellationToken)"/> operation.
		/// See also <seealso cref="TraceInfo.IsAsync"/>.
		/// </summary>
#endif
		ExecuteScalar,
#if NET40
/// <summary>
/// <see cref="DbConnection.Open"/> operation.
/// See also <seealso cref="TraceInfo.IsAsync"/>.
/// </summary>
#else
		/// <summary>
		/// <see cref="DataConnectionExtensions.BulkCopy{T}(ITable{T}, System.Collections.Generic.IEnumerable{T})"/> or <see cref="DataConnectionExtensions.BulkCopyAsync{T}(DataConnection, int, System.Collections.Generic.IEnumerable{T}, System.Threading.CancellationToken)"/> operation.
		/// See also <seealso cref="TraceInfo.IsAsync"/>.
		/// </summary>
#endif
		BulkCopy,
#if NET40
		/// <summary>
		/// <see cref="DbConnection.Open"/> operation.
		/// See also <seealso cref="TraceInfo.IsAsync"/>.
		/// </summary>
#else
		/// <summary>
		/// <see cref="DbConnection.Open"/> or <see cref="DbConnection.OpenAsync(System.Threading.CancellationToken)"/> operation.
		/// See also <seealso cref="TraceInfo.IsAsync"/>.
		/// </summary>
#endif
		Open,

		/// <summary>
		/// Mapper build operation.
		/// See also <seealso cref="TraceInfo.IsAsync"/>.
		/// </summary>
		BuildMapping,

		/// <summary>
		/// Query runner disposal operation.
		/// See also <seealso cref="TraceInfo.IsAsync"/>.
		/// </summary>
		DisposeQuery,

#if NET40
		/// <summary>
		/// <see cref="DataConnection.BeginTransaction()"/> or <see cref="DataConnection.BeginTransaction(System.Data.IsolationLevel)"/> operation.
		/// See also <seealso cref="TraceInfo.IsAsync"/>.
		/// </summary>
#else
		/// <summary>
		/// <see cref="DataConnection.BeginTransaction()"/> or <see cref="DataConnection.BeginTransaction(System.Data.IsolationLevel)"/> or
		/// <see cref="DataConnection.BeginTransactionAsync(System.Threading.CancellationToken)"/> or <see cref="DataConnection.BeginTransactionAsync(System.Data.IsolationLevel, System.Threading.CancellationToken)"/>operation.
		/// See also <seealso cref="TraceInfo.IsAsync"/>.
		/// </summary>
#endif
		BeginTransaction,

#if NET40
		/// <summary>
		/// <see cref="DataConnection.CommitTransaction"/> operation.
		/// See also <seealso cref="TraceInfo.IsAsync"/>.
		/// </summary>
#else
		/// <summary>
		/// <see cref="DataConnection.CommitTransaction"/> or <see cref="DataConnection.CommitTransactionAsync(System.Threading.CancellationToken)"/> operation.
		/// See also <seealso cref="TraceInfo.IsAsync"/>.
		/// </summary>
#endif
		CommitTransaction,
#if NET40
		/// <summary>                    
		/// <see cref="DataConnection.RollbackTransaction"/> operation.
		/// See also <seealso cref="TraceInfo.IsAsync"/>.
		/// </summary>
#else
		/// <summary>
		/// <see cref="DataConnection.RollbackTransaction"/> or <see cref="DataConnection.RollbackTransactionAsync(System.Threading.CancellationToken)"/> operation.
		/// See also <seealso cref="TraceInfo.IsAsync"/>.
		/// </summary>
#endif
		RollbackTransaction
	}
}
