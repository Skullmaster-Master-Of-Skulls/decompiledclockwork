using System;
using System.Diagnostics;
using System.Security.Permissions;
using System.Text;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000076 RID: 118
	internal static class OraclePerfParams
	{
		// Token: 0x0600063E RID: 1598 RVA: 0x00038BB8 File Offset: 0x00036DB8
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		static OraclePerfParams()
		{
			string text = AppDomain.CurrentDomain.FriendlyName;
			string text2 = new StringBuilder().Append("[").Append(Process.GetCurrentProcess().Id).Append(",").Append(AppDomain.CurrentDomain.Id).Append("]").ToString();
			if (text.Length + text2.Length > 40)
			{
				text = text.Substring(0, 40 - text2.Length - 3) + "...";
			}
			OraclePerfParams.m_appDomainPfcInstanceName = new StringBuilder().Append(text).Append(text2).Replace('/', '_').ToString();
		}

		// Token: 0x040006A0 RID: 1696
		internal const byte MAX_COUNTERS = 12;

		// Token: 0x040006A1 RID: 1697
		internal const string HARD_CONNECTS_PER_SECOND = "HardConnectsPerSecond";

		// Token: 0x040006A2 RID: 1698
		internal const string HARD_DISCONNECTS_PER_SECOND = "HardDisconnectsPerSecond";

		// Token: 0x040006A3 RID: 1699
		internal const string SOFT_CONNECTS_PER_SECOND = "SoftConnectsPerSecond";

		// Token: 0x040006A4 RID: 1700
		internal const string SOFT_DISCONNECTS_PER_SECOND = "SoftDisconnectsPerSecond";

		// Token: 0x040006A5 RID: 1701
		internal const string NUMBER_OF_ACTIVE_CONNECTION_POOLS = "NumberOfActiveConnectionPools";

		// Token: 0x040006A6 RID: 1702
		internal const string NUMBER_OF_INACTIVE_CONNECTION_POOLS = "NumberOfInactiveConnectionPools";

		// Token: 0x040006A7 RID: 1703
		internal const string NUMBER_OF_ACTIVE_CONNECTIONS = "NumberOfActiveConnections";

		// Token: 0x040006A8 RID: 1704
		internal const string NUMBER_OF_FREE_CONNECTIONS = "NumberOfFreeConnections";

		// Token: 0x040006A9 RID: 1705
		internal const string NUMBER_OF_POOLED_CONNECTIONS = "NumberOfPooledConnections";

		// Token: 0x040006AA RID: 1706
		internal const string NUMBER_OF_NON_POOLED_CONNECTIONS = "NumberOfNonPooledConnections";

		// Token: 0x040006AB RID: 1707
		internal const string NUMBER_OF_RECLAIMED_CONNECTIONS = "NumberOfReclaimedConnections";

		// Token: 0x040006AC RID: 1708
		internal const string NUMBER_OF_STASIS_CONNECTIONS = "NumberOfStasisConnections";

		// Token: 0x040006AD RID: 1709
		internal const int LENGTH_OF_APPDOMAIN_NAME = 40;

		// Token: 0x040006AE RID: 1710
		internal const int LENGTH_OF_POOL_NAME = 70;

		// Token: 0x040006AF RID: 1711
		internal const int LENGTH_OF_INSTANCE_NAME = 16;

		// Token: 0x040006B0 RID: 1712
		internal const string CATEGORY_NAME = "ODP.NET, Managed Driver";

		// Token: 0x040006B1 RID: 1713
		internal static readonly string m_appDomainPfcInstanceName = string.Empty;

		// Token: 0x02000077 RID: 119
		internal enum CounterIndex
		{
			// Token: 0x040006B3 RID: 1715
			HardConnectsPerSecond,
			// Token: 0x040006B4 RID: 1716
			HardDisconnectsPerSecond,
			// Token: 0x040006B5 RID: 1717
			SoftConnectsPerSecond,
			// Token: 0x040006B6 RID: 1718
			SoftDisconnectsPerSecond,
			// Token: 0x040006B7 RID: 1719
			NumberOfActiveConnectionPools,
			// Token: 0x040006B8 RID: 1720
			NumberOfActiveConnections,
			// Token: 0x040006B9 RID: 1721
			NumberOfFreeConnections,
			// Token: 0x040006BA RID: 1722
			NumberOfInactiveConnectionPools,
			// Token: 0x040006BB RID: 1723
			NumberOfNonPooledConnections,
			// Token: 0x040006BC RID: 1724
			NumberOfPooledConnections,
			// Token: 0x040006BD RID: 1725
			NumberOfReclaimedConnections,
			// Token: 0x040006BE RID: 1726
			NumberOfStasisConnections
		}

		// Token: 0x02000078 RID: 120
		[Flags]
		internal enum Counter : ushort
		{
			// Token: 0x040006C0 RID: 1728
			None = 0,
			// Token: 0x040006C1 RID: 1729
			HardConnectsPerSecond = 1,
			// Token: 0x040006C2 RID: 1730
			HardDisconnectsPerSecond = 2,
			// Token: 0x040006C3 RID: 1731
			SoftConnectsPerSecond = 4,
			// Token: 0x040006C4 RID: 1732
			SoftDisconnectsPerSecond = 8,
			// Token: 0x040006C5 RID: 1733
			NumberOfActiveConnectionPools = 16,
			// Token: 0x040006C6 RID: 1734
			NumberOfInactiveConnectionPools = 32,
			// Token: 0x040006C7 RID: 1735
			NumberOfActiveConnections = 64,
			// Token: 0x040006C8 RID: 1736
			NumberOfFreeConnections = 128,
			// Token: 0x040006C9 RID: 1737
			NumberOfPooledConnections = 256,
			// Token: 0x040006CA RID: 1738
			NumberOfNonPooledConnections = 512,
			// Token: 0x040006CB RID: 1739
			NumberOfReclaimedConnections = 1024,
			// Token: 0x040006CC RID: 1740
			NumberOfStasisConnections = 2048
		}
	}
}
