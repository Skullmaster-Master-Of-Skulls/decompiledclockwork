using System;

namespace WebGrease.Css.ImageAssemblyAnalysis.LogModel
{
	// Token: 0x0200018F RID: 399
	public enum FailureReason
	{
		// Token: 0x04000B08 RID: 2824
		IncorrectPosition,
		// Token: 0x04000B09 RID: 2825
		BackgroundSizeIsSetToNonDefaultValue,
		// Token: 0x04000B0A RID: 2826
		InvalidDpi,
		// Token: 0x04000B0B RID: 2827
		BackgroundRepeatInvalid,
		// Token: 0x04000B0C RID: 2828
		MultipleUrls,
		// Token: 0x04000B0D RID: 2829
		NoRepeat,
		// Token: 0x04000B0E RID: 2830
		NoUrl,
		// Token: 0x04000B0F RID: 2831
		IgnoreUrl,
		// Token: 0x04000B10 RID: 2832
		SpritingIgnore
	}
}
