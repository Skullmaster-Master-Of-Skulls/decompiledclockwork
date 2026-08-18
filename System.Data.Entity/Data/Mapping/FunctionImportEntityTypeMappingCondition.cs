using System;

namespace System.Data.Mapping
{
	// Token: 0x02000226 RID: 550
	internal abstract class FunctionImportEntityTypeMappingCondition
	{
		// Token: 0x060023B1 RID: 9137 RVA: 0x00081344 File Offset: 0x0007F544
		protected FunctionImportEntityTypeMappingCondition(string columnName, LineInfo lineInfo)
		{
			this.ColumnName = EntityUtil.CheckArgumentNull<string>(columnName, "columnName");
			this.LineInfo = lineInfo;
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060023B2 RID: 9138
		internal abstract ValueCondition ConditionValue { get; }

		// Token: 0x060023B3 RID: 9139
		internal abstract bool ColumnValueMatchesCondition(object columnValue);

		// Token: 0x060023B4 RID: 9140 RVA: 0x00081364 File Offset: 0x0007F564
		public override string ToString()
		{
			return this.ConditionValue.ToString();
		}

		// Token: 0x04000FD2 RID: 4050
		internal readonly string ColumnName;

		// Token: 0x04000FD3 RID: 4051
		internal readonly LineInfo LineInfo;
	}
}
