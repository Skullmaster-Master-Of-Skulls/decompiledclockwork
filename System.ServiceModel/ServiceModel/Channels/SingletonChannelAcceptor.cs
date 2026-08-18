using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200076E RID: 1902
	internal abstract class SingletonChannelAcceptor<ChannelInterfaceType, TChannel, QueueItemType> : InputQueueChannelAcceptor<ChannelInterfaceType> where ChannelInterfaceType : class, IChannel where TChannel : InputQueueChannel<QueueItemType> where QueueItemType : class, IDisposable
	{
		// Token: 0x060048A4 RID: 18596 RVA: 0x0010C660 File Offset: 0x0010A860
		public SingletonChannelAcceptor(ChannelManagerBase channelManager) : base(channelManager)
		{
		}

		// Token: 0x060048A5 RID: 18597 RVA: 0x0010C674 File Offset: 0x0010A874
		public override ChannelInterfaceType AcceptChannel(TimeSpan timeout)
		{
			this.EnsureChannelAvailable();
			return base.AcceptChannel(timeout);
		}

		// Token: 0x060048A6 RID: 18598 RVA: 0x0010C684 File Offset: 0x0010A884
		public override IAsyncResult BeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.EnsureChannelAvailable();
			return base.BeginAcceptChannel(timeout, callback, state);
		}

		// Token: 0x060048A7 RID: 18599 RVA: 0x0010C696 File Offset: 0x0010A896
		protected TChannel GetCurrentChannel()
		{
			return this.currentChannel;
		}

		// Token: 0x060048A8 RID: 18600 RVA: 0x0010C6A0 File Offset: 0x0010A8A0
		private TChannel EnsureChannelAvailable()
		{
			bool flag = false;
			TChannel tchannel;
			if ((tchannel = this.currentChannel) == null)
			{
				object obj = this.currentChannelLock;
				lock (obj)
				{
					if (base.IsDisposed)
					{
						return default(TChannel);
					}
					if ((tchannel = this.currentChannel) == null)
					{
						tchannel = this.OnCreateChannel();
						tchannel.Closed += this.OnChannelClosed;
						this.currentChannel = tchannel;
						flag = true;
					}
				}
			}
			if (flag)
			{
				base.EnqueueAndDispatch((ChannelInterfaceType)((object)tchannel));
			}
			return tchannel;
		}

		// Token: 0x060048A9 RID: 18601
		protected abstract TChannel OnCreateChannel();

		// Token: 0x060048AA RID: 18602
		protected abstract void OnTraceMessageReceived(QueueItemType item);

		// Token: 0x060048AB RID: 18603 RVA: 0x0010C750 File Offset: 0x0010A950
		public void DispatchItems()
		{
			TChannel tchannel = this.EnsureChannelAvailable();
			if (tchannel != null)
			{
				tchannel.Dispatch();
			}
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x0010C777 File Offset: 0x0010A977
		public void Enqueue(QueueItemType item)
		{
			this.Enqueue(item, null);
		}

		// Token: 0x060048AD RID: 18605 RVA: 0x0010C781 File Offset: 0x0010A981
		public void Enqueue(QueueItemType item, Action dequeuedCallback)
		{
			this.Enqueue(item, dequeuedCallback, true);
		}

		// Token: 0x060048AE RID: 18606 RVA: 0x0010C78C File Offset: 0x0010A98C
		public void Enqueue(QueueItemType item, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			TChannel tchannel = this.EnsureChannelAvailable();
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				this.OnTraceMessageReceived(item);
			}
			if (tchannel != null)
			{
				tchannel.EnqueueAndDispatch(item, dequeuedCallback, canDispatchOnThisThread);
				return;
			}
			SingletonChannelAcceptor<ChannelInterfaceType, TChannel, QueueItemType>.InvokeDequeuedCallback(dequeuedCallback, canDispatchOnThisThread);
			item.Dispose();
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x0010C7D7 File Offset: 0x0010A9D7
		public void Enqueue(Exception exception, Action dequeuedCallback)
		{
			this.Enqueue(exception, dequeuedCallback, true);
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x0010C7E4 File Offset: 0x0010A9E4
		public void Enqueue(Exception exception, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			TChannel tchannel = this.EnsureChannelAvailable();
			if (tchannel != null)
			{
				tchannel.EnqueueAndDispatch(exception, dequeuedCallback, canDispatchOnThisThread);
				return;
			}
			SingletonChannelAcceptor<ChannelInterfaceType, TChannel, QueueItemType>.InvokeDequeuedCallback(dequeuedCallback, canDispatchOnThisThread);
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x0010C818 File Offset: 0x0010AA18
		public bool EnqueueWithoutDispatch(QueueItemType item, Action dequeuedCallback)
		{
			TChannel tchannel = this.EnsureChannelAvailable();
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				this.OnTraceMessageReceived(item);
			}
			if (tchannel != null)
			{
				return tchannel.EnqueueWithoutDispatch(item, dequeuedCallback);
			}
			SingletonChannelAcceptor<ChannelInterfaceType, TChannel, QueueItemType>.InvokeDequeuedCallback(dequeuedCallback, false);
			item.Dispose();
			return false;
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x0010C864 File Offset: 0x0010AA64
		public override bool EnqueueWithoutDispatch(Exception exception, Action dequeuedCallback)
		{
			TChannel tchannel = this.EnsureChannelAvailable();
			if (tchannel != null)
			{
				return tchannel.EnqueueWithoutDispatch(exception, dequeuedCallback);
			}
			SingletonChannelAcceptor<ChannelInterfaceType, TChannel, QueueItemType>.InvokeDequeuedCallback(dequeuedCallback, false);
			return false;
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x0010C898 File Offset: 0x0010AA98
		public void EnqueueAndDispatch(QueueItemType item, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			TChannel tchannel = this.EnsureChannelAvailable();
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				this.OnTraceMessageReceived(item);
			}
			if (tchannel != null)
			{
				tchannel.EnqueueAndDispatch(item, dequeuedCallback, canDispatchOnThisThread);
				return;
			}
			SingletonChannelAcceptor<ChannelInterfaceType, TChannel, QueueItemType>.InvokeDequeuedCallback(dequeuedCallback, canDispatchOnThisThread);
			item.Dispose();
		}

		// Token: 0x060048B4 RID: 18612 RVA: 0x0010C8E4 File Offset: 0x0010AAE4
		public override void EnqueueAndDispatch(Exception exception, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			TChannel tchannel = this.EnsureChannelAvailable();
			if (tchannel != null)
			{
				tchannel.EnqueueAndDispatch(exception, dequeuedCallback, canDispatchOnThisThread);
				return;
			}
			SingletonChannelAcceptor<ChannelInterfaceType, TChannel, QueueItemType>.InvokeDequeuedCallback(dequeuedCallback, canDispatchOnThisThread);
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x0010C918 File Offset: 0x0010AB18
		protected void OnChannelClosed(object sender, EventArgs args)
		{
			IChannel channel = (IChannel)sender;
			object obj = this.currentChannelLock;
			lock (obj)
			{
				if (channel == this.currentChannel)
				{
					this.currentChannel = default(TChannel);
				}
			}
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x0010C974 File Offset: 0x0010AB74
		private static void InvokeDequeuedCallback(Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			if (dequeuedCallback != null)
			{
				if (canDispatchOnThisThread)
				{
					dequeuedCallback();
					return;
				}
				if (SingletonChannelAcceptor<ChannelInterfaceType, TChannel, QueueItemType>.onInvokeDequeuedCallback == null)
				{
					SingletonChannelAcceptor<ChannelInterfaceType, TChannel, QueueItemType>.onInvokeDequeuedCallback = new Action<object>(SingletonChannelAcceptor<ChannelInterfaceType, TChannel, QueueItemType>.OnInvokeDequeuedCallback);
				}
				ActionItem.Schedule(SingletonChannelAcceptor<ChannelInterfaceType, TChannel, QueueItemType>.onInvokeDequeuedCallback, dequeuedCallback);
			}
		}

		// Token: 0x060048B7 RID: 18615 RVA: 0x0010C9A8 File Offset: 0x0010ABA8
		private static void OnInvokeDequeuedCallback(object state)
		{
			Action action = (Action)state;
			action();
		}

		// Token: 0x04002DF6 RID: 11766
		private TChannel currentChannel;

		// Token: 0x04002DF7 RID: 11767
		private object currentChannelLock = new object();

		// Token: 0x04002DF8 RID: 11768
		private static Action<object> onInvokeDequeuedCallback;
	}
}
