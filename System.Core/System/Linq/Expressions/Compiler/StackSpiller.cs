using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x02000284 RID: 644
	internal class StackSpiller
	{
		// Token: 0x060017BA RID: 6074 RVA: 0x000552E8 File Offset: 0x000534E8
		internal static LambdaExpression AnalyzeLambda(LambdaExpression lambda)
		{
			return lambda.Accept(new StackSpiller(StackSpiller.Stack.Empty));
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x000552F6 File Offset: 0x000534F6
		private StackSpiller(StackSpiller.Stack stack)
		{
			this._startingStack = stack;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0005531C File Offset: 0x0005351C
		internal Expression<T> Rewrite<T>(Expression<T> lambda)
		{
			StackSpiller.Result result = this.RewriteExpressionFreeTemps(lambda.Body, this._startingStack);
			this._lambdaRewrite = result.Action;
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				Expression expression = result.Node;
				if (this._tm.Temps.Count > 0)
				{
					expression = Expression.Block(this._tm.Temps, new Expression[]
					{
						expression
					});
				}
				return new Expression<T>(expression, lambda.Name, lambda.TailCall, lambda.Parameters);
			}
			return lambda;
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0005539F File Offset: 0x0005359F
		[Conditional("DEBUG")]
		private static void VerifyRewrite(StackSpiller.Result result, Expression node)
		{
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x000553A4 File Offset: 0x000535A4
		private StackSpiller.Result RewriteExpressionFreeTemps(Expression expression, StackSpiller.Stack stack)
		{
			int mark = this.Mark();
			StackSpiller.Result result = this.RewriteExpression(expression, stack);
			this.Free(mark);
			return result;
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x000553CC File Offset: 0x000535CC
		private StackSpiller.Result RewriteDynamicExpression(Expression expr, StackSpiller.Stack stack)
		{
			DynamicExpression dynamicExpression = (DynamicExpression)expr;
			IArgumentProvider argumentProvider = dynamicExpression;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, StackSpiller.Stack.NonEmpty, argumentProvider.ArgumentCount);
			childRewriter.AddArguments(argumentProvider);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				StackSpiller.RequireNoRefArgs(dynamicExpression.DelegateType.GetMethod("Invoke"));
			}
			return childRewriter.Finish(childRewriter.Rewrite ? dynamicExpression.Rewrite(childRewriter[0, -1]) : expr);
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00055434 File Offset: 0x00053634
		private StackSpiller.Result RewriteIndexAssignment(BinaryExpression node, StackSpiller.Stack stack)
		{
			IndexExpression indexExpression = (IndexExpression)node.Left;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, 2 + indexExpression.Arguments.Count);
			childRewriter.Add(indexExpression.Object);
			childRewriter.Add(indexExpression.Arguments);
			childRewriter.Add(node.Right);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				StackSpiller.RequireNotRefInstance(indexExpression.Object);
			}
			if (childRewriter.Rewrite)
			{
				node = new AssignBinaryExpression(new IndexExpression(childRewriter[0], indexExpression.Indexer, childRewriter[1, -2]), childRewriter[-1]);
			}
			return childRewriter.Finish(node);
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x000554D4 File Offset: 0x000536D4
		private StackSpiller.Result RewriteLogicalBinaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			BinaryExpression binaryExpression = (BinaryExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(binaryExpression.Left, stack);
			StackSpiller.Result result2 = this.RewriteExpression(binaryExpression.Right, stack);
			StackSpiller.Result result3 = this.RewriteExpression(binaryExpression.Conversion, stack);
			StackSpiller.RewriteAction rewriteAction = result.Action | result2.Action | result3.Action;
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = BinaryExpression.Create(binaryExpression.NodeType, result.Node, result2.Node, binaryExpression.Type, binaryExpression.Method, (LambdaExpression)result3.Node);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00055564 File Offset: 0x00053764
		private StackSpiller.Result RewriteReducibleExpression(Expression expr, StackSpiller.Stack stack)
		{
			StackSpiller.Result result = this.RewriteExpression(expr.Reduce(), stack);
			return new StackSpiller.Result(result.Action | StackSpiller.RewriteAction.Copy, result.Node);
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00055594 File Offset: 0x00053794
		private StackSpiller.Result RewriteBinaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			BinaryExpression binaryExpression = (BinaryExpression)expr;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, 3);
			childRewriter.Add(binaryExpression.Left);
			childRewriter.Add(binaryExpression.Right);
			childRewriter.Add(binaryExpression.Conversion);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				StackSpiller.RequireNoRefArgs(binaryExpression.Method);
			}
			return childRewriter.Finish(childRewriter.Rewrite ? BinaryExpression.Create(binaryExpression.NodeType, childRewriter[0], childRewriter[1], binaryExpression.Type, binaryExpression.Method, (LambdaExpression)childRewriter[2]) : expr);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0005562C File Offset: 0x0005382C
		private StackSpiller.Result RewriteVariableAssignment(BinaryExpression node, StackSpiller.Stack stack)
		{
			StackSpiller.Result result = this.RewriteExpression(node.Right, stack);
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				node = Expression.Assign(node.Left, result.Node);
			}
			return new StackSpiller.Result(result.Action, node);
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00055670 File Offset: 0x00053870
		private StackSpiller.Result RewriteAssignBinaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			BinaryExpression binaryExpression = (BinaryExpression)expr;
			ExpressionType nodeType = binaryExpression.Left.NodeType;
			if (nodeType <= ExpressionType.Parameter)
			{
				if (nodeType == ExpressionType.MemberAccess)
				{
					return this.RewriteMemberAssignment(binaryExpression, stack);
				}
				if (nodeType == ExpressionType.Parameter)
				{
					return this.RewriteVariableAssignment(binaryExpression, stack);
				}
			}
			else
			{
				if (nodeType == ExpressionType.Extension)
				{
					return this.RewriteExtensionAssignment(binaryExpression, stack);
				}
				if (nodeType == ExpressionType.Index)
				{
					return this.RewriteIndexAssignment(binaryExpression, stack);
				}
			}
			throw Error.InvalidLvalue(binaryExpression.Left.NodeType);
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x000556E4 File Offset: 0x000538E4
		private StackSpiller.Result RewriteExtensionAssignment(BinaryExpression node, StackSpiller.Stack stack)
		{
			node = Expression.Assign(node.Left.ReduceExtensions(), node.Right);
			StackSpiller.Result result = this.RewriteAssignBinaryExpression(node, stack);
			return new StackSpiller.Result(result.Action | StackSpiller.RewriteAction.Copy, result.Node);
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00055728 File Offset: 0x00053928
		private static StackSpiller.Result RewriteLambdaExpression(Expression expr, StackSpiller.Stack stack)
		{
			LambdaExpression lambdaExpression = (LambdaExpression)expr;
			expr = StackSpiller.AnalyzeLambda(lambdaExpression);
			StackSpiller.RewriteAction action = (expr == lambdaExpression) ? StackSpiller.RewriteAction.None : StackSpiller.RewriteAction.Copy;
			return new StackSpiller.Result(action, expr);
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00055754 File Offset: 0x00053954
		private StackSpiller.Result RewriteConditionalExpression(Expression expr, StackSpiller.Stack stack)
		{
			ConditionalExpression conditionalExpression = (ConditionalExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(conditionalExpression.Test, stack);
			StackSpiller.Result result2 = this.RewriteExpression(conditionalExpression.IfTrue, stack);
			StackSpiller.Result result3 = this.RewriteExpression(conditionalExpression.IfFalse, stack);
			StackSpiller.RewriteAction rewriteAction = result.Action | result2.Action | result3.Action;
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = Expression.Condition(result.Node, result2.Node, result3.Node, conditionalExpression.Type);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x000557D4 File Offset: 0x000539D4
		private StackSpiller.Result RewriteMemberAssignment(BinaryExpression node, StackSpiller.Stack stack)
		{
			MemberExpression memberExpression = (MemberExpression)node.Left;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, 2);
			childRewriter.Add(memberExpression.Expression);
			childRewriter.Add(node.Right);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				StackSpiller.RequireNotRefInstance(memberExpression.Expression);
			}
			if (childRewriter.Rewrite)
			{
				return childRewriter.Finish(new AssignBinaryExpression(MemberExpression.Make(childRewriter[0], memberExpression.Member), childRewriter[1]));
			}
			return new StackSpiller.Result(StackSpiller.RewriteAction.None, node);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00055858 File Offset: 0x00053A58
		private StackSpiller.Result RewriteMemberExpression(Expression expr, StackSpiller.Stack stack)
		{
			MemberExpression memberExpression = (MemberExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(memberExpression.Expression, stack);
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				if (result.Action == StackSpiller.RewriteAction.SpillStack && memberExpression.Member.MemberType == MemberTypes.Property)
				{
					StackSpiller.RequireNotRefInstance(memberExpression.Expression);
				}
				expr = MemberExpression.Make(result.Node, memberExpression.Member);
			}
			return new StackSpiller.Result(result.Action, expr);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x000558C4 File Offset: 0x00053AC4
		private StackSpiller.Result RewriteIndexExpression(Expression expr, StackSpiller.Stack stack)
		{
			IndexExpression indexExpression = (IndexExpression)expr;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, indexExpression.Arguments.Count + 1);
			childRewriter.Add(indexExpression.Object);
			childRewriter.Add(indexExpression.Arguments);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				StackSpiller.RequireNotRefInstance(indexExpression.Object);
			}
			if (childRewriter.Rewrite)
			{
				expr = new IndexExpression(childRewriter[0], indexExpression.Indexer, childRewriter[1, -1]);
			}
			return childRewriter.Finish(expr);
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00055944 File Offset: 0x00053B44
		private StackSpiller.Result RewriteMethodCallExpression(Expression expr, StackSpiller.Stack stack)
		{
			MethodCallExpression methodCallExpression = (MethodCallExpression)expr;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, methodCallExpression.Arguments.Count + 1);
			childRewriter.Add(methodCallExpression.Object);
			childRewriter.AddArguments(methodCallExpression);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				StackSpiller.RequireNotRefInstance(methodCallExpression.Object);
				StackSpiller.RequireNoRefArgs(methodCallExpression.Method);
			}
			return childRewriter.Finish(childRewriter.Rewrite ? methodCallExpression.Rewrite(childRewriter[0], childRewriter[1, -1]) : expr);
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x000559C8 File Offset: 0x00053BC8
		private StackSpiller.Result RewriteNewArrayExpression(Expression expr, StackSpiller.Stack stack)
		{
			NewArrayExpression newArrayExpression = (NewArrayExpression)expr;
			if (newArrayExpression.NodeType == ExpressionType.NewArrayInit)
			{
				stack = StackSpiller.Stack.NonEmpty;
			}
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, newArrayExpression.Expressions.Count);
			childRewriter.Add(newArrayExpression.Expressions);
			if (childRewriter.Rewrite)
			{
				Type elementType = newArrayExpression.Type.GetElementType();
				if (newArrayExpression.NodeType == ExpressionType.NewArrayInit)
				{
					expr = Expression.NewArrayInit(elementType, childRewriter[0, -1]);
				}
				else
				{
					expr = Expression.NewArrayBounds(elementType, childRewriter[0, -1]);
				}
			}
			return childRewriter.Finish(expr);
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00055A50 File Offset: 0x00053C50
		private StackSpiller.Result RewriteInvocationExpression(Expression expr, StackSpiller.Stack stack)
		{
			InvocationExpression invocationExpression = (InvocationExpression)expr;
			LambdaExpression lambdaExpression = invocationExpression.LambdaOperand;
			StackSpiller.ChildRewriter childRewriter;
			if (lambdaExpression != null)
			{
				childRewriter = new StackSpiller.ChildRewriter(this, stack, invocationExpression.Arguments.Count);
				childRewriter.Add(invocationExpression.Arguments);
				if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
				{
					StackSpiller.RequireNoRefArgs(Expression.GetInvokeMethod(invocationExpression.Expression));
				}
				StackSpiller stackSpiller = new StackSpiller(stack);
				lambdaExpression = lambdaExpression.Accept(stackSpiller);
				if (childRewriter.Rewrite || stackSpiller._lambdaRewrite != StackSpiller.RewriteAction.None)
				{
					invocationExpression = new InvocationExpression(lambdaExpression, childRewriter[0, -1], invocationExpression.Type);
				}
				StackSpiller.Result result = childRewriter.Finish(invocationExpression);
				return new StackSpiller.Result(result.Action | stackSpiller._lambdaRewrite, result.Node);
			}
			childRewriter = new StackSpiller.ChildRewriter(this, stack, invocationExpression.Arguments.Count + 1);
			childRewriter.Add(invocationExpression.Expression);
			childRewriter.Add(invocationExpression.Arguments);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				StackSpiller.RequireNoRefArgs(Expression.GetInvokeMethod(invocationExpression.Expression));
			}
			return childRewriter.Finish(childRewriter.Rewrite ? new InvocationExpression(childRewriter[0], childRewriter[1, -1], invocationExpression.Type) : expr);
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00055B74 File Offset: 0x00053D74
		private StackSpiller.Result RewriteNewExpression(Expression expr, StackSpiller.Stack stack)
		{
			NewExpression newExpression = (NewExpression)expr;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, newExpression.Arguments.Count);
			childRewriter.AddArguments(newExpression);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				StackSpiller.RequireNoRefArgs(newExpression.Constructor);
			}
			return childRewriter.Finish(childRewriter.Rewrite ? new NewExpression(newExpression.Constructor, childRewriter[0, -1], newExpression.Members) : expr);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00055BE0 File Offset: 0x00053DE0
		private StackSpiller.Result RewriteTypeBinaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			TypeBinaryExpression typeBinaryExpression = (TypeBinaryExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(typeBinaryExpression.Expression, stack);
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				if (typeBinaryExpression.NodeType == ExpressionType.TypeIs)
				{
					expr = Expression.TypeIs(result.Node, typeBinaryExpression.TypeOperand);
				}
				else
				{
					expr = Expression.TypeEqual(result.Node, typeBinaryExpression.TypeOperand);
				}
			}
			return new StackSpiller.Result(result.Action, expr);
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00055C48 File Offset: 0x00053E48
		private StackSpiller.Result RewriteThrowUnaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			UnaryExpression unaryExpression = (UnaryExpression)expr;
			StackSpiller.Result result = this.RewriteExpressionFreeTemps(unaryExpression.Operand, StackSpiller.Stack.Empty);
			StackSpiller.RewriteAction rewriteAction = result.Action;
			if (stack != StackSpiller.Stack.Empty)
			{
				rewriteAction = StackSpiller.RewriteAction.SpillStack;
			}
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = Expression.Throw(result.Node, unaryExpression.Type);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00055C94 File Offset: 0x00053E94
		private StackSpiller.Result RewriteUnaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			UnaryExpression unaryExpression = (UnaryExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(unaryExpression.Operand, stack);
			if (result.Action == StackSpiller.RewriteAction.SpillStack)
			{
				StackSpiller.RequireNoRefArgs(unaryExpression.Method);
			}
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				expr = new UnaryExpression(unaryExpression.NodeType, result.Node, unaryExpression.Type, unaryExpression.Method);
			}
			return new StackSpiller.Result(result.Action, expr);
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00055D00 File Offset: 0x00053F00
		private StackSpiller.Result RewriteListInitExpression(Expression expr, StackSpiller.Stack stack)
		{
			ListInitExpression listInitExpression = (ListInitExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(listInitExpression.NewExpression, stack);
			Expression node = result.Node;
			StackSpiller.RewriteAction rewriteAction = result.Action;
			ReadOnlyCollection<ElementInit> initializers = listInitExpression.Initializers;
			StackSpiller.ChildRewriter[] array = new StackSpiller.ChildRewriter[initializers.Count];
			for (int i = 0; i < initializers.Count; i++)
			{
				ElementInit elementInit = initializers[i];
				StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, StackSpiller.Stack.NonEmpty, elementInit.Arguments.Count);
				childRewriter.Add(elementInit.Arguments);
				rewriteAction |= childRewriter.Action;
				array[i] = childRewriter;
			}
			switch (rewriteAction)
			{
			case StackSpiller.RewriteAction.None:
				goto IL_1CD;
			case StackSpiller.RewriteAction.Copy:
			{
				ElementInit[] array2 = new ElementInit[initializers.Count];
				for (int j = 0; j < initializers.Count; j++)
				{
					StackSpiller.ChildRewriter childRewriter2 = array[j];
					if (childRewriter2.Action == StackSpiller.RewriteAction.None)
					{
						array2[j] = initializers[j];
					}
					else
					{
						array2[j] = Expression.ElementInit(initializers[j].AddMethod, childRewriter2[0, -1]);
					}
				}
				expr = Expression.ListInit((NewExpression)node, new TrueReadOnlyCollection<ElementInit>(array2));
				goto IL_1CD;
			}
			case StackSpiller.RewriteAction.SpillStack:
			{
				StackSpiller.RequireNotRefInstance(listInitExpression.NewExpression);
				ParameterExpression parameterExpression = this.MakeTemp(node.Type);
				Expression[] array3 = new Expression[initializers.Count + 2];
				array3[0] = Expression.Assign(parameterExpression, node);
				for (int k = 0; k < initializers.Count; k++)
				{
					StackSpiller.ChildRewriter childRewriter3 = array[k];
					StackSpiller.Result result2 = childRewriter3.Finish(Expression.Call(parameterExpression, initializers[k].AddMethod, childRewriter3[0, -1]));
					array3[k + 1] = result2.Node;
				}
				array3[initializers.Count + 1] = parameterExpression;
				expr = StackSpiller.MakeBlock(array3);
				goto IL_1CD;
			}
			}
			throw ContractUtils.Unreachable;
			IL_1CD:
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00055EE4 File Offset: 0x000540E4
		private StackSpiller.Result RewriteMemberInitExpression(Expression expr, StackSpiller.Stack stack)
		{
			MemberInitExpression memberInitExpression = (MemberInitExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(memberInitExpression.NewExpression, stack);
			Expression node = result.Node;
			StackSpiller.RewriteAction rewriteAction = result.Action;
			ReadOnlyCollection<MemberBinding> bindings = memberInitExpression.Bindings;
			StackSpiller.BindingRewriter[] array = new StackSpiller.BindingRewriter[bindings.Count];
			for (int i = 0; i < bindings.Count; i++)
			{
				MemberBinding binding = bindings[i];
				StackSpiller.BindingRewriter bindingRewriter = StackSpiller.BindingRewriter.Create(binding, this, StackSpiller.Stack.NonEmpty);
				array[i] = bindingRewriter;
				rewriteAction |= bindingRewriter.Action;
			}
			switch (rewriteAction)
			{
			case StackSpiller.RewriteAction.None:
				goto IL_162;
			case StackSpiller.RewriteAction.Copy:
			{
				MemberBinding[] array2 = new MemberBinding[bindings.Count];
				for (int j = 0; j < bindings.Count; j++)
				{
					array2[j] = array[j].AsBinding();
				}
				expr = Expression.MemberInit((NewExpression)node, new TrueReadOnlyCollection<MemberBinding>(array2));
				goto IL_162;
			}
			case StackSpiller.RewriteAction.SpillStack:
			{
				StackSpiller.RequireNotRefInstance(memberInitExpression.NewExpression);
				ParameterExpression parameterExpression = this.MakeTemp(node.Type);
				Expression[] array3 = new Expression[bindings.Count + 2];
				array3[0] = Expression.Assign(parameterExpression, node);
				for (int k = 0; k < bindings.Count; k++)
				{
					StackSpiller.BindingRewriter bindingRewriter2 = array[k];
					Expression expression = bindingRewriter2.AsExpression(parameterExpression);
					array3[k + 1] = expression;
				}
				array3[bindings.Count + 1] = parameterExpression;
				expr = StackSpiller.MakeBlock(array3);
				goto IL_162;
			}
			}
			throw ContractUtils.Unreachable;
			IL_162:
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x0005605C File Offset: 0x0005425C
		private StackSpiller.Result RewriteBlockExpression(Expression expr, StackSpiller.Stack stack)
		{
			BlockExpression blockExpression = (BlockExpression)expr;
			int expressionCount = blockExpression.ExpressionCount;
			StackSpiller.RewriteAction rewriteAction = StackSpiller.RewriteAction.None;
			Expression[] array = null;
			for (int i = 0; i < expressionCount; i++)
			{
				Expression expression = blockExpression.GetExpression(i);
				StackSpiller.Result result = this.RewriteExpression(expression, stack);
				rewriteAction |= result.Action;
				if (array == null && result.Action != StackSpiller.RewriteAction.None)
				{
					array = StackSpiller.Clone<Expression>(blockExpression.Expressions, i);
				}
				if (array != null)
				{
					array[i] = result.Node;
				}
			}
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = blockExpression.Rewrite(null, array);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x000560E8 File Offset: 0x000542E8
		private StackSpiller.Result RewriteLabelExpression(Expression expr, StackSpiller.Stack stack)
		{
			LabelExpression labelExpression = (LabelExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(labelExpression.DefaultValue, stack);
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				expr = Expression.Label(labelExpression.Target, result.Node);
			}
			return new StackSpiller.Result(result.Action, expr);
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x00056134 File Offset: 0x00054334
		private StackSpiller.Result RewriteLoopExpression(Expression expr, StackSpiller.Stack stack)
		{
			LoopExpression loopExpression = (LoopExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(loopExpression.Body, StackSpiller.Stack.Empty);
			StackSpiller.RewriteAction rewriteAction = result.Action;
			if (stack != StackSpiller.Stack.Empty)
			{
				rewriteAction = StackSpiller.RewriteAction.SpillStack;
			}
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = new LoopExpression(result.Node, loopExpression.BreakLabel, loopExpression.ContinueLabel);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00056188 File Offset: 0x00054388
		private StackSpiller.Result RewriteGotoExpression(Expression expr, StackSpiller.Stack stack)
		{
			GotoExpression gotoExpression = (GotoExpression)expr;
			StackSpiller.Result result = this.RewriteExpressionFreeTemps(gotoExpression.Value, StackSpiller.Stack.Empty);
			StackSpiller.RewriteAction rewriteAction = result.Action;
			if (stack != StackSpiller.Stack.Empty)
			{
				rewriteAction = StackSpiller.RewriteAction.SpillStack;
			}
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = Expression.MakeGoto(gotoExpression.Kind, gotoExpression.Target, result.Node, gotoExpression.Type);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x000561E0 File Offset: 0x000543E0
		private StackSpiller.Result RewriteSwitchExpression(Expression expr, StackSpiller.Stack stack)
		{
			SwitchExpression switchExpression = (SwitchExpression)expr;
			StackSpiller.Result result = this.RewriteExpressionFreeTemps(switchExpression.SwitchValue, stack);
			StackSpiller.RewriteAction rewriteAction = result.Action;
			ReadOnlyCollection<SwitchCase> readOnlyCollection = switchExpression.Cases;
			SwitchCase[] array = null;
			for (int i = 0; i < readOnlyCollection.Count; i++)
			{
				SwitchCase switchCase = readOnlyCollection[i];
				Expression[] array2 = null;
				ReadOnlyCollection<Expression> readOnlyCollection2 = switchCase.TestValues;
				for (int j = 0; j < readOnlyCollection2.Count; j++)
				{
					StackSpiller.Result result2 = this.RewriteExpression(readOnlyCollection2[j], stack);
					rewriteAction |= result2.Action;
					if (array2 == null && result2.Action != StackSpiller.RewriteAction.None)
					{
						array2 = StackSpiller.Clone<Expression>(readOnlyCollection2, j);
					}
					if (array2 != null)
					{
						array2[j] = result2.Node;
					}
				}
				StackSpiller.Result result3 = this.RewriteExpression(switchCase.Body, stack);
				rewriteAction |= result3.Action;
				if (result3.Action != StackSpiller.RewriteAction.None || array2 != null)
				{
					if (array2 != null)
					{
						readOnlyCollection2 = new ReadOnlyCollection<Expression>(array2);
					}
					switchCase = new SwitchCase(result3.Node, readOnlyCollection2);
					if (array == null)
					{
						array = StackSpiller.Clone<SwitchCase>(readOnlyCollection, i);
					}
				}
				if (array != null)
				{
					array[i] = switchCase;
				}
			}
			StackSpiller.Result result4 = this.RewriteExpression(switchExpression.DefaultBody, stack);
			rewriteAction |= result4.Action;
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				if (array != null)
				{
					readOnlyCollection = new ReadOnlyCollection<SwitchCase>(array);
				}
				expr = new SwitchExpression(switchExpression.Type, result.Node, result4.Node, switchExpression.Comparison, readOnlyCollection);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0005634C File Offset: 0x0005454C
		private StackSpiller.Result RewriteTryExpression(Expression expr, StackSpiller.Stack stack)
		{
			TryExpression tryExpression = (TryExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(tryExpression.Body, StackSpiller.Stack.Empty);
			ReadOnlyCollection<CatchBlock> readOnlyCollection = tryExpression.Handlers;
			CatchBlock[] array = null;
			StackSpiller.RewriteAction rewriteAction = result.Action;
			if (readOnlyCollection != null)
			{
				for (int i = 0; i < readOnlyCollection.Count; i++)
				{
					StackSpiller.RewriteAction rewriteAction2 = result.Action;
					CatchBlock catchBlock = readOnlyCollection[i];
					Expression filter = catchBlock.Filter;
					if (catchBlock.Filter != null)
					{
						StackSpiller.Result result2 = this.RewriteExpression(catchBlock.Filter, StackSpiller.Stack.Empty);
						rewriteAction |= result2.Action;
						rewriteAction2 |= result2.Action;
						filter = result2.Node;
					}
					StackSpiller.Result result3 = this.RewriteExpression(catchBlock.Body, StackSpiller.Stack.Empty);
					rewriteAction |= result3.Action;
					rewriteAction2 |= result3.Action;
					if (rewriteAction2 != StackSpiller.RewriteAction.None)
					{
						catchBlock = Expression.MakeCatchBlock(catchBlock.Test, catchBlock.Variable, result3.Node, filter);
						if (array == null)
						{
							array = StackSpiller.Clone<CatchBlock>(readOnlyCollection, i);
						}
					}
					if (array != null)
					{
						array[i] = catchBlock;
					}
				}
			}
			StackSpiller.Result result4 = this.RewriteExpression(tryExpression.Fault, StackSpiller.Stack.Empty);
			rewriteAction |= result4.Action;
			StackSpiller.Result result5 = this.RewriteExpression(tryExpression.Finally, StackSpiller.Stack.Empty);
			rewriteAction |= result5.Action;
			if (stack != StackSpiller.Stack.Empty)
			{
				rewriteAction = StackSpiller.RewriteAction.SpillStack;
			}
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				if (array != null)
				{
					readOnlyCollection = new ReadOnlyCollection<CatchBlock>(array);
				}
				expr = new TryExpression(tryExpression.Type, result.Node, result5.Node, result4.Node, readOnlyCollection);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x000564C8 File Offset: 0x000546C8
		private StackSpiller.Result RewriteExtensionExpression(Expression expr, StackSpiller.Stack stack)
		{
			StackSpiller.Result result = this.RewriteExpression(expr.ReduceExtensions(), stack);
			return new StackSpiller.Result(result.Action | StackSpiller.RewriteAction.Copy, result.Node);
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x000564F8 File Offset: 0x000546F8
		private static T[] Clone<T>(ReadOnlyCollection<T> original, int max)
		{
			T[] array = new T[original.Count];
			for (int i = 0; i < max; i++)
			{
				array[i] = original[i];
			}
			return array;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0005652C File Offset: 0x0005472C
		private static void RequireNoRefArgs(MethodBase method)
		{
			if (method != null)
			{
				if (method.GetParametersCached().Any((ParameterInfo p) => p.ParameterType.IsByRef))
				{
					throw Error.TryNotSupportedForMethodsWithRefArgs(method);
				}
			}
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0005656A File Offset: 0x0005476A
		private static void RequireNotRefInstance(Expression instance)
		{
			if (instance != null && instance.Type.IsValueType && Type.GetTypeCode(instance.Type) == TypeCode.Object)
			{
				throw Error.TryNotSupportedForValueTypeInstances(instance.Type);
			}
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00056598 File Offset: 0x00054798
		private StackSpiller.Result RewriteExpression(Expression node, StackSpiller.Stack stack)
		{
			if (node == null)
			{
				return new StackSpiller.Result(StackSpiller.RewriteAction.None, null);
			}
			if (!this._guard.TryEnterOnCurrentStack())
			{
				return this._guard.RunOnEmptyStack<StackSpiller, Expression, StackSpiller.Stack, StackSpiller.Result>((StackSpiller @this, Expression n, StackSpiller.Stack s) => @this.RewriteExpression(n, s), this, node, stack);
			}
			StackSpiller.Result result;
			switch (node.NodeType)
			{
			case ExpressionType.Add:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.AddChecked:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.And:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.AndAlso:
				result = this.RewriteLogicalBinaryExpression(node, stack);
				break;
			case ExpressionType.ArrayLength:
				result = this.RewriteUnaryExpression(node, stack);
				break;
			case ExpressionType.ArrayIndex:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.Call:
				result = this.RewriteMethodCallExpression(node, stack);
				break;
			case ExpressionType.Coalesce:
				result = this.RewriteLogicalBinaryExpression(node, stack);
				break;
			case ExpressionType.Conditional:
				result = this.RewriteConditionalExpression(node, stack);
				break;
			case ExpressionType.Constant:
			case ExpressionType.Parameter:
			case ExpressionType.Quote:
			case ExpressionType.DebugInfo:
			case ExpressionType.Default:
			case ExpressionType.RuntimeVariables:
				return new StackSpiller.Result(StackSpiller.RewriteAction.None, node);
			case ExpressionType.Convert:
				result = this.RewriteUnaryExpression(node, stack);
				break;
			case ExpressionType.ConvertChecked:
				result = this.RewriteUnaryExpression(node, stack);
				break;
			case ExpressionType.Divide:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.Equal:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.ExclusiveOr:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.GreaterThan:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.GreaterThanOrEqual:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.Invoke:
				result = this.RewriteInvocationExpression(node, stack);
				break;
			case ExpressionType.Lambda:
				result = StackSpiller.RewriteLambdaExpression(node, stack);
				break;
			case ExpressionType.LeftShift:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.LessThan:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.LessThanOrEqual:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.ListInit:
				result = this.RewriteListInitExpression(node, stack);
				break;
			case ExpressionType.MemberAccess:
				result = this.RewriteMemberExpression(node, stack);
				break;
			case ExpressionType.MemberInit:
				result = this.RewriteMemberInitExpression(node, stack);
				break;
			case ExpressionType.Modulo:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.Multiply:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.MultiplyChecked:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.Negate:
				result = this.RewriteUnaryExpression(node, stack);
				break;
			case ExpressionType.UnaryPlus:
				result = this.RewriteUnaryExpression(node, stack);
				break;
			case ExpressionType.NegateChecked:
				result = this.RewriteUnaryExpression(node, stack);
				break;
			case ExpressionType.New:
				result = this.RewriteNewExpression(node, stack);
				break;
			case ExpressionType.NewArrayInit:
				result = this.RewriteNewArrayExpression(node, stack);
				break;
			case ExpressionType.NewArrayBounds:
				result = this.RewriteNewArrayExpression(node, stack);
				break;
			case ExpressionType.Not:
				result = this.RewriteUnaryExpression(node, stack);
				break;
			case ExpressionType.NotEqual:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.Or:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.OrElse:
				result = this.RewriteLogicalBinaryExpression(node, stack);
				break;
			case ExpressionType.Power:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.RightShift:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.Subtract:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.SubtractChecked:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.TypeAs:
				result = this.RewriteUnaryExpression(node, stack);
				break;
			case ExpressionType.TypeIs:
				result = this.RewriteTypeBinaryExpression(node, stack);
				break;
			case ExpressionType.Assign:
				result = this.RewriteAssignBinaryExpression(node, stack);
				break;
			case ExpressionType.Block:
				result = this.RewriteBlockExpression(node, stack);
				break;
			case ExpressionType.Decrement:
				result = this.RewriteUnaryExpression(node, stack);
				break;
			case ExpressionType.Dynamic:
				result = this.RewriteDynamicExpression(node, stack);
				break;
			case ExpressionType.Extension:
				result = this.RewriteExtensionExpression(node, stack);
				break;
			case ExpressionType.Goto:
				result = this.RewriteGotoExpression(node, stack);
				break;
			case ExpressionType.Increment:
				result = this.RewriteUnaryExpression(node, stack);
				break;
			case ExpressionType.Index:
				result = this.RewriteIndexExpression(node, stack);
				break;
			case ExpressionType.Label:
				result = this.RewriteLabelExpression(node, stack);
				break;
			case ExpressionType.Loop:
				result = this.RewriteLoopExpression(node, stack);
				break;
			case ExpressionType.Switch:
				result = this.RewriteSwitchExpression(node, stack);
				break;
			case ExpressionType.Throw:
				result = this.RewriteThrowUnaryExpression(node, stack);
				break;
			case ExpressionType.Try:
				result = this.RewriteTryExpression(node, stack);
				break;
			case ExpressionType.Unbox:
				result = this.RewriteUnaryExpression(node, stack);
				break;
			case ExpressionType.AddAssign:
			case ExpressionType.AndAssign:
			case ExpressionType.DivideAssign:
			case ExpressionType.ExclusiveOrAssign:
			case ExpressionType.LeftShiftAssign:
			case ExpressionType.ModuloAssign:
			case ExpressionType.MultiplyAssign:
			case ExpressionType.OrAssign:
			case ExpressionType.PowerAssign:
			case ExpressionType.RightShiftAssign:
			case ExpressionType.SubtractAssign:
			case ExpressionType.AddAssignChecked:
			case ExpressionType.MultiplyAssignChecked:
			case ExpressionType.SubtractAssignChecked:
			case ExpressionType.PreIncrementAssign:
			case ExpressionType.PreDecrementAssign:
			case ExpressionType.PostIncrementAssign:
			case ExpressionType.PostDecrementAssign:
				result = this.RewriteReducibleExpression(node, stack);
				break;
			case ExpressionType.TypeEqual:
				result = this.RewriteTypeBinaryExpression(node, stack);
				break;
			case ExpressionType.OnesComplement:
				result = this.RewriteUnaryExpression(node, stack);
				break;
			case ExpressionType.IsTrue:
				result = this.RewriteUnaryExpression(node, stack);
				break;
			case ExpressionType.IsFalse:
				result = this.RewriteUnaryExpression(node, stack);
				break;
			default:
				throw ContractUtils.Unreachable;
			}
			return result;
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00056AA2 File Offset: 0x00054CA2
		private ParameterExpression MakeTemp(Type type)
		{
			return this._tm.Temp(type);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00056AB0 File Offset: 0x00054CB0
		private int Mark()
		{
			return this._tm.Mark();
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00056ABD File Offset: 0x00054CBD
		private void Free(int mark)
		{
			this._tm.Free(mark);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00056ACB File Offset: 0x00054CCB
		[Conditional("DEBUG")]
		private void VerifyTemps()
		{
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00056AD0 File Offset: 0x00054CD0
		private ParameterExpression ToTemp(Expression expression, out Expression save)
		{
			ParameterExpression parameterExpression = this.MakeTemp(expression.Type);
			save = Expression.Assign(parameterExpression, expression);
			return parameterExpression;
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00056AF4 File Offset: 0x00054CF4
		private static Expression MakeBlock(params Expression[] expressions)
		{
			return StackSpiller.MakeBlock(expressions);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00056AFC File Offset: 0x00054CFC
		private static Expression MakeBlock(IList<Expression> expressions)
		{
			return new SpilledExpressionBlock(expressions);
		}

		// Token: 0x04000B71 RID: 2929
		private readonly StackSpiller.TempMaker _tm = new StackSpiller.TempMaker();

		// Token: 0x04000B72 RID: 2930
		private readonly StackSpiller.Stack _startingStack;

		// Token: 0x04000B73 RID: 2931
		private StackSpiller.RewriteAction _lambdaRewrite;

		// Token: 0x04000B74 RID: 2932
		private readonly StackGuard _guard = new StackGuard();

		// Token: 0x0200045A RID: 1114
		private abstract class BindingRewriter
		{
			// Token: 0x06001FE5 RID: 8165 RVA: 0x0006F673 File Offset: 0x0006D873
			internal BindingRewriter(MemberBinding binding, StackSpiller spiller)
			{
				this._binding = binding;
				this._spiller = spiller;
			}

			// Token: 0x1700063B RID: 1595
			// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x0006F689 File Offset: 0x0006D889
			internal StackSpiller.RewriteAction Action
			{
				get
				{
					return this._action;
				}
			}

			// Token: 0x06001FE7 RID: 8167
			internal abstract MemberBinding AsBinding();

			// Token: 0x06001FE8 RID: 8168
			internal abstract Expression AsExpression(Expression target);

			// Token: 0x06001FE9 RID: 8169 RVA: 0x0006F694 File Offset: 0x0006D894
			internal static StackSpiller.BindingRewriter Create(MemberBinding binding, StackSpiller spiller, StackSpiller.Stack stack)
			{
				switch (binding.BindingType)
				{
				case MemberBindingType.Assignment:
				{
					MemberAssignment binding2 = (MemberAssignment)binding;
					return new StackSpiller.MemberAssignmentRewriter(binding2, spiller, stack);
				}
				case MemberBindingType.MemberBinding:
				{
					MemberMemberBinding binding3 = (MemberMemberBinding)binding;
					return new StackSpiller.MemberMemberBindingRewriter(binding3, spiller, stack);
				}
				case MemberBindingType.ListBinding:
				{
					MemberListBinding binding4 = (MemberListBinding)binding;
					return new StackSpiller.ListBindingRewriter(binding4, spiller, stack);
				}
				default:
					throw Error.UnhandledBinding();
				}
			}

			// Token: 0x04001304 RID: 4868
			protected MemberBinding _binding;

			// Token: 0x04001305 RID: 4869
			protected StackSpiller.RewriteAction _action;

			// Token: 0x04001306 RID: 4870
			protected StackSpiller _spiller;
		}

		// Token: 0x0200045B RID: 1115
		private class MemberMemberBindingRewriter : StackSpiller.BindingRewriter
		{
			// Token: 0x06001FEA RID: 8170 RVA: 0x0006F6F4 File Offset: 0x0006D8F4
			internal MemberMemberBindingRewriter(MemberMemberBinding binding, StackSpiller spiller, StackSpiller.Stack stack) : base(binding, spiller)
			{
				this._bindings = binding.Bindings;
				this._bindingRewriters = new StackSpiller.BindingRewriter[this._bindings.Count];
				for (int i = 0; i < this._bindings.Count; i++)
				{
					StackSpiller.BindingRewriter bindingRewriter = StackSpiller.BindingRewriter.Create(this._bindings[i], spiller, stack);
					this._action |= bindingRewriter.Action;
					this._bindingRewriters[i] = bindingRewriter;
				}
			}

			// Token: 0x06001FEB RID: 8171 RVA: 0x0006F774 File Offset: 0x0006D974
			internal override MemberBinding AsBinding()
			{
				StackSpiller.RewriteAction action = this._action;
				if (action == StackSpiller.RewriteAction.None)
				{
					return this._binding;
				}
				if (action != StackSpiller.RewriteAction.Copy)
				{
					throw ContractUtils.Unreachable;
				}
				MemberBinding[] array = new MemberBinding[this._bindings.Count];
				for (int i = 0; i < this._bindings.Count; i++)
				{
					array[i] = this._bindingRewriters[i].AsBinding();
				}
				return Expression.MemberBind(this._binding.Member, new TrueReadOnlyCollection<MemberBinding>(array));
			}

			// Token: 0x06001FEC RID: 8172 RVA: 0x0006F7EC File Offset: 0x0006D9EC
			internal override Expression AsExpression(Expression target)
			{
				if (target.Type.IsValueType && this._binding.Member is PropertyInfo)
				{
					throw Error.CannotAutoInitializeValueTypeMemberThroughProperty(this._binding.Member);
				}
				StackSpiller.RequireNotRefInstance(target);
				MemberExpression memberExpression = Expression.MakeMemberAccess(target, this._binding.Member);
				ParameterExpression parameterExpression = this._spiller.MakeTemp(memberExpression.Type);
				Expression[] array = new Expression[this._bindings.Count + 2];
				array[0] = Expression.Assign(parameterExpression, memberExpression);
				for (int i = 0; i < this._bindings.Count; i++)
				{
					StackSpiller.BindingRewriter bindingRewriter = this._bindingRewriters[i];
					array[i + 1] = bindingRewriter.AsExpression(parameterExpression);
				}
				if (parameterExpression.Type.IsValueType)
				{
					array[this._bindings.Count + 1] = Expression.Block(typeof(void), new Expression[]
					{
						Expression.Assign(Expression.MakeMemberAccess(target, this._binding.Member), parameterExpression)
					});
				}
				else
				{
					array[this._bindings.Count + 1] = Expression.Empty();
				}
				return StackSpiller.MakeBlock(array);
			}

			// Token: 0x04001307 RID: 4871
			private ReadOnlyCollection<MemberBinding> _bindings;

			// Token: 0x04001308 RID: 4872
			private StackSpiller.BindingRewriter[] _bindingRewriters;
		}

		// Token: 0x0200045C RID: 1116
		private class ListBindingRewriter : StackSpiller.BindingRewriter
		{
			// Token: 0x06001FED RID: 8173 RVA: 0x0006F904 File Offset: 0x0006DB04
			internal ListBindingRewriter(MemberListBinding binding, StackSpiller spiller, StackSpiller.Stack stack) : base(binding, spiller)
			{
				this._inits = binding.Initializers;
				this._childRewriters = new StackSpiller.ChildRewriter[this._inits.Count];
				for (int i = 0; i < this._inits.Count; i++)
				{
					ElementInit elementInit = this._inits[i];
					StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(spiller, stack, elementInit.Arguments.Count);
					childRewriter.Add(elementInit.Arguments);
					this._action |= childRewriter.Action;
					this._childRewriters[i] = childRewriter;
				}
			}

			// Token: 0x06001FEE RID: 8174 RVA: 0x0006F99C File Offset: 0x0006DB9C
			internal override MemberBinding AsBinding()
			{
				StackSpiller.RewriteAction action = this._action;
				if (action == StackSpiller.RewriteAction.None)
				{
					return this._binding;
				}
				if (action != StackSpiller.RewriteAction.Copy)
				{
					throw ContractUtils.Unreachable;
				}
				ElementInit[] array = new ElementInit[this._inits.Count];
				for (int i = 0; i < this._inits.Count; i++)
				{
					StackSpiller.ChildRewriter childRewriter = this._childRewriters[i];
					if (childRewriter.Action == StackSpiller.RewriteAction.None)
					{
						array[i] = this._inits[i];
					}
					else
					{
						array[i] = Expression.ElementInit(this._inits[i].AddMethod, childRewriter[0, -1]);
					}
				}
				return Expression.ListBind(this._binding.Member, new TrueReadOnlyCollection<ElementInit>(array));
			}

			// Token: 0x06001FEF RID: 8175 RVA: 0x0006FA4C File Offset: 0x0006DC4C
			internal override Expression AsExpression(Expression target)
			{
				if (target.Type.IsValueType && this._binding.Member is PropertyInfo)
				{
					throw Error.CannotAutoInitializeValueTypeElementThroughProperty(this._binding.Member);
				}
				StackSpiller.RequireNotRefInstance(target);
				MemberExpression memberExpression = Expression.MakeMemberAccess(target, this._binding.Member);
				ParameterExpression parameterExpression = this._spiller.MakeTemp(memberExpression.Type);
				Expression[] array = new Expression[this._inits.Count + 2];
				array[0] = Expression.Assign(parameterExpression, memberExpression);
				for (int i = 0; i < this._inits.Count; i++)
				{
					StackSpiller.ChildRewriter childRewriter = this._childRewriters[i];
					StackSpiller.Result result = childRewriter.Finish(Expression.Call(parameterExpression, this._inits[i].AddMethod, childRewriter[0, -1]));
					array[i + 1] = result.Node;
				}
				if (parameterExpression.Type.IsValueType)
				{
					array[this._inits.Count + 1] = Expression.Block(typeof(void), new Expression[]
					{
						Expression.Assign(Expression.MakeMemberAccess(target, this._binding.Member), parameterExpression)
					});
				}
				else
				{
					array[this._inits.Count + 1] = Expression.Empty();
				}
				return StackSpiller.MakeBlock(array);
			}

			// Token: 0x04001309 RID: 4873
			private ReadOnlyCollection<ElementInit> _inits;

			// Token: 0x0400130A RID: 4874
			private StackSpiller.ChildRewriter[] _childRewriters;
		}

		// Token: 0x0200045D RID: 1117
		private class MemberAssignmentRewriter : StackSpiller.BindingRewriter
		{
			// Token: 0x06001FF0 RID: 8176 RVA: 0x0006FB8C File Offset: 0x0006DD8C
			internal MemberAssignmentRewriter(MemberAssignment binding, StackSpiller spiller, StackSpiller.Stack stack) : base(binding, spiller)
			{
				StackSpiller.Result result = spiller.RewriteExpression(binding.Expression, stack);
				this._action = result.Action;
				this._rhs = result.Node;
			}

			// Token: 0x06001FF1 RID: 8177 RVA: 0x0006FBC8 File Offset: 0x0006DDC8
			internal override MemberBinding AsBinding()
			{
				StackSpiller.RewriteAction action = this._action;
				if (action == StackSpiller.RewriteAction.None)
				{
					return this._binding;
				}
				if (action != StackSpiller.RewriteAction.Copy)
				{
					throw ContractUtils.Unreachable;
				}
				return Expression.Bind(this._binding.Member, this._rhs);
			}

			// Token: 0x06001FF2 RID: 8178 RVA: 0x0006FC08 File Offset: 0x0006DE08
			internal override Expression AsExpression(Expression target)
			{
				StackSpiller.RequireNotRefInstance(target);
				MemberExpression memberExpression = Expression.MakeMemberAccess(target, this._binding.Member);
				ParameterExpression parameterExpression = this._spiller.MakeTemp(memberExpression.Type);
				return StackSpiller.MakeBlock(new Expression[]
				{
					Expression.Assign(parameterExpression, this._rhs),
					Expression.Assign(memberExpression, parameterExpression),
					Expression.Empty()
				});
			}

			// Token: 0x0400130B RID: 4875
			private Expression _rhs;
		}

		// Token: 0x0200045E RID: 1118
		private enum Stack
		{
			// Token: 0x0400130D RID: 4877
			Empty,
			// Token: 0x0400130E RID: 4878
			NonEmpty
		}

		// Token: 0x0200045F RID: 1119
		[Flags]
		private enum RewriteAction
		{
			// Token: 0x04001310 RID: 4880
			None = 0,
			// Token: 0x04001311 RID: 4881
			Copy = 1,
			// Token: 0x04001312 RID: 4882
			SpillStack = 3
		}

		// Token: 0x02000460 RID: 1120
		private struct Result
		{
			// Token: 0x06001FF3 RID: 8179 RVA: 0x0006FC6B File Offset: 0x0006DE6B
			internal Result(StackSpiller.RewriteAction action, Expression node)
			{
				this.Action = action;
				this.Node = node;
			}

			// Token: 0x04001313 RID: 4883
			internal readonly StackSpiller.RewriteAction Action;

			// Token: 0x04001314 RID: 4884
			internal readonly Expression Node;
		}

		// Token: 0x02000461 RID: 1121
		private class TempMaker
		{
			// Token: 0x1700063C RID: 1596
			// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x0006FC7B File Offset: 0x0006DE7B
			internal List<ParameterExpression> Temps
			{
				get
				{
					return this._temps;
				}
			}

			// Token: 0x06001FF5 RID: 8181 RVA: 0x0006FC84 File Offset: 0x0006DE84
			internal ParameterExpression Temp(Type type)
			{
				ParameterExpression parameterExpression;
				if (this._freeTemps != null)
				{
					for (int i = this._freeTemps.Count - 1; i >= 0; i--)
					{
						parameterExpression = this._freeTemps[i];
						if (parameterExpression.Type == type)
						{
							this._freeTemps.RemoveAt(i);
							return this.UseTemp(parameterExpression);
						}
					}
				}
				string str = "$temp$";
				int temp = this._temp;
				this._temp = temp + 1;
				parameterExpression = Expression.Variable(type, str + temp.ToString());
				this._temps.Add(parameterExpression);
				return this.UseTemp(parameterExpression);
			}

			// Token: 0x06001FF6 RID: 8182 RVA: 0x0006FD1B File Offset: 0x0006DF1B
			private ParameterExpression UseTemp(ParameterExpression temp)
			{
				if (this._usedTemps == null)
				{
					this._usedTemps = new Stack<ParameterExpression>();
				}
				this._usedTemps.Push(temp);
				return temp;
			}

			// Token: 0x06001FF7 RID: 8183 RVA: 0x0006FD3D File Offset: 0x0006DF3D
			private void FreeTemp(ParameterExpression temp)
			{
				if (this._freeTemps == null)
				{
					this._freeTemps = new List<ParameterExpression>();
				}
				this._freeTemps.Add(temp);
			}

			// Token: 0x06001FF8 RID: 8184 RVA: 0x0006FD5E File Offset: 0x0006DF5E
			internal int Mark()
			{
				if (this._usedTemps == null)
				{
					return 0;
				}
				return this._usedTemps.Count;
			}

			// Token: 0x06001FF9 RID: 8185 RVA: 0x0006FD75 File Offset: 0x0006DF75
			internal void Free(int mark)
			{
				if (this._usedTemps != null)
				{
					while (mark < this._usedTemps.Count)
					{
						this.FreeTemp(this._usedTemps.Pop());
					}
				}
			}

			// Token: 0x06001FFA RID: 8186 RVA: 0x0006FDA0 File Offset: 0x0006DFA0
			[Conditional("DEBUG")]
			internal void VerifyTemps()
			{
			}

			// Token: 0x04001315 RID: 4885
			private int _temp;

			// Token: 0x04001316 RID: 4886
			private List<ParameterExpression> _freeTemps;

			// Token: 0x04001317 RID: 4887
			private Stack<ParameterExpression> _usedTemps;

			// Token: 0x04001318 RID: 4888
			private List<ParameterExpression> _temps = new List<ParameterExpression>();
		}

		// Token: 0x02000462 RID: 1122
		private class ChildRewriter
		{
			// Token: 0x06001FFC RID: 8188 RVA: 0x0006FDB5 File Offset: 0x0006DFB5
			internal ChildRewriter(StackSpiller self, StackSpiller.Stack stack, int count)
			{
				this._self = self;
				this._stack = stack;
				this._expressions = new Expression[count];
			}

			// Token: 0x06001FFD RID: 8189 RVA: 0x0006FDD8 File Offset: 0x0006DFD8
			internal void Add(Expression node)
			{
				int expressionsCount;
				if (node == null)
				{
					Expression[] expressions = this._expressions;
					expressionsCount = this._expressionsCount;
					this._expressionsCount = expressionsCount + 1;
					expressions[expressionsCount] = null;
					return;
				}
				StackSpiller.Result result = this._self.RewriteExpression(node, this._stack);
				this._action |= result.Action;
				this._stack = StackSpiller.Stack.NonEmpty;
				Expression[] expressions2 = this._expressions;
				expressionsCount = this._expressionsCount;
				this._expressionsCount = expressionsCount + 1;
				expressions2[expressionsCount] = result.Node;
			}

			// Token: 0x06001FFE RID: 8190 RVA: 0x0006FE50 File Offset: 0x0006E050
			internal void Add(IList<Expression> expressions)
			{
				int i = 0;
				int count = expressions.Count;
				while (i < count)
				{
					this.Add(expressions[i]);
					i++;
				}
			}

			// Token: 0x06001FFF RID: 8191 RVA: 0x0006FE80 File Offset: 0x0006E080
			internal void AddArguments(IArgumentProvider expressions)
			{
				int i = 0;
				int argumentCount = expressions.ArgumentCount;
				while (i < argumentCount)
				{
					this.Add(expressions.GetArgument(i));
					i++;
				}
			}

			// Token: 0x06002000 RID: 8192 RVA: 0x0006FEB0 File Offset: 0x0006E0B0
			private void EnsureDone()
			{
				if (!this._done)
				{
					this._done = true;
					if (this._action == StackSpiller.RewriteAction.SpillStack)
					{
						Expression[] expressions = this._expressions;
						int num = expressions.Length;
						List<Expression> list = new List<Expression>(num + 1);
						for (int i = 0; i < num; i++)
						{
							if (expressions[i] != null)
							{
								Expression item;
								expressions[i] = this._self.ToTemp(expressions[i], out item);
								list.Add(item);
							}
						}
						list.Capacity = list.Count + 1;
						this._comma = list;
					}
				}
			}

			// Token: 0x1700063D RID: 1597
			// (get) Token: 0x06002001 RID: 8193 RVA: 0x0006FF2A File Offset: 0x0006E12A
			internal bool Rewrite
			{
				get
				{
					return this._action > StackSpiller.RewriteAction.None;
				}
			}

			// Token: 0x1700063E RID: 1598
			// (get) Token: 0x06002002 RID: 8194 RVA: 0x0006FF35 File Offset: 0x0006E135
			internal StackSpiller.RewriteAction Action
			{
				get
				{
					return this._action;
				}
			}

			// Token: 0x06002003 RID: 8195 RVA: 0x0006FF3D File Offset: 0x0006E13D
			internal StackSpiller.Result Finish(Expression expr)
			{
				this.EnsureDone();
				if (this._action == StackSpiller.RewriteAction.SpillStack)
				{
					this._comma.Add(expr);
					expr = StackSpiller.MakeBlock(this._comma);
				}
				return new StackSpiller.Result(this._action, expr);
			}

			// Token: 0x1700063F RID: 1599
			internal Expression this[int index]
			{
				get
				{
					this.EnsureDone();
					if (index < 0)
					{
						index += this._expressions.Length;
					}
					return this._expressions[index];
				}
			}

			// Token: 0x17000640 RID: 1600
			internal Expression[] this[int first, int last]
			{
				get
				{
					this.EnsureDone();
					if (last < 0)
					{
						last += this._expressions.Length;
					}
					int num = last - first + 1;
					ContractUtils.RequiresArrayRange<Expression>(this._expressions, first, num, "first", "last");
					if (num == this._expressions.Length)
					{
						return this._expressions;
					}
					Expression[] array = new Expression[num];
					Array.Copy(this._expressions, first, array, 0, num);
					return array;
				}
			}

			// Token: 0x04001319 RID: 4889
			private readonly StackSpiller _self;

			// Token: 0x0400131A RID: 4890
			private readonly Expression[] _expressions;

			// Token: 0x0400131B RID: 4891
			private int _expressionsCount;

			// Token: 0x0400131C RID: 4892
			private List<Expression> _comma;

			// Token: 0x0400131D RID: 4893
			private StackSpiller.RewriteAction _action;

			// Token: 0x0400131E RID: 4894
			private StackSpiller.Stack _stack;

			// Token: 0x0400131F RID: 4895
			private bool _done;
		}
	}
}
