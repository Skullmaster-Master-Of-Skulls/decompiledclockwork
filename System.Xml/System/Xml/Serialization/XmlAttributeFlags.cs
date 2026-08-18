using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000305 RID: 773
	internal enum XmlAttributeFlags
	{
		// Token: 0x04001555 RID: 5461
		Enum = 1,
		// Token: 0x04001556 RID: 5462
		Array,
		// Token: 0x04001557 RID: 5463
		Text = 4,
		// Token: 0x04001558 RID: 5464
		ArrayItems = 8,
		// Token: 0x04001559 RID: 5465
		Elements = 16,
		// Token: 0x0400155A RID: 5466
		Attribute = 32,
		// Token: 0x0400155B RID: 5467
		Root = 64,
		// Token: 0x0400155C RID: 5468
		Type = 128,
		// Token: 0x0400155D RID: 5469
		AnyElements = 256,
		// Token: 0x0400155E RID: 5470
		AnyAttribute = 512,
		// Token: 0x0400155F RID: 5471
		ChoiceIdentifier = 1024,
		// Token: 0x04001560 RID: 5472
		XmlnsDeclarations = 2048
	}
}
