using System;
using System.Runtime;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000919 RID: 2329
	internal abstract class ReliableChannelListenerBase<TChannel> : DelegatingChannelListener<TChannel>, IReliableFactorySettings where TChannel : class, IChannel
	{
		// Token: 0x0600592D RID: 22829 RVA: 0x00146440 File Offset: 0x00144640
		protected ReliableChannelListenerBase(ReliableSessionBindingElement settings, Binding binding) : base(true, binding)
		{
			this.acknowledgementInterval = settings.AcknowledgementInterval;
			this.flowControlEnabled = settings.FlowControlEnabled;
			this.inactivityTimeout = settings.InactivityTimeout;
			this.maxPendingChannels = settings.MaxPendingChannels;
			this.maxRetryCount = settings.MaxRetryCount;
			this.maxTransferWindowSize = settings.MaxTransferWindowSize;
			this.messageVersion = binding.MessageVersion;
			this.ordered = settings.Ordered;
			this.reliableMessagingVersion = settings.ReliableMessagingVersion;
		}

		// Token: 0x170015BE RID: 5566
		// (get) Token: 0x0600592E RID: 22830 RVA: 0x001464C1 File Offset: 0x001446C1
		public TimeSpan AcknowledgementInterval
		{
			get
			{
				return this.acknowledgementInterval;
			}
		}

		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x0600592F RID: 22831 RVA: 0x001464C9 File Offset: 0x001446C9
		// (set) Token: 0x06005930 RID: 22832 RVA: 0x001464D1 File Offset: 0x001446D1
		protected FaultHelper FaultHelper
		{
			get
			{
				return this.faultHelper;
			}
			set
			{
				this.faultHelper = value;
			}
		}

		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x06005931 RID: 22833 RVA: 0x001464DA File Offset: 0x001446DA
		public bool FlowControlEnabled
		{
			get
			{
				return this.flowControlEnabled;
			}
		}

		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x06005932 RID: 22834 RVA: 0x001464E2 File Offset: 0x001446E2
		public TimeSpan InactivityTimeout
		{
			get
			{
				return this.inactivityTimeout;
			}
		}

		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x06005933 RID: 22835 RVA: 0x001464EA File Offset: 0x001446EA
		protected bool IsAccepting
		{
			get
			{
				return base.State == CommunicationState.Opened;
			}
		}

		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x06005934 RID: 22836 RVA: 0x001464F5 File Offset: 0x001446F5
		// (set) Token: 0x06005935 RID: 22837 RVA: 0x001464FD File Offset: 0x001446FD
		public IMessageFilterTable<EndpointAddress> LocalAddresses
		{
			get
			{
				return this.localAddresses;
			}
			set
			{
				this.localAddresses = value;
			}
		}

		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x06005936 RID: 22838 RVA: 0x00146506 File Offset: 0x00144706
		public int MaxPendingChannels
		{
			get
			{
				return this.maxPendingChannels;
			}
		}

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x06005937 RID: 22839 RVA: 0x0014650E File Offset: 0x0014470E
		public int MaxRetryCount
		{
			get
			{
				return this.maxRetryCount;
			}
		}

		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x06005938 RID: 22840 RVA: 0x00146516 File Offset: 0x00144716
		public int MaxTransferWindowSize
		{
			get
			{
				return this.maxTransferWindowSize;
			}
		}

		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x06005939 RID: 22841 RVA: 0x0014651E File Offset: 0x0014471E
		public MessageVersion MessageVersion
		{
			get
			{
				return this.messageVersion;
			}
		}

		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x0600593A RID: 22842 RVA: 0x00146526 File Offset: 0x00144726
		public bool Ordered
		{
			get
			{
				return this.ordered;
			}
		}

		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x0600593B RID: 22843 RVA: 0x0014652E File Offset: 0x0014472E
		public ReliableMessagingVersion ReliableMessagingVersion
		{
			get
			{
				return this.reliableMessagingVersion;
			}
		}

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x0600593C RID: 22844 RVA: 0x00146536 File Offset: 0x00144736
		public TimeSpan SendTimeout
		{
			get
			{
				return base.InternalSendTimeout;
			}
		}

		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x0600593D RID: 22845
		protected abstract bool Duplex { get; }

		// Token: 0x0600593E RID: 22846
		protected abstract bool HasChannels();

		// Token: 0x0600593F RID: 22847
		protected abstract bool IsLastChannel(UniqueId inputId);

		// Token: 0x06005940 RID: 22848 RVA: 0x00146540 File Offset: 0x00144740
		protected override void OnAbort()
		{
			object thisLock = base.ThisLock;
			bool flag2;
			lock (thisLock)
			{
				this.closed = true;
				flag2 = !this.HasChannels();
			}
			if (flag2)
			{
				this.AbortInnerListener();
			}
			base.OnAbort();
		}

		// Token: 0x06005941 RID: 22849 RVA: 0x0014659C File Offset: 0x0014479C
		protected virtual void AbortInnerListener()
		{
			this.faultHelper.Abort();
			this.InnerChannelListener.Abort();
		}

		// Token: 0x06005942 RID: 22850 RVA: 0x001465B4 File Offset: 0x001447B4
		protected virtual void CloseInnerListener(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.faultHelper.Close(timeoutHelper.RemainingTime());
			this.InnerChannelListener.Close(timeoutHelper.RemainingTime());
		}

		// Token: 0x06005943 RID: 22851 RVA: 0x001465F0 File Offset: 0x001447F0
		protected virtual IAsyncResult BeginCloseInnerListener(TimeSpan timeout, AsyncCallback callback, object state)
		{
			OperationWithTimeoutBeginCallback[] beginOperations = new OperationWithTimeoutBeginCallback[]
			{
				new OperationWithTimeoutBeginCallback(this.faultHelper.BeginClose),
				new OperationWithTimeoutBeginCallback(this.InnerChannelListener.BeginClose)
			};
			OperationEndCallback[] endOperations = new OperationEndCallback[]
			{
				new OperationEndCallback(this.faultHelper.EndClose),
				new OperationEndCallback(this.InnerChannelListener.EndClose)
			};
			return OperationWithTimeoutComposer.BeginComposeAsyncOperations(timeout, beginOperations, endOperations, callback, state);
		}

		// Token: 0x06005944 RID: 22852 RVA: 0x00146669 File Offset: 0x00144869
		protected virtual void EndCloseInnerListener(IAsyncResult result)
		{
			OperationWithTimeoutComposer.EndComposeAsyncOperations(result);
		}

		// Token: 0x06005945 RID: 22853 RVA: 0x00146674 File Offset: 0x00144874
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.ShouldCloseOnChannelListenerClose())
			{
				this.CloseInnerListener(timeoutHelper.RemainingTime());
				this.closed = true;
			}
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06005946 RID: 22854 RVA: 0x001466B2 File Offset: 0x001448B2
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ReliableChannelListenerBase<TChannel>.CloseAsyncResult(this, new OperationWithTimeoutBeginCallback(base.OnBeginClose), new OperationEndCallback(base.OnEndClose), timeout, callback, state);
		}

		// Token: 0x06005947 RID: 22855 RVA: 0x001466D5 File Offset: 0x001448D5
		protected override void OnEndClose(IAsyncResult result)
		{
			ReliableChannelListenerBase<TChannel>.CloseAsyncResult.End(result);
		}

		// Token: 0x06005948 RID: 22856 RVA: 0x001466E0 File Offset: 0x001448E0
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
			this.InnerChannelListener.Open(timeoutHelper.RemainingTime());
		}

		// Token: 0x06005949 RID: 22857 RVA: 0x00146714 File Offset: 0x00144914
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return OperationWithTimeoutComposer.BeginComposeAsyncOperations(timeout, new OperationWithTimeoutBeginCallback[]
			{
				new OperationWithTimeoutBeginCallback(base.OnBeginOpen),
				new OperationWithTimeoutBeginCallback(this.InnerChannelListener.BeginOpen)
			}, new OperationEndCallback[]
			{
				new OperationEndCallback(base.OnEndOpen),
				new OperationEndCallback(this.InnerChannelListener.EndOpen)
			}, callback, state);
		}

		// Token: 0x0600594A RID: 22858 RVA: 0x0014677D File Offset: 0x0014497D
		protected override void OnEndOpen(IAsyncResult result)
		{
			OperationWithTimeoutComposer.EndComposeAsyncOperations(result);
		}

		// Token: 0x0600594B RID: 22859 RVA: 0x00146788 File Offset: 0x00144988
		public void OnReliableChannelAbort(UniqueId inputId, UniqueId outputId)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				this.RemoveChannel(inputId, outputId);
				if (!this.closed || this.HasChannels())
				{
					return;
				}
			}
			this.AbortInnerListener();
		}

		// Token: 0x0600594C RID: 22860 RVA: 0x001467E4 File Offset: 0x001449E4
		public void OnReliableChannelClose(UniqueId inputId, UniqueId outputId, TimeSpan timeout)
		{
			if (this.ShouldCloseOnReliableChannelClose(inputId, outputId))
			{
				this.CloseInnerListener(timeout);
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					this.RemoveChannel(inputId, outputId);
				}
			}
		}

		// Token: 0x0600594D RID: 22861 RVA: 0x00146838 File Offset: 0x00144A38
		public IAsyncResult OnReliableChannelBeginClose(UniqueId inputId, UniqueId outputId, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ReliableChannelListenerBase<TChannel>.OnReliableChannelCloseAsyncResult(this, inputId, outputId, timeout, callback, state);
		}

		// Token: 0x0600594E RID: 22862 RVA: 0x00146847 File Offset: 0x00144A47
		public void OnReliableChannelEndClose(IAsyncResult result)
		{
			ReliableChannelListenerBase<TChannel>.OnReliableChannelCloseAsyncResult.End(result);
		}

		// Token: 0x0600594F RID: 22863
		protected abstract void RemoveChannel(UniqueId inputId, UniqueId outputId);

		// Token: 0x06005950 RID: 22864 RVA: 0x00146850 File Offset: 0x00144A50
		private bool ShouldCloseOnChannelListenerClose()
		{
			object thisLock = base.ThisLock;
			bool result;
			lock (thisLock)
			{
				if (!this.HasChannels())
				{
					result = true;
				}
				else
				{
					this.closed = true;
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06005951 RID: 22865 RVA: 0x001468A0 File Offset: 0x00144AA0
		private bool ShouldCloseOnReliableChannelClose(UniqueId inputId, UniqueId outputId)
		{
			object thisLock = base.ThisLock;
			bool result;
			lock (thisLock)
			{
				if (this.closed && this.IsLastChannel(inputId))
				{
					result = true;
				}
				else
				{
					this.RemoveChannel(inputId, outputId);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x04003654 RID: 13908
		private TimeSpan acknowledgementInterval;

		// Token: 0x04003655 RID: 13909
		private bool closed;

		// Token: 0x04003656 RID: 13910
		private FaultHelper faultHelper;

		// Token: 0x04003657 RID: 13911
		private bool flowControlEnabled;

		// Token: 0x04003658 RID: 13912
		private TimeSpan inactivityTimeout;

		// Token: 0x04003659 RID: 13913
		private IMessageFilterTable<EndpointAddress> localAddresses;

		// Token: 0x0400365A RID: 13914
		private int maxPendingChannels;

		// Token: 0x0400365B RID: 13915
		private int maxRetryCount;

		// Token: 0x0400365C RID: 13916
		private int maxTransferWindowSize;

		// Token: 0x0400365D RID: 13917
		private MessageVersion messageVersion;

		// Token: 0x0400365E RID: 13918
		private bool ordered;

		// Token: 0x0400365F RID: 13919
		private ReliableMessagingVersion reliableMessagingVersion;

		// Token: 0x02000DC4 RID: 3524
		private class CloseAsyncResult : AsyncResult
		{
			// Token: 0x06007FE1 RID: 32737 RVA: 0x001DBC00 File Offset: 0x001D9E00
			public CloseAsyncResult(ReliableChannelListenerBase<TChannel> parent, OperationWithTimeoutBeginCallback baseBeginClose, OperationEndCallback baseEndClose, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.parent = parent;
				this.baseBeginClose = baseBeginClose;
				this.baseEndClose = baseEndClose;
				bool flag = false;
				if (this.parent.ShouldCloseOnChannelListenerClose())
				{
					this.timeoutHelper = new TimeoutHelper(timeout);
					IAsyncResult asyncResult = this.parent.BeginCloseInnerListener(this.timeoutHelper.RemainingTime(), ReliableChannelListenerBase<TChannel>.CloseAsyncResult.onInnerChannelListenerCloseComplete, this);
					if (asyncResult.CompletedSynchronously)
					{
						flag = this.CompleteInnerChannelListenerClose(asyncResult);
					}
				}
				else
				{
					flag = this.CloseBaseChannelListener(timeout);
				}
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007FE2 RID: 32738 RVA: 0x001DBC8C File Offset: 0x001D9E8C
			private bool CloseBaseChannelListener(TimeSpan timeout)
			{
				IAsyncResult asyncResult = this.baseBeginClose(timeout, ReliableChannelListenerBase<TChannel>.CloseAsyncResult.onBaseChannelListenerCloseComplete, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.baseEndClose(asyncResult);
					return true;
				}
				return false;
			}

			// Token: 0x06007FE3 RID: 32739 RVA: 0x001DBCC3 File Offset: 0x001D9EC3
			private bool CompleteInnerChannelListenerClose(IAsyncResult result)
			{
				this.parent.EndCloseInnerListener(result);
				this.parent.closed = true;
				this.parent.faultHelper.Abort();
				return this.CloseBaseChannelListener(this.timeoutHelper.RemainingTime());
			}

			// Token: 0x06007FE4 RID: 32740 RVA: 0x001DBCFE File Offset: 0x001D9EFE
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ReliableChannelListenerBase<TChannel>.CloseAsyncResult>(result);
			}

			// Token: 0x06007FE5 RID: 32741 RVA: 0x001DBD08 File Offset: 0x001D9F08
			private void OnBaseChannelListenerCloseComplete(IAsyncResult result)
			{
				Exception exception = null;
				try
				{
					this.baseEndClose(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				base.Complete(false, exception);
			}

			// Token: 0x06007FE6 RID: 32742 RVA: 0x001DBD4C File Offset: 0x001D9F4C
			private static void OnBaseChannelListenerCloseCompleteStatic(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					ReliableChannelListenerBase<TChannel>.CloseAsyncResult closeAsyncResult = (ReliableChannelListenerBase<TChannel>.CloseAsyncResult)result.AsyncState;
					closeAsyncResult.OnBaseChannelListenerCloseComplete(result);
				}
			}

			// Token: 0x06007FE7 RID: 32743 RVA: 0x001DBD74 File Offset: 0x001D9F74
			private void OnInnerChannelListenerCloseComplete(IAsyncResult result)
			{
				Exception exception = null;
				bool flag;
				try
				{
					flag = this.CompleteInnerChannelListenerClose(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					base.Complete(false, exception);
				}
			}

			// Token: 0x06007FE8 RID: 32744 RVA: 0x001DBDBC File Offset: 0x001D9FBC
			private static void OnInnerChannelListenerCloseCompleteStatic(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					ReliableChannelListenerBase<TChannel>.CloseAsyncResult closeAsyncResult = (ReliableChannelListenerBase<TChannel>.CloseAsyncResult)result.AsyncState;
					closeAsyncResult.OnInnerChannelListenerCloseComplete(result);
				}
			}

			// Token: 0x04004918 RID: 18712
			private OperationWithTimeoutBeginCallback baseBeginClose;

			// Token: 0x04004919 RID: 18713
			private OperationEndCallback baseEndClose;

			// Token: 0x0400491A RID: 18714
			private ReliableChannelListenerBase<TChannel> parent;

			// Token: 0x0400491B RID: 18715
			private TimeoutHelper timeoutHelper;

			// Token: 0x0400491C RID: 18716
			private static AsyncCallback onBaseChannelListenerCloseComplete = Fx.ThunkCallback(new AsyncCallback(ReliableChannelListenerBase<TChannel>.CloseAsyncResult.OnBaseChannelListenerCloseCompleteStatic));

			// Token: 0x0400491D RID: 18717
			private static AsyncCallback onInnerChannelListenerCloseComplete = Fx.ThunkCallback(new AsyncCallback(ReliableChannelListenerBase<TChannel>.CloseAsyncResult.OnInnerChannelListenerCloseCompleteStatic));
		}

		// Token: 0x02000DC5 RID: 3525
		private class OnReliableChannelCloseAsyncResult : AsyncResult
		{
			// Token: 0x06007FEA RID: 32746 RVA: 0x001DBE14 File Offset: 0x001DA014
			public OnReliableChannelCloseAsyncResult(ReliableChannelListenerBase<TChannel> channelListener, UniqueId inputId, UniqueId outputId, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				if (!channelListener.ShouldCloseOnReliableChannelClose(inputId, outputId))
				{
					base.Complete(true);
					return;
				}
				this.channelListener = channelListener;
				this.inputId = inputId;
				this.outputId = outputId;
				IAsyncResult asyncResult = this.channelListener.BeginCloseInnerListener(timeout, ReliableChannelListenerBase<TChannel>.OnReliableChannelCloseAsyncResult.onInnerChannelListenerCloseComplete, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.CompleteInnerChannelListenerClose(asyncResult);
					base.Complete(true);
				}
			}

			// Token: 0x06007FEB RID: 32747 RVA: 0x001DBE7C File Offset: 0x001DA07C
			private void CompleteInnerChannelListenerClose(IAsyncResult result)
			{
				this.channelListener.EndCloseInnerListener(result);
				object thisLock = this.channelListener.ThisLock;
				lock (thisLock)
				{
					this.channelListener.RemoveChannel(this.inputId, this.outputId);
				}
			}

			// Token: 0x06007FEC RID: 32748 RVA: 0x001DBEE0 File Offset: 0x001DA0E0
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ReliableChannelListenerBase<TChannel>.OnReliableChannelCloseAsyncResult>(result);
			}

			// Token: 0x06007FED RID: 32749 RVA: 0x001DBEEC File Offset: 0x001DA0EC
			private void OnInnerChannelListenerCloseComplete(IAsyncResult result)
			{
				Exception exception = null;
				try
				{
					this.CompleteInnerChannelListenerClose(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				base.Complete(false, exception);
			}

			// Token: 0x06007FEE RID: 32750 RVA: 0x001DBF2C File Offset: 0x001DA12C
			private static void OnInnerChannelListenerCloseCompleteStatic(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					ReliableChannelListenerBase<TChannel>.OnReliableChannelCloseAsyncResult onReliableChannelCloseAsyncResult = (ReliableChannelListenerBase<TChannel>.OnReliableChannelCloseAsyncResult)result.AsyncState;
					onReliableChannelCloseAsyncResult.OnInnerChannelListenerCloseComplete(result);
				}
			}

			// Token: 0x0400491E RID: 18718
			private ReliableChannelListenerBase<TChannel> channelListener;

			// Token: 0x0400491F RID: 18719
			private UniqueId inputId;

			// Token: 0x04004920 RID: 18720
			private UniqueId outputId;

			// Token: 0x04004921 RID: 18721
			private static AsyncCallback onInnerChannelListenerCloseComplete = Fx.ThunkCallback(new AsyncCallback(ReliableChannelListenerBase<TChannel>.OnReliableChannelCloseAsyncResult.OnInnerChannelListenerCloseCompleteStatic));
		}
	}
}
