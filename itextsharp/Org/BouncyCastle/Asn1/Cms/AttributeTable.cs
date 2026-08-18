using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x020000B0 RID: 176
	public class AttributeTable
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x0001C945 File Offset: 0x0001B945
		public AttributeTable(Hashtable attrs)
		{
			this.attributes = new Hashtable(attrs);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001C95C File Offset: 0x0001B95C
		public AttributeTable(Asn1EncodableVector v)
		{
			this.attributes = new Hashtable(v.Count);
			foreach (object obj in v)
			{
				Asn1Encodable obj2 = (Asn1Encodable)obj;
				Attribute instance = Attribute.GetInstance(obj2);
				this.AddAttribute(instance);
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001C9D0 File Offset: 0x0001B9D0
		public AttributeTable(Asn1Set s)
		{
			this.attributes = new Hashtable(s.Count);
			for (int num = 0; num != s.Count; num++)
			{
				Attribute instance = Attribute.GetInstance(s[num]);
				this.AddAttribute(instance);
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001CA1C File Offset: 0x0001BA1C
		private void AddAttribute(Attribute a)
		{
			DerObjectIdentifier attrType = a.AttrType;
			object obj = this.attributes[attrType];
			if (obj == null)
			{
				this.attributes[attrType] = a;
				return;
			}
			ArrayList arrayList;
			if (obj is Attribute)
			{
				arrayList = new ArrayList();
				arrayList.Add(obj);
				arrayList.Add(a);
			}
			else
			{
				arrayList = (ArrayList)obj;
				arrayList.Add(a);
			}
			this.attributes[attrType] = arrayList;
		}

		// Token: 0x17000100 RID: 256
		public Attribute this[DerObjectIdentifier oid]
		{
			get
			{
				object obj = this.attributes[oid];
				if (obj is ArrayList)
				{
					return (Attribute)((ArrayList)obj)[0];
				}
				return (Attribute)obj;
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001CAC6 File Offset: 0x0001BAC6
		[Obsolete("Use 'object[oid]' syntax instead")]
		public Attribute Get(DerObjectIdentifier oid)
		{
			return this[oid];
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001CAD0 File Offset: 0x0001BAD0
		public Asn1EncodableVector GetAll(DerObjectIdentifier oid)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			object obj = this.attributes[oid];
			if (obj is ArrayList)
			{
				using (IEnumerator enumerator = ((ArrayList)obj).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						Attribute attribute = (Attribute)obj2;
						asn1EncodableVector.Add(new Asn1Encodable[]
						{
							attribute
						});
					}
					return asn1EncodableVector;
				}
			}
			if (obj != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					(Attribute)obj
				});
			}
			return asn1EncodableVector;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001CB7C File Offset: 0x0001BB7C
		public Hashtable ToHashtable()
		{
			return new Hashtable(this.attributes);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001CB8C File Offset: 0x0001BB8C
		public Asn1EncodableVector ToAsn1EncodableVector()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (object obj in this.attributes.Values)
			{
				if (obj is ArrayList)
				{
					using (IEnumerator enumerator2 = ((ArrayList)obj).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							asn1EncodableVector.Add(new Asn1Encodable[]
							{
								Attribute.GetInstance(obj2)
							});
						}
						continue;
					}
				}
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					Attribute.GetInstance(obj)
				});
			}
			return asn1EncodableVector;
		}

		// Token: 0x040002B5 RID: 693
		private readonly Hashtable attributes;
	}
}
