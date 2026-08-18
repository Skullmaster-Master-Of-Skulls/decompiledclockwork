using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.EntitySql.AST;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200025C RID: 604
	internal abstract class InlineFunctionInfo
	{
		// Token: 0x060014E9 RID: 5353 RVA: 0x00062FED File Offset: 0x000611ED
		internal InlineFunctionInfo(FunctionDefinition functionDef, List<DbVariableReferenceExpression> parameters)
		{
			this.FunctionDefAst = functionDef;
			this.Parameters = parameters;
		}

		// Token: 0x060014EA RID: 5354
		internal abstract DbLambda GetLambda(SemanticResolver sr);

		// Token: 0x04000738 RID: 1848
		internal readonly FunctionDefinition FunctionDefAst;

		// Token: 0x04000739 RID: 1849
		internal readonly List<DbVariableReferenceExpression> Parameters;
	}
}
