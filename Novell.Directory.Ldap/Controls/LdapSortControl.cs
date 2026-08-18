using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x02000067 RID: 103
	public class LdapSortControl : LdapControl
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x00012248 File Offset: 0x00011248
		public LdapSortControl(LdapSortKey key, bool critical) : this(new LdapSortKey[]
		{
			key
		}, critical)
		{
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001226C File Offset: 0x0001126C
		public LdapSortControl(LdapSortKey[] keys, bool critical) : base(LdapSortControl.requestOID, critical, null)
		{
			Asn1SequenceOf asn1SequenceOf = new Asn1SequenceOf();
			for (int i = 0; i < keys.Length; i++)
			{
				Asn1Sequence asn1Sequence = new Asn1Sequence();
				asn1Sequence.add(new Asn1OctetString(keys[i].Key));
				if (keys[i].MatchRule != null)
				{
					asn1Sequence.add(new Asn1Tagged(new Asn1Identifier(2, false, LdapSortControl.ORDERING_RULE), new Asn1OctetString(keys[i].MatchRule), false));
				}
				if (keys[i].Reverse)
				{
					asn1Sequence.add(new Asn1Tagged(new Asn1Identifier(2, false, LdapSortControl.REVERSE_ORDER), new Asn1Boolean(true), false));
				}
				asn1SequenceOf.add(asn1Sequence);
			}
			this.setValue(asn1SequenceOf.getEncoding(new LBEREncoder()));
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00012328 File Offset: 0x00011328
		static LdapSortControl()
		{
			try
			{
				LdapControl.register(LdapSortControl.responseOID, Type.GetType("Novell.Directory.Ldap.Controls.LdapSortResponse"));
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x040001BA RID: 442
		private static int ORDERING_RULE = 0;

		// Token: 0x040001BB RID: 443
		private static int REVERSE_ORDER = 1;

		// Token: 0x040001BC RID: 444
		private static string requestOID = "1.2.840.113556.1.4.473";

		// Token: 0x040001BD RID: 445
		private static string responseOID = "1.2.840.113556.1.4.474";
	}
}
