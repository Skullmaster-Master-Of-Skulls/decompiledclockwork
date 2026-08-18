using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x0200003C RID: 60
	internal sealed class SqlSelectStatement : ISqlFragment
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00014088 File Offset: 0x00012288
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x00014090 File Offset: 0x00012290
		internal bool OutputColumnsRenamed { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00014099 File Offset: 0x00012299
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x000140A1 File Offset: 0x000122A1
		internal Dictionary<string, Symbol> OutputColumns { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x000140AA File Offset: 0x000122AA
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x000140B2 File Offset: 0x000122B2
		internal List<Symbol> AllJoinExtents { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x000140BB File Offset: 0x000122BB
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

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x000140D6 File Offset: 0x000122D6
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

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x000140F1 File Offset: 0x000122F1
		internal SqlSelectClauseBuilder Select
		{
			get
			{
				return this.select;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x000140F9 File Offset: 0x000122F9
		internal SqlBuilder From
		{
			get
			{
				return this.from;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00014101 File Offset: 0x00012301
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

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0001411C File Offset: 0x0001231C
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00014137 File Offset: 0x00012337
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

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00014152 File Offset: 0x00012352
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0001415A File Offset: 0x0001235A
		internal bool IsTopMost { get; set; }

		// Token: 0x0600042E RID: 1070 RVA: 0x0001416C File Offset: 0x0001236C
		internal SqlSelectStatement()
		{
			this.select = new SqlSelectClauseBuilder(() => this.IsTopMost);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000141A8 File Offset: 0x000123A8
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
			this.select.WriteSql(writer, sqlGenerator);
			writer.WriteLine();
			writer.Write("FROM ");
			this.From.WriteSql(writer, sqlGenerator);
			if (this.where != null && !this.Where.IsEmpty)
			{
				writer.WriteLine();
				writer.Write("WHERE ");
				this.Where.WriteSql(writer, sqlGenerator);
			}
			if (this.groupBy != null && !this.GroupBy.IsEmpty)
			{
				writer.WriteLine();
				writer.Write("GROUP BY ");
				this.GroupBy.WriteSql(writer, sqlGenerator);
			}
			if (this.orderBy != null && !this.OrderBy.IsEmpty && (this.IsTopMost || this.Select.Top != null || this.Select.Skip != null))
			{
				writer.WriteLine();
				writer.Write("ORDER BY ");
				this.OrderBy.WriteSql(writer, sqlGenerator);
			}
			if (this.Select.Skip != null)
			{
				writer.WriteLine();
				SqlSelectStatement.WriteOffsetFetch(writer, this.Select.Top, this.Select.Skip, sqlGenerator);
			}
			writer.Indent--;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000144A8 File Offset: 0x000126A8
		private static void WriteOffsetFetch(SqlWriter writer, TopClause top, SkipClause skip, SqlGenerator sqlGenerator)
		{
			skip.WriteSql(writer, sqlGenerator);
			if (top != null)
			{
				writer.Write("FETCH NEXT ");
				top.TopCount.WriteSql(writer, sqlGenerator);
				writer.Write(" ROWS ONLY ");
			}
		}

		// Token: 0x040000E8 RID: 232
		private List<Symbol> fromExtents;

		// Token: 0x040000E9 RID: 233
		private Dictionary<Symbol, bool> outerExtents;

		// Token: 0x040000EA RID: 234
		private readonly SqlSelectClauseBuilder select;

		// Token: 0x040000EB RID: 235
		private readonly SqlBuilder from = new SqlBuilder();

		// Token: 0x040000EC RID: 236
		private SqlBuilder where;

		// Token: 0x040000ED RID: 237
		private SqlBuilder groupBy;

		// Token: 0x040000EE RID: 238
		private SqlBuilder orderBy;
	}
}
