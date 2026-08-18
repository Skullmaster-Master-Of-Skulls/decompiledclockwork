using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000392 RID: 914
	public class BerSequenceParser : Asn1SequenceParser, IAsn1Convertible
	{
		// Token: 0x06001FCA RID: 8138 RVA: 0x000BD15C File Offset: 0x000BC15C
		internal BerSequenceParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x000BD16B File Offset: 0x000BC16B
		public IAsn1Convertible ReadObject()
		{
			return this._parser.ReadObject();
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x000BD178 File Offset: 0x000BC178
		public Asn1Object ToAsn1Object()
		{
			return new BerSequence(this._parser.ReadVector());
		}

		// Token: 0x040015EF RID: 5615
		private readonly Asn1StreamParser _parser;
	}
}
