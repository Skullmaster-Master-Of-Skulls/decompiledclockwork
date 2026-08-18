using System;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200072E RID: 1838
	internal abstract class ChannelWrapper<TChannel, TItem> : LayeredChannel<TChannel> where TChannel : class, IChannel where TItem : class, IDisposable
	{
		// Token: 0x060045E1 RID: 17889 RVA: 0x0010598A File Offset: 0x00103B8A
		public ChannelWrapper(ChannelManagerBase channelManager, TChannel innerChannel, TItem firstItem) : base(channelManager, innerChannel)
		{
			this.firstItem = firstItem;
		}

		// Token: 0x060045E2 RID: 17890
		protected abstract void CloseFirstItem(TimeSpan timeout);

		// Token: 0x060045E3 RID: 17891 RVA: 0x0010599C File Offset: 0x00103B9C
		protected TItem GetFirstItem()
		{
			return Interlocked.Exchange<TItem>(ref this.firstItem, default(TItem));
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x001059BD File Offset: 0x00103BBD
		protected bool HaveFirstItem()
		{
			return this.firstItem != null;
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x001059CD File Offset: 0x00103BCD
		protected override void OnAbort()
		{
			base.OnAbort();
			this.CloseFirstItem(TimeSpan.Zero);
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x001059E0 File Offset: 0x00103BE0
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.CloseFirstItem(timeoutHelper.RemainingTime());
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x00105A10 File Offset: 0x00103C10
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.CloseFirstItem(timeoutHelper.RemainingTime());
			return base.OnBeginClose(timeoutHelper.RemainingTime(), callback, state);
		}

		// Token: 0x060045E8 RID: 17896 RVA: 0x00105A41 File Offset: 0x00103C41
		protected override void OnEndClose(IAsyncResult result)
		{
			base.OnEndClose(result);
		}

		// Token: 0x04002D6F RID: 11631
		private TItem firstItem;

		// Token: 0x02000CD0 RID: 3280
		protected class ReceiveAsyncResult : AsyncResult
		{
			// Token: 0x060079CD RID: 31181 RVA: 0x001C6601 File Offset: 0x001C4801
			public ReceiveAsyncResult(TItem item, AsyncCallback callback, object state) : base(callback, state)
			{
				this.item = item;
				base.Complete(true);
			}

			// Token: 0x060079CE RID: 31182 RVA: 0x001C661C File Offset: 0x001C481C
			public static TItem End(IAsyncResult result)
			{
				ChannelWrapper<TChannel, TItem>.ReceiveAsyncResult receiveAsyncResult = AsyncResult.End<ChannelWrapper<TChannel, TItem>.ReceiveAsyncResult>(result);
				return receiveAsyncResult.item;
			}

			// Token: 0x040045B1 RID: 17841
			private TItem item;
		}

		// Token: 0x02000CD1 RID: 3281
		protected class WaitAsyncResult : AsyncResult
		{
			// Token: 0x060079CF RID: 31183 RVA: 0x001C6636 File Offset: 0x001C4836
			public WaitAsyncResult(AsyncCallback callback, object state) : base(callback, state)
			{
				base.Complete(true);
			}

			// Token: 0x060079D0 RID: 31184 RVA: 0x001C6648 File Offset: 0x001C4848
			public static bool End(IAsyncResult result)
			{
				ChannelWrapper<TChannel, TItem>.WaitAsyncResult waitAsyncResult = AsyncResult.End<ChannelWrapper<TChannel, TItem>.WaitAsyncResult>(result);
				return true;
			}
		}
	}
}
