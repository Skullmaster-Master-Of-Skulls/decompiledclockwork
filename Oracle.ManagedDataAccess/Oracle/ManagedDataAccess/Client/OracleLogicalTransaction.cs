using System;
using System.Data;
using OracleInternal.Common;
using OracleInternal.MTS;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x0200005D RID: 93
	public class OracleLogicalTransaction : IDisposable
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x00021C20 File Offset: 0x0001FE20
		internal OracleLogicalTransaction(OracleConnection connection, byte[] ltxId)
		{
			this.m_connection = connection;
			if (ltxId != null)
			{
				this.m_ltxId = (byte[])ltxId.Clone();
			}
			if (connection != null && connection.m_oracleConnectionImpl != null)
			{
				if (connection.m_oracleConnectionImpl.m_mtsTxnCtx != null && connection.m_oracleConnectionImpl.m_mtsTxnCtx.m_txnType == MTSTxnType.Distributed)
				{
					this.bDistributed = new bool?(true);
				}
				else
				{
					this.bDistributed = new bool?(false);
				}
				if (connection.m_oracleConnectionImpl.m_cs != null)
				{
					if (connection.m_oracleConnectionImpl.m_cs.m_proxyUserId == null || connection.m_oracleConnectionImpl.m_cs.m_proxyUserId == string.Empty)
					{
						this.m_conString = string.Format("user id={0};data source={1};pooling=false", connection.m_oracleConnectionImpl.m_cs.m_userId, connection.m_oracleConnectionImpl.m_cs.m_dataSource);
						return;
					}
					this.m_conString = string.Format("user id={0};data source={1};proxy user id={2};pooling=false", connection.m_oracleConnectionImpl.m_cs.m_userId, connection.m_oracleConnectionImpl.m_cs.m_dataSource, connection.m_oracleConnectionImpl.m_cs.m_proxyUserId);
				}
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00021D6C File Offset: 0x0001FF6C
		~OracleLogicalTransaction()
		{
			this.Dispose();
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00021D98 File Offset: 0x0001FF98
		public void Dispose()
		{
			if (!this.m_disposed)
			{
				try
				{
					GC.SuppressFinalize(this);
					try
					{
						if (this.m_connforTxnStatus != null)
						{
							this.m_connforTxnStatus.Dispose();
							this.m_connforTxnStatus = null;
						}
					}
					catch
					{
					}
				}
				finally
				{
					this.m_connection = null;
					this.m_disposed = true;
				}
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00021E00 File Offset: 0x00020000
		public static void GetOutcome(string constring, byte[] ltxid, out bool? bCommitted, out bool? bUserCallCompleted)
		{
			OracleConnection oracleConnection = null;
			bCommitted = null;
			bUserCallCompleted = null;
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (ltxid == null)
				{
					throw new ArgumentNullException("LogicalTransactionId");
				}
				oracleConnection = new OracleConnection(constring);
				oracleConnection.Open();
				if (!oracleConnection.m_isDb12cR1OrHigher)
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_FEATURE_NOT_AVAILABLE, new string[]
					{
						oracleConnection.ServerVersion
					}));
				}
				OracleLogicalTransaction oracleLogicalTransaction = new OracleLogicalTransaction(oracleConnection, ltxid);
				oracleLogicalTransaction.GetOutcome(oracleConnection);
				bCommitted = oracleLogicalTransaction.Committed;
				bUserCallCompleted = oracleLogicalTransaction.UserCallCompleted;
			}
			finally
			{
				try
				{
					oracleConnection.Dispose();
				}
				catch
				{
				}
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00021EE4 File Offset: 0x000200E4
		internal void GetOutcome(OracleConnection con)
		{
			OracleCommand oracleCommand = null;
			OracleParameter oracleParameter = null;
			OracleParameter oracleParameter2 = null;
			OracleParameter oracleParameter3 = null;
			try
			{
				con.bConnectionforTxnStatus = true;
				oracleCommand = new OracleCommand(OracleConnection.s_getLTXIDstatus, con);
				oracleParameter = oracleCommand.Parameters.Add(null, OracleDbType.Raw, ParameterDirection.Input);
				oracleParameter.Value = this.m_ltxId;
				oracleParameter2 = oracleCommand.Parameters.Add("committed", 0);
				oracleParameter2.DbType = DbType.Int16;
				oracleParameter2.Direction = ParameterDirection.InputOutput;
				oracleParameter3 = oracleCommand.Parameters.Add("userCallCompleted", 0);
				oracleParameter3.DbType = DbType.Int16;
				oracleParameter3.Direction = ParameterDirection.InputOutput;
				oracleCommand.CommandTimeout = 15;
				oracleCommand.ExecuteNonQuery();
				if ((short)oracleParameter2.Value == 1)
				{
					this.m_bCommitted = new bool?(true);
				}
				else
				{
					this.m_bCommitted = new bool?(false);
				}
				if ((short)oracleParameter3.Value == 1)
				{
					this.m_bUserCallCompleted = new bool?(true);
				}
				else
				{
					this.m_bUserCallCompleted = new bool?(false);
				}
			}
			finally
			{
				oracleParameter.Dispose();
				oracleParameter2.Dispose();
				oracleParameter3.Dispose();
				oracleCommand.Dispose();
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00022000 File Offset: 0x00020200
		internal void GetOutcome()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (this.m_bCommitted == null)
				{
					if (this.m_connection != null)
					{
						this.m_connforTxnStatus = (OracleConnection)this.m_connection.Clone();
					}
					this.m_connforTxnStatus.Open();
					if (!this.m_connforTxnStatus.m_isDb12cR1OrHigher)
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_FEATURE_NOT_AVAILABLE, new string[]
						{
							this.m_connforTxnStatus.ServerVersion
						}));
					}
					this.GetOutcome(this.m_connforTxnStatus);
					this.m_connforTxnStatus.Dispose();
				}
			}
			catch (Exception ex)
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
					{
						Trace.GetCPInfo(null, null, null, null, false, false) + ex.ToString()
					});
				}
			}
			finally
			{
				try
				{
					if (this.m_connforTxnStatus != null && this.m_connforTxnStatus.State == ConnectionState.Open)
					{
						this.m_connforTxnStatus.Close();
					}
				}
				catch
				{
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0002214C File Offset: 0x0002034C
		public bool? Committed
		{
			get
			{
				return this.m_bCommitted;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00022154 File Offset: 0x00020354
		public bool? UserCallCompleted
		{
			get
			{
				return this.m_bUserCallCompleted;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0002215C File Offset: 0x0002035C
		public string ConnectionString
		{
			get
			{
				return this.m_conString;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00022164 File Offset: 0x00020364
		public byte[] LogicalTransactionId
		{
			get
			{
				if (this.bDistributed == false && (this.m_bCommitted == null || this.m_bUserCallCompleted == null))
				{
					return this.m_ltxId;
				}
				return null;
			}
		}

		// Token: 0x040005B7 RID: 1463
		internal OracleConnection m_connection;

		// Token: 0x040005B8 RID: 1464
		internal bool? bDistributed = null;

		// Token: 0x040005B9 RID: 1465
		internal OracleConnection m_connforTxnStatus;

		// Token: 0x040005BA RID: 1466
		internal byte[] m_ltxId;

		// Token: 0x040005BB RID: 1467
		private string m_conString;

		// Token: 0x040005BC RID: 1468
		private bool? m_bCommitted = null;

		// Token: 0x040005BD RID: 1469
		private bool? m_bUserCallCompleted = null;

		// Token: 0x040005BE RID: 1470
		private bool m_disposed;
	}
}
