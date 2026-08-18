using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A84 RID: 2692
	internal class MessageTransmitTraceRecord : MessageTraceRecord
	{
		// Token: 0x06006A36 RID: 27190 RVA: 0x0018C3EC File Offset: 0x0018A5EC
		private MessageTransmitTraceRecord(Message message) : base(message)
		{
		}

		// Token: 0x06006A37 RID: 27191 RVA: 0x0018C3F5 File Offset: 0x0018A5F5
		private MessageTransmitTraceRecord(Message message, string addressElementName) : this(message)
		{
			this.addressElementName = addressElementName;
		}

		// Token: 0x06006A38 RID: 27192 RVA: 0x0018C405 File Offset: 0x0018A605
		private MessageTransmitTraceRecord(Message message, string addressElementName, EndpointAddress address) : this(message, addressElementName)
		{
			if (address != null)
			{
				this.address = address.Uri;
			}
		}

		// Token: 0x06006A39 RID: 27193 RVA: 0x0018C424 File Offset: 0x0018A624
		private MessageTransmitTraceRecord(Message message, string addressElementName, Uri uri) : this(message, addressElementName)
		{
			this.address = uri;
		}

		// Token: 0x17001950 RID: 6480
		// (get) Token: 0x06006A3A RID: 27194 RVA: 0x0018C435 File Offset: 0x0018A635
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("MessageTransmit");
			}
		}

		// Token: 0x06006A3B RID: 27195 RVA: 0x0018C442 File Offset: 0x0018A642
		internal static MessageTransmitTraceRecord CreateSendTraceRecord(Message message, EndpointAddress address)
		{
			return new MessageTransmitTraceRecord(message, "RemoteAddress", address);
		}

		// Token: 0x06006A3C RID: 27196 RVA: 0x0018C450 File Offset: 0x0018A650
		internal static MessageTransmitTraceRecord CreateReceiveTraceRecord(Message message, Uri uri)
		{
			return new MessageTransmitTraceRecord(message, "LocalAddress", uri);
		}

		// Token: 0x06006A3D RID: 27197 RVA: 0x0018C45E File Offset: 0x0018A65E
		internal static MessageTransmitTraceRecord CreateReceiveTraceRecord(Message message, EndpointAddress address)
		{
			return new MessageTransmitTraceRecord(message, "LocalAddress", address);
		}

		// Token: 0x06006A3E RID: 27198 RVA: 0x0018C46C File Offset: 0x0018A66C
		internal static MessageTransmitTraceRecord CreateReceiveTraceRecord(Message message)
		{
			return new MessageTransmitTraceRecord(message);
		}

		// Token: 0x06006A3F RID: 27199 RVA: 0x0018C474 File Offset: 0x0018A674
		internal override void WriteTo(XmlWriter xml)
		{
			base.WriteTo(xml);
			if (this.address != null)
			{
				xml.WriteElementString(this.addressElementName, this.address.ToString());
			}
		}

		// Token: 0x04003CA4 RID: 15524
		private Uri address;

		// Token: 0x04003CA5 RID: 15525
		private string addressElementName;
	}
}
