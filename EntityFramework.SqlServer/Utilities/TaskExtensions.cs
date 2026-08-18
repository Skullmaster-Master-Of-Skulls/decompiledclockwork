using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x02000002 RID: 2
	public static class TaskExtensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static TaskExtensions.CultureAwaiter<T> WithCurrentCulture<T>(this Task<T> task)
		{
			return new TaskExtensions.CultureAwaiter<T>(task);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public static TaskExtensions.CultureAwaiter WithCurrentCulture(this Task task)
		{
			return new TaskExtensions.CultureAwaiter(task);
		}

		// Token: 0x02000003 RID: 3
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Awaiter")]
		[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
		public struct CultureAwaiter<T> : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000003 RID: 3 RVA: 0x000020E0 File Offset: 0x000002E0
			public CultureAwaiter(Task<T> task)
			{
				this._task = task;
			}

			// Token: 0x06000004 RID: 4 RVA: 0x000020E9 File Offset: 0x000002E9
			[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Awaiter")]
			[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
			public TaskExtensions.CultureAwaiter<T> GetAwaiter()
			{
				return this;
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000005 RID: 5 RVA: 0x000020F1 File Offset: 0x000002F1
			public bool IsCompleted
			{
				get
				{
					return this._task.IsCompleted;
				}
			}

			// Token: 0x06000006 RID: 6 RVA: 0x00002100 File Offset: 0x00000300
			[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
			public T GetResult()
			{
				return this._task.GetAwaiter().GetResult();
			}

			// Token: 0x06000007 RID: 7 RVA: 0x00002120 File Offset: 0x00000320
			public void OnCompleted(Action continuation)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000008 RID: 8 RVA: 0x000021A8 File Offset: 0x000003A8
			public void UnsafeOnCompleted(Action continuation)
			{
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
				this._task.ConfigureAwait(false).GetAwaiter().UnsafeOnCompleted(delegate
				{
					CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
					CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
					Thread.CurrentThread.CurrentCulture = currentCulture;
					Thread.CurrentThread.CurrentUICulture = currentUICulture;
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

		// Token: 0x02000004 RID: 4
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Awaiter")]
		[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public struct CultureAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000009 RID: 9 RVA: 0x0000220A File Offset: 0x0000040A
			public CultureAwaiter(Task task)
			{
				this._task = task;
			}

			// Token: 0x0600000A RID: 10 RVA: 0x00002213 File Offset: 0x00000413
			[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Awaiter")]
			[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
			public TaskExtensions.CultureAwaiter GetAwaiter()
			{
				return this;
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600000B RID: 11 RVA: 0x0000221B File Offset: 0x0000041B
			public bool IsCompleted
			{
				get
				{
					return this._task.IsCompleted;
				}
			}

			// Token: 0x0600000C RID: 12 RVA: 0x00002228 File Offset: 0x00000428
			public void GetResult()
			{
				this._task.GetAwaiter().GetResult();
			}

			// Token: 0x0600000D RID: 13 RVA: 0x00002248 File Offset: 0x00000448
			public void OnCompleted(Action continuation)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600000E RID: 14 RVA: 0x000022D0 File Offset: 0x000004D0
			public void UnsafeOnCompleted(Action continuation)
			{
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
				this._task.ConfigureAwait(false).GetAwaiter().UnsafeOnCompleted(delegate
				{
					CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
					CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
					Thread.CurrentThread.CurrentCulture = currentCulture;
					Thread.CurrentThread.CurrentUICulture = currentUICulture;
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
