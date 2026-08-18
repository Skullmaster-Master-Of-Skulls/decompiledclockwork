using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000282 RID: 642
	public class XmlSchemaDocumentation : XmlSchemaObject
	{
		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002669 RID: 9833 RVA: 0x000CE958 File Offset: 0x000CCB58
		// (set) Token: 0x0600266A RID: 9834 RVA: 0x000CE960 File Offset: 0x000CCB60
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

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x000CE969 File Offset: 0x000CCB69
		// (set) Token: 0x0600266C RID: 9836 RVA: 0x000CE971 File Offset: 0x000CCB71
		[XmlAttribute("xml:lang")]
		public string Language
		{
			get
			{
				return this.language;
			}
			set
			{
				this.language = (string)XmlSchemaDocumentation.languageType.Datatype.ParseValue(value, null, null);
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x0600266D RID: 9837 RVA: 0x000CE990 File Offset: 0x000CCB90
		// (set) Token: 0x0600266E RID: 9838 RVA: 0x000CE998 File Offset: 0x000CCB98
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

		// Token: 0x040010C4 RID: 4292
		private string source;

		// Token: 0x040010C5 RID: 4293
		private string language;

		// Token: 0x040010C6 RID: 4294
		private XmlNode[] markup;

		// Token: 0x040010C7 RID: 4295
		private static XmlSchemaSimpleType languageType = DatatypeImplementation.GetSimpleTypeFromXsdType(new XmlQualifiedName("language", "http://www.w3.org/2001/XMLSchema"));
	}
}
