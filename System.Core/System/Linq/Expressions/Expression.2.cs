using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Linq.Expressions.Compiler;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000247 RID: 583
	[__DynamicallyInvokable]
	public sealed class Expression<TDelegate> : LambdaExpression
	{
		// Token: 0x06001562 RID: 5474 RVA: 0x0004844B File Offset: 0x0004664B
		internal Expression(Expression body, string name, bool tailCall, ReadOnlyCollection<ParameterExpression> parameters) : base(typeof(TDelegate), name, body, tailCall, parameters)
		{
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00048462 File Offset: 0x00046662
		[__DynamicallyInvokable]
		public new TDelegate Compile()
		{
			return (TDelegate)((object)LambdaCompiler.Compile(this, null));
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00048470 File Offset: 0x00046670
		public new TDelegate Compile(DebugInfoGenerator debugInfoGenerator)
		{
			ContractUtils.RequiresNotNull(debugInfoGenerator, "debugInfoGenerator");
			return (TDelegate)((object)LambdaCompiler.Compile(this, debugInfoGenerator));
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00048489 File Offset: 0x00046689
		public new TDelegate Compile(bool preferInterpretation)
		{
			return this.Compile();
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00048491 File Offset: 0x00046691
		[__DynamicallyInvokable]
		public Expression<TDelegate> Update(Expression body, IEnumerable<ParameterExpression> parameters)
		{
			if (body == base.Body && parameters == base.Parameters)
			{
				return this;
			}
			return Expression.Lambda<TDelegate>(body, base.Name, base.TailCall, parameters);
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x000484BA File Offset: 0x000466BA
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitLambda<TDelegate>(this);
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x000484C3 File Offset: 0x000466C3
		internal override LambdaExpression Accept(StackSpiller spiller)
		{
			return spiller.Rewrite<TDelegate>(this);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x000484CC File Offset: 0x000466CC
		internal static LambdaExpression Create(Expression body, string name, bool tailCall, ReadOnlyCollection<ParameterExpression> parameters)
		{
			return new Expression<TDelegate>(body, name, tailCall, parameters);
		}
	}
}
