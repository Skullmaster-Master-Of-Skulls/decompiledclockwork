using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000003 RID: 3
	internal static class TaskExtensions
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002131 File Offset: 0x00000331
		public static TaskExtensions.CultureAwaiter<T> WithCurrentCulture<T>(this Task<T> task)
		{
			return new TaskExtensions.CultureAwaiter<T>(task);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002139 File Offset: 0x00000339
		public static TaskExtensions.CultureAwaiter WithCurrentCulture(this Task task)
		{
			return new TaskExtensions.CultureAwaiter(task);
		}

		// Token: 0x02000004 RID: 4
		public struct CultureAwaiter<T> : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000004 RID: 4 RVA: 0x00002141 File Offset: 0x00000341
			public CultureAwaiter(Task<T> task)
			{
				this._task = task;
			}

			// Token: 0x06000005 RID: 5 RVA: 0x0000214A File Offset: 0x0000034A
			public TaskExtensions.CultureAwaiter<T> GetAwaiter()
			{
				return this;
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000006 RID: 6 RVA: 0x00002152 File Offset: 0x00000352
			public bool IsCompleted
			{
				get
				{
					return this._task.IsCompleted;
				}
			}

			// Token: 0x06000007 RID: 7 RVA: 0x00002160 File Offset: 0x00000360
			public T GetResult()
			{
				return this._task.GetAwaiter().GetResult();
			}

			// Token: 0x06000008 RID: 8 RVA: 0x00002180 File Offset: 0x00000380
			public void OnCompleted(Action continuation)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000009 RID: 9 RVA: 0x00002208 File Offset: 0x00000408
			public void UnsafeOnCompleted(Action continuation)
			{
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				CultureInfo currentUiCulture = Thread.CurrentThread.CurrentUICulture;
				this._task.ConfigureAwait(false).GetAwaiter().UnsafeOnCompleted(delegate
				{
					CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
					CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
					Thread.CurrentThread.CurrentCulture = currentCulture;
					Thread.CurrentThread.CurrentUICulture = currentUiCulture;
					try
					{
						continuation();
					}
					finally
					{
						Thread.CurrentThread.CurrentCulture = currentCulture;
						Thread.CurrentThread.CurrentUICulture = currentUICulture;
					}
				});
			}

			// Token: 0x04000001 RID: 1
			private readonly Task<T> _task;
		}

		// Token: 0x02000005 RID: 5
		public struct CultureAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x0600000A RID: 10 RVA: 0x0000226A File Offset: 0x0000046A
			public CultureAwaiter(Task task)
			{
				this._task = task;
			}

			// Token: 0x0600000B RID: 11 RVA: 0x00002273 File Offset: 0x00000473
			public TaskExtensions.CultureAwaiter GetAwaiter()
			{
				return this;
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600000C RID: 12 RVA: 0x0000227B File Offset: 0x0000047B
			public bool IsCompleted
			{
				get
				{
					return this._task.IsCompleted;
				}
			}

			// Token: 0x0600000D RID: 13 RVA: 0x00002288 File Offset: 0x00000488
			public void GetResult()
			{
				this._task.GetAwaiter().GetResult();
			}

			// Token: 0x0600000E RID: 14 RVA: 0x000022A8 File Offset: 0x000004A8
			public void OnCompleted(Action continuation)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600000F RID: 15 RVA: 0x00002330 File Offset: 0x00000530
			public void UnsafeOnCompleted(Action continuation)
			{
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				CultureInfo currentUiCulture = Thread.CurrentThread.CurrentUICulture;
				this._task.ConfigureAwait(false).GetAwaiter().UnsafeOnCompleted(delegate
				{
					CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
					CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
					Thread.CurrentThread.CurrentCulture = currentCulture;
					Thread.CurrentThread.CurrentUICulture = currentUiCulture;
					try
					{
						continuation();
					}
					finally
					{
						Thread.CurrentThread.CurrentCulture = currentCulture;
						Thread.CurrentThread.CurrentUICulture = currentUICulture;
					}
				});
			}

			// Token: 0x04000002 RID: 2
			private readonly Task _task;
		}
	}
}
