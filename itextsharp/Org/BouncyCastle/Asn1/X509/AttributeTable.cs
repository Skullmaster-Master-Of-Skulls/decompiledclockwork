using System;
using System.Collections;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200030A RID: 778
	public class AttributeTable
	{
		// Token: 0x06001C81 RID: 7297 RVA: 0x000AADD4 File Offset: 0x000A9DD4
		public AttributeTable(Hashtable attrs)
		{
			this.attributes = new Hashtable(attrs);
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x000AADE8 File Offset: 0x000A9DE8
		public AttributeTable(Asn1EncodableVector v)
		{
			this.attributes = new Hashtable(v.Count);
			for (int num = 0; num != v.Count; num++)
			{
				AttributeX509 instance = AttributeX509.GetInstance(v[num]);
				this.attributes.Add(instance.AttrType, instance);
			}
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x000AAE3C File Offset: 0x000A9E3C
		public AttributeTable(Asn1Set s)
		{
			this.attributes = new Hashtable(s.Count);
			for (int num = 0; num != s.Count; num++)
			{
				AttributeX509 instance = AttributeX509.GetInstance(s[num]);
				this.attributes.Add(instance.AttrType, instance);
			}
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x000AAE90 File Offset: 0x000A9E90
		public AttributeX509 Get(DerObjectIdentifier oid)
		{
			return (AttributeX509)this.attributes[oid];
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x000AAEA3 File Offset: 0x000A9EA3
		public Hashtable ToHashtable()
		{
			return new Hashtable(this.attributes);
		}

		// Token: 0x040013A7 RID: 5031
		private readonly Hashtable attributes;
	}
}
