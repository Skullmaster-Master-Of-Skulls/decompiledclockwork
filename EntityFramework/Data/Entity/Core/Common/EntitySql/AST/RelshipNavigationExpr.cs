using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200022F RID: 559
	internal sealed class RelshipNavigationExpr : Node
	{
		// Token: 0x060013BC RID: 5052 RVA: 0x000513FF File Offset: 0x0004F5FF
		internal RelshipNavigationExpr(Node refExpr, Node relshipTypeName, Identifier toEndIdentifier, Identifier fromEndIdentifier)
		{
			this._refExpr = refExpr;
			this._relshipTypeName = relshipTypeName;
			this._toEndIdentifier = toEndIdentifier;
			this._fromEndIdentifier = fromEndIdentifier;
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x00051424 File Offset: 0x0004F624
		internal Node RefExpr
		{
			get
			{
				return this._refExpr;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0005142C File Offset: 0x0004F62C
		internal Node TypeName
		{
			get
			{
				return this._relshipTypeName;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x00051434 File Offset: 0x0004F634
		internal Identifier ToEndIdentifier
		{
			get
			{
				return this._toEndIdentifier;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x0005143C File Offset: 0x0004F63C
		internal Identifier FromEndIdentifier
		{
			get
			{
				return this._fromEndIdentifier;
			}
		}

		// Token: 0x0400061F RID: 1567
		private readonly Node _refExpr;

		// Token: 0x04000620 RID: 1568
		private readonly Node _relshipTypeName;

		// Token: 0x04000621 RID: 1569
		private readonly Identifier _toEndIdentifier;

		// Token: 0x04000622 RID: 1570
		private readonly Identifier _fromEndIdentifier;
	}
}
