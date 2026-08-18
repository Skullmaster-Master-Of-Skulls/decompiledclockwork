using System;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000057 RID: 87
	internal class UdpDiscoveryMessageFilter : MessageFilter
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x0000CAC3 File Offset: 0x0000ACC3
		public UdpDiscoveryMessageFilter(MessageFilter innerFilter)
		{
			if (innerFilter == null)
			{
				throw FxTrace.Exception.ArgumentNull("innerFilter");
			}
			this.innerFilter = innerFilter;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000CAE5 File Offset: 0x0000ACE5
		public MessageFilter InnerFilter
		{
			get
			{
				return this.innerFilter;
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000CAF0 File Offset: 0x0000ACF0
		public override bool Match(Message message)
		{
			if (message == null)
			{
				throw FxTrace.Exception.ArgumentNull("message");
			}
			if (this.InnerFilter.Match(message))
			{
				bool flag = message.Headers.ReplyTo == null || message.Headers.ReplyTo.IsAnonymous;
				if (!flag && TD.DiscoveryMessageWithInvalidReplyToIsEnabled())
				{
					EventTraceActivity eventTraceActivity = null;
					if (Fx.Trace.IsEtwProviderEnabled)
					{
						eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
					}
					TD.DiscoveryMessageWithInvalidReplyTo(eventTraceActivity, message.Headers.MessageId.ToString());
				}
				return flag;
			}
			return false;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000CB7D File Offset: 0x0000AD7D
		public override bool Match(MessageBuffer buffer)
		{
			if (buffer == null)
			{
				throw FxTrace.Exception.ArgumentNull("buffer");
			}
			return this.Match(buffer.CreateMessage());
		}

		// Token: 0x0400010F RID: 271
		private MessageFilter innerFilter;
	}
}
