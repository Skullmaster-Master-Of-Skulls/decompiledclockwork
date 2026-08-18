using System;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	// Token: 0x0200024A RID: 586
	[DebuggerTypeProxy(typeof(Expression.LoopExpressionProxy))]
	[__DynamicallyInvokable]
	public sealed class LoopExpression : Expression
	{
		// Token: 0x06001582 RID: 5506 RVA: 0x0004866A File Offset: 0x0004686A
		internal LoopExpression(Expression body, LabelTarget @break, LabelTarget @continue)
		{
			this._body = body;
			this._break = @break;
			this._continue = @continue;
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x00048687 File Offset: 0x00046887
		[__DynamicallyInvokable]
		public sealed override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._break != null)
				{
					return this._break.Type;
				}
				return typeof(void);
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x000486A7 File Offset: 0x000468A7
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.Loop;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x000486AB File Offset: 0x000468AB
		[__DynamicallyInvokable]
		public Expression Body
		{
			[__DynamicallyInvokable]
			get
			{
				return this._body;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x000486B3 File Offset: 0x000468B3
		[__DynamicallyInvokable]
		public LabelTarget BreakLabel
		{
			[__DynamicallyInvokable]
			get
			{
				return this._break;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x000486BB File Offset: 0x000468BB
		[__DynamicallyInvokable]
		public LabelTarget ContinueLabel
		{
			[__DynamicallyInvokable]
			get
			{
				return this._continue;
			}
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x000486C3 File Offset: 0x000468C3
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitLoop(this);
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000486CC File Offset: 0x000468CC
		[__DynamicallyInvokable]
		public LoopExpression Update(LabelTarget breakLabel, LabelTarget continueLabel, Expression body)
		{
			if (breakLabel == this.BreakLabel && continueLabel == this.ContinueLabel && body == this.Body)
			{
				return this;
			}
			return Expression.Loop(body, breakLabel, continueLabel);
		}

		// Token: 0x04000A1A RID: 2586
		private readonly Expression _body;

		// Token: 0x04000A1B RID: 2587
		private readonly LabelTarget _break;

		// Token: 0x04000A1C RID: 2588
		private readonly LabelTarget _continue;
	}
}
