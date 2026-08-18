using System;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000011 RID: 17
	internal struct TZInfo
	{
		// Token: 0x06000097 RID: 151 RVA: 0x0000E506 File Offset: 0x0000D506
		internal TZInfo(int tzHours, int tzMinutes)
		{
			this.m_tzHours = tzHours;
			this.m_tzMinutes = tzMinutes;
		}

		// Token: 0x04000075 RID: 117
		internal int m_tzHours;

		// Token: 0x04000076 RID: 118
		internal int m_tzMinutes;
	}
}
