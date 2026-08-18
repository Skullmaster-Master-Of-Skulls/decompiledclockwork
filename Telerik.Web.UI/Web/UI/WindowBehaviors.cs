using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001368 RID: 4968
	[Flags]
	public enum WindowBehaviors
	{
		// Token: 0x0400379B RID: 14235
		None = 0,
		// Token: 0x0400379C RID: 14236
		Resize = 1,
		// Token: 0x0400379D RID: 14237
		Minimize = 2,
		// Token: 0x0400379E RID: 14238
		Close = 4,
		// Token: 0x0400379F RID: 14239
		Pin = 8,
		// Token: 0x040037A0 RID: 14240
		Maximize = 16,
		// Token: 0x040037A1 RID: 14241
		Move = 32,
		// Token: 0x040037A2 RID: 14242
		Reload = 64,
		// Token: 0x040037A3 RID: 14243
		Default = 127
	}
}
