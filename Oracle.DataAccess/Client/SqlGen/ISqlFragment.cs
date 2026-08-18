using System;

namespace Oracle.DataAccess.Client.SqlGen
{
	// Token: 0x02000015 RID: 21
	internal interface ISqlFragment
	{
		// Token: 0x060000AF RID: 175
		void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator);
	}
}
