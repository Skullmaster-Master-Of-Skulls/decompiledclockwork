using System;
using System.Collections.ObjectModel;
using System.Data.Metadata.Edm;

namespace System.Data.Mapping
{
	// Token: 0x02000259 RID: 601
	internal sealed class FunctionImportComplexTypeMapping : FunctionImportStructuralTypeMapping
	{
		// Token: 0x06002584 RID: 9604 RVA: 0x0008BE54 File Offset: 0x0008A054
		internal FunctionImportComplexTypeMapping(ComplexType returnType, Collection<FunctionImportReturnTypePropertyMapping> columnsRenameList, LineInfo lineInfo) : base(columnsRenameList, lineInfo)
		{
			this.ReturnType = returnType;
		}

		// Token: 0x0400112C RID: 4396
		internal readonly ComplexType ReturnType;
	}
}
