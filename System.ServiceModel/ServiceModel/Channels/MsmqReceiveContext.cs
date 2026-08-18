using System;
using System.Runtime;
using System.Transactions;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008F2 RID: 2290
	internal class MsmqReceiveContext : ReceiveContext
	{
		// Token: 0x06005758 RID: 22360 RVA: 0x0014089B File Offset: 0x0013EA9B
		public MsmqReceiveContext(long lookupId, DateTime expiryTime, MsmqReceiveContextLockManager manager)
		{
			this.manager = manager;
			this.lookupId = lookupId;
			this.expiryTime = expiryTime;
		}

		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x06005759 RID: 22361 RVA: 0x001408B8 File Offset: 0x0013EAB8
		public long LookupId
		{
			get
			{
				return this.lookupId;
			}
		}

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x0600575A RID: 22362 RVA: 0x001408C0 File Offset: 0x0013EAC0
		public DateTime ExpiryTime
		{
			get
			{
				return this.expiryTime;
			}
		}

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x0600575B RID: 22363 RVA: 0x001408C8 File Offset: 0x0013EAC8
		public MsmqReceiveContextLockManager Manager
		{
			get
			{
				return this.manager;
			}
		}

		// Token: 0x0600575C RID: 22364 RVA: 0x001408D0 File Offset: 0x0013EAD0
		public void MarkContextExpired()
		{
			base.Fault();
		}

		// Token: 0x0600575D RID: 22365 RVA: 0x001408D8 File Offset: 0x0013EAD8
		protected override void OnComplete(TimeSpan timeout)
		{
			this.manager.DeleteMessage(this, timeout);
		}

		// Token: 0x0600575E RID: 22366 RVA: 0x001408E7 File Offset: 0x0013EAE7
		protected override void OnAbandon(TimeSpan timeout)
		{
			this.manager.UnlockMessage(this, timeout);
		}

		// Token: 0x0600575F RID: 22367 RVA: 0x001408F6 File Offset: 0x0013EAF6
		protected override IAsyncResult OnBeginComplete(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return MsmqReceiveContext.ReceiveContextAsyncResult.CreateComplete(this, timeout, callback, state);
		}

		// Token: 0x06005760 RID: 22368 RVA: 0x00140901 File Offset: 0x0013EB01
		protected override void OnEndComplete(IAsyncResult result)
		{
			MsmqReceiveContext.ReceiveContextAsyncResult.End(result);
		}

		// Token: 0x06005761 RID: 22369 RVA: 0x00140909 File Offset: 0x0013EB09
		protected override IAsyncResult OnBeginAbandon(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return MsmqReceiveContext.ReceiveContextAsyncResult.CreateAbandon(this, timeout, callback, state);
		}

		// Token: 0x06005762 RID: 22370 RVA: 0x00140914 File Offset: 0x0013EB14
		protected override void OnEndAbandon(IAsyncResult result)
		{
			MsmqReceiveContext.ReceiveContextAsyncResult.End(result);
		}

		// Token: 0x040035B9 RID: 13753
		private long lookupId;

		// Token: 0x040035BA RID: 13754
		private DateTime expiryTime;

		// Token: 0x040035BB RID: 13755
		private MsmqReceiveContextLockManager manager;

		// Token: 0x02000D94 RID: 3476
		private class ReceiveContextAsyncResult : AsyncResult
		{
			// Token: 0x06007EB3 RID: 32435 RVA: 0x001D7F85 File Offset: 0x001D6185
			private ReceiveContextAsyncResult(MsmqReceiveContext receiver, TimeSpan timeout, AsyncCallback callback, object state, Action<object> target) : base(callback, state)
			{
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.receiver = receiver;
				if (Transaction.Current != null)
				{
					this.associatedTransaction = Transaction.Current;
				}
				ActionItem.Schedule(target, this);
			}

			// Token: 0x06007EB4 RID: 32436 RVA: 0x001D7FC3 File Offset: 0x001D61C3
			public static IAsyncResult CreateComplete(MsmqReceiveContext receiver, TimeSpan timeout, AsyncCallback callback, object state)
			{
				if (MsmqReceiveContext.ReceiveContextAsyncResult.onComplete == null)
				{
					MsmqReceiveContext.ReceiveContextAsyncResult.onComplete = new Action<object>(MsmqReceiveContext.ReceiveContextAsyncResult.OnComplete);
				}
				return new MsmqReceiveContext.ReceiveContextAsyncResult(receiver, timeout, callback, state, MsmqReceiveContext.ReceiveContextAsyncResult.onComplete);
			}

			// Token: 0x06007EB5 RID: 32437 RVA: 0x001D7FEB File Offset: 0x001D61EB
			public static IAsyncResult CreateAbandon(MsmqReceiveContext receiver, TimeSpan timeout, AsyncCallback callback, object state)
			{
				if (MsmqReceiveContext.ReceiveContextAsyncResult.onAbandon == null)
				{
					MsmqReceiveContext.ReceiveContextAsyncResult.onAbandon = new Action<object>(MsmqReceiveContext.ReceiveContextAsyncResult.OnAbandon);
				}
				return new MsmqReceiveContext.ReceiveContextAsyncResult(receiver, timeout, callback, state, MsmqReceiveContext.ReceiveContextAsyncResult.onAbandon);
			}

			// Token: 0x06007EB6 RID: 32438 RVA: 0x001D8014 File Offset: 0x001D6214
			private static void OnComplete(object parameter)
			{
				MsmqReceiveContext.ReceiveContextAsyncResult receiveContextAsyncResult = parameter as MsmqReceiveContext.ReceiveContextAsyncResult;
				Exception exception = null;
				Transaction value = null;
				try
				{
					value = Transaction.Current;
					Transaction.Current = receiveContextAsyncResult.associatedTransaction;
					receiveContextAsyncResult.receiver.OnComplete(receiveContextAsyncResult.timeoutHelper.RemainingTime());
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				finally
				{
					Transaction.Current = value;
				}
				receiveContextAsyncResult.Complete(false, exception);
			}

			// Token: 0x06007EB7 RID: 32439 RVA: 0x001D8090 File Offset: 0x001D6290
			private static void OnAbandon(object parameter)
			{
				MsmqReceiveContext.ReceiveContextAsyncResult receiveContextAsyncResult = parameter as MsmqReceiveContext.ReceiveContextAsyncResult;
				Exception exception = null;
				try
				{
					receiveContextAsyncResult.receiver.OnAbandon(receiveContextAsyncResult.timeoutHelper.RemainingTime());
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				receiveContextAsyncResult.Complete(false, exception);
			}

			// Token: 0x06007EB8 RID: 32440 RVA: 0x001D80E8 File Offset: 0x001D62E8
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<MsmqReceiveContext.ReceiveContextAsyncResult>(result);
			}

			// Token: 0x040048AF RID: 18607
			private MsmqReceiveContext receiver;

			// Token: 0x040048B0 RID: 18608
			private TimeoutHelper timeoutHelper;

			// Token: 0x040048B1 RID: 18609
			private static Action<object> onComplete;

			// Token: 0x040048B2 RID: 18610
			private static Action<object> onAbandon;

			// Token: 0x040048B3 RID: 18611
			private Transaction associatedTransaction;
		}
	}
}
