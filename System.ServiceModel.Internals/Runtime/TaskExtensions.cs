using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Runtime
{
	// Token: 0x0200002B RID: 43
	internal static class TaskExtensions
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00006250 File Offset: 0x00004450
		public static IAsyncResult AsAsyncResult<T>(this Task<T> task, AsyncCallback callback, object state)
		{
			if (task == null)
			{
				throw Fx.Exception.ArgumentNull("task");
			}
			if (task.Status == TaskStatus.Created)
			{
				throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.SFxTaskNotStarted));
			}
			TaskCompletionSource<T> tcs = new TaskCompletionSource<T>(state);
			task.ContinueWith(delegate(Task<T> t)
			{
				if (t.IsFaulted)
				{
					tcs.TrySetException(t.Exception.InnerExceptions);
				}
				else if (t.IsCanceled)
				{
					tcs.TrySetCanceled();
				}
				else
				{
					tcs.TrySetResult(t.Result);
				}
				if (callback != null)
				{
					callback(tcs.Task);
				}
			}, TaskContinuationOptions.ExecuteSynchronously);
			return tcs.Task;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000062CC File Offset: 0x000044CC
		public static IAsyncResult AsAsyncResult(this Task task, AsyncCallback callback, object state)
		{
			if (task == null)
			{
				throw Fx.Exception.ArgumentNull("task");
			}
			if (task.Status == TaskStatus.Created)
			{
				throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.SFxTaskNotStarted));
			}
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>(state);
			task.ContinueWith(delegate(Task t)
			{
				if (t.IsFaulted)
				{
					tcs.TrySetException(t.Exception.InnerExceptions);
				}
				else if (t.IsCanceled)
				{
					tcs.TrySetCanceled();
				}
				else
				{
					tcs.TrySetResult(null);
				}
				if (callback != null)
				{
					callback(tcs.Task);
				}
			}, TaskContinuationOptions.ExecuteSynchronously);
			return tcs.Task;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006345 File Offset: 0x00004545
		public static ConfiguredTaskAwaitable SuppressContextFlow(this Task task)
		{
			return task.ConfigureAwait(false);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000634E File Offset: 0x0000454E
		public static ConfiguredTaskAwaitable<T> SuppressContextFlow<T>(this Task<T> task)
		{
			return task.ConfigureAwait(false);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006357 File Offset: 0x00004557
		public static ConfiguredTaskAwaitable ContinueOnCapturedContextFlow(this Task task)
		{
			return task.ConfigureAwait(true);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006360 File Offset: 0x00004560
		public static ConfiguredTaskAwaitable<T> ContinueOnCapturedContextFlow<T>(this Task<T> task)
		{
			return task.ConfigureAwait(true);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000636C File Offset: 0x0000456C
		public static void Wait<TException>(this Task task)
		{
			try
			{
				task.Wait();
			}
			catch (AggregateException aggregateException)
			{
				throw Fx.Exception.AsError<TException>(aggregateException);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000063A0 File Offset: 0x000045A0
		public static bool Wait<TException>(this Task task, int millisecondsTimeout)
		{
			bool result;
			try
			{
				result = task.Wait(millisecondsTimeout);
			}
			catch (AggregateException aggregateException)
			{
				throw Fx.Exception.AsError<TException>(aggregateException);
			}
			return result;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000063D8 File Offset: 0x000045D8
		public static bool Wait<TException>(this Task task, TimeSpan timeout)
		{
			bool result;
			try
			{
				if (timeout == TimeSpan.MaxValue)
				{
					result = task.Wait(-1);
				}
				else
				{
					result = task.Wait(timeout);
				}
			}
			catch (AggregateException aggregateException)
			{
				throw Fx.Exception.AsError<TException>(aggregateException);
			}
			return result;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006424 File Offset: 0x00004624
		public static void Wait(this Task task, TimeSpan timeout, Action<Exception, TimeSpan, string> exceptionConverter, string operationType)
		{
			bool flag = false;
			try
			{
				if (timeout > TimeoutHelper.MaxWait)
				{
					task.Wait();
				}
				else
				{
					flag = !task.Wait(timeout);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex) || exceptionConverter == null)
				{
					throw;
				}
				exceptionConverter(ex, timeout, operationType);
			}
			if (flag)
			{
				throw Fx.Exception.AsError(new TimeoutException(InternalSR.TaskTimedOutError(timeout)));
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000649C File Offset: 0x0000469C
		public static Task<TBase> Upcast<TDerived, TBase>(this Task<TDerived> task) where TDerived : TBase
		{
			if (task.Status != TaskStatus.RanToCompletion)
			{
				return task.UpcastPrivate<TDerived, TBase>();
			}
			return Task.FromResult<TBase>((TBase)((object)task.Result));
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000064C4 File Offset: 0x000046C4
		private static Task<TBase> UpcastPrivate<TDerived, TBase>(this Task<TDerived> task) where TDerived : TBase
		{
			TaskExtensions.<UpcastPrivate>d__11<TDerived, TBase> <UpcastPrivate>d__;
			<UpcastPrivate>d__.<>t__builder = AsyncTaskMethodBuilder<TBase>.Create();
			<UpcastPrivate>d__.task = task;
			<UpcastPrivate>d__.<>1__state = -1;
			<UpcastPrivate>d__.<>t__builder.Start<TaskExtensions.<UpcastPrivate>d__11<TDerived, TBase>>(ref <UpcastPrivate>d__);
			return <UpcastPrivate>d__.<>t__builder.Task;
		}
	}
}
