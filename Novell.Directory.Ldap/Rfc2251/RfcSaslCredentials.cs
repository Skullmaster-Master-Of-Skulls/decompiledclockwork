using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000E2 RID: 226
	public class RfcSaslCredentials : Asn1Sequence
	{
		// Token: 0x0600059D RID: 1437 RVA: 0x0001A940 File Offset: 0x00019940
		public RfcSaslCredentials(RfcLdapString mechanism) : this(mechanism, null)
		{
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001A958 File Offset: 0x00019958
		public RfcSaslCredentials(RfcLdapString mechanism, Asn1OctetString credentials) : base(2)
		{
			base.add(mechanism);
			if (credentials != null)
			{
				base.add(credentials);
			}
		}
	}
}
