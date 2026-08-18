using System;
using System.Globalization;

namespace System.ServiceModel.Activation.Configuration
{
	// Token: 0x020005D4 RID: 1492
	internal static class ConfigurationStrings
	{
		// Token: 0x060039F9 RID: 14841 RVA: 0x000DFCDD File Offset: 0x000DDEDD
		private static string GetSectionPath(string sectionName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				"system.serviceModel.activation",
				sectionName
			});
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x060039FA RID: 14842 RVA: 0x000DFD00 File Offset: 0x000DDF00
		internal static string DiagnosticSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("diagnostics");
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x060039FB RID: 14843 RVA: 0x000DFD0C File Offset: 0x000DDF0C
		internal static string NetTcpSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("net.tcp");
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x060039FC RID: 14844 RVA: 0x000DFD18 File Offset: 0x000DDF18
		internal static string NetPipeSectionPath
		{
			get
			{
				return ConfigurationStrings.GetSectionPath("net.pipe");
			}
		}

		// Token: 0x04002A37 RID: 10807
		internal const string SectionGroupName = "system.serviceModel.activation";

		// Token: 0x04002A38 RID: 10808
		internal const string IIS_IUSRSSid = "S-1-5-32-568";

		// Token: 0x04002A39 RID: 10809
		internal const string DiagnosticSectionName = "diagnostics";

		// Token: 0x04002A3A RID: 10810
		internal const string NetTcpSectionName = "net.tcp";

		// Token: 0x04002A3B RID: 10811
		internal const string NetPipeSectionName = "net.pipe";

		// Token: 0x04002A3C RID: 10812
		internal const string AllowAccounts = "allowAccounts";

		// Token: 0x04002A3D RID: 10813
		internal const string Enabled = "enabled";

		// Token: 0x04002A3E RID: 10814
		internal const string ListenBacklog = "listenBacklog";

		// Token: 0x04002A3F RID: 10815
		internal const string MaxPendingAccepts = "maxPendingAccepts";

		// Token: 0x04002A40 RID: 10816
		internal const string MaxPendingConnections = "maxPendingConnections";

		// Token: 0x04002A41 RID: 10817
		internal const string PerformanceCountersEnabled = "performanceCountersEnabled";

		// Token: 0x04002A42 RID: 10818
		internal const string ReceiveTimeout = "receiveTimeout";

		// Token: 0x04002A43 RID: 10819
		internal const string SecurityIdentifier = "securityIdentifier";

		// Token: 0x04002A44 RID: 10820
		internal const string TeredoEnabled = "teredoEnabled";

		// Token: 0x04002A45 RID: 10821
		internal const string TimeSpanOneTick = "00:00:00.0000001";

		// Token: 0x04002A46 RID: 10822
		internal const string TimeSpanZero = "00:00:00";
	}
}
