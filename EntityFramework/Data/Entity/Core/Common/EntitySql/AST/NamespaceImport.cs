using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200022E RID: 558
	internal sealed class NamespaceImport : Node
	{
		// Token: 0x060013B7 RID: 5047 RVA: 0x0005137A File Offset: 0x0004F57A
		internal NamespaceImport(Identifier idenitifier)
		{
			this._namespaceName = idenitifier;
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00051389 File Offset: 0x0004F589
		internal NamespaceImport(DotExpr dorExpr)
		{
			this._namespaceName = dorExpr;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00051398 File Offset: 0x0004F598
		internal NamespaceImport(BuiltInExpr bltInExpr)
		{
			this._namespaceAlias = null;
			Identifier identifier = bltInExpr.Arg1 as Identifier;
			if (identifier == null)
			{
				ErrorContext errCtx = bltInExpr.Arg1.ErrCtx;
				string invalidNamespaceAlias = Strings.InvalidNamespaceAlias;
				throw EntitySqlException.Create(errCtx, invalidNamespaceAlias, null);
			}
			this._namespaceAlias = identifier;
			this._namespaceName = bltInExpr.Arg2;
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x000513EF File Offset: 0x0004F5EF
		internal Identifier Alias
		{
			get
			{
				return this._namespaceAlias;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x000513F7 File Offset: 0x0004F5F7
		internal Node NamespaceName
		{
			get
			{
				return this._namespaceName;
			}
		}

		// Token: 0x0400061D RID: 1565
		private readonly Identifier _namespaceAlias;

		// Token: 0x0400061E RID: 1566
		private readonly Node _namespaceName;
	}
}
