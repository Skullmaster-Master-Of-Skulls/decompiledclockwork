using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000046 RID: 70
	public class LdapSearchResultReference : LdapMessage
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000E274 File Offset: 0x0000D274
		public virtual string[] Referrals
		{
			get
			{
				Asn1Object[] array = ((RfcSearchResultReference)this.message.Response).toArray();
				this.srefs = new string[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.srefs[i] = ((Asn1OctetString)array[i]).stringValue();
				}
				return this.srefs;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000E2D4 File Offset: 0x0000D2D4
		internal LdapSearchResultReference(RfcLdapMessage message) : base(message)
		{
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000E2EC File Offset: 0x0000D2EC
		static LdapSearchResultReference()
		{
			LdapSearchResultReference.nameLock = new object();
		}

		// Token: 0x0400014B RID: 331
		private string[] srefs;

		// Token: 0x0400014C RID: 332
		private static object nameLock;

		// Token: 0x0400014D RID: 333
		private static int refNum = 0;

		// Token: 0x0400014E RID: 334
		private string name;
	}
}
