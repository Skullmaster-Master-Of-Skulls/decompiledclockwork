using System;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003B1 RID: 945
	public abstract class FunctionImportEntityTypeMappingCondition : MappingItem
	{
		// Token: 0x06002269 RID: 8809 RVA: 0x000A0C72 File Offset: 0x0009EE72
		internal FunctionImportEntityTypeMappingCondition(string columnName, LineInfo lineInfo)
		{
			this._columnName = columnName;
			this.LineInfo = lineInfo;
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x000A0C88 File Offset: 0x0009EE88
		public string ColumnName
		{
			get
			{
				return this._columnName;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600226B RID: 8811
		internal abstract ValueCondition ConditionValue { get; }

		// Token: 0x0600226C RID: 8812
		internal abstract bool ColumnValueMatchesCondition(object columnValue);

		// Token: 0x0600226D RID: 8813 RVA: 0x000A0C90 File Offset: 0x0009EE90
		public override string ToString()
		{
			return this.ConditionValue.ToString();
		}

		// Token: 0x04000C23 RID: 3107
		private readonly string _columnName;

		// Token: 0x04000C24 RID: 3108
		internal readonly LineInfo LineInfo;
	}
}
