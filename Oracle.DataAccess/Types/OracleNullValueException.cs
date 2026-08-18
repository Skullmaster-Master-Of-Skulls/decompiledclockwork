using System;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000003 RID: 3
	public sealed class OracleNullValueException : OracleTypeException
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002238 File Offset: 0x00001238
		static OracleNullValueException()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002246 File Offset: 0x00001246
		public OracleNullValueException()
		{
			this.m_number = ErrRes.TYP_NULLVALUE;
			this.m_mesg = OpoErrResManager.GetErrorMesg(ErrRes.TYP_NULLVALUE, new string[0]);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000226F File Offset: 0x0000126F
		public OracleNullValueException(string message) : base(message)
		{
			this.m_number = 0;
			this.m_mesg = message;
		}
	}
}
