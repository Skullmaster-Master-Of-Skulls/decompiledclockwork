using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000677 RID: 1655
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal class ITreeGenerator : DbExpressionVisitor<Node>
	{
		// Token: 0x0600407C RID: 16508 RVA: 0x00128358 File Offset: 0x00126558
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

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x0600407D RID: 16509 RVA: 0x001283E3 File Offset: 0x001265E3
		internal Dictionary<Node, Var> VarMap
		{
			get
			{
				return this._varMap;
			}
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x001283EB File Offset: 0x001265EB
		public static Command Generate(DbQueryCommandTree ctree)
		{
			return ITreeGenerator.Generate(ctree, null);
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x001283F4 File Offset: 0x001265F4
		internal static Command Generate(DbQueryCommandTree ctree, DiscriminatorMap discriminatorMap)
		{
			ITreeGenerator treeGenerator = new ITreeGenerator(ctree, discriminatorMap);
			return treeGenerator._iqtCommand;
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x00128410 File Offset: 0x00126610
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		private ITreeGenerator(DbQueryCommandTree ctree, DiscriminatorMap discriminatorMap)
		{
			this._useDatabaseNullSemantics = ctree.UseDatabaseNullSemantics;
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
					throw new NotSupportedException(Strings.ParameterTypeNotSupported(keyValuePair.Key, keyValuePair.Value.ToString()));
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

		// Token: 0x06004081 RID: 16513 RVA: 0x00128660 File Offset: 0x00126860
		private static bool ValidateParameterType(TypeUsage paramType)
		{
			return paramType != null && paramType.EdmType != null && (TypeSemantics.IsPrimitiveType(paramType) || paramType.EdmType is EnumType);
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x00128687 File Offset: 0x00126887
		private static RowType ExtractElementRowType(TypeUsage typeUsage)
		{
			return TypeHelpers.GetEdmType<RowType>(TypeHelpers.GetEdmType<CollectionType>(typeUsage).TypeUsage);
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x0012869C File Offset: 0x0012689C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IsPredicate")]
		private bool IsPredicate(DbExpression expr)
		{
			if (TypeSemantics.IsPrimitiveType(expr.ResultType, PrimitiveTypeKind.Boolean))
			{
				DbExpressionKind expressionKind = expr.ExpressionKind;
				if (expressionKind <= DbExpressionKind.NotEquals)
				{
					switch (expressionKind)
					{
					case DbExpressionKind.All:
					case DbExpressionKind.And:
					case DbExpressionKind.Any:
						break;
					default:
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
						break;
					}
				}
				else if (expressionKind != DbExpressionKind.Or)
				{
					switch (expressionKind)
					{
					case DbExpressionKind.VariableReference:
					{
						DbVariableReferenceExpression dbVariableReferenceExpression = (DbVariableReferenceExpression)expr;
						return this.ResolveScope(dbVariableReferenceExpression).IsPredicate(dbVariableReferenceExpression.VariableName);
					}
					case DbExpressionKind.Lambda:
					{
						bool result2;
						if (this._functionsIsPredicateFlag.TryGetValue(expr, out result2))
						{
							return result2;
						}
						PlanCompiler.Assert(false, "IsPredicate must be called on a visited lambda expression");
						return false;
					}
					case DbExpressionKind.In:
						break;
					default:
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x001287E2 File Offset: 0x001269E2
		private Node VisitExpr(DbExpression e)
		{
			if (e == null)
			{
				return null;
			}
			return e.Accept<Node>(this);
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x001287F0 File Offset: 0x001269F0
		private Node VisitExprAsScalar(DbExpression expr)
		{
			if (expr == null)
			{
				return null;
			}
			Node node = this.VisitExpr(expr);
			return this.ConvertToScalarOpTree(node, expr);
		}

		// Token: 0x06004086 RID: 16518 RVA: 0x00128814 File Offset: 0x00126A14
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

		// Token: 0x06004087 RID: 16519 RVA: 0x00128848 File Offset: 0x00126A48
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "RelOp")]
		private Node ConvertRelOpToScalarOpTree(Node node, TypeUsage resultType)
		{
			PlanCompiler.Assert(TypeSemantics.IsCollectionType(resultType), "RelOp with non-Collection result type");
			CollectOp op = this._iqtCommand.CreateCollectOp(resultType);
			Node arg = this.CapWithPhysicalProject(node);
			node = this._iqtCommand.CreateNode(op, arg);
			return node;
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x0012888C File Offset: 0x00126A8C
		private Node ConvertPredicateToScalarOpTree(Node node, DbExpression expr)
		{
			CaseOp op = this._iqtCommand.CreateCaseOp(this._iqtCommand.BooleanType);
			bool flag = this.IsNullable(expr);
			List<Node> list = new List<Node>(flag ? 5 : 3);
			list.Add(node);
			list.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateInternalConstantOp(this._iqtCommand.BooleanType, true)));
			if (flag)
			{
				Node arg = this.VisitExpr(expr);
				list.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.Not), arg));
			}
			list.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateInternalConstantOp(this._iqtCommand.BooleanType, false)));
			if (flag)
			{
				list.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateNullOp(this._iqtCommand.BooleanType)));
			}
			node = this._iqtCommand.CreateNode(op, list);
			return node;
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x0012898C File Offset: 0x00126B8C
		private bool IsNullable(DbExpression expression)
		{
			DbExpressionKind expressionKind = expression.ExpressionKind;
			if (expressionKind <= DbExpressionKind.IsNull)
			{
				switch (expressionKind)
				{
				case DbExpressionKind.All:
				case DbExpressionKind.Any:
					break;
				case DbExpressionKind.And:
					goto IL_51;
				default:
					switch (expressionKind)
					{
					case DbExpressionKind.IsEmpty:
					case DbExpressionKind.IsNull:
						break;
					default:
						return true;
					}
					break;
				}
				return false;
			}
			if (expressionKind == DbExpressionKind.Not)
			{
				return this.IsNullable(((DbNotExpression)expression).Argument);
			}
			if (expressionKind != DbExpressionKind.Or)
			{
				return true;
			}
			IL_51:
			DbBinaryExpression dbBinaryExpression = (DbBinaryExpression)expression;
			return this.IsNullable(dbBinaryExpression.Left) || this.IsNullable(dbBinaryExpression.Right);
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x00128A10 File Offset: 0x00126C10
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "relOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private Node VisitExprAsPredicate(DbExpression expr)
		{
			if (expr == null)
			{
				return null;
			}
			Node node = this.VisitExpr(expr);
			if (!this.IsPredicate(expr))
			{
				ComparisonOp op = this._iqtCommand.CreateComparisonOp(OpType.EQ, false);
				Node arg = this._iqtCommand.CreateNode(this._iqtCommand.CreateInternalConstantOp(this._iqtCommand.BooleanType, true));
				node = this._iqtCommand.CreateNode(op, node, arg);
			}
			else
			{
				PlanCompiler.Assert(!node.Op.IsRelOp, "unexpected relOp as predicate?");
			}
			return node;
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x00128A94 File Offset: 0x00126C94
		private static IList<Node> VisitExpr(IList<DbExpression> exprs, ITreeGenerator.VisitExprDelegate exprDelegate)
		{
			List<Node> list = new List<Node>();
			for (int i = 0; i < exprs.Count; i++)
			{
				list.Add(exprDelegate(exprs[i]));
			}
			return list;
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x00128ACC File Offset: 0x00126CCC
		private IList<Node> VisitExprAsScalar(IList<DbExpression> exprs)
		{
			return ITreeGenerator.VisitExpr(exprs, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x0600408D RID: 16525 RVA: 0x00128AE0 File Offset: 0x00126CE0
		private Node VisitUnary(DbUnaryExpression e, Op op, ITreeGenerator.VisitExprDelegate exprDelegate)
		{
			return this._iqtCommand.CreateNode(op, exprDelegate(e.Argument));
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x00128AFA File Offset: 0x00126CFA
		private Node VisitBinary(DbBinaryExpression e, Op op, ITreeGenerator.VisitExprDelegate exprDelegate)
		{
			return this._iqtCommand.CreateNode(op, exprDelegate(e.Left), exprDelegate(e.Right));
		}

		// Token: 0x0600408F RID: 16527 RVA: 0x00128B20 File Offset: 0x00126D20
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CollectOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ScalarOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "PhysicalProjectOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "RelOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "non-ScalarOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "non-RelOp")]
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
			PlanCompiler.Assert(unnestOp.Table.Columns.Count == 1, "Un-nest of collection ScalarOp produced unexpected number of columns (1 expected)");
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

		// Token: 0x06004090 RID: 16528 RVA: 0x00128CB4 File Offset: 0x00126EB4
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "non-RelOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x06004091 RID: 16529 RVA: 0x00128D34 File Offset: 0x00126F34
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "non-RelOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private Node CapWithPhysicalProject(Node input)
		{
			PlanCompiler.Assert(input.Op.IsRelOp, "unexpected non-RelOp?");
			Var outputVar = this._varMap[input];
			PhysicalProjectOp op = this._iqtCommand.CreatePhysicalProjectOp(outputVar);
			return this._iqtCommand.CreateNode(op, input);
		}

		// Token: 0x06004092 RID: 16530 RVA: 0x00128D7F File Offset: 0x00126F7F
		private Node EnterExpressionBinding(DbExpressionBinding binding)
		{
			return this.VisitBoundExpressionPushBindingScope(binding.Expression, binding.VariableName);
		}

		// Token: 0x06004093 RID: 16531 RVA: 0x00128D93 File Offset: 0x00126F93
		private Node EnterGroupExpressionBinding(DbGroupExpressionBinding binding)
		{
			return this.VisitBoundExpressionPushBindingScope(binding.Expression, binding.VariableName);
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x00128DA8 File Offset: 0x00126FA8
		private Node VisitBoundExpressionPushBindingScope(DbExpression boundExpression, string bindingName)
		{
			Var boundVar;
			Node result = this.VisitBoundExpression(boundExpression, out boundVar);
			this.PushBindingScope(boundVar, bindingName);
			return result;
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x00128DC8 File Offset: 0x00126FC8
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbExpressionBinding")]
		private Node VisitBoundExpression(DbExpression boundExpression, out Var boundVar)
		{
			Node node = this.VisitExpr(boundExpression);
			PlanCompiler.Assert(node != null, "DbExpressionBinding.Expression produced null conversion");
			node = this.EnsureRelOp(node);
			boundVar = this._varMap[node];
			PlanCompiler.Assert(boundVar != null, "No Var found for Input Op");
			return node;
		}

		// Token: 0x06004096 RID: 16534 RVA: 0x00128E17 File Offset: 0x00127017
		private void PushBindingScope(Var boundVar, string bindingName)
		{
			this._varScopes.Push(new ITreeGenerator.ExpressionBindingScope(this._iqtCommand, bindingName, boundVar));
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x00128E34 File Offset: 0x00127034
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ExpressionBindingScope")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ExitExpressionBinding")]
		private ITreeGenerator.ExpressionBindingScope ExitExpressionBinding()
		{
			ITreeGenerator.ExpressionBindingScope expressionBindingScope = this._varScopes.Pop() as ITreeGenerator.ExpressionBindingScope;
			PlanCompiler.Assert(expressionBindingScope != null, "ExitExpressionBinding called without ExpressionBindingScope on top of scope stack");
			return expressionBindingScope;
		}

		// Token: 0x06004098 RID: 16536 RVA: 0x00128E64 File Offset: 0x00127064
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ExitGroupExpressionBinding")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ExpressionBindingScope")]
		private void ExitGroupExpressionBinding()
		{
			ITreeGenerator.ExpressionBindingScope expressionBindingScope = this._varScopes.Pop() as ITreeGenerator.ExpressionBindingScope;
			PlanCompiler.Assert(expressionBindingScope != null, "ExitGroupExpressionBinding called without ExpressionBindingScope on top of scope stack");
		}

		// Token: 0x06004099 RID: 16537 RVA: 0x00128E94 File Offset: 0x00127094
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
					throw new EntityCommandCompilationException(Strings.Cqt_UDF_FunctionDefinitionWithCircularReference(expandingEdmFunction.FullName), null);
				}
				this._functionExpansions.Push(expandingEdmFunction);
			}
			this._varScopes.Push(new ITreeGenerator.LambdaScope(this, this._iqtCommand, dictionary));
		}

		// Token: 0x0600409A RID: 16538 RVA: 0x00128F4C File Offset: 0x0012714C
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ExitLambdaFunction")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "LambdaScope")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x0600409B RID: 16539 RVA: 0x00128F9C File Offset: 0x0012719C
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

		// Token: 0x0600409C RID: 16540 RVA: 0x00129060 File Offset: 0x00127260
		public override Node Visit(DbExpression e)
		{
			Check.NotNull<DbExpression>(e, "e");
			throw new NotSupportedException(Strings.Cqt_General_UnsupportedExpression(e.GetType().FullName));
		}

		// Token: 0x0600409D RID: 16541 RVA: 0x00129084 File Offset: 0x00127284
		public override Node Visit(DbConstantExpression e)
		{
			Check.NotNull<DbConstantExpression>(e, "e");
			ConstantBaseOp op = this._iqtCommand.CreateConstantOp(e.ResultType, e.GetValue());
			return this._iqtCommand.CreateNode(op);
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x001290C4 File Offset: 0x001272C4
		public override Node Visit(DbNullExpression e)
		{
			Check.NotNull<DbNullExpression>(e, "e");
			NullOp op = this._iqtCommand.CreateNullOp(e.ResultType);
			return this._iqtCommand.CreateNode(op);
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x001290FC File Offset: 0x001272FC
		public override Node Visit(DbVariableReferenceExpression e)
		{
			Check.NotNull<DbVariableReferenceExpression>(e, "e");
			return this.ResolveScope(e)[e.VariableName];
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x0012912C File Offset: 0x0012732C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "VarRef")]
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

		// Token: 0x060040A1 RID: 16545 RVA: 0x00129198 File Offset: 0x00127398
		public override Node Visit(DbParameterReferenceExpression e)
		{
			Check.NotNull<DbParameterReferenceExpression>(e, "e");
			Op op = this._iqtCommand.CreateVarRefOp(this._iqtCommand.GetParameter(e.ParameterName));
			return this._iqtCommand.CreateNode(op);
		}

		// Token: 0x060040A2 RID: 16546 RVA: 0x001291DC File Offset: 0x001273DC
		public override Node Visit(DbFunctionExpression e)
		{
			Check.NotNull<DbFunctionExpression>(e, "e");
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
					if (ex.IsCatchableExceptionType())
					{
						throw new EntityCommandCompilationException(Strings.Cqt_UDF_FunctionDefinitionGenerationFailed(e.Function.FullName), ex);
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

		// Token: 0x060040A3 RID: 16547 RVA: 0x001292E8 File Offset: 0x001274E8
		public override Node Visit(DbLambdaExpression e)
		{
			Check.NotNull<DbLambdaExpression>(e, "e");
			return this.VisitLambdaExpression(e.Lambda, e.Arguments, e, null);
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x0012930C File Offset: 0x0012750C
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

		// Token: 0x060040A5 RID: 16549 RVA: 0x001293B0 File Offset: 0x001275B0
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x060040A6 RID: 16550 RVA: 0x001294B3 File Offset: 0x001276B3
		private Node BuildSoftCast(Node node, EdmType targetType)
		{
			return this.BuildSoftCast(node, TypeUsage.Create(targetType));
		}

		// Token: 0x060040A7 RID: 16551 RVA: 0x001294C4 File Offset: 0x001276C4
		private Node BuildEntityRef(Node arg, TypeUsage entityType)
		{
			TypeUsage type = TypeHelpers.CreateReferenceTypeUsage((EntityType)entityType.EdmType);
			return this._iqtCommand.CreateNode(this._iqtCommand.CreateGetEntityRefOp(type), arg);
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x001294FC File Offset: 0x001276FC
		private static bool TryRewriteKeyPropertyAccess(DbPropertyExpression propertyExpression, out DbExpression rewritten)
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

		// Token: 0x060040A9 RID: 16553 RVA: 0x001295C8 File Offset: 0x001277C8
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		public override Node Visit(DbPropertyExpression e)
		{
			Check.NotNull<DbPropertyExpression>(e, "e");
			if (BuiltInTypeKind.EdmProperty != e.Property.BuiltInTypeKind && e.Property.BuiltInTypeKind != BuiltInTypeKind.AssociationEndMember && BuiltInTypeKind.NavigationProperty != e.Property.BuiltInTypeKind)
			{
				throw new NotSupportedException();
			}
			PlanCompiler.Assert(e.Instance != null, "Static properties are not supported");
			DbExpression e2;
			Node node;
			if (ITreeGenerator.TryRewriteKeyPropertyAccess(e, out e2))
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

		// Token: 0x060040AA RID: 16554 RVA: 0x00129738 File Offset: 0x00127938
		public override Node Visit(DbComparisonExpression e)
		{
			Check.NotNull<DbComparisonExpression>(e, "e");
			Op op = this._iqtCommand.CreateComparisonOp(ITreeGenerator._opMap[e.ExpressionKind], false);
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

		// Token: 0x060040AB RID: 16555 RVA: 0x00129810 File Offset: 0x00127A10
		public override Node Visit(DbLikeExpression e)
		{
			Check.NotNull<DbLikeExpression>(e, "e");
			return this._iqtCommand.CreateNode(this._iqtCommand.CreateLikeOp(), this.VisitExpr(e.Argument), this.VisitExpr(e.Pattern), this.VisitExpr(e.Escape));
		}

		// Token: 0x060040AC RID: 16556 RVA: 0x00129864 File Offset: 0x00127A64
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

		// Token: 0x060040AD RID: 16557 RVA: 0x0012995C File Offset: 0x00127B5C
		public override Node Visit(DbLimitExpression expression)
		{
			Check.NotNull<DbLimitExpression>(expression, "expression");
			Node node = this.EnsureRelOp(this.VisitExpr(expression.Argument));
			Var value = this._varMap[node];
			Node limitNode = this.VisitExprAsScalar(expression.Limit);
			Node node2;
			if (OpType.Project == node.Op.OpType && (node.Child0.Op.OpType == OpType.Sort || node.Child0.Op.OpType == OpType.ConstrainedSort))
			{
				node.Child0 = this.CreateLimitNode(node.Child0, limitNode, expression.WithTies);
				node2 = node;
			}
			else
			{
				node2 = this.CreateLimitNode(node, limitNode, expression.WithTies);
			}
			if (!object.ReferenceEquals(node2, node))
			{
				this._varMap[node2] = value;
			}
			return node2;
		}

		// Token: 0x060040AE RID: 16558 RVA: 0x00129A1C File Offset: 0x00127C1C
		public override Node Visit(DbIsNullExpression e)
		{
			Check.NotNull<DbIsNullExpression>(e, "e");
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

		// Token: 0x060040AF RID: 16559 RVA: 0x00129AFC File Offset: 0x00127CFC
		public override Node Visit(DbArithmeticExpression e)
		{
			Check.NotNull<DbArithmeticExpression>(e, "e");
			Op op = this._iqtCommand.CreateArithmeticOp(ITreeGenerator._opMap[e.ExpressionKind], e.ResultType);
			List<Node> list = new List<Node>();
			foreach (DbExpression expr in e.Arguments)
			{
				Node node = this.VisitExprAsScalar(expr);
				list.Add(this.BuildSoftCast(node, e.ResultType));
			}
			return this._iqtCommand.CreateNode(op, list);
		}

		// Token: 0x060040B0 RID: 16560 RVA: 0x00129BA4 File Offset: 0x00127DA4
		public override Node Visit(DbAndExpression e)
		{
			Check.NotNull<DbAndExpression>(e, "e");
			Op op = this._iqtCommand.CreateConditionalOp(OpType.And);
			return this.VisitBinary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsPredicate));
		}

		// Token: 0x060040B1 RID: 16561 RVA: 0x00129BE0 File Offset: 0x00127DE0
		public override Node Visit(DbOrExpression e)
		{
			Check.NotNull<DbOrExpression>(e, "e");
			Op op = this._iqtCommand.CreateConditionalOp(OpType.Or);
			return this.VisitBinary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsPredicate));
		}

		// Token: 0x060040B2 RID: 16562 RVA: 0x00129C1C File Offset: 0x00127E1C
		public override Node Visit(DbInExpression e)
		{
			Check.NotNull<DbInExpression>(e, "e");
			Op op = this._iqtCommand.CreateConditionalOp(OpType.In);
			List<Node> list = new List<Node>(1 + e.List.Count)
			{
				this.VisitExpr(e.Item)
			};
			list.AddRange(e.List.Select(new Func<DbExpression, Node>(this.VisitExpr)));
			return this._iqtCommand.CreateNode(op, list);
		}

		// Token: 0x060040B3 RID: 16563 RVA: 0x00129C94 File Offset: 0x00127E94
		public override Node Visit(DbNotExpression e)
		{
			Check.NotNull<DbNotExpression>(e, "e");
			Op op = this._iqtCommand.CreateConditionalOp(OpType.Not);
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsPredicate));
		}

		// Token: 0x060040B4 RID: 16564 RVA: 0x00129CD0 File Offset: 0x00127ED0
		public override Node Visit(DbDistinctExpression e)
		{
			Check.NotNull<DbDistinctExpression>(e, "e");
			Node node = this.EnsureRelOp(this.VisitExpr(e.Argument));
			Var var = this._varMap[node];
			Op op = this._iqtCommand.CreateDistinctOp(var);
			Node node2 = this._iqtCommand.CreateNode(op, node);
			this._varMap[node2] = var;
			return node2;
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x00129D34 File Offset: 0x00127F34
		public override Node Visit(DbElementExpression e)
		{
			Check.NotNull<DbElementExpression>(e, "e");
			Op op = this._iqtCommand.CreateElementOp(e.ResultType);
			Node node = this.EnsureRelOp(this.VisitExpr(e.Argument));
			node = this.BuildSoftCast(node, TypeHelpers.CreateCollectionTypeUsage(e.ResultType));
			Var value = this._varMap[node];
			node = this._iqtCommand.CreateNode(this._iqtCommand.CreateSingleRowOp(), node);
			this._varMap[node] = value;
			node = this.CapWithProject(node);
			return this._iqtCommand.CreateNode(op, node);
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x00129DCC File Offset: 0x00127FCC
		public override Node Visit(DbIsEmptyExpression e)
		{
			Check.NotNull<DbIsEmptyExpression>(e, "e");
			Op op = this._iqtCommand.CreateExistsOp();
			Node arg = this.EnsureRelOp(this.VisitExpr(e.Argument));
			return this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.Not), this._iqtCommand.CreateNode(op, arg));
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x00129E2C File Offset: 0x0012802C
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbExpression")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SetOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "VisitSetOpExpression")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "Non-SetOp")]
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

		// Token: 0x060040B8 RID: 16568 RVA: 0x00129F7F File Offset: 0x0012817F
		public override Node Visit(DbUnionAllExpression e)
		{
			Check.NotNull<DbUnionAllExpression>(e, "e");
			return this.VisitSetOpExpression(e);
		}

		// Token: 0x060040B9 RID: 16569 RVA: 0x00129F94 File Offset: 0x00128194
		public override Node Visit(DbIntersectExpression e)
		{
			Check.NotNull<DbIntersectExpression>(e, "e");
			return this.VisitSetOpExpression(e);
		}

		// Token: 0x060040BA RID: 16570 RVA: 0x00129FA9 File Offset: 0x001281A9
		public override Node Visit(DbExceptExpression e)
		{
			Check.NotNull<DbExceptExpression>(e, "e");
			return this.VisitSetOpExpression(e);
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x00129FC0 File Offset: 0x001281C0
		public override Node Visit(DbTreatExpression e)
		{
			Check.NotNull<DbTreatExpression>(e, "e");
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

		// Token: 0x060040BC RID: 16572 RVA: 0x0012A024 File Offset: 0x00128224
		public override Node Visit(DbIsOfExpression e)
		{
			Check.NotNull<DbIsOfExpression>(e, "e");
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

		// Token: 0x060040BD RID: 16573 RVA: 0x0012A084 File Offset: 0x00128284
		public override Node Visit(DbCastExpression e)
		{
			Check.NotNull<DbCastExpression>(e, "e");
			Op op = this._iqtCommand.CreateCastOp(e.ResultType);
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x060040BE RID: 16574 RVA: 0x0012A0C4 File Offset: 0x001282C4
		public override Node Visit(DbCaseExpression e)
		{
			Check.NotNull<DbCaseExpression>(e, "e");
			List<Node> list = new List<Node>();
			for (int i = 0; i < e.When.Count; i++)
			{
				list.Add(this.VisitExprAsPredicate(e.When[i]));
				list.Add(this.BuildSoftCast(this.VisitExprAsScalar(e.Then[i]), e.ResultType));
			}
			list.Add(this.BuildSoftCast(this.VisitExprAsScalar(e.Else), e.ResultType));
			return this._iqtCommand.CreateNode(this._iqtCommand.CreateCaseOp(e.ResultType), list);
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x0012A1BC File Offset: 0x001283BC
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

		// Token: 0x060040C0 RID: 16576 RVA: 0x0012A23C File Offset: 0x0012843C
		private static bool IsIsOfFilter(DbFilterExpression filter)
		{
			if (filter.Predicate.ExpressionKind != DbExpressionKind.IsOf && filter.Predicate.ExpressionKind != DbExpressionKind.IsOfOnly)
			{
				return false;
			}
			DbExpression argument = ((DbIsOfExpression)filter.Predicate).Argument;
			return argument.ExpressionKind == DbExpressionKind.VariableReference && ((DbVariableReferenceExpression)argument).VariableName == filter.Input.VariableName;
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x0012A2A4 File Offset: 0x001284A4
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
					if (ITreeGenerator.IsIsOfFilter(dbFilterExpression))
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
				switch (expressionKind)
				{
				case DbExpressionKind.OfType:
				case DbExpressionKind.OfTypeOnly:
				{
					DbOfTypeExpression dbOfTypeExpression = (DbOfTypeExpression)current;
					typeFilter = typeFilter.Merge(dbOfTypeExpression);
					DbExpression input2 = this.ApplyIsOfFilter(dbOfTypeExpression.Argument, typeFilter);
					DbExpressionBinding dbExpressionBinding = input2.Bind();
					DbTreatExpression dbTreatExpression = dbExpressionBinding.Variable.TreatAs(dbOfTypeExpression.OfType);
					this._fakeTreats.Add(dbTreatExpression);
					return dbExpressionBinding.Project(dbTreatExpression);
				}
				default:
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
					break;
				}
			}
			return this.CreateIsOfFilterExpression(current, typeFilter);
		}

		// Token: 0x060040C2 RID: 16578 RVA: 0x0012A48C File Offset: 0x0012868C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbOfTypeExpression")]
		public override Node Visit(DbOfTypeExpression e)
		{
			Check.NotNull<DbOfTypeExpression>(e, "e");
			PlanCompiler.Assert(TypeSemantics.IsCollectionType(e.Argument.ResultType), "Non-Collection Type Argument in DbOfTypeExpression");
			DbExpression e2 = this.ApplyIsOfFilter(e.Argument, new ITreeGenerator.IsOfFilter(e));
			Node node = this.EnsureRelOp(this.VisitExpr(e2));
			Var inputVar = this._varMap[node];
			Var value;
			Node node2 = this._iqtCommand.BuildFakeTreatProject(node, inputVar, e.OfType, out value);
			this._varMap[node2] = value;
			return node2;
		}

		// Token: 0x060040C3 RID: 16579 RVA: 0x0012A514 File Offset: 0x00128714
		public override Node Visit(DbNewInstanceExpression e)
		{
			Check.NotNull<DbNewInstanceExpression>(e, "e");
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
					goto IL_205;
				}
			}
			CollectionType edmType2 = TypeHelpers.GetEdmType<CollectionType>(e.ResultType);
			TypeUsage typeUsage = edmType2.TypeUsage;
			foreach (DbExpression expr in e.Arguments)
			{
				Node item4 = this.BuildSoftCast(this.VisitExprAsScalar(expr), typeUsage);
				list3.Add(item4);
			}
			IL_205:
			if (list != null)
			{
				list3.AddRange(list);
			}
			return this._iqtCommand.CreateNode(op, list3);
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x0012A76C File Offset: 0x0012896C
		public override Node Visit(DbRefExpression e)
		{
			Check.NotNull<DbRefExpression>(e, "e");
			Op op = this._iqtCommand.CreateRefOp(e.EntitySet, e.ResultType);
			Node arg = this.BuildSoftCast(this.VisitExprAsScalar(e.Argument), TypeHelpers.CreateKeyRowType(e.EntitySet.ElementType));
			return this._iqtCommand.CreateNode(op, arg);
		}

		// Token: 0x060040C5 RID: 16581 RVA: 0x0012A7D0 File Offset: 0x001289D0
		public override Node Visit(DbRelationshipNavigationExpression e)
		{
			Check.NotNull<DbRelationshipNavigationExpression>(e, "e");
			RelProperty relProperty = new RelProperty(e.Relationship, e.NavigateFrom, e.NavigateTo);
			Op op = this._iqtCommand.CreateNavigateOp(e.ResultType, relProperty);
			Node arg = this.VisitExprAsScalar(e.NavigationSource);
			return this._iqtCommand.CreateNode(op, arg);
		}

		// Token: 0x060040C6 RID: 16582 RVA: 0x0012A830 File Offset: 0x00128A30
		public override Node Visit(DbDerefExpression e)
		{
			Check.NotNull<DbDerefExpression>(e, "e");
			Op op = this._iqtCommand.CreateDerefOp(e.ResultType);
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x060040C7 RID: 16583 RVA: 0x0012A870 File Offset: 0x00128A70
		public override Node Visit(DbRefKeyExpression e)
		{
			Check.NotNull<DbRefKeyExpression>(e, "e");
			Op op = this._iqtCommand.CreateGetRefKeyOp(e.ResultType);
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x0012A8B0 File Offset: 0x00128AB0
		public override Node Visit(DbEntityRefExpression e)
		{
			Check.NotNull<DbEntityRefExpression>(e, "e");
			Op op = this._iqtCommand.CreateGetEntityRefOp(e.ResultType);
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x0012A8F0 File Offset: 0x00128AF0
		public override Node Visit(DbScanExpression e)
		{
			Check.NotNull<DbScanExpression>(e, "e");
			TableMD tableMetadata = Command.CreateTableDefinition(e.Target);
			ScanTableOp scanTableOp = this._iqtCommand.CreateScanTableOp(tableMetadata);
			Node node = this._iqtCommand.CreateNode(scanTableOp);
			Var value = scanTableOp.Table.Columns[0];
			this._varMap[node] = value;
			return node;
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x0012A950 File Offset: 0x00128B50
		public override Node Visit(DbFilterExpression e)
		{
			Check.NotNull<DbFilterExpression>(e, "e");
			if (!ITreeGenerator.IsIsOfFilter(e) || this._processedIsOfFilters.Contains(e))
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

		// Token: 0x060040CB RID: 16587 RVA: 0x0012AA04 File Offset: 0x00128C04
		public override Node Visit(DbProjectExpression e)
		{
			Check.NotNull<DbProjectExpression>(e, "e");
			if (e == this._discriminatedViewTopProject)
			{
				return this.GenerateDiscriminatedProject(e);
			}
			return this.GenerateStandardProject(e);
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x0012AA2C File Offset: 0x00128C2C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private Node GenerateDiscriminatedProject(DbProjectExpression e)
		{
			PlanCompiler.Assert(null != this._discriminatedViewTopProject, "if a project matches the pattern, there must be a corresponding discriminator map");
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

		// Token: 0x060040CD RID: 16589 RVA: 0x0012AC08 File Offset: 0x00128E08
		private Node CreateNewInstanceArgument(EdmMember property, DbExpression value)
		{
			return this.BuildSoftCast(this.VisitExprAsScalar(value), Helper.GetModelTypeUsage(property));
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x0012AC2C File Offset: 0x00128E2C
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

		// Token: 0x060040CF RID: 16591 RVA: 0x0012AC98 File Offset: 0x00128E98
		public override Node Visit(DbCrossJoinExpression e)
		{
			Check.NotNull<DbCrossJoinExpression>(e, "e");
			return this.VisitJoin(e, e.Inputs, null);
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x0012ACB4 File Offset: 0x00128EB4
		public override Node Visit(DbJoinExpression e)
		{
			Check.NotNull<DbJoinExpression>(e, "e");
			return this.VisitJoin(e, new List<DbExpressionBinding>
			{
				e.Left,
				e.Right
			}, e.JoinCondition);
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x0012ACFC File Offset: 0x00128EFC
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbJoinExpression")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CrossJoinOps")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "JoinType")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "JoinOp")]
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

		// Token: 0x060040D2 RID: 16594 RVA: 0x0012AE90 File Offset: 0x00129090
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbExpressionKind")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbApplyExpression")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		public override Node Visit(DbApplyExpression e)
		{
			Check.NotNull<DbApplyExpression>(e, "e");
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

		// Token: 0x060040D3 RID: 16595 RVA: 0x0012AF64 File Offset: 0x00129164
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbGroupByExpression")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbAggregate")]
		public override Node Visit(DbGroupByExpression e)
		{
			Check.NotNull<DbGroupByExpression>(e, "e");
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

		// Token: 0x060040D4 RID: 16596 RVA: 0x0012B1C8 File Offset: 0x001293C8
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ScalarOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GroupBy")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x060040D5 RID: 16597 RVA: 0x0012B264 File Offset: 0x00129464
		private Node ProcessFunctionAggregate(DbFunctionAggregate funcAgg, IList<Node> argNodes, out Var aggVar)
		{
			Node definingExpr = this._iqtCommand.CreateNode(this._iqtCommand.CreateAggregateOp(funcAgg.Function, funcAgg.Distinct), argNodes);
			return this._iqtCommand.CreateVarDefNode(definingExpr, out aggVar);
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x0012B2A4 File Offset: 0x001294A4
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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
				PlanCompiler.Assert(list.Count == list2.Count, "The flattened keys lists should have the same number of elements");
				Node node4 = null;
				for (int j = 0; j < list.Count; j++)
				{
					Node node5 = list[j];
					Node node6 = list2[j];
					Node node7;
					if (this._useDatabaseNullSemantics)
					{
						node7 = this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.Or), this._iqtCommand.CreateNode(this._iqtCommand.CreateComparisonOp(OpType.EQ, false), node5, node6), this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.And), this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.IsNull), OpCopier.Copy(this._iqtCommand, node5)), this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.IsNull), OpCopier.Copy(this._iqtCommand, node6))));
					}
					else
					{
						node7 = this._iqtCommand.CreateNode(this._iqtCommand.CreateComparisonOp(OpType.EQ, false), node5, node6);
					}
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

		// Token: 0x060040D7 RID: 16599 RVA: 0x0012B540 File Offset: 0x00129740
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "RowType")]
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

		// Token: 0x060040D8 RID: 16600 RVA: 0x0012B5E0 File Offset: 0x001297E0
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbSortClause")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SortKey")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SortClauses")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "non-ScalarOp")]
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

		// Token: 0x060040D9 RID: 16601 RVA: 0x0012B718 File Offset: 0x00129918
		public override Node Visit(DbSkipExpression expression)
		{
			Check.NotNull<DbSkipExpression>(expression, "expression");
			List<SortKey> sortKeys = new List<SortKey>();
			Var value;
			Node arg = this.VisitSortArguments(expression.Input, expression.SortOrder, sortKeys, out value);
			Node arg2 = this.VisitExprAsScalar(expression.Count);
			Node node = this._iqtCommand.CreateNode(this._iqtCommand.CreateConstrainedSortOp(sortKeys), arg, arg2, this._iqtCommand.CreateNode(this._iqtCommand.CreateNullOp(this._iqtCommand.IntegerType)));
			this._varMap[node] = value;
			return node;
		}

		// Token: 0x060040DA RID: 16602 RVA: 0x0012B7A8 File Offset: 0x001299A8
		public override Node Visit(DbSortExpression e)
		{
			Check.NotNull<DbSortExpression>(e, "e");
			List<SortKey> sortKeys = new List<SortKey>();
			Var value;
			Node arg = this.VisitSortArguments(e.Input, e.SortOrder, sortKeys, out value);
			SortOp op = this._iqtCommand.CreateSortOp(sortKeys);
			Node node = this._iqtCommand.CreateNode(op, arg);
			this._varMap[node] = value;
			return node;
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x0012B80C File Offset: 0x00129A0C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbExpressionKind")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbQuantifierExpression")]
		public override Node Visit(DbQuantifierExpression e)
		{
			Check.NotNull<DbQuantifierExpression>(e, "e");
			PlanCompiler.Assert(DbExpressionKind.Any == e.ExpressionKind || DbExpressionKind.All == e.ExpressionKind, "Invalid DbExpressionKind in DbQuantifierExpression");
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

		// Token: 0x04001813 RID: 6163
		private static readonly Dictionary<DbExpressionKind, OpType> _opMap = ITreeGenerator.InitializeExpressionKindToOpTypeMap();

		// Token: 0x04001814 RID: 6164
		private readonly bool _useDatabaseNullSemantics;

		// Token: 0x04001815 RID: 6165
		private readonly Command _iqtCommand;

		// Token: 0x04001816 RID: 6166
		private readonly Stack<ITreeGenerator.CqtVariableScope> _varScopes = new Stack<ITreeGenerator.CqtVariableScope>();

		// Token: 0x04001817 RID: 6167
		private readonly Dictionary<Node, Var> _varMap = new Dictionary<Node, Var>();

		// Token: 0x04001818 RID: 6168
		private readonly Stack<EdmFunction> _functionExpansions = new Stack<EdmFunction>();

		// Token: 0x04001819 RID: 6169
		private readonly Dictionary<DbExpression, bool> _functionsIsPredicateFlag = new Dictionary<DbExpression, bool>();

		// Token: 0x0400181A RID: 6170
		private readonly HashSet<DbFilterExpression> _processedIsOfFilters = new HashSet<DbFilterExpression>();

		// Token: 0x0400181B RID: 6171
		private readonly HashSet<DbTreatExpression> _fakeTreats = new HashSet<DbTreatExpression>();

		// Token: 0x0400181C RID: 6172
		private readonly DiscriminatorMap _discriminatorMap;

		// Token: 0x0400181D RID: 6173
		private readonly DbProjectExpression _discriminatedViewTopProject;

		// Token: 0x02000678 RID: 1656
		private abstract class CqtVariableScope
		{
			// Token: 0x060040DE RID: 16606
			internal abstract bool Contains(string varName);

			// Token: 0x170009EF RID: 2543
			internal abstract Node this[string varName]
			{
				get;
			}

			// Token: 0x060040E0 RID: 16608
			internal abstract bool IsPredicate(string varName);
		}

		// Token: 0x02000679 RID: 1657
		private class ExpressionBindingScope : ITreeGenerator.CqtVariableScope
		{
			// Token: 0x060040E2 RID: 16610 RVA: 0x0012B94F File Offset: 0x00129B4F
			internal ExpressionBindingScope(Command iqtTree, string name, Var iqtVar)
			{
				this._tree = iqtTree;
				this._varName = name;
				this._var = iqtVar;
			}

			// Token: 0x060040E3 RID: 16611 RVA: 0x0012B96C File Offset: 0x00129B6C
			internal override bool Contains(string name)
			{
				return this._varName == name;
			}

			// Token: 0x170009F0 RID: 2544
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
			internal override Node this[string name]
			{
				get
				{
					PlanCompiler.Assert(name == this._varName, "huh?");
					return this._tree.CreateNode(this._tree.CreateVarRefOp(this._var));
				}
			}

			// Token: 0x060040E5 RID: 16613 RVA: 0x0012B9AE File Offset: 0x00129BAE
			internal override bool IsPredicate(string varName)
			{
				return false;
			}

			// Token: 0x170009F1 RID: 2545
			// (get) Token: 0x060040E6 RID: 16614 RVA: 0x0012B9B1 File Offset: 0x00129BB1
			internal Var ScopeVar
			{
				get
				{
					return this._var;
				}
			}

			// Token: 0x0400181F RID: 6175
			private readonly Command _tree;

			// Token: 0x04001820 RID: 6176
			private readonly string _varName;

			// Token: 0x04001821 RID: 6177
			private readonly Var _var;
		}

		// Token: 0x0200067A RID: 1658
		private sealed class LambdaScope : ITreeGenerator.CqtVariableScope
		{
			// Token: 0x060040E7 RID: 16615 RVA: 0x0012B9B9 File Offset: 0x00129BB9
			internal LambdaScope(ITreeGenerator treeGen, Command command, Dictionary<string, Tuple<Node, bool>> args)
			{
				this._treeGen = treeGen;
				this._command = command;
				this._arguments = args;
				this._referencedArgs = new Dictionary<Node, bool>(this._arguments.Count);
			}

			// Token: 0x060040E8 RID: 16616 RVA: 0x0012B9EC File Offset: 0x00129BEC
			internal override bool Contains(string name)
			{
				return this._arguments.ContainsKey(name);
			}

			// Token: 0x170009F2 RID: 2546
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
			[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "LambdaScope")]
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

			// Token: 0x060040EA RID: 16618 RVA: 0x0012BA93 File Offset: 0x00129C93
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
			[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "LambdaScope")]
			internal override bool IsPredicate(string name)
			{
				PlanCompiler.Assert(this._arguments.ContainsKey(name), "LambdaScope indexer called for invalid Var");
				return this._arguments[name].Item2;
			}

			// Token: 0x060040EB RID: 16619 RVA: 0x0012BABC File Offset: 0x00129CBC
			[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OpCopier")]
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

			// Token: 0x04001822 RID: 6178
			private readonly ITreeGenerator _treeGen;

			// Token: 0x04001823 RID: 6179
			private readonly Command _command;

			// Token: 0x04001824 RID: 6180
			private readonly Dictionary<string, Tuple<Node, bool>> _arguments;

			// Token: 0x04001825 RID: 6181
			private readonly Dictionary<Node, bool> _referencedArgs;
		}

		// Token: 0x0200067B RID: 1659
		// (Invoke) Token: 0x060040ED RID: 16621
		private delegate Node VisitExprDelegate(DbExpression e);

		// Token: 0x0200067C RID: 1660
		private class IsOfFilter
		{
			// Token: 0x060040F0 RID: 16624 RVA: 0x0012BB63 File Offset: 0x00129D63
			internal IsOfFilter(DbIsOfExpression template)
			{
				this.requiredType = template.OfType;
				this.isExact = (template.ExpressionKind == DbExpressionKind.IsOfOnly);
			}

			// Token: 0x060040F1 RID: 16625 RVA: 0x0012BB87 File Offset: 0x00129D87
			internal IsOfFilter(DbOfTypeExpression template)
			{
				this.requiredType = template.OfType;
				this.isExact = (template.ExpressionKind == DbExpressionKind.OfTypeOnly);
			}

			// Token: 0x060040F2 RID: 16626 RVA: 0x0012BBAB File Offset: 0x00129DAB
			private IsOfFilter(TypeUsage required, bool exact)
			{
				this.requiredType = required;
				this.isExact = exact;
			}

			// Token: 0x060040F3 RID: 16627 RVA: 0x0012BBC4 File Offset: 0x00129DC4
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
						if (object.ReferenceEquals(typeUsage, this.requiredType) && this.isExact)
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

			// Token: 0x060040F4 RID: 16628 RVA: 0x0012BCF1 File Offset: 0x00129EF1
			internal ITreeGenerator.IsOfFilter Merge(DbIsOfExpression other)
			{
				return this.Merge(other.OfType, other.ExpressionKind == DbExpressionKind.IsOfOnly);
			}

			// Token: 0x060040F5 RID: 16629 RVA: 0x0012BD09 File Offset: 0x00129F09
			internal ITreeGenerator.IsOfFilter Merge(DbOfTypeExpression other)
			{
				return this.Merge(other.OfType, other.ExpressionKind == DbExpressionKind.OfTypeOnly);
			}

			// Token: 0x060040F6 RID: 16630 RVA: 0x0012BE2C File Offset: 0x0012A02C
			internal IEnumerable<KeyValuePair<TypeUsage, bool>> ToEnumerable()
			{
				for (ITreeGenerator.IsOfFilter currentFilter = this; currentFilter != null; currentFilter = currentFilter.next)
				{
					yield return new KeyValuePair<TypeUsage, bool>(currentFilter.requiredType, currentFilter.isExact);
				}
				yield break;
			}

			// Token: 0x04001826 RID: 6182
			private readonly TypeUsage requiredType;

			// Token: 0x04001827 RID: 6183
			private readonly bool isExact;

			// Token: 0x04001828 RID: 6184
			private ITreeGenerator.IsOfFilter next;
		}
	}
}
