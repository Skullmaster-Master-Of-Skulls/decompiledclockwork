using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NLog.Internal;

namespace NLog.Common
{
	// Token: 0x02000023 RID: 35
	public static class AsyncHelpers
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002440 File Offset: 0x00000640
		public static void ForEachItemSequentially<T>(IEnumerable<T> items, AsyncContinuation asyncContinuation, AsynchronousAction<T> action)
		{
			action = AsyncHelpers.ExceptionGuard<T>(action);
			AsyncContinuation invokeNext = null;
			IEnumerator<T> enumerator = items.GetEnumerator();
			invokeNext = delegate(Exception ex)
			{
				if (ex != null)
				{
					asyncContinuation(ex);
					return;
				}
				if (!enumerator.MoveNext())
				{
					asyncContinuation(null);
					return;
				}
				action(enumerator.Current, AsyncHelpers.PreventMultipleCalls(invokeNext));
			};
			invokeNext(null);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002500 File Offset: 0x00000700
		public static void Repeat(int repeatCount, AsyncContinuation asyncContinuation, AsynchronousAction action)
		{
			action = AsyncHelpers.ExceptionGuard(action);
			AsyncContinuation invokeNext = null;
			int remaining = repeatCount;
			invokeNext = delegate(Exception ex)
			{
				if (ex != null)
				{
					asyncContinuation(ex);
					return;
				}
				if (remaining-- <= 0)
				{
					asyncContinuation(null);
					return;
				}
				action(AsyncHelpers.PreventMultipleCalls(invokeNext));
			};
			invokeNext(null);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002590 File Offset: 0x00000790
		public static AsyncContinuation PrecededBy(AsyncContinuation asyncContinuation, AsynchronousAction action)
		{
			action = AsyncHelpers.ExceptionGuard(action);
			return delegate(Exception ex)
			{
				if (ex != null)
				{
					asyncContinuation(ex);
					return;
				}
				action(AsyncHelpers.PreventMultipleCalls(asyncContinuation));
			};
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000025D0 File Offset: 0x000007D0
		public static AsyncContinuation WithTimeout(AsyncContinuation asyncContinuation, TimeSpan timeout)
		{
			return new AsyncContinuation(new TimeoutContinuation(asyncContinuation, timeout).Function);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000026C0 File Offset: 0x000008C0
		public static void ForEachItemInParallel<T>(IEnumerable<T> values, AsyncContinuation asyncContinuation, AsynchronousAction<T> action)
		{
			action = AsyncHelpers.ExceptionGuard<T>(action);
			List<T> list = new List<T>(values);
			int remaining = list.Count;
			List<Exception> exceptions = new List<Exception>();
			InternalLogger.Trace("ForEachItemInParallel() {0} items", new object[]
			{
				list.Count
			});
			if (remaining == 0)
			{
				asyncContinuation(null);
				return;
			}
			AsyncContinuation continuation = delegate(Exception ex)
			{
				InternalLogger.Trace("Continuation invoked: {0}", new object[]
				{
					ex
				});
				List<Exception> exceptions;
				if (ex != null)
				{
					lock (exceptions)
					{
						exceptions.Add(ex);
					}
				}
				int num = Interlocked.Decrement(ref remaining);
				InternalLogger.Trace("Parallel task completed. {0} items remaining", new object[]
				{
					num
				});
				if (num == 0)
				{
					asyncContinuation(AsyncHelpers.GetCombinedException(exceptions));
				}
			};
			foreach (T itemCopy2 in list)
			{
				T itemCopy = itemCopy2;
				ThreadPool.QueueUserWorkItem(delegate(object s)
				{
					action(itemCopy, AsyncHelpers.PreventMultipleCalls(continuation));
				});
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000027DC File Offset: 0x000009DC
		public static void RunSynchronously(AsynchronousAction action)
		{
			ManualResetEvent ev = new ManualResetEvent(false);
			Exception lastException = null;
			action(AsyncHelpers.PreventMultipleCalls(delegate(Exception ex)
			{
				lastException = ex;
				ev.Set();
			}));
			ev.WaitOne();
			if (lastException != null)
			{
				throw new NLogRuntimeException("Asynchronous exception has occurred.", lastException);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000283E File Offset: 0x00000A3E
		public static AsyncContinuation PreventMultipleCalls(AsyncContinuation asyncContinuation)
		{
			if (asyncContinuation.Target is SingleCallContinuation)
			{
				return asyncContinuation;
			}
			return new AsyncContinuation(new SingleCallContinuation(asyncContinuation).Function);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002860 File Offset: 0x00000A60
		public static Exception GetCombinedException(IList<Exception> exceptions)
		{
			if (exceptions.Count == 0)
			{
				return null;
			}
			if (exceptions.Count == 1)
			{
				return exceptions[0];
			}
			StringBuilder stringBuilder = new StringBuilder();
			string value = string.Empty;
			string newLine = EnvironmentHelper.NewLine;
			foreach (Exception ex in exceptions)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(ex.ToString());
				stringBuilder.Append(newLine);
				value = newLine;
			}
			return new NLogRuntimeException("Got multiple exceptions:\r\n" + stringBuilder);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000294C File Offset: 0x00000B4C
		private static AsynchronousAction ExceptionGuard(AsynchronousAction action)
		{
			return delegate(AsyncContinuation cont)
			{
				try
				{
					action(cont);
				}
				catch (Exception exception)
				{
					if (exception.MustBeRethrown())
					{
						throw;
					}
					cont(exception);
				}
			};
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000029BC File Offset: 0x00000BBC
		private static AsynchronousAction<T> ExceptionGuard<T>(AsynchronousAction<T> action)
		{
			return delegate(T argument, AsyncContinuation cont)
			{
				try
				{
					action(argument, cont);
				}
				catch (Exception exception)
				{
					if (exception.MustBeRethrown())
					{
						throw;
					}
					cont(exception);
				}
			};
		}
	}
}
