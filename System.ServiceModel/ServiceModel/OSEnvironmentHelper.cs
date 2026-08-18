using System;
using System.Net.WebSockets;

namespace System.ServiceModel
{
	// Token: 0x0200004A RID: 74
	internal static class OSEnvironmentHelper
	{
		// Token: 0x06000200 RID: 512 RVA: 0x0000ACE4 File Offset: 0x00008EE4
		static OSEnvironmentHelper()
		{
			int major = Environment.OSVersion.Version.Major;
			int minor = Environment.OSVersion.Version.Minor;
			if (major < 5 || (major == 5 && minor == 0))
			{
				OSEnvironmentHelper.currentVersion = OSVersion.PreWinXP;
			}
			if (major == 5 && minor == 1)
			{
				OSEnvironmentHelper.currentVersion = OSVersion.WinXP;
				return;
			}
			if (major == 5 && minor == 2)
			{
				OSEnvironmentHelper.currentVersion = OSVersion.Win2003;
				return;
			}
			if (major == 6 && minor == 0)
			{
				OSEnvironmentHelper.currentVersion = OSVersion.WinVista;
				return;
			}
			if (major == 6 && minor == 1)
			{
				OSEnvironmentHelper.currentVersion = OSVersion.Win7;
				return;
			}
			if (major == 6 && minor == 2)
			{
				OSEnvironmentHelper.currentVersion = OSVersion.Win8;
				return;
			}
			if (major > 6 || (major == 6 && minor > 2))
			{
				OSEnvironmentHelper.currentVersion = OSVersion.PostWin8;
				return;
			}
			OSEnvironmentHelper.currentVersion = OSVersion.Unknown;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000AD9A File Offset: 0x00008F9A
		internal static bool IsVistaOrGreater
		{
			get
			{
				return OSEnvironmentHelper.IsAtLeast(OSVersion.WinVista);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000ADA2 File Offset: 0x00008FA2
		internal static bool IsApplicationTargeting45
		{
			get
			{
				return WebSocket.IsApplicationTargeting45();
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000ADA9 File Offset: 0x00008FA9
		internal static int ProcessorCount
		{
			get
			{
				return Environment.ProcessorCount;
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		internal static bool IsAtLeast(OSVersion version)
		{
			return OSEnvironmentHelper.IsAtLeast(version, 0);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000ADB9 File Offset: 0x00008FB9
		private static bool IsAtLeast(OSVersion version, byte servicePack)
		{
			if (servicePack == 0)
			{
				return version <= OSEnvironmentHelper.currentVersion;
			}
			if (version == OSEnvironmentHelper.currentVersion)
			{
				return servicePack <= OSEnvironmentHelper.currentServicePack;
			}
			return version < OSEnvironmentHelper.currentVersion;
		}

		// Token: 0x04000291 RID: 657
		private static readonly OSVersion currentVersion;

		// Token: 0x04000292 RID: 658
		private static readonly byte currentServicePack = (byte)Environment.OSVersion.Version.MajorRevision;
	}
}
