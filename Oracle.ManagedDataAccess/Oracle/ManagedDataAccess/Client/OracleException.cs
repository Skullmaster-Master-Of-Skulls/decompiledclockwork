using System;
using System.Data.Common;
using System.Runtime.Serialization;
using OracleInternal.Common;
using OracleInternal.Network;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x0200006A RID: 106
	[Serializable]
	public sealed class OracleException : DbException
	{
		// Token: 0x0600053C RID: 1340 RVA: 0x00030528 File Offset: 0x0002E728
		internal OracleException(OracleErrorCollection oec)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			this.m_errors = oec;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00030568 File Offset: 0x0002E768
		internal OracleException(NetworkException inner) : base(inner.Message, inner)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			this.m_errors = new OracleErrorCollection();
			this.m_errors.Add(new OracleError(inner.ErrorCode, inner.Source, string.Empty, inner.Message));
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000305E8 File Offset: 0x0002E7E8
		internal OracleException(int errCode, string dataSrc, string procedure, string errMsg)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			this.m_errors = new OracleErrorCollection();
			this.m_errors.Add(new OracleError(errCode, dataSrc, procedure, errMsg));
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0003064C File Offset: 0x0002E84C
		internal OracleException(int errCode, string dataSrc, string procedure, string errMsg, Exception innerException) : base(innerException.Message, innerException)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			this.m_errors = new OracleErrorCollection();
			this.m_errors.Add(new OracleError(errCode, dataSrc, procedure, errMsg));
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000306BC File Offset: 0x0002E8BC
		private OracleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			this.m_errors = (OracleErrorCollection)info.GetValue(base.GetType().FullName, typeof(OracleErrorCollection));
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00030728 File Offset: 0x0002E928
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				base.GetObjectData(info, context);
				info.AddValue(base.GetType().FullName, this.m_errors);
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

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x000307B8 File Offset: 0x0002E9B8
		public OracleLogicalTransaction OracleLogicalTransaction
		{
			get
			{
				return this.m_OracleLogicalTransaction;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x000307C0 File Offset: 0x0002E9C0
		public OracleErrorCollection Errors
		{
			get
			{
				return this.m_errors;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x000307C8 File Offset: 0x0002E9C8
		public string DataSource
		{
			get
			{
				return this.m_errors[0].DataSource;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x000307DC File Offset: 0x0002E9DC
		public override string Message
		{
			get
			{
				string text = string.Empty;
				if (this.m_errors != null)
				{
					int count = this.m_errors.Count;
					int i = 0;
					while (i < count)
					{
						text += this.m_errors[i].Message;
						if (++i < count)
						{
							text += "\n";
						}
					}
				}
				return text.TrimEnd(new char[]
				{
					'\n'
				});
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0003084C File Offset: 0x0002EA4C
		public string Procedure
		{
			get
			{
				return this.m_errors[0].Procedure;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00030860 File Offset: 0x0002EA60
		public override string Source
		{
			get
			{
				return this.m_errors[0].Source;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00030874 File Offset: 0x0002EA74
		public int Number
		{
			get
			{
				return this.m_errors[0].Number;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00030888 File Offset: 0x0002EA88
		public bool IsRecoverable
		{
			get
			{
				return this.m_errors[0].IsRecoverable;
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0003089C File Offset: 0x0002EA9C
		internal void AddBindErrorToCollection(int errCode, string dataSrc, string procedure, string errMsg, int arrayBindIndex)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			this.m_errors.Add(new OracleError(errCode, dataSrc, procedure, errMsg, arrayBindIndex));
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000308F4 File Offset: 0x0002EAF4
		internal static void HandleError(OracleTraceLevel level, OracleTraceTag tag, Exception ex, OracleLogicalTransaction oracleLogicalTransaction = null)
		{
			bool flag = false;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (OracleTraceTag.Error == (tag & OracleTraceTag.Error) && level == OracleTraceLevel.Public)
				{
					flag = true;
				}
				else if (level == OracleTraceLevel.Config)
				{
					flag = ProviderConfig.m_bTraceLevelConfig;
				}
				else if (level == OracleTraceLevel.Network)
				{
					flag = ProviderConfig.m_bTraceLevelNetwork;
				}
				else if (level == OracleTraceLevel.Private)
				{
					flag = ProviderConfig.m_bTraceLevelPrivate;
				}
				else if (level == OracleTraceLevel.Public)
				{
					flag = ProviderConfig.m_bTraceLevelPublic;
				}
				if (ex is OracleException)
				{
					if (oracleLogicalTransaction != null && oracleLogicalTransaction.m_ltxId != null)
					{
						try
						{
							if (((OracleException)ex).IsRecoverable && (((OracleException)ex).m_OracleLogicalTransaction == null || ((OracleException)ex).m_OracleLogicalTransaction.m_ltxId == null) && oracleLogicalTransaction.m_connection != null && !oracleLogicalTransaction.m_connection.bConnectionforTxnStatus)
							{
								((OracleException)ex).m_OracleLogicalTransaction = oracleLogicalTransaction;
								try
								{
									OracleConnection.bIgnoreLogicalTransaction = true;
									((OracleException)ex).m_OracleLogicalTransaction.m_connection.Close();
								}
								catch
								{
								}
								finally
								{
									OracleConnection.bIgnoreLogicalTransaction = false;
								}
								if (((OracleException)ex).OracleLogicalTransaction.m_ltxId != null && ((OracleException)ex).OracleLogicalTransaction.bDistributed == false)
								{
									((OracleException)ex).m_OracleLogicalTransaction.GetOutcome();
								}
							}
						}
						finally
						{
							oracleLogicalTransaction.m_connection = null;
						}
					}
					if (((OracleException)ex).m_OracleLogicalTransaction == null)
					{
						((OracleException)ex).m_OracleLogicalTransaction = new OracleLogicalTransaction(null, null);
					}
				}
				if (ex is NetworkException)
				{
					OracleException ex2 = new OracleException((NetworkException)ex);
					if (oracleLogicalTransaction != null && oracleLogicalTransaction.m_ltxId != null)
					{
						try
						{
							if (ex2.IsRecoverable && oracleLogicalTransaction.m_connection != null && !oracleLogicalTransaction.m_connection.bConnectionforTxnStatus)
							{
								ex2.m_OracleLogicalTransaction = oracleLogicalTransaction;
								try
								{
									OracleConnection.bIgnoreLogicalTransaction = true;
									((OracleException)ex).m_OracleLogicalTransaction.m_connection.Close();
								}
								catch
								{
								}
								finally
								{
									OracleConnection.bIgnoreLogicalTransaction = false;
								}
								if (oracleLogicalTransaction.bDistributed == false)
								{
									ex2.m_OracleLogicalTransaction.GetOutcome();
								}
							}
						}
						finally
						{
							oracleLogicalTransaction.m_connection = null;
						}
					}
					if (ex2.m_OracleLogicalTransaction == null)
					{
						ex2.m_OracleLogicalTransaction = new OracleLogicalTransaction(null, null);
					}
					if (ex2.OracleLogicalTransaction == null || !(ex2.OracleLogicalTransaction.UserCallCompleted == true) || !(ex2.OracleLogicalTransaction.Committed == true))
					{
						throw ex2;
					}
				}
			}
			finally
			{
				try
				{
					if (flag)
					{
						Trace.Write(level, tag, new string[]
						{
							Trace.GetCPInfo(null, null, null, null, false, false) + ex.ToString()
						});
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

		// Token: 0x04000650 RID: 1616
		private OracleErrorCollection m_errors;

		// Token: 0x04000651 RID: 1617
		internal OracleLogicalTransaction m_OracleLogicalTransaction;
	}
}
