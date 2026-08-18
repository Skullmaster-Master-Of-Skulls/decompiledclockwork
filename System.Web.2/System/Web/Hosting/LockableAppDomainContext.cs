using System;

namespace System.Web.Hosting
{
	// Token: 0x020007A6 RID: 1958
	internal class LockableAppDomainContext
	{
		// Token: 0x17001B17 RID: 6935
		// (get) Token: 0x06005CC9 RID: 23753 RVA: 0x00140BCA File Offset: 0x0013EDCA
		// (set) Token: 0x06005CCA RID: 23754 RVA: 0x00140BD2 File Offset: 0x0013EDD2
		internal HostingEnvironment HostEnv { get; set; }

		// Token: 0x17001B18 RID: 6936
		// (get) Token: 0x06005CCB RID: 23755 RVA: 0x00140BDB File Offset: 0x0013EDDB
		// (set) Token: 0x06005CCC RID: 23756 RVA: 0x00140BE3 File Offset: 0x0013EDE3
		internal string PreloadContext { get; set; }

		// Token: 0x17001B19 RID: 6937
		// (get) Token: 0x06005CCD RID: 23757 RVA: 0x00140BEC File Offset: 0x0013EDEC
		// (set) Token: 0x06005CCE RID: 23758 RVA: 0x00140BF4 File Offset: 0x0013EDF4
		internal bool RetryingPreload { get; set; }

		// Token: 0x06005CCF RID: 23759 RVA: 0x000030B5 File Offset: 0x000012B5
		internal LockableAppDomainContext()
		{
		}
	}
}
