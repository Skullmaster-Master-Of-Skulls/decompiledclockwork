using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200029A RID: 666
	public class XmlSchemaXPath : XmlSchemaAnnotated
	{
		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x000CF40A File Offset: 0x000CD60A
		// (set) Token: 0x06002709 RID: 9993 RVA: 0x000CF412 File Offset: 0x000CD612
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

		// Token: 0x0400110D RID: 4365
		private string xpath;
	}
}
