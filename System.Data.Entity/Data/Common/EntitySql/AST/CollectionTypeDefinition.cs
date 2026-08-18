using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000387 RID: 903
	internal sealed class CollectionTypeDefinition : Node
	{
		// Token: 0x0600326A RID: 12906 RVA: 0x000C5233 File Offset: 0x000C3433
		internal CollectionTypeDefinition(Node elementTypeDef)
		{
			this._elementTypeDef = elementTypeDef;
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x0600326B RID: 12907 RVA: 0x000C5242 File Offset: 0x000C3442
		internal Node ElementTypeDef
		{
			get
			{
				return this._elementTypeDef;
			}
		}

		// Token: 0x0400164B RID: 5707
		private readonly Node _elementTypeDef;
	}
}
