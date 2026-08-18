using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000384 RID: 900
	internal sealed class QueryStatement : Statement
	{
		// Token: 0x06003263 RID: 12899 RVA: 0x000C51DF File Offset: 0x000C33DF
		internal QueryStatement(NodeList<FunctionDefinition> functionDefList, Node expr)
		{
			this._functionDefList = functionDefList;
			this._expr = expr;
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x000C51F5 File Offset: 0x000C33F5
		internal NodeList<FunctionDefinition> FunctionDefList
		{
			get
			{
				return this._functionDefList;
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x000C51FD File Offset: 0x000C33FD
		internal Node Expr
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x04001647 RID: 5703
		private readonly NodeList<FunctionDefinition> _functionDefList;

		// Token: 0x04001648 RID: 5704
		private readonly Node _expr;
	}
}
