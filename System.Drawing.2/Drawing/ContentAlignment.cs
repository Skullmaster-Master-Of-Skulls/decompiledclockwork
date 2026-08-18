using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Drawing
{
	// Token: 0x02000019 RID: 25
	[Editor("System.Drawing.Design.ContentAlignmentEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public enum ContentAlignment
	{
		// Token: 0x0400015A RID: 346
		TopLeft = 1,
		// Token: 0x0400015B RID: 347
		TopCenter,
		// Token: 0x0400015C RID: 348
		TopRight = 4,
		// Token: 0x0400015D RID: 349
		MiddleLeft = 16,
		// Token: 0x0400015E RID: 350
		MiddleCenter = 32,
		// Token: 0x0400015F RID: 351
		MiddleRight = 64,
		// Token: 0x04000160 RID: 352
		BottomLeft = 256,
		// Token: 0x04000161 RID: 353
		BottomCenter = 512,
		// Token: 0x04000162 RID: 354
		BottomRight = 1024
	}
}
