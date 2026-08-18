using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000222 RID: 546
	internal class AsyncProtocolRequest
	{
		// Token: 0x0600141A RID: 5146 RVA: 0x0006A9EE File Offset: 0x00068BEE
		public AsyncProtocolRequest(LazyAsyncResult userAsyncResult)
		{
			this.UserAsyncResult = userAsyncResult;
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0006A9FD File Offset: 0x00068BFD
		public void SetNextRequest(byte[] buffer, int offset, int count, AsyncProtocolCallback callback)
		{
			if (this._CompletionStatus != 0)
			{
				throw new InternalException();
			}
			this.Buffer = buffer;
			this.Offset = offset;
			this.Count = count;
			this._Callback = callback;
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x0006AA2A File Offset: 0x00068C2A
		internal object AsyncObject
		{
			get
			{
				return this.UserAsyncResult.AsyncObject;
			}
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0006AA38 File Offset: 0x00068C38
		internal void CompleteRequest(int result)
		{
			this.Result = result;
			int num = Interlocked.Exchange(ref this._CompletionStatus, 1);
			if (num == 1)
			{
				throw new InternalException();
			}
			if (num == 2)
			{
				this._CompletionStatus = 0;
				this._Callback(this);
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x0006AA7C File Offset: 0x00068C7C
		public bool MustCompleteSynchronously
		{
			get
			{
				int num = Interlocked.Exchange(ref this._CompletionStatus, 2);
				if (num == 2)
				{
					throw new InternalException();
				}
				if (num == 1)
				{
					this._CompletionStatus = 0;
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0006AAAE File Offset: 0x00068CAE
		internal void CompleteWithError(Exception e)
		{
			this.UserAsyncResult.InvokeCallback(e);
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0006AABC File Offset: 0x00068CBC
		internal void CompleteUser()
		{
			this.UserAsyncResult.InvokeCallback();
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0006AAC9 File Offset: 0x00068CC9
		internal void CompleteUser(object userResult)
		{
			this.UserAsyncResult.InvokeCallback(userResult);
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x0006AAD7 File Offset: 0x00068CD7
		internal bool IsUserCompleted
		{
			get
			{
				return this.UserAsyncResult.InternalPeekCompleted;
			}
		}

		// Token: 0x04001614 RID: 5652
		private AsyncProtocolCallback _Callback;

		// Token: 0x04001615 RID: 5653
		private int _CompletionStatus;

		// Token: 0x04001616 RID: 5654
		private const int StatusNotStarted = 0;

		// Token: 0x04001617 RID: 5655
		private const int StatusCompleted = 1;

		// Token: 0x04001618 RID: 5656
		private const int StatusCheckedOnSyncCompletion = 2;

		// Token: 0x04001619 RID: 5657
		public LazyAsyncResult UserAsyncResult;

		// Token: 0x0400161A RID: 5658
		public int Result;

		// Token: 0x0400161B RID: 5659
		public object AsyncState;

		// Token: 0x0400161C RID: 5660
		public byte[] Buffer;

		// Token: 0x0400161D RID: 5661
		public int Offset;

		// Token: 0x0400161E RID: 5662
		public int Count;
	}
}
