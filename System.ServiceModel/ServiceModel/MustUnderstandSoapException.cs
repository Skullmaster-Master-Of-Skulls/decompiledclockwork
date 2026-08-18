using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200010D RID: 269
	[Serializable]
	internal class MustUnderstandSoapException : CommunicationException
	{
		// Token: 0x06000631 RID: 1585 RVA: 0x0001B4A1 File Offset: 0x000196A1
		public MustUnderstandSoapException()
		{
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001B4A9 File Offset: 0x000196A9
		protected MustUnderstandSoapException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001B4B3 File Offset: 0x000196B3
		public MustUnderstandSoapException(Collection<MessageHeaderInfo> notUnderstoodHeaders, EnvelopeVersion envelopeVersion)
		{
			this.notUnderstoodHeaders = notUnderstoodHeaders;
			this.envelopeVersion = envelopeVersion;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001B4C9 File Offset: 0x000196C9
		public Collection<MessageHeaderInfo> NotUnderstoodHeaders
		{
			get
			{
				return this.notUnderstoodHeaders;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001B4D1 File Offset: 0x000196D1
		public EnvelopeVersion EnvelopeVersion
		{
			get
			{
				return this.envelopeVersion;
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001B4DC File Offset: 0x000196DC
		internal Message ProvideFault(MessageVersion messageVersion)
		{
			string name = this.notUnderstoodHeaders[0].Name;
			string @namespace = this.notUnderstoodHeaders[0].Namespace;
			FaultCode code = new FaultCode("MustUnderstand", this.envelopeVersion.Namespace);
			FaultReason reason = new FaultReason(SR.GetString("SFxHeaderNotUnderstood", new object[]
			{
				name,
				@namespace
			}), CultureInfo.CurrentCulture);
			MessageFault fault = MessageFault.CreateFault(code, reason);
			string defaultFaultAction = messageVersion.Addressing.DefaultFaultAction;
			Message message = System.ServiceModel.Channels.Message.CreateMessage(messageVersion, fault, defaultFaultAction);
			if (this.envelopeVersion == EnvelopeVersion.Soap12)
			{
				this.AddNotUnderstoodHeaders(message.Headers);
			}
			return message;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001B584 File Offset: 0x00019784
		private void AddNotUnderstoodHeaders(MessageHeaders headers)
		{
			for (int i = 0; i < this.notUnderstoodHeaders.Count; i++)
			{
				headers.Add(new MustUnderstandSoapException.NotUnderstoodHeader(this.notUnderstoodHeaders[i].Name, this.notUnderstoodHeaders[i].Namespace));
			}
		}

		// Token: 0x04000A67 RID: 2663
		private Collection<MessageHeaderInfo> notUnderstoodHeaders;

		// Token: 0x04000A68 RID: 2664
		private EnvelopeVersion envelopeVersion;

		// Token: 0x02000AE3 RID: 2787
		private class NotUnderstoodHeader : MessageHeader
		{
			// Token: 0x06006EB4 RID: 28340 RVA: 0x0019C85A File Offset: 0x0019AA5A
			public NotUnderstoodHeader(string name, string ns)
			{
				this.notUnderstoodName = name;
				this.notUnderstoodNs = ns;
			}

			// Token: 0x170019D2 RID: 6610
			// (get) Token: 0x06006EB5 RID: 28341 RVA: 0x0019C870 File Offset: 0x0019AA70
			public override string Name
			{
				get
				{
					return "NotUnderstood";
				}
			}

			// Token: 0x170019D3 RID: 6611
			// (get) Token: 0x06006EB6 RID: 28342 RVA: 0x0019C877 File Offset: 0x0019AA77
			public override string Namespace
			{
				get
				{
					return "http://www.w3.org/2003/05/soap-envelope";
				}
			}

			// Token: 0x06006EB7 RID: 28343 RVA: 0x0019C880 File Offset: 0x0019AA80
			protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
			{
				writer.WriteStartElement(this.Name, this.Namespace);
				writer.WriteXmlnsAttribute(null, this.notUnderstoodNs);
				writer.WriteStartAttribute("qname");
				writer.WriteQualifiedName(this.notUnderstoodName, this.notUnderstoodNs);
				writer.WriteEndAttribute();
			}

			// Token: 0x06006EB8 RID: 28344 RVA: 0x0019C8CF File Offset: 0x0019AACF
			protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
			{
			}

			// Token: 0x04003F29 RID: 16169
			private string notUnderstoodName;

			// Token: 0x04003F2A RID: 16170
			private string notUnderstoodNs;
		}
	}
}
