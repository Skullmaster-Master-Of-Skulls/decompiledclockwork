using System;

namespace Spire.Xls.Core
{
	// Token: 0x02000053 RID: 83
	public interface IRadioButton : ITextBoxShape
	{
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000820 RID: 2080
		// (set) Token: 0x06000821 RID: 2081
		CheckState CheckState { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000822 RID: 2082
		bool IsFirstButton { get; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000823 RID: 2083
		// (set) Token: 0x06000824 RID: 2084
		bool Display3DShading { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000825 RID: 2085
		// (set) Token: 0x06000826 RID: 2086
		IXLSRange LinkedCell { get; set; }
	}
}
