using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000580 RID: 1408
	public class DerSequenceParser : Asn1SequenceParser, IAsn1Convertible
	{
		// Token: 0x06002FF1 RID: 12273 RVA: 0x00127CA9 File Offset: 0x00126CA9
		internal DerSequenceParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x00127CB8 File Offset: 0x00126CB8
		public IAsn1Convertible ReadObject()
		{
			return this._parser.ReadObject();
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x00127CC5 File Offset: 0x00126CC5
		public Asn1Object ToAsn1Object()
		{
			return new DerSequence(this._parser.ReadVector());
		}

		// Token: 0x040020E7 RID: 8423
		private readonly Asn1StreamParser _parser;
	}
}
