using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020002B6 RID: 694
	public enum XmlSchemaUse
	{
		// Token: 0x04001167 RID: 4455
		[XmlIgnore]
		None,
		// Token: 0x04001168 RID: 4456
		[XmlEnum("optional")]
		Optional,
		// Token: 0x04001169 RID: 4457
		[XmlEnum("prohibited")]
		Prohibited,
		// Token: 0x0400116A RID: 4458
		[XmlEnum("required")]
		Required
	}
}
