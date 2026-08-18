using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000251 RID: 593
	[DebuggerTypeProxy(typeof(Expression.MemberInitExpressionProxy))]
	[__DynamicallyInvokable]
	public sealed class MemberInitExpression : Expression
	{
		// Token: 0x0600159F RID: 5535 RVA: 0x0004881E File Offset: 0x00046A1E
		internal MemberInitExpression(NewExpression newExpression, ReadOnlyCollection<MemberBinding> bindings)
		{
			this._newExpression = newExpression;
			this._bindings = bindings;
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x00048834 File Offset: 0x00046A34
		[__DynamicallyInvokable]
		public sealed override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this._newExpression.Type;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x00048841 File Offset: 0x00046A41
		[__DynamicallyInvokable]
		public override bool CanReduce
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x00048844 File Offset: 0x00046A44
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.MemberInit;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x00048848 File Offset: 0x00046A48
		[__DynamicallyInvokable]
		public NewExpression NewExpression
		{
			[__DynamicallyInvokable]
			get
			{
				return this._newExpression;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x00048850 File Offset: 0x00046A50
		[__DynamicallyInvokable]
		public ReadOnlyCollection<MemberBinding> Bindings
		{
			[__DynamicallyInvokable]
			get
			{
				return this._bindings;
			}
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00048858 File Offset: 0x00046A58
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitMemberInit(this);
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00048861 File Offset: 0x00046A61
		[__DynamicallyInvokable]
		public override Expression Reduce()
		{
			return MemberInitExpression.ReduceMemberInit(this._newExpression, this._bindings, true);
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00048878 File Offset: 0x00046A78
		internal static Expression ReduceMemberInit(Expression objExpression, ReadOnlyCollection<MemberBinding> bindings, bool keepOnStack)
		{
			ParameterExpression parameterExpression = Expression.Variable(objExpression.Type, null);
			int count = bindings.Count;
			Expression[] array = new Expression[count + 2];
			array[0] = Expression.Assign(parameterExpression, objExpression);
			for (int i = 0; i < count; i++)
			{
				array[i + 1] = MemberInitExpression.ReduceMemberBinding(parameterExpression, bindings[i]);
			}
			array[count + 1] = (keepOnStack ? parameterExpression : Expression.Empty());
			return Expression.Block(new TrueReadOnlyCollection<Expression>(array));
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x000488E8 File Offset: 0x00046AE8
		internal static Expression ReduceListInit(Expression listExpression, ReadOnlyCollection<ElementInit> initializers, bool keepOnStack)
		{
			ParameterExpression parameterExpression = Expression.Variable(listExpression.Type, null);
			int count = initializers.Count;
			Expression[] array = new Expression[count + 2];
			array[0] = Expression.Assign(parameterExpression, listExpression);
			for (int i = 0; i < count; i++)
			{
				ElementInit elementInit = initializers[i];
				array[i + 1] = Expression.Call(parameterExpression, elementInit.AddMethod, elementInit.Arguments);
			}
			array[count + 1] = (keepOnStack ? parameterExpression : Expression.Empty());
			return Expression.Block(new TrueReadOnlyCollection<Expression>(array));
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x00048968 File Offset: 0x00046B68
		internal static Expression ReduceMemberBinding(ParameterExpression objVar, MemberBinding binding)
		{
			MemberExpression memberExpression = Expression.MakeMemberAccess(objVar, binding.Member);
			switch (binding.BindingType)
			{
			case MemberBindingType.Assignment:
				return Expression.Assign(memberExpression, ((MemberAssignment)binding).Expression);
			case MemberBindingType.MemberBinding:
				return MemberInitExpression.ReduceMemberInit(memberExpression, ((MemberMemberBinding)binding).Bindings, false);
			case MemberBindingType.ListBinding:
				return MemberInitExpression.ReduceListInit(memberExpression, ((MemberListBinding)binding).Initializers, false);
			default:
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x000489DA File Offset: 0x00046BDA
		[__DynamicallyInvokable]
		public MemberInitExpression Update(NewExpression newExpression, IEnumerable<MemberBinding> bindings)
		{
			if (newExpression == this.NewExpression && bindings == this.Bindings)
			{
				return this;
			}
			return Expression.MemberInit(newExpression, bindings);
		}

		// Token: 0x04000A27 RID: 2599
		private readonly NewExpression _newExpression;

		// Token: 0x04000A28 RID: 2600
		private readonly ReadOnlyCollection<MemberBinding> _bindings;
	}
}
