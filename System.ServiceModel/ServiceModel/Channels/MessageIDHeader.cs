using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009AC RID: 2476
	internal class MessageIDHeader : AddressingHeader
	{
		// Token: 0x06006112 RID: 24850 RVA: 0x0016A4C0 File Offset: 0x001686C0
		private MessageIDHeader(UniqueId messageId, AddressingVersion version) : base(version)
		{
			this.messageId = messageId;
		}

		// Token: 0x1700175D RID: 5981
		// (get) Token: 0x06006113 RID: 24851 RVA: 0x0016A4D0 File Offset: 0x001686D0
		public override XmlDictionaryString DictionaryName
		{
			get
			{
				return XD.AddressingDictionary.MessageId;
			}
		}

		// Token: 0x1700175E RID: 5982
		// (get) Token: 0x06006114 RID: 24852 RVA: 0x0016A4DC File Offset: 0x001686DC
		public UniqueId MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x1700175F RID: 5983
		// (get) Token: 0x06006115 RID: 24853 RVA: 0x0016A4E4 File Offset: 0x001686E4
		public override bool MustUnderstand
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006116 RID: 24854 RVA: 0x0016A4E7 File Offset: 0x001686E7
		public static MessageIDHeader Create(UniqueId messageId, AddressingVersion addressingVersion)
		{
			if (messageId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("messageId"));
			}
			if (addressingVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("addressingVersion"));
			}
			return new MessageIDHeader(messageId, addressingVersion);
		}

		// Token: 0x06006117 RID: 24855 RVA: 0x0016A520 File Offset: 0x00168720
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			writer.WriteValue(this.messageId);
		}

		// Token: 0x06006118 RID: 24856 RVA: 0x0016A52E File Offset: 0x0016872E
		public static UniqueId ReadHeaderValue(XmlDictionaryReader reader, AddressingVersion version)
		{
			return reader.ReadElementContentAsUniqueId();
		}

		// Token: 0x06006119 RID: 24857 RVA: 0x0016A538 File Offset: 0x00168738
		public static MessageIDHeader ReadHeader(XmlDictionaryReader reader, AddressingVersion version, string actor, bool mustUnderstand, bool relay)
		{
			UniqueId uniqueId = MessageIDHeader.ReadHeaderValue(reader, version);
			if (actor.Length == 0 && !mustUnderstand && !relay)
			{
				return new MessageIDHeader(uniqueId, version);
			}
			return new MessageIDHeader.FullMessageIDHeader(uniqueId, actor, mustUnderstand, relay, version);
		}

		// Token: 0x040038BA RID: 14522
		private UniqueId messageId;

		// Token: 0x040038BB RID: 14523
		private const bool mustUnderstandValue = false;

		// Token: 0x02000E3B RID: 3643
		private class FullMessageIDHeader : MessageIDHeader
		{
			// Token: 0x060082A3 RID: 33443 RVA: 0x001E31B2 File Offset: 0x001E13B2
			public FullMessageIDHeader(UniqueId messageId, string actor, bool mustUnderstand, bool relay, AddressingVersion version) : base(messageId, version)
			{
				this.actor = actor;
				this.mustUnderstand = mustUnderstand;
				this.relay = relay;
			}

			// Token: 0x17001CD8 RID: 7384
			// (get) Token: 0x060082A4 RID: 33444 RVA: 0x001E31D3 File Offset: 0x001E13D3
			public override string Actor
			{
				get
				{
					return this.actor;
				}
			}

			// Token: 0x17001CD9 RID: 7385
			// (get) Token: 0x060082A5 RID: 33445 RVA: 0x001E31DB File Offset: 0x001E13DB
			public override bool MustUnderstand
			{
				get
				{
					return this.mustUnderstand;
				}
			}

			// Token: 0x17001CDA RID: 7386
			// (get) Token: 0x060082A6 RID: 33446 RVA: 0x001E31E3 File Offset: 0x001E13E3
			public override bool Relay
			{
				get
				{
					return this.relay;
				}
			}

			// Token: 0x04004A2B RID: 18987
			private string actor;

			// Token: 0x04004A2C RID: 18988
			private bool mustUnderstand;

			// Token: 0x04004A2D RID: 18989
			private bool relay;
		}
	}
}
