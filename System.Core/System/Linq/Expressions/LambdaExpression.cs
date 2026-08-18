using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Linq.Expressions.Compiler;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000246 RID: 582
	[DebuggerTypeProxy(typeof(Expression.LambdaExpressionProxy))]
	[__DynamicallyInvokable]
	public abstract class LambdaExpression : Expression
	{
		// Token: 0x06001553 RID: 5459 RVA: 0x0004834C File Offset: 0x0004654C
		internal LambdaExpression(Type delegateType, string name, Expression body, bool tailCall, ReadOnlyCollection<ParameterExpression> parameters)
		{
			this._name = name;
			this._body = body;
			this._parameters = parameters;
			this._delegateType = delegateType;
			this._tailCall = tailCall;
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x00048379 File Offset: 0x00046579
		[__DynamicallyInvokable]
		public sealed override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this._delegateType;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x00048381 File Offset: 0x00046581
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.Lambda;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x00048385 File Offset: 0x00046585
		[__DynamicallyInvokable]
		public ReadOnlyCollection<ParameterExpression> Parameters
		{
			[__DynamicallyInvokable]
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x0004838D File Offset: 0x0004658D
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this._name;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x00048395 File Offset: 0x00046595
		[__DynamicallyInvokable]
		public Expression Body
		{
			[__DynamicallyInvokable]
			get
			{
				return this._body;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x0004839D File Offset: 0x0004659D
		[__DynamicallyInvokable]
		public Type ReturnType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Type.GetMethod("Invoke").ReturnType;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x000483B4 File Offset: 0x000465B4
		[__DynamicallyInvokable]
		public bool TailCall
		{
			[__DynamicallyInvokable]
			get
			{
				return this._tailCall;
			}
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x000483BC File Offset: 0x000465BC
		[__DynamicallyInvokable]
		public Delegate Compile()
		{
			return LambdaCompiler.Compile(this, null);
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x000483C5 File Offset: 0x000465C5
		public Delegate Compile(DebugInfoGenerator debugInfoGenerator)
		{
			ContractUtils.RequiresNotNull(debugInfoGenerator, "debugInfoGenerator");
			return LambdaCompiler.Compile(this, debugInfoGenerator);
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x000483D9 File Offset: 0x000465D9
		public Delegate Compile(bool preferInterpretation)
		{
			return this.Compile();
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x000483E1 File Offset: 0x000465E1
		public void CompileToMethod(MethodBuilder method)
		{
			this.CompileToMethodInternal(method, null);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x000483EB File Offset: 0x000465EB
		public void CompileToMethod(MethodBuilder method, DebugInfoGenerator debugInfoGenerator)
		{
			ContractUtils.RequiresNotNull(debugInfoGenerator, "debugInfoGenerator");
			this.CompileToMethodInternal(method, debugInfoGenerator);
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x00048400 File Offset: 0x00046600
		private void CompileToMethodInternal(MethodBuilder method, DebugInfoGenerator debugInfoGenerator)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.Requires(method.IsStatic, "method");
			TypeBuilder left = method.DeclaringType as TypeBuilder;
			if (left == null)
			{
				throw Error.MethodBuilderDoesNotHaveTypeBuilder();
			}
			LambdaCompiler.Compile(this, method, debugInfoGenerator);
		}

		// Token: 0x06001561 RID: 5473
		internal abstract LambdaExpression Accept(StackSpiller spiller);

		// Token: 0x04000A11 RID: 2577
		private readonly string _name;

		// Token: 0x04000A12 RID: 2578
		private readonly Expression _body;

		// Token: 0x04000A13 RID: 2579
		private readonly ReadOnlyCollection<ParameterExpression> _parameters;

		// Token: 0x04000A14 RID: 2580
		private readonly Type _delegateType;

		// Token: 0x04000A15 RID: 2581
		private readonly bool _tailCall;
	}
}
