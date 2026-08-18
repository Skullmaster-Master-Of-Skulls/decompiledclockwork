using System;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x0200013F RID: 319
	public sealed class OracleTruncateException : OracleTypeException
	{
		// Token: 0x06000CC8 RID: 3272 RVA: 0x000864E1 File Offset: 0x000854E1
		static OracleTruncateException()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000864F9 File Offset: 0x000854F9
		public OracleTruncateException()
		{
			this.m_number = 16550;
			this.m_mesg = OracleTruncateException.DefMesg;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00086517 File Offset: 0x00085517
		public OracleTruncateException(string message) : base(message)
		{
			this.m_number = 0;
			this.m_mesg = message;
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00086530 File Offset: 0x00085530
		internal static string GetDefMesg()
		{
			string result = "";
			int num = 0;
			try
			{
				num = OpsErr.GetOraMesg(16550, out result);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				throw;
			}
			if (num == 0)
			{
				return result;
			}
			return "";
		}

		// Token: 0x04000A15 RID: 2581
		internal static readonly string DefMesg = OracleTruncateException.GetDefMesg();
	}
}
