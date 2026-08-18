using System;
using System.Data.Objects.DataClasses;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x020000E3 RID: 227
	public static class EFOracleFunctions
	{
		// Token: 0x06000900 RID: 2304 RVA: 0x00069644 File Offset: 0x00067844
		[EdmFunction("OracleEFProvider", "regexp_LIKE")]
		public static bool regexp_LIKE(string columnName, string regexp)
		{
			throw new NotSupportedException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.EF_LINQ_EDMFUNC_NO_DIRECT_CALL, new string[0]));
		}

		// Token: 0x04000BFA RID: 3066
		internal const string FunctionNameRegExpLike = "regexp_LIKE";
	}
}
