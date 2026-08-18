using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200029D RID: 669
	public class XmlSchemaKeyref : XmlSchemaIdentityConstraint
	{
		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x000CF433 File Offset: 0x000CD633
		// (set) Token: 0x0600270E RID: 9998 RVA: 0x000CF43B File Offset: 0x000CD63B
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

		// Token: 0x0400110E RID: 4366
		private XmlQualifiedName refer = XmlQualifiedName.Empty;
	}
}
