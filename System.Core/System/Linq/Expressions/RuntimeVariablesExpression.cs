using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000267 RID: 615
	[DebuggerTypeProxy(typeof(Expression.RuntimeVariablesExpressionProxy))]
	[__DynamicallyInvokable]
	public sealed class RuntimeVariablesExpression : Expression
	{
		// Token: 0x06001612 RID: 5650 RVA: 0x00049383 File Offset: 0x00047583
		internal RuntimeVariablesExpression(ReadOnlyCollection<ParameterExpression> variables)
		{
			this._variables = variables;
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x00049392 File Offset: 0x00047592
		[__DynamicallyInvokable]
		public sealed override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(IRuntimeVariables);
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x0004939E File Offset: 0x0004759E
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.RuntimeVariables;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x000493A2 File Offset: 0x000475A2
		[__DynamicallyInvokable]
		public ReadOnlyCollection<ParameterExpression> Variables
		{
			[__DynamicallyInvokable]
			get
			{
				return this._variables;
			}
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x000493AA File Offset: 0x000475AA
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitRuntimeVariables(this);
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x000493B3 File Offset: 0x000475B3
		[__DynamicallyInvokable]
		public RuntimeVariablesExpression Update(IEnumerable<ParameterExpression> variables)
		{
			if (variables == this.Variables)
			{
				return this;
			}
			return Expression.RuntimeVariables(variables);
		}

		// Token: 0x04000A4D RID: 2637
		private readonly ReadOnlyCollection<ParameterExpression> _variables;
	}
}
