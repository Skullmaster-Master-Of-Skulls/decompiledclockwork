using System;
using System.Data.Common;

namespace System.Data.Sql
{
	// Token: 0x0200014C RID: 332
	internal sealed class SqlGenericUtil
	{
		// Token: 0x06001363 RID: 4963 RVA: 0x0009A514 File Offset: 0x00099914
		private SqlGenericUtil()
		{
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0009A528 File Offset: 0x00099928
		internal static Exception NullCommandText()
		{
			return ADP.Argument(Res.GetString("Sql_NullCommandText"));
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0009A544 File Offset: 0x00099944
		internal static Exception MismatchedMetaDataDirectionArrayLengths()
		{
			return ADP.Argument(Res.GetString("Sql_MismatchedMetaDataDirectionArrayLengths"));
		}
	}
}
