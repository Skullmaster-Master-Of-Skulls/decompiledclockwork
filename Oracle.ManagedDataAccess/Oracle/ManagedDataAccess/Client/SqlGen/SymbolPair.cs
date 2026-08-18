using System;

namespace Oracle.ManagedDataAccess.Client.SqlGen
{
	// Token: 0x020000F6 RID: 246
	internal class SymbolPair : ISqlFragment
	{
		// Token: 0x06000A59 RID: 2649 RVA: 0x000753E8 File Offset: 0x000735E8
		public SymbolPair(Symbol source, Symbol column)
		{
			this.Source = source;
			this.Column = column;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00075400 File Offset: 0x00073600
		public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
		}

		// Token: 0x04000C81 RID: 3201
		public Symbol Source;

		// Token: 0x04000C82 RID: 3202
		public Symbol Column;
	}
}
