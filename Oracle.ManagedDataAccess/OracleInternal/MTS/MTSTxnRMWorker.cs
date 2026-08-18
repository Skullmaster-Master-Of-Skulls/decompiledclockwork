using System;
using OracleInternal.Common;

namespace OracleInternal.MTS
{
	// Token: 0x0200013C RID: 316
	internal class MTSTxnRMWorker
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000C9E RID: 3230 RVA: 0x0008BE9C File Offset: 0x0008A09C
		// (remove) Token: 0x06000C9F RID: 3231 RVA: 0x0008BED4 File Offset: 0x0008A0D4
		public event OnPrepareEventHandler PrepareEvent;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000CA0 RID: 3232 RVA: 0x0008BF0C File Offset: 0x0008A10C
		// (remove) Token: 0x06000CA1 RID: 3233 RVA: 0x0008BF44 File Offset: 0x0008A144
		public event OnCommitEventHandler CommitEvent;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000CA2 RID: 3234 RVA: 0x0008BF7C File Offset: 0x0008A17C
		// (remove) Token: 0x06000CA3 RID: 3235 RVA: 0x0008BFB4 File Offset: 0x0008A1B4
		public event OnAbortEventHandler AbortEvent;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000CA4 RID: 3236 RVA: 0x0008BFEC File Offset: 0x0008A1EC
		// (remove) Token: 0x06000CA5 RID: 3237 RVA: 0x0008C024 File Offset: 0x0008A224
		public event OnSinglePhaseEventHandler SinglePhaseEvent;

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0008C064 File Offset: 0x0008A264
		~MTSTxnRMWorker()
		{
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0008C08C File Offset: 0x0008A28C
		public void OnPrepare(OnPrepareEventArgs e)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[0]);
			}
			this.PrepareEvent(this, e);
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[0]);
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0008C0CC File Offset: 0x0008A2CC
		public void OnCommit(OnCommitEventArgs e)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[0]);
			}
			this.CommitEvent(this, e);
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[0]);
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0008C10C File Offset: 0x0008A30C
		public void OnAbort(OnAbortEventArgs e)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[0]);
			}
			this.AbortEvent(this, e);
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[0]);
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0008C14C File Offset: 0x0008A34C
		public void OnSinglePhase(OnSinglePhaseEventArgs e)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[0]);
			}
			this.SinglePhaseEvent(this, e);
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[0]);
			}
		}
	}
}
