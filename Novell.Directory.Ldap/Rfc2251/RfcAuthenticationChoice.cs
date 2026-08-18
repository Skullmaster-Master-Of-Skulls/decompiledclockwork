using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000C7 RID: 199
	public class RfcAuthenticationChoice : Asn1Choice
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x00017B68 File Offset: 0x00016B68
		public RfcAuthenticationChoice(Asn1Tagged choice) : base(choice)
		{
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00017B7C File Offset: 0x00016B7C
		[CLSCompliant(false)]
		public RfcAuthenticationChoice(string mechanism, sbyte[] credentials) : base(new Asn1Tagged(new Asn1Identifier(2, true, 3), new RfcSaslCredentials(new RfcLdapString(mechanism), (credentials != null) ? new Asn1OctetString(credentials) : null), false))
		{
		}
	}
}
