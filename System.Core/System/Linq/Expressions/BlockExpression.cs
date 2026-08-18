using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Threading;

namespace System.Linq.Expressions
{
	// Token: 0x02000217 RID: 535
	[DebuggerTypeProxy(typeof(Expression.BlockExpressionProxy))]
	[__DynamicallyInvokable]
	public class BlockExpression : Expression
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x000438A4 File Offset: 0x00041AA4
		[__DynamicallyInvokable]
		public ReadOnlyCollection<Expression> Expressions
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetOrMakeExpressions();
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x000438AC File Offset: 0x00041AAC
		[__DynamicallyInvokable]
		public ReadOnlyCollection<ParameterExpression> Variables
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetOrMakeVariables();
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x000438B4 File Offset: 0x00041AB4
		[__DynamicallyInvokable]
		public Expression Result
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetExpression(this.ExpressionCount - 1);
			}
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x000438C4 File Offset: 0x00041AC4
		internal BlockExpression()
		{
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x000438CC File Offset: 0x00041ACC
		[__DynamicallyInvokable]
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitBlock(this);
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x000438D5 File Offset: 0x00041AD5
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.Block;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x000438D9 File Offset: 0x00041AD9
		[__DynamicallyInvokable]
		public override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetExpression(this.ExpressionCount - 1).Type;
			}
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x000438EE File Offset: 0x00041AEE
		[__DynamicallyInvokable]
		public BlockExpression Update(IEnumerable<ParameterExpression> variables, IEnumerable<Expression> expressions)
		{
			if (variables == this.Variables && expressions == this.Expressions)
			{
				return this;
			}
			return Expression.Block(this.Type, variables, expressions);
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00043911 File Offset: 0x00041B11
		internal virtual Expression GetExpression(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00043918 File Offset: 0x00041B18
		internal virtual int ExpressionCount
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0004391F File Offset: 0x00041B1F
		internal virtual ReadOnlyCollection<Expression> GetOrMakeExpressions()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00043926 File Offset: 0x00041B26
		internal virtual ParameterExpression GetVariable(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x0004392D File Offset: 0x00041B2D
		internal virtual int VariableCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00043930 File Offset: 0x00041B30
		internal virtual ReadOnlyCollection<ParameterExpression> GetOrMakeVariables()
		{
			return EmptyReadOnlyCollection<ParameterExpression>.Instance;
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00043937 File Offset: 0x00041B37
		internal virtual BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00043940 File Offset: 0x00041B40
		internal static ReadOnlyCollection<Expression> ReturnReadOnlyExpressions(BlockExpression provider, ref object collection)
		{
			Expression expression = collection as Expression;
			if (expression != null)
			{
				Interlocked.CompareExchange(ref collection, new ReadOnlyCollection<Expression>(new BlockExpressionList(provider, expression)), expression);
			}
			return (ReadOnlyCollection<Expression>)collection;
		}
	}
}
