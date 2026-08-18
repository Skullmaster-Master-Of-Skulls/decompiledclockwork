using System;

namespace System.Xml.Schema
{
	// Token: 0x0200025D RID: 605
	internal enum AttributeMatchState
	{
		// Token: 0x04000F32 RID: 3890
		AttributeFound,
		// Token: 0x04000F33 RID: 3891
		AnyIdAttributeFound,
		// Token: 0x04000F34 RID: 3892
		UndeclaredElementAndAttribute,
		// Token: 0x04000F35 RID: 3893
		UndeclaredAttribute,
		// Token: 0x04000F36 RID: 3894
		AnyAttributeLax,
		// Token: 0x04000F37 RID: 3895
		AnyAttributeSkip,
		// Token: 0x04000F38 RID: 3896
		ProhibitedAnyAttribute,
		// Token: 0x04000F39 RID: 3897
		ProhibitedAttribute,
		// Token: 0x04000F3A RID: 3898
		AttributeNameMismatch,
		// Token: 0x04000F3B RID: 3899
		ValidateAttributeInvalidCall
	}
}
