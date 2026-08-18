using System;

namespace System.Runtime
{
	// Token: 0x02000007 RID: 7
	internal abstract class AsyncEventArgs : IAsyncEventArgs
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002584 File Offset: 0x00000784
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000258C File Offset: 0x0000078C
		public object AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x17000005 RID: 5
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002594 File Offset: 0x00000794
		private AsyncEventArgs.OperationState State
		{
			set
			{
				if (value != AsyncEventArgs.OperationState.PendingCompletion)
				{
					if (value - AsyncEventArgs.OperationState.CompletedSynchronously <= 1)
					{
						if (this.state != AsyncEventArgs.OperationState.PendingCompletion)
						{
							throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.AsyncEventArgsCompletedTwice(base.GetType())));
						}
					}
				}
				else if (this.state == AsyncEventArgs.OperationState.PendingCompletion)
				{
					throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.AsyncEventArgsCompletionPending(base.GetType())));
				}
				this.state = value;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025FC File Offset: 0x000007FC
		public void Complete(bool completedSynchronously)
		{
			this.Complete(completedSynchronously, null);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002606 File Offset: 0x00000806
		public virtual void Complete(bool completedSynchronously, Exception exception)
		{
			this.exception = exception;
			if (completedSynchronously)
			{
				this.State = AsyncEventArgs.OperationState.CompletedSynchronously;
				return;
			}
			this.State = AsyncEventArgs.OperationState.CompletedAsynchronously;
			this.callback(this);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000262D File Offset: 0x0000082D
		protected void SetAsyncState(AsyncEventArgsCallback callback, object state)
		{
			if (callback == null)
			{
				throw Fx.Exception.ArgumentNull("callback");
			}
			this.State = AsyncEventArgs.OperationState.PendingCompletion;
			this.asyncState = state;
			this.callback = callback;
		}

		// Token: 0x0400000C RID: 12
		private AsyncEventArgs.OperationState state;

		// Token: 0x0400000D RID: 13
		private object asyncState;

		// Token: 0x0400000E RID: 14
		private AsyncEventArgsCallback callback;

		// Token: 0x0400000F RID: 15
		private Exception exception;

		// Token: 0x0200005A RID: 90
		private enum OperationState
		{
			// Token: 0x040001D0 RID: 464
			Created,
			// Token: 0x040001D1 RID: 465
			PendingCompletion,
			// Token: 0x040001D2 RID: 466
			CompletedSynchronously,
			// Token: 0x040001D3 RID: 467
			CompletedAsynchronously
		}
	}
}
