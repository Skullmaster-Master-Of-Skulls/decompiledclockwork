using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000045 RID: 69
	public class LdapSearchResult : LdapMessage
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000E108 File Offset: 0x0000D108
		public virtual LdapEntry Entry
		{
			get
			{
				if (this.entry == null)
				{
					LdapAttributeSet ldapAttributeSet = new LdapAttributeSet();
					Asn1Sequence attributes = ((RfcSearchResultEntry)this.message.Response).Attributes;
					foreach (Asn1Sequence asn1Sequence in attributes.toArray())
					{
						LdapAttribute ldapAttribute = new LdapAttribute(((Asn1OctetString)asn1Sequence.get_Renamed(0)).stringValue());
						Asn1Set asn1Set = (Asn1Set)asn1Sequence.get_Renamed(1);
						object[] array2 = asn1Set.toArray();
						for (int j = 0; j < array2.Length; j++)
						{
							ldapAttribute.addValue(((Asn1OctetString)array2[j]).byteValue());
						}
						ldapAttributeSet.Add(ldapAttribute);
					}
					this.entry = new LdapEntry(((RfcSearchResultEntry)this.message.Response).ObjectName.stringValue(), ldapAttributeSet);
				}
				return this.entry;
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000E1F0 File Offset: 0x0000D1F0
		internal LdapSearchResult(RfcLdapMessage message) : base(message)
		{
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000E210 File Offset: 0x0000D210
		public LdapSearchResult(LdapEntry entry, LdapControl[] cont)
		{
			if (entry == null)
			{
				throw new ArgumentException("Argument \"entry\" cannot be null");
			}
			this.entry = entry;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000E244 File Offset: 0x0000D244
		public override string ToString()
		{
			string result;
			if (this.entry == null)
			{
				result = base.ToString();
			}
			else
			{
				result = this.entry.ToString();
			}
			return result;
		}

		// Token: 0x0400014A RID: 330
		private LdapEntry entry = null;
	}
}
