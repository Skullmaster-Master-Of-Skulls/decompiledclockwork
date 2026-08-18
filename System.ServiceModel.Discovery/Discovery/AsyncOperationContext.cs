using System;
using System.ComponentModel;
using System.Runtime;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000009 RID: 9
	internal abstract class AsyncOperationContext
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00003201 File Offset: 0x00001401
		internal AsyncOperationContext(UniqueId operationId, int maxResults, TimeSpan duration, object userState)
		{
			this.maxResults = maxResults;
			this.duration = duration;
			this.userState = userState;
			this.operationId = operationId;
			this.syncRoot = new object();
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003231 File Offset: 0x00001431
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003239 File Offset: 0x00001439
		public AsyncOperation AsyncOperation
		{
			get
			{
				return this.asyncOperation;
			}
			set
			{
				this.asyncOperation = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003242 File Offset: 0x00001442
		public TimeSpan Duration
		{
			get
			{
				return this.duration;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000324A File Offset: 0x0000144A
		public bool IsCompleted
		{
			get
			{
				return this.isCompleted;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003252 File Offset: 0x00001452
		public bool IsSyncOperation
		{
			get
			{
				return this.UserState is SyncOperationState;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003262 File Offset: 0x00001462
		public int MaxResults
		{
			get
			{
				return this.maxResults;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000326A File Offset: 0x0000146A
		public UniqueId OperationId
		{
			get
			{
				return this.operationId;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003272 File Offset: 0x00001472
		public object SyncRoot
		{
			get
			{
				return this.syncRoot;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000327A File Offset: 0x0000147A
		public object UserState
		{
			get
			{
				return this.userState;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003282 File Offset: 0x00001482
		public DateTime? StartedAt
		{
			get
			{
				return this.startTime;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000328A File Offset: 0x0000148A
		public void Complete()
		{
			this.StopTimer();
			this.isCompleted = true;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003299 File Offset: 0x00001499
		public void StartTimer(Action<object> waitCallback)
		{
			this.startTime = new DateTime?(DateTime.UtcNow);
			this.timer = new IOThreadTimer(waitCallback, this, false);
			this.timer.Set(this.Duration);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000032CA File Offset: 0x000014CA
		private void StopTimer()
		{
			if (this.timer != null)
			{
				this.timer.Cancel();
				this.timer = null;
			}
		}

		// Token: 0x04000020 RID: 32
		private AsyncOperation asyncOperation;

		// Token: 0x04000021 RID: 33
		private TimeSpan duration;

		// Token: 0x04000022 RID: 34
		private bool isCompleted;

		// Token: 0x04000023 RID: 35
		private int maxResults;

		// Token: 0x04000024 RID: 36
		private UniqueId operationId;

		// Token: 0x04000025 RID: 37
		private DateTime? startTime;

		// Token: 0x04000026 RID: 38
		private object syncRoot;

		// Token: 0x04000027 RID: 39
		private IOThreadTimer timer;

		// Token: 0x04000028 RID: 40
		private object userState;
	}
}
