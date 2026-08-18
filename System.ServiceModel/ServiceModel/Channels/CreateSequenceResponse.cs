using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200090B RID: 2315
	internal sealed class CreateSequenceResponse : BodyWriter
	{
		// Token: 0x06005853 RID: 22611 RVA: 0x00144A6B File Offset: 0x00142C6B
		private CreateSequenceResponse() : base(true)
		{
		}

		// Token: 0x06005854 RID: 22612 RVA: 0x00144A74 File Offset: 0x00142C74
		public CreateSequenceResponse(AddressingVersion addressingVersion, ReliableMessagingVersion reliableMessagingVersion) : base(true)
		{
			this.addressingVersion = addressingVersion;
			this.reliableMessagingVersion = reliableMessagingVersion;
		}

		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x06005855 RID: 22613 RVA: 0x00144A8B File Offset: 0x00142C8B
		// (set) Token: 0x06005856 RID: 22614 RVA: 0x00144A93 File Offset: 0x00142C93
		public EndpointAddress AcceptAcksTo
		{
			get
			{
				return this.acceptAcksTo;
			}
			set
			{
				this.acceptAcksTo = value;
			}
		}

		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x06005857 RID: 22615 RVA: 0x00144A9C File Offset: 0x00142C9C
		// (set) Token: 0x06005858 RID: 22616 RVA: 0x00144AA4 File Offset: 0x00142CA4
		public TimeSpan? Expires
		{
			get
			{
				return this.expires;
			}
			set
			{
				this.expires = value;
			}
		}

		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x06005859 RID: 22617 RVA: 0x00144AAD File Offset: 0x00142CAD
		// (set) Token: 0x0600585A RID: 22618 RVA: 0x00144AB5 File Offset: 0x00142CB5
		public UniqueId Identifier
		{
			get
			{
				return this.identifier;
			}
			set
			{
				this.identifier = value;
			}
		}

		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x0600585B RID: 22619 RVA: 0x00144ABE File Offset: 0x00142CBE
		// (set) Token: 0x0600585C RID: 22620 RVA: 0x00144AC6 File Offset: 0x00142CC6
		public bool Ordered
		{
			get
			{
				return this.ordered;
			}
			set
			{
				this.ordered = value;
			}
		}

		// Token: 0x0600585D RID: 22621 RVA: 0x00144AD0 File Offset: 0x00142CD0
		public static CreateSequenceResponseInfo Create(AddressingVersion addressingVersion, ReliableMessagingVersion reliableMessagingVersion, XmlDictionaryReader reader)
		{
			CreateSequenceResponseInfo createSequenceResponseInfo = new CreateSequenceResponseInfo();
			WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
			XmlDictionaryString @namespace = WsrmIndex.GetNamespace(reliableMessagingVersion);
			reader.ReadStartElement(wsrmFeb2005Dictionary.CreateSequenceResponse, @namespace);
			reader.ReadStartElement(wsrmFeb2005Dictionary.Identifier, @namespace);
			createSequenceResponseInfo.Identifier = reader.ReadContentAsUniqueId();
			reader.ReadEndElement();
			if (reader.IsStartElement(wsrmFeb2005Dictionary.Expires, @namespace))
			{
				reader.ReadElementContentAsTimeSpan();
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11 && reader.IsStartElement(DXD.Wsrm11Dictionary.IncompleteSequenceBehavior, @namespace))
			{
				string a = reader.ReadElementContentAsString();
				if (a != "DiscardEntireSequence" && a != "DiscardFollowingFirstGap" && a != "NoDiscard")
				{
					string @string = SR.GetString("CSResponseWithInvalidIncompleteSequenceBehavior");
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(@string));
				}
			}
			if (reader.IsStartElement(wsrmFeb2005Dictionary.Accept, @namespace))
			{
				reader.ReadStartElement();
				createSequenceResponseInfo.AcceptAcksTo = EndpointAddress.ReadFrom(addressingVersion, reader, wsrmFeb2005Dictionary.AcksTo, @namespace);
				while (reader.IsStartElement())
				{
					reader.Skip();
				}
				reader.ReadEndElement();
			}
			while (reader.IsStartElement())
			{
				reader.Skip();
			}
			reader.ReadEndElement();
			return createSequenceResponseInfo;
		}

		// Token: 0x0600585E RID: 22622 RVA: 0x00144BF4 File Offset: 0x00142DF4
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
			XmlDictionaryString @namespace = WsrmIndex.GetNamespace(this.reliableMessagingVersion);
			writer.WriteStartElement(wsrmFeb2005Dictionary.CreateSequenceResponse, @namespace);
			writer.WriteStartElement(wsrmFeb2005Dictionary.Identifier, @namespace);
			writer.WriteValue(this.identifier);
			writer.WriteEndElement();
			if (this.expires != null)
			{
				writer.WriteStartElement(wsrmFeb2005Dictionary.Expires, @namespace);
				writer.WriteValue(this.expires.Value);
				writer.WriteEndElement();
			}
			if (this.reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				Wsrm11Dictionary wsrm11Dictionary = DXD.Wsrm11Dictionary;
				writer.WriteStartElement(wsrm11Dictionary.IncompleteSequenceBehavior, @namespace);
				writer.WriteValue(this.ordered ? wsrm11Dictionary.DiscardFollowingFirstGap : wsrm11Dictionary.NoDiscard);
				writer.WriteEndElement();
			}
			if (this.acceptAcksTo != null)
			{
				writer.WriteStartElement(wsrmFeb2005Dictionary.Accept, @namespace);
				this.acceptAcksTo.WriteTo(this.addressingVersion, writer, wsrmFeb2005Dictionary.AcksTo, @namespace);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x04003628 RID: 13864
		private EndpointAddress acceptAcksTo;

		// Token: 0x04003629 RID: 13865
		private AddressingVersion addressingVersion;

		// Token: 0x0400362A RID: 13866
		private TimeSpan? expires;

		// Token: 0x0400362B RID: 13867
		private UniqueId identifier;

		// Token: 0x0400362C RID: 13868
		private bool ordered;

		// Token: 0x0400362D RID: 13869
		private ReliableMessagingVersion reliableMessagingVersion;
	}
}
