using System;

namespace System.Xml.Schema
{
	// Token: 0x020002B9 RID: 697
	[Flags]
	public enum XmlSchemaValidationFlags
	{
		// Token: 0x0400116D RID: 4461
		None = 0,
		// Token: 0x0400116E RID: 4462
		ProcessInlineSchema = 1,
		// Token: 0x0400116F RID: 4463
		ProcessSchemaLocation = 2,
		// Token: 0x04001170 RID: 4464
		ReportValidationWarnings = 4,
		// Token: 0x04001171 RID: 4465
		ProcessIdentityConstraints = 8,
		// Token: 0x04001172 RID: 4466
		AllowXmlAttributes = 16
	}
}
