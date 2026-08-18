using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200021D RID: 541
	internal sealed class CreateRefExpr : Node
	{
		// Token: 0x0600136F RID: 4975 RVA: 0x00050525 File Offset: 0x0004E725
		internal CreateRefExpr(Node entitySet, Node keys) : this(entitySet, keys, null)
		{
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00050530 File Offset: 0x0004E730
		internal CreateRefExpr(Node entitySet, Node keys, Node typeIdentifier)
		{
			this._entitySet = entitySet;
			this._keys = keys;
			this._typeIdentifier = typeIdentifier;
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x0005054D File Offset: 0x0004E74D
		internal Node EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x00050555 File Offset: 0x0004E755
		internal Node Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x0005055D File Offset: 0x0004E75D
		internal Node TypeIdentifier
		{
			get
			{
				return this._typeIdentifier;
			}
		}

		// Token: 0x040005DC RID: 1500
		private readonly Node _entitySet;

		// Token: 0x040005DD RID: 1501
		private readonly Node _keys;

		// Token: 0x040005DE RID: 1502
		private readonly Node _typeIdentifier;
	}
}
