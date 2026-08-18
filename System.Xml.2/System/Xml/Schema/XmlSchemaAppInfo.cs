using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000270 RID: 624
	public class XmlSchemaAppInfo : XmlSchemaObject
	{
		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x000CCF73 File Offset: 0x000CB173
		// (set) Token: 0x06002597 RID: 9623 RVA: 0x000CCF7B File Offset: 0x000CB17B
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

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002598 RID: 9624 RVA: 0x000CCF84 File Offset: 0x000CB184
		// (set) Token: 0x06002599 RID: 9625 RVA: 0x000CCF8C File Offset: 0x000CB18C
		[XmlText]
		[XmlAnyElement]
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

		// Token: 0x04001072 RID: 4210
		private string source;

		// Token: 0x04001073 RID: 4211
		private XmlNode[] markup;
	}
}
