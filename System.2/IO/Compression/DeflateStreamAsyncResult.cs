using System;
using System.Threading;

namespace System.IO.Compression
{
	// Token: 0x02000427 RID: 1063
	internal class DeflateStreamAsyncResult : IAsyncResult
	{
		// Token: 0x060027E7 RID: 10215 RVA: 0x000B77EE File Offset: 0x000B59EE
		public DeflateStreamAsyncResult(object asyncObject, object asyncState, AsyncCallback asyncCallback, byte[] buffer, int offset, int count)
		{
			this.buffer = buffer;
			this.offset = offset;
			this.count = count;
			this.m_CompletedSynchronously = true;
			this.m_AsyncObject = asyncObject;
			this.m_AsyncState = asyncState;
			this.m_AsyncCallback = asyncCallback;
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x000B782A File Offset: 0x000B5A2A
		public object AsyncState
		{
			get
			{
				return this.m_AsyncState;
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x060027E9 RID: 10217 RVA: 0x000B7834 File Offset: 0x000B5A34
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				int completed = this.m_Completed;
				if (this.m_Event == null)
				{
					Interlocked.CompareExchange(ref this.m_Event, new ManualResetEvent(completed != 0), null);
				}
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.m_Event;
				if (completed == 0 && this.m_Completed != 0)
				{
					manualResetEvent.Set();
				}
				return manualResetEvent;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x000B7885 File Offset: 0x000B5A85
		public bool CompletedSynchronously
		{
			get
			{
				return this.m_CompletedSynchronously;
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x060027EB RID: 10219 RVA: 0x000B788D File Offset: 0x000B5A8D
		public bool IsCompleted
		{
			get
			{
				return this.m_Completed != 0;
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x000B7898 File Offset: 0x000B5A98
		internal object Result
		{
			get
			{
				return this.m_Result;
			}
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x000B78A0 File Offset: 0x000B5AA0
		internal void Close()
		{
			if (this.m_Event != null)
			{
				((ManualResetEvent)this.m_Event).Close();
			}
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x000B78BA File Offset: 0x000B5ABA
		internal void InvokeCallback(bool completedSynchronously, object result)
		{
			this.Complete(completedSynchronously, result);
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x000B78C4 File Offset: 0x000B5AC4
		internal void InvokeCallback(object result)
		{
			this.Complete(result);
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x000B78CD File Offset: 0x000B5ACD
		private void Complete(bool completedSynchronously, object result)
		{
			this.m_CompletedSynchronously = completedSynchronously;
			this.Complete(result);
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000B78E0 File Offset: 0x000B5AE0
		private void Complete(object result)
		{
			this.m_Result = result;
			Interlocked.Increment(ref this.m_Completed);
			if (this.m_Event != null)
			{
				((ManualResetEvent)this.m_Event).Set();
			}
			if (Interlocked.Increment(ref this.m_InvokedCallback) == 1 && this.m_AsyncCallback != null)
			{
				this.m_AsyncCallback(this);
			}
		}

		// Token: 0x040021A5 RID: 8613
		public byte[] buffer;

		// Token: 0x040021A6 RID: 8614
		public int offset;

		// Token: 0x040021A7 RID: 8615
		public int count;

		// Token: 0x040021A8 RID: 8616
		public bool isWrite;

		// Token: 0x040021A9 RID: 8617
		private object m_AsyncObject;

		// Token: 0x040021AA RID: 8618
		private object m_AsyncState;

		// Token: 0x040021AB RID: 8619
		private AsyncCallback m_AsyncCallback;

		// Token: 0x040021AC RID: 8620
		private object m_Result;

		// Token: 0x040021AD RID: 8621
		internal bool m_CompletedSynchronously;

		// Token: 0x040021AE RID: 8622
		private int m_InvokedCallback;

		// Token: 0x040021AF RID: 8623
		private int m_Completed;

		// Token: 0x040021B0 RID: 8624
		private object m_Event;
	}
}
