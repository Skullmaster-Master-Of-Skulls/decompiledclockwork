using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000728 RID: 1832
	internal class InputSessionChannelDemuxer : SessionChannelDemuxer<IInputSessionChannel, Message>
	{
		// Token: 0x060045B7 RID: 17847 RVA: 0x0010541B File Offset: 0x0010361B
		public InputSessionChannelDemuxer(BindingContext context, TimeSpan peekTimeout, int maxPendingSessions) : base(context, peekTimeout, maxPendingSessions)
		{
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x00105426 File Offset: 0x00103626
		protected override void AbortItem(Message message)
		{
			TypedChannelDemuxer.AbortMessage(message);
		}

		// Token: 0x060045B9 RID: 17849 RVA: 0x0010542E File Offset: 0x0010362E
		protected override IAsyncResult BeginReceive(IInputSessionChannel channel, AsyncCallback callback, object state)
		{
			return channel.BeginReceive(callback, state);
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x00105438 File Offset: 0x00103638
		protected override IAsyncResult BeginReceive(IInputSessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return channel.BeginReceive(timeout, callback, state);
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x00105444 File Offset: 0x00103644
		protected override IInputSessionChannel CreateChannel(ChannelManagerBase channelManager, IInputSessionChannel innerChannel, Message firstMessage)
		{
			return new InputSessionChannelWrapper(channelManager, innerChannel, firstMessage);
		}

		// Token: 0x060045BC RID: 17852 RVA: 0x0010544E File Offset: 0x0010364E
		protected override void EndpointNotFound(IInputSessionChannel channel, Message message)
		{
			if (base.DemuxFailureHandler != null)
			{
				base.DemuxFailureHandler.HandleDemuxFailure(message);
			}
			this.AbortItem(message);
			channel.Abort();
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x00105471 File Offset: 0x00103671
		protected override Message EndReceive(IInputSessionChannel channel, IAsyncResult result)
		{
			return channel.EndReceive(result);
		}

		// Token: 0x060045BE RID: 17854 RVA: 0x0010547A File Offset: 0x0010367A
		protected override Message GetMessage(Message message)
		{
			return message;
		}
	}
}
