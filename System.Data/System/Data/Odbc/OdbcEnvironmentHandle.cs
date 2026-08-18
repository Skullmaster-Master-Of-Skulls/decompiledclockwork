using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x020001EA RID: 490
	internal sealed class OdbcEnvironmentHandle : OdbcHandle
	{
		// Token: 0x06001B6E RID: 7022 RVA: 0x00263638 File Offset: 0x00262A38
		internal OdbcEnvironmentHandle() : base(ODBC32.SQL_HANDLE.ENV, null)
		{
			ODBC32.RetCode retcode = UnsafeNativeMethods.SQLSetEnvAttr(this, ODBC32.SQL_ATTR.ODBC_VERSION, ODBC32.SQL_OV_ODBC3, ODBC32.SQL_IS.INTEGER);
			retcode = UnsafeNativeMethods.SQLSetEnvAttr(this, ODBC32.SQL_ATTR.CONNECTION_POOLING, ODBC32.SQL_CP_ONE_PER_HENV, ODBC32.SQL_IS.INTEGER);
			switch (retcode)
			{
			case ODBC32.RetCode.SUCCESS:
			case ODBC32.RetCode.SUCCESS_WITH_INFO:
				return;
			default:
				base.Dispose();
				throw ODBC.CantEnableConnectionpooling(retcode);
			}
		}
	}
}
