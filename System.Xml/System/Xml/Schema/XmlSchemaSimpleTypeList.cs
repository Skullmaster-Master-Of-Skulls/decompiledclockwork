using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200027B RID: 635
	public class XmlSchemaSimpleTypeList : XmlSchemaSimpleTypeContent
	{
		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x00085E08 File Offset: 0x00084E08
		// (set) Token: 0x06001D4E RID: 7502 RVA: 0x00085E10 File Offset: 0x00084E10
		[XmlAttribute("itemType")]
		public XmlQualifiedName ItemTypeName
		{
			get
			{
				return this.itemTypeName;
			}
			set
			{
				this.itemTypeName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x00085E29 File Offset: 0x00084E29
		// (set) Token: 0x06001D50 RID: 7504 RVA: 0x00085E31 File Offset: 0x00084E31
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		public XmlSchemaSimpleType ItemType
		{
			get
			{
				return this.itemType;
			}
			set
			{
				this.itemType = value;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x00085E3A File Offset: 0x00084E3A
		// (set) Token: 0x06001D52 RID: 7506 RVA: 0x00085E42 File Offset: 0x00084E42
		[XmlIgnore]
		public XmlSchemaSimpleType BaseItemType
		{
			get
			{
				return this.baseItemType;
			}
			set
			{
				this.baseItemType = value;
			}
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x00085E4C File Offset: 0x00084E4C
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = (XmlSchemaSimpleTypeList)base.MemberwiseClone();
			xmlSchemaSimpleTypeList.ItemTypeName = this.itemTypeName.Clone();
			return xmlSchemaSimpleTypeList;
		}

		// Token: 0x040011DA RID: 4570
		private XmlQualifiedName itemTypeName = XmlQualifiedName.Empty;

		// Token: 0x040011DB RID: 4571
		private XmlSchemaSimpleType itemType;

		// Token: 0x040011DC RID: 4572
		private XmlSchemaSimpleType baseItemType;
	}
}
