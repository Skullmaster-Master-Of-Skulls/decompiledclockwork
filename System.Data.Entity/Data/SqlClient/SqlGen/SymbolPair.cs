using System;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x02000036 RID: 54
	internal class SymbolPair : ISqlFragment
	{
		// Token: 0x060004F6 RID: 1270 RVA: 0x00017093 File Offset: 0x00015293
		public SymbolPair(Symbol source, Symbol column)
		{
			this.Source = source;
			this.Column = column;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000089D0 File Offset: 0x00006BD0
		public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
		}

		// Token: 0x04000737 RID: 1847
		public Symbol Source;

		// Token: 0x04000738 RID: 1848
		public Symbol Column;
	}
}
