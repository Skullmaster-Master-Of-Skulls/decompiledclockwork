using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000722 RID: 1826
	internal class InputChannelDemuxer : DatagramChannelDemuxer<IInputChannel, Message>
	{
		// Token: 0x0600455F RID: 17759 RVA: 0x00103F3D File Offset: 0x0010213D
		public InputChannelDemuxer(BindingContext context) : base(context)
		{
		}

		// Token: 0x06004560 RID: 17760 RVA: 0x00103F46 File Offset: 0x00102146
		protected override void AbortItem(Message message)
		{
			TypedChannelDemuxer.AbortMessage(message);
		}

		// Token: 0x06004561 RID: 17761 RVA: 0x00103F4E File Offset: 0x0010214E
		protected override IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginReceive(timeout, callback, state);
		}

		// Token: 0x06004562 RID: 17762 RVA: 0x00103F60 File Offset: 0x00102160
		protected override LayeredChannelListener<IInputChannel> CreateListener<IInputChannel>(ChannelDemuxerFilter filter)
		{
			SingletonChannelListener<IInputChannel, InputChannel, Message> singletonChannelListener = new SingletonChannelListener<IInputChannel, InputChannel, Message>(filter, this);
			singletonChannelListener.Acceptor = (IChannelAcceptor<IInputChannel>)new InputChannelAcceptor(singletonChannelListener);
			return singletonChannelListener;
		}

		// Token: 0x06004563 RID: 17763 RVA: 0x00103F88 File Offset: 0x00102188
		protected override void Dispatch(IChannelListener listener)
		{
			SingletonChannelListener<IInputChannel, InputChannel, Message> singletonChannelListener = (SingletonChannelListener<IInputChannel, InputChannel, Message>)listener;
			singletonChannelListener.Dispatch();
		}

		// Token: 0x06004564 RID: 17764 RVA: 0x00103FA2 File Offset: 0x001021A2
		protected override void EndpointNotFound(Message message)
		{
			if (base.DemuxFailureHandler != null)
			{
				base.DemuxFailureHandler.HandleDemuxFailure(message);
			}
			this.AbortItem(message);
		}

		// Token: 0x06004565 RID: 17765 RVA: 0x00103FBF File Offset: 0x001021BF
		protected override Message EndReceive(IAsyncResult result)
		{
			return base.InnerChannel.EndReceive(result);
		}

		// Token: 0x06004566 RID: 17766 RVA: 0x00103FD0 File Offset: 0x001021D0
		protected override void EnqueueAndDispatch(IChannelListener listener, Message message, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			SingletonChannelListener<IInputChannel, InputChannel, Message> singletonChannelListener = (SingletonChannelListener<IInputChannel, InputChannel, Message>)listener;
			singletonChannelListener.EnqueueAndDispatch(message, dequeuedCallback, canDispatchOnThisThread);
		}

		// Token: 0x06004567 RID: 17767 RVA: 0x00103FF0 File Offset: 0x001021F0
		protected override void EnqueueAndDispatch(IChannelListener listener, Exception exception, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			SingletonChannelListener<IInputChannel, InputChannel, Message> singletonChannelListener = (SingletonChannelListener<IInputChannel, InputChannel, Message>)listener;
			singletonChannelListener.EnqueueAndDispatch(exception, dequeuedCallback, canDispatchOnThisThread);
		}

		// Token: 0x06004568 RID: 17768 RVA: 0x0010400E File Offset: 0x0010220E
		protected override Message GetMessage(Message message)
		{
			return message;
		}
	}
}
