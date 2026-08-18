using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace a
{
	// Token: 0x020004A3 RID: 1187
	internal static class q
	{
		// Token: 0x06002878 RID: 10360 RVA: 0x000BCCF4 File Offset: 0x000BBCF4
		public static IAsyncResult a<a>(this Task<a> A_0, AsyncCallback A_1, object A_2)
		{
			return A_0.a(A_1, A_2);
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x000BCCFE File Offset: 0x000BBCFE
		public static IAsyncResult b(this Task A_0, AsyncCallback A_1, object A_2)
		{
			return A_0.a(A_1, A_2);
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x000BCD08 File Offset: 0x000BBD08
		private static IAsyncResult a<a>(this Task A_0, AsyncCallback A_1, object A_2)
		{
			q<a>.a a = new q<a>.a();
			a.a = A_1;
			if (A_2 == null && a.a == null)
			{
				return A_0;
			}
			if (A_0.AsyncState == A_2 && a.a != null)
			{
				return A_0.ContinueWith(new Action<Task>(a.c));
			}
			a.b = new TaskCompletionSource<a>(A_2);
			A_0.ContinueWith(new Action<Task>(a.d), CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.Default);
			return a.b.Task;
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x000BCD88 File Offset: 0x000BBD88
		public static a b<a>(this IAsyncResult A_0)
		{
			return ((Task<a>)A_0).GetAwaiter().GetResult();
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000BCDA8 File Offset: 0x000BBDA8
		public static void a(this IAsyncResult A_0)
		{
			((Task)A_0).GetAwaiter().GetResult();
		}

		// Token: 0x020004A4 RID: 1188
		[CompilerGenerated]
		private sealed class a<a>
		{
			// Token: 0x0600287E RID: 10366 RVA: 0x000BCDD0 File Offset: 0x000BBDD0
			internal void c(Task A_0)
			{
				this.a(A_0);
			}

			// Token: 0x0600287F RID: 10367 RVA: 0x000BCDE0 File Offset: 0x000BBDE0
			internal void d(Task A_0)
			{
				if (A_0.IsFaulted)
				{
					this.b.TrySetException(A_0.Exception.InnerExceptions);
				}
				else if (A_0.IsCanceled)
				{
					this.b.TrySetCanceled();
				}
				else if (A_0 is Task<a>)
				{
					this.b.TrySetResult(((Task<a>)A_0).Result);
				}
				else
				{
					this.b.TrySetResult(default(a));
				}
				if (this.a != null)
				{
					this.a(this.b.Task);
				}
			}

			// Token: 0x04001BB4 RID: 7092
			public AsyncCallback a;

			// Token: 0x04001BB5 RID: 7093
			public TaskCompletionSource<a> b;
		}
	}
}
