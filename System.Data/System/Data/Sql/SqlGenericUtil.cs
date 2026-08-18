using System;
using System.Data.Common;

namespace System.Data.Sql
{
	// Token: 0x02000285 RID: 645
	internal sealed class SqlGenericUtil
	{
		// Token: 0x060021AF RID: 8623 RVA: 0x00287C48 File Offset: 0x00287048
		private SqlGenericUtil()
		{
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x00287C68 File Offset: 0x00287068
		internal static Exception NullCommandText()
		{
			return ADP.Argument(Res.GetString("Sql_NullCommandText"));
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x00287C88 File Offset: 0x00287088
		internal static Exception MismatchedMetaDataDirectionArrayLengths()
		{
			return ADP.Argument(Res.GetString("Sql_MismatchedMetaDataDirectionArrayLengths"));
		}
	}
}
