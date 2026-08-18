using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009AA RID: 2474
	internal class ToHeader : AddressingHeader
	{
		// Token: 0x060060FC RID: 24828 RVA: 0x0016A21C File Offset: 0x0016841C
		protected ToHeader(Uri to, AddressingVersion version) : base(version)
		{
			this.to = to;
		}

		// Token: 0x17001753 RID: 5971
		// (get) Token: 0x060060FD RID: 24829 RVA: 0x0016A22C File Offset: 0x0016842C
		private static ToHeader AnonymousTo10
		{
			get
			{
				if (ToHeader.anonymousToHeader10 == null)
				{
					ToHeader.anonymousToHeader10 = new ToHeader.AnonymousToHeader(AddressingVersion.WSAddressing10);
				}
				return ToHeader.anonymousToHeader10;
			}
		}

		// Token: 0x17001754 RID: 5972
		// (get) Token: 0x060060FE RID: 24830 RVA: 0x0016A249 File Offset: 0x00168449
		private static ToHeader AnonymousTo200408
		{
			get
			{
				if (ToHeader.anonymousToHeader200408 == null)
				{
					ToHeader.anonymousToHeader200408 = new ToHeader.AnonymousToHeader(AddressingVersion.WSAddressingAugust2004);
				}
				return ToHeader.anonymousToHeader200408;
			}
		}

		// Token: 0x17001755 RID: 5973
		// (get) Token: 0x060060FF RID: 24831 RVA: 0x0016A266 File Offset: 0x00168466
		public override XmlDictionaryString DictionaryName
		{
			get
			{
				return XD.AddressingDictionary.To;
			}
		}

		// Token: 0x17001756 RID: 5974
		// (get) Token: 0x06006100 RID: 24832 RVA: 0x0016A272 File Offset: 0x00168472
		public override bool MustUnderstand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001757 RID: 5975
		// (get) Token: 0x06006101 RID: 24833 RVA: 0x0016A275 File Offset: 0x00168475
		public Uri To
		{
			get
			{
				return this.to;
			}
		}

		// Token: 0x06006102 RID: 24834 RVA: 0x0016A27D File Offset: 0x0016847D
		public static ToHeader Create(Uri toUri, XmlDictionaryString dictionaryTo, AddressingVersion addressingVersion)
		{
			if (addressingVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("addressingVersion");
			}
			if (toUri != addressingVersion.AnonymousUri)
			{
				return new ToHeader.DictionaryToHeader(toUri, dictionaryTo, addressingVersion);
			}
			if (addressingVersion == AddressingVersion.WSAddressing10)
			{
				return ToHeader.AnonymousTo10;
			}
			return ToHeader.AnonymousTo200408;
		}

		// Token: 0x06006103 RID: 24835 RVA: 0x0016A2B7 File Offset: 0x001684B7
		public static ToHeader Create(Uri to, AddressingVersion addressingVersion)
		{
			if (to == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("to"));
			}
			if (to != addressingVersion.AnonymousUri)
			{
				return new ToHeader(to, addressingVersion);
			}
			if (addressingVersion == AddressingVersion.WSAddressing10)
			{
				return ToHeader.AnonymousTo10;
			}
			return ToHeader.AnonymousTo200408;
		}

		// Token: 0x06006104 RID: 24836 RVA: 0x0016A2F5 File Offset: 0x001684F5
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			writer.WriteString(this.to.AbsoluteUri);
		}

		// Token: 0x06006105 RID: 24837 RVA: 0x0016A308 File Offset: 0x00168508
		public static Uri ReadHeaderValue(XmlDictionaryReader reader, AddressingVersion version)
		{
			return ToHeader.ReadHeaderValue(reader, version, null);
		}

		// Token: 0x06006106 RID: 24838 RVA: 0x0016A314 File Offset: 0x00168514
		public static Uri ReadHeaderValue(XmlDictionaryReader reader, AddressingVersion version, UriCache uriCache)
		{
			string text = reader.ReadElementContentAsString();
			if (text == version.Anonymous)
			{
				return version.AnonymousUri;
			}
			if (uriCache == null)
			{
				return new Uri(text);
			}
			return uriCache.CreateUri(text);
		}

		// Token: 0x06006107 RID: 24839 RVA: 0x0016A34C File Offset: 0x0016854C
		public static ToHeader ReadHeader(XmlDictionaryReader reader, AddressingVersion version, UriCache uriCache, string actor, bool mustUnderstand, bool relay)
		{
			Uri uri = ToHeader.ReadHeaderValue(reader, version, uriCache);
			if (actor.Length != 0 || !mustUnderstand || relay)
			{
				return new ToHeader.FullToHeader(uri, actor, mustUnderstand, relay, version);
			}
			if (uri != version.AnonymousUri)
			{
				return new ToHeader(uri, version);
			}
			if (version == AddressingVersion.WSAddressing10)
			{
				return ToHeader.AnonymousTo10;
			}
			return ToHeader.AnonymousTo200408;
		}

		// Token: 0x040038B2 RID: 14514
		private Uri to;

		// Token: 0x040038B3 RID: 14515
		private const bool mustUnderstandValue = true;

		// Token: 0x040038B4 RID: 14516
		private static ToHeader anonymousToHeader10;

		// Token: 0x040038B5 RID: 14517
		private static ToHeader anonymousToHeader200408;

		// Token: 0x02000E37 RID: 3639
		private class AnonymousToHeader : ToHeader
		{
			// Token: 0x06008297 RID: 33431 RVA: 0x001E30FF File Offset: 0x001E12FF
			public AnonymousToHeader(AddressingVersion version) : base(version.AnonymousUri, version)
			{
			}

			// Token: 0x06008298 RID: 33432 RVA: 0x001E310E File Offset: 0x001E130E
			protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
			{
				writer.WriteString(base.Version.DictionaryAnonymous);
			}
		}

		// Token: 0x02000E38 RID: 3640
		private class DictionaryToHeader : ToHeader
		{
			// Token: 0x06008299 RID: 33433 RVA: 0x001E3121 File Offset: 0x001E1321
			public DictionaryToHeader(Uri to, XmlDictionaryString dictionaryTo, AddressingVersion version) : base(to, version)
			{
				this.dictionaryTo = dictionaryTo;
			}

			// Token: 0x0600829A RID: 33434 RVA: 0x001E3132 File Offset: 0x001E1332
			protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
			{
				writer.WriteString(this.dictionaryTo);
			}

			// Token: 0x04004A24 RID: 18980
			private XmlDictionaryString dictionaryTo;
		}

		// Token: 0x02000E39 RID: 3641
		private class FullToHeader : ToHeader
		{
			// Token: 0x0600829B RID: 33435 RVA: 0x001E3140 File Offset: 0x001E1340
			public FullToHeader(Uri to, string actor, bool mustUnderstand, bool relay, AddressingVersion version) : base(to, version)
			{
				this.actor = actor;
				this.mustUnderstand = mustUnderstand;
				this.relay = relay;
			}

			// Token: 0x17001CD2 RID: 7378
			// (get) Token: 0x0600829C RID: 33436 RVA: 0x001E3161 File Offset: 0x001E1361
			public override string Actor
			{
				get
				{
					return this.actor;
				}
			}

			// Token: 0x17001CD3 RID: 7379
			// (get) Token: 0x0600829D RID: 33437 RVA: 0x001E3169 File Offset: 0x001E1369
			public override bool MustUnderstand
			{
				get
				{
					return this.mustUnderstand;
				}
			}

			// Token: 0x17001CD4 RID: 7380
			// (get) Token: 0x0600829E RID: 33438 RVA: 0x001E3171 File Offset: 0x001E1371
			public override bool Relay
			{
				get
				{
					return this.relay;
				}
			}

			// Token: 0x04004A25 RID: 18981
			private string actor;

			// Token: 0x04004A26 RID: 18982
			private bool mustUnderstand;

			// Token: 0x04004A27 RID: 18983
			private bool relay;
		}
	}
}
