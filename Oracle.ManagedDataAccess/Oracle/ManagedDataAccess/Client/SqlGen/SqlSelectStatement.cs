using System;
using System.Collections.Generic;
using System.Globalization;

namespace Oracle.ManagedDataAccess.Client.SqlGen
{
	// Token: 0x020000F4 RID: 244
	internal sealed class SqlSelectStatement : ISqlFragment
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00074D24 File Offset: 0x00072F24
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x00074D2C File Offset: 0x00072F2C
		internal bool OutputColumnsRenamed
		{
			get
			{
				return this.outputColumnsRenamed;
			}
			set
			{
				this.outputColumnsRenamed = value;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00074D38 File Offset: 0x00072F38
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x00074D40 File Offset: 0x00072F40
		internal Dictionary<string, Symbol> OutputColumns
		{
			get
			{
				return this.outputColumns;
			}
			set
			{
				this.outputColumns = value;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00074D4C File Offset: 0x00072F4C
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x00074D54 File Offset: 0x00072F54
		internal bool IsDistinct
		{
			get
			{
				return this.isDistinct;
			}
			set
			{
				this.isDistinct = value;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00074D60 File Offset: 0x00072F60
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x00074D68 File Offset: 0x00072F68
		internal List<Symbol> AllJoinExtents
		{
			get
			{
				return this.allJoinExtents;
			}
			set
			{
				this.allJoinExtents = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00074D74 File Offset: 0x00072F74
		internal List<Symbol> FromExtents
		{
			get
			{
				if (this.fromExtents == null)
				{
					this.fromExtents = new List<Symbol>();
				}
				return this.fromExtents;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00074D90 File Offset: 0x00072F90
		internal Dictionary<Symbol, bool> OuterExtents
		{
			get
			{
				if (this.outerExtents == null)
				{
					this.outerExtents = new Dictionary<Symbol, bool>();
				}
				return this.outerExtents;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00074DAC File Offset: 0x00072FAC
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x00074DB4 File Offset: 0x00072FB4
		internal TopClause Top
		{
			get
			{
				return this.top;
			}
			set
			{
				this.top = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00074DC0 File Offset: 0x00072FC0
		internal SqlBuilder Select
		{
			get
			{
				return this.select;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00074DC8 File Offset: 0x00072FC8
		internal SqlBuilder From
		{
			get
			{
				return this.from;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x00074DD0 File Offset: 0x00072FD0
		internal SqlBuilder Where
		{
			get
			{
				if (this.where == null)
				{
					this.where = new SqlBuilder();
				}
				return this.where;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00074DEC File Offset: 0x00072FEC
		internal SqlBuilder GroupBy
		{
			get
			{
				if (this.groupBy == null)
				{
					this.groupBy = new SqlBuilder();
				}
				return this.groupBy;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x00074E08 File Offset: 0x00073008
		public SqlBuilder OrderBy
		{
			get
			{
				if (this.orderBy == null)
				{
					this.orderBy = new SqlBuilder();
				}
				return this.orderBy;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00074E24 File Offset: 0x00073024
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x00074E2C File Offset: 0x0007302C
		internal static TopClause Top_s
		{
			get
			{
				return SqlSelectStatement.top_s;
			}
			set
			{
				SqlSelectStatement.top_s = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x00074E34 File Offset: 0x00073034
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x00074E3C File Offset: 0x0007303C
		internal bool IsTopMost
		{
			get
			{
				return this.isTopMost;
			}
			set
			{
				this.isTopMost = value;
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00074E48 File Offset: 0x00073048
		public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
			List<string> list = null;
			if (this.outerExtents != null && 0 < this.outerExtents.Count)
			{
				foreach (Symbol symbol in this.outerExtents.Keys)
				{
					JoinSymbol joinSymbol = symbol as JoinSymbol;
					if (joinSymbol != null)
					{
						using (List<Symbol>.Enumerator enumerator2 = joinSymbol.FlattenedExtentList.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								Symbol symbol2 = enumerator2.Current;
								if (list == null)
								{
									list = new List<string>();
								}
								list.Add(symbol2.NewName);
							}
							continue;
						}
					}
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(symbol.NewName);
				}
			}
			List<Symbol> list2 = this.AllJoinExtents ?? this.fromExtents;
			if (list2 != null)
			{
				foreach (Symbol symbol3 in list2)
				{
					if (list != null && list.Contains(symbol3.Name))
					{
						int num = sqlGenerator.AllExtentNames[symbol3.Name];
						string text;
						do
						{
							num++;
							text = symbol3.Name + num.ToString(CultureInfo.InvariantCulture);
						}
						while (sqlGenerator.AllExtentNames.ContainsKey(text));
						sqlGenerator.AllExtentNames[symbol3.Name] = num;
						symbol3.NewName = text;
						sqlGenerator.AllExtentNames[text] = 0;
					}
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(symbol3.NewName);
				}
			}
			writer.Indent++;
			if ((this.IsTopMost && this.Top != null && this.orderBy != null && !this.OrderBy.IsEmpty) || (!this.IsTopMost && this.orderBy != null && !this.OrderBy.IsEmpty))
			{
				writer.Write("SELECT * ");
				writer.WriteLine();
				writer.Write("FROM ( ");
				writer.WriteLine();
			}
			writer.Write("SELECT ");
			if (this.IsDistinct)
			{
				writer.Write("DISTINCT ");
			}
			if (this.select == null || this.Select.IsEmpty)
			{
				writer.Write("*");
			}
			else
			{
				this.Select.WriteSql(writer, sqlGenerator);
			}
			writer.WriteLine();
			writer.Write("FROM ");
			this.From.WriteSql(writer, sqlGenerator);
			if (this.where != null && !this.Where.IsEmpty)
			{
				writer.WriteLine();
				writer.Write("WHERE (");
				this.Where.WriteSql(writer, sqlGenerator);
				writer.Write(")");
				if (this.Top != null && (this.orderBy == null || (this.orderBy != null && this.OrderBy.IsEmpty)))
				{
					writer.Write(" AND (");
					this.Top.WriteSql(writer, sqlGenerator);
					writer.Write(")");
				}
			}
			else if (this.Top != null && (this.orderBy == null || (this.orderBy != null && this.OrderBy.IsEmpty)))
			{
				writer.WriteLine();
				writer.Write("WHERE (");
				this.Top.WriteSql(writer, sqlGenerator);
				writer.Write(")");
			}
			if (this.groupBy != null && !this.GroupBy.IsEmpty)
			{
				writer.WriteLine();
				writer.Write("GROUP BY ");
				this.GroupBy.WriteSql(writer, sqlGenerator);
			}
			if (this.orderBy != null && !this.OrderBy.IsEmpty && (this.IsTopMost || this.Top != null))
			{
				writer.WriteLine();
				writer.Write("ORDER BY ");
				this.OrderBy.WriteSql(writer, sqlGenerator);
			}
			if (this.Top != null && this.orderBy != null && !this.OrderBy.IsEmpty)
			{
				SqlSelectStatement.Top_s = this.Top;
			}
			if (this.IsTopMost || (this.orderBy != null && !this.OrderBy.IsEmpty))
			{
				if ((this.IsTopMost && this.Top != null && this.orderBy != null && !this.OrderBy.IsEmpty) || (!this.IsTopMost && this.orderBy != null && !this.OrderBy.IsEmpty))
				{
					writer.WriteLine();
					writer.Write(")");
				}
				if (SqlSelectStatement.Top_s != null)
				{
					writer.WriteLine();
					writer.Write("WHERE (");
					SqlSelectStatement.Top_s.WriteSql(writer, sqlGenerator);
					writer.Write(")");
					SqlSelectStatement.Top_s = null;
				}
			}
			writer.Indent--;
		}

		// Token: 0x04000C71 RID: 3185
		private bool outputColumnsRenamed;

		// Token: 0x04000C72 RID: 3186
		private Dictionary<string, Symbol> outputColumns;

		// Token: 0x04000C73 RID: 3187
		private bool isDistinct;

		// Token: 0x04000C74 RID: 3188
		private List<Symbol> allJoinExtents;

		// Token: 0x04000C75 RID: 3189
		private List<Symbol> fromExtents;

		// Token: 0x04000C76 RID: 3190
		private Dictionary<Symbol, bool> outerExtents;

		// Token: 0x04000C77 RID: 3191
		private TopClause top;

		// Token: 0x04000C78 RID: 3192
		private SqlBuilder select = new SqlBuilder();

		// Token: 0x04000C79 RID: 3193
		private SqlBuilder from = new SqlBuilder();

		// Token: 0x04000C7A RID: 3194
		private SqlBuilder where;

		// Token: 0x04000C7B RID: 3195
		private SqlBuilder groupBy;

		// Token: 0x04000C7C RID: 3196
		private SqlBuilder orderBy;

		// Token: 0x04000C7D RID: 3197
		private static TopClause top_s;

		// Token: 0x04000C7E RID: 3198
		private bool isTopMost;
	}
}
