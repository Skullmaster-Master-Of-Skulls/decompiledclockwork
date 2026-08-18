using System;

namespace System.Xml
{
	// Token: 0x020000C2 RID: 194
	public enum ValidationType
	{
		// Token: 0x040002D1 RID: 721
		None,
		// Token: 0x040002D2 RID: 722
		[Obsolete("Validation type should be specified as DTD or Schema.")]
		Auto,
		// Token: 0x040002D3 RID: 723
		DTD,
		// Token: 0x040002D4 RID: 724
		[Obsolete("XDR Validation through XmlValidatingReader is obsoleted")]
		XDR,
		// Token: 0x040002D5 RID: 725
		Schema
	}
}
