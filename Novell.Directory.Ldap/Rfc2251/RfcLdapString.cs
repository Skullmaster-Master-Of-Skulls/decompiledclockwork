using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000C0 RID: 192
	public class RfcLdapString : Asn1OctetString
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x000179D4 File Offset: 0x000169D4
		public RfcLdapString(string s) : base(s)
		{
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000179E8 File Offset: 0x000169E8
		[CLSCompliant(false)]
		public RfcLdapString(sbyte[] ba) : base(ba)
		{
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000179FC File Offset: 0x000169FC
		[CLSCompliant(false)]
		public RfcLdapString(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
		{
		}
	}
}
