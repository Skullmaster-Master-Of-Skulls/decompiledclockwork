using System;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x0200021D RID: 541
	[Serializable]
	public enum eRowFormattingConditionType
	{
		// Token: 0x04000E61 RID: 3681
		None,
		// Token: 0x04000E62 RID: 3682
		Equal,
		// Token: 0x04000E63 RID: 3683
		NotEqual,
		// Token: 0x04000E64 RID: 3684
		StartsWith,
		// Token: 0x04000E65 RID: 3685
		EndsWith,
		// Token: 0x04000E66 RID: 3686
		Contains,
		// Token: 0x04000E67 RID: 3687
		DoesNotContain,
		// Token: 0x04000E68 RID: 3688
		Greater,
		// Token: 0x04000E69 RID: 3689
		GreaterOrEqual,
		// Token: 0x04000E6A RID: 3690
		Less,
		// Token: 0x04000E6B RID: 3691
		LessOrEqual,
		// Token: 0x04000E6C RID: 3692
		Between,
		// Token: 0x04000E6D RID: 3693
		NotBetween
	}
}
