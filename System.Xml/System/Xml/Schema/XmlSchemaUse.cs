using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x02000280 RID: 640
	public enum XmlSchemaUse
	{
		// Token: 0x040011E7 RID: 4583
		[XmlIgnore]
		None,
		// Token: 0x040011E8 RID: 4584
		[XmlEnum("optional")]
		Optional,
		// Token: 0x040011E9 RID: 4585
		[XmlEnum("prohibited")]
		Prohibited,
		// Token: 0x040011EA RID: 4586
		[XmlEnum("required")]
		Required
	}
}
