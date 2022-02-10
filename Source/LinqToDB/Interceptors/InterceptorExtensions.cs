﻿using System;
using System.Collections.Generic;
using System.Data.Common;

namespace LinqToDB
{
	using Data;
	using Interceptors;

	/// <summary>
	/// Contains extensions that add one-time interceptors to connection.
	/// </summary>
	public static class InterceptorExtensions
	{
		#region ICommandInterceptor

		// CommandInitialized

		/// <summary>
		/// Adds <see cref="ICommandInterceptor.CommandInitialized(CommandEventData, DbCommand)"/> interceptor, fired on next command only.
		/// </summary>
		/// <param name="dataConnection">Data connection to apply interceptor to.</param>
		/// <param name="onCommandInitialized">Interceptor delegate.</param>
		public static void OnNextCommandInitialized(this DataConnection dataConnection, Func<CommandEventData, DbCommand, DbCommand> onCommandInitialized)
		{
			dataConnection.AddInterceptor(new OneTimeCommandInterceptor(onCommandInitialized));
		}

		/// <summary>
		/// Adds <see cref="ICommandInterceptor.CommandInitialized(CommandEventData, DbCommand)"/> interceptor, fired on next command only.
		/// </summary>
		/// <param name="dataContext">Data context to apply interceptor to.</param>
		/// <param name="onCommandInitialized">Interceptor delegate.</param>
		public static void OnNextCommandInitialized(this DataContext dataContext, Func<CommandEventData, DbCommand, DbCommand> onCommandInitialized)
		{
			dataContext.AddInterceptor(new OneTimeCommandInterceptor(onCommandInitialized));
		}

		#endregion

		internal static void AddInterceptorImpl(this IInterceptable interceptable, IInterceptor interceptor)
		{
			switch (interceptor)
			{
				case ICommandInterceptor       cm : AddInterceptorImpl((IInterceptable<ICommandInterceptor>)      interceptable, cm); break;
				case IConnectionInterceptor    cn : AddInterceptorImpl((IInterceptable<IConnectionInterceptor>)   interceptable, cn); break;
				case IDataContextInterceptor   dc : AddInterceptorImpl((IInterceptable<IDataContextInterceptor>)  interceptable, dc); break;
				case IEntityServiceInterceptor es : AddInterceptorImpl((IInterceptable<IEntityServiceInterceptor>)interceptable, es); break;
			}
		}

		internal static void AddInterceptorImpl<T>(this IInterceptable<T> interceptable, T interceptor)
			where T : IInterceptor
		{
			if (interceptable.Interceptor == null)
				interceptable.Interceptor = interceptor;
			else if (interceptable.Interceptor is AggregatedInterceptor<T> aggregated)
				aggregated.Interceptors.Add(interceptor);
			else if (interceptable is IInterceptable<ICommandInterceptor> cmi && interceptor is ICommandInterceptor cm)
				cmi.Interceptor = new AggregatedCommandInterceptor
				{
					Interceptors = { cmi.Interceptor!, cm }
				};
			else if (interceptable is IInterceptable<IConnectionInterceptor> ci && interceptor is IConnectionInterceptor c)
				ci.Interceptor = new AggregatedConnectionInterceptor
				{
					Interceptors = { ci.Interceptor!, c }
				};
			else if (interceptable is IInterceptable<IDataContextInterceptor> dci && interceptor is IDataContextInterceptor dc)
				dci.Interceptor = new AggregatedDataContextInterceptor
				{
					Interceptors = { dci.Interceptor!, dc }
				};
			else if (interceptable is IInterceptable<IEntityServiceInterceptor> esi && interceptor is IEntityServiceInterceptor es)
				esi.Interceptor = new AggregatedEntityServiceInterceptor
				{
					Interceptors = { esi.Interceptor!, es }
				};
			else
				throw new NotImplementedException($"AddInterceptor for '{typeof(T).Name}' is not implemented.");
		}

		internal static void RemoveInterceptor<T>(this IInterceptable<T> interceptable, IInterceptor interceptor)
			where T : IInterceptor
		{
			if (interceptor is T i)
			{
				switch (interceptable.Interceptor)
				{
					case null :
						break;
					case AggregatedInterceptor<T> ae :
						ae.Remove(i);
						break;
					case IInterceptor e when e == interceptor :
						interceptable.Interceptor = default;
						break;
				}
			}
		}

		internal static IEnumerable<T> GetInterceptors<T>(this IInterceptable<T> interceptable)
			where T : IInterceptor
		{
			if (interceptable.Interceptor == null)
				yield break;

			if (interceptable.Interceptor is AggregatedInterceptor<T> ai)
				foreach (var interceptor in ai.Interceptors)
					yield return interceptor;
			else
				yield return interceptable.Interceptor;
		}


		internal static TA Clone<TA,TI>(this AggregatedInterceptor<TI> aggregatedInterceptor)
			where TI : IInterceptor
			where TA : AggregatedInterceptor<TI>, new()
		{
			var clone = new TA();
			clone.Interceptors.AddRange(aggregatedInterceptor.Interceptors);
			return clone;
		}
	}
}
