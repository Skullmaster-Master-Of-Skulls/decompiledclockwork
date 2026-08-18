using System;
using System.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000840 RID: 2112
	internal class NamedPipeReplyChannelListener : NamedPipeChannelListener<IReplyChannel, ReplyChannelAcceptor>, ISingletonChannelListener
	{
		// Token: 0x06004EF3 RID: 20211 RVA: 0x0011F92B File Offset: 0x0011DB2B
		public NamedPipeReplyChannelListener(NamedPipeTransportBindingElement bindingElement, BindingContext context) : base(bindingElement, context)
		{
			this.replyAcceptor = new ConnectionOrientedTransportChannelListener.ConnectionOrientedTransportReplyChannelAcceptor(this);
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x06004EF4 RID: 20212 RVA: 0x0011F941 File Offset: 0x0011DB41
		protected override ReplyChannelAcceptor ChannelAcceptor
		{
			get
			{
				return this.replyAcceptor;
			}
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x06004EF5 RID: 20213 RVA: 0x0011F949 File Offset: 0x0011DB49
		TimeSpan ISingletonChannelListener.ReceiveTimeout
		{
			get
			{
				return base.InternalReceiveTimeout;
			}
		}

		// Token: 0x06004EF6 RID: 20214 RVA: 0x0011F951 File Offset: 0x0011DB51
		void ISingletonChannelListener.ReceiveRequest(RequestContext requestContext, Action callback, bool canDispatchOnThisThread)
		{
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 262162, SR.GetString("TraceCodeNamedPipeChannelMessageReceived"), requestContext.RequestMessage);
			}
			this.replyAcceptor.Enqueue(requestContext, callback, canDispatchOnThisThread);
		}

		// Token: 0x0400310D RID: 12557
		private ReplyChannelAcceptor replyAcceptor;
	}
}
