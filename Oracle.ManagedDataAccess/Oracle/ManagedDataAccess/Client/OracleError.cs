using System;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000068 RID: 104
	[Serializable]
	public sealed class OracleError
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x00030234 File Offset: 0x0002E434
		internal OracleError(int errNumber, string dataSrc, string procedure, string errMsg)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_number = errNumber;
				this.m_dataSource = dataSrc;
				this.m_procedure = procedure;
				if (errMsg == null || errMsg.Length == 0)
				{
					this.m_message = OracleStringResourceManager.GetErrorMesg(errNumber, new string[0]);
				}
				else
				{
					this.m_message = errMsg;
				}
				this.m_isRecoverable = this.IsErrorRecoverable(this.m_number);
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

		// Token: 0x06000530 RID: 1328 RVA: 0x00030300 File Offset: 0x0002E500
		internal OracleError(int errNumber, string dataSrc, string procedure, string errMsg, int arrayBindIndex)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_number = errNumber;
				this.m_dataSource = dataSrc;
				this.m_procedure = procedure;
				if (errMsg == null || errMsg.Length == 0)
				{
					this.m_message = OracleStringResourceManager.GetErrorMesg(errNumber, new string[0]);
				}
				else
				{
					this.m_message = errMsg;
				}
				this.m_arrayBindIndex = arrayBindIndex;
				this.m_isRecoverable = this.IsErrorRecoverable(this.m_number);
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

		// Token: 0x06000531 RID: 1329 RVA: 0x000303D4 File Offset: 0x0002E5D4
		private bool IsErrorRecoverable(int errcode)
		{
			return errcode == 1033 || errcode == 1034 || errcode == 1089 || errcode == 1090 || errcode == 1092 || errcode == 3135 || errcode == 3113 || errcode == 3114 || errcode == 28 || errcode == 12537 || errcode == 12614 || errcode == 12547 || errcode == 12583 || errcode == 12570 || errcode == 12153 || errcode == 603 || errcode == 1012 || errcode == 12514 || errcode == 16456 || errcode == 31 || errcode == 376 || errcode == 1115 || errcode == 17002 || errcode == 17008 || errcode == 17410 || errcode == 12571;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x000304CC File Offset: 0x0002E6CC
		public string DataSource
		{
			get
			{
				return this.m_dataSource;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x000304D4 File Offset: 0x0002E6D4
		public string Message
		{
			get
			{
				return this.m_message;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x000304DC File Offset: 0x0002E6DC
		public int Number
		{
			get
			{
				return this.m_number;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x000304E4 File Offset: 0x0002E6E4
		public string Procedure
		{
			get
			{
				return this.m_procedure;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x000304EC File Offset: 0x0002E6EC
		public string Source
		{
			get
			{
				return "Oracle Data Provider for .NET, Managed Driver";
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x000304F4 File Offset: 0x0002E6F4
		public int ArrayBindIndex
		{
			get
			{
				return this.m_arrayBindIndex;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x000304FC File Offset: 0x0002E6FC
		internal bool IsRecoverable
		{
			get
			{
				return this.m_isRecoverable;
			}
		}

		// Token: 0x0400064A RID: 1610
		private string m_dataSource;

		// Token: 0x0400064B RID: 1611
		private string m_procedure;

		// Token: 0x0400064C RID: 1612
		private string m_message;

		// Token: 0x0400064D RID: 1613
		private int m_number;

		// Token: 0x0400064E RID: 1614
		private int m_arrayBindIndex = -1;

		// Token: 0x0400064F RID: 1615
		private bool m_isRecoverable;
	}
}
