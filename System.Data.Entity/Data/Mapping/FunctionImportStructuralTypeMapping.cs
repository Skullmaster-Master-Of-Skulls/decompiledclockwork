using System;
using System.Collections.ObjectModel;

namespace System.Data.Mapping
{
	// Token: 0x02000257 RID: 599
	internal abstract class FunctionImportStructuralTypeMapping
	{
		// Token: 0x06002580 RID: 9600 RVA: 0x0008BD6D File Offset: 0x00089F6D
		internal FunctionImportStructuralTypeMapping(Collection<FunctionImportReturnTypePropertyMapping> columnsRenameList, LineInfo lineInfo)
		{
			this.ColumnsRenameList = columnsRenameList;
			this.LineInfo = lineInfo;
		}

		// Token: 0x04001127 RID: 4391
		internal readonly LineInfo LineInfo;

		// Token: 0x04001128 RID: 4392
		internal readonly Collection<FunctionImportReturnTypePropertyMapping> ColumnsRenameList;
	}
}
