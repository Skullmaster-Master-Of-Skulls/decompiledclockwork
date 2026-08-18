using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000D8 RID: 216
	public class RfcLdapOID : Asn1OctetString
	{
		// Token: 0x06000583 RID: 1411 RVA: 0x0001A5D4 File Offset: 0x000195D4
		public RfcLdapOID(string s) : base(s)
		{
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001A5E8 File Offset: 0x000195E8
		[CLSCompliant(false)]
		public RfcLdapOID(sbyte[] s) : base(s)
		{
		}
	}
}
