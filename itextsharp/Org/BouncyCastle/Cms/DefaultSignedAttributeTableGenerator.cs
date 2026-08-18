using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020005AF RID: 1455
	public class DefaultSignedAttributeTableGenerator : CmsAttributeTableGenerator
	{
		// Token: 0x06003239 RID: 12857 RVA: 0x00138565 File Offset: 0x00137565
		public DefaultSignedAttributeTableGenerator()
		{
			this.table = new Hashtable();
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x00138578 File Offset: 0x00137578
		public DefaultSignedAttributeTableGenerator(AttributeTable attributeTable)
		{
			if (attributeTable != null)
			{
				this.table = attributeTable.ToHashtable();
				return;
			}
			this.table = new Hashtable();
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x0013859C File Offset: 0x0013759C
		protected virtual Hashtable createStandardAttributeTable(IDictionary parameters)
		{
			Hashtable hashtable = (Hashtable)this.table.Clone();
			if (!hashtable.ContainsKey(CmsAttributes.ContentType))
			{
				DerObjectIdentifier obj = (DerObjectIdentifier)parameters[CmsAttributeTableParameter.ContentType];
				Org.BouncyCastle.Asn1.Cms.Attribute attribute = new Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.ContentType, new DerSet(obj));
				hashtable[attribute.AttrType] = attribute;
			}
			if (!hashtable.ContainsKey(CmsAttributes.SigningTime))
			{
				Org.BouncyCastle.Asn1.Cms.Attribute attribute2 = new Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.SigningTime, new DerSet(new Time(DateTime.UtcNow)));
				hashtable[attribute2.AttrType] = attribute2;
			}
			if (!hashtable.ContainsKey(CmsAttributes.MessageDigest))
			{
				byte[] str = (byte[])parameters[CmsAttributeTableParameter.Digest];
				Org.BouncyCastle.Asn1.Cms.Attribute attribute3 = new Org.BouncyCastle.Asn1.Cms.Attribute(CmsAttributes.MessageDigest, new DerSet(new DerOctetString(str)));
				hashtable[attribute3.AttrType] = attribute3;
			}
			return hashtable;
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x00138673 File Offset: 0x00137673
		public virtual AttributeTable GetAttributes(IDictionary parameters)
		{
			return new AttributeTable(this.createStandardAttributeTable(parameters));
		}

		// Token: 0x0400226D RID: 8813
		private readonly Hashtable table;
	}
}
