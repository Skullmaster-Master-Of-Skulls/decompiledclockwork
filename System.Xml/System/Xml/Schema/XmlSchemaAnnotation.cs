using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000235 RID: 565
	public class XmlSchemaAnnotation : XmlSchemaObject
	{
		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x00080D62 File Offset: 0x0007FD62
		// (set) Token: 0x06001AEC RID: 6892 RVA: 0x00080D6A File Offset: 0x0007FD6A
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

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x00080D73 File Offset: 0x0007FD73
		[XmlElement("documentation", typeof(XmlSchemaDocumentation))]
		[XmlElement("appinfo", typeof(XmlSchemaAppInfo))]
		public XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x00080D7B File Offset: 0x0007FD7B
		// (set) Token: 0x06001AEF RID: 6895 RVA: 0x00080D83 File Offset: 0x0007FD83
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

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x00080D8C File Offset: 0x0007FD8C
		// (set) Token: 0x06001AF1 RID: 6897 RVA: 0x00080D94 File Offset: 0x0007FD94
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

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00080D9D File Offset: 0x0007FD9D
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x040010E1 RID: 4321
		private string id;

		// Token: 0x040010E2 RID: 4322
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();

		// Token: 0x040010E3 RID: 4323
		private XmlAttribute[] moreAttributes;
	}
}
