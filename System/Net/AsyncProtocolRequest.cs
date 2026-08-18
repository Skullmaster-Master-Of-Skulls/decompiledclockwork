using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200059C RID: 1436
	internal class AsyncProtocolRequest
	{
		// Token: 0x06002C3A RID: 11322 RVA: 0x000BE24E File Offset: 0x000BD24E
		public AsyncProtocolRequest(LazyAsyncResult userAsyncResult)
		{
			this.UserAsyncResult = userAsyncResult;
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000BE25D File Offset: 0x000BD25D
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

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002C3C RID: 11324 RVA: 0x000BE28A File Offset: 0x000BD28A
		internal object AsyncObject
		{
			get
			{
				return this.UserAsyncResult.AsyncObject;
			}
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000BE298 File Offset: 0x000BD298
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

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06002C3E RID: 11326 RVA: 0x000BE2DC File Offset: 0x000BD2DC
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

		// Token: 0x06002C3F RID: 11327 RVA: 0x000BE30E File Offset: 0x000BD30E
		internal void CompleteWithError(Exception e)
		{
			this.UserAsyncResult.InvokeCallback(e);
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000BE31C File Offset: 0x000BD31C
		internal void CompleteUser()
		{
			this.UserAsyncResult.InvokeCallback();
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000BE329 File Offset: 0x000BD329
		internal void CompleteUser(object userResult)
		{
			this.UserAsyncResult.InvokeCallback(userResult);
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06002C42 RID: 11330 RVA: 0x000BE337 File Offset: 0x000BD337
		internal bool IsUserCompleted
		{
			get
			{
				return this.UserAsyncResult.InternalPeekCompleted;
			}
		}

		// Token: 0x04002A12 RID: 10770
		private const int StatusNotStarted = 0;

		// Token: 0x04002A13 RID: 10771
		private const int StatusCompleted = 1;

		// Token: 0x04002A14 RID: 10772
		private const int StatusCheckedOnSyncCompletion = 2;

		// Token: 0x04002A15 RID: 10773
		private AsyncProtocolCallback _Callback;

		// Token: 0x04002A16 RID: 10774
		private int _CompletionStatus;

		// Token: 0x04002A17 RID: 10775
		public LazyAsyncResult UserAsyncResult;

		// Token: 0x04002A18 RID: 10776
		public int Result;

		// Token: 0x04002A19 RID: 10777
		public object AsyncState;

		// Token: 0x04002A1A RID: 10778
		public byte[] Buffer;

		// Token: 0x04002A1B RID: 10779
		public int Offset;

		// Token: 0x04002A1C RID: 10780
		public int Count;
	}
}
