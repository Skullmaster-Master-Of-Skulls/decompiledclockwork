using System;

namespace System.Data.OleDb
{
	// Token: 0x0200024C RID: 588
	internal sealed class MetaData : IComparable
	{
		// Token: 0x0600258D RID: 9613 RVA: 0x001003C0 File Offset: 0x000FF7C0
		int IComparable.CompareTo(object obj)
		{
			if (this.isHidden == (obj as MetaData).isHidden)
			{
				long num = (long)this.ordinal - (long)(obj as MetaData).ordinal;
				if (0L < num)
				{
					return 1;
				}
				if (num >= 0L)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (!this.isHidden)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x00100418 File Offset: 0x000FF818
		internal MetaData()
		{
		}

		// Token: 0x040015F1 RID: 5617
		internal Bindings bindings;

		// Token: 0x040015F2 RID: 5618
		internal ColumnBinding columnBinding;

		// Token: 0x040015F3 RID: 5619
		internal string columnName;

		// Token: 0x040015F4 RID: 5620
		internal Guid guid;

		// Token: 0x040015F5 RID: 5621
		internal int kind;

		// Token: 0x040015F6 RID: 5622
		internal IntPtr propid;

		// Token: 0x040015F7 RID: 5623
		internal string idname;

		// Token: 0x040015F8 RID: 5624
		internal NativeDBType type;

		// Token: 0x040015F9 RID: 5625
		internal IntPtr ordinal;

		// Token: 0x040015FA RID: 5626
		internal int size;

		// Token: 0x040015FB RID: 5627
		internal int flags;

		// Token: 0x040015FC RID: 5628
		internal byte precision;

		// Token: 0x040015FD RID: 5629
		internal byte scale;

		// Token: 0x040015FE RID: 5630
		internal bool isAutoIncrement;

		// Token: 0x040015FF RID: 5631
		internal bool isUnique;

		// Token: 0x04001600 RID: 5632
		internal bool isKeyColumn;

		// Token: 0x04001601 RID: 5633
		internal bool isHidden;

		// Token: 0x04001602 RID: 5634
		internal string baseSchemaName;

		// Token: 0x04001603 RID: 5635
		internal string baseCatalogName;

		// Token: 0x04001604 RID: 5636
		internal string baseTableName;

		// Token: 0x04001605 RID: 5637
		internal string baseColumnName;
	}
}
