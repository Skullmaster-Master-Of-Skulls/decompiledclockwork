using System;

namespace Oracle.DataAccess.Client.SqlGen
{
	// Token: 0x0200012F RID: 303
	internal class SymbolPair : ISqlFragment
	{
		// Token: 0x06000C26 RID: 3110 RVA: 0x00078FBE File Offset: 0x00077FBE
		public SymbolPair(Symbol source, Symbol column)
		{
			this.Source = source;
			this.Column = column;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00078FD4 File Offset: 0x00077FD4
		public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
		}

		// Token: 0x04000992 RID: 2450
		public Symbol Source;

		// Token: 0x04000993 RID: 2451
		public Symbol Column;
	}
}
