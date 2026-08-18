using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000297 RID: 663
	public abstract class XmlSchemaGroupBase : XmlSchemaParticle
	{
		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060026F2 RID: 9970
		[XmlIgnore]
		public abstract XmlSchemaObjectCollection Items { get; }

		// Token: 0x060026F3 RID: 9971
		internal abstract void SetItems(XmlSchemaObjectCollection newItems);
	}
}
