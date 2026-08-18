using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000213 RID: 531
	internal sealed class OpAssignMethodConversionBinaryExpression : MethodBinaryExpression
	{
		// Token: 0x06001214 RID: 4628 RVA: 0x0003C808 File Offset: 0x0003AA08
		internal OpAssignMethodConversionBinaryExpression(ExpressionType nodeType, Expression left, Expression right, Type type, MethodInfo method, LambdaExpression conversion) : base(nodeType, left, right, type, method)
		{
			this._conversion = conversion;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0003C81F File Offset: 0x0003AA1F
		internal override LambdaExpression GetConversion()
		{
			return this._conversion;
		}

		// Token: 0x0400095B RID: 2395
		private readonly LambdaExpression _conversion;
	}
}
