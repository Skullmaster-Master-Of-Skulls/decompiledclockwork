using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000238 RID: 568
	public class XmlSchemaAppInfo : XmlSchemaObject
	{
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x000810AF File Offset: 0x000800AF
		// (set) Token: 0x06001B0E RID: 6926 RVA: 0x000810B7 File Offset: 0x000800B7
		[XmlAttribute("source", DataType = "anyURI")]
		public string Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x000810C0 File Offset: 0x000800C0
		// (set) Token: 0x06001B10 RID: 6928 RVA: 0x000810C8 File Offset: 0x000800C8
		[XmlAnyElement]
		[XmlText]
		public XmlNode[] Markup
		{
			get
			{
				return this.markup;
			}
			set
			{
				this.markup = value;
			}
		}

		// Token: 0x040010EA RID: 4330
		private string source;

		// Token: 0x040010EB RID: 4331
		private XmlNode[] markup;
	}
}
