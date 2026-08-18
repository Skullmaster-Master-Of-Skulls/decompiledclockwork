using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200022D RID: 557
	[DebuggerTypeProxy(typeof(Expression.DynamicExpressionProxy))]
	[__DynamicallyInvokable]
	public class DynamicExpression : Expression, IDynamicExpression, IArgumentProvider
	{
		// Token: 0x0600147A RID: 5242 RVA: 0x00045D58 File Offset: 0x00043F58
		internal DynamicExpression(Type delegateType, CallSiteBinder binder)
		{
			this._delegateType = delegateType;
			this._binder = binder;
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00045D6E File Offset: 0x00043F6E
		internal static DynamicExpression Make(Type returnType, Type delegateType, CallSiteBinder binder, ReadOnlyCollection<Expression> arguments)
		{
			if (returnType == typeof(object))
			{
				return new DynamicExpressionN(delegateType, binder, arguments);
			}
			return new TypedDynamicExpressionN(returnType, delegateType, binder, arguments);
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00045D94 File Offset: 0x00043F94
		internal static DynamicExpression Make(Type returnType, Type delegateType, CallSiteBinder binder, Expression arg0)
		{
			if (returnType == typeof(object))
			{
				return new DynamicExpression1(delegateType, binder, arg0);
			}
			return new TypedDynamicExpression1(returnType, delegateType, binder, arg0);
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x00045DBA File Offset: 0x00043FBA
		internal static DynamicExpression Make(Type returnType, Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1)
		{
			if (returnType == typeof(object))
			{
				return new DynamicExpression2(delegateType, binder, arg0, arg1);
			}
			return new TypedDynamicExpression2(returnType, delegateType, binder, arg0, arg1);
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00045DE4 File Offset: 0x00043FE4
		internal static DynamicExpression Make(Type returnType, Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2)
		{
			if (returnType == typeof(object))
			{
				return new DynamicExpression3(delegateType, binder, arg0, arg1, arg2);
			}
			return new TypedDynamicExpression3(returnType, delegateType, binder, arg0, arg1, arg2);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00045E12 File Offset: 0x00044012
		internal static DynamicExpression Make(Type returnType, Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			if (returnType == typeof(object))
			{
				return new DynamicExpression4(delegateType, binder, arg0, arg1, arg2, arg3);
			}
			return new TypedDynamicExpression4(returnType, delegateType, binder, arg0, arg1, arg2, arg3);
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x00045E44 File Offset: 0x00044044
		[__DynamicallyInvokable]
		public override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x00045E50 File Offset: 0x00044050
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.Dynamic;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x00045E54 File Offset: 0x00044054
		[__DynamicallyInvokable]
		public CallSiteBinder Binder
		{
			[__DynamicallyInvokable]
			get
			{
				return this._binder;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00045E5C File Offset: 0x0004405C
		[__DynamicallyInvokable]
		public Type DelegateType
		{
			[__DynamicallyInvokable]
			get
			{
				return this._delegateType;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x00045E64 File Offset: 0x00044064
		[__DynamicallyInvokable]
		public ReadOnlyCollection<Expression> Arguments
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetOrMakeArguments();
			}
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00045E6C File Offset: 0x0004406C
		internal virtual ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00045E73 File Offset: 0x00044073
		[__DynamicallyInvokable]
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitDynamic(this);
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00045E7C File Offset: 0x0004407C
		internal virtual DynamicExpression Rewrite(Expression[] args)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00045E83 File Offset: 0x00044083
		[__DynamicallyInvokable]
		public DynamicExpression Update(IEnumerable<Expression> arguments)
		{
			if (arguments == this.Arguments)
			{
				return this;
			}
			return Expression.MakeDynamic(this.DelegateType, this.Binder, arguments);
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00045EA2 File Offset: 0x000440A2
		[__DynamicallyInvokable]
		Expression IArgumentProvider.GetArgument(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x00045EA9 File Offset: 0x000440A9
		[__DynamicallyInvokable]
		int IArgumentProvider.ArgumentCount
		{
			[__DynamicallyInvokable]
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00045EB0 File Offset: 0x000440B0
		[__DynamicallyInvokable]
		public new static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, params Expression[] arguments)
		{
			return Expression.Dynamic(binder, returnType, arguments);
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x00045EBA File Offset: 0x000440BA
		[__DynamicallyInvokable]
		public new static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, IEnumerable<Expression> arguments)
		{
			return Expression.Dynamic(binder, returnType, arguments);
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x00045EC4 File Offset: 0x000440C4
		[__DynamicallyInvokable]
		public new static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0)
		{
			return Expression.Dynamic(binder, returnType, arg0);
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00045ECE File Offset: 0x000440CE
		[__DynamicallyInvokable]
		public new static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1)
		{
			return Expression.Dynamic(binder, returnType, arg0, arg1);
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x00045ED9 File Offset: 0x000440D9
		[__DynamicallyInvokable]
		public new static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1, Expression arg2)
		{
			return Expression.Dynamic(binder, returnType, arg0, arg1, arg2);
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00045EE6 File Offset: 0x000440E6
		[__DynamicallyInvokable]
		public new static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			return Expression.Dynamic(binder, returnType, arg0, arg1, arg2, arg3);
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00045EF5 File Offset: 0x000440F5
		[__DynamicallyInvokable]
		public new static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, IEnumerable<Expression> arguments)
		{
			return Expression.MakeDynamic(delegateType, binder, arguments);
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00045EFF File Offset: 0x000440FF
		[__DynamicallyInvokable]
		public new static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, params Expression[] arguments)
		{
			return Expression.MakeDynamic(delegateType, binder, arguments);
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00045F09 File Offset: 0x00044109
		[__DynamicallyInvokable]
		public new static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0)
		{
			return Expression.MakeDynamic(delegateType, binder, arg0);
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x00045F13 File Offset: 0x00044113
		[__DynamicallyInvokable]
		public new static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1)
		{
			return Expression.MakeDynamic(delegateType, binder, arg0, arg1);
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x00045F1E File Offset: 0x0004411E
		[__DynamicallyInvokable]
		public new static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2)
		{
			return Expression.MakeDynamic(delegateType, binder, arg0, arg1, arg2);
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x00045F2B File Offset: 0x0004412B
		[__DynamicallyInvokable]
		public new static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			return Expression.MakeDynamic(delegateType, binder, arg0, arg1, arg2, arg3);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x00045F3A File Offset: 0x0004413A
		[__DynamicallyInvokable]
		Expression IDynamicExpression.Rewrite(Expression[] args)
		{
			return this.Rewrite(args);
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x00045F43 File Offset: 0x00044143
		[__DynamicallyInvokable]
		object IDynamicExpression.CreateCallSite()
		{
			return CallSite.Create(this.DelegateType, this.Binder);
		}

		// Token: 0x04000992 RID: 2450
		private readonly CallSiteBinder _binder;

		// Token: 0x04000993 RID: 2451
		private readonly Type _delegateType;
	}
}
