using System;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000CD RID: 205
	internal class CriteriaCtx
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x00054B20 File Offset: 0x00052D20
		internal CriteriaCtx()
		{
			this.m_criteriaIds = new uint[3];
			this.m_criteriaIds[(int)((UIntPtr)0)] = 0U;
			this.m_criteriaIds[(int)((UIntPtr)1)] = 0U;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00054B50 File Offset: 0x00052D50
		internal bool CanReturnBestMatchingPR()
		{
			return this.m_bfoundPRMatchingAllCrit || this.m_bBestMatchPRHasAllMustCrit;
		}

		// Token: 0x04000AD1 RID: 2769
		internal string m_connectionClass;

		// Token: 0x04000AD2 RID: 2770
		internal string m_pdbName;

		// Token: 0x04000AD3 RID: 2771
		internal string m_serviceName;

		// Token: 0x04000AD4 RID: 2772
		internal string m_edition;

		// Token: 0x04000AD5 RID: 2773
		internal byte m_bDrcpPurityNew;

		// Token: 0x04000AD6 RID: 2774
		internal int m_drcpEnabled = -1;

		// Token: 0x04000AD7 RID: 2775
		internal uint[] m_criteriaIds;

		// Token: 0x04000AD8 RID: 2776
		internal bool m_bBestMatchPRHasAllMustCrit;

		// Token: 0x04000AD9 RID: 2777
		internal bool m_bfoundPRMatchingAllCrit;

		// Token: 0x04000ADA RID: 2778
		internal bool m_bNewConCreated;

		// Token: 0x04000ADB RID: 2779
		internal bool m_bPrelimAuthSession;

		// Token: 0x04000ADC RID: 2780
		internal bool m_serviceSwitchRequested;

		// Token: 0x04000ADD RID: 2781
		internal bool m_fromMTS;
	}
}
