using System;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000CE RID: 206
	public class RfcLdapDN : RfcLdapString
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x00018264 File Offset: 0x00017264
		public RfcLdapDN(string s) : base(s)
		{
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00018278 File Offset: 0x00017278
		[CLSCompliant(false)]
		public RfcLdapDN(sbyte[] s) : base(s)
		{
		}
	}
}
