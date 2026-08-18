using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000171 RID: 369
	[Flags]
	public enum ControlStyles
	{
		// Token: 0x04000927 RID: 2343
		ContainerControl = 1,
		// Token: 0x04000928 RID: 2344
		UserPaint = 2,
		// Token: 0x04000929 RID: 2345
		Opaque = 4,
		// Token: 0x0400092A RID: 2346
		ResizeRedraw = 16,
		// Token: 0x0400092B RID: 2347
		FixedWidth = 32,
		// Token: 0x0400092C RID: 2348
		FixedHeight = 64,
		// Token: 0x0400092D RID: 2349
		StandardClick = 256,
		// Token: 0x0400092E RID: 2350
		Selectable = 512,
		// Token: 0x0400092F RID: 2351
		UserMouse = 1024,
		// Token: 0x04000930 RID: 2352
		SupportsTransparentBackColor = 2048,
		// Token: 0x04000931 RID: 2353
		StandardDoubleClick = 4096,
		// Token: 0x04000932 RID: 2354
		AllPaintingInWmPaint = 8192,
		// Token: 0x04000933 RID: 2355
		CacheText = 16384,
		// Token: 0x04000934 RID: 2356
		EnableNotifyMessage = 32768,
		// Token: 0x04000935 RID: 2357
		[EditorBrowsable(EditorBrowsableState.Never)]
		DoubleBuffer = 65536,
		// Token: 0x04000936 RID: 2358
		OptimizedDoubleBuffer = 131072,
		// Token: 0x04000937 RID: 2359
		UseTextForAccessibility = 262144
	}
}
