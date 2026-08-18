using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020002AB RID: 683
	public class XmlSchemaSimpleContent : XmlSchemaContentModel
	{
		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060027CC RID: 10188 RVA: 0x000D1D89 File Offset: 0x000CFF89
		// (set) Token: 0x060027CD RID: 10189 RVA: 0x000D1D91 File Offset: 0x000CFF91
		[XmlElement("restriction", typeof(XmlSchemaSimpleContentRestriction))]
		[XmlElement("extension", typeof(XmlSchemaSimpleContentExtension))]
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

		// Token: 0x04001146 RID: 4422
		private XmlSchemaContent content;
	}
}
