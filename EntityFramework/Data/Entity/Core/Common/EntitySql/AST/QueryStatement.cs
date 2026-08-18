using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000237 RID: 567
	internal sealed class QueryStatement : Statement
	{
		// Token: 0x060013D8 RID: 5080 RVA: 0x000515F3 File Offset: 0x0004F7F3
		internal QueryStatement(NodeList<FunctionDefinition> functionDefList, Node expr)
		{
			this._functionDefList = functionDefList;
			this._expr = expr;
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00051609 File Offset: 0x0004F809
		internal NodeList<FunctionDefinition> FunctionDefList
		{
			get
			{
				return this._functionDefList;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x00051611 File Offset: 0x0004F811
		internal Node Expr
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x04000636 RID: 1590
		private readonly NodeList<FunctionDefinition> _functionDefList;

		// Token: 0x04000637 RID: 1591
		private readonly Node _expr;
	}
}
