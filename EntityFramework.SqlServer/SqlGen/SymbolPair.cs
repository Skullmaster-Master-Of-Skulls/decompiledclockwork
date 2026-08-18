using System;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x0200003E RID: 62
	internal class SymbolPair : ISqlFragment
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x000144EB File Offset: 0x000126EB
		public SymbolPair(Symbol source, Symbol column)
		{
			this.Source = source;
			this.Column = column;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00014501 File Offset: 0x00012701
		public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
		}

		// Token: 0x040000F3 RID: 243
		public Symbol Source;

		// Token: 0x040000F4 RID: 244
		public Symbol Column;
	}
}
