using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000915 RID: 2325
	[Flags]
	internal enum MaskingMode
	{
		// Token: 0x0400363A RID: 13882
		None = 0,
		// Token: 0x0400363B RID: 13883
		Handled = 1,
		// Token: 0x0400363C RID: 13884
		Unhandled = 2,
		// Token: 0x0400363D RID: 13885
		All = 3
	}
}
