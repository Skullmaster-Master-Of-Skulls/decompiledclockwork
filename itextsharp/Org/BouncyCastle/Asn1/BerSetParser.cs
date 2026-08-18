using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000414 RID: 1044
	public class BerSetParser : Asn1SetParser, IAsn1Convertible
	{
		// Token: 0x06002384 RID: 9092 RVA: 0x000D9B38 File Offset: 0x000D8B38
		internal BerSetParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x000D9B47 File Offset: 0x000D8B47
		public IAsn1Convertible ReadObject()
		{
			return this._parser.ReadObject();
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x000D9B54 File Offset: 0x000D8B54
		public Asn1Object ToAsn1Object()
		{
			return new BerSet(this._parser.ReadVector(), false);
		}

		// Token: 0x04001885 RID: 6277
		private readonly Asn1StreamParser _parser;
	}
}
