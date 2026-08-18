using System;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x0200002F RID: 47
	internal interface ISqlFragment
	{
		// Token: 0x06000441 RID: 1089
		void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator);
	}
}
