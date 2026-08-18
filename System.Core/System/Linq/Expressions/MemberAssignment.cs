using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200024B RID: 587
	[__DynamicallyInvokable]
	public sealed class MemberAssignment : MemberBinding
	{
		// Token: 0x0600158A RID: 5514 RVA: 0x000486F3 File Offset: 0x000468F3
		internal MemberAssignment(MemberInfo member, Expression expression) : base(MemberBindingType.Assignment, member)
		{
			this._expression = expression;
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x00048704 File Offset: 0x00046904
		[__DynamicallyInvokable]
		public Expression Expression
		{
			[__DynamicallyInvokable]
			get
			{
				return this._expression;
			}
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0004870C File Offset: 0x0004690C
		[__DynamicallyInvokable]
		public MemberAssignment Update(Expression expression)
		{
			if (expression == this.Expression)
			{
				return this;
			}
			return Expression.Bind(base.Member, expression);
		}

		// Token: 0x04000A1D RID: 2589
		private Expression _expression;
	}
}
