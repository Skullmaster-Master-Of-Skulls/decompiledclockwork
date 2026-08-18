using System;

namespace System.Xml.Schema
{
	// Token: 0x020002BA RID: 698
	internal enum ValidatorState
	{
		// Token: 0x04001174 RID: 4468
		None,
		// Token: 0x04001175 RID: 4469
		Start,
		// Token: 0x04001176 RID: 4470
		TopLevelAttribute,
		// Token: 0x04001177 RID: 4471
		TopLevelTextOrWS,
		// Token: 0x04001178 RID: 4472
		Element,
		// Token: 0x04001179 RID: 4473
		Attribute,
		// Token: 0x0400117A RID: 4474
		EndOfAttributes,
		// Token: 0x0400117B RID: 4475
		Text,
		// Token: 0x0400117C RID: 4476
		Whitespace,
		// Token: 0x0400117D RID: 4477
		EndElement,
		// Token: 0x0400117E RID: 4478
		SkipToEndElement,
		// Token: 0x0400117F RID: 4479
		Finish
	}
}
