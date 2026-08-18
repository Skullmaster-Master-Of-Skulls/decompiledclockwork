using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Utilities
{
	// Token: 0x020006EB RID: 1771
	public static class TaskExtensions
	{
		// Token: 0x06004719 RID: 18201 RVA: 0x00150A6C File Offset: 0x0014EC6C
		public static TaskExtensions.CultureAwaiter<T> WithCurrentCulture<T>(this Task<T> task)
		{
			return new TaskExtensions.CultureAwaiter<T>(task);
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x00150A74 File Offset: 0x0014EC74
		public static TaskExtensions.CultureAwaiter WithCurrentCulture(this Task task)
		{
			return new TaskExtensions.CultureAwaiter(task);
		}

		// Token: 0x020006EC RID: 1772
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Awaiter")]
		[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
		public struct CultureAwaiter<T> : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x0600471B RID: 18203 RVA: 0x00150A7C File Offset: 0x0014EC7C
			public CultureAwaiter(Task<T> task)
			{
				this._task = task;
			}

			// Token: 0x0600471C RID: 18204 RVA: 0x00150A85 File Offset: 0x0014EC85
			[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
			[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Awaiter")]
			public TaskExtensions.CultureAwaiter<T> GetAwaiter()
			{
				return this;
			}

			// Token: 0x17000A99 RID: 2713
			// (get) Token: 0x0600471D RID: 18205 RVA: 0x00150A8D File Offset: 0x0014EC8D
			public bool IsCompleted
			{
				get
				{
					return this._task.IsCompleted;
				}
			}

			// Token: 0x0600471E RID: 18206 RVA: 0x00150A9C File Offset: 0x0014EC9C
			[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
			public T GetResult()
			{
				return this._task.GetAwaiter().GetResult();
			}

			// Token: 0x0600471F RID: 18207 RVA: 0x00150ABC File Offset: 0x0014ECBC
			public void OnCompleted(Action continuation)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06004720 RID: 18208 RVA: 0x00150B44 File Offset: 0x0014ED44
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

			// Token: 0x04001A18 RID: 6680
			private readonly Task<T> _task;
		}

		// Token: 0x020006ED RID: 1773
		[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Awaiter")]
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public struct CultureAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06004721 RID: 18209 RVA: 0x00150BA6 File Offset: 0x0014EDA6
			public CultureAwaiter(Task task)
			{
				this._task = task;
			}

			// Token: 0x06004722 RID: 18210 RVA: 0x00150BAF File Offset: 0x0014EDAF
			[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Awaiter")]
			[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
			public TaskExtensions.CultureAwaiter GetAwaiter()
			{
				return this;
			}

			// Token: 0x17000A9A RID: 2714
			// (get) Token: 0x06004723 RID: 18211 RVA: 0x00150BB7 File Offset: 0x0014EDB7
			public bool IsCompleted
			{
				get
				{
					return this._task.IsCompleted;
				}
			}

			// Token: 0x06004724 RID: 18212 RVA: 0x00150BC4 File Offset: 0x0014EDC4
			public void GetResult()
			{
				this._task.GetAwaiter().GetResult();
			}

			// Token: 0x06004725 RID: 18213 RVA: 0x00150BE4 File Offset: 0x0014EDE4
			public void OnCompleted(Action continuation)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06004726 RID: 18214 RVA: 0x00150C6C File Offset: 0x0014EE6C
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

			// Token: 0x04001A19 RID: 6681
			private readonly Task _task;
		}
	}
}
