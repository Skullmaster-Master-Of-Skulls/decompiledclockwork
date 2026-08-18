using System;

namespace System.Data.Mapping
{
	// Token: 0x02000228 RID: 552
	internal sealed class FunctionImportEntityTypeMappingConditionIsNull : FunctionImportEntityTypeMappingCondition
	{
		// Token: 0x060023BA RID: 9146 RVA: 0x0008149C File Offset: 0x0007F69C
		internal FunctionImportEntityTypeMappingConditionIsNull(string columnName, bool isNull, LineInfo lineInfo) : base(columnName, lineInfo)
		{
			this.IsNull = isNull;
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x000814AD File Offset: 0x0007F6AD
		internal override ValueCondition ConditionValue
		{
			get
			{
				if (!this.IsNull)
				{
					return ValueCondition.IsNotNull;
				}
				return ValueCondition.IsNull;
			}
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000814C4 File Offset: 0x0007F6C4
		internal override bool ColumnValueMatchesCondition(object columnValue)
		{
			bool flag = columnValue == null || Convert.IsDBNull(columnValue);
			return flag == this.IsNull;
		}

		// Token: 0x04000FD6 RID: 4054
		internal readonly bool IsNull;
	}
}
