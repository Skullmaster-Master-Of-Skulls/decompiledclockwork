using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Entity.Util;
using System.Data.Mapping.ViewGeneration;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;
using System.Linq;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200004E RID: 78
	internal class ITreeGenerator : DbExpressionVisitor<Node>
	{
		// Token: 0x06000670 RID: 1648 RVA: 0x0001BD8C File Offset: 0x00019F8C
		private static Dictionary<DbExpressionKind, OpType> InitializeExpressionKindToOpTypeMap()
		{
			Dictionary<DbExpressionKind, OpType> dictionary = new Dictionary<DbExpressionKind, OpType>(12);
			dictionary[DbExpressionKind.Plus] = OpType.Plus;
			dictionary[DbExpressionKind.Minus] = OpType.Minus;
			dictionary[DbExpressionKind.Multiply] = OpType.Multiply;
			dictionary[DbExpressionKind.Divide] = OpType.Divide;
			dictionary[DbExpressionKind.Modulo] = OpType.Modulo;
			dictionary[DbExpressionKind.UnaryMinus] = OpType.UnaryMinus;
			dictionary[DbExpressionKind.Equals] = OpType.EQ;
			dictionary[DbExpressionKind.NotEquals] = OpType.NE;
			dictionary[DbExpressionKind.LessThan] = OpType.LT;
			dictionary[DbExpressionKind.GreaterThan] = OpType.GT;
			dictionary[DbExpressionKind.LessThanOrEquals] = OpType.LE;
			dictionary[DbExpressionKind.GreaterThanOrEquals] = OpType.GE;
			return dictionary;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001BE17 File Offset: 0x0001A017
		internal Dictionary<Node, Var> VarMap
		{
			get
			{
				return this._varMap;
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001BE1F File Offset: 0x0001A01F
		public static Command Generate(DbQueryCommandTree ctree)
		{
			return ITreeGenerator.Generate(ctree, null);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001BE28 File Offset: 0x0001A028
		internal static Command Generate(DbQueryCommandTree ctree, DiscriminatorMap discriminatorMap)
		{
			ITreeGenerator treeGenerator = new ITreeGenerator(ctree, discriminatorMap);
			return treeGenerator._iqtCommand;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001BE44 File Offset: 0x0001A044
		private ITreeGenerator(DbQueryCommandTree ctree, DiscriminatorMap discriminatorMap)
		{
			this._iqtCommand = new Command(ctree.MetadataWorkspace);
			if (discriminatorMap != null)
			{
				this._discriminatorMap = discriminatorMap;
				PlanCompiler.Assert(ctree.Query.ExpressionKind == DbExpressionKind.Project, "top level QMV expression must be project to match discriminator pattern");
				this._discriminatedViewTopProject = (DbProjectExpression)ctree.Query;
			}
			foreach (KeyValuePair<string, TypeUsage> keyValuePair in ctree.Parameters)
			{
				if (!ITreeGenerator.ValidateParameterType(keyValuePair.Value))
				{
					throw EntityUtil.NotSupported(Strings.ParameterTypeNotSupported(keyValuePair.Key, keyValuePair.Value.ToString()));
				}
				this._iqtCommand.CreateParameterVar(keyValuePair.Key, keyValuePair.Value);
			}
			this._iqtCommand.Root = this.VisitExpr(ctree.Query);
			if (!this._iqtCommand.Root.Op.IsRelOp)
			{
				Node definingExpr = this.ConvertToScalarOpTree(this._iqtCommand.Root, ctree.Query);
				Node arg = this._iqtCommand.CreateNode(this._iqtCommand.CreateSingleRowTableOp());
				Var var;
				Node node = this._iqtCommand.CreateVarDefListNode(definingExpr, out var);
				ProjectOp op = this._iqtCommand.CreateProjectOp(var);
				Node root = this._iqtCommand.CreateNode(op, arg, node);
				if (TypeSemantics.IsCollectionType(this._iqtCommand.Root.Op.Type))
				{
					UnnestOp unnestOp = this._iqtCommand.CreateUnnestOp(var);
					root = this._iqtCommand.CreateNode(unnestOp, node.Child0);
					var = unnestOp.Table.Columns[0];
				}
				this._iqtCommand.Root = root;
				this._varMap[this._iqtCommand.Root] = var;
			}
			this._iqtCommand.Root = this.CapWithPhysicalProject(this._iqtCommand.Root);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001C084 File Offset: 0x0001A284
		private static bool ValidateParameterType(TypeUsage paramType)
		{
			return paramType != null && paramType.EdmType != null && (TypeSemantics.IsPrimitiveType(paramType) || paramType.EdmType is EnumType);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001C0AB File Offset: 0x0001A2AB
		private static RowType ExtractElementRowType(TypeUsage typeUsage)
		{
			return TypeHelpers.GetEdmType<RowType>(TypeHelpers.GetEdmType<CollectionType>(typeUsage).TypeUsage);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001C0C0 File Offset: 0x0001A2C0
		private bool IsPredicate(DbExpression expr)
		{
			if (TypeSemantics.IsPrimitiveType(expr.ResultType, PrimitiveTypeKind.Boolean))
			{
				DbExpressionKind expressionKind = expr.ExpressionKind;
				if (expressionKind <= DbExpressionKind.NotEquals)
				{
					if (expressionKind > DbExpressionKind.Any)
					{
						switch (expressionKind)
						{
						case DbExpressionKind.Equals:
						case DbExpressionKind.GreaterThan:
						case DbExpressionKind.GreaterThanOrEquals:
						case DbExpressionKind.IsEmpty:
						case DbExpressionKind.IsNull:
						case DbExpressionKind.IsOf:
						case DbExpressionKind.IsOfOnly:
						case DbExpressionKind.LessThan:
						case DbExpressionKind.LessThanOrEquals:
						case DbExpressionKind.Like:
						case DbExpressionKind.Not:
						case DbExpressionKind.NotEquals:
							break;
						case DbExpressionKind.Except:
						case DbExpressionKind.Filter:
						case DbExpressionKind.FullOuterJoin:
						case DbExpressionKind.GroupBy:
						case DbExpressionKind.InnerJoin:
						case DbExpressionKind.Intersect:
						case DbExpressionKind.LeftOuterJoin:
						case DbExpressionKind.Limit:
						case DbExpressionKind.Minus:
						case DbExpressionKind.Modulo:
						case DbExpressionKind.Multiply:
						case DbExpressionKind.NewInstance:
							return false;
						case DbExpressionKind.Function:
						{
							EdmFunction function = ((DbFunctionExpression)expr).Function;
							if (!function.HasUserDefinedBody)
							{
								return false;
							}
							bool result;
							if (this._functionsIsPredicateFlag.TryGetValue(expr, out result))
							{
								return result;
							}
							PlanCompiler.Assert(false, "IsPredicate must be called on a visited function expression");
							return false;
						}
						default:
							return false;
						}
					}
				}
				else if (expressionKind != DbExpressionKind.Or)
				{
					if (expressionKind == DbExpressionKind.VariableReference)
					{
						DbVariableReferenceExpression dbVariableReferenceExpression = (DbVariableReferenceExpression)expr;
						return this.ResolveScope(dbVariableReferenceExpression).IsPredicate(dbVariableReferenceExpression.VariableName);
					}
					if (expressionKind != DbExpressionKind.Lambda)
					{
						return false;
					}
					bool result2;
					if (this._functionsIsPredicateFlag.TryGetValue(expr, out result2))
					{
						return result2;
					}
					PlanCompiler.Assert(false, "IsPredicate must be called on a visited lambda expression");
					return false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001C1E8 File Offset: 0x0001A3E8
		private Node VisitExpr(DbExpression e)
		{
			if (e == null)
			{
				return null;
			}
			return e.Accept<Node>(this);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001C1F8 File Offset: 0x0001A3F8
		private Node VisitExprAsScalar(DbExpression expr)
		{
			if (expr == null)
			{
				return null;
			}
			Node node = this.VisitExpr(expr);
			return this.ConvertToScalarOpTree(node, expr);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001C21C File Offset: 0x0001A41C
		private Node ConvertToScalarOpTree(Node node, DbExpression expr)
		{
			if (node.Op.IsRelOp)
			{
				node = this.ConvertRelOpToScalarOpTree(node, expr.ResultType);
			}
			else if (this.IsPredicate(expr))
			{
				node = this.ConvertPredicateToScalarOpTree(node, expr);
			}
			return node;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001C250 File Offset: 0x0001A450
		private Node ConvertRelOpToScalarOpTree(Node node, TypeUsage resultType)
		{
			PlanCompiler.Assert(TypeSemantics.IsCollectionType(resultType), "RelOp with non-Collection result type");
			CollectOp op = this._iqtCommand.CreateCollectOp(resultType);
			Node arg = this.CapWithPhysicalProject(node);
			node = this._iqtCommand.CreateNode(op, arg);
			return node;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001C294 File Offset: 0x0001A494
		private Node ConvertPredicateToScalarOpTree(Node node, DbExpression expr)
		{
			CaseOp op = this._iqtCommand.CreateCaseOp(this._iqtCommand.BooleanType);
			List<Node> list = new List<Node>((expr.ExpressionKind == DbExpressionKind.IsNull) ? 3 : 5);
			list.Add(node);
			list.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateInternalConstantOp(this._iqtCommand.BooleanType, true)));
			if (expr.ExpressionKind != DbExpressionKind.IsNull)
			{
				Node arg = this.VisitExpr(expr);
				list.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.Not), arg));
			}
			list.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateInternalConstantOp(this._iqtCommand.BooleanType, false)));
			if (expr.ExpressionKind != DbExpressionKind.IsNull)
			{
				list.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateNullOp(this._iqtCommand.BooleanType)));
			}
			node = this._iqtCommand.CreateNode(op, list);
			return node;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001C3A0 File Offset: 0x0001A5A0
		private Node VisitExprAsPredicate(DbExpression expr)
		{
			if (expr == null)
			{
				return null;
			}
			Node node = this.VisitExpr(expr);
			if (!this.IsPredicate(expr))
			{
				ComparisonOp op = this._iqtCommand.CreateComparisonOp(OpType.EQ);
				Node arg = this._iqtCommand.CreateNode(this._iqtCommand.CreateInternalConstantOp(this._iqtCommand.BooleanType, true));
				node = this._iqtCommand.CreateNode(op, node, arg);
			}
			else
			{
				PlanCompiler.Assert(!node.Op.IsRelOp, "unexpected relOp as predicate?");
			}
			return node;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001C424 File Offset: 0x0001A624
		private static IList<Node> VisitExpr(IList<DbExpression> exprs, ITreeGenerator.VisitExprDelegate exprDelegate)
		{
			List<Node> list = new List<Node>();
			for (int i = 0; i < exprs.Count; i++)
			{
				list.Add(exprDelegate(exprs[i]));
			}
			return list;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001C45C File Offset: 0x0001A65C
		private IList<Node> VisitExprAsScalar(IList<DbExpression> exprs)
		{
			return ITreeGenerator.VisitExpr(exprs, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001C470 File Offset: 0x0001A670
		private Node VisitUnary(DbUnaryExpression e, Op op, ITreeGenerator.VisitExprDelegate exprDelegate)
		{
			return this._iqtCommand.CreateNode(op, exprDelegate(e.Argument));
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001C48A File Offset: 0x0001A68A
		private Node VisitBinary(DbBinaryExpression e, Op op, ITreeGenerator.VisitExprDelegate exprDelegate)
		{
			return this._iqtCommand.CreateNode(op, exprDelegate(e.Left), exprDelegate(e.Right));
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001C4B0 File Offset: 0x0001A6B0
		private Node EnsureRelOp(Node inputNode)
		{
			Op op = inputNode.Op;
			if (op.IsRelOp)
			{
				return inputNode;
			}
			ScalarOp scalarOp = op as ScalarOp;
			PlanCompiler.Assert(scalarOp != null, "An expression in a CQT produced a non-ScalarOp and non-RelOp output Op");
			PlanCompiler.Assert(TypeSemantics.IsCollectionType(scalarOp.Type), "An expression used as a RelOp argument was neither a RelOp or a collection");
			CollectOp collectOp = op as CollectOp;
			if (collectOp != null)
			{
				PlanCompiler.Assert(inputNode.HasChild0, "CollectOp without argument");
				if (inputNode.Child0.Op is PhysicalProjectOp)
				{
					PlanCompiler.Assert(inputNode.Child0.HasChild0, "PhysicalProjectOp without argument");
					PlanCompiler.Assert(inputNode.Child0.Child0.Op.IsRelOp, "PhysicalProjectOp applied to non-RelOp input");
					return inputNode.Child0.Child0;
				}
			}
			Var v;
			Node arg = this._iqtCommand.CreateVarDefNode(inputNode, out v);
			UnnestOp unnestOp = this._iqtCommand.CreateUnnestOp(v);
			PlanCompiler.Assert(unnestOp.Table.Columns.Count == 1, "Unnest of collection ScalarOp produced unexpected number of columns (1 expected)");
			Node node = this._iqtCommand.CreateNode(unnestOp, arg);
			this._varMap[node] = unnestOp.Table.Columns[0];
			Node definingExpr = this._iqtCommand.CreateNode(this._iqtCommand.CreateVarRefOp(unnestOp.Table.Columns[0]));
			Var var;
			Node arg2 = this._iqtCommand.CreateVarDefListNode(definingExpr, out var);
			ProjectOp op2 = this._iqtCommand.CreateProjectOp(var);
			Node node2 = this._iqtCommand.CreateNode(op2, node, arg2);
			this._varMap[node2] = var;
			return node2;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001C640 File Offset: 0x0001A840
		private Node CapWithProject(Node input)
		{
			PlanCompiler.Assert(input.Op.IsRelOp, "unexpected non-RelOp?");
			if (input.Op.OpType == OpType.Project)
			{
				return input;
			}
			Var var = this._varMap[input];
			ProjectOp op = this._iqtCommand.CreateProjectOp(var);
			Node node = this._iqtCommand.CreateNode(op, input, this._iqtCommand.CreateNode(this._iqtCommand.CreateVarDefListOp()));
			this._varMap[node] = var;
			return node;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001C6C0 File Offset: 0x0001A8C0
		private Node CapWithPhysicalProject(Node input)
		{
			PlanCompiler.Assert(input.Op.IsRelOp, "unexpected non-RelOp?");
			Var outputVar = this._varMap[input];
			PhysicalProjectOp op = this._iqtCommand.CreatePhysicalProjectOp(outputVar);
			return this._iqtCommand.CreateNode(op, input);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001C70B File Offset: 0x0001A90B
		private Node EnterExpressionBinding(DbExpressionBinding binding)
		{
			return this.VisitBoundExpressionPushBindingScope(binding.Expression, binding.VariableName);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001C71F File Offset: 0x0001A91F
		private Node EnterGroupExpressionBinding(DbGroupExpressionBinding binding)
		{
			return this.VisitBoundExpressionPushBindingScope(binding.Expression, binding.VariableName);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001C734 File Offset: 0x0001A934
		private Node VisitBoundExpressionPushBindingScope(DbExpression boundExpression, string bindingName)
		{
			Var boundVar;
			Node result = this.VisitBoundExpression(boundExpression, out boundVar);
			this.PushBindingScope(boundVar, bindingName);
			return result;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001C754 File Offset: 0x0001A954
		private Node VisitBoundExpression(DbExpression boundExpression, out Var boundVar)
		{
			Node node = this.VisitExpr(boundExpression);
			PlanCompiler.Assert(node != null, "DbExpressionBinding.Expression produced null conversion");
			node = this.EnsureRelOp(node);
			boundVar = this._varMap[node];
			PlanCompiler.Assert(boundVar != null, "No Var found for Input Op");
			return node;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001C79D File Offset: 0x0001A99D
		private void PushBindingScope(Var boundVar, string bindingName)
		{
			this._varScopes.Push(new ITreeGenerator.ExpressionBindingScope(this._iqtCommand, bindingName, boundVar));
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001C7B8 File Offset: 0x0001A9B8
		private ITreeGenerator.ExpressionBindingScope ExitExpressionBinding()
		{
			ITreeGenerator.ExpressionBindingScope expressionBindingScope = this._varScopes.Pop() as ITreeGenerator.ExpressionBindingScope;
			PlanCompiler.Assert(expressionBindingScope != null, "ExitExpressionBinding called without ExpressionBindingScope on top of scope stack");
			return expressionBindingScope;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001C7E8 File Offset: 0x0001A9E8
		private void ExitGroupExpressionBinding()
		{
			ITreeGenerator.ExpressionBindingScope expressionBindingScope = this._varScopes.Pop() as ITreeGenerator.ExpressionBindingScope;
			PlanCompiler.Assert(expressionBindingScope != null, "ExitGroupExpressionBinding called without ExpressionBindingScope on top of scope stack");
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001C814 File Offset: 0x0001AA14
		private void EnterLambdaFunction(DbLambda lambda, List<Tuple<Node, bool>> argumentValues, EdmFunction expandingEdmFunction)
		{
			IList<DbVariableReferenceExpression> variables = lambda.Variables;
			Dictionary<string, Tuple<Node, bool>> dictionary = new Dictionary<string, Tuple<Node, bool>>();
			int num = 0;
			foreach (Tuple<Node, bool> value in argumentValues)
			{
				dictionary.Add(variables[num].VariableName, value);
				num++;
			}
			if (expandingEdmFunction != null)
			{
				if (this._functionExpansions.Contains(expandingEdmFunction))
				{
					throw EntityUtil.CommandCompilation(Strings.Cqt_UDF_FunctionDefinitionWithCircularReference(expandingEdmFunction.FullName), null);
				}
				this._functionExpansions.Push(expandingEdmFunction);
			}
			this._varScopes.Push(new ITreeGenerator.LambdaScope(this, this._iqtCommand, dictionary));
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001C8CC File Offset: 0x0001AACC
		private ITreeGenerator.LambdaScope ExitLambdaFunction(EdmFunction expandingEdmFunction)
		{
			ITreeGenerator.LambdaScope lambdaScope = this._varScopes.Pop() as ITreeGenerator.LambdaScope;
			PlanCompiler.Assert(lambdaScope != null, "ExitLambdaFunction called without LambdaScope on top of scope stack");
			if (expandingEdmFunction != null)
			{
				EdmFunction edmFunction = this._functionExpansions.Pop();
				PlanCompiler.Assert(edmFunction == expandingEdmFunction, "Function expansion stack corruption: unexpected function at the top of the stack");
			}
			return lambdaScope;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001C918 File Offset: 0x0001AB18
		private Node ProjectNewRecord(Node inputNode, RowType recType, IEnumerable<Var> colVars)
		{
			List<Node> list = new List<Node>();
			foreach (Var v in colVars)
			{
				list.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateVarRefOp(v)));
			}
			Node definingExpr = this._iqtCommand.CreateNode(this._iqtCommand.CreateNewRecordOp(recType), list);
			Var var;
			Node arg = this._iqtCommand.CreateVarDefListNode(definingExpr, out var);
			ProjectOp op = this._iqtCommand.CreateProjectOp(var);
			Node node = this._iqtCommand.CreateNode(op, inputNode, arg);
			this._varMap[node] = var;
			return node;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00017364 File Offset: 0x00015564
		public override Node Visit(DbExpression e)
		{
			throw EntityUtil.NotSupported(Strings.Cqt_General_UnsupportedExpression(e.GetType().FullName));
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001C9DC File Offset: 0x0001ABDC
		public override Node Visit(DbConstantExpression e)
		{
			ConstantBaseOp op = this._iqtCommand.CreateConstantOp(e.ResultType, e.GetValue());
			return this._iqtCommand.CreateNode(op);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001CA10 File Offset: 0x0001AC10
		public override Node Visit(DbNullExpression e)
		{
			NullOp op = this._iqtCommand.CreateNullOp(e.ResultType);
			return this._iqtCommand.CreateNode(op);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001CA3C File Offset: 0x0001AC3C
		public override Node Visit(DbVariableReferenceExpression e)
		{
			return this.ResolveScope(e)[e.VariableName];
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001CA60 File Offset: 0x0001AC60
		private ITreeGenerator.CqtVariableScope ResolveScope(DbVariableReferenceExpression e)
		{
			foreach (ITreeGenerator.CqtVariableScope cqtVariableScope in this._varScopes)
			{
				if (cqtVariableScope.Contains(e.VariableName))
				{
					return cqtVariableScope;
				}
			}
			PlanCompiler.Assert(false, "CQT VarRef could not be resolved in the variable scope stack");
			return null;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001CACC File Offset: 0x0001ACCC
		public override Node Visit(DbParameterReferenceExpression e)
		{
			Op op = this._iqtCommand.CreateVarRefOp(this._iqtCommand.GetParameter(e.ParameterName));
			return this._iqtCommand.CreateNode(op);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001CB04 File Offset: 0x0001AD04
		public override Node Visit(DbFunctionExpression e)
		{
			Node result;
			if (e.Function.IsModelDefinedFunction)
			{
				DbLambda generatedFunctionDefinition;
				try
				{
					generatedFunctionDefinition = this._iqtCommand.MetadataWorkspace.GetGeneratedFunctionDefinition(e.Function);
				}
				catch (Exception ex)
				{
					if (EntityUtil.IsCatchableExceptionType(ex))
					{
						throw EntityUtil.CommandCompilation(Strings.Cqt_UDF_FunctionDefinitionGenerationFailed(e.Function.FullName), ex);
					}
					throw;
				}
				result = this.VisitLambdaExpression(generatedFunctionDefinition, e.Arguments, e, e.Function);
			}
			else
			{
				List<Node> list = new List<Node>(e.Arguments.Count);
				for (int i = 0; i < e.Arguments.Count; i++)
				{
					list.Add(this.BuildSoftCast(this.VisitExprAsScalar(e.Arguments[i]), e.Function.Parameters[i].TypeUsage));
				}
				result = this._iqtCommand.CreateNode(this._iqtCommand.CreateFunctionOp(e.Function), list);
			}
			return result;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001CC04 File Offset: 0x0001AE04
		public override Node Visit(DbLambdaExpression e)
		{
			return this.VisitLambdaExpression(e.Lambda, e.Arguments, e, null);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001CC1C File Offset: 0x0001AE1C
		private Node VisitLambdaExpression(DbLambda lambda, IList<DbExpression> arguments, DbExpression applicationExpr, EdmFunction expandingEdmFunction)
		{
			List<Tuple<Node, bool>> list = new List<Tuple<Node, bool>>(arguments.Count);
			foreach (DbExpression dbExpression in arguments)
			{
				list.Add(Tuple.Create<Node, bool>(this.VisitExpr(dbExpression), this.IsPredicate(dbExpression)));
			}
			this.EnterLambdaFunction(lambda, list, expandingEdmFunction);
			Node result = this.VisitExpr(lambda.Body);
			this._functionsIsPredicateFlag[applicationExpr] = this.IsPredicate(lambda.Body);
			this.ExitLambdaFunction(expandingEdmFunction);
			return result;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001CCC0 File Offset: 0x0001AEC0
		private Node BuildSoftCast(Node node, TypeUsage targetType)
		{
			if (node.Op.IsRelOp)
			{
				CollectionType edmType = TypeHelpers.GetEdmType<CollectionType>(targetType);
				targetType = edmType.TypeUsage;
				Var var = this._varMap[node];
				if (Command.EqualTypes(targetType, var.Type))
				{
					return node;
				}
				Node arg = this._iqtCommand.CreateNode(this._iqtCommand.CreateVarRefOp(var));
				Node definingExpr = this._iqtCommand.CreateNode(this._iqtCommand.CreateSoftCastOp(targetType), arg);
				Var var2;
				Node arg2 = this._iqtCommand.CreateVarDefListNode(definingExpr, out var2);
				ProjectOp op = this._iqtCommand.CreateProjectOp(var2);
				Node node2 = this._iqtCommand.CreateNode(op, node, arg2);
				this._varMap[node2] = var2;
				return node2;
			}
			else
			{
				PlanCompiler.Assert(node.Op.IsScalarOp, "I want a scalar op");
				if (Command.EqualTypes(node.Op.Type, targetType))
				{
					return node;
				}
				SoftCastOp op2 = this._iqtCommand.CreateSoftCastOp(targetType);
				return this._iqtCommand.CreateNode(op2, node);
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001CDC3 File Offset: 0x0001AFC3
		private Node BuildSoftCast(Node node, EdmType targetType)
		{
			return this.BuildSoftCast(node, TypeUsage.Create(targetType));
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001CDD4 File Offset: 0x0001AFD4
		private Node BuildEntityRef(Node arg, TypeUsage entityType)
		{
			TypeUsage type = TypeHelpers.CreateReferenceTypeUsage((EntityType)entityType.EdmType);
			return this._iqtCommand.CreateNode(this._iqtCommand.CreateGetEntityRefOp(type), arg);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001CE0C File Offset: 0x0001B00C
		private bool TryRewriteKeyPropertyAccess(DbPropertyExpression propertyExpression, out DbExpression rewritten)
		{
			if (propertyExpression.Instance.ExpressionKind == DbExpressionKind.Property && Helper.IsEntityType(propertyExpression.Instance.ResultType.EdmType))
			{
				EntityType entityType = (EntityType)propertyExpression.Instance.ResultType.EdmType;
				DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)propertyExpression.Instance;
				if (Helper.IsNavigationProperty(dbPropertyExpression.Property) && entityType.KeyMembers.Contains(propertyExpression.Property))
				{
					NavigationProperty navigationProperty = (NavigationProperty)dbPropertyExpression.Property;
					DbExpression entityRef = dbPropertyExpression.Instance.GetEntityRef();
					DbExpression argument = entityRef.Navigate(navigationProperty.FromEndMember, navigationProperty.ToEndMember);
					rewritten = argument.GetRefKey();
					rewritten = rewritten.Property(propertyExpression.Property.Name);
					return true;
				}
			}
			rewritten = null;
			return false;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001CED8 File Offset: 0x0001B0D8
		public override Node Visit(DbPropertyExpression e)
		{
			if (BuiltInTypeKind.EdmProperty != e.Property.BuiltInTypeKind && e.Property.BuiltInTypeKind != BuiltInTypeKind.AssociationEndMember && BuiltInTypeKind.NavigationProperty != e.Property.BuiltInTypeKind)
			{
				throw EntityUtil.NotSupported();
			}
			PlanCompiler.Assert(e.Instance != null, "Static properties are not supported");
			DbExpression e2;
			Node node;
			if (this.TryRewriteKeyPropertyAccess(e, out e2))
			{
				node = this.VisitExpr(e2);
			}
			else
			{
				Node node2 = this.VisitExpr(e.Instance);
				if (e.Instance.ExpressionKind == DbExpressionKind.NewInstance && Helper.IsStructuralType(e.Instance.ResultType.EdmType))
				{
					IList allStructuralMembers = Helper.GetAllStructuralMembers(e.Instance.ResultType.EdmType);
					int num = -1;
					for (int i = 0; i < allStructuralMembers.Count; i++)
					{
						if (string.Equals(e.Property.Name, ((EdmMember)allStructuralMembers[i]).Name, StringComparison.Ordinal))
						{
							num = i;
							break;
						}
					}
					PlanCompiler.Assert(num > -1, "The specified property was not found");
					node = node2.Children[num];
					node = this.BuildSoftCast(node, e.ResultType);
				}
				else
				{
					Op op = this._iqtCommand.CreatePropertyOp(e.Property);
					node2 = this.BuildSoftCast(node2, e.Property.DeclaringType);
					node = this._iqtCommand.CreateNode(op, node2);
				}
			}
			return node;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001D038 File Offset: 0x0001B238
		public override Node Visit(DbComparisonExpression e)
		{
			Op op = this._iqtCommand.CreateComparisonOp(ITreeGenerator.s_opMap[e.ExpressionKind]);
			Node node = this.VisitExprAsScalar(e.Left);
			Node node2 = this.VisitExprAsScalar(e.Right);
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(e.Left.ResultType, e.Right.ResultType);
			if (!Command.EqualTypes(e.Left.ResultType, e.Right.ResultType))
			{
				node = this.BuildSoftCast(node, commonTypeUsage);
				node2 = this.BuildSoftCast(node2, commonTypeUsage);
			}
			if (TypeSemantics.IsEntityType(commonTypeUsage) && (e.ExpressionKind == DbExpressionKind.Equals || e.ExpressionKind == DbExpressionKind.NotEquals))
			{
				node = this.BuildEntityRef(node, commonTypeUsage);
				node2 = this.BuildEntityRef(node2, commonTypeUsage);
			}
			return this._iqtCommand.CreateNode(op, node, node2);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001D102 File Offset: 0x0001B302
		public override Node Visit(DbLikeExpression e)
		{
			return this._iqtCommand.CreateNode(this._iqtCommand.CreateLikeOp(), this.VisitExpr(e.Argument), this.VisitExpr(e.Pattern), this.VisitExpr(e.Escape));
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001D140 File Offset: 0x0001B340
		private Node CreateLimitNode(Node inputNode, Node limitNode, bool withTies)
		{
			Node result;
			if (OpType.ConstrainedSort == inputNode.Op.OpType && OpType.Null == inputNode.Child2.Op.OpType)
			{
				inputNode.Child2 = limitNode;
				if (withTies)
				{
					((ConstrainedSortOp)inputNode.Op).WithTies = true;
				}
				result = inputNode;
			}
			else if (OpType.Sort == inputNode.Op.OpType)
			{
				result = this._iqtCommand.CreateNode(this._iqtCommand.CreateConstrainedSortOp(((SortOp)inputNode.Op).Keys, withTies), inputNode.Child0, this._iqtCommand.CreateNode(this._iqtCommand.CreateNullOp(this._iqtCommand.IntegerType)), limitNode);
			}
			else
			{
				result = this._iqtCommand.CreateNode(this._iqtCommand.CreateConstrainedSortOp(new List<SortKey>(), withTies), inputNode, this._iqtCommand.CreateNode(this._iqtCommand.CreateNullOp(this._iqtCommand.IntegerType)), limitNode);
			}
			return result;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001D238 File Offset: 0x0001B438
		public override Node Visit(DbLimitExpression expression)
		{
			Node node = this.EnsureRelOp(this.VisitExpr(expression.Argument));
			Var value = this._varMap[node];
			Node limitNode = this.VisitExprAsScalar(expression.Limit);
			Node node2;
			if (OpType.Project == node.Op.OpType && (!AppSettings.SimplifyLimitOperations || OpType.Sort == node.Child0.Op.OpType || OpType.ConstrainedSort == node.Child0.Op.OpType))
			{
				node.Child0 = this.CreateLimitNode(node.Child0, limitNode, expression.WithTies);
				node2 = node;
			}
			else
			{
				node2 = this.CreateLimitNode(node, limitNode, expression.WithTies);
			}
			if (node2 != node)
			{
				this._varMap[node2] = value;
			}
			return node2;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001D2F0 File Offset: 0x0001B4F0
		public override Node Visit(DbIsNullExpression e)
		{
			bool flag = false;
			if (e.Argument.ExpressionKind == DbExpressionKind.IsNull)
			{
				flag = true;
			}
			else if (e.Argument.ExpressionKind == DbExpressionKind.Not)
			{
				DbNotExpression dbNotExpression = (DbNotExpression)e.Argument;
				if (dbNotExpression.Argument.ExpressionKind == DbExpressionKind.IsNull)
				{
					flag = true;
				}
			}
			Op op = this._iqtCommand.CreateConditionalOp(OpType.IsNull);
			if (flag)
			{
				return this._iqtCommand.CreateNode(op, this._iqtCommand.CreateNode(this._iqtCommand.CreateInternalConstantOp(this._iqtCommand.BooleanType, true)));
			}
			Node node = this.VisitExprAsScalar(e.Argument);
			if (TypeSemantics.IsEntityType(e.Argument.ResultType))
			{
				node = this.BuildEntityRef(node, e.Argument.ResultType);
			}
			return this._iqtCommand.CreateNode(op, node);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001D3C4 File Offset: 0x0001B5C4
		public override Node Visit(DbArithmeticExpression e)
		{
			Op op = this._iqtCommand.CreateArithmeticOp(ITreeGenerator.s_opMap[e.ExpressionKind], e.ResultType);
			List<Node> list = new List<Node>();
			foreach (DbExpression expr in e.Arguments)
			{
				Node node = this.VisitExprAsScalar(expr);
				list.Add(this.BuildSoftCast(node, e.ResultType));
			}
			return this._iqtCommand.CreateNode(op, list);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001D45C File Offset: 0x0001B65C
		public override Node Visit(DbAndExpression e)
		{
			Op op = this._iqtCommand.CreateConditionalOp(OpType.And);
			return this.VisitBinary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsPredicate));
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001D48C File Offset: 0x0001B68C
		public override Node Visit(DbOrExpression e)
		{
			Op op = this._iqtCommand.CreateConditionalOp(OpType.Or);
			return this.VisitBinary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsPredicate));
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001D4BC File Offset: 0x0001B6BC
		public override Node Visit(DbNotExpression e)
		{
			Op op = this._iqtCommand.CreateConditionalOp(OpType.Not);
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsPredicate));
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001D4EC File Offset: 0x0001B6EC
		public override Node Visit(DbDistinctExpression e)
		{
			Node node = this.EnsureRelOp(this.VisitExpr(e.Argument));
			Var var = this._varMap[node];
			Op op = this._iqtCommand.CreateDistinctOp(var);
			Node node2 = this._iqtCommand.CreateNode(op, node);
			this._varMap[node2] = var;
			return node2;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001D544 File Offset: 0x0001B744
		public override Node Visit(DbElementExpression e)
		{
			Op op = this._iqtCommand.CreateElementOp(e.ResultType);
			Node node = this.EnsureRelOp(this.VisitExpr(e.Argument));
			node = this.BuildSoftCast(node, TypeHelpers.CreateCollectionTypeUsage(e.ResultType));
			Var value = this._varMap[node];
			node = this._iqtCommand.CreateNode(this._iqtCommand.CreateSingleRowOp(), node);
			this._varMap[node] = value;
			node = this.CapWithProject(node);
			return this._iqtCommand.CreateNode(op, node);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001D5D0 File Offset: 0x0001B7D0
		public override Node Visit(DbIsEmptyExpression e)
		{
			Op op = this._iqtCommand.CreateExistsOp();
			Node arg = this.EnsureRelOp(this.VisitExpr(e.Argument));
			return this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.Not), this._iqtCommand.CreateNode(op, arg));
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001D624 File Offset: 0x0001B824
		private Node VisitSetOpExpression(DbBinaryExpression expression)
		{
			PlanCompiler.Assert(DbExpressionKind.Except == expression.ExpressionKind || DbExpressionKind.Intersect == expression.ExpressionKind || DbExpressionKind.UnionAll == expression.ExpressionKind, "Non-SetOp DbExpression used as argument to VisitSetOpExpression");
			PlanCompiler.Assert(TypeSemantics.IsCollectionType(expression.ResultType), "SetOp DbExpression does not have collection result type?");
			Node node = this.EnsureRelOp(this.VisitExpr(expression.Left));
			Node node2 = this.EnsureRelOp(this.VisitExpr(expression.Right));
			node = this.BuildSoftCast(node, expression.ResultType);
			node2 = this.BuildSoftCast(node2, expression.ResultType);
			Var var = this._iqtCommand.CreateSetOpVar(TypeHelpers.GetEdmType<CollectionType>(expression.ResultType).TypeUsage);
			VarMap varMap = new VarMap();
			varMap.Add(var, this._varMap[node]);
			VarMap varMap2 = new VarMap();
			varMap2.Add(var, this._varMap[node2]);
			Op op = null;
			DbExpressionKind expressionKind = expression.ExpressionKind;
			if (expressionKind != DbExpressionKind.Except)
			{
				if (expressionKind != DbExpressionKind.Intersect)
				{
					if (expressionKind == DbExpressionKind.UnionAll)
					{
						op = this._iqtCommand.CreateUnionAllOp(varMap, varMap2);
					}
				}
				else
				{
					op = this._iqtCommand.CreateIntersectOp(varMap, varMap2);
				}
			}
			else
			{
				op = this._iqtCommand.CreateExceptOp(varMap, varMap2);
			}
			Node node3 = this._iqtCommand.CreateNode(op, node, node2);
			this._varMap[node3] = var;
			return node3;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001D777 File Offset: 0x0001B977
		public override Node Visit(DbUnionAllExpression e)
		{
			return this.VisitSetOpExpression(e);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001D777 File Offset: 0x0001B977
		public override Node Visit(DbIntersectExpression e)
		{
			return this.VisitSetOpExpression(e);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001D777 File Offset: 0x0001B977
		public override Node Visit(DbExceptExpression e)
		{
			return this.VisitSetOpExpression(e);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001D780 File Offset: 0x0001B980
		public override Node Visit(DbTreatExpression e)
		{
			Op op;
			if (this._fakeTreats.Contains(e))
			{
				op = this._iqtCommand.CreateFakeTreatOp(e.ResultType);
			}
			else
			{
				op = this._iqtCommand.CreateTreatOp(e.ResultType);
			}
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001D7D8 File Offset: 0x0001B9D8
		public override Node Visit(DbIsOfExpression e)
		{
			Op op;
			if (DbExpressionKind.IsOfOnly == e.ExpressionKind)
			{
				op = this._iqtCommand.CreateIsOfOnlyOp(e.OfType);
			}
			else
			{
				op = this._iqtCommand.CreateIsOfOp(e.OfType);
			}
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001D82C File Offset: 0x0001BA2C
		public override Node Visit(DbCastExpression e)
		{
			Op op = this._iqtCommand.CreateCastOp(e.ResultType);
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001D860 File Offset: 0x0001BA60
		public override Node Visit(DbCaseExpression e)
		{
			List<Node> list = new List<Node>();
			for (int i = 0; i < e.When.Count; i++)
			{
				list.Add(this.VisitExprAsPredicate(e.When[i]));
				list.Add(this.BuildSoftCast(this.VisitExprAsScalar(e.Then[i]), e.ResultType));
			}
			list.Add(this.BuildSoftCast(this.VisitExprAsScalar(e.Else), e.ResultType));
			return this._iqtCommand.CreateNode(this._iqtCommand.CreateCaseOp(e.ResultType), list);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001D900 File Offset: 0x0001BB00
		private DbFilterExpression CreateIsOfFilterExpression(DbExpression input, ITreeGenerator.IsOfFilter typeFilter)
		{
			DbExpressionBinding resultBinding = input.Bind();
			List<DbExpression> nodes = new List<DbExpression>(typeFilter.ToEnumerable().Select(delegate(KeyValuePair<TypeUsage, bool> tf)
			{
				if (!tf.Value)
				{
					return resultBinding.Variable.IsOf(tf.Key);
				}
				return resultBinding.Variable.IsOfOnly(tf.Key);
			}).ToList<DbIsOfExpression>());
			DbExpression predicate = Helpers.BuildBalancedTreeInPlace<DbExpression>(nodes, (DbExpression left, DbExpression right) => left.And(right));
			DbFilterExpression dbFilterExpression = resultBinding.Filter(predicate);
			this._processedIsOfFilters.Add(dbFilterExpression);
			return dbFilterExpression;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001D984 File Offset: 0x0001BB84
		private bool IsIsOfFilter(DbFilterExpression filter)
		{
			if (filter.Predicate.ExpressionKind != DbExpressionKind.IsOf && filter.Predicate.ExpressionKind != DbExpressionKind.IsOfOnly)
			{
				return false;
			}
			DbExpression argument = ((DbIsOfExpression)filter.Predicate).Argument;
			return argument.ExpressionKind == DbExpressionKind.VariableReference && ((DbVariableReferenceExpression)argument).VariableName == filter.Input.VariableName;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001D9EC File Offset: 0x0001BBEC
		private DbExpression ApplyIsOfFilter(DbExpression current, ITreeGenerator.IsOfFilter typeFilter)
		{
			DbExpressionKind expressionKind = current.ExpressionKind;
			if (expressionKind <= DbExpressionKind.Filter)
			{
				if (expressionKind == DbExpressionKind.Distinct)
				{
					return this.ApplyIsOfFilter(((DbDistinctExpression)current).Argument, typeFilter).Distinct();
				}
				if (expressionKind == DbExpressionKind.Filter)
				{
					DbFilterExpression dbFilterExpression = (DbFilterExpression)current;
					if (this.IsIsOfFilter(dbFilterExpression))
					{
						DbIsOfExpression other = (DbIsOfExpression)dbFilterExpression.Predicate;
						typeFilter = typeFilter.Merge(other);
						return this.ApplyIsOfFilter(dbFilterExpression.Input.Expression, typeFilter);
					}
					DbExpression input = this.ApplyIsOfFilter(dbFilterExpression.Input.Expression, typeFilter);
					return input.BindAs(dbFilterExpression.Input.VariableName).Filter(dbFilterExpression.Predicate);
				}
			}
			else
			{
				if (expressionKind - DbExpressionKind.OfType <= 1)
				{
					DbOfTypeExpression dbOfTypeExpression = (DbOfTypeExpression)current;
					typeFilter = typeFilter.Merge(dbOfTypeExpression);
					DbExpression input2 = this.ApplyIsOfFilter(dbOfTypeExpression.Argument, typeFilter);
					DbExpressionBinding dbExpressionBinding = input2.Bind();
					DbTreatExpression dbTreatExpression = dbExpressionBinding.Variable.TreatAs(dbOfTypeExpression.OfType);
					this._fakeTreats.Add(dbTreatExpression);
					return dbExpressionBinding.Project(dbTreatExpression);
				}
				if (expressionKind != DbExpressionKind.Project)
				{
					if (expressionKind == DbExpressionKind.Sort)
					{
						DbSortExpression dbSortExpression = (DbSortExpression)current;
						DbExpression input3 = this.ApplyIsOfFilter(dbSortExpression.Input.Expression, typeFilter);
						return input3.BindAs(dbSortExpression.Input.VariableName).Sort(dbSortExpression.SortOrder);
					}
				}
				else
				{
					DbProjectExpression dbProjectExpression = (DbProjectExpression)current;
					if (dbProjectExpression.Projection.ExpressionKind == DbExpressionKind.VariableReference && ((DbVariableReferenceExpression)dbProjectExpression.Projection).VariableName == dbProjectExpression.Input.VariableName)
					{
						return this.ApplyIsOfFilter(dbProjectExpression.Input.Expression, typeFilter);
					}
					return this.CreateIsOfFilterExpression(current, typeFilter);
				}
			}
			return this.CreateIsOfFilterExpression(current, typeFilter);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001DBC8 File Offset: 0x0001BDC8
		public override Node Visit(DbOfTypeExpression e)
		{
			PlanCompiler.Assert(TypeSemantics.IsCollectionType(e.Argument.ResultType), "Non-Collection Type Argument in DbOfTypeExpression");
			DbExpression e2 = this.ApplyIsOfFilter(e.Argument, new ITreeGenerator.IsOfFilter(e));
			Node node = this.EnsureRelOp(this.VisitExpr(e2));
			Var inputVar = this._varMap[node];
			Var value;
			Node node2 = this._iqtCommand.BuildFakeTreatProject(node, inputVar, e.OfType, out value);
			this._varMap[node2] = value;
			return node2;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001DC44 File Offset: 0x0001BE44
		public override Node Visit(DbNewInstanceExpression e)
		{
			Op op = null;
			List<Node> list = null;
			if (TypeSemantics.IsCollectionType(e.ResultType))
			{
				op = this._iqtCommand.CreateNewMultisetOp(e.ResultType);
			}
			else if (TypeSemantics.IsRowType(e.ResultType))
			{
				op = this._iqtCommand.CreateNewRecordOp(e.ResultType);
			}
			else if (TypeSemantics.IsEntityType(e.ResultType))
			{
				List<RelProperty> list2 = new List<RelProperty>();
				list = new List<Node>();
				if (e.HasRelatedEntityReferences)
				{
					foreach (DbRelatedEntityRef dbRelatedEntityRef in e.RelatedEntityReferences)
					{
						RelProperty item = new RelProperty((RelationshipType)dbRelatedEntityRef.TargetEnd.DeclaringType, dbRelatedEntityRef.SourceEnd, dbRelatedEntityRef.TargetEnd);
						list2.Add(item);
						Node item2 = this.VisitExprAsScalar(dbRelatedEntityRef.TargetEntityReference);
						list.Add(item2);
					}
				}
				op = this._iqtCommand.CreateNewEntityOp(e.ResultType, list2);
			}
			else
			{
				op = this._iqtCommand.CreateNewInstanceOp(e.ResultType);
			}
			List<Node> list3 = new List<Node>();
			if (TypeSemantics.IsStructuralType(e.ResultType))
			{
				StructuralType edmType = TypeHelpers.GetEdmType<StructuralType>(e.ResultType);
				int num = 0;
				using (IEnumerator enumerator2 = TypeHelpers.GetAllStructuralMembers(edmType).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						object obj = enumerator2.Current;
						EdmMember member = (EdmMember)obj;
						Node item3 = this.BuildSoftCast(this.VisitExprAsScalar(e.Arguments[num]), Helper.GetModelTypeUsage(member));
						list3.Add(item3);
						num++;
					}
					goto IL_1FE;
				}
			}
			CollectionType edmType2 = TypeHelpers.GetEdmType<CollectionType>(e.ResultType);
			TypeUsage typeUsage = edmType2.TypeUsage;
			foreach (DbExpression expr in e.Arguments)
			{
				Node item4 = this.BuildSoftCast(this.VisitExprAsScalar(expr), typeUsage);
				list3.Add(item4);
			}
			IL_1FE:
			if (list != null)
			{
				list3.AddRange(list);
			}
			return this._iqtCommand.CreateNode(op, list3);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001DE90 File Offset: 0x0001C090
		public override Node Visit(DbRefExpression e)
		{
			Op op = this._iqtCommand.CreateRefOp(e.EntitySet, e.ResultType);
			Node arg = this.BuildSoftCast(this.VisitExprAsScalar(e.Argument), TypeHelpers.CreateKeyRowType(e.EntitySet.ElementType));
			return this._iqtCommand.CreateNode(op, arg);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001DEE8 File Offset: 0x0001C0E8
		public override Node Visit(DbRelationshipNavigationExpression e)
		{
			RelProperty relProperty = new RelProperty(e.Relationship, e.NavigateFrom, e.NavigateTo);
			Op op = this._iqtCommand.CreateNavigateOp(e.ResultType, relProperty);
			Node arg = this.VisitExprAsScalar(e.NavigationSource);
			return this._iqtCommand.CreateNode(op, arg);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001DF3C File Offset: 0x0001C13C
		public override Node Visit(DbDerefExpression e)
		{
			Op op = this._iqtCommand.CreateDerefOp(e.ResultType);
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001DF70 File Offset: 0x0001C170
		public override Node Visit(DbRefKeyExpression e)
		{
			Op op = this._iqtCommand.CreateGetRefKeyOp(e.ResultType);
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001DFA4 File Offset: 0x0001C1A4
		public override Node Visit(DbEntityRefExpression e)
		{
			Op op = this._iqtCommand.CreateGetEntityRefOp(e.ResultType);
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001DFD8 File Offset: 0x0001C1D8
		public override Node Visit(DbScanExpression e)
		{
			TableMD tableMetadata = Command.CreateTableDefinition(e.Target);
			ScanTableOp scanTableOp = this._iqtCommand.CreateScanTableOp(tableMetadata);
			Node node = this._iqtCommand.CreateNode(scanTableOp);
			Var value = scanTableOp.Table.Columns[0];
			this._varMap[node] = value;
			return node;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001E02C File Offset: 0x0001C22C
		public override Node Visit(DbFilterExpression e)
		{
			if (!this.IsIsOfFilter(e) || this._processedIsOfFilters.Contains(e))
			{
				Node node = this.EnterExpressionBinding(e.Input);
				Node arg = this.VisitExprAsPredicate(e.Predicate);
				this.ExitExpressionBinding();
				Op op = this._iqtCommand.CreateFilterOp();
				Node node2 = this._iqtCommand.CreateNode(op, node, arg);
				this._varMap[node2] = this._varMap[node];
				return node2;
			}
			DbIsOfExpression template = (DbIsOfExpression)e.Predicate;
			DbExpression e2 = this.ApplyIsOfFilter(e.Input.Expression, new ITreeGenerator.IsOfFilter(template));
			return this.VisitExpr(e2);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001E0D5 File Offset: 0x0001C2D5
		public override Node Visit(DbProjectExpression e)
		{
			if (e == this._discriminatedViewTopProject)
			{
				return this.GenerateDiscriminatedProject(e);
			}
			return this.GenerateStandardProject(e);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001E0F0 File Offset: 0x0001C2F0
		private Node GenerateDiscriminatedProject(DbProjectExpression e)
		{
			PlanCompiler.Assert(this._discriminatedViewTopProject != null, "if a project matches the pattern, there must be a corresponding discriminator map");
			Node arg = this.EnterExpressionBinding(e.Input);
			List<RelProperty> list = new List<RelProperty>();
			List<Node> list2 = new List<Node>();
			foreach (KeyValuePair<RelProperty, DbExpression> keyValuePair in this._discriminatorMap.RelPropertyMap)
			{
				list.Add(keyValuePair.Key);
				list2.Add(this.VisitExprAsScalar(keyValuePair.Value));
			}
			DiscriminatedNewEntityOp op = this._iqtCommand.CreateDiscriminatedNewEntityOp(e.Projection.ResultType, new ExplicitDiscriminatorMap(this._discriminatorMap), this._discriminatorMap.EntitySet, list);
			List<Node> list3 = new List<Node>(this._discriminatorMap.PropertyMap.Count + 1);
			list3.Add(this.CreateNewInstanceArgument(this._discriminatorMap.Discriminator.Property, this._discriminatorMap.Discriminator));
			foreach (KeyValuePair<EdmProperty, DbExpression> keyValuePair2 in this._discriminatorMap.PropertyMap)
			{
				DbExpression value = keyValuePair2.Value;
				EdmProperty key = keyValuePair2.Key;
				Node item = this.CreateNewInstanceArgument(key, value);
				list3.Add(item);
			}
			list3.AddRange(list2);
			Node definingExpr = this._iqtCommand.CreateNode(op, list3);
			this.ExitExpressionBinding();
			Var var;
			Node arg2 = this._iqtCommand.CreateVarDefListNode(definingExpr, out var);
			ProjectOp op2 = this._iqtCommand.CreateProjectOp(var);
			Node node = this._iqtCommand.CreateNode(op2, arg, arg2);
			this._varMap[node] = var;
			return node;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001E2C8 File Offset: 0x0001C4C8
		private Node CreateNewInstanceArgument(EdmMember property, DbExpression value)
		{
			return this.BuildSoftCast(this.VisitExprAsScalar(value), Helper.GetModelTypeUsage(property));
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001E2EC File Offset: 0x0001C4EC
		private Node GenerateStandardProject(DbProjectExpression e)
		{
			Node arg = this.EnterExpressionBinding(e.Input);
			Node definingExpr = this.VisitExprAsScalar(e.Projection);
			this.ExitExpressionBinding();
			Var var;
			Node arg2 = this._iqtCommand.CreateVarDefListNode(definingExpr, out var);
			ProjectOp op = this._iqtCommand.CreateProjectOp(var);
			Node node = this._iqtCommand.CreateNode(op, arg, arg2);
			this._varMap[node] = var;
			return node;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001E358 File Offset: 0x0001C558
		public override Node Visit(DbCrossJoinExpression e)
		{
			return this.VisitJoin(e, e.Inputs, null);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001E368 File Offset: 0x0001C568
		public override Node Visit(DbJoinExpression e)
		{
			return this.VisitJoin(e, new List<DbExpressionBinding>
			{
				e.Left,
				e.Right
			}, e.JoinCondition);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001E3A4 File Offset: 0x0001C5A4
		private Node VisitJoin(DbExpression e, IList<DbExpressionBinding> inputs, DbExpression joinCond)
		{
			PlanCompiler.Assert(DbExpressionKind.CrossJoin == e.ExpressionKind || DbExpressionKind.InnerJoin == e.ExpressionKind || DbExpressionKind.LeftOuterJoin == e.ExpressionKind || DbExpressionKind.FullOuterJoin == e.ExpressionKind, "Unrecognized JoinType specified in DbJoinExpression");
			List<Node> list = new List<Node>();
			List<Var> list2 = new List<Var>();
			for (int i = 0; i < inputs.Count; i++)
			{
				Var item2;
				Node item = this.VisitBoundExpression(inputs[i].Expression, out item2);
				list.Add(item);
				list2.Add(item2);
			}
			for (int j = 0; j < list.Count; j++)
			{
				this.PushBindingScope(list2[j], inputs[j].VariableName);
			}
			Node node = this.VisitExprAsPredicate(joinCond);
			for (int k = 0; k < list.Count; k++)
			{
				this.ExitExpressionBinding();
			}
			JoinBaseOp joinBaseOp = null;
			DbExpressionKind expressionKind = e.ExpressionKind;
			if (expressionKind <= DbExpressionKind.FullOuterJoin)
			{
				if (expressionKind != DbExpressionKind.CrossJoin)
				{
					if (expressionKind == DbExpressionKind.FullOuterJoin)
					{
						joinBaseOp = this._iqtCommand.CreateFullOuterJoinOp();
					}
				}
				else
				{
					joinBaseOp = this._iqtCommand.CreateCrossJoinOp();
				}
			}
			else if (expressionKind != DbExpressionKind.InnerJoin)
			{
				if (expressionKind == DbExpressionKind.LeftOuterJoin)
				{
					joinBaseOp = this._iqtCommand.CreateLeftOuterJoinOp();
				}
			}
			else
			{
				joinBaseOp = this._iqtCommand.CreateInnerJoinOp();
			}
			PlanCompiler.Assert(joinBaseOp != null, "Unrecognized JoinOp specified in DbJoinExpression, no JoinOp was produced");
			if (e.ExpressionKind != DbExpressionKind.CrossJoin)
			{
				PlanCompiler.Assert(node != null, "Non CrossJoinOps must specify a join condition");
				list.Add(node);
			}
			return this.ProjectNewRecord(this._iqtCommand.CreateNode(joinBaseOp, list), ITreeGenerator.ExtractElementRowType(e.ResultType), list2);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001E530 File Offset: 0x0001C730
		public override Node Visit(DbApplyExpression e)
		{
			Node node = this.EnterExpressionBinding(e.Input);
			Node node2 = this.EnterExpressionBinding(e.Apply);
			this.ExitExpressionBinding();
			this.ExitExpressionBinding();
			PlanCompiler.Assert(DbExpressionKind.CrossApply == e.ExpressionKind || DbExpressionKind.OuterApply == e.ExpressionKind, "Unrecognized DbExpressionKind specified in DbApplyExpression");
			ApplyBaseOp op;
			if (DbExpressionKind.CrossApply == e.ExpressionKind)
			{
				op = this._iqtCommand.CreateCrossApplyOp();
			}
			else
			{
				op = this._iqtCommand.CreateOuterApplyOp();
			}
			Node inputNode = this._iqtCommand.CreateNode(op, node, node2);
			return this.ProjectNewRecord(inputNode, ITreeGenerator.ExtractElementRowType(e.ResultType), new Var[]
			{
				this._varMap[node],
				this._varMap[node2]
			});
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001E5F0 File Offset: 0x0001C7F0
		public override Node Visit(DbGroupByExpression e)
		{
			VarVec varVec = this._iqtCommand.CreateVarVec();
			VarVec varVec2 = this._iqtCommand.CreateVarVec();
			Node node;
			List<Node> list;
			ITreeGenerator.ExpressionBindingScope expressionBindingScope;
			this.ExtractKeys(e, varVec, varVec2, out node, out list, out expressionBindingScope);
			int num = -1;
			for (int i = 0; i < e.Aggregates.Count; i++)
			{
				if (e.Aggregates[i].GetType() == typeof(DbGroupAggregate))
				{
					num = i;
					break;
				}
			}
			Node copyOfInput = null;
			List<Node> copyOfkeyVarDefNodes = null;
			VarVec outputVarSet = this._iqtCommand.CreateVarVec();
			VarVec varVec3 = this._iqtCommand.CreateVarVec();
			if (num >= 0)
			{
				ITreeGenerator.ExpressionBindingScope expressionBindingScope2;
				this.ExtractKeys(e, varVec3, outputVarSet, out copyOfInput, out copyOfkeyVarDefNodes, out expressionBindingScope2);
			}
			expressionBindingScope = new ITreeGenerator.ExpressionBindingScope(this._iqtCommand, e.Input.GroupVariableName, expressionBindingScope.ScopeVar);
			this._varScopes.Push(expressionBindingScope);
			List<Node> list2 = new List<Node>();
			Node arg = null;
			for (int j = 0; j < e.Aggregates.Count; j++)
			{
				DbAggregate dbAggregate = e.Aggregates[j];
				IList<Node> argNodes = this.VisitExprAsScalar(dbAggregate.Arguments);
				Var v;
				if (j != num)
				{
					DbFunctionAggregate dbFunctionAggregate = dbAggregate as DbFunctionAggregate;
					PlanCompiler.Assert(dbFunctionAggregate != null, "Unrecognized DbAggregate used in DbGroupByExpression");
					list2.Add(this.ProcessFunctionAggregate(dbFunctionAggregate, argNodes, out v));
				}
				else
				{
					arg = this.ProcessGroupAggregate(list, copyOfInput, copyOfkeyVarDefNodes, varVec3, e.Input.Expression.ResultType, out v);
				}
				varVec2.Set(v);
			}
			this.ExitGroupExpressionBinding();
			List<Node> list3 = new List<Node>();
			list3.Add(node);
			list3.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateVarDefListOp(), list));
			list3.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateVarDefListOp(), list2));
			GroupByBaseOp op;
			if (num >= 0)
			{
				list3.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateVarDefListOp(), arg));
				op = this._iqtCommand.CreateGroupByIntoOp(varVec, this._iqtCommand.CreateVarVec(this._varMap[node]), varVec2);
			}
			else
			{
				op = this._iqtCommand.CreateGroupByOp(varVec, varVec2);
			}
			Node inputNode = this._iqtCommand.CreateNode(op, list3);
			return this.ProjectNewRecord(inputNode, ITreeGenerator.ExtractElementRowType(e.ResultType), varVec2);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001E844 File Offset: 0x0001CA44
		private void ExtractKeys(DbGroupByExpression e, VarVec keyVarSet, VarVec outputVarSet, out Node inputNode, out List<Node> keyVarDefNodes, out ITreeGenerator.ExpressionBindingScope scope)
		{
			inputNode = this.EnterGroupExpressionBinding(e.Input);
			keyVarDefNodes = new List<Node>();
			for (int i = 0; i < e.Keys.Count; i++)
			{
				DbExpression expr = e.Keys[i];
				Node node = this.VisitExprAsScalar(expr);
				ScalarOp scalarOp = node.Op as ScalarOp;
				PlanCompiler.Assert(scalarOp != null, "GroupBy Key is not a ScalarOp");
				Var v;
				keyVarDefNodes.Add(this._iqtCommand.CreateVarDefNode(node, out v));
				outputVarSet.Set(v);
				keyVarSet.Set(v);
			}
			scope = this.ExitExpressionBinding();
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001E8DC File Offset: 0x0001CADC
		private Node ProcessFunctionAggregate(DbFunctionAggregate funcAgg, IList<Node> argNodes, out Var aggVar)
		{
			Node definingExpr = this._iqtCommand.CreateNode(this._iqtCommand.CreateAggregateOp(funcAgg.Function, funcAgg.Distinct), argNodes);
			return this._iqtCommand.CreateVarDefNode(definingExpr, out aggVar);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001E91C File Offset: 0x0001CB1C
		private Node ProcessGroupAggregate(List<Node> keyVarDefNodes, Node copyOfInput, List<Node> copyOfkeyVarDefNodes, VarVec copyKeyVarSet, TypeUsage inputResultType, out Var groupAggVar)
		{
			Var var = this._varMap[copyOfInput];
			Node node = copyOfInput;
			if (keyVarDefNodes.Count > 0)
			{
				VarVec varVec = this._iqtCommand.CreateVarVec();
				varVec.Set(var);
				varVec.Or(copyKeyVarSet);
				Node arg = this._iqtCommand.CreateNode(this._iqtCommand.CreateProjectOp(varVec), node, this._iqtCommand.CreateNode(this._iqtCommand.CreateVarDefListOp(), copyOfkeyVarDefNodes));
				List<Node> list = new List<Node>();
				List<Node> list2 = new List<Node>();
				for (int i = 0; i < keyVarDefNodes.Count; i++)
				{
					Node node2 = keyVarDefNodes[i];
					Node node3 = copyOfkeyVarDefNodes[i];
					Var var2 = ((VarDefOp)node2.Op).Var;
					Var var3 = ((VarDefOp)node3.Op).Var;
					this.FlattenProperties(this._iqtCommand.CreateNode(this._iqtCommand.CreateVarRefOp(var2)), list);
					this.FlattenProperties(this._iqtCommand.CreateNode(this._iqtCommand.CreateVarRefOp(var3)), list2);
				}
				PlanCompiler.Assert(list.Count == list2.Count, "The flattened keys lists should have the same nubmer of elements");
				Node node4 = null;
				for (int j = 0; j < list.Count; j++)
				{
					Node node5 = list[j];
					Node node6 = list2[j];
					Node node7 = this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.Or), this._iqtCommand.CreateNode(this._iqtCommand.CreateComparisonOp(OpType.EQ), node5, node6), this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.And), this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.IsNull), OpCopier.Copy(this._iqtCommand, node5)), this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.IsNull), OpCopier.Copy(this._iqtCommand, node6))));
					if (node4 == null)
					{
						node4 = node7;
					}
					else
					{
						node4 = this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.And), node4, node7);
					}
				}
				Node node8 = this._iqtCommand.CreateNode(this._iqtCommand.CreateFilterOp(), arg, node4);
				node = node8;
			}
			this._varMap[node] = var;
			node = this.ConvertRelOpToScalarOpTree(node, inputResultType);
			return this._iqtCommand.CreateVarDefNode(node, out groupAggVar);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001EB88 File Offset: 0x0001CD88
		private void FlattenProperties(Node input, IList<Node> flattenedProperties)
		{
			if (input.Op.Type.EdmType.BuiltInTypeKind == BuiltInTypeKind.RowType)
			{
				IList<EdmProperty> properties = TypeHelpers.GetProperties(input.Op.Type);
				PlanCompiler.Assert(properties.Count != 0, "No nested properties for RowType");
				for (int i = 0; i < properties.Count; i++)
				{
					Node arg = (i == 0) ? input : OpCopier.Copy(this._iqtCommand, input);
					this.FlattenProperties(this._iqtCommand.CreateNode(this._iqtCommand.CreatePropertyOp(properties[i]), arg), flattenedProperties);
				}
				return;
			}
			flattenedProperties.Add(input);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001EC24 File Offset: 0x0001CE24
		private Node VisitSortArguments(DbExpressionBinding input, IList<DbSortClause> sortOrder, List<SortKey> sortKeys, out Var inputVar)
		{
			Node node = this.EnterExpressionBinding(input);
			inputVar = this._varMap[node];
			VarVec varVec = this._iqtCommand.CreateVarVec();
			varVec.Set(inputVar);
			List<Node> list = new List<Node>();
			PlanCompiler.Assert(sortKeys.Count == 0, "Non-empty SortKey list before adding converted SortClauses");
			for (int i = 0; i < sortOrder.Count; i++)
			{
				DbSortClause dbSortClause = sortOrder[i];
				Node node2 = this.VisitExprAsScalar(dbSortClause.Expression);
				ScalarOp scalarOp = node2.Op as ScalarOp;
				PlanCompiler.Assert(scalarOp != null, "DbSortClause Expression converted to non-ScalarOp");
				Var v;
				list.Add(this._iqtCommand.CreateVarDefNode(node2, out v));
				varVec.Set(v);
				SortKey item;
				if (string.IsNullOrEmpty(dbSortClause.Collation))
				{
					item = Command.CreateSortKey(v, dbSortClause.Ascending);
				}
				else
				{
					item = Command.CreateSortKey(v, dbSortClause.Ascending, dbSortClause.Collation);
				}
				sortKeys.Add(item);
			}
			this.ExitExpressionBinding();
			return this._iqtCommand.CreateNode(this._iqtCommand.CreateProjectOp(varVec), node, this._iqtCommand.CreateNode(this._iqtCommand.CreateVarDefListOp(), list));
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001ED5C File Offset: 0x0001CF5C
		public override Node Visit(DbSkipExpression expression)
		{
			List<SortKey> sortKeys = new List<SortKey>();
			Var value;
			Node arg = this.VisitSortArguments(expression.Input, expression.SortOrder, sortKeys, out value);
			Node arg2 = this.VisitExprAsScalar(expression.Count);
			Node node = this._iqtCommand.CreateNode(this._iqtCommand.CreateConstrainedSortOp(sortKeys), arg, arg2, this._iqtCommand.CreateNode(this._iqtCommand.CreateNullOp(this._iqtCommand.IntegerType)));
			this._varMap[node] = value;
			return node;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001EDE0 File Offset: 0x0001CFE0
		public override Node Visit(DbSortExpression e)
		{
			List<SortKey> sortKeys = new List<SortKey>();
			Var value;
			Node arg = this.VisitSortArguments(e.Input, e.SortOrder, sortKeys, out value);
			SortOp op = this._iqtCommand.CreateSortOp(sortKeys);
			Node node = this._iqtCommand.CreateNode(op, arg);
			this._varMap[node] = value;
			return node;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001EE38 File Offset: 0x0001D038
		public override Node Visit(DbQuantifierExpression e)
		{
			PlanCompiler.Assert(DbExpressionKind.Any == e.ExpressionKind || e.ExpressionKind == DbExpressionKind.All, "Invalid DbExpressionKind in DbQuantifierExpression");
			Node node = this.EnterExpressionBinding(e.Input);
			Node node2 = this.VisitExprAsPredicate(e.Predicate);
			if (e.ExpressionKind == DbExpressionKind.All)
			{
				node2 = this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.Not), node2);
				Node node3 = this.VisitExprAsScalar(e.Predicate);
				node3 = this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.IsNull), node3);
				node2 = this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.Or), node2, node3);
			}
			this.ExitExpressionBinding();
			Var value = this._varMap[node];
			node = this._iqtCommand.CreateNode(this._iqtCommand.CreateFilterOp(), node, node2);
			this._varMap[node] = value;
			Node node4 = this._iqtCommand.CreateNode(this._iqtCommand.CreateExistsOp(), node);
			if (e.ExpressionKind == DbExpressionKind.All)
			{
				node4 = this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.Not), node4);
			}
			return node4;
		}

		// Token: 0x04000787 RID: 1927
		private static Dictionary<DbExpressionKind, OpType> s_opMap = ITreeGenerator.InitializeExpressionKindToOpTypeMap();

		// Token: 0x04000788 RID: 1928
		private readonly Command _iqtCommand;

		// Token: 0x04000789 RID: 1929
		private readonly Stack<ITreeGenerator.CqtVariableScope> _varScopes = new Stack<ITreeGenerator.CqtVariableScope>();

		// Token: 0x0400078A RID: 1930
		private readonly Dictionary<Node, Var> _varMap = new Dictionary<Node, Var>();

		// Token: 0x0400078B RID: 1931
		private readonly Stack<EdmFunction> _functionExpansions = new Stack<EdmFunction>();

		// Token: 0x0400078C RID: 1932
		private readonly Dictionary<DbExpression, bool> _functionsIsPredicateFlag = new Dictionary<DbExpression, bool>();

		// Token: 0x0400078D RID: 1933
		private readonly HashSet<DbFilterExpression> _processedIsOfFilters = new HashSet<DbFilterExpression>();

		// Token: 0x0400078E RID: 1934
		private readonly HashSet<DbTreatExpression> _fakeTreats = new HashSet<DbTreatExpression>();

		// Token: 0x0400078F RID: 1935
		private readonly DiscriminatorMap _discriminatorMap;

		// Token: 0x04000790 RID: 1936
		private readonly DbProjectExpression _discriminatedViewTopProject;

		// Token: 0x0200046B RID: 1131
		private abstract class CqtVariableScope
		{
			// Token: 0x06003AFE RID: 15102
			internal abstract bool Contains(string varName);

			// Token: 0x17000AC3 RID: 2755
			internal abstract Node this[string varName]
			{
				get;
			}

			// Token: 0x06003B00 RID: 15104
			internal abstract bool IsPredicate(string varName);
		}

		// Token: 0x0200046C RID: 1132
		private class ExpressionBindingScope : ITreeGenerator.CqtVariableScope
		{
			// Token: 0x06003B02 RID: 15106 RVA: 0x000DF20C File Offset: 0x000DD40C
			internal ExpressionBindingScope(Command iqtTree, string name, Var iqtVar)
			{
				this._tree = iqtTree;
				this._varName = name;
				this._var = iqtVar;
			}

			// Token: 0x06003B03 RID: 15107 RVA: 0x000DF229 File Offset: 0x000DD429
			internal override bool Contains(string name)
			{
				return this._varName == name;
			}

			// Token: 0x17000AC4 RID: 2756
			internal override Node this[string name]
			{
				get
				{
					PlanCompiler.Assert(name == this._varName, "huh?");
					return this._tree.CreateNode(this._tree.CreateVarRefOp(this._var));
				}
			}

			// Token: 0x06003B05 RID: 15109 RVA: 0x000173E2 File Offset: 0x000155E2
			internal override bool IsPredicate(string varName)
			{
				return false;
			}

			// Token: 0x17000AC5 RID: 2757
			// (get) Token: 0x06003B06 RID: 15110 RVA: 0x000DF26B File Offset: 0x000DD46B
			internal Var ScopeVar
			{
				get
				{
					return this._var;
				}
			}

			// Token: 0x0400195A RID: 6490
			private Command _tree;

			// Token: 0x0400195B RID: 6491
			private string _varName;

			// Token: 0x0400195C RID: 6492
			private Var _var;
		}

		// Token: 0x0200046D RID: 1133
		private sealed class LambdaScope : ITreeGenerator.CqtVariableScope
		{
			// Token: 0x06003B07 RID: 15111 RVA: 0x000DF273 File Offset: 0x000DD473
			internal LambdaScope(ITreeGenerator treeGen, Command command, Dictionary<string, Tuple<Node, bool>> args)
			{
				this._treeGen = treeGen;
				this._command = command;
				this._arguments = args;
				this._referencedArgs = new Dictionary<Node, bool>(this._arguments.Count);
			}

			// Token: 0x06003B08 RID: 15112 RVA: 0x000DF2A6 File Offset: 0x000DD4A6
			internal override bool Contains(string name)
			{
				return this._arguments.ContainsKey(name);
			}

			// Token: 0x17000AC6 RID: 2758
			internal override Node this[string name]
			{
				get
				{
					PlanCompiler.Assert(this._arguments.ContainsKey(name), "LambdaScope indexer called for invalid Var");
					Node node = this._arguments[name].Item1;
					if (this._referencedArgs.ContainsKey(node))
					{
						VarMap varMap = null;
						Node node2 = OpCopier.Copy(this._command, node, out varMap);
						if (varMap.Count > 0)
						{
							this.MapCopiedNodeVars(new List<Node>(1)
							{
								node
							}, new List<Node>(1)
							{
								node2
							}, varMap);
						}
						node = node2;
					}
					else
					{
						this._referencedArgs[node] = true;
					}
					return node;
				}
			}

			// Token: 0x06003B0A RID: 15114 RVA: 0x000DF34B File Offset: 0x000DD54B
			internal override bool IsPredicate(string name)
			{
				PlanCompiler.Assert(this._arguments.ContainsKey(name), "LambdaScope indexer called for invalid Var");
				return this._arguments[name].Item2;
			}

			// Token: 0x06003B0B RID: 15115 RVA: 0x000DF374 File Offset: 0x000DD574
			private void MapCopiedNodeVars(IList<Node> sources, IList<Node> copies, Dictionary<Var, Var> varMappings)
			{
				PlanCompiler.Assert(sources.Count == copies.Count, "Source/Copy Node count mismatch");
				for (int i = 0; i < sources.Count; i++)
				{
					Node node = sources[i];
					Node node2 = copies[i];
					if (node.Children.Count > 0)
					{
						this.MapCopiedNodeVars(node.Children, node2.Children, varMappings);
					}
					Var key = null;
					if (this._treeGen.VarMap.TryGetValue(node, out key))
					{
						PlanCompiler.Assert(varMappings.ContainsKey(key), "No mapping found for Var in Var to Var map from OpCopier");
						this._treeGen.VarMap[node2] = varMappings[key];
					}
				}
			}

			// Token: 0x0400195D RID: 6493
			private readonly ITreeGenerator _treeGen;

			// Token: 0x0400195E RID: 6494
			private readonly Command _command;

			// Token: 0x0400195F RID: 6495
			private readonly Dictionary<string, Tuple<Node, bool>> _arguments;

			// Token: 0x04001960 RID: 6496
			private readonly Dictionary<Node, bool> _referencedArgs;
		}

		// Token: 0x0200046E RID: 1134
		// (Invoke) Token: 0x06003B0D RID: 15117
		private delegate Node VisitExprDelegate(DbExpression e);

		// Token: 0x0200046F RID: 1135
		private class IsOfFilter
		{
			// Token: 0x06003B10 RID: 15120 RVA: 0x000DF41B File Offset: 0x000DD61B
			internal IsOfFilter(DbIsOfExpression template)
			{
				this.requiredType = template.OfType;
				this.isExact = (template.ExpressionKind == DbExpressionKind.IsOfOnly);
			}

			// Token: 0x06003B11 RID: 15121 RVA: 0x000DF43F File Offset: 0x000DD63F
			internal IsOfFilter(DbOfTypeExpression template)
			{
				this.requiredType = template.OfType;
				this.isExact = (template.ExpressionKind == DbExpressionKind.OfTypeOnly);
			}

			// Token: 0x06003B12 RID: 15122 RVA: 0x000DF463 File Offset: 0x000DD663
			private IsOfFilter(TypeUsage required, bool exact)
			{
				this.requiredType = required;
				this.isExact = exact;
			}

			// Token: 0x06003B13 RID: 15123 RVA: 0x000DF47C File Offset: 0x000DD67C
			private ITreeGenerator.IsOfFilter Merge(TypeUsage otherRequiredType, bool otherIsExact)
			{
				bool flag = this.requiredType.EdmEquals(otherRequiredType);
				ITreeGenerator.IsOfFilter isOfFilter;
				if (flag && this.isExact == otherIsExact)
				{
					isOfFilter = this;
				}
				else if (this.isExact && otherIsExact)
				{
					isOfFilter = new ITreeGenerator.IsOfFilter(otherRequiredType, otherIsExact);
					isOfFilter.next = this;
				}
				else if (!this.isExact && !otherIsExact)
				{
					if (otherRequiredType.IsSubtypeOf(this.requiredType))
					{
						isOfFilter = new ITreeGenerator.IsOfFilter(otherRequiredType, false);
						isOfFilter.next = this.next;
					}
					else if (this.requiredType.IsSubtypeOf(otherRequiredType))
					{
						isOfFilter = this;
					}
					else
					{
						isOfFilter = new ITreeGenerator.IsOfFilter(otherRequiredType, otherIsExact);
						isOfFilter.next = this;
					}
				}
				else if (flag)
				{
					isOfFilter = new ITreeGenerator.IsOfFilter(otherRequiredType, true);
					isOfFilter.next = this.next;
				}
				else
				{
					TypeUsage typeUsage = this.isExact ? this.requiredType : otherRequiredType;
					TypeUsage typeUsage2 = this.isExact ? otherRequiredType : this.requiredType;
					if (typeUsage.IsSubtypeOf(typeUsage2))
					{
						if (typeUsage == this.requiredType && this.isExact)
						{
							isOfFilter = this;
						}
						else
						{
							isOfFilter = new ITreeGenerator.IsOfFilter(typeUsage, true);
							isOfFilter.next = this.next;
						}
					}
					else
					{
						isOfFilter = new ITreeGenerator.IsOfFilter(otherRequiredType, otherIsExact);
						isOfFilter.next = this;
					}
				}
				return isOfFilter;
			}

			// Token: 0x06003B14 RID: 15124 RVA: 0x000DF5A3 File Offset: 0x000DD7A3
			internal ITreeGenerator.IsOfFilter Merge(DbIsOfExpression other)
			{
				return this.Merge(other.OfType, other.ExpressionKind == DbExpressionKind.IsOfOnly);
			}

			// Token: 0x06003B15 RID: 15125 RVA: 0x000DF5BB File Offset: 0x000DD7BB
			internal ITreeGenerator.IsOfFilter Merge(DbOfTypeExpression other)
			{
				return this.Merge(other.OfType, other.ExpressionKind == DbExpressionKind.OfTypeOnly);
			}

			// Token: 0x06003B16 RID: 15126 RVA: 0x000DF5D3 File Offset: 0x000DD7D3
			internal IEnumerable<KeyValuePair<TypeUsage, bool>> ToEnumerable()
			{
				for (ITreeGenerator.IsOfFilter currentFilter = this; currentFilter != null; currentFilter = currentFilter.next)
				{
					yield return new KeyValuePair<TypeUsage, bool>(currentFilter.requiredType, currentFilter.isExact);
				}
				yield break;
			}

			// Token: 0x04001961 RID: 6497
			private readonly TypeUsage requiredType;

			// Token: 0x04001962 RID: 6498
			private readonly bool isExact;

			// Token: 0x04001963 RID: 6499
			private ITreeGenerator.IsOfFilter next;
		}
	}
}
