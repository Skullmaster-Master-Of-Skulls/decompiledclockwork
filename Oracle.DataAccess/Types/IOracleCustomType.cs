using System;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000067 RID: 103
	public interface IOracleCustomType
	{
		// Token: 0x060004E7 RID: 1255
		void FromCustomObject(OracleConnection con, IntPtr pUdt);

		// Token: 0x060004E8 RID: 1256
		void ToCustomObject(OracleConnection con, IntPtr pUdt);
	}
}
