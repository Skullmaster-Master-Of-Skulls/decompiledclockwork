using System;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	// Token: 0x02000244 RID: 580
	[DebuggerTypeProxy(typeof(Expression.LabelExpressionProxy))]
	[__DynamicallyInvokable]
	public sealed class LabelExpression : Expression
	{
		// Token: 0x06001548 RID: 5448 RVA: 0x000482AE File Offset: 0x000464AE
		internal LabelExpression(LabelTarget label, Expression defaultValue)
		{
			this._target = label;
			this._defaultValue = defaultValue;
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x000482C4 File Offset: 0x000464C4
		[__DynamicallyInvokable]
		public sealed override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this._target.Type;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x000482D1 File Offset: 0x000464D1
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.Label;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x000482D5 File Offset: 0x000464D5
		[__DynamicallyInvokable]
		public LabelTarget Target
		{
			[__DynamicallyInvokable]
			get
			{
				return this._target;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x000482DD File Offset: 0x000464DD
		[__DynamicallyInvokable]
		public Expression DefaultValue
		{
			[__DynamicallyInvokable]
			get
			{
				return this._defaultValue;
			}
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x000482E5 File Offset: 0x000464E5
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitLabel(this);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x000482EE File Offset: 0x000464EE
		[__DynamicallyInvokable]
		public LabelExpression Update(LabelTarget target, Expression defaultValue)
		{
			if (target == this.Target && defaultValue == this.DefaultValue)
			{
				return this;
			}
			return Expression.Label(target, defaultValue);
		}

		// Token: 0x04000A0D RID: 2573
		private readonly Expression _defaultValue;

		// Token: 0x04000A0E RID: 2574
		private readonly LabelTarget _target;
	}
}
