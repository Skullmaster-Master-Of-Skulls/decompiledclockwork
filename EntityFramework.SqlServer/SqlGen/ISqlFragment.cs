using System;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000022 RID: 34
	internal interface ISqlFragment
	{
		// Token: 0x060001EA RID: 490
		void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator);
	}
}
