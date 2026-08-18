using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200027E RID: 638
	public enum XmlSchemaContentProcessing
	{
		// Token: 0x040010B2 RID: 4274
		[XmlIgnore]
		None,
		// Token: 0x040010B3 RID: 4275
		[XmlEnum("skip")]
		Skip,
		// Token: 0x040010B4 RID: 4276
		[XmlEnum("lax")]
		Lax,
		// Token: 0x040010B5 RID: 4277
		[XmlEnum("strict")]
		Strict
	}
}
