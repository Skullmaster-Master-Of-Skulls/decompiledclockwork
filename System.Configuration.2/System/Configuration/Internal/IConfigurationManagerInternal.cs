using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	// Token: 0x020000B0 RID: 176
	[ComVisible(false)]
	public interface IConfigurationManagerInternal
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060006F0 RID: 1776
		bool SupportsUserConfig { get; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060006F1 RID: 1777
		bool SetConfigurationSystemInProgress { get; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060006F2 RID: 1778
		string MachineConfigPath { get; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060006F3 RID: 1779
		string ApplicationConfigUri { get; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060006F4 RID: 1780
		string ExeProductName { get; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060006F5 RID: 1781
		string ExeProductVersion { get; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060006F6 RID: 1782
		string ExeRoamingConfigDirectory { get; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060006F7 RID: 1783
		string ExeRoamingConfigPath { get; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060006F8 RID: 1784
		string ExeLocalConfigDirectory { get; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060006F9 RID: 1785
		string ExeLocalConfigPath { get; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060006FA RID: 1786
		string UserConfigFilename { get; }
	}
}
