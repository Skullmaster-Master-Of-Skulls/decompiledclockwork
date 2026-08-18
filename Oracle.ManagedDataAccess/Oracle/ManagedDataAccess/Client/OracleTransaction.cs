using System;
using System.Data;
using System.Data.Common;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x0200007F RID: 127
	public sealed class OracleTransaction : DbTransaction
	{
		// Token: 0x06000664 RID: 1636 RVA: 0x00039648 File Offset: 0x00037848
		internal OracleTransaction(OracleConnection con, IsolationLevel isolationLevel)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_connection = con;
				this.m_isolationLevel = isolationLevel;
				this.m_connection.m_oracleConnectionImpl.SetAutoCommit(false);
				this.m_oracleTransactionImpl = new OracleTransactionImpl(con.m_oracleConnectionImpl, isolationLevel);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x000396F0 File Offset: 0x000378F0
		public new OracleConnection Connection
		{
			get
			{
				return this.m_connection;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x000396F8 File Offset: 0x000378F8
		public override IsolationLevel IsolationLevel
		{
			get
			{
				if (this.m_completed || this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				return this.m_isolationLevel;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00039728 File Offset: 0x00037928
		internal bool Completed
		{
			get
			{
				return this.m_completed;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00039730 File Offset: 0x00037930
		protected override DbConnection DbConnection
		{
			get
			{
				return this.m_connection;
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00039738 File Offset: 0x00037938
		public override void Commit()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleLogicalTransaction oracleLogicalTransaction = null;
			try
			{
				if (this.m_completed || this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				try
				{
					this.m_oracleTransactionImpl.Commit(this.m_connection, ref oracleLogicalTransaction);
					this.m_connection.CheckForWarnings(this);
					this.m_completed = true;
				}
				finally
				{
					this.m_connection.m_oracleConnectionImpl.SetAutoCommit(true);
					if (ConfigBaseClass.m_bLegacyIsolationLevelBehavior && IsolationLevel.Serializable == this.m_isolationLevel)
					{
						this.m_connection.m_oracleConnectionImpl.SwitchIsolationLevel(IsolationLevel.ReadCommitted);
					}
				}
				this.Dispose();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, oracleLogicalTransaction);
				if (!(ex is OracleException))
				{
					throw;
				}
				if (((OracleException)ex).OracleLogicalTransaction == null || !(((OracleException)ex).OracleLogicalTransaction.UserCallCompleted == true) || !(((OracleException)ex).OracleLogicalTransaction.Committed == true))
				{
					throw;
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x000398CC File Offset: 0x00037ACC
		public override void Rollback()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			OracleLogicalTransaction oracleLogicalTransaction = null;
			try
			{
				if (this.m_completed || this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				try
				{
					this.m_oracleTransactionImpl.Rollback(this.m_connection, ref oracleLogicalTransaction);
					this.m_connection.CheckForWarnings(this);
					this.m_completed = true;
				}
				finally
				{
					this.m_connection.m_oracleConnectionImpl.SetAutoCommit(true);
					if (ConfigBaseClass.m_bLegacyIsolationLevelBehavior && IsolationLevel.Serializable == this.m_isolationLevel)
					{
						this.m_connection.m_oracleConnectionImpl.SwitchIsolationLevel(IsolationLevel.ReadCommitted);
					}
				}
				this.Dispose();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, oracleLogicalTransaction);
				if (!(ex is OracleException))
				{
					throw;
				}
				if (((OracleException)ex).OracleLogicalTransaction == null || !(((OracleException)ex).OracleLogicalTransaction.UserCallCompleted == true) || !(((OracleException)ex).OracleLogicalTransaction.Committed == true))
				{
					throw;
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00039A60 File Offset: 0x00037C60
		public void Rollback(string savepointName)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_completed || this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_command == null)
				{
					this.m_command = new OracleCommand("", this.m_connection);
				}
				this.m_command.CommandText = "ROLLBACK TO SAVEPOINT " + savepointName;
				this.m_command.CommandTimeout = 0;
				this.m_command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00039B40 File Offset: 0x00037D40
		public void Save(string savepointName)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_completed || this.m_bClosed)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.OBJECT_CLOSED, new string[0]));
				}
				if (this.m_command == null)
				{
					this.m_command = new OracleCommand("", this.m_connection);
				}
				this.m_command.CommandText = "SAVEPOINT " + savepointName;
				this.m_command.CommandTimeout = 0;
				this.m_command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00039C20 File Offset: 0x00037E20
		internal void ConnectionClose()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.Dispose(true);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00039C98 File Offset: 0x00037E98
		public new void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00039CA4 File Offset: 0x00037EA4
		protected override void Dispose(bool disposing)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (!this.m_bClosed)
				{
					try
					{
						if (!this.m_completed)
						{
							this.Rollback();
							return;
						}
					}
					catch (Exception ex)
					{
						OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
					}
					if (disposing)
					{
						this.m_connection.m_oraTransaction = null;
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
					this.m_bClosed = true;
				}
			}
			finally
			{
				GC.SuppressFinalize(this);
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00039D78 File Offset: 0x00037F78
		protected override void Finalize()
		{
			try
			{
				this.Dispose(false);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x040006E0 RID: 1760
		internal OracleTransactionImpl m_oracleTransactionImpl;

		// Token: 0x040006E1 RID: 1761
		internal OracleConnection m_connection;

		// Token: 0x040006E2 RID: 1762
		private OracleCommand m_command;

		// Token: 0x040006E3 RID: 1763
		private bool m_completed;

		// Token: 0x040006E4 RID: 1764
		private bool m_bClosed;

		// Token: 0x040006E5 RID: 1765
		private IsolationLevel m_isolationLevel;
	}
}
