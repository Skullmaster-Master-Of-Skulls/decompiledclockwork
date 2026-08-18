using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000E6 RID: 230
	public class RfcSearchResultReference : Asn1SequenceOf
	{
		// Token: 0x060005AB RID: 1451 RVA: 0x0001AB54 File Offset: 0x00019B54
		[CLSCompliant(false)]
		public RfcSearchResultReference(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
		{
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001AB6C File Offset: 0x00019B6C
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 19);
		}
	}
}
