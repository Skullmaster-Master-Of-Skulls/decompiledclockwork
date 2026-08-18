using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000189 RID: 393
	internal class InternalDispatcher<TInterceptor> where TInterceptor : class, IDbInterceptor
	{
		// Token: 0x06000D71 RID: 3441 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void Add(IDbInterceptor interceptor)
		{
			TInterceptor tinterceptor = interceptor as TInterceptor;
			if (tinterceptor == null)
			{
				return;
			}
			lock (this._lock)
			{
				List<TInterceptor> list = this._interceptors.ToList<TInterceptor>();
				list.Add(tinterceptor);
				this._interceptors = list;
			}
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0003C45C File Offset: 0x0003A65C
		public void Remove(IDbInterceptor interceptor)
		{
			TInterceptor tinterceptor = interceptor as TInterceptor;
			if (tinterceptor == null)
			{
				return;
			}
			lock (this._lock)
			{
				List<TInterceptor> list = this._interceptors.ToList<TInterceptor>();
				list.Remove(tinterceptor);
				this._interceptors = list;
			}
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0003C4CC File Offset: 0x0003A6CC
		public TResult Dispatch<TResult>(TResult result, Func<TResult, TInterceptor, TResult> accumulator)
		{
			if (this._interceptors.Count != 0)
			{
				return this._interceptors.Aggregate(result, accumulator);
			}
			return result;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0003C4EE File Offset: 0x0003A6EE
		public void Dispatch(Action<TInterceptor> action)
		{
			if (this._interceptors.Count != 0)
			{
				this._interceptors.Each(action);
			}
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0003C510 File Offset: 0x0003A710
		public TResult Dispatch<TInterceptionContext, TResult>(TResult result, TInterceptionContext interceptionContext, Action<TInterceptor, TInterceptionContext> intercept) where TInterceptionContext : DbInterceptionContext, IDbMutableInterceptionContext<TResult>
		{
			if (this._interceptors.Count == 0)
			{
				return result;
			}
			interceptionContext.MutableData.SetExecuted(result);
			foreach (TInterceptor arg in this._interceptors)
			{
				intercept(arg, interceptionContext);
			}
			if (interceptionContext.MutableData.Exception != null)
			{
				throw interceptionContext.MutableData.Exception;
			}
			return interceptionContext.MutableData.Result;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0003C5C4 File Offset: 0x0003A7C4
		public void Dispatch<TTarget, TInterceptionContext>(TTarget target, Action<TTarget, TInterceptionContext> operation, TInterceptionContext interceptionContext, Action<TInterceptor, TTarget, TInterceptionContext> executing, Action<TInterceptor, TTarget, TInterceptionContext> executed) where TInterceptionContext : DbInterceptionContext, IDbMutableInterceptionContext
		{
			if (this._interceptors.Count == 0)
			{
				operation(target, interceptionContext);
				return;
			}
			foreach (TInterceptor arg in this._interceptors)
			{
				executing(arg, target, interceptionContext);
			}
			if (!interceptionContext.MutableData.IsExecutionSuppressed)
			{
				try
				{
					operation(target, interceptionContext);
					interceptionContext.MutableData.HasExecuted = true;
				}
				catch (Exception ex)
				{
					interceptionContext.MutableData.SetExceptionThrown(ex);
					foreach (TInterceptor arg2 in this._interceptors)
					{
						executed(arg2, target, interceptionContext);
					}
					if (object.ReferenceEquals(interceptionContext.MutableData.Exception, ex))
					{
						throw;
					}
				}
			}
			if (interceptionContext.MutableData.OriginalException == null)
			{
				foreach (TInterceptor arg3 in this._interceptors)
				{
					executed(arg3, target, interceptionContext);
				}
			}
			if (interceptionContext.MutableData.Exception != null)
			{
				throw interceptionContext.MutableData.Exception;
			}
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0003C774 File Offset: 0x0003A974
		public TResult Dispatch<TTarget, TInterceptionContext, TResult>(TTarget target, Func<TTarget, TInterceptionContext, TResult> operation, TInterceptionContext interceptionContext, Action<TInterceptor, TTarget, TInterceptionContext> executing, Action<TInterceptor, TTarget, TInterceptionContext> executed) where TInterceptionContext : DbInterceptionContext, IDbMutableInterceptionContext<TResult>
		{
			if (this._interceptors.Count == 0)
			{
				return operation(target, interceptionContext);
			}
			foreach (TInterceptor arg in this._interceptors)
			{
				executing(arg, target, interceptionContext);
			}
			if (!interceptionContext.MutableData.IsExecutionSuppressed)
			{
				try
				{
					interceptionContext.MutableData.SetExecuted(operation(target, interceptionContext));
				}
				catch (Exception ex)
				{
					interceptionContext.MutableData.SetExceptionThrown(ex);
					foreach (TInterceptor arg2 in this._interceptors)
					{
						executed(arg2, target, interceptionContext);
					}
					if (object.ReferenceEquals(interceptionContext.MutableData.Exception, ex))
					{
						throw;
					}
				}
			}
			if (interceptionContext.MutableData.OriginalException == null)
			{
				foreach (TInterceptor arg3 in this._interceptors)
				{
					executed(arg3, target, interceptionContext);
				}
			}
			if (interceptionContext.MutableData.Exception != null)
			{
				throw interceptionContext.MutableData.Exception;
			}
			return interceptionContext.MutableData.Result;
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0003CAA0 File Offset: 0x0003ACA0
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public Task DispatchAsync<TTarget, TInterceptionContext>(TTarget target, Func<TTarget, TInterceptionContext, CancellationToken, Task> operation, TInterceptionContext interceptionContext, Action<TInterceptor, TTarget, TInterceptionContext> executing, Action<TInterceptor, TTarget, TInterceptionContext> executed, CancellationToken cancellationToken) where TInterceptionContext : DbInterceptionContext, IDbMutableInterceptionContext
		{
			if (this._interceptors.Count == 0)
			{
				return operation(target, interceptionContext, cancellationToken);
			}
			foreach (TInterceptor arg in this._interceptors)
			{
				executing(arg, target, interceptionContext);
			}
			Task task = interceptionContext.MutableData.IsExecutionSuppressed ? Task.FromResult<object>(null) : operation(target, interceptionContext, cancellationToken);
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			task.ContinueWith(delegate(Task t)
			{
				interceptionContext.MutableData.TaskStatus = t.Status;
				if (t.IsFaulted)
				{
					interceptionContext.MutableData.SetExceptionThrown(t.Exception.InnerException);
				}
				else if (!interceptionContext.MutableData.IsExecutionSuppressed)
				{
					interceptionContext.MutableData.HasExecuted = true;
				}
				try
				{
					foreach (TInterceptor arg2 in this._interceptors)
					{
						executed(arg2, target, interceptionContext);
					}
				}
				catch (Exception exception)
				{
					interceptionContext.MutableData.Exception = exception;
				}
				if (interceptionContext.MutableData.Exception != null)
				{
					tcs.SetException(interceptionContext.MutableData.Exception);
					return;
				}
				if (t.IsCanceled)
				{
					tcs.SetCanceled();
					return;
				}
				tcs.SetResult(null);
			}, TaskContinuationOptions.ExecuteSynchronously);
			return tcs.Task;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0003CD4C File Offset: 0x0003AF4C
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public Task<TResult> DispatchAsync<TTarget, TInterceptionContext, TResult>(TTarget target, Func<TTarget, TInterceptionContext, CancellationToken, Task<TResult>> operation, TInterceptionContext interceptionContext, Action<TInterceptor, TTarget, TInterceptionContext> executing, Action<TInterceptor, TTarget, TInterceptionContext> executed, CancellationToken cancellationToken) where TInterceptionContext : DbInterceptionContext, IDbMutableInterceptionContext<TResult>
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (this._interceptors.Count == 0)
			{
				return operation(target, interceptionContext, cancellationToken);
			}
			foreach (TInterceptor arg in this._interceptors)
			{
				executing(arg, target, interceptionContext);
			}
			Task<TResult> task = interceptionContext.MutableData.IsExecutionSuppressed ? Task.FromResult<TResult>(interceptionContext.MutableData.Result) : operation(target, interceptionContext, cancellationToken);
			TaskCompletionSource<TResult> tcs = new TaskCompletionSource<TResult>();
			task.ContinueWith(delegate(Task<TResult> t)
			{
				interceptionContext.MutableData.TaskStatus = t.Status;
				if (t.IsFaulted)
				{
					interceptionContext.MutableData.SetExceptionThrown(t.Exception.InnerException);
				}
				else if (!interceptionContext.MutableData.IsExecutionSuppressed)
				{
					interceptionContext.MutableData.SetExecuted((t.IsCanceled || t.IsFaulted) ? default(TResult) : t.Result);
				}
				try
				{
					foreach (TInterceptor arg2 in this._interceptors)
					{
						executed(arg2, target, interceptionContext);
					}
				}
				catch (Exception exception)
				{
					interceptionContext.MutableData.Exception = exception;
				}
				if (interceptionContext.MutableData.Exception != null)
				{
					tcs.SetException(interceptionContext.MutableData.Exception);
					return;
				}
				if (t.IsCanceled)
				{
					tcs.SetCanceled();
					return;
				}
				tcs.SetResult(interceptionContext.MutableData.Result);
			}, TaskContinuationOptions.ExecuteSynchronously);
			return tcs.Task;
		}

		// Token: 0x040003A9 RID: 937
		private volatile List<TInterceptor> _interceptors = new List<TInterceptor>();

		// Token: 0x040003AA RID: 938
		private readonly object _lock = new object();
	}
}
