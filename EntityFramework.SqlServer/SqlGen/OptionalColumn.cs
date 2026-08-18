using System;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000032 RID: 50
	internal sealed class OptionalColumn
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000BB60 File Offset: 0x00009D60
		internal void Append(object s)
		{
			this.m_builder.Append(s);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000BB6E File Offset: 0x00009D6E
		internal void MarkAsUsed()
		{
			this.m_usageManager.MarkAsUsed(this.m_symbol);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000BB81 File Offset: 0x00009D81
		internal OptionalColumn(SymbolUsageManager usageManager, Symbol symbol)
		{
			this.m_usageManager = usageManager;
			this.m_symbol = symbol;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000BBA2 File Offset: 0x00009DA2
		public bool WriteSqlIfUsed(SqlWriter writer, SqlGenerator sqlGenerator, string separator)
		{
			if (this.m_usageManager.IsUsed(this.m_symbol))
			{
				writer.Write(separator);
				this.m_builder.WriteSql(writer, sqlGenerator);
				return true;
			}
			return false;
		}

		// Token: 0x0400008C RID: 140
		private readonly SymbolUsageManager m_usageManager;

		// Token: 0x0400008D RID: 141
		private readonly SqlBuilder m_builder = new SqlBuilder();

		// Token: 0x0400008E RID: 142
		private readonly Symbol m_symbol;
	}
}
