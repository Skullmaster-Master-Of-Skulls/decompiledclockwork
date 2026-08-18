using System;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200001A RID: 26
	public class LdapAbandonRequest : LdapMessage
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00005ED0 File Offset: 0x00004ED0
		public LdapAbandonRequest(int id, LdapControl[] cont) : base(16, new RfcAbandonRequest(id), cont)
		{
		}
	}
}
