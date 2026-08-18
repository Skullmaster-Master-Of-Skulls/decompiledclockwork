using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000264 RID: 612
	public class XmlSchemaKeyref : XmlSchemaIdentityConstraint
	{
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x000832F9 File Offset: 0x000822F9
		// (set) Token: 0x06001C83 RID: 7299 RVA: 0x00083301 File Offset: 0x00082301
		[XmlAttribute("refer")]
		public XmlQualifiedName Refer
		{
			get
			{
				return this.refer;
			}
			set
			{
				this.refer = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		// Token: 0x04001192 RID: 4498
		private XmlQualifiedName refer = XmlQualifiedName.Empty;
	}
}
