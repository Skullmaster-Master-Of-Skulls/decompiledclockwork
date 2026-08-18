using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009FD RID: 2557
	internal class FloodAsyncResult : AsyncResult
	{
		// Token: 0x14000043 RID: 67
		// (add) Token: 0x0600657E RID: 25982 RVA: 0x0017A6E8 File Offset: 0x001788E8
		// (remove) Token: 0x0600657F RID: 25983 RVA: 0x0017A720 File Offset: 0x00178920
		public event EventHandler OnMessageSent;

		// Token: 0x06006580 RID: 25984 RVA: 0x0017A755 File Offset: 0x00178955
		public FloodAsyncResult(PeerNeighborManager owner, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
		{
			this.pnm = owner;
			this.timeoutHelper = new TimeoutHelper(timeout);
		}

		// Token: 0x17001876 RID: 6262
		// (get) Token: 0x06006581 RID: 25985 RVA: 0x0017A794 File Offset: 0x00178994
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x06006582 RID: 25986 RVA: 0x0017A79C File Offset: 0x0017899C
		public void AddResult(IAsyncResult result, IPeerNeighbor neighbor)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.results.Add(result, neighbor);
			}
		}

		// Token: 0x06006583 RID: 25987 RVA: 0x0017A7E4 File Offset: 0x001789E4
		public void End()
		{
			if (!this.doneAdding || !this.shouldCallComplete)
			{
				throw Fx.AssertAndThrow("Unexpected end!");
			}
			if (this.isCompleted)
			{
				return;
			}
			if (!TimeoutHelper.WaitOne(base.AsyncWaitHandle, this.timeoutHelper.RemainingTime()))
			{
				if (!this.offNode)
				{
					try
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException());
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
						this.exception = ex;
					}
				}
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.isCompleted)
					{
						return;
					}
					this.isCompleted = true;
				}
				this.CompleteOp(false);
			}
			AsyncResult.End<FloodAsyncResult>(this);
		}

		// Token: 0x06006584 RID: 25988 RVA: 0x0017A8C0 File Offset: 0x00178AC0
		public void MarkEnd(bool success)
		{
			bool flag = false;
			try
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					foreach (IAsyncResult result in this.pending)
					{
						this.OnSendComplete(result);
					}
					this.pending.Clear();
					this.doneAdding = true;
					this.shouldCallComplete = success;
					if (this.results.Count == 0)
					{
						this.isCompleted = true;
						flag = true;
					}
				}
			}
			finally
			{
				if (flag)
				{
					this.CompleteOp(true);
				}
			}
		}

		// Token: 0x06006585 RID: 25989 RVA: 0x0017A988 File Offset: 0x00178B88
		internal void OnSendComplete(IAsyncResult result)
		{
			bool flag = false;
			IPeerNeighbor peerNeighbor = null;
			bool flag2 = false;
			if (this.isCompleted)
			{
				return;
			}
			Message message = (Message)result.AsyncState;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.isCompleted)
				{
					return;
				}
				if (!this.results.TryGetValue(result, out peerNeighbor))
				{
					if (!this.doneAdding)
					{
						this.pending.Add(result);
						return;
					}
					throw Fx.AssertAndThrow("IAsyncResult is un-accounted for.");
				}
				else
				{
					this.results.Remove(result);
					try
					{
						if (!result.CompletedSynchronously)
						{
							peerNeighbor.EndSend(result);
							this.offNode = true;
							UtilityExtension.OnEndSend(peerNeighbor, this);
						}
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							flag2 = true;
							throw;
						}
						Exception ex2 = PeerFlooderBase<Message, UtilityInfo>.CloseNeighborIfKnownException(this.pnm, ex, peerNeighbor);
						if (ex2 != null && this.doneAdding && !this.shouldCallComplete)
						{
							throw;
						}
						if (this.exception == null)
						{
							this.exception = ex2;
						}
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
					}
					finally
					{
						if (message != null && !result.CompletedSynchronously && !flag2)
						{
							message.Close();
						}
					}
					if (this.results.Count == 0 && this.doneAdding && this.shouldCallComplete)
					{
						this.isCompleted = true;
						flag = true;
					}
				}
			}
			if (flag && this.shouldCallComplete)
			{
				this.CompleteOp(false);
			}
		}

		// Token: 0x06006586 RID: 25990 RVA: 0x0017AB30 File Offset: 0x00178D30
		private void CompleteOp(bool sync)
		{
			this.OnMessageSent(this, EventArgs.Empty);
			base.Complete(sync, this.exception);
		}

		// Token: 0x04003A27 RID: 14887
		private bool doneAdding;

		// Token: 0x04003A28 RID: 14888
		private Exception exception;

		// Token: 0x04003A29 RID: 14889
		private PeerNeighborManager pnm;

		// Token: 0x04003A2A RID: 14890
		private volatile bool isCompleted;

		// Token: 0x04003A2B RID: 14891
		private List<IAsyncResult> pending = new List<IAsyncResult>();

		// Token: 0x04003A2C RID: 14892
		private Dictionary<IAsyncResult, IPeerNeighbor> results = new Dictionary<IAsyncResult, IPeerNeighbor>();

		// Token: 0x04003A2D RID: 14893
		private bool shouldCallComplete;

		// Token: 0x04003A2E RID: 14894
		private object thisLock = new object();

		// Token: 0x04003A2F RID: 14895
		private TimeoutHelper timeoutHelper;

		// Token: 0x04003A30 RID: 14896
		private bool offNode;
	}
}
