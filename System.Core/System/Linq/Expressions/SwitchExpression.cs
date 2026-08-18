using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000269 RID: 617
	[DebuggerTypeProxy(typeof(Expression.SwitchExpressionProxy))]
	[__DynamicallyInvokable]
	public sealed class SwitchExpression : Expression
	{
		// Token: 0x0600161D RID: 5661 RVA: 0x00049411 File Offset: 0x00047611
		internal SwitchExpression(Type type, Expression switchValue, Expression defaultBody, MethodInfo comparison, ReadOnlyCollection<SwitchCase> cases)
		{
			this._type = type;
			this._switchValue = switchValue;
			this._defaultBody = defaultBody;
			this._comparison = comparison;
			this._cases = cases;
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x0004943E File Offset: 0x0004763E
		[__DynamicallyInvokable]
		public sealed override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this._type;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x00049446 File Offset: 0x00047646
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.Switch;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x0004944A File Offset: 0x0004764A
		[__DynamicallyInvokable]
		public Expression SwitchValue
		{
			[__DynamicallyInvokable]
			get
			{
				return this._switchValue;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x00049452 File Offset: 0x00047652
		[__DynamicallyInvokable]
		public ReadOnlyCollection<SwitchCase> Cases
		{
			[__DynamicallyInvokable]
			get
			{
				return this._cases;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x0004945A File Offset: 0x0004765A
		[__DynamicallyInvokable]
		public Expression DefaultBody
		{
			[__DynamicallyInvokable]
			get
			{
				return this._defaultBody;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x00049462 File Offset: 0x00047662
		[__DynamicallyInvokable]
		public MethodInfo Comparison
		{
			[__DynamicallyInvokable]
			get
			{
				return this._comparison;
			}
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0004946A File Offset: 0x0004766A
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitSwitch(this);
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x00049474 File Offset: 0x00047674
		internal bool IsLifted
		{
			get
			{
				return this._switchValue.Type.IsNullableType() && (this._comparison == null || !TypeUtils.AreEquivalent(this._switchValue.Type, this._comparison.GetParametersCached()[0].ParameterType.GetNonRefType()));
			}
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x000494CF File Offset: 0x000476CF
		[__DynamicallyInvokable]
		public SwitchExpression Update(Expression switchValue, IEnumerable<SwitchCase> cases, Expression defaultBody)
		{
			if (switchValue == this.SwitchValue && cases == this.Cases && defaultBody == this.DefaultBody)
			{
				return this;
			}
			return Expression.Switch(this.Type, switchValue, defaultBody, this.Comparison, cases);
		}

		// Token: 0x04000A50 RID: 2640
		private readonly Type _type;

		// Token: 0x04000A51 RID: 2641
		private readonly Expression _switchValue;

		// Token: 0x04000A52 RID: 2642
		private readonly ReadOnlyCollection<SwitchCase> _cases;

		// Token: 0x04000A53 RID: 2643
		private readonly Expression _defaultBody;

		// Token: 0x04000A54 RID: 2644
		private readonly MethodInfo _comparison;
	}
}
