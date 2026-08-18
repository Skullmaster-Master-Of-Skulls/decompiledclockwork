using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200023B RID: 571
	public class XmlSchemaAttributeGroupRef : XmlSchemaAnnotated
	{
		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x000813C1 File Offset: 0x000803C1
		// (set) Token: 0x06001B45 RID: 6981 RVA: 0x000813C9 File Offset: 0x000803C9
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

		// Token: 0x04001100 RID: 4352
		private XmlQualifiedName refName = XmlQualifiedName.Empty;
	}
}
