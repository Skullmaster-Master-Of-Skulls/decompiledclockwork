using System;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x020002BF RID: 703
	public class ContentInfoParser
	{
		// Token: 0x06001A73 RID: 6771 RVA: 0x0009C0AC File Offset: 0x0009B0AC
		public ContentInfoParser(Asn1SequenceParser seq)
		{
			this.contentType = (DerObjectIdentifier)seq.ReadObject();
			this.content = (Asn1TaggedObjectParser)seq.ReadObject();
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x0009C0D6 File Offset: 0x0009B0D6
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0009C0DE File Offset: 0x0009B0DE
		public IAsn1Convertible GetContent(int tag)
		{
			if (this.content == null)
			{
				return null;
			}
			return this.content.GetObjectParser(tag, true);
		}

		// Token: 0x040011A6 RID: 4518
		private DerObjectIdentifier contentType;

		// Token: 0x040011A7 RID: 4519
		private Asn1TaggedObjectParser content;
	}
}
