using System;
using System.Collections;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200003C RID: 60
	public class LdapModifyRequest : LdapMessage
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000C31C File Offset: 0x0000B31C
		public virtual string DN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000C338 File Offset: 0x0000B338
		public virtual LdapModification[] Modifications
		{
			get
			{
				RfcModifyRequest rfcModifyRequest = (RfcModifyRequest)this.Asn1Object.getRequest();
				Asn1SequenceOf modifications = rfcModifyRequest.Modifications;
				Asn1Object[] array = modifications.toArray();
				LdapModification[] array2 = new LdapModification[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					Asn1Sequence asn1Sequence = (Asn1Sequence)array[i];
					if (asn1Sequence.size() != 2)
					{
						throw new SystemException(string.Concat(new object[]
						{
							"LdapModifyRequest: modification ",
							i,
							" is wrong size: ",
							asn1Sequence.size()
						}));
					}
					Asn1Object[] array3 = asn1Sequence.toArray();
					Asn1Enumerated asn1Enumerated = (Asn1Enumerated)array3[0];
					int op = asn1Enumerated.intValue();
					Asn1Sequence asn1Sequence2 = (Asn1Sequence)array3[1];
					Asn1Object[] array4 = asn1Sequence2.toArray();
					RfcAttributeDescription rfcAttributeDescription = (RfcAttributeDescription)array4[0];
					string attrName = rfcAttributeDescription.stringValue();
					Asn1SetOf asn1SetOf = (Asn1SetOf)array4[1];
					Asn1Object[] array5 = asn1SetOf.toArray();
					LdapAttribute ldapAttribute = new LdapAttribute(attrName);
					foreach (RfcAttributeValue rfcAttributeValue in array5)
					{
						ldapAttribute.addValue(rfcAttributeValue.byteValue());
					}
					array2[i] = new LdapModification(op, ldapAttribute);
				}
				return array2;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000C480 File Offset: 0x0000B480
		public LdapModifyRequest(string dn, LdapModification[] mods, LdapControl[] cont) : base(6, new RfcModifyRequest(new RfcLdapDN(dn), LdapModifyRequest.encodeModifications(mods)), cont)
		{
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000C4A8 File Offset: 0x0000B4A8
		private static Asn1SequenceOf encodeModifications(LdapModification[] mods)
		{
			Asn1SequenceOf asn1SequenceOf = new Asn1SequenceOf(mods.Length);
			for (int i = 0; i < mods.Length; i++)
			{
				LdapAttribute attribute = mods[i].Attribute;
				Asn1SetOf asn1SetOf = new Asn1SetOf(attribute.size());
				if (attribute.size() > 0)
				{
					IEnumerator byteValues = attribute.ByteValues;
					while (byteValues.MoveNext())
					{
						object obj = byteValues.Current;
						asn1SetOf.add(new RfcAttributeValue((sbyte[])obj));
					}
				}
				Asn1Sequence asn1Sequence = new Asn1Sequence(2);
				asn1Sequence.add(new Asn1Enumerated(mods[i].Op));
				asn1Sequence.add(new RfcAttributeTypeAndValues(new RfcAttributeDescription(attribute.Name), asn1SetOf));
				asn1SequenceOf.add(asn1Sequence);
			}
			return asn1SequenceOf;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000C560 File Offset: 0x0000B560
		public override string ToString()
		{
			return this.Asn1Object.ToString();
		}
	}
}
