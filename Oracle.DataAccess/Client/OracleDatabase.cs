using System;
using System.Security.Permissions;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000036 RID: 54
	[SecurityPermission(SecurityAction.Assert, ControlThread = true)]
	public sealed class OracleDatabase : IDisposable
	{
		// Token: 0x0600024C RID: 588 RVA: 0x0001C534 File Offset: 0x0001B534
		internal void Open()
		{
			try
			{
				this.m_con.m_bPrelimAuthSession = false;
				this.m_con.Open();
			}
			catch
			{
				this.m_con.m_bPrelimAuthSession = true;
				this.m_con.Open();
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0001C584 File Offset: 0x0001B584
		public OracleDatabase(string connectionString)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDatabase()\n"
				});
			}
			try
			{
				this.m_con = new OracleConnection(connectionString);
				this.m_con.m_bStartupShutdown = true;
				this.m_cmd = new OracleCommand("", this.m_con);
				this.m_encryptedConString = new EncryptedPassword(connectionString);
				this.Open();
			}
			finally
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDatabase()\n"
					});
				}
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0001C624 File Offset: 0x0001B624
		public void Startup()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDatabase::StartupDB(1)\n"
				});
			}
			try
			{
				this.Startup(OracleDBStartupMode.NoRestriction, null, true);
			}
			finally
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDatabase::StartupDB(1)\n"
					});
				}
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0001C688 File Offset: 0x0001B688
		public unsafe void Startup(OracleDBStartupMode startupMode, string pfile, bool bMountAndOpen)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDatabase::StartupDB(4)\n"
				});
			}
			try
			{
				int num = 0;
				int num2 = 0;
				if (pfile == null || pfile == "")
				{
					pfile = null;
				}
				this.m_con.m_opoConCtx.pOpoConValCtx->OracleDBStartupMode = (int)startupMode;
				try
				{
					num = OpsCon.StartupDB(this.m_con.m_opoConCtx.opsConCtx, this.m_con.m_opoConCtx.opsErrCtx, this.m_con.m_opoConCtx.pOpoConValCtx, pfile, out num2);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				if (num == -1)
				{
					try
					{
						this.m_con.Dispose();
						this.m_con = new OracleConnection(this.m_encryptedConString.Password);
						this.m_con.m_bStartupShutdown = true;
						this.Open();
						try
						{
							this.m_con.m_opoConCtx.pOpoConValCtx->OracleDBStartupMode = (int)startupMode;
							num = OpsCon.StartupDB(this.m_con.m_opoConCtx.opsConCtx, this.m_con.m_opoConCtx.opsErrCtx, this.m_con.m_opoConCtx.pOpoConValCtx, pfile, out num2);
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
					finally
					{
						if (num == -1)
						{
							if (!bMountAndOpen)
							{
								OracleException.HandleError(num, this.m_con, this.m_con.m_opoConCtx.opsErrCtx, this.m_con);
							}
							if (num2 != 1081)
							{
								OracleException.HandleError(num, this.m_con, this.m_con.m_opoConCtx.opsErrCtx, this.m_con);
							}
						}
					}
				}
				this.m_con.Dispose();
				this.m_con = new OracleConnection(this.m_encryptedConString.Password);
				this.m_con.m_bStartupShutdown = true;
				this.Open();
				if ((num2 == 1081 || num == 0) && bMountAndOpen)
				{
					try
					{
						this.ExecuteNonQuery("ALTER DATABASE MOUNT");
					}
					catch (OracleException ex3)
					{
						if (ex3.Number != 1100)
						{
							throw;
						}
					}
					try
					{
						this.ExecuteNonQuery("ALTER DATABASE OPEN");
					}
					catch (OracleException ex4)
					{
						if (ex4.Number != 1531)
						{
							throw;
						}
					}
				}
			}
			finally
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDatabase::StartupDB(4)\n"
					});
				}
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0001C958 File Offset: 0x0001B958
		public void Shutdown()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDatabase::Shutdown(1)\n"
				});
			}
			try
			{
				this.Shutdown(OracleDBShutdownMode.Default, true);
			}
			finally
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDatabase::Shutdown(1)\n"
					});
				}
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0001C9BC File Offset: 0x0001B9BC
		public unsafe void Shutdown(OracleDBShutdownMode shutdownMode, bool bCloseDismountAndFinalize)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDatabase::Shutdown(3)\n"
				});
			}
			try
			{
				int num = 0;
				int num2 = 0;
				this.m_bCloseDismountAndFinalize = bCloseDismountAndFinalize;
				this.m_con.m_opoConCtx.pOpoConValCtx->OracleDBShutdownMode = (int)shutdownMode;
				try
				{
					num = OpsCon.ShutdownDB(this.m_con.m_opoConCtx.opsConCtx, this.m_con.m_opoConCtx.opsErrCtx, this.m_con.m_opoConCtx.pOpoConValCtx, out num2);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
					throw;
				}
				if (num == 0 && bCloseDismountAndFinalize && shutdownMode != OracleDBShutdownMode.Final && shutdownMode != OracleDBShutdownMode.Abort)
				{
					try
					{
						this.ExecuteNonQuery("ALTER DATABASE CLOSE NORMAL");
					}
					catch (OracleException ex2)
					{
						if (ex2.Number != 1109 && ex2.Number != 1507)
						{
							throw;
						}
					}
					try
					{
						this.ExecuteNonQuery("ALTER DATABASE DISMOUNT");
					}
					catch (OracleException ex3)
					{
						if (ex3.Number != 1507)
						{
							throw;
						}
					}
					this.Shutdown(OracleDBShutdownMode.Final, bCloseDismountAndFinalize);
				}
				else if (num != 0 && !bCloseDismountAndFinalize)
				{
					OracleException.HandleError(num, this.m_con, this.m_con.m_opoConCtx.opsErrCtx, this.m_con);
				}
				if (num2 != 1012 && num != 0)
				{
					OracleException.HandleError(num, this.m_con, this.m_con.m_opoConCtx.opsErrCtx, this.m_con);
				}
			}
			finally
			{
				this.m_bCloseDismountAndFinalize = false;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDatabase::Shutdown(3)\n"
					});
				}
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0001CB9C File Offset: 0x0001BB9C
		public void ExecuteNonQuery(string sql)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleDatabase::ExecuteNonQuery()\n"
				});
			}
			try
			{
				this.m_cmd.CommandText = sql;
				this.m_cmd.Connection = this.m_con;
				try
				{
					this.m_cmd.ExecuteNonQuery();
				}
				catch (Exception)
				{
					if (this.m_bCloseDismountAndFinalize)
					{
						throw;
					}
					this.m_con.Dispose();
					this.m_con = new OracleConnection(this.m_encryptedConString.Password);
					this.m_con.m_bStartupShutdown = true;
					this.Open();
					this.m_cmd.Connection = this.m_con;
					this.m_cmd.ExecuteNonQuery();
				}
			}
			finally
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT)  OracleDatabase::ExecuteNonQuery()\n"
					});
				}
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0001CC8C File Offset: 0x0001BC8C
		public string ServerVersion
		{
			get
			{
				return this.m_con.ServerVersion;
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0001CC99 File Offset: 0x0001BC99
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0001CCA8 File Offset: 0x0001BCA8
		~OracleDatabase()
		{
			this.Dispose();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0001CCD4 File Offset: 0x0001BCD4
		protected void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				try
				{
					if (disposing)
					{
						if (this.m_encryptedConString != null)
						{
							this.m_encryptedConString.Dispose();
						}
						if (this.m_cmd != null)
						{
							this.m_cmd.Dispose();
						}
						if (this.m_con != null)
						{
							this.m_con.Dispose();
						}
					}
				}
				finally
				{
					this.m_encryptedConString = null;
					this.m_cmd = null;
					this.m_con = null;
					this.m_disposed = true;
				}
			}
		}

		// Token: 0x040001B0 RID: 432
		private OracleConnection m_con;

		// Token: 0x040001B1 RID: 433
		private OracleCommand m_cmd;

		// Token: 0x040001B2 RID: 434
		private EncryptedPassword m_encryptedConString;

		// Token: 0x040001B3 RID: 435
		private bool m_disposed;

		// Token: 0x040001B4 RID: 436
		private bool m_bCloseDismountAndFinalize;
	}
}
