using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;

namespace Oracle.DataAccess.Client.SqlGen
{
	// Token: 0x0200004C RID: 76
	internal sealed class JoinSymbol : Symbol
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000346 RID: 838 RVA: 0x000286C4 File Offset: 0x000276C4
		// (set) Token: 0x06000347 RID: 839 RVA: 0x000286DF File Offset: 0x000276DF
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

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000348 RID: 840 RVA: 0x000286E8 File Offset: 0x000276E8
		internal List<Symbol> ExtentList
		{
			get
			{
				return this.extentList;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000349 RID: 841 RVA: 0x000286F0 File Offset: 0x000276F0
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0002870B File Offset: 0x0002770B
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

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00028714 File Offset: 0x00027714
		internal Dictionary<string, Symbol> NameToExtent
		{
			get
			{
				return this.nameToExtent;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0002871C File Offset: 0x0002771C
		// (set) Token: 0x0600034D RID: 845 RVA: 0x00028724 File Offset: 0x00027724
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

		// Token: 0x0600034E RID: 846 RVA: 0x00028730 File Offset: 0x00027730
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

		// Token: 0x0400025E RID: 606
		private List<Symbol> columnList;

		// Token: 0x0400025F RID: 607
		private List<Symbol> extentList;

		// Token: 0x04000260 RID: 608
		private List<Symbol> flattenedExtentList;

		// Token: 0x04000261 RID: 609
		private Dictionary<string, Symbol> nameToExtent;

		// Token: 0x04000262 RID: 610
		private bool isNestedJoin;
	}
}
