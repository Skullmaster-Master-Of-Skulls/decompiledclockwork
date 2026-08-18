using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000261 RID: 609
	public class XmlSchemaXPath : XmlSchemaAnnotated
	{
		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x000832D0 File Offset: 0x000822D0
		// (set) Token: 0x06001C7E RID: 7294 RVA: 0x000832D8 File Offset: 0x000822D8
		[XmlAttribute("xpath")]
		[DefaultValue("")]
		public string XPath
		{
			get
			{
				return this.xpath;
			}
			set
			{
				this.xpath = value;
			}
		}

		// Token: 0x04001191 RID: 4497
		private string xpath;
	}
}
