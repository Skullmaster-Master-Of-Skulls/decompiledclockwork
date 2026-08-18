using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200040C RID: 1036
	public class DerSetParser : Asn1SetParser, IAsn1Convertible
	{
		// Token: 0x06002341 RID: 9025 RVA: 0x000D903F File Offset: 0x000D803F
		internal DerSetParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000D904E File Offset: 0x000D804E
		public IAsn1Convertible ReadObject()
		{
			return this._parser.ReadObject();
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x000D905B File Offset: 0x000D805B
		public Asn1Object ToAsn1Object()
		{
			return new DerSet(this._parser.ReadVector(), false);
		}

		// Token: 0x04001871 RID: 6257
		private readonly Asn1StreamParser _parser;
	}
}
