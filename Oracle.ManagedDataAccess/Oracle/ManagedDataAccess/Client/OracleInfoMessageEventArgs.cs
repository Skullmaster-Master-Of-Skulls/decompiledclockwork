using System;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x0200006D RID: 109
	public sealed class OracleInfoMessageEventArgs : EventArgs
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0003135C File Offset: 0x0002F55C
		public OracleErrorCollection Errors
		{
			get
			{
				return this.m_oraErrors;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x00031364 File Offset: 0x0002F564
		public string Message
		{
			get
			{
				return this.m_message;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0003136C File Offset: 0x0002F56C
		public string Source
		{
			get
			{
				return this.m_source;
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00031374 File Offset: 0x0002F574
		internal OracleInfoMessageEventArgs(OracleErrorCollection oraErrors)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				this.m_oraErrors = oraErrors;
				this.m_message = oraErrors[0].Message;
				this.m_source = oraErrors[0].Source;
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

		// Token: 0x0600057E RID: 1406 RVA: 0x00031414 File Offset: 0x0002F614
		public override string ToString()
		{
			return this.m_message;
		}

		// Token: 0x0400065F RID: 1631
		private OracleErrorCollection m_oraErrors;

		// Token: 0x04000660 RID: 1632
		private string m_message;

		// Token: 0x04000661 RID: 1633
		private string m_source;
	}
}
