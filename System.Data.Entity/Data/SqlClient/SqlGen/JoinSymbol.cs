using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x0200003A RID: 58
	internal sealed class JoinSymbol : Symbol
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0001764E File Offset: 0x0001584E
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x00017669 File Offset: 0x00015869
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00017672 File Offset: 0x00015872
		internal List<Symbol> ExtentList
		{
			get
			{
				return this.extentList;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0001767A File Offset: 0x0001587A
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x00017695 File Offset: 0x00015895
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

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0001769E File Offset: 0x0001589E
		internal Dictionary<string, Symbol> NameToExtent
		{
			get
			{
				return this.nameToExtent;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x000176A6 File Offset: 0x000158A6
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x000176AE File Offset: 0x000158AE
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

		// Token: 0x06000542 RID: 1346 RVA: 0x000176B8 File Offset: 0x000158B8
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

		// Token: 0x0400073C RID: 1852
		private List<Symbol> columnList;

		// Token: 0x0400073D RID: 1853
		private List<Symbol> extentList;

		// Token: 0x0400073E RID: 1854
		private List<Symbol> flattenedExtentList;

		// Token: 0x0400073F RID: 1855
		private Dictionary<string, Symbol> nameToExtent;

		// Token: 0x04000740 RID: 1856
		private bool isNestedJoin;
	}
}
