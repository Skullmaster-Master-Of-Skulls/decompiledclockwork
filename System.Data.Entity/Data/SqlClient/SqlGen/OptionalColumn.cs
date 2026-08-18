using System;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x0200003B RID: 59
	internal sealed class OptionalColumn
	{
		// Token: 0x06000543 RID: 1347 RVA: 0x0001774C File Offset: 0x0001594C
		internal void Append(object s)
		{
			this.m_builder.Append(s);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001775A File Offset: 0x0001595A
		internal void MarkAsUsed()
		{
			this.m_usageManager.MarkAsUsed(this.m_symbol);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001776D File Offset: 0x0001596D
		internal OptionalColumn(SymbolUsageManager usageManager, Symbol symbol)
		{
			this.m_usageManager = usageManager;
			this.m_symbol = symbol;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001778E File Offset: 0x0001598E
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

		// Token: 0x04000741 RID: 1857
		private readonly SymbolUsageManager m_usageManager;

		// Token: 0x04000742 RID: 1858
		private readonly SqlBuilder m_builder = new SqlBuilder();

		// Token: 0x04000743 RID: 1859
		private readonly Symbol m_symbol;
	}
}
