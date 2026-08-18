using System;
using System.Collections;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200001B RID: 27
	public class LdapAddRequest : LdapMessage
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00005EF0 File Offset: 0x00004EF0
		public virtual LdapEntry Entry
		{
			get
			{
				RfcAddRequest rfcAddRequest = (RfcAddRequest)this.Asn1Object.getRequest();
				LdapAttributeSet ldapAttributeSet = new LdapAttributeSet();
				foreach (RfcAttributeTypeAndValues rfcAttributeTypeAndValues in rfcAddRequest.Attributes.toArray())
				{
					LdapAttribute ldapAttribute = new LdapAttribute(((Asn1OctetString)rfcAttributeTypeAndValues.get_Renamed(0)).stringValue());
					Asn1SetOf asn1SetOf = (Asn1SetOf)rfcAttributeTypeAndValues.get_Renamed(1);
					object[] array2 = asn1SetOf.toArray();
					for (int j = 0; j < array2.Length; j++)
					{
						ldapAttribute.addValue(((Asn1OctetString)array2[j]).byteValue());
					}
					ldapAttributeSet.Add(ldapAttribute);
				}
				return new LdapEntry(this.Asn1Object.RequestDN, ldapAttributeSet);
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005FB4 File Offset: 0x00004FB4
		public LdapAddRequest(LdapEntry entry, LdapControl[] cont) : base(8, new RfcAddRequest(new RfcLdapDN(entry.DN), LdapAddRequest.makeRfcAttrList(entry)), cont)
		{
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005FE4 File Offset: 0x00004FE4
		private static RfcAttributeList makeRfcAttrList(LdapEntry entry)
		{
			LdapAttributeSet attributeSet = entry.getAttributeSet();
			RfcAttributeList rfcAttributeList = new RfcAttributeList(attributeSet.Count);
			foreach (object obj in attributeSet)
			{
				LdapAttribute ldapAttribute = (LdapAttribute)obj;
				Asn1SetOf asn1SetOf = new Asn1SetOf(ldapAttribute.size());
				IEnumerator byteValues = ldapAttribute.ByteValues;
				while (byteValues.MoveNext())
				{
					object obj2 = byteValues.Current;
					asn1SetOf.add(new RfcAttributeValue((sbyte[])obj2));
				}
				rfcAttributeList.add(new RfcAttributeTypeAndValues(new RfcAttributeDescription(ldapAttribute.Name), asn1SetOf));
			}
			return rfcAttributeList;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006078 File Offset: 0x00005078
		public override string ToString()
		{
			return this.Asn1Object.ToString();
		}
	}
}
