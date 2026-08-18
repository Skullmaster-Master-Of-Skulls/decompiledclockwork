using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000215 RID: 533
	internal class MethodBinaryExpression : SimpleBinaryExpression
	{
		// Token: 0x06001219 RID: 4633 RVA: 0x0003C850 File Offset: 0x0003AA50
		internal MethodBinaryExpression(ExpressionType nodeType, Expression left, Expression right, Type type, MethodInfo method) : base(nodeType, left, right, type)
		{
			this._method = method;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0003C865 File Offset: 0x0003AA65
		internal override MethodInfo GetMethod()
		{
			return this._method;
		}

		// Token: 0x0400095E RID: 2398
		private readonly MethodInfo _method;
	}
}
