using System;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000258 RID: 600
	internal struct OracleTimeZoneInfo
	{
		// Token: 0x06001847 RID: 6215 RVA: 0x000FFBA8 File Offset: 0x000FDDA8
		internal OracleTimeZoneInfo(int zoneId, int tzHours, int tzMinutes)
		{
			this.m_zoneId = zoneId;
			this.m_tzHours = tzHours;
			this.m_tzMinutes = tzMinutes;
		}

		// Token: 0x04001A77 RID: 6775
		internal int m_zoneId;

		// Token: 0x04001A78 RID: 6776
		internal int m_tzHours;

		// Token: 0x04001A79 RID: 6777
		internal int m_tzMinutes;
	}
}
