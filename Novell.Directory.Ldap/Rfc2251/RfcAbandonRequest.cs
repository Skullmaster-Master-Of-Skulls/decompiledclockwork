using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000BA RID: 186
	internal class RfcAbandonRequest : RfcMessageID, RfcRequest
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x000176D0 File Offset: 0x000166D0
		public RfcAbandonRequest(int msgId) : base(msgId)
		{
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000176E8 File Offset: 0x000166E8
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, false, 16);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00017704 File Offset: 0x00016704
		public RfcRequest dupRequest(string base_Renamed, string filter, bool reference)
		{
			throw new LdapException("NO_DUP_REQUEST", new object[]
			{
				"Abandon"
			}, 92, null);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00017730 File Offset: 0x00016730
		public string getRequestDN()
		{
			return null;
		}
	}
}
