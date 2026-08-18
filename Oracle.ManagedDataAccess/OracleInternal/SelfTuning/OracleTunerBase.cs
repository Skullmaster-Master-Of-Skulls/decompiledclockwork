using System;
using System.Threading;
using OracleInternal.Common;

namespace OracleInternal.SelfTuning
{
	// Token: 0x02000195 RID: 405
	internal abstract class OracleTunerBase
	{
		// Token: 0x06000F55 RID: 3925 RVA: 0x000A04C8 File Offset: 0x0009E6C8
		protected virtual void DoInitialize()
		{
			this.m_agentState = OracleTunerState.WAIT;
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x000A04D4 File Offset: 0x0009E6D4
		protected virtual void DoWait()
		{
			this.m_agentState = OracleTunerState.SCAN;
		}

		// Token: 0x06000F57 RID: 3927
		protected abstract void DoScan();

		// Token: 0x06000F58 RID: 3928 RVA: 0x000A04E0 File Offset: 0x0009E6E0
		protected virtual void DoReduce()
		{
			this.m_agentState = OracleTunerState.SCAN;
		}

		// Token: 0x06000F59 RID: 3929
		protected abstract void DoOptimize();

		// Token: 0x06000F5A RID: 3930
		protected abstract void DoWatch();

		// Token: 0x06000F5B RID: 3931 RVA: 0x000A04EC File Offset: 0x0009E6EC
		protected virtual void DoRevert()
		{
			this.m_agentState = OracleTunerState.SCAN;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x000A04F8 File Offset: 0x0009E6F8
		protected virtual void TuningFunction()
		{
			try
			{
				for (;;)
				{
					if (!this.m_tunerEvt.IsSet)
					{
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SelfTuning, new string[]
							{
								"Tuner thread going to sleep."
							});
						}
						this.m_tunerEvt.Wait();
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.SelfTuning, new string[]
							{
								"Tuner thread woken up."
							});
						}
						this.m_agentState = OracleTunerState.INIT;
					}
					switch (this.m_agentState)
					{
					case OracleTunerState.INIT:
						this.DoInitialize();
						break;
					case OracleTunerState.WAIT:
						this.DoWait();
						break;
					case OracleTunerState.SCAN:
						this.DoScan();
						break;
					case OracleTunerState.REDUCE:
						this.DoReduce();
						break;
					case OracleTunerState.OPTIMIZE:
						this.DoOptimize();
						break;
					case OracleTunerState.WATCH:
						this.DoWatch();
						break;
					case OracleTunerState.REVERT:
						this.DoRevert();
						break;
					}
				}
			}
			catch (ThreadAbortException ex)
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268500992, new string[]
					{
						"OracleTunerBase:TuningFunction(): Tuning thread aborted: " + ex.Message
					});
				}
			}
		}

		// Token: 0x04001200 RID: 4608
		protected readonly ManualResetEventSlim m_tunerEvt = new ManualResetEventSlim(false);

		// Token: 0x04001201 RID: 4609
		protected OracleTunerState m_agentState;
	}
}
