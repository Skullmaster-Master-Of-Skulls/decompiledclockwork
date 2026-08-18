using System;

namespace Oracle.ManagedDataAccess.Client.SqlGen
{
	// Token: 0x020000EE RID: 238
	internal interface ISqlFragment
	{
		// Token: 0x06000995 RID: 2453
		void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator);
	}
}
