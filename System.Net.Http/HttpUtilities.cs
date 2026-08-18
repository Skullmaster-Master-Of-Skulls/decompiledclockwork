using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000013 RID: 19
	internal static class HttpUtilities
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00005278 File Offset: 0x00003478
		internal static bool IsHttpUri(Uri uri)
		{
			string scheme = uri.Scheme;
			return string.Compare("http", scheme, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare("https", scheme, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000052AB File Offset: 0x000034AB
		internal static bool HandleFaultsAndCancelation<T>(Task task, TaskCompletionSource<T> tcs)
		{
			if (task.IsFaulted)
			{
				tcs.TrySetException(task.Exception.GetBaseException());
				return true;
			}
			if (task.IsCanceled)
			{
				tcs.TrySetCanceled();
				return true;
			}
			return false;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000052DB File Offset: 0x000034DB
		internal static Task ContinueWithStandard(this Task task, Action<Task> continuation)
		{
			return task.ContinueWith(continuation, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000052F3 File Offset: 0x000034F3
		internal static Task ContinueWithStandard<T>(this Task<T> task, Action<Task<T>> continuation)
		{
			return task.ContinueWith(continuation, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		// Token: 0x0400009A RID: 154
		internal static readonly Version DefaultVersion = HttpVersion.Version11;

		// Token: 0x0400009B RID: 155
		internal static readonly byte[] EmptyByteArray = new byte[0];
	}
}
