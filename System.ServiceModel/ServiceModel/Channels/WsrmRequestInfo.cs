using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000974 RID: 2420
	internal abstract class WsrmRequestInfo
	{
		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x06005DCC RID: 24012 RVA: 0x0015AAE9 File Offset: 0x00158CE9
		public UniqueId MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x06005DCD RID: 24013 RVA: 0x0015AAF1 File Offset: 0x00158CF1
		public EndpointAddress ReplyTo
		{
			get
			{
				return this.replyTo;
			}
		}

		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x06005DCE RID: 24014
		public abstract string RequestName { get; }

		// Token: 0x06005DCF RID: 24015 RVA: 0x0015AAFC File Offset: 0x00158CFC
		protected void SetMessageId(MessageVersion messageVersion, MessageHeaders headers)
		{
			this.messageId = headers.MessageId;
			if (this.messageId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageHeaderException(SR.GetString("MissingMessageIdOnWsrmRequest", new object[]
				{
					this.RequestName
				}), messageVersion.Addressing.Namespace, "MessageID", false));
			}
		}

		// Token: 0x06005DD0 RID: 24016 RVA: 0x0015AB60 File Offset: 0x00158D60
		protected void SetReplyTo(MessageVersion messageVersion, MessageHeaders headers)
		{
			this.replyTo = headers.ReplyTo;
			if (messageVersion.Addressing == AddressingVersion.WSAddressing10 && this.replyTo == null)
			{
				this.replyTo = EndpointAddress.AnonymousAddress;
			}
			if (this.replyTo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageHeaderException(SR.GetString("MissingReplyToOnWsrmRequest", new object[]
				{
					this.RequestName
				}), messageVersion.Addressing.Namespace, "ReplyTo", false));
			}
		}

		// Token: 0x040037AE RID: 14254
		private UniqueId messageId;

		// Token: 0x040037AF RID: 14255
		private EndpointAddress replyTo;
	}
}
