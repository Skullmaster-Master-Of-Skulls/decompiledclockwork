using System;
using System.Collections.Generic;
using System.Globalization;

namespace Oracle.DataAccess.Client.SqlGen
{
	// Token: 0x020000DF RID: 223
	internal sealed class SqlSelectStatement : ISqlFragment
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x000504A4 File Offset: 0x0004F4A4
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x000504AC File Offset: 0x0004F4AC
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

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x000504B5 File Offset: 0x0004F4B5
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x000504BD File Offset: 0x0004F4BD
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

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x000504C6 File Offset: 0x0004F4C6
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x000504CE File Offset: 0x0004F4CE
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

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x000504D7 File Offset: 0x0004F4D7
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x000504DF File Offset: 0x0004F4DF
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

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x000504E8 File Offset: 0x0004F4E8
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

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x00050503 File Offset: 0x0004F503
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

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x0005051E File Offset: 0x0004F51E
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x00050526 File Offset: 0x0004F526
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

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x0005052F File Offset: 0x0004F52F
		internal SqlBuilder Select
		{
			get
			{
				return this.select;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00050537 File Offset: 0x0004F537
		internal SqlBuilder From
		{
			get
			{
				return this.from;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0005053F File Offset: 0x0004F53F
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

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x0005055A File Offset: 0x0004F55A
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

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00050575 File Offset: 0x0004F575
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

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x00050590 File Offset: 0x0004F590
		// (set) Token: 0x06000839 RID: 2105 RVA: 0x00050597 File Offset: 0x0004F597
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

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0005059F File Offset: 0x0004F59F
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x000505A7 File Offset: 0x0004F5A7
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

		// Token: 0x0600083C RID: 2108 RVA: 0x000505B0 File Offset: 0x0004F5B0
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

		// Token: 0x040006EC RID: 1772
		private bool outputColumnsRenamed;

		// Token: 0x040006ED RID: 1773
		private Dictionary<string, Symbol> outputColumns;

		// Token: 0x040006EE RID: 1774
		private bool isDistinct;

		// Token: 0x040006EF RID: 1775
		private List<Symbol> allJoinExtents;

		// Token: 0x040006F0 RID: 1776
		private List<Symbol> fromExtents;

		// Token: 0x040006F1 RID: 1777
		private Dictionary<Symbol, bool> outerExtents;

		// Token: 0x040006F2 RID: 1778
		private TopClause top;

		// Token: 0x040006F3 RID: 1779
		private SqlBuilder select = new SqlBuilder();

		// Token: 0x040006F4 RID: 1780
		private SqlBuilder from = new SqlBuilder();

		// Token: 0x040006F5 RID: 1781
		private SqlBuilder where;

		// Token: 0x040006F6 RID: 1782
		private SqlBuilder groupBy;

		// Token: 0x040006F7 RID: 1783
		private SqlBuilder orderBy;

		// Token: 0x040006F8 RID: 1784
		private static TopClause top_s;

		// Token: 0x040006F9 RID: 1785
		private bool isTopMost;
	}
}
