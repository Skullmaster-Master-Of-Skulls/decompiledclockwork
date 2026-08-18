using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000276 RID: 630
	public class XmlSchemaSimpleContent : XmlSchemaContentModel
	{
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x00085C7D File Offset: 0x00084C7D
		// (set) Token: 0x06001D34 RID: 7476 RVA: 0x00085C85 File Offset: 0x00084C85
		[XmlElement("extension", typeof(XmlSchemaSimpleContentExtension))]
		[XmlElement("restriction", typeof(XmlSchemaSimpleContentRestriction))]
		public override XmlSchemaContent Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.content = value;
			}
		}

		// Token: 0x040011D0 RID: 4560
		private XmlSchemaContent content;
	}
}
