using System;

namespace OracleInternal.Common
{
	// Token: 0x0200009D RID: 157
	[Serializable]
	internal struct Transitions
	{
		// Token: 0x04000906 RID: 2310
		internal DateTime m_dateTimeInUtc;

		// Token: 0x04000907 RID: 2311
		internal DateTime m_dateTimeInLocal;

		// Token: 0x04000908 RID: 2312
		internal TimeSpan m_hourOffset;

		// Token: 0x04000909 RID: 2313
		internal byte m_dst;

		// Token: 0x0400090A RID: 2314
		internal TimeSpan m_dstDuration;
	}
}
