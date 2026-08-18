using System;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200024E RID: 590
	[DebuggerTypeProxy(typeof(Expression.MemberExpressionProxy))]
	[__DynamicallyInvokable]
	public class MemberExpression : Expression
	{
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x00048753 File Offset: 0x00046953
		[__DynamicallyInvokable]
		public MemberInfo Member
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetMember();
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x0004875B File Offset: 0x0004695B
		[__DynamicallyInvokable]
		public Expression Expression
		{
			[__DynamicallyInvokable]
			get
			{
				return this._expression;
			}
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00048763 File Offset: 0x00046963
		internal MemberExpression(Expression expression)
		{
			this._expression = expression;
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00048774 File Offset: 0x00046974
		internal static MemberExpression Make(Expression expression, MemberInfo member)
		{
			if (member.MemberType == MemberTypes.Field)
			{
				FieldInfo member2 = (FieldInfo)member;
				return new FieldExpression(expression, member2);
			}
			PropertyInfo member3 = (PropertyInfo)member;
			return new PropertyExpression(expression, member3);
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x000487A7 File Offset: 0x000469A7
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.MemberAccess;
			}
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x000487AB File Offset: 0x000469AB
		internal virtual MemberInfo GetMember()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x000487B2 File Offset: 0x000469B2
		[__DynamicallyInvokable]
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitMember(this);
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x000487BB File Offset: 0x000469BB
		[__DynamicallyInvokable]
		public MemberExpression Update(Expression expression)
		{
			if (expression == this.Expression)
			{
				return this;
			}
			return Expression.MakeMemberAccess(expression, this.Member);
		}

		// Token: 0x04000A24 RID: 2596
		private readonly Expression _expression;
	}
}
