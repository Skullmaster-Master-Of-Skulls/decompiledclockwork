using System;
using System.Runtime.InteropServices;
using System.Transactions;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000F1 RID: 241
	internal class PromotableTxnMgr : IPromotableSinglePhaseNotification, ITransactionPromoter
	{
		// Token: 0x060008D0 RID: 2256 RVA: 0x00057EC0 File Offset: 0x00056EC0
		protected override void Finalize()
		{
			try
			{
				if (null != this.m_pOpoConValCtx)
				{
					try
					{
						OpsCon.FreeValCtx(ref this.m_pOpoConValCtx);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
					}
				}
			}
			catch
			{
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00057F2C File Offset: 0x00056F2C
		internal PromotableTxnMgr()
		{
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00057F34 File Offset: 0x00056F34
		public void Initialize()
		{
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00057F38 File Offset: 0x00056F38
		public void CommitTransaction()
		{
			try
			{
				if (!string.IsNullOrEmpty(this.m_localTxnIdentifier))
				{
					if (!this.m_bLocalTxnPromoted)
					{
						if (this.m_oraTransaction == null)
						{
							goto IL_67;
						}
						try
						{
							OpsTxn.Commit(this.m_opsConCtx, this.m_opsErrCtx, null);
							goto IL_67;
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
							throw;
						}
					}
					try
					{
						OpsCon.CommitPromotedTxn(this.m_opsConCtx, this.m_pOpoConValCtx);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
						throw;
					}
				}
				IL_67:;
			}
			finally
			{
				this.CloseNonPooledConnection();
				if (this.m_oraTransaction != null)
				{
					this.m_oraTransaction.Completed = true;
					this.m_oraTransaction.Dispose();
					this.m_oraTransaction = null;
				}
				string localTxnIdentifier = this.m_localTxnIdentifier;
				if (!string.IsNullOrEmpty(localTxnIdentifier))
				{
					OracleConnection.m_pspePrimaryResourceEntry.Remove(localTxnIdentifier);
					this.m_localTxnIdentifier = null;
				}
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00058024 File Offset: 0x00057024
		public void SinglePhaseCommit(SinglePhaseEnlistment spe)
		{
			try
			{
				this.CommitTransaction();
			}
			finally
			{
				spe.Committed();
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00058050 File Offset: 0x00057050
		internal void RollbackTransaction()
		{
			try
			{
				if (!string.IsNullOrEmpty(this.m_localTxnIdentifier))
				{
					if (!this.m_bLocalTxnPromoted)
					{
						if (this.m_oraTransaction == null)
						{
							goto IL_67;
						}
						try
						{
							OpsTxn.Rollback(this.m_opsConCtx, this.m_opsErrCtx, null);
							goto IL_67;
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
							throw;
						}
					}
					try
					{
						OpsCon.AbortPromotedTxn(this.m_opsConCtx, this.m_pOpoConValCtx);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
						throw;
					}
				}
				IL_67:;
			}
			finally
			{
				this.CloseNonPooledConnection();
				if (this.m_oraTransaction != null)
				{
					this.m_oraTransaction.Completed = true;
					this.m_oraTransaction.Dispose();
					this.m_oraTransaction = null;
				}
				string localTxnIdentifier = this.m_localTxnIdentifier;
				if (!string.IsNullOrEmpty(localTxnIdentifier))
				{
					OracleConnection.m_pspePrimaryResourceEntry.Remove(localTxnIdentifier);
					this.m_localTxnIdentifier = null;
				}
			}
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0005813C File Offset: 0x0005713C
		private void CloseNonPooledConnection()
		{
			try
			{
				if (!string.IsNullOrEmpty(this.m_localTxnIdentifier))
				{
					object obj = ConnectionDispenser.m_pspePrimaryResources[this.m_localTxnIdentifier];
					if (obj != null)
					{
						ConnectionDispenser.m_pspePrimaryResources.Remove(this.m_localTxnIdentifier);
						OpoConCtx opoConCtx = obj as OpoConCtx;
						opoConCtx.m_txnType = TxnType.None;
						if (this.m_bLocalTxnPromoted)
						{
							try
							{
								OpsCon.DelistPromotedTxn(opoConCtx.opsConCtx);
							}
							catch (Exception ex)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex);
								}
							}
						}
						ConnectionDispenser.Close(ref opoConCtx, false);
						if (null != opoConCtx.pOpoConValCtx)
						{
							try
							{
								OpsCon.FreeValCtx(ref opoConCtx.pOpoConValCtx);
							}
							catch (Exception ex2)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex2);
								}
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00058210 File Offset: 0x00057210
		public void Rollback(SinglePhaseEnlistment spe)
		{
			try
			{
				this.RollbackTransaction();
			}
			finally
			{
				spe.Aborted();
			}
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0005823C File Offset: 0x0005723C
		public unsafe byte[] Promote()
		{
			int num = 0;
			byte[] array = null;
			try
			{
				num = OpsCon.Promote(this.m_opsConCtx, this.m_pOpoConValCtx, this.m_opoConRefCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
			}
			if (num == 0 && ErrRes.INT_ERR != num && this.m_pOpoConValCtx->token_size > 0)
			{
				array = new byte[this.m_pOpoConValCtx->token_size];
				Marshal.Copy(this.m_pOpoConValCtx->token, array, 0, this.m_pOpoConValCtx->token_size);
				this.m_bLocalTxnPromoted = true;
			}
			return array;
		}

		// Token: 0x04000797 RID: 1943
		internal IntPtr m_opsConCtx;

		// Token: 0x04000798 RID: 1944
		internal IntPtr m_opsErrCtx;

		// Token: 0x04000799 RID: 1945
		internal OpoConRefCtx m_opoConRefCtx;

		// Token: 0x0400079A RID: 1946
		internal unsafe OpoConValCtx* m_pOpoConValCtx;

		// Token: 0x0400079B RID: 1947
		internal OracleTransaction m_oraTransaction;

		// Token: 0x0400079C RID: 1948
		internal bool m_bLocalTxnPromoted;

		// Token: 0x0400079D RID: 1949
		internal string m_localTxnIdentifier;
	}
}
