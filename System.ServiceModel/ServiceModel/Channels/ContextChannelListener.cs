using System;
using System.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007AA RID: 1962
	internal class ContextChannelListener<TChannel> : LayeredChannelListener<TChannel> where TChannel : class, IChannel
	{
		// Token: 0x06004A3A RID: 19002 RVA: 0x001111AC File Offset: 0x0010F3AC
		public ContextChannelListener(BindingContext context, ContextExchangeMechanism contextExchangeMechanism) : base((context == null) ? null : context.Binding, (context == null) ? null : context.BuildInnerChannelListener<TChannel>())
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (!ContextExchangeMechanismHelper.IsDefined(contextExchangeMechanism))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("contextExchangeMechanism"));
			}
			this.contextExchangeMechanism = contextExchangeMechanism;
			this.listenBaseAddress = context.ListenUriBaseAddress;
		}

		// Token: 0x06004A3B RID: 19003 RVA: 0x0011121A File Offset: 0x0010F41A
		protected override TChannel OnAcceptChannel(TimeSpan timeout)
		{
			return this.InternalAcceptChannel(((IChannelListener<TChannel>)this.InnerChannelListener).AcceptChannel(timeout));
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x00111233 File Offset: 0x0010F433
		protected override IAsyncResult OnBeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return ((IChannelListener<TChannel>)this.InnerChannelListener).BeginAcceptChannel(timeout, callback, state);
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x00111248 File Offset: 0x0010F448
		protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.InnerChannelListener.BeginWaitForChannel(timeout, callback, state);
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x00111258 File Offset: 0x0010F458
		protected override TChannel OnEndAcceptChannel(IAsyncResult result)
		{
			return this.InternalAcceptChannel(((IChannelListener<TChannel>)this.InnerChannelListener).EndAcceptChannel(result));
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x00111271 File Offset: 0x0010F471
		protected override bool OnEndWaitForChannel(IAsyncResult result)
		{
			return this.InnerChannelListener.EndWaitForChannel(result);
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x0011127F File Offset: 0x0010F47F
		protected override bool OnWaitForChannel(TimeSpan timeout)
		{
			return this.InnerChannelListener.WaitForChannel(timeout);
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x00111290 File Offset: 0x0010F490
		private TChannel InternalAcceptChannel(TChannel innerChannel)
		{
			if (innerChannel == null)
			{
				return innerChannel;
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983044, SR.GetString("TraceCodeContextChannelListenerChannelAccepted"), this);
			}
			if (typeof(TChannel) == typeof(IInputChannel))
			{
				return (TChannel)((object)new ContextInputChannel(this, (IInputChannel)((object)innerChannel), this.contextExchangeMechanism));
			}
			if (typeof(TChannel) == typeof(IInputSessionChannel))
			{
				return (TChannel)((object)new ContextInputSessionChannel(this, (IInputSessionChannel)((object)innerChannel), this.contextExchangeMechanism));
			}
			if (typeof(TChannel) == typeof(IReplyChannel))
			{
				return (TChannel)((object)new ContextReplyChannel(this, (IReplyChannel)((object)innerChannel), this.contextExchangeMechanism));
			}
			if (typeof(TChannel) == typeof(IReplySessionChannel))
			{
				return (TChannel)((object)new ContextReplySessionChannel(this, (IReplySessionChannel)((object)innerChannel), this.contextExchangeMechanism));
			}
			return (TChannel)((object)new ContextDuplexSessionChannel(this, (IDuplexSessionChannel)((object)innerChannel), this.contextExchangeMechanism));
		}

		// Token: 0x04002F08 RID: 12040
		private ContextExchangeMechanism contextExchangeMechanism;

		// Token: 0x04002F09 RID: 12041
		private Uri listenBaseAddress;
	}
}
