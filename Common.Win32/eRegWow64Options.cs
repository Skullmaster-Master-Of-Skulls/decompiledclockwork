using System;

namespace TechnoPro.Common.Win32
{
	// Token: 0x0200000D RID: 13
	[Flags]
	public enum eRegWow64Options
	{
		// Token: 0x04000024 RID: 36
		None = 0,
		// Token: 0x04000025 RID: 37
		KEY_WOW64_64KEY = 256,
		// Token: 0x04000026 RID: 38
		KEY_WOW64_32KEY = 512
	}
}
