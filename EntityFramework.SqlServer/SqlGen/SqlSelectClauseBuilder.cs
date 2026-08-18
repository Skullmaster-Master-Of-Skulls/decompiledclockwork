using System;
using System.Collections.Generic;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x0200003B RID: 59
	internal class SqlSelectClauseBuilder : SqlBuilder
	{
		// Token: 0x06000414 RID: 1044 RVA: 0x00013E81 File Offset: 0x00012081
		internal void AddOptionalColumn(OptionalColumn column)
		{
			if (this.m_optionalColumns == null)
			{
				this.m_optionalColumns = new List<OptionalColumn>();
			}
			this.m_optionalColumns.Add(column);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00013EA2 File Offset: 0x000120A2
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x00013EAA File Offset: 0x000120AA
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

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00013EB3 File Offset: 0x000120B3
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x00013EBB File Offset: 0x000120BB
		internal SkipClause Skip
		{
			get
			{
				return this.m_skip;
			}
			set
			{
				this.m_skip = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00013EC4 File Offset: 0x000120C4
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x00013ECC File Offset: 0x000120CC
		internal bool IsDistinct { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00013ED5 File Offset: 0x000120D5
		public override bool IsEmpty
		{
			get
			{
				return base.IsEmpty && (this.m_optionalColumns == null || this.m_optionalColumns.Count == 0);
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00013EF9 File Offset: 0x000120F9
		internal SqlSelectClauseBuilder(Func<bool> isPartOfTopMostStatement)
		{
			this.m_isPartOfTopMostStatement = isPartOfTopMostStatement;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00013F08 File Offset: 0x00012108
		public override void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
			writer.Write("SELECT ");
			if (this.IsDistinct)
			{
				writer.Write("DISTINCT ");
			}
			if (this.Top != null && this.Skip == null)
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

		// Token: 0x0600041E RID: 1054 RVA: 0x00013FBC File Offset: 0x000121BC
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

		// Token: 0x040000E3 RID: 227
		private List<OptionalColumn> m_optionalColumns;

		// Token: 0x040000E4 RID: 228
		private TopClause m_top;

		// Token: 0x040000E5 RID: 229
		private SkipClause m_skip;

		// Token: 0x040000E6 RID: 230
		private readonly Func<bool> m_isPartOfTopMostStatement;
	}
}
