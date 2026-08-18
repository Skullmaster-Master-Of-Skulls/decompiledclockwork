using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000C1 RID: 193
	public class RfcAttributeDescription : RfcLdapString
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x00017A14 File Offset: 0x00016A14
		public RfcAttributeDescription(string s) : base(s)
		{
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00017A28 File Offset: 0x00016A28
		[CLSCompliant(false)]
		public RfcAttributeDescription(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
		{
		}
	}
}
