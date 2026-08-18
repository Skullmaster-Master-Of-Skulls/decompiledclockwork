using System;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004EB RID: 1259
	[Flags]
	internal enum WindowsPenStyle
	{
		// Token: 0x04003608 RID: 13832
		Solid = 0,
		// Token: 0x04003609 RID: 13833
		Dash = 1,
		// Token: 0x0400360A RID: 13834
		Dot = 2,
		// Token: 0x0400360B RID: 13835
		DashDot = 3,
		// Token: 0x0400360C RID: 13836
		DashDotDot = 4,
		// Token: 0x0400360D RID: 13837
		Null = 5,
		// Token: 0x0400360E RID: 13838
		InsideFrame = 6,
		// Token: 0x0400360F RID: 13839
		UserStyle = 7,
		// Token: 0x04003610 RID: 13840
		Alternate = 8,
		// Token: 0x04003611 RID: 13841
		EndcapRound = 0,
		// Token: 0x04003612 RID: 13842
		EndcapSquare = 256,
		// Token: 0x04003613 RID: 13843
		EndcapFlat = 512,
		// Token: 0x04003614 RID: 13844
		JoinRound = 0,
		// Token: 0x04003615 RID: 13845
		JoinBevel = 4096,
		// Token: 0x04003616 RID: 13846
		JoinMiter = 8192,
		// Token: 0x04003617 RID: 13847
		Cosmetic = 0,
		// Token: 0x04003618 RID: 13848
		Geometric = 65536,
		// Token: 0x04003619 RID: 13849
		Default = 0
	}
}
