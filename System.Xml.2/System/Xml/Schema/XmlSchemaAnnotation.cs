using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200026D RID: 621
	public class XmlSchemaAnnotation : XmlSchemaObject
	{
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x000CCC28 File Offset: 0x000CAE28
		// (set) Token: 0x06002575 RID: 9589 RVA: 0x000CCC30 File Offset: 0x000CAE30
		[XmlAttribute("id", DataType = "ID")]
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06002576 RID: 9590 RVA: 0x000CCC39 File Offset: 0x000CAE39
		[XmlElement("documentation", typeof(XmlSchemaDocumentation))]
		[XmlElement("appinfo", typeof(XmlSchemaAppInfo))]
		public XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x000CCC41 File Offset: 0x000CAE41
		// (set) Token: 0x06002578 RID: 9592 RVA: 0x000CCC49 File Offset: 0x000CAE49
		[XmlAnyAttribute]
		public XmlAttribute[] UnhandledAttributes
		{
			get
			{
				return this.moreAttributes;
			}
			set
			{
				this.moreAttributes = value;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002579 RID: 9593 RVA: 0x000CCC52 File Offset: 0x000CAE52
		// (set) Token: 0x0600257A RID: 9594 RVA: 0x000CCC5A File Offset: 0x000CAE5A
		[XmlIgnore]
		internal override string IdAttribute
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x000CCC63 File Offset: 0x000CAE63
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x04001069 RID: 4201
		private string id;

		// Token: 0x0400106A RID: 4202
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();

		// Token: 0x0400106B RID: 4203
		private XmlAttribute[] moreAttributes;
	}
}
