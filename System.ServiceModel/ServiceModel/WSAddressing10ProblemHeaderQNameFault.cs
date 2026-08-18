using System;
using System.Globalization;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000110 RID: 272
	internal class WSAddressing10ProblemHeaderQNameFault : MessageFault
	{
		// Token: 0x06000676 RID: 1654 RVA: 0x0001BDD4 File Offset: 0x00019FD4
		public WSAddressing10ProblemHeaderQNameFault(MessageHeaderException e)
		{
			this.invalidHeaderName = e.HeaderName;
			if (e.IsDuplicate)
			{
				this.code = FaultCode.CreateSenderFaultCode(new FaultCode("InvalidAddressingHeader", AddressingVersion.WSAddressing10.Namespace, new FaultCode("InvalidCardinality", AddressingVersion.WSAddressing10.Namespace)));
			}
			else
			{
				this.code = FaultCode.CreateSenderFaultCode(new FaultCode("MessageAddressingHeaderRequired", AddressingVersion.WSAddressing10.Namespace));
			}
			this.reason = new FaultReason(e.Message, CultureInfo.CurrentCulture);
			this.actor = "";
			this.node = "";
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001BE7C File Offset: 0x0001A07C
		public WSAddressing10ProblemHeaderQNameFault(ActionMismatchAddressingException e)
		{
			this.invalidHeaderName = "Action";
			this.code = FaultCode.CreateSenderFaultCode(new FaultCode("ActionMismatch", AddressingVersion.WSAddressing10.Namespace));
			this.reason = new FaultReason(e.Message, CultureInfo.CurrentCulture);
			this.actor = "";
			this.node = "";
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0001BEE5 File Offset: 0x0001A0E5
		public override string Actor
		{
			get
			{
				return this.actor;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001BEED File Offset: 0x0001A0ED
		public override FaultCode Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0001BEF5 File Offset: 0x0001A0F5
		public override bool HasDetail
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001BEF8 File Offset: 0x0001A0F8
		public override string Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001BF00 File Offset: 0x0001A100
		public override FaultReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001BF08 File Offset: 0x0001A108
		protected override void OnWriteDetail(XmlDictionaryWriter writer, EnvelopeVersion version)
		{
			if (version == EnvelopeVersion.Soap12)
			{
				this.OnWriteStartDetail(writer, version);
				this.OnWriteDetailContents(writer);
				writer.WriteEndElement();
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001BF27 File Offset: 0x0001A127
		protected override void OnWriteDetailContents(XmlDictionaryWriter writer)
		{
			writer.WriteStartElement("ProblemHeaderQName", AddressingVersion.WSAddressing10.Namespace);
			writer.WriteQualifiedName(this.invalidHeaderName, AddressingVersion.WSAddressing10.Namespace);
			writer.WriteEndElement();
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001BF5A File Offset: 0x0001A15A
		public void AddHeaders(MessageHeaders headers)
		{
			if (headers.MessageVersion.Envelope == EnvelopeVersion.Soap11)
			{
				headers.Add(new WSAddressing10ProblemHeaderQNameFault.WSAddressing10ProblemHeaderQNameHeader(this.invalidHeaderName));
			}
		}

		// Token: 0x04000A84 RID: 2692
		private FaultCode code;

		// Token: 0x04000A85 RID: 2693
		private FaultReason reason;

		// Token: 0x04000A86 RID: 2694
		private string actor;

		// Token: 0x04000A87 RID: 2695
		private string node;

		// Token: 0x04000A88 RID: 2696
		private string invalidHeaderName;

		// Token: 0x02000AE5 RID: 2789
		private class WSAddressing10ProblemHeaderQNameHeader : MessageHeader
		{
			// Token: 0x06006EBC RID: 28348 RVA: 0x0019C8EA File Offset: 0x0019AAEA
			public WSAddressing10ProblemHeaderQNameHeader(string invalidHeaderName)
			{
				this.invalidHeaderName = invalidHeaderName;
			}

			// Token: 0x170019D5 RID: 6613
			// (get) Token: 0x06006EBD RID: 28349 RVA: 0x0019C8F9 File Offset: 0x0019AAF9
			public override string Name
			{
				get
				{
					return "FaultDetail";
				}
			}

			// Token: 0x170019D6 RID: 6614
			// (get) Token: 0x06006EBE RID: 28350 RVA: 0x0019C900 File Offset: 0x0019AB00
			public override string Namespace
			{
				get
				{
					return AddressingVersion.WSAddressing10.Namespace;
				}
			}

			// Token: 0x06006EBF RID: 28351 RVA: 0x0019C90C File Offset: 0x0019AB0C
			protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
			{
				writer.WriteStartElement(this.Name, this.Namespace);
			}

			// Token: 0x06006EC0 RID: 28352 RVA: 0x0019C920 File Offset: 0x0019AB20
			protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
			{
				writer.WriteStartElement("ProblemHeaderQName", this.Namespace);
				writer.WriteQualifiedName(this.invalidHeaderName, this.Namespace);
				writer.WriteEndElement();
			}

			// Token: 0x04003F2C RID: 16172
			private string invalidHeaderName;
		}
	}
}
