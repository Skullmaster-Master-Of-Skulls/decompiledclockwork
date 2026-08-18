using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;

namespace Oracle.ManagedDataAccess.Client.SqlGen
{
	// Token: 0x020000F0 RID: 240
	internal sealed class JoinSymbol : Symbol
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0006E248 File Offset: 0x0006C448
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x0006E264 File Offset: 0x0006C464
		internal List<Symbol> ColumnList
		{
			get
			{
				if (this.columnList == null)
				{
					this.columnList = new List<Symbol>();
				}
				return this.columnList;
			}
			set
			{
				this.columnList = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0006E270 File Offset: 0x0006C470
		internal List<Symbol> ExtentList
		{
			get
			{
				return this.extentList;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0006E278 File Offset: 0x0006C478
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x0006E294 File Offset: 0x0006C494
		internal List<Symbol> FlattenedExtentList
		{
			get
			{
				if (this.flattenedExtentList == null)
				{
					this.flattenedExtentList = new List<Symbol>();
				}
				return this.flattenedExtentList;
			}
			set
			{
				this.flattenedExtentList = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0006E2A0 File Offset: 0x0006C4A0
		internal Dictionary<string, Symbol> NameToExtent
		{
			get
			{
				return this.nameToExtent;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0006E2A8 File Offset: 0x0006C4A8
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x0006E2B0 File Offset: 0x0006C4B0
		internal bool IsNestedJoin
		{
			get
			{
				return this.isNestedJoin;
			}
			set
			{
				this.isNestedJoin = value;
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0006E2BC File Offset: 0x0006C4BC
		public JoinSymbol(string name, TypeUsage type, List<Symbol> extents) : base(name, type)
		{
			this.extentList = new List<Symbol>(extents.Count);
			this.nameToExtent = new Dictionary<string, Symbol>(extents.Count, StringComparer.OrdinalIgnoreCase);
			foreach (Symbol symbol in extents)
			{
				this.nameToExtent[symbol.Name] = symbol;
				this.ExtentList.Add(symbol);
			}
		}

		// Token: 0x04000C5B RID: 3163
		private List<Symbol> columnList;

		// Token: 0x04000C5C RID: 3164
		private List<Symbol> extentList;

		// Token: 0x04000C5D RID: 3165
		private List<Symbol> flattenedExtentList;

		// Token: 0x04000C5E RID: 3166
		private Dictionary<string, Symbol> nameToExtent;

		// Token: 0x04000C5F RID: 3167
		private bool isNestedJoin;
	}
}
