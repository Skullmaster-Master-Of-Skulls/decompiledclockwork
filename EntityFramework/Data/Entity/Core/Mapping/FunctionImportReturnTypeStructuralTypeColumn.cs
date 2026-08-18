using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003BE RID: 958
	internal sealed class FunctionImportReturnTypeStructuralTypeColumn
	{
		// Token: 0x06002301 RID: 8961 RVA: 0x000A34D1 File Offset: 0x000A16D1
		internal FunctionImportReturnTypeStructuralTypeColumn(string columnName, StructuralType type, bool isTypeOf, LineInfo lineInfo)
		{
			this.ColumnName = columnName;
			this.IsTypeOf = isTypeOf;
			this.Type = type;
			this.LineInfo = lineInfo;
		}

		// Token: 0x04000C47 RID: 3143
		internal readonly StructuralType Type;

		// Token: 0x04000C48 RID: 3144
		internal readonly bool IsTypeOf;

		// Token: 0x04000C49 RID: 3145
		internal readonly string ColumnName;

		// Token: 0x04000C4A RID: 3146
		internal readonly LineInfo LineInfo;
	}
}
