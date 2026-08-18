using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000450 RID: 1104
	public class DerExternalParser : Asn1Encodable
	{
		// Token: 0x0600254D RID: 9549 RVA: 0x000E2097 File Offset: 0x000E1097
		public DerExternalParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x000E20A6 File Offset: 0x000E10A6
		public IAsn1Convertible ReadObject()
		{
			return this._parser.ReadObject();
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x000E20B3 File Offset: 0x000E10B3
		public override Asn1Object ToAsn1Object()
		{
			return new DerExternal(this._parser.ReadVector());
		}

		// Token: 0x04001A20 RID: 6688
		private readonly Asn1StreamParser _parser;
	}
}
