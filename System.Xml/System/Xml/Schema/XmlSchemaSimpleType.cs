using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000279 RID: 633
	public class XmlSchemaSimpleType : XmlSchemaType
	{
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x00085D84 File Offset: 0x00084D84
		// (set) Token: 0x06001D49 RID: 7497 RVA: 0x00085D8C File Offset: 0x00084D8C
		[XmlElement("union", typeof(XmlSchemaSimpleTypeUnion))]
		[XmlElement("list", typeof(XmlSchemaSimpleTypeList))]
		[XmlElement("restriction", typeof(XmlSchemaSimpleTypeRestriction))]
		public XmlSchemaSimpleTypeContent Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x00085D95 File Offset: 0x00084D95
		internal override XmlQualifiedName DerivedFrom
		{
			get
			{
				if (this.content == null)
				{
					return XmlQualifiedName.Empty;
				}
				if (this.content is XmlSchemaSimpleTypeRestriction)
				{
					return ((XmlSchemaSimpleTypeRestriction)this.content).BaseTypeName;
				}
				return XmlQualifiedName.Empty;
			}
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x00085DC8 File Offset: 0x00084DC8
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)base.MemberwiseClone();
			if (this.content != null)
			{
				xmlSchemaSimpleType.Content = (XmlSchemaSimpleTypeContent)this.content.Clone();
			}
			return xmlSchemaSimpleType;
		}

		// Token: 0x040011D9 RID: 4569
		private XmlSchemaSimpleTypeContent content;
	}
}
