using System;

namespace System.Xml.Schema
{
	// Token: 0x02000213 RID: 531
	internal enum AttributeMatchState
	{
		// Token: 0x04000EE4 RID: 3812
		AttributeFound,
		// Token: 0x04000EE5 RID: 3813
		AnyIdAttributeFound,
		// Token: 0x04000EE6 RID: 3814
		UndeclaredElementAndAttribute,
		// Token: 0x04000EE7 RID: 3815
		UndeclaredAttribute,
		// Token: 0x04000EE8 RID: 3816
		AnyAttributeLax,
		// Token: 0x04000EE9 RID: 3817
		AnyAttributeSkip,
		// Token: 0x04000EEA RID: 3818
		ProhibitedAnyAttribute,
		// Token: 0x04000EEB RID: 3819
		ProhibitedAttribute,
		// Token: 0x04000EEC RID: 3820
		AttributeNameMismatch,
		// Token: 0x04000EED RID: 3821
		ValidateAttributeInvalidCall
	}
}
