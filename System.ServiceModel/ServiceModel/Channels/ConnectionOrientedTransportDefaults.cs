using System;
using System.Net.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000790 RID: 1936
	internal static class ConnectionOrientedTransportDefaults
	{
		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x060049A3 RID: 18851 RVA: 0x0010EC23 File Offset: 0x0010CE23
		internal static TimeSpan IdleTimeout
		{
			get
			{
				return TimeSpanHelper.FromMinutes(2, "00:02:00");
			}
		}

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x060049A4 RID: 18852 RVA: 0x0010EC30 File Offset: 0x0010CE30
		internal static TimeSpan ChannelInitializationTimeout
		{
			get
			{
				return TimeSpanHelper.FromSeconds(30, "00:00:30");
			}
		}

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x060049A5 RID: 18853 RVA: 0x0010EC3E File Offset: 0x0010CE3E
		internal static TimeSpan MaxOutputDelay
		{
			get
			{
				return TimeSpanHelper.FromMilliseconds(200, "00:00:00.2");
			}
		}

		// Token: 0x060049A6 RID: 18854 RVA: 0x0010EC4F File Offset: 0x0010CE4F
		internal static int GetMaxConnections()
		{
			return ConnectionOrientedTransportDefaults.GetMaxPendingConnections();
		}

		// Token: 0x060049A7 RID: 18855 RVA: 0x0010EC56 File Offset: 0x0010CE56
		internal static int GetMaxPendingConnections()
		{
			if (OSEnvironmentHelper.IsApplicationTargeting45)
			{
				return 12 * OSEnvironmentHelper.ProcessorCount;
			}
			return 10;
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x0010EC6A File Offset: 0x0010CE6A
		internal static int GetMaxPendingAccepts()
		{
			if (OSEnvironmentHelper.IsApplicationTargeting45)
			{
				return 2 * OSEnvironmentHelper.ProcessorCount;
			}
			return 1;
		}

		// Token: 0x04002E7A RID: 11898
		internal const bool AllowNtlm = true;

		// Token: 0x04002E7B RID: 11899
		internal const int ConnectionBufferSize = 8192;

		// Token: 0x04002E7C RID: 11900
		internal const string ConnectionPoolGroupName = "default";

		// Token: 0x04002E7D RID: 11901
		internal const HostNameComparisonMode HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;

		// Token: 0x04002E7E RID: 11902
		internal const string IdleTimeoutString = "00:02:00";

		// Token: 0x04002E7F RID: 11903
		internal const string ChannelInitializationTimeoutString = "00:00:30";

		// Token: 0x04002E80 RID: 11904
		internal const int MaxContentTypeSize = 256;

		// Token: 0x04002E81 RID: 11905
		internal const int MaxOutboundConnectionsPerEndpoint = 10;

		// Token: 0x04002E82 RID: 11906
		internal const int MaxPendingConnectionsConst = 0;

		// Token: 0x04002E83 RID: 11907
		internal const string MaxOutputDelayString = "00:00:00.2";

		// Token: 0x04002E84 RID: 11908
		internal const int MaxPendingAcceptsConst = 0;

		// Token: 0x04002E85 RID: 11909
		internal const int MaxViaSize = 2048;

		// Token: 0x04002E86 RID: 11910
		internal const ProtectionLevel ProtectionLevel = ProtectionLevel.EncryptAndSign;

		// Token: 0x04002E87 RID: 11911
		internal const TransferMode TransferMode = TransferMode.Buffered;

		// Token: 0x04002E88 RID: 11912
		private const int MaxPendingConnectionsPre45 = 10;

		// Token: 0x04002E89 RID: 11913
		private const int MaxPendingAcceptsPre45 = 1;
	}
}
