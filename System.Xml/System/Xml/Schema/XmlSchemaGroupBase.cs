using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000233 RID: 563
	public abstract class XmlSchemaGroupBase : XmlSchemaParticle
	{
		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001AE4 RID: 6884
		[XmlIgnore]
		public abstract XmlSchemaObjectCollection Items { get; }

		// Token: 0x06001AE5 RID: 6885
		internal abstract void SetItems(XmlSchemaObjectCollection newItems);
	}
}
