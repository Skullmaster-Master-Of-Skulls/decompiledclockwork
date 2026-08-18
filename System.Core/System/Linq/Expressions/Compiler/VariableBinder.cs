using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x02000286 RID: 646
	internal sealed class VariableBinder : ExpressionVisitor
	{
		// Token: 0x060017E9 RID: 6121 RVA: 0x00056B14 File Offset: 0x00054D14
		internal static AnalyzedTree Bind(LambdaExpression lambda)
		{
			VariableBinder variableBinder = new VariableBinder();
			variableBinder.Visit(lambda);
			return variableBinder._tree;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00056B35 File Offset: 0x00054D35
		private VariableBinder()
		{
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00056B6C File Offset: 0x00054D6C
		public override Expression Visit(Expression node)
		{
			if (!this._guard.TryEnterOnCurrentStack())
			{
				return this._guard.RunOnEmptyStack<VariableBinder, Expression, Expression>((VariableBinder @this, Expression e) => @this.Visit(e), this, node);
			}
			return base.Visit(node);
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00056BBA File Offset: 0x00054DBA
		protected internal override Expression VisitConstant(ConstantExpression node)
		{
			if (this._inQuote)
			{
				return node;
			}
			if (ILGen.CanEmitConstant(node.Value, node.Type))
			{
				return node;
			}
			this._constants.Peek().AddReference(node.Value, node.Type);
			return node;
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00056BF8 File Offset: 0x00054DF8
		protected internal override Expression VisitUnary(UnaryExpression node)
		{
			if (node.NodeType == ExpressionType.Quote)
			{
				bool inQuote = this._inQuote;
				this._inQuote = true;
				this.Visit(node.Operand);
				this._inQuote = inQuote;
			}
			else
			{
				this.Visit(node.Operand);
			}
			return node;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00056C44 File Offset: 0x00054E44
		protected internal override Expression VisitLambda<T>(Expression<T> node)
		{
			this._scopes.Push(this._tree.Scopes[node] = new CompilerScope(node, true));
			this._constants.Push(this._tree.Constants[node] = new BoundConstants());
			base.Visit(this.MergeScopes(node));
			this._constants.Pop();
			this._scopes.Pop();
			return node;
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00056CC4 File Offset: 0x00054EC4
		protected internal override Expression VisitInvocation(InvocationExpression node)
		{
			LambdaExpression lambdaOperand = node.LambdaOperand;
			if (lambdaOperand != null)
			{
				this._scopes.Push(this._tree.Scopes[lambdaOperand] = new CompilerScope(lambdaOperand, false));
				base.Visit(this.MergeScopes(lambdaOperand));
				this._scopes.Pop();
				base.Visit(node.Arguments);
				return node;
			}
			return base.VisitInvocation(node);
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x00056D34 File Offset: 0x00054F34
		protected internal override Expression VisitBlock(BlockExpression node)
		{
			if (node.Variables.Count == 0)
			{
				base.Visit(node.Expressions);
				return node;
			}
			this._scopes.Push(this._tree.Scopes[node] = new CompilerScope(node, false));
			base.Visit(this.MergeScopes(node));
			this._scopes.Pop();
			return node;
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00056DA0 File Offset: 0x00054FA0
		protected override CatchBlock VisitCatchBlock(CatchBlock node)
		{
			if (node.Variable == null)
			{
				this.Visit(node.Body);
				return node;
			}
			this._scopes.Push(this._tree.Scopes[node] = new CompilerScope(node, false));
			this.Visit(node.Body);
			this._scopes.Pop();
			return node;
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00056E04 File Offset: 0x00055004
		private ReadOnlyCollection<Expression> MergeScopes(Expression node)
		{
			LambdaExpression lambdaExpression = node as LambdaExpression;
			ReadOnlyCollection<Expression> readOnlyCollection;
			if (lambdaExpression != null)
			{
				readOnlyCollection = new ReadOnlyCollection<Expression>(new Expression[]
				{
					lambdaExpression.Body
				});
			}
			else
			{
				readOnlyCollection = ((BlockExpression)node).Expressions;
			}
			CompilerScope compilerScope = this._scopes.Peek();
			while (readOnlyCollection.Count == 1 && readOnlyCollection[0].NodeType == ExpressionType.Block)
			{
				BlockExpression blockExpression = (BlockExpression)readOnlyCollection[0];
				if (blockExpression.Variables.Count > 0)
				{
					foreach (ParameterExpression key in blockExpression.Variables)
					{
						if (compilerScope.Definitions.ContainsKey(key))
						{
							return readOnlyCollection;
						}
					}
					if (compilerScope.MergedScopes == null)
					{
						compilerScope.MergedScopes = new Set<object>(ReferenceEqualityComparer<object>.Instance);
					}
					compilerScope.MergedScopes.Add(blockExpression);
					foreach (ParameterExpression key2 in blockExpression.Variables)
					{
						compilerScope.Definitions.Add(key2, VariableStorageKind.Local);
					}
				}
				node = blockExpression;
				readOnlyCollection = blockExpression.Expressions;
			}
			return readOnlyCollection;
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00056F5C File Offset: 0x0005515C
		protected internal override Expression VisitParameter(ParameterExpression node)
		{
			this.Reference(node, VariableStorageKind.Local);
			CompilerScope compilerScope = null;
			foreach (CompilerScope compilerScope2 in this._scopes)
			{
				if (compilerScope2.IsMethod || compilerScope2.Definitions.ContainsKey(node))
				{
					compilerScope = compilerScope2;
					break;
				}
			}
			if (compilerScope.ReferenceCount == null)
			{
				compilerScope.ReferenceCount = new Dictionary<ParameterExpression, int>();
			}
			Helpers.IncrementCount<ParameterExpression>(node, compilerScope.ReferenceCount);
			return node;
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00056FEC File Offset: 0x000551EC
		protected internal override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
		{
			foreach (ParameterExpression node2 in node.Variables)
			{
				this.Reference(node2, VariableStorageKind.Hoisted);
			}
			return node;
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0005703C File Offset: 0x0005523C
		private void Reference(ParameterExpression node, VariableStorageKind storage)
		{
			CompilerScope compilerScope = null;
			foreach (CompilerScope compilerScope2 in this._scopes)
			{
				if (compilerScope2.Definitions.ContainsKey(node))
				{
					compilerScope = compilerScope2;
					break;
				}
				compilerScope2.NeedsClosure = true;
				if (compilerScope2.IsMethod)
				{
					storage = VariableStorageKind.Hoisted;
				}
			}
			if (compilerScope == null)
			{
				throw Error.UndefinedVariable(node.Name, node.Type, this.CurrentLambdaName);
			}
			if (storage == VariableStorageKind.Hoisted)
			{
				if (node.IsByRef)
				{
					throw Error.CannotCloseOverByRef(node.Name, this.CurrentLambdaName);
				}
				compilerScope.Definitions[node] = VariableStorageKind.Hoisted;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x000570F4 File Offset: 0x000552F4
		private string CurrentLambdaName
		{
			get
			{
				foreach (CompilerScope compilerScope in this._scopes)
				{
					LambdaExpression lambdaExpression = compilerScope.Node as LambdaExpression;
					if (lambdaExpression != null)
					{
						return lambdaExpression.Name;
					}
				}
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x04000B75 RID: 2933
		private readonly AnalyzedTree _tree = new AnalyzedTree();

		// Token: 0x04000B76 RID: 2934
		private readonly Stack<CompilerScope> _scopes = new Stack<CompilerScope>();

		// Token: 0x04000B77 RID: 2935
		private readonly Stack<BoundConstants> _constants = new Stack<BoundConstants>();

		// Token: 0x04000B78 RID: 2936
		private readonly StackGuard _guard = new StackGuard();

		// Token: 0x04000B79 RID: 2937
		private bool _inQuote;
	}
}
