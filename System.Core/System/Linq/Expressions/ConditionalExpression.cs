using System;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	// Token: 0x02000223 RID: 547
	[DebuggerTypeProxy(typeof(Expression.ConditionalExpressionProxy))]
	[__DynamicallyInvokable]
	public class ConditionalExpression : Expression
	{
		// Token: 0x060013FC RID: 5116 RVA: 0x00043E64 File Offset: 0x00042064
		internal ConditionalExpression(Expression test, Expression ifTrue)
		{
			this._test = test;
			this._true = ifTrue;
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00043E7C File Offset: 0x0004207C
		internal static ConditionalExpression Make(Expression test, Expression ifTrue, Expression ifFalse, Type type)
		{
			if (ifTrue.Type != type || ifFalse.Type != type)
			{
				return new FullConditionalExpressionWithType(test, ifTrue, ifFalse, type);
			}
			if (ifFalse is DefaultExpression && ifFalse.Type == typeof(void))
			{
				return new ConditionalExpression(test, ifTrue);
			}
			return new FullConditionalExpression(test, ifTrue, ifFalse);
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x00043EDE File Offset: 0x000420DE
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.Conditional;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x00043EE1 File Offset: 0x000420E1
		[__DynamicallyInvokable]
		public override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this.IfTrue.Type;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x00043EEE File Offset: 0x000420EE
		[__DynamicallyInvokable]
		public Expression Test
		{
			[__DynamicallyInvokable]
			get
			{
				return this._test;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x00043EF6 File Offset: 0x000420F6
		[__DynamicallyInvokable]
		public Expression IfTrue
		{
			[__DynamicallyInvokable]
			get
			{
				return this._true;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x00043EFE File Offset: 0x000420FE
		[__DynamicallyInvokable]
		public Expression IfFalse
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetFalse();
			}
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00043F06 File Offset: 0x00042106
		internal virtual Expression GetFalse()
		{
			return Expression.Empty();
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x00043F0D File Offset: 0x0004210D
		[__DynamicallyInvokable]
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitConditional(this);
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x00043F16 File Offset: 0x00042116
		[__DynamicallyInvokable]
		public ConditionalExpression Update(Expression test, Expression ifTrue, Expression ifFalse)
		{
			if (test == this.Test && ifTrue == this.IfTrue && ifFalse == this.IfFalse)
			{
				return this;
			}
			return Expression.Condition(test, ifTrue, ifFalse, this.Type);
		}

		// Token: 0x0400097B RID: 2427
		private readonly Expression _test;

		// Token: 0x0400097C RID: 2428
		private readonly Expression _true;
	}
}
