using System;
using System.Collections.Generic;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x0200002D RID: 45
	internal class SqlSelectClauseBuilder : SqlBuilder
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x000117EB File Offset: 0x0000F9EB
		internal void AddOptionalColumn(OptionalColumn column)
		{
			if (this.m_optionalColumns == null)
			{
				this.m_optionalColumns = new List<OptionalColumn>();
			}
			this.m_optionalColumns.Add(column);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0001180C File Offset: 0x0000FA0C
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x00011814 File Offset: 0x0000FA14
		internal TopClause Top
		{
			get
			{
				return this.m_top;
			}
			set
			{
				this.m_top = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0001181D File Offset: 0x0000FA1D
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x00011825 File Offset: 0x0000FA25
		internal bool IsDistinct { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0001182E File Offset: 0x0000FA2E
		public override bool IsEmpty
		{
			get
			{
				return base.IsEmpty && (this.m_optionalColumns == null || this.m_optionalColumns.Count == 0);
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00011852 File Offset: 0x0000FA52
		internal SqlSelectClauseBuilder(Func<bool> isPartOfTopMostStatement)
		{
			this.m_isPartOfTopMostStatement = isPartOfTopMostStatement;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00011864 File Offset: 0x0000FA64
		public override void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
			writer.Write("SELECT ");
			if (this.IsDistinct)
			{
				writer.Write("DISTINCT ");
			}
			if (this.Top != null)
			{
				this.Top.WriteSql(writer, sqlGenerator);
			}
			if (this.IsEmpty)
			{
				writer.Write("*");
				return;
			}
			bool flag = this.WriteOptionalColumns(writer, sqlGenerator);
			if (!base.IsEmpty)
			{
				if (flag)
				{
					writer.Write(", ");
				}
				base.WriteSql(writer, sqlGenerator);
				return;
			}
			if (!flag)
			{
				this.m_optionalColumns[0].MarkAsUsed();
				this.m_optionalColumns[0].WriteSqlIfUsed(writer, sqlGenerator, "");
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00011910 File Offset: 0x0000FB10
		private bool WriteOptionalColumns(SqlWriter writer, SqlGenerator sqlGenerator)
		{
			if (this.m_optionalColumns == null)
			{
				return false;
			}
			if (this.m_isPartOfTopMostStatement() || this.IsDistinct)
			{
				foreach (OptionalColumn optionalColumn in this.m_optionalColumns)
				{
					optionalColumn.MarkAsUsed();
				}
			}
			string separator = "";
			bool result = false;
			foreach (OptionalColumn optionalColumn2 in this.m_optionalColumns)
			{
				if (optionalColumn2.WriteSqlIfUsed(writer, sqlGenerator, separator))
				{
					result = true;
					separator = ", ";
				}
			}
			return result;
		}

		// Token: 0x04000706 RID: 1798
		private List<OptionalColumn> m_optionalColumns;

		// Token: 0x04000707 RID: 1799
		private TopClause m_top;

		// Token: 0x04000709 RID: 1801
		private readonly Func<bool> m_isPartOfTopMostStatement;
	}
}
