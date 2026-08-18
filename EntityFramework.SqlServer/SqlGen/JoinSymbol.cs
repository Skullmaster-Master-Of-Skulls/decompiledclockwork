using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000031 RID: 49
	internal sealed class JoinSymbol : Symbol
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000BA63 File Offset: 0x00009C63
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000BA7E File Offset: 0x00009C7E
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000BA87 File Offset: 0x00009C87
		internal List<Symbol> ExtentList
		{
			get
			{
				return this.extentList;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000BA8F File Offset: 0x00009C8F
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000BAAA File Offset: 0x00009CAA
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000BAB3 File Offset: 0x00009CB3
		internal Dictionary<string, Symbol> NameToExtent
		{
			get
			{
				return this.nameToExtent;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000BABB File Offset: 0x00009CBB
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000BAC3 File Offset: 0x00009CC3
		internal bool IsNestedJoin { get; set; }

		// Token: 0x060002BC RID: 700 RVA: 0x0000BACC File Offset: 0x00009CCC
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

		// Token: 0x04000087 RID: 135
		private List<Symbol> columnList;

		// Token: 0x04000088 RID: 136
		private readonly List<Symbol> extentList;

		// Token: 0x04000089 RID: 137
		private List<Symbol> flattenedExtentList;

		// Token: 0x0400008A RID: 138
		private readonly Dictionary<string, Symbol> nameToExtent;
	}
}
