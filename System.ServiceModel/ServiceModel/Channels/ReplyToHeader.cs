using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009AB RID: 2475
	internal class ReplyToHeader : AddressingHeader
	{
		// Token: 0x06006108 RID: 24840 RVA: 0x0016A3A3 File Offset: 0x001685A3
		private ReplyToHeader(EndpointAddress replyTo, AddressingVersion version) : base(version)
		{
			this.replyTo = replyTo;
		}

		// Token: 0x17001758 RID: 5976
		// (get) Token: 0x06006109 RID: 24841 RVA: 0x0016A3B3 File Offset: 0x001685B3
		public EndpointAddress ReplyTo
		{
			get
			{
				return this.replyTo;
			}
		}

		// Token: 0x17001759 RID: 5977
		// (get) Token: 0x0600610A RID: 24842 RVA: 0x0016A3BB File Offset: 0x001685BB
		public override XmlDictionaryString DictionaryName
		{
			get
			{
				return XD.AddressingDictionary.ReplyTo;
			}
		}

		// Token: 0x1700175A RID: 5978
		// (get) Token: 0x0600610B RID: 24843 RVA: 0x0016A3C7 File Offset: 0x001685C7
		public override bool MustUnderstand
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700175B RID: 5979
		// (get) Token: 0x0600610C RID: 24844 RVA: 0x0016A3CA File Offset: 0x001685CA
		public static ReplyToHeader AnonymousReplyTo10
		{
			get
			{
				if (ReplyToHeader.anonymousReplyToHeader10 == null)
				{
					ReplyToHeader.anonymousReplyToHeader10 = new ReplyToHeader(EndpointAddress.AnonymousAddress, AddressingVersion.WSAddressing10);
				}
				return ReplyToHeader.anonymousReplyToHeader10;
			}
		}

		// Token: 0x1700175C RID: 5980
		// (get) Token: 0x0600610D RID: 24845 RVA: 0x0016A3EC File Offset: 0x001685EC
		public static ReplyToHeader AnonymousReplyTo200408
		{
			get
			{
				if (ReplyToHeader.anonymousReplyToHeader200408 == null)
				{
					ReplyToHeader.anonymousReplyToHeader200408 = new ReplyToHeader(EndpointAddress.AnonymousAddress, AddressingVersion.WSAddressingAugust2004);
				}
				return ReplyToHeader.anonymousReplyToHeader200408;
			}
		}

		// Token: 0x0600610E RID: 24846 RVA: 0x0016A40E File Offset: 0x0016860E
		public static ReplyToHeader Create(EndpointAddress replyTo, AddressingVersion addressingVersion)
		{
			if (replyTo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("replyTo"));
			}
			if (addressingVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("addressingVersion"));
			}
			return new ReplyToHeader(replyTo, addressingVersion);
		}

		// Token: 0x0600610F RID: 24847 RVA: 0x0016A44D File Offset: 0x0016864D
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			this.replyTo.WriteContentsTo(base.Version, writer);
		}

		// Token: 0x06006110 RID: 24848 RVA: 0x0016A464 File Offset: 0x00168664
		public static ReplyToHeader ReadHeader(XmlDictionaryReader reader, AddressingVersion version, string actor, bool mustUnderstand, bool relay)
		{
			EndpointAddress endpointAddress = ReplyToHeader.ReadHeaderValue(reader, version);
			if (actor.Length != 0 || mustUnderstand || relay)
			{
				return new ReplyToHeader.FullReplyToHeader(endpointAddress, actor, mustUnderstand, relay, version);
			}
			if (endpointAddress != EndpointAddress.AnonymousAddress)
			{
				return new ReplyToHeader(endpointAddress, version);
			}
			if (version == AddressingVersion.WSAddressing10)
			{
				return ReplyToHeader.AnonymousReplyTo10;
			}
			return ReplyToHeader.AnonymousReplyTo200408;
		}

		// Token: 0x06006111 RID: 24849 RVA: 0x0016A4B7 File Offset: 0x001686B7
		public static EndpointAddress ReadHeaderValue(XmlDictionaryReader reader, AddressingVersion version)
		{
			return EndpointAddress.ReadFrom(version, reader);
		}

		// Token: 0x040038B6 RID: 14518
		private EndpointAddress replyTo;

		// Token: 0x040038B7 RID: 14519
		private const bool mustUnderstandValue = false;

		// Token: 0x040038B8 RID: 14520
		private static ReplyToHeader anonymousReplyToHeader10;

		// Token: 0x040038B9 RID: 14521
		private static ReplyToHeader anonymousReplyToHeader200408;

		// Token: 0x02000E3A RID: 3642
		private class FullReplyToHeader : ReplyToHeader
		{
			// Token: 0x0600829F RID: 33439 RVA: 0x001E3179 File Offset: 0x001E1379
			public FullReplyToHeader(EndpointAddress replyTo, string actor, bool mustUnderstand, bool relay, AddressingVersion version) : base(replyTo, version)
			{
				this.actor = actor;
				this.mustUnderstand = mustUnderstand;
				this.relay = relay;
			}

			// Token: 0x17001CD5 RID: 7381
			// (get) Token: 0x060082A0 RID: 33440 RVA: 0x001E319A File Offset: 0x001E139A
			public override string Actor
			{
				get
				{
					return this.actor;
				}
			}

			// Token: 0x17001CD6 RID: 7382
			// (get) Token: 0x060082A1 RID: 33441 RVA: 0x001E31A2 File Offset: 0x001E13A2
			public override bool MustUnderstand
			{
				get
				{
					return this.mustUnderstand;
				}
			}

			// Token: 0x17001CD7 RID: 7383
			// (get) Token: 0x060082A2 RID: 33442 RVA: 0x001E31AA File Offset: 0x001E13AA
			public override bool Relay
			{
				get
				{
					return this.relay;
				}
			}

			// Token: 0x04004A28 RID: 18984
			private string actor;

			// Token: 0x04004A29 RID: 18985
			private bool mustUnderstand;

			// Token: 0x04004A2A RID: 18986
			private bool relay;
		}
	}
}
