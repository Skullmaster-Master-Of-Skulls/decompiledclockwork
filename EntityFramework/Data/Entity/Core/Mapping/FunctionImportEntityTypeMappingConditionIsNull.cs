using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003B2 RID: 946
	public sealed class FunctionImportEntityTypeMappingConditionIsNull : FunctionImportEntityTypeMappingCondition
	{
		// Token: 0x0600226E RID: 8814 RVA: 0x000A0C9D File Offset: 0x0009EE9D
		public FunctionImportEntityTypeMappingConditionIsNull(string columnName, bool isNull) : this(Check.NotNull<string>(columnName, "columnName"), isNull, LineInfo.Empty)
		{
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x000A0CB6 File Offset: 0x0009EEB6
		internal FunctionImportEntityTypeMappingConditionIsNull(string columnName, bool isNull, LineInfo lineInfo) : base(columnName, lineInfo)
		{
			this._isNull = isNull;
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x000A0CC7 File Offset: 0x0009EEC7
		public bool IsNull
		{
			get
			{
				return this._isNull;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x000A0CCF File Offset: 0x0009EECF
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

		// Token: 0x06002272 RID: 8818 RVA: 0x000A0CE4 File Offset: 0x0009EEE4
		internal override bool ColumnValueMatchesCondition(object columnValue)
		{
			bool flag = columnValue == null || Convert.IsDBNull(columnValue);
			return flag == this.IsNull;
		}

		// Token: 0x04000C25 RID: 3109
		private readonly bool _isNull;
	}
}
