using System;

namespace System.Xml.Schema
{
	// Token: 0x02000283 RID: 643
	[Flags]
	public enum XmlSchemaValidationFlags
	{
		// Token: 0x040011ED RID: 4589
		None = 0,
		// Token: 0x040011EE RID: 4590
		ProcessInlineSchema = 1,
		// Token: 0x040011EF RID: 4591
		ProcessSchemaLocation = 2,
		// Token: 0x040011F0 RID: 4592
		ReportValidationWarnings = 4,
		// Token: 0x040011F1 RID: 4593
		ProcessIdentityConstraints = 8,
		// Token: 0x040011F2 RID: 4594
		AllowXmlAttributes = 16
	}
}
