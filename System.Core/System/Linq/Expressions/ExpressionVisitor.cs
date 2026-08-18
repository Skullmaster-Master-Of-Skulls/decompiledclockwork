using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200023B RID: 571
	[__DynamicallyInvokable]
	public abstract class ExpressionVisitor
	{
		// Token: 0x060014F5 RID: 5365 RVA: 0x0004775A File Offset: 0x0004595A
		[__DynamicallyInvokable]
		protected ExpressionVisitor()
		{
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x00047762 File Offset: 0x00045962
		[__DynamicallyInvokable]
		public virtual Expression Visit(Expression node)
		{
			if (node != null)
			{
				return node.Accept(this);
			}
			return null;
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00047770 File Offset: 0x00045970
		[__DynamicallyInvokable]
		public ReadOnlyCollection<Expression> Visit(ReadOnlyCollection<Expression> nodes)
		{
			Expression[] array = null;
			int i = 0;
			int count = nodes.Count;
			while (i < count)
			{
				Expression expression = this.Visit(nodes[i]);
				if (array != null)
				{
					array[i] = expression;
				}
				else if (expression != nodes[i])
				{
					array = new Expression[count];
					for (int j = 0; j < i; j++)
					{
						array[j] = nodes[j];
					}
					array[i] = expression;
				}
				i++;
			}
			if (array == null)
			{
				return nodes;
			}
			return new TrueReadOnlyCollection<Expression>(array);
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x000477E8 File Offset: 0x000459E8
		internal Expression[] VisitArguments(IArgumentProvider nodes)
		{
			Expression[] array = null;
			int i = 0;
			int argumentCount = nodes.ArgumentCount;
			while (i < argumentCount)
			{
				Expression argument = nodes.GetArgument(i);
				Expression expression = this.Visit(argument);
				if (array != null)
				{
					array[i] = expression;
				}
				else if (expression != argument)
				{
					array = new Expression[argumentCount];
					for (int j = 0; j < i; j++)
					{
						array[j] = nodes.GetArgument(j);
					}
					array[i] = expression;
				}
				i++;
			}
			return array;
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00047854 File Offset: 0x00045A54
		[__DynamicallyInvokable]
		public static ReadOnlyCollection<T> Visit<T>(ReadOnlyCollection<T> nodes, Func<T, T> elementVisitor)
		{
			T[] array = null;
			int i = 0;
			int count = nodes.Count;
			while (i < count)
			{
				T t = elementVisitor(nodes[i]);
				if (array != null)
				{
					array[i] = t;
				}
				else if (t != nodes[i])
				{
					array = new T[count];
					for (int j = 0; j < i; j++)
					{
						array[j] = nodes[j];
					}
					array[i] = t;
				}
				i++;
			}
			if (array == null)
			{
				return nodes;
			}
			return new TrueReadOnlyCollection<T>(array);
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x000478E0 File Offset: 0x00045AE0
		[__DynamicallyInvokable]
		public T VisitAndConvert<T>(T node, string callerName) where T : Expression
		{
			if (node == null)
			{
				return default(T);
			}
			node = (this.Visit(node) as T);
			if (node == null)
			{
				throw Error.MustRewriteToSameNode(callerName, typeof(T), callerName);
			}
			return node;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x00047934 File Offset: 0x00045B34
		[__DynamicallyInvokable]
		public ReadOnlyCollection<T> VisitAndConvert<T>(ReadOnlyCollection<T> nodes, string callerName) where T : Expression
		{
			T[] array = null;
			int i = 0;
			int count = nodes.Count;
			while (i < count)
			{
				T t = this.Visit(nodes[i]) as T;
				if (t == null)
				{
					throw Error.MustRewriteToSameNode(callerName, typeof(T), callerName);
				}
				if (array != null)
				{
					array[i] = t;
				}
				else if (t != nodes[i])
				{
					array = new T[count];
					for (int j = 0; j < i; j++)
					{
						array[j] = nodes[j];
					}
					array[i] = t;
				}
				i++;
			}
			if (array == null)
			{
				return nodes;
			}
			return new TrueReadOnlyCollection<T>(array);
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x000479EE File Offset: 0x00045BEE
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitBinary(BinaryExpression node)
		{
			return ExpressionVisitor.ValidateBinary(node, node.Update(this.Visit(node.Left), this.VisitAndConvert<LambdaExpression>(node.Conversion, "VisitBinary"), this.Visit(node.Right)));
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x00047A28 File Offset: 0x00045C28
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitBlock(BlockExpression node)
		{
			int expressionCount = node.ExpressionCount;
			Expression[] array = null;
			for (int i = 0; i < expressionCount; i++)
			{
				Expression expression = node.GetExpression(i);
				Expression expression2 = this.Visit(expression);
				if (expression != expression2)
				{
					if (array == null)
					{
						array = new Expression[expressionCount];
					}
					array[i] = expression2;
				}
			}
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = this.VisitAndConvert<ParameterExpression>(node.Variables, "VisitBlock");
			if (readOnlyCollection == node.Variables && array == null)
			{
				return node;
			}
			for (int j = 0; j < expressionCount; j++)
			{
				if (array[j] == null)
				{
					array[j] = node.GetExpression(j);
				}
			}
			return node.Rewrite(readOnlyCollection, array);
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00047ABC File Offset: 0x00045CBC
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitConditional(ConditionalExpression node)
		{
			return node.Update(this.Visit(node.Test), this.Visit(node.IfTrue), this.Visit(node.IfFalse));
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00047AE8 File Offset: 0x00045CE8
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitConstant(ConstantExpression node)
		{
			return node;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00047AEB File Offset: 0x00045CEB
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitDebugInfo(DebugInfoExpression node)
		{
			return node;
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00047AF0 File Offset: 0x00045CF0
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitDynamic(DynamicExpression node)
		{
			Expression[] array = this.VisitArguments(node);
			if (array == null)
			{
				return node;
			}
			return node.Rewrite(array);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00047B11 File Offset: 0x00045D11
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitDefault(DefaultExpression node)
		{
			return node;
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x00047B14 File Offset: 0x00045D14
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitExtension(Expression node)
		{
			return node.VisitChildren(this);
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x00047B1D File Offset: 0x00045D1D
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitGoto(GotoExpression node)
		{
			return node.Update(this.VisitLabelTarget(node.Target), this.Visit(node.Value));
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00047B40 File Offset: 0x00045D40
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitInvocation(InvocationExpression node)
		{
			Expression expression = this.Visit(node.Expression);
			Expression[] array = this.VisitArguments(node);
			if (expression == node.Expression && array == null)
			{
				return node;
			}
			return node.Rewrite(expression, array);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00047B78 File Offset: 0x00045D78
		[__DynamicallyInvokable]
		protected virtual LabelTarget VisitLabelTarget(LabelTarget node)
		{
			return node;
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00047B7B File Offset: 0x00045D7B
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitLabel(LabelExpression node)
		{
			return node.Update(this.VisitLabelTarget(node.Target), this.Visit(node.DefaultValue));
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x00047B9B File Offset: 0x00045D9B
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitLambda<T>(Expression<T> node)
		{
			return node.Update(this.Visit(node.Body), this.VisitAndConvert<ParameterExpression>(node.Parameters, "VisitLambda"));
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x00047BC0 File Offset: 0x00045DC0
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitLoop(LoopExpression node)
		{
			return node.Update(this.VisitLabelTarget(node.BreakLabel), this.VisitLabelTarget(node.ContinueLabel), this.Visit(node.Body));
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x00047BEC File Offset: 0x00045DEC
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitMember(MemberExpression node)
		{
			return node.Update(this.Visit(node.Expression));
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x00047C00 File Offset: 0x00045E00
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitIndex(IndexExpression node)
		{
			Expression expression = this.Visit(node.Object);
			Expression[] array = this.VisitArguments(node);
			if (expression == node.Object && array == null)
			{
				return node;
			}
			return node.Rewrite(expression, array);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00047C38 File Offset: 0x00045E38
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitMethodCall(MethodCallExpression node)
		{
			Expression expression = this.Visit(node.Object);
			Expression[] array = this.VisitArguments(node);
			if (expression == node.Object && array == null)
			{
				return node;
			}
			return node.Rewrite(expression, array);
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x00047C70 File Offset: 0x00045E70
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitNewArray(NewArrayExpression node)
		{
			return node.Update(this.Visit(node.Expressions));
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00047C84 File Offset: 0x00045E84
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitNew(NewExpression node)
		{
			return node.Update(this.Visit(node.Arguments));
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x00047C98 File Offset: 0x00045E98
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitParameter(ParameterExpression node)
		{
			return node;
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x00047C9B File Offset: 0x00045E9B
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
		{
			return node.Update(this.VisitAndConvert<ParameterExpression>(node.Variables, "VisitRuntimeVariables"));
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x00047CB4 File Offset: 0x00045EB4
		[__DynamicallyInvokable]
		protected virtual SwitchCase VisitSwitchCase(SwitchCase node)
		{
			return node.Update(this.Visit(node.TestValues), this.Visit(node.Body));
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x00047CD4 File Offset: 0x00045ED4
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitSwitch(SwitchExpression node)
		{
			return ExpressionVisitor.ValidateSwitch(node, node.Update(this.Visit(node.SwitchValue), ExpressionVisitor.Visit<SwitchCase>(node.Cases, new Func<SwitchCase, SwitchCase>(this.VisitSwitchCase)), this.Visit(node.DefaultBody)));
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00047D12 File Offset: 0x00045F12
		[__DynamicallyInvokable]
		protected virtual CatchBlock VisitCatchBlock(CatchBlock node)
		{
			return node.Update(this.VisitAndConvert<ParameterExpression>(node.Variable, "VisitCatchBlock"), this.Visit(node.Filter), this.Visit(node.Body));
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00047D44 File Offset: 0x00045F44
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitTry(TryExpression node)
		{
			return node.Update(this.Visit(node.Body), ExpressionVisitor.Visit<CatchBlock>(node.Handlers, new Func<CatchBlock, CatchBlock>(this.VisitCatchBlock)), this.Visit(node.Finally), this.Visit(node.Fault));
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00047D93 File Offset: 0x00045F93
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitTypeBinary(TypeBinaryExpression node)
		{
			return node.Update(this.Visit(node.Expression));
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x00047DA7 File Offset: 0x00045FA7
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitUnary(UnaryExpression node)
		{
			return ExpressionVisitor.ValidateUnary(node, node.Update(this.Visit(node.Operand)));
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x00047DC1 File Offset: 0x00045FC1
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitMemberInit(MemberInitExpression node)
		{
			return node.Update(this.VisitAndConvert<NewExpression>(node.NewExpression, "VisitMemberInit"), ExpressionVisitor.Visit<MemberBinding>(node.Bindings, new Func<MemberBinding, MemberBinding>(this.VisitMemberBinding)));
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x00047DF2 File Offset: 0x00045FF2
		[__DynamicallyInvokable]
		protected internal virtual Expression VisitListInit(ListInitExpression node)
		{
			return node.Update(this.VisitAndConvert<NewExpression>(node.NewExpression, "VisitListInit"), ExpressionVisitor.Visit<ElementInit>(node.Initializers, new Func<ElementInit, ElementInit>(this.VisitElementInit)));
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x00047E23 File Offset: 0x00046023
		[__DynamicallyInvokable]
		protected virtual ElementInit VisitElementInit(ElementInit node)
		{
			return node.Update(this.Visit(node.Arguments));
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x00047E38 File Offset: 0x00046038
		[__DynamicallyInvokable]
		protected virtual MemberBinding VisitMemberBinding(MemberBinding node)
		{
			switch (node.BindingType)
			{
			case MemberBindingType.Assignment:
				return this.VisitMemberAssignment((MemberAssignment)node);
			case MemberBindingType.MemberBinding:
				return this.VisitMemberMemberBinding((MemberMemberBinding)node);
			case MemberBindingType.ListBinding:
				return this.VisitMemberListBinding((MemberListBinding)node);
			default:
				throw Error.UnhandledBindingType(node.BindingType);
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00047E97 File Offset: 0x00046097
		[__DynamicallyInvokable]
		protected virtual MemberAssignment VisitMemberAssignment(MemberAssignment node)
		{
			return node.Update(this.Visit(node.Expression));
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00047EAB File Offset: 0x000460AB
		[__DynamicallyInvokable]
		protected virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
		{
			return node.Update(ExpressionVisitor.Visit<MemberBinding>(node.Bindings, new Func<MemberBinding, MemberBinding>(this.VisitMemberBinding)));
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00047ECB File Offset: 0x000460CB
		[__DynamicallyInvokable]
		protected virtual MemberListBinding VisitMemberListBinding(MemberListBinding node)
		{
			return node.Update(ExpressionVisitor.Visit<ElementInit>(node.Initializers, new Func<ElementInit, ElementInit>(this.VisitElementInit)));
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00047EEC File Offset: 0x000460EC
		private static UnaryExpression ValidateUnary(UnaryExpression before, UnaryExpression after)
		{
			if (before != after && before.Method == null)
			{
				if (after.Method != null)
				{
					throw Error.MustRewriteWithoutMethod(after.Method, "VisitUnary");
				}
				if (before.Operand != null && after.Operand != null)
				{
					ExpressionVisitor.ValidateChildType(before.Operand.Type, after.Operand.Type, "VisitUnary");
				}
			}
			return after;
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00047F5C File Offset: 0x0004615C
		private static BinaryExpression ValidateBinary(BinaryExpression before, BinaryExpression after)
		{
			if (before != after && before.Method == null)
			{
				if (after.Method != null)
				{
					throw Error.MustRewriteWithoutMethod(after.Method, "VisitBinary");
				}
				ExpressionVisitor.ValidateChildType(before.Left.Type, after.Left.Type, "VisitBinary");
				ExpressionVisitor.ValidateChildType(before.Right.Type, after.Right.Type, "VisitBinary");
			}
			return after;
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x00047FDB File Offset: 0x000461DB
		private static SwitchExpression ValidateSwitch(SwitchExpression before, SwitchExpression after)
		{
			if (before.Comparison == null && after.Comparison != null)
			{
				throw Error.MustRewriteWithoutMethod(after.Comparison, "VisitSwitch");
			}
			return after;
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0004800B File Offset: 0x0004620B
		private static void ValidateChildType(Type before, Type after, string methodName)
		{
			if (before.IsValueType)
			{
				if (TypeUtils.AreEquivalent(before, after))
				{
					return;
				}
			}
			else if (!after.IsValueType)
			{
				return;
			}
			throw Error.MustRewriteChildToSameType(before, after, methodName);
		}
	}
}
