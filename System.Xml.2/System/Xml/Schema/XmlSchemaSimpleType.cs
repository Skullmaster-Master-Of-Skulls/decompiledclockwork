using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020002AE RID: 686
	public class XmlSchemaSimpleType : XmlSchemaType
	{
		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x060027E1 RID: 10209 RVA: 0x000D1E90 File Offset: 0x000D0090
		// (set) Token: 0x060027E2 RID: 10210 RVA: 0x000D1E98 File Offset: 0x000D0098
		[XmlElement("restriction", typeof(XmlSchemaSimpleTypeRestriction))]
		[XmlElement("list", typeof(XmlSchemaSimpleTypeList))]
		[XmlElement("union", typeof(XmlSchemaSimpleTypeUnion))]
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

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x060027E3 RID: 10211 RVA: 0x000D1EA1 File Offset: 0x000D00A1
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

		// Token: 0x060027E4 RID: 10212 RVA: 0x000D1ED4 File Offset: 0x000D00D4
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)base.MemberwiseClone();
			if (this.content != null)
			{
				xmlSchemaSimpleType.Content = (XmlSchemaSimpleTypeContent)this.content.Clone();
			}
			return xmlSchemaSimpleType;
		}

		// Token: 0x0400114F RID: 4431
		private XmlSchemaSimpleTypeContent content;
	}
}
