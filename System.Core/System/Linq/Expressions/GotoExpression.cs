using System;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	// Token: 0x0200023E RID: 574
	[DebuggerTypeProxy(typeof(Expression.GotoExpressionProxy))]
	[__DynamicallyInvokable]
	public sealed class GotoExpression : Expression
	{
		// Token: 0x06001524 RID: 5412 RVA: 0x00048041 File Offset: 0x00046241
		internal GotoExpression(GotoExpressionKind kind, LabelTarget target, Expression value, Type type)
		{
			this._kind = kind;
			this._value = value;
			this._target = target;
			this._type = type;
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00048066 File Offset: 0x00046266
		[__DynamicallyInvokable]
		public sealed override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x0004806E File Offset: 0x0004626E
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.Goto;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x00048072 File Offset: 0x00046272
		[__DynamicallyInvokable]
		public Expression Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x0004807A File Offset: 0x0004627A
		[__DynamicallyInvokable]
		public LabelTarget Target
		{
			[__DynamicallyInvokable]
			get
			{
				return this._target;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x00048082 File Offset: 0x00046282
		[__DynamicallyInvokable]
		public GotoExpressionKind Kind
		{
			[__DynamicallyInvokable]
			get
			{
				return this._kind;
			}
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0004808A File Offset: 0x0004628A
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitGoto(this);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x00048093 File Offset: 0x00046293
		[__DynamicallyInvokable]
		public GotoExpression Update(LabelTarget target, Expression value)
		{
			if (target == this.Target && value == this.Value)
			{
				return this;
			}
			return Expression.MakeGoto(this.Kind, target, value, this.Type);
		}

		// Token: 0x04000A03 RID: 2563
		private readonly GotoExpressionKind _kind;

		// Token: 0x04000A04 RID: 2564
		private readonly Expression _value;

		// Token: 0x04000A05 RID: 2565
		private readonly LabelTarget _target;

		// Token: 0x04000A06 RID: 2566
		private readonly Type _type;
	}
}
