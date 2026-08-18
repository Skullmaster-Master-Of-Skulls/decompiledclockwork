using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000273 RID: 627
	public class XmlSchemaAttributeGroupRef : XmlSchemaAnnotated
	{
		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060025CC RID: 9676 RVA: 0x000CD295 File Offset: 0x000CB495
		// (set) Token: 0x060025CD RID: 9677 RVA: 0x000CD29D File Offset: 0x000CB49D
		[XmlAttribute("ref")]
		public XmlQualifiedName RefName
		{
			get
			{
				return this.refName;
			}
			set
			{
				this.refName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		// Token: 0x04001087 RID: 4231
		private XmlQualifiedName refName = XmlQualifiedName.Empty;
	}
}
