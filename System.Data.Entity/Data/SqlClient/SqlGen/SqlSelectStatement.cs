using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x02000033 RID: 51
	internal sealed class SqlSelectStatement : ISqlFragment
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00016AB4 File Offset: 0x00014CB4
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x00016ABC File Offset: 0x00014CBC
		internal bool OutputColumnsRenamed { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00016AC5 File Offset: 0x00014CC5
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x00016ACD File Offset: 0x00014CCD
		internal Dictionary<string, Symbol> OutputColumns { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00016AD6 File Offset: 0x00014CD6
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x00016ADE File Offset: 0x00014CDE
		internal List<Symbol> AllJoinExtents { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00016AE7 File Offset: 0x00014CE7
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00016B02 File Offset: 0x00014D02
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00016B1D File Offset: 0x00014D1D
		internal SqlSelectClauseBuilder Select
		{
			get
			{
				return this.select;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00016B25 File Offset: 0x00014D25
		internal SqlBuilder From
		{
			get
			{
				return this.from;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00016B2D File Offset: 0x00014D2D
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00016B48 File Offset: 0x00014D48
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00016B63 File Offset: 0x00014D63
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00016B7E File Offset: 0x00014D7E
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x00016B86 File Offset: 0x00014D86
		internal bool IsTopMost { get; set; }

		// Token: 0x060004E0 RID: 1248 RVA: 0x00016B8F File Offset: 0x00014D8F
		internal SqlSelectStatement()
		{
			this.select = new SqlSelectClauseBuilder(() => this.IsTopMost);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00016BBC File Offset: 0x00014DBC
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
			if (this.orderBy != null && !this.OrderBy.IsEmpty && (this.IsTopMost || this.Select.Top != null))
			{
				writer.WriteLine();
				writer.Write("ORDER BY ");
				this.OrderBy.WriteSql(writer, sqlGenerator);
			}
			int indent = writer.Indent - 1;
			writer.Indent = indent;
		}

		// Token: 0x04000726 RID: 1830
		private List<Symbol> fromExtents;

		// Token: 0x04000727 RID: 1831
		private Dictionary<Symbol, bool> outerExtents;

		// Token: 0x04000728 RID: 1832
		private readonly SqlSelectClauseBuilder select;

		// Token: 0x04000729 RID: 1833
		private readonly SqlBuilder from = new SqlBuilder();

		// Token: 0x0400072A RID: 1834
		private SqlBuilder where;

		// Token: 0x0400072B RID: 1835
		private SqlBuilder groupBy;

		// Token: 0x0400072C RID: 1836
		private SqlBuilder orderBy;
	}
}
