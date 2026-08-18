using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020002B0 RID: 688
	public class XmlSchemaSimpleTypeList : XmlSchemaSimpleTypeContent
	{
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x060027E6 RID: 10214 RVA: 0x000D1F14 File Offset: 0x000D0114
		// (set) Token: 0x060027E7 RID: 10215 RVA: 0x000D1F1C File Offset: 0x000D011C
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

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x000D1F35 File Offset: 0x000D0135
		// (set) Token: 0x060027E9 RID: 10217 RVA: 0x000D1F3D File Offset: 0x000D013D
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

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x000D1F46 File Offset: 0x000D0146
		// (set) Token: 0x060027EB RID: 10219 RVA: 0x000D1F4E File Offset: 0x000D014E
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

		// Token: 0x060027EC RID: 10220 RVA: 0x000D1F58 File Offset: 0x000D0158
		internal override XmlSchemaObject Clone()
		{
			XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = (XmlSchemaSimpleTypeList)base.MemberwiseClone();
			xmlSchemaSimpleTypeList.ItemTypeName = this.itemTypeName.Clone();
			return xmlSchemaSimpleTypeList;
		}

		// Token: 0x04001150 RID: 4432
		private XmlQualifiedName itemTypeName = XmlQualifiedName.Empty;

		// Token: 0x04001151 RID: 4433
		private XmlSchemaSimpleType itemType;

		// Token: 0x04001152 RID: 4434
		private XmlSchemaSimpleType baseItemType;
	}
}
