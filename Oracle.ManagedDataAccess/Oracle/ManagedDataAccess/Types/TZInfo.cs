using System;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x02000257 RID: 599
	internal struct TZInfo
	{
		// Token: 0x06001846 RID: 6214 RVA: 0x000FFB98 File Offset: 0x000FDD98
		internal TZInfo(int tzHours, int tzMinutes)
		{
			this.m_tzHours = tzHours;
			this.m_tzMinutes = tzMinutes;
		}

		// Token: 0x04001A75 RID: 6773
		internal int m_tzHours;

		// Token: 0x04001A76 RID: 6774
		internal int m_tzMinutes;
	}
}
