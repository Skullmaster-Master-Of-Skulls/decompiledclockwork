using System;

namespace ClockWorkAPI
{
	// Token: 0x0200002E RID: 46
	[Flags]
	public enum DotNetVersions
	{
		// Token: 0x04000133 RID: 307
		NoVersions = 0,
		// Token: 0x04000134 RID: 308
		[RegKey("Software\\Microsoft\\NET Framework Setup\\NDP\\v1.1.4322")]
		DotnetVersion11 = 1,
		// Token: 0x04000135 RID: 309
		[RegKey("Software\\Microsoft\\NET Framework Setup\\NDP\\v2.0.50727")]
		DotnetVersion20 = 2,
		// Token: 0x04000136 RID: 310
		[RegKey("Software\\Microsoft\\NET Framework Setup\\NDP\\v3.0")]
		DotnetVersion30 = 4,
		// Token: 0x04000137 RID: 311
		[RegKey("Software\\Microsoft\\NET Framework Setup\\NDP\\v3.5")]
		DotnetVersion35 = 8
	}
}
