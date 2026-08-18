using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000218 RID: 536
	internal sealed class CollectionTypeDefinition : Node
	{
		// Token: 0x06001364 RID: 4964 RVA: 0x000504A3 File Offset: 0x0004E6A3
		internal CollectionTypeDefinition(Node elementTypeDef)
		{
			this._elementTypeDef = elementTypeDef;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x000504B2 File Offset: 0x0004E6B2
		internal Node ElementTypeDef
		{
			get
			{
				return this._elementTypeDef;
			}
		}

		// Token: 0x040005D6 RID: 1494
		private readonly Node _elementTypeDef;
	}
}
