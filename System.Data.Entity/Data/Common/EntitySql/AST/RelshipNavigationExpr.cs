using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000373 RID: 883
	internal sealed class RelshipNavigationExpr : Node
	{
		// Token: 0x0600322F RID: 12847 RVA: 0x000C4EE2 File Offset: 0x000C30E2
		internal RelshipNavigationExpr(Node refExpr, Node relshipTypeName, Identifier toEndIdentifier, Identifier fromEndIdentifier)
		{
			this._refExpr = refExpr;
			this._relshipTypeName = relshipTypeName;
			this._toEndIdentifier = toEndIdentifier;
			this._fromEndIdentifier = fromEndIdentifier;
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06003230 RID: 12848 RVA: 0x000C4F07 File Offset: 0x000C3107
		internal Node RefExpr
		{
			get
			{
				return this._refExpr;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06003231 RID: 12849 RVA: 0x000C4F0F File Offset: 0x000C310F
		internal Node TypeName
		{
			get
			{
				return this._relshipTypeName;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x000C4F17 File Offset: 0x000C3117
		internal Identifier ToEndIdentifier
		{
			get
			{
				return this._toEndIdentifier;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x000C4F1F File Offset: 0x000C311F
		internal Identifier FromEndIdentifier
		{
			get
			{
				return this._fromEndIdentifier;
			}
		}

		// Token: 0x0400160C RID: 5644
		private readonly Node _refExpr;

		// Token: 0x0400160D RID: 5645
		private readonly Node _relshipTypeName;

		// Token: 0x0400160E RID: 5646
		private readonly Identifier _toEndIdentifier;

		// Token: 0x0400160F RID: 5647
		private readonly Identifier _fromEndIdentifier;
	}
}
