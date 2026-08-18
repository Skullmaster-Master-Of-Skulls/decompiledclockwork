using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009AD RID: 2477
	internal class RelatesToHeader : AddressingHeader
	{
		// Token: 0x0600611A RID: 24858 RVA: 0x0016A56F File Offset: 0x0016876F
		private RelatesToHeader(UniqueId messageId, AddressingVersion version) : base(version)
		{
			this.messageId = messageId;
		}

		// Token: 0x17001760 RID: 5984
		// (get) Token: 0x0600611B RID: 24859 RVA: 0x0016A57F File Offset: 0x0016877F
		public override XmlDictionaryString DictionaryName
		{
			get
			{
				return XD.AddressingDictionary.RelatesTo;
			}
		}

		// Token: 0x17001761 RID: 5985
		// (get) Token: 0x0600611C RID: 24860 RVA: 0x0016A58B File Offset: 0x0016878B
		public UniqueId UniqueId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x17001762 RID: 5986
		// (get) Token: 0x0600611D RID: 24861 RVA: 0x0016A593 File Offset: 0x00168793
		public override bool MustUnderstand
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001763 RID: 5987
		// (get) Token: 0x0600611E RID: 24862 RVA: 0x0016A596 File Offset: 0x00168796
		public virtual Uri RelationshipType
		{
			get
			{
				return RelatesToHeader.ReplyRelationshipType;
			}
		}

		// Token: 0x0600611F RID: 24863 RVA: 0x0016A59D File Offset: 0x0016879D
		public static RelatesToHeader Create(UniqueId messageId, AddressingVersion addressingVersion)
		{
			if (messageId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("messageId"));
			}
			if (addressingVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("addressingVersion"));
			}
			return new RelatesToHeader(messageId, addressingVersion);
		}

		// Token: 0x06006120 RID: 24864 RVA: 0x0016A5D8 File Offset: 0x001687D8
		public static RelatesToHeader Create(UniqueId messageId, AddressingVersion addressingVersion, Uri relationshipType)
		{
			if (messageId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("messageId"));
			}
			if (addressingVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("addressingVersion"));
			}
			if (relationshipType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("relationshipType"));
			}
			if (relationshipType == RelatesToHeader.ReplyRelationshipType)
			{
				return new RelatesToHeader(messageId, addressingVersion);
			}
			return new RelatesToHeader.FullRelatesToHeader(messageId, "", false, false, addressingVersion);
		}

		// Token: 0x06006121 RID: 24865 RVA: 0x0016A656 File Offset: 0x00168856
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			writer.WriteValue(this.messageId);
		}

		// Token: 0x06006122 RID: 24866 RVA: 0x0016A664 File Offset: 0x00168864
		public static void ReadHeaderValue(XmlDictionaryReader reader, AddressingVersion version, out Uri relationshipType, out UniqueId messageId)
		{
			AddressingDictionary addressingDictionary = XD.AddressingDictionary;
			relationshipType = RelatesToHeader.ReplyRelationshipType;
			messageId = reader.ReadElementContentAsUniqueId();
		}

		// Token: 0x06006123 RID: 24867 RVA: 0x0016A688 File Offset: 0x00168888
		public static RelatesToHeader ReadHeader(XmlDictionaryReader reader, AddressingVersion version, string actor, bool mustUnderstand, bool relay)
		{
			Uri uri;
			UniqueId uniqueId;
			RelatesToHeader.ReadHeaderValue(reader, version, out uri, out uniqueId);
			if (actor.Length == 0 && !mustUnderstand && !relay && uri == RelatesToHeader.ReplyRelationshipType)
			{
				return new RelatesToHeader(uniqueId, version);
			}
			return new RelatesToHeader.FullRelatesToHeader(uniqueId, actor, mustUnderstand, relay, version);
		}

		// Token: 0x040038BC RID: 14524
		private UniqueId messageId;

		// Token: 0x040038BD RID: 14525
		private const bool mustUnderstandValue = false;

		// Token: 0x040038BE RID: 14526
		internal static readonly Uri ReplyRelationshipType = new Uri("http://www.w3.org/2005/08/addressing/reply");

		// Token: 0x02000E3C RID: 3644
		private class FullRelatesToHeader : RelatesToHeader
		{
			// Token: 0x060082A7 RID: 33447 RVA: 0x001E31EB File Offset: 0x001E13EB
			public FullRelatesToHeader(UniqueId messageId, string actor, bool mustUnderstand, bool relay, AddressingVersion version) : base(messageId, version)
			{
				this.actor = actor;
				this.mustUnderstand = mustUnderstand;
				this.relay = relay;
			}

			// Token: 0x17001CDB RID: 7387
			// (get) Token: 0x060082A8 RID: 33448 RVA: 0x001E320C File Offset: 0x001E140C
			public override string Actor
			{
				get
				{
					return this.actor;
				}
			}

			// Token: 0x17001CDC RID: 7388
			// (get) Token: 0x060082A9 RID: 33449 RVA: 0x001E3214 File Offset: 0x001E1414
			public override bool MustUnderstand
			{
				get
				{
					return this.mustUnderstand;
				}
			}

			// Token: 0x17001CDD RID: 7389
			// (get) Token: 0x060082AA RID: 33450 RVA: 0x001E321C File Offset: 0x001E141C
			public override bool Relay
			{
				get
				{
					return this.relay;
				}
			}

			// Token: 0x060082AB RID: 33451 RVA: 0x001E3224 File Offset: 0x001E1424
			protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
			{
				writer.WriteValue(this.messageId);
			}

			// Token: 0x04004A2E RID: 18990
			private string actor;

			// Token: 0x04004A2F RID: 18991
			private bool mustUnderstand;

			// Token: 0x04004A30 RID: 18992
			private bool relay;
		}
	}
}
