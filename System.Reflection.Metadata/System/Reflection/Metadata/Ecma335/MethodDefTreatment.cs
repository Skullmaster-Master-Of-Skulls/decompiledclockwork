using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000111 RID: 273
	[Flags]
	internal enum MethodDefTreatment : byte
	{
		// Token: 0x04000804 RID: 2052
		None = 0,
		// Token: 0x04000805 RID: 2053
		KindMask = 15,
		// Token: 0x04000806 RID: 2054
		Other = 1,
		// Token: 0x04000807 RID: 2055
		DelegateMethod = 2,
		// Token: 0x04000808 RID: 2056
		AttributeMethod = 3,
		// Token: 0x04000809 RID: 2057
		InterfaceMethod = 4,
		// Token: 0x0400080A RID: 2058
		Implementation = 5,
		// Token: 0x0400080B RID: 2059
		HiddenInterfaceImplementation = 6,
		// Token: 0x0400080C RID: 2060
		DisposeMethod = 7,
		// Token: 0x0400080D RID: 2061
		MarkAbstractFlag = 16,
		// Token: 0x0400080E RID: 2062
		MarkPublicFlag = 32
	}
}
