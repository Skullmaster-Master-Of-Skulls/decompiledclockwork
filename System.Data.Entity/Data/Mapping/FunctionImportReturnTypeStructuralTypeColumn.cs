using System;
using System.Data.Metadata.Edm;

namespace System.Data.Mapping
{
	// Token: 0x0200025D RID: 605
	internal sealed class FunctionImportReturnTypeStructuralTypeColumn
	{
		// Token: 0x06002589 RID: 9609 RVA: 0x0008C018 File Offset: 0x0008A218
		internal FunctionImportReturnTypeStructuralTypeColumn(string columnName, StructuralType type, bool isTypeOf, LineInfo lineInfo)
		{
			this.ColumnName = columnName;
			this.IsTypeOf = isTypeOf;
			this.Type = type;
			this.LineInfo = lineInfo;
		}

		// Token: 0x04001131 RID: 4401
		internal readonly StructuralType Type;

		// Token: 0x04001132 RID: 4402
		internal readonly bool IsTypeOf;

		// Token: 0x04001133 RID: 4403
		internal readonly string ColumnName;

		// Token: 0x04001134 RID: 4404
		internal readonly LineInfo LineInfo;
	}
}
