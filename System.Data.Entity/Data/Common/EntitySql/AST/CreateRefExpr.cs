using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x0200036C RID: 876
	internal sealed class CreateRefExpr : Node
	{
		// Token: 0x06003210 RID: 12816 RVA: 0x000C4BEE File Offset: 0x000C2DEE
		internal CreateRefExpr(Node entitySet, Node keys) : this(entitySet, keys, null)
		{
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x000C4BF9 File Offset: 0x000C2DF9
		internal CreateRefExpr(Node entitySet, Node keys, Node typeIdentifier)
		{
			this._entitySet = entitySet;
			this._keys = keys;
			this._typeIdentifier = typeIdentifier;
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06003212 RID: 12818 RVA: 0x000C4C16 File Offset: 0x000C2E16
		internal Node EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06003213 RID: 12819 RVA: 0x000C4C1E File Offset: 0x000C2E1E
		internal Node Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06003214 RID: 12820 RVA: 0x000C4C26 File Offset: 0x000C2E26
		internal Node TypeIdentifier
		{
			get
			{
				return this._typeIdentifier;
			}
		}

		// Token: 0x040015F8 RID: 5624
		private readonly Node _entitySet;

		// Token: 0x040015F9 RID: 5625
		private readonly Node _keys;

		// Token: 0x040015FA RID: 5626
		private readonly Node _typeIdentifier;
	}
}
