using System;
using System.Data.Entity;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000372 RID: 882
	internal sealed class NamespaceImport : Node
	{
		// Token: 0x0600322A RID: 12842 RVA: 0x000C4E6F File Offset: 0x000C306F
		internal NamespaceImport(Identifier idenitifier)
		{
			this._namespaceName = idenitifier;
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000C4E6F File Offset: 0x000C306F
		internal NamespaceImport(DotExpr dorExpr)
		{
			this._namespaceName = dorExpr;
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000C4E80 File Offset: 0x000C3080
		internal NamespaceImport(BuiltInExpr bltInExpr)
		{
			this._namespaceAlias = null;
			Identifier identifier = bltInExpr.Arg1 as Identifier;
			if (identifier == null)
			{
				throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.InvalidNamespaceAlias);
			}
			this._namespaceAlias = identifier;
			this._namespaceName = bltInExpr.Arg2;
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600322D RID: 12845 RVA: 0x000C4ED2 File Offset: 0x000C30D2
		internal Identifier Alias
		{
			get
			{
				return this._namespaceAlias;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x0600322E RID: 12846 RVA: 0x000C4EDA File Offset: 0x000C30DA
		internal Node NamespaceName
		{
			get
			{
				return this._namespaceName;
			}
		}

		// Token: 0x0400160A RID: 5642
		private readonly Identifier _namespaceAlias;

		// Token: 0x0400160B RID: 5643
		private readonly Node _namespaceName;
	}
}
