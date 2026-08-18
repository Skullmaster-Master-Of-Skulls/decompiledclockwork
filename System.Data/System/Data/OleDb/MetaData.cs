using System;

namespace System.Data.OleDb
{
	// Token: 0x02000223 RID: 547
	internal sealed class MetaData : IComparable
	{
		// Token: 0x06001F74 RID: 8052 RVA: 0x0027AE48 File Offset: 0x0027A248
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

		// Token: 0x06001F75 RID: 8053 RVA: 0x0027AEA8 File Offset: 0x0027A2A8
		internal MetaData()
		{
		}

		// Token: 0x040012DC RID: 4828
		internal Bindings bindings;

		// Token: 0x040012DD RID: 4829
		internal ColumnBinding columnBinding;

		// Token: 0x040012DE RID: 4830
		internal string columnName;

		// Token: 0x040012DF RID: 4831
		internal Guid guid;

		// Token: 0x040012E0 RID: 4832
		internal int kind;

		// Token: 0x040012E1 RID: 4833
		internal IntPtr propid;

		// Token: 0x040012E2 RID: 4834
		internal string idname;

		// Token: 0x040012E3 RID: 4835
		internal NativeDBType type;

		// Token: 0x040012E4 RID: 4836
		internal IntPtr ordinal;

		// Token: 0x040012E5 RID: 4837
		internal int size;

		// Token: 0x040012E6 RID: 4838
		internal int flags;

		// Token: 0x040012E7 RID: 4839
		internal byte precision;

		// Token: 0x040012E8 RID: 4840
		internal byte scale;

		// Token: 0x040012E9 RID: 4841
		internal bool isAutoIncrement;

		// Token: 0x040012EA RID: 4842
		internal bool isUnique;

		// Token: 0x040012EB RID: 4843
		internal bool isKeyColumn;

		// Token: 0x040012EC RID: 4844
		internal bool isHidden;

		// Token: 0x040012ED RID: 4845
		internal string baseSchemaName;

		// Token: 0x040012EE RID: 4846
		internal string baseCatalogName;

		// Token: 0x040012EF RID: 4847
		internal string baseTableName;

		// Token: 0x040012F0 RID: 4848
		internal string baseColumnName;
	}
}
