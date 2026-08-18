using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x02000285 RID: 645
	internal sealed class SpilledExpressionBlock : BlockN
	{
		// Token: 0x060017E7 RID: 6119 RVA: 0x00056B04 File Offset: 0x00054D04
		internal SpilledExpressionBlock(IList<Expression> expressions) : base(expressions)
		{
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00056B0D File Offset: 0x00054D0D
		internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			throw ContractUtils.Unreachable;
		}
	}
}
