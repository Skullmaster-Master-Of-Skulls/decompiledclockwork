using System;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003BC RID: 956
	public abstract class FunctionImportReturnTypePropertyMapping : MappingItem
	{
		// Token: 0x060022F8 RID: 8952 RVA: 0x000A3468 File Offset: 0x000A1668
		internal FunctionImportReturnTypePropertyMapping(LineInfo lineInfo)
		{
			this.LineInfo = lineInfo;
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060022F9 RID: 8953
		internal abstract string CMember { get; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060022FA RID: 8954
		internal abstract string SColumn { get; }

		// Token: 0x04000C44 RID: 3140
		internal readonly LineInfo LineInfo;
	}
}
