using System;

namespace System.Windows.Forms
{
	// Token: 0x020002DB RID: 731
	[Flags]
	public enum ListViewHitTestLocations
	{
		// Token: 0x04001321 RID: 4897
		None = 1,
		// Token: 0x04001322 RID: 4898
		AboveClientArea = 256,
		// Token: 0x04001323 RID: 4899
		BelowClientArea = 16,
		// Token: 0x04001324 RID: 4900
		LeftOfClientArea = 64,
		// Token: 0x04001325 RID: 4901
		RightOfClientArea = 32,
		// Token: 0x04001326 RID: 4902
		Image = 2,
		// Token: 0x04001327 RID: 4903
		StateImage = 512,
		// Token: 0x04001328 RID: 4904
		Label = 4
	}
}
