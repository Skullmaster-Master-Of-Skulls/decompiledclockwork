using System;

namespace System.Xml.Schema
{
	// Token: 0x02000284 RID: 644
	internal enum ValidatorState
	{
		// Token: 0x040011F4 RID: 4596
		None,
		// Token: 0x040011F5 RID: 4597
		Start,
		// Token: 0x040011F6 RID: 4598
		TopLevelAttribute,
		// Token: 0x040011F7 RID: 4599
		TopLevelTextOrWS,
		// Token: 0x040011F8 RID: 4600
		Element,
		// Token: 0x040011F9 RID: 4601
		Attribute,
		// Token: 0x040011FA RID: 4602
		EndOfAttributes,
		// Token: 0x040011FB RID: 4603
		Text,
		// Token: 0x040011FC RID: 4604
		Whitespace,
		// Token: 0x040011FD RID: 4605
		EndElement,
		// Token: 0x040011FE RID: 4606
		SkipToEndElement,
		// Token: 0x040011FF RID: 4607
		Finish
	}
}
