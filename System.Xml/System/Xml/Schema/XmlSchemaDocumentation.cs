using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200024A RID: 586
	public class XmlSchemaDocumentation : XmlSchemaObject
	{
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x0008284C File Offset: 0x0008184C
		// (set) Token: 0x06001BE4 RID: 7140 RVA: 0x00082854 File Offset: 0x00081854
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

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x0008285D File Offset: 0x0008185D
		// (set) Token: 0x06001BE6 RID: 7142 RVA: 0x00082865 File Offset: 0x00081865
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

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x00082884 File Offset: 0x00081884
		// (set) Token: 0x06001BE8 RID: 7144 RVA: 0x0008288C File Offset: 0x0008188C
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

		// Token: 0x04001148 RID: 4424
		private string source;

		// Token: 0x04001149 RID: 4425
		private string language;

		// Token: 0x0400114A RID: 4426
		private XmlNode[] markup;

		// Token: 0x0400114B RID: 4427
		private static XmlSchemaSimpleType languageType = DatatypeImplementation.GetSimpleTypeFromXsdType(new XmlQualifiedName("language", "http://www.w3.org/2001/XMLSchema"));
	}
}
