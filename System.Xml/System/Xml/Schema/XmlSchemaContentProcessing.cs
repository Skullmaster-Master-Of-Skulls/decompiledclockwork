using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000247 RID: 583
	public enum XmlSchemaContentProcessing
	{
		// Token: 0x04001136 RID: 4406
		[XmlIgnore]
		None,
		// Token: 0x04001137 RID: 4407
		[XmlEnum("skip")]
		Skip,
		// Token: 0x04001138 RID: 4408
		[XmlEnum("lax")]
		Lax,
		// Token: 0x04001139 RID: 4409
		[XmlEnum("strict")]
		Strict
	}
}
