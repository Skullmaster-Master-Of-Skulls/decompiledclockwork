using System;
using System.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000856 RID: 2134
	internal class TcpReplyChannelListener : TcpChannelListener<IReplyChannel, ReplyChannelAcceptor>, ISingletonChannelListener
	{
		// Token: 0x0600500F RID: 20495 RVA: 0x00125BD7 File Offset: 0x00123DD7
		public TcpReplyChannelListener(TcpTransportBindingElement bindingElement, BindingContext context) : base(bindingElement, context)
		{
			this.replyAcceptor = new ConnectionOrientedTransportChannelListener.ConnectionOrientedTransportReplyChannelAcceptor(this);
		}

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x06005010 RID: 20496 RVA: 0x00125BED File Offset: 0x00123DED
		protected override ReplyChannelAcceptor ChannelAcceptor
		{
			get
			{
				return this.replyAcceptor;
			}
		}

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x06005011 RID: 20497 RVA: 0x00125BF5 File Offset: 0x00123DF5
		TimeSpan ISingletonChannelListener.ReceiveTimeout
		{
			get
			{
				return base.InternalReceiveTimeout;
			}
		}

		// Token: 0x06005012 RID: 20498 RVA: 0x00125BFD File Offset: 0x00123DFD
		void ISingletonChannelListener.ReceiveRequest(RequestContext requestContext, Action callback, bool canDispatchOnThisThread)
		{
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 262167, SR.GetString("TraceCodeTcpChannelMessageReceived"), requestContext.RequestMessage);
			}
			this.replyAcceptor.Enqueue(requestContext, callback, canDispatchOnThisThread);
		}

		// Token: 0x04003199 RID: 12697
		private ReplyChannelAcceptor replyAcceptor;
	}
}
