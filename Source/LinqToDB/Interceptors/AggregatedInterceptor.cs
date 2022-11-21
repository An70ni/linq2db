using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LinqToDB.Interceptors
{
	abstract class AggregatedInterceptor<TInterceptor>: IInterceptor
		where TInterceptor : IInterceptor
	{
		public List<TInterceptor> Interceptors { get; } = new ();

		// as we support interceptor removal we should delay removal when interceptors collection enumerated to
		// avoid errors
		bool _enumerating;
		readonly List<TInterceptor> _removeList = new ();

		public void Add(TInterceptor interceptor)
		{
			Interceptors.Add(interceptor);
		}

		public void Remove(TInterceptor interceptor)
		{
			if (!_enumerating)
				Interceptors.Remove(interceptor);
			else
				_removeList.Add(interceptor);
		}

#if !THE_RAOT_CORE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#else
		[MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
#endif
		protected void RemoveDelayed()
		{
			foreach (var interceptor in _removeList)
				Interceptors.Remove(interceptor);
			_removeList.Clear();
		}

#if !THE_RAOT_CORE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#else
		[MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
#endif
		protected void Apply(Action func)
		{
			_enumerating = true;

			try
			{
				func();
			}
			finally
			{
				_enumerating = false;
				RemoveDelayed();
			}
		}

#if !THE_RAOT_CORE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#else
		[MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
#endif
		protected T Apply<T>(Func<T> func)
		{
			_enumerating = true;

			try
			{
				return func();
			}
			finally
			{
				_enumerating = false;
				RemoveDelayed();
			}
		}

#if !THE_RAOT_CORE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#else
		[MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
#endif
		protected async Task Apply(Func<Task> func)
		{
			_enumerating = true;

			try
			{
				await func().ConfigureAwait(Common.Configuration.ContinueOnCapturedContext);
			}
			finally
			{
				_enumerating = false;
				RemoveDelayed();
			}
		}

#if !THE_RAOT_CORE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#else
		[MethodImpl(MethodImplOptionsEx.AggressiveInlining)]
#endif
		protected async Task<T> Apply<T>(Func<Task<T>> func)
		{
			_enumerating = true;

			try
			{
				return await func().ConfigureAwait(Common.Configuration.ContinueOnCapturedContext);
			}
			finally
			{
				_enumerating = false;
				RemoveDelayed();
			}
		}

		protected abstract AggregatedInterceptor<TInterceptor> Create();

		public AggregatedInterceptor<TInterceptor> Clone()
		{
			var clone = Create();
			clone.Interceptors.AddRange(Interceptors);
			return clone;
		}
	}
}
