using System;
using System.Data;
using System.Data.Common;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000088 RID: 136
	public sealed class OracleTransaction : DbTransaction
	{
		// Token: 0x06000604 RID: 1540 RVA: 0x0003F80C File Offset: 0x0003E80C
		static OracleTransaction()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0003F81A File Offset: 0x0003E81A
		protected override DbConnection DbConnection
		{
			get
			{
				return this.m_connection;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0003F822 File Offset: 0x0003E822
		public new OracleConnection Connection
		{
			get
			{
				return this.m_connection;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0003F82A File Offset: 0x0003E82A
		public override IsolationLevel IsolationLevel
		{
			get
			{
				if (this.m_completed)
				{
					throw new InvalidOperationException();
				}
				return this.m_isolationLevel;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0003F840 File Offset: 0x0003E840
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x0003F848 File Offset: 0x0003E848
		internal bool Completed
		{
			get
			{
				return this.m_completed;
			}
			set
			{
				this.m_completed = value;
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0003F854 File Offset: 0x0003E854
		public unsafe override void Commit()
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTransaction::Commit()\n"
				});
			}
			if (this.m_completed)
			{
				throw new InvalidOperationException();
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			try
			{
				this.m_pOpoTxnValCtx->ErrHndAllocated = 1;
				num = OpsTxn.Commit(this.m_opsConCtx, this.m_opsErrCtx, this.m_pOpoTxnValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			this.m_completed = true;
			this.Dispose();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTransaction::Commit()\n"
				});
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0003F980 File Offset: 0x0003E980
		public void Save(string savepointName)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTransaction::Save()\n"
				});
			}
			if (this.m_completed)
			{
				throw new InvalidOperationException();
			}
			if (this.m_command == null)
			{
				this.m_command = new OracleCommand("", this.m_connection);
			}
			this.m_command.CommandText = "SAVEPOINT " + savepointName;
			this.m_command.CommandTimeout = 0;
			this.m_command.ExecuteNonQuery();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTransaction::Save()\n"
				});
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0003FA24 File Offset: 0x0003EA24
		public unsafe override void Rollback()
		{
			int num = 0;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTransaction::Rollback(1)\n"
				});
			}
			if (this.m_completed)
			{
				throw new InvalidOperationException();
			}
			if (this.m_connection.m_opoConCtx.opsConCtx == IntPtr.Zero)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			if (this.m_connection.m_conSignature != this.m_conSignature)
			{
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_REOPENED, new string[0]));
			}
			try
			{
				this.m_pOpoTxnValCtx->ErrHndAllocated = 1;
				this.m_pOpoTxnValCtx->ForceDispose = 0;
				num = OpsTxn.Rollback(this.m_opsConCtx, this.m_opsErrCtx, this.m_pOpoTxnValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			finally
			{
				if (num != 0)
				{
					OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
				}
			}
			this.m_completed = true;
			this.Dispose();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTransaction::Rollback(1)\n"
				});
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0003FB5C File Offset: 0x0003EB5C
		public void Rollback(string savepointName)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTransaction::Rollback(2)\n"
				});
			}
			if (this.m_completed)
			{
				throw new InvalidOperationException();
			}
			if (this.m_command == null)
			{
				this.m_command = new OracleCommand("", this.m_connection);
			}
			this.m_command.CommandText = "ROLLBACK TO SAVEPOINT " + savepointName;
			this.m_command.CommandTimeout = 0;
			this.m_command.ExecuteNonQuery();
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTransaction::Rollback(2)\n"
				});
			}
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0003FC00 File Offset: 0x0003EC00
		public new void Dispose()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleTransaction::Dispose()\n"
				});
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleTransaction::Dispose()\n"
				});
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0003FC54 File Offset: 0x0003EC54
		protected unsafe override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (!this.m_completed)
				{
					try
					{
						if (this.m_pOpoTxnValCtx != null)
						{
							this.m_pOpoTxnValCtx->ErrHndAllocated = 1;
							this.m_pOpoTxnValCtx->ForceDispose = 1;
						}
						if (this.m_connection.m_opoConCtx.opsConCtx != IntPtr.Zero && this.m_opsErrCtx != IntPtr.Zero && this.m_pOpoTxnValCtx != null)
						{
							OpsTxn.Rollback(this.m_opsConCtx, this.m_opsErrCtx, this.m_pOpoTxnValCtx);
						}
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
					}
					this.m_completed = true;
				}
				try
				{
					if (this.m_pOpoTxnValCtx != null)
					{
						this.m_pOpoTxnValCtx->ErrHndAllocated = 1;
					}
					OpsTxn.Dispose(this.m_opsErrCtx, this.m_pOpoTxnValCtx);
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
				}
				try
				{
					if (this.m_opsConCtx != IntPtr.Zero)
					{
						OpsCon.RelRef(ref this.m_opsConCtx);
					}
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
				}
				this.m_pOpoTxnValCtx = null;
				this.m_opsConCtx = IntPtr.Zero;
				this.m_opsErrCtx = IntPtr.Zero;
				if (!this.m_disabled)
				{
					try
					{
						this.m_connection.EndTransaction();
					}
					catch
					{
					}
					this.m_disabled = true;
				}
				if (disposing)
				{
					this.m_connection = null;
					if (this.m_command != null)
					{
						try
						{
							this.m_command.Dispose();
						}
						catch
						{
						}
						this.m_command = null;
					}
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0003FE18 File Offset: 0x0003EE18
		internal unsafe OracleTransaction(OracleConnection connection, IsolationLevel isolationLevel, int txnHndAllocated)
		{
			int num = 0;
			this.m_connection = connection;
			this.m_isolationLevel = isolationLevel;
			this.m_conSignature = this.m_connection.m_conSignature;
			this.m_opsConCtx = this.m_connection.m_opoConCtx.opsConCtx;
			if (this.m_opsConCtx == IntPtr.Zero)
			{
				GC.SuppressFinalize(this);
				throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
			}
			try
			{
				int num2 = OpsCon.AddRef(this.m_opsConCtx);
				if (num2 <= 1)
				{
					throw new InvalidOperationException(OpoErrResManager.GetErrorMesg(ErrRes.CON_CLOSED, new string[0]));
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				GC.SuppressFinalize(this);
				throw;
			}
			try
			{
				num = OpsTxn.AllocValCtx(ref this.m_pOpoTxnValCtx);
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				num = ErrRes.INT_ERR;
				GC.SuppressFinalize(this);
				throw;
			}
			finally
			{
				if (num != 0)
				{
					try
					{
						OpsCon.RelRef(ref this.m_opsConCtx);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
					}
					if (num != ErrRes.INT_ERR)
					{
						GC.SuppressFinalize(this);
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
			}
			this.m_pOpoTxnValCtx->TxnHndAllocated = txnHndAllocated;
			if (this.m_isolationLevel == IsolationLevel.Serializable)
			{
				this.m_pOpoTxnValCtx->Serializable = 1;
			}
			else
			{
				this.m_pOpoTxnValCtx->Serializable = 0;
			}
			if (this.m_opsErrCtx == IntPtr.Zero)
			{
				this.m_pOpoTxnValCtx->ErrHndAllocated = 0;
			}
			try
			{
				num = OpsTxn.Begin(this.m_opsConCtx, out this.m_opsErrCtx, this.m_pOpoTxnValCtx);
			}
			catch (Exception ex4)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex4);
				}
				num = ErrRes.INT_ERR;
				GC.SuppressFinalize(this);
				throw;
			}
			finally
			{
				this.m_connection.TxnHndAllocated = this.m_pOpoTxnValCtx->TxnHndAllocated;
				if (num != 0)
				{
					try
					{
						OpsTxn.FreeValCtx(this.m_pOpoTxnValCtx);
						this.m_pOpoTxnValCtx = null;
					}
					catch (Exception ex5)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex5);
						}
					}
					try
					{
						OpsCon.RelRef(ref this.m_opsConCtx);
					}
					catch (Exception ex6)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex6);
						}
					}
					if (num != ErrRes.INT_ERR)
					{
						GC.SuppressFinalize(this);
						OracleException.HandleError(num, this.m_connection, this.m_opsErrCtx, this);
					}
				}
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x000400B4 File Offset: 0x0003F0B4
		~OracleTransaction()
		{
			this.Dispose(false);
		}

		// Token: 0x040003D8 RID: 984
		private OracleConnection m_connection;

		// Token: 0x040003D9 RID: 985
		private IsolationLevel m_isolationLevel;

		// Token: 0x040003DA RID: 986
		private OracleCommand m_command;

		// Token: 0x040003DB RID: 987
		private bool m_completed;

		// Token: 0x040003DC RID: 988
		private bool m_disposed;

		// Token: 0x040003DD RID: 989
		private IntPtr m_opsConCtx;

		// Token: 0x040003DE RID: 990
		private IntPtr m_opsErrCtx;

		// Token: 0x040003DF RID: 991
		private bool m_disabled;

		// Token: 0x040003E0 RID: 992
		private unsafe OpoTxnValCtx* m_pOpoTxnValCtx;

		// Token: 0x040003E1 RID: 993
		private int m_conSignature;
	}
}
