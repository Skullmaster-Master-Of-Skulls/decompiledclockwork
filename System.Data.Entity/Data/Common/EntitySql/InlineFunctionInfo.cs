using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.EntitySql.AST;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000340 RID: 832
	internal abstract class InlineFunctionInfo
	{
		// Token: 0x06003149 RID: 12617 RVA: 0x000C26E3 File Offset: 0x000C08E3
		internal InlineFunctionInfo(FunctionDefinition functionDef, List<DbVariableReferenceExpression> parameters)
		{
			this.FunctionDefAst = functionDef;
			this.Parameters = parameters;
		}

		// Token: 0x0600314A RID: 12618
		internal abstract DbLambda GetLambda(SemanticResolver sr);

		// Token: 0x04001567 RID: 5479
		internal readonly FunctionDefinition FunctionDefAst;

		// Token: 0x04001568 RID: 5480
		internal readonly List<DbVariableReferenceExpression> Parameters;
	}
}
