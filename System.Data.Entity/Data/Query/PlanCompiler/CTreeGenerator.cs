using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;
using System.Globalization;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200004D RID: 77
	internal class CTreeGenerator : BasicOpVisitorOfT<DbExpression>
	{
		// Token: 0x06000614 RID: 1556 RVA: 0x0001A1E0 File Offset: 0x000183E0
		internal static DbCommandTree Generate(Command itree, Node toConvert)
		{
			CTreeGenerator ctreeGenerator = new CTreeGenerator(itree, toConvert);
			return ctreeGenerator._queryTree;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001A1FC File Offset: 0x000183FC
		private CTreeGenerator(Command itree, Node toConvert)
		{
			this._iqtCommand = itree;
			DbExpression query = base.VisitNode(toConvert);
			this._queryTree = DbQueryCommandTree.FromValidExpression(itree.MetadataWorkspace, DataSpace.SSpace, query);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001A34D File Offset: 0x0001854D
		private void AssertRelOp(DbExpression expr)
		{
			PlanCompiler.Assert(this._relOpState.ContainsKey(expr), "not a relOp expression?");
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001A368 File Offset: 0x00018568
		private CTreeGenerator.RelOpInfo PublishRelOp(string name, DbExpression expr, CTreeGenerator.VarInfoList publishedVars)
		{
			CTreeGenerator.RelOpInfo relOpInfo = new CTreeGenerator.RelOpInfo(name, expr, publishedVars);
			this._relOpState.Add(expr, relOpInfo);
			return relOpInfo;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001A38C File Offset: 0x0001858C
		private CTreeGenerator.RelOpInfo ConsumeRelOp(DbExpression expr)
		{
			this.AssertRelOp(expr);
			CTreeGenerator.RelOpInfo result = this._relOpState[expr];
			this._relOpState.Remove(expr);
			return result;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001A3BC File Offset: 0x000185BC
		private CTreeGenerator.RelOpInfo VisitAsRelOp(Node inputNode)
		{
			PlanCompiler.Assert(inputNode.Op is RelOp, "Non-RelOp used as DbExpressionBinding Input");
			DbExpression expr = base.VisitNode(inputNode);
			return this.ConsumeRelOp(expr);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001A3F0 File Offset: 0x000185F0
		private void PushExpressionBindingScope(CTreeGenerator.RelOpInfo inputState)
		{
			PlanCompiler.Assert(inputState != null && inputState.PublisherName != null && inputState.PublishedVars != null, "Invalid RelOpInfo produced by DbExpressionBinding Input");
			this._bindingScopes.Push(inputState);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001A420 File Offset: 0x00018620
		private CTreeGenerator.RelOpInfo EnterExpressionBindingScope(Node inputNode, bool pushScope)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.VisitAsRelOp(inputNode);
			if (pushScope)
			{
				this.PushExpressionBindingScope(relOpInfo);
			}
			return relOpInfo;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001A440 File Offset: 0x00018640
		private CTreeGenerator.RelOpInfo EnterExpressionBindingScope(Node inputNode)
		{
			return this.EnterExpressionBindingScope(inputNode, true);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001A44C File Offset: 0x0001864C
		private void ExitExpressionBindingScope(CTreeGenerator.RelOpInfo scope, bool wasPushed)
		{
			if (wasPushed)
			{
				PlanCompiler.Assert(this._bindingScopes.Count > 0, "ExitExpressionBindingScope called on empty ExpressionBindingScope stack");
				CTreeGenerator.RelOpInfo relOpInfo = (CTreeGenerator.RelOpInfo)this._bindingScopes.Pop();
				PlanCompiler.Assert(relOpInfo == scope, "ExitExpressionBindingScope called on incorrect expression");
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001A493 File Offset: 0x00018693
		private void ExitExpressionBindingScope(CTreeGenerator.RelOpInfo scope)
		{
			this.ExitExpressionBindingScope(scope, true);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001A4A0 File Offset: 0x000186A0
		private CTreeGenerator.GroupByScope EnterGroupByScope(Node inputNode)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.VisitAsRelOp(inputNode);
			string publisherName = relOpInfo.PublisherName;
			string groupVarName = string.Format(CultureInfo.InvariantCulture, "{0}Group", new object[]
			{
				publisherName
			});
			DbGroupExpressionBinding binding = relOpInfo.CreateBinding().Expression.GroupBindAs(publisherName, groupVarName);
			CTreeGenerator.GroupByScope groupByScope = new CTreeGenerator.GroupByScope(binding, relOpInfo.PublishedVars);
			this._bindingScopes.Push(groupByScope);
			return groupByScope;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001A508 File Offset: 0x00018708
		private void ExitGroupByScope(CTreeGenerator.GroupByScope scope)
		{
			PlanCompiler.Assert(this._bindingScopes.Count > 0, "ExitGroupByScope called on empty ExpressionBindingScope stack");
			CTreeGenerator.GroupByScope groupByScope = (CTreeGenerator.GroupByScope)this._bindingScopes.Pop();
			PlanCompiler.Assert(groupByScope == scope, "ExitGroupByScope called on incorrect expression");
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001A54C File Offset: 0x0001874C
		private void EnterVarDefScope(List<Node> varDefNodes)
		{
			Dictionary<Var, DbExpression> dictionary = new Dictionary<Var, DbExpression>();
			foreach (Node node in varDefNodes)
			{
				VarDefOp varDefOp = node.Op as VarDefOp;
				PlanCompiler.Assert(varDefOp != null, "VarDefListOp contained non-VarDefOp child node");
				PlanCompiler.Assert(varDefOp.Var is ComputedVar, "VarDefOp defined non-Computed Var");
				dictionary.Add(varDefOp.Var, base.VisitNode(node.Child0));
			}
			this._varScopes.Push(new CTreeGenerator.VarDefScope(dictionary));
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001A5F4 File Offset: 0x000187F4
		private void EnterVarDefListScope(Node varDefListNode)
		{
			PlanCompiler.Assert(varDefListNode.Op is VarDefListOp, "EnterVarDefListScope called with non-VarDefListOp");
			this.EnterVarDefScope(varDefListNode.Children);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001A61A File Offset: 0x0001881A
		private void ExitVarDefScope()
		{
			PlanCompiler.Assert(this._varScopes.Count > 0, "ExitVarDefScope called on empty VarDefScope stack");
			this._varScopes.Pop();
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001A640 File Offset: 0x00018840
		private DbExpression ResolveVar(Var referencedVar)
		{
			DbExpression dbExpression = null;
			ParameterVar parameterVar = referencedVar as ParameterVar;
			if (parameterVar != null)
			{
				DbParameterReferenceExpression dbParameterReferenceExpression;
				if (!this._addedParams.TryGetValue(parameterVar, out dbParameterReferenceExpression))
				{
					dbParameterReferenceExpression = parameterVar.Type.Parameter(parameterVar.ParameterName);
					this._addedParams[parameterVar] = dbParameterReferenceExpression;
				}
				dbExpression = dbParameterReferenceExpression;
			}
			else
			{
				ComputedVar computedVar = referencedVar as ComputedVar;
				if (computedVar != null && this._varScopes.Count > 0 && !this._varScopes.Peek().TryResolveVar(computedVar, out dbExpression))
				{
					dbExpression = null;
				}
				if (dbExpression == null)
				{
					DbExpression dbExpression2 = null;
					foreach (CTreeGenerator.IqtVarScope iqtVarScope in this._bindingScopes)
					{
						if (iqtVarScope.TryResolveVar(referencedVar, out dbExpression2))
						{
							dbExpression = dbExpression2;
							break;
						}
					}
				}
			}
			PlanCompiler.Assert(dbExpression != null, string.Format(CultureInfo.InvariantCulture, "Unresolvable Var used in Command: VarType={0}, Id={1}", new object[]
			{
				Enum.GetName(typeof(VarType), referencedVar.VarType),
				referencedVar.Id
			}));
			return dbExpression;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001A760 File Offset: 0x00018960
		private static void AssertBinary(Node n)
		{
			PlanCompiler.Assert(2 == n.Children.Count, string.Format(CultureInfo.InvariantCulture, "Non-Binary {0} encountered", new object[]
			{
				n.Op.GetType().Name
			}));
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001A79D File Offset: 0x0001899D
		private DbExpression VisitChild(Node n, int index)
		{
			PlanCompiler.Assert(n.Children.Count > index, "VisitChild called with invalid index");
			return base.VisitNode(n.Children[index]);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001A7CC File Offset: 0x000189CC
		private new List<DbExpression> VisitChildren(Node n)
		{
			List<DbExpression> list = new List<DbExpression>();
			foreach (Node n2 in n.Children)
			{
				list.Add(base.VisitNode(n2));
			}
			return list;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001A82C File Offset: 0x00018A2C
		protected override DbExpression VisitConstantOp(ConstantBaseOp op, Node n)
		{
			return op.Type.Constant(op.Value);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001A83F File Offset: 0x00018A3F
		public override DbExpression Visit(ConstantOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001A83F File Offset: 0x00018A3F
		public override DbExpression Visit(InternalConstantOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001A849 File Offset: 0x00018A49
		public override DbExpression Visit(NullOp op, Node n)
		{
			return op.Type.Null();
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001A83F File Offset: 0x00018A3F
		public override DbExpression Visit(NullSentinelOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001A856 File Offset: 0x00018A56
		public override DbExpression Visit(ConstantPredicateOp op, Node n)
		{
			return DbExpressionBuilder.True.Equal(op.IsTrue ? DbExpressionBuilder.True : DbExpressionBuilder.False);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001A876 File Offset: 0x00018A76
		public override DbExpression Visit(FunctionOp op, Node n)
		{
			return op.Function.Invoke(this.VisitChildren(n));
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(PropertyOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(RelPropertyOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001A88C File Offset: 0x00018A8C
		public override DbExpression Visit(ArithmeticOp op, Node n)
		{
			DbExpression dbExpression;
			if (OpType.UnaryMinus == op.OpType)
			{
				dbExpression = this.VisitChild(n, 0).UnaryMinus();
			}
			else
			{
				DbExpression left = this.VisitChild(n, 0);
				DbExpression right = this.VisitChild(n, 1);
				switch (op.OpType)
				{
				case OpType.Plus:
					dbExpression = left.Plus(right);
					break;
				case OpType.Minus:
					dbExpression = left.Minus(right);
					break;
				case OpType.Multiply:
					dbExpression = left.Multiply(right);
					break;
				case OpType.Divide:
					dbExpression = left.Divide(right);
					break;
				case OpType.Modulo:
					dbExpression = left.Modulo(right);
					break;
				default:
					dbExpression = null;
					break;
				}
			}
			PlanCompiler.Assert(dbExpression != null, string.Format(CultureInfo.InvariantCulture, "ArithmeticOp OpType not recognized: {0}", new object[]
			{
				Enum.GetName(typeof(OpType), op.OpType)
			}));
			return dbExpression;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001A960 File Offset: 0x00018B60
		public override DbExpression Visit(CaseOp op, Node n)
		{
			int num = n.Children.Count;
			PlanCompiler.Assert(num > 1, "Invalid CaseOp: At least 2 child Nodes (1 When/Then pair) must be present");
			List<DbExpression> list = new List<DbExpression>();
			List<DbExpression> list2 = new List<DbExpression>();
			DbExpression elseExpression;
			if (n.Children.Count % 2 == 0)
			{
				elseExpression = op.Type.Null();
			}
			else
			{
				num--;
				elseExpression = this.VisitChild(n, n.Children.Count - 1);
			}
			for (int i = 0; i < num; i += 2)
			{
				list.Add(this.VisitChild(n, i));
				list2.Add(this.VisitChild(n, i + 1));
			}
			return DbExpressionBuilder.Case(list, list2, elseExpression);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001AA04 File Offset: 0x00018C04
		public override DbExpression Visit(ComparisonOp op, Node n)
		{
			CTreeGenerator.AssertBinary(n);
			DbExpression left = this.VisitChild(n, 0);
			DbExpression right = this.VisitChild(n, 1);
			DbExpression dbExpression;
			switch (op.OpType)
			{
			case OpType.GT:
				dbExpression = left.GreaterThan(right);
				break;
			case OpType.GE:
				dbExpression = left.GreaterThanOrEqual(right);
				break;
			case OpType.LE:
				dbExpression = left.LessThanOrEqual(right);
				break;
			case OpType.LT:
				dbExpression = left.LessThan(right);
				break;
			case OpType.EQ:
				dbExpression = left.Equal(right);
				break;
			case OpType.NE:
				dbExpression = left.NotEqual(right);
				break;
			default:
				dbExpression = null;
				break;
			}
			PlanCompiler.Assert(dbExpression != null, string.Format(CultureInfo.InvariantCulture, "ComparisonOp OpType not recognized: {0}", new object[]
			{
				Enum.GetName(typeof(OpType), op.OpType)
			}));
			return dbExpression;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001AAD0 File Offset: 0x00018CD0
		public override DbExpression Visit(ConditionalOp op, Node n)
		{
			DbExpression dbExpression = this.VisitChild(n, 0);
			DbExpression dbExpression2;
			switch (op.OpType)
			{
			case OpType.And:
				dbExpression2 = dbExpression.And(this.VisitChild(n, 1));
				break;
			case OpType.Or:
				dbExpression2 = dbExpression.Or(this.VisitChild(n, 1));
				break;
			case OpType.Not:
			{
				DbNotExpression dbNotExpression = dbExpression as DbNotExpression;
				if (dbNotExpression != null)
				{
					dbExpression2 = dbNotExpression.Argument;
				}
				else
				{
					dbExpression2 = dbExpression.Not();
				}
				break;
			}
			case OpType.IsNull:
				dbExpression2 = dbExpression.IsNull();
				break;
			default:
				dbExpression2 = null;
				break;
			}
			PlanCompiler.Assert(dbExpression2 != null, string.Format(CultureInfo.InvariantCulture, "ConditionalOp OpType not recognized: {0}", new object[]
			{
				Enum.GetName(typeof(OpType), op.OpType)
			}));
			return dbExpression2;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001AB8F File Offset: 0x00018D8F
		public override DbExpression Visit(LikeOp op, Node n)
		{
			return this.VisitChild(n, 0).Like(this.VisitChild(n, 1), this.VisitChild(n, 2));
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001ABAE File Offset: 0x00018DAE
		public override DbExpression Visit(AggregateOp op, Node n)
		{
			PlanCompiler.Assert(false, "AggregateOp encountered outside of GroupByOp");
			throw EntityUtil.NotSupported(Strings.Iqt_CTGen_UnexpectedAggregate);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(NavigateOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(NewEntityOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(NewInstanceOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(DiscriminatedNewEntityOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(NewMultisetOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(NewRecordOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(RefOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001ABC5 File Offset: 0x00018DC5
		public override DbExpression Visit(VarRefOp op, Node n)
		{
			return this.ResolveVar(op.Var);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(TreatOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001ABD3 File Offset: 0x00018DD3
		public override DbExpression Visit(CastOp op, Node n)
		{
			return this.VisitChild(n, 0).CastTo(op.Type);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001ABE8 File Offset: 0x00018DE8
		public override DbExpression Visit(SoftCastOp op, Node n)
		{
			return this.VisitChild(n, 0);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001ABF2 File Offset: 0x00018DF2
		public override DbExpression Visit(IsOfOp op, Node n)
		{
			if (op.IsOfOnly)
			{
				return this.VisitChild(n, 0).IsOfOnly(op.IsOfType);
			}
			return this.VisitChild(n, 0).IsOf(op.IsOfType);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001AC24 File Offset: 0x00018E24
		public override DbExpression Visit(ExistsOp op, Node n)
		{
			DbExpression dbExpression = base.VisitNode(n.Child0);
			this.ConsumeRelOp(dbExpression);
			return dbExpression.IsEmpty().Not();
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001AC54 File Offset: 0x00018E54
		public override DbExpression Visit(ElementOp op, Node n)
		{
			DbExpression dbExpression = base.VisitNode(n.Child0);
			this.AssertRelOp(dbExpression);
			this.ConsumeRelOp(dbExpression);
			return DbExpressionBuilder.CreateElementExpressionUnwrapSingleProperty(dbExpression);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(GetRefKeyOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(GetEntityRefOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(CollectOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001AC88 File Offset: 0x00018E88
		private static string GenerateNameForVar(Var projectedVar, Dictionary<string, AliasGenerator> aliasMap, AliasGenerator defaultAliasGenerator, Dictionary<string, string> alreadyUsedNames)
		{
			string text;
			AliasGenerator aliasGenerator;
			if (projectedVar.TryGetName(out text))
			{
				if (!aliasMap.TryGetValue(text, out aliasGenerator))
				{
					aliasGenerator = new AliasGenerator(text);
					aliasMap[text] = aliasGenerator;
				}
				else
				{
					text = aliasGenerator.Next();
				}
			}
			else
			{
				aliasGenerator = defaultAliasGenerator;
				text = aliasGenerator.Next();
			}
			while (alreadyUsedNames.ContainsKey(text))
			{
				text = aliasGenerator.Next();
			}
			alreadyUsedNames[text] = text;
			return text;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001ACE8 File Offset: 0x00018EE8
		private DbExpression CreateProject(CTreeGenerator.RelOpInfo sourceInfo, IEnumerable<Var> outputVars)
		{
			CTreeGenerator.VarInfoList varInfoList = new CTreeGenerator.VarInfoList();
			List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>();
			AliasGenerator defaultAliasGenerator = new AliasGenerator("C");
			Dictionary<string, AliasGenerator> aliasMap = new Dictionary<string, AliasGenerator>(StringComparer.InvariantCultureIgnoreCase);
			Dictionary<string, string> alreadyUsedNames = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
			foreach (Var var in outputVars)
			{
				string text = CTreeGenerator.GenerateNameForVar(var, aliasMap, defaultAliasGenerator, alreadyUsedNames);
				DbExpression value = this.ResolveVar(var);
				list.Add(new KeyValuePair<string, DbExpression>(text, value));
				CTreeGenerator.VarInfo varInfo = new CTreeGenerator.VarInfo(var);
				varInfo.PrependProperty(text);
				varInfoList.Add(varInfo);
			}
			DbExpression dbExpression = sourceInfo.CreateBinding().Project(DbExpressionBuilder.NewRow(list));
			this.PublishRelOp(this._projectAliases.Next(), dbExpression, varInfoList);
			return dbExpression;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001ADC8 File Offset: 0x00018FC8
		private static CTreeGenerator.VarInfoList GetTableVars(Table targetTable)
		{
			CTreeGenerator.VarInfoList varInfoList = new CTreeGenerator.VarInfoList();
			if (targetTable.TableMetadata.Flattened)
			{
				for (int i = 0; i < targetTable.Columns.Count; i++)
				{
					CTreeGenerator.VarInfo varInfo = new CTreeGenerator.VarInfo(targetTable.Columns[i]);
					varInfo.PrependProperty(targetTable.TableMetadata.Columns[i].Name);
					varInfoList.Add(varInfo);
				}
			}
			else
			{
				varInfoList.Add(new CTreeGenerator.VarInfo(targetTable.Columns[0]));
			}
			return varInfoList;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001AE50 File Offset: 0x00019050
		public override DbExpression Visit(ScanTableOp op, Node n)
		{
			PlanCompiler.Assert(op.Table.TableMetadata.Extent != null, "Invalid TableMetadata used in ScanTableOp - no Extent specified");
			PlanCompiler.Assert(!n.HasChild0, "views are not expected here");
			CTreeGenerator.VarInfoList tableVars = CTreeGenerator.GetTableVars(op.Table);
			DbExpression dbExpression = op.Table.TableMetadata.Extent.Scan();
			this.PublishRelOp(this._extentAliases.Next(), dbExpression, tableVars);
			return dbExpression;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(ScanViewOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001AEC4 File Offset: 0x000190C4
		public override DbExpression Visit(UnnestOp op, Node n)
		{
			PlanCompiler.Assert(n.Child0.Op.OpType == OpType.VarDef, "an unnest's child must be a VarDef");
			Node child = n.Child0.Child0;
			DbExpression dbExpression = child.Op.Accept<DbExpression>(this, child);
			PlanCompiler.Assert(dbExpression.ResultType.EdmType.BuiltInTypeKind == BuiltInTypeKind.CollectionType, "the input to unnest must yield a collection after plan compilation");
			CTreeGenerator.VarInfoList tableVars = CTreeGenerator.GetTableVars(op.Table);
			this.PublishRelOp(this._extentAliases.Next(), dbExpression, tableVars);
			return dbExpression;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001AF48 File Offset: 0x00019148
		private CTreeGenerator.RelOpInfo BuildEmptyProjection(Node relOpNode)
		{
			if (relOpNode.Op.OpType == OpType.Project)
			{
				relOpNode = relOpNode.Child0;
			}
			CTreeGenerator.RelOpInfo relOpInfo = this.EnterExpressionBindingScope(relOpNode);
			DbExpression value = DbExpressionBuilder.Constant(1);
			List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>();
			list.Add(new KeyValuePair<string, DbExpression>("C0", value));
			DbExpression expr = relOpInfo.CreateBinding().Project(DbExpressionBuilder.NewRow(list));
			this.PublishRelOp(this._projectAliases.Next(), expr, new CTreeGenerator.VarInfoList());
			this.ExitExpressionBindingScope(relOpInfo);
			return this.ConsumeRelOp(expr);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001AFD4 File Offset: 0x000191D4
		private CTreeGenerator.RelOpInfo BuildProjection(Node relOpNode, IEnumerable<Var> projectionVars)
		{
			ProjectOp projectOp = relOpNode.Op as ProjectOp;
			DbExpression expr;
			if (projectOp != null)
			{
				expr = this.VisitProject(projectOp, relOpNode, projectionVars);
			}
			else
			{
				CTreeGenerator.RelOpInfo relOpInfo = this.EnterExpressionBindingScope(relOpNode);
				expr = this.CreateProject(relOpInfo, projectionVars);
				this.ExitExpressionBindingScope(relOpInfo);
			}
			return this.ConsumeRelOp(expr);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001B020 File Offset: 0x00019220
		private DbExpression VisitProject(ProjectOp op, Node n, IEnumerable<Var> varList)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.EnterExpressionBindingScope(n.Child0);
			if (n.Children.Count > 1)
			{
				this.EnterVarDefListScope(n.Child1);
			}
			DbExpression result = this.CreateProject(relOpInfo, varList);
			if (n.Children.Count > 1)
			{
				this.ExitVarDefScope();
			}
			this.ExitExpressionBindingScope(relOpInfo);
			return result;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001B079 File Offset: 0x00019279
		public override DbExpression Visit(ProjectOp op, Node n)
		{
			return this.VisitProject(op, n, op.Outputs);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001B08C File Offset: 0x0001928C
		public override DbExpression Visit(FilterOp op, Node n)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.EnterExpressionBindingScope(n.Child0);
			DbExpression dbExpression = base.VisitNode(n.Child1);
			PlanCompiler.Assert(TypeSemantics.IsPrimitiveType(dbExpression.ResultType, PrimitiveTypeKind.Boolean), "Invalid FilterOp Predicate (non-ScalarOp or non-Boolean result)");
			DbExpression dbExpression2 = relOpInfo.CreateBinding().Filter(dbExpression);
			this.ExitExpressionBindingScope(relOpInfo);
			this.PublishRelOp(this._filterAliases.Next(), dbExpression2, relOpInfo.PublishedVars);
			return dbExpression2;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001B0F8 File Offset: 0x000192F8
		private List<DbSortClause> VisitSortKeys(IList<System.Data.Query.InternalTrees.SortKey> sortKeys)
		{
			VarVec varVec = this._iqtCommand.CreateVarVec();
			List<DbSortClause> list = new List<DbSortClause>();
			foreach (System.Data.Query.InternalTrees.SortKey sortKey in sortKeys)
			{
				if (!varVec.IsSet(sortKey.Var))
				{
					varVec.Set(sortKey.Var);
					DbExpression key = this.ResolveVar(sortKey.Var);
					DbSortClause item;
					if (!string.IsNullOrEmpty(sortKey.Collation))
					{
						item = (sortKey.AscendingSort ? key.ToSortClause(sortKey.Collation) : key.ToSortClauseDescending(sortKey.Collation));
					}
					else
					{
						item = (sortKey.AscendingSort ? key.ToSortClause() : key.ToSortClauseDescending());
					}
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001B1D4 File Offset: 0x000193D4
		public override DbExpression Visit(SortOp op, Node n)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.EnterExpressionBindingScope(n.Child0);
			PlanCompiler.Assert(!n.HasChild1, "SortOp can have only one child");
			DbExpression dbExpression = relOpInfo.CreateBinding().Sort(this.VisitSortKeys(op.Keys));
			this.ExitExpressionBindingScope(relOpInfo);
			this.PublishRelOp(this._sortAliases.Next(), dbExpression, relOpInfo.PublishedVars);
			return dbExpression;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001B23A File Offset: 0x0001943A
		private DbExpression CreateLimitExpression(DbExpression argument, DbExpression limit, bool withTies)
		{
			PlanCompiler.Assert(!withTies, "Limit with Ties is not currently supported");
			return argument.Limit(limit);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001B254 File Offset: 0x00019454
		public override DbExpression Visit(ConstrainedSortOp op, Node n)
		{
			DbExpression dbExpression = null;
			string name = null;
			bool flag = OpType.Null == n.Child1.Op.OpType;
			bool flag2 = OpType.Null == n.Child2.Op.OpType;
			PlanCompiler.Assert(!flag || !flag2, "ConstrainedSortOp with no Skip Count and no Limit?");
			CTreeGenerator.RelOpInfo relOpInfo;
			if (op.Keys.Count == 0)
			{
				PlanCompiler.Assert(flag, "ConstrainedSortOp without SortKeys cannot have Skip Count");
				DbExpression dbExpression2 = base.VisitNode(n.Child0);
				relOpInfo = this.ConsumeRelOp(dbExpression2);
				dbExpression = this.CreateLimitExpression(dbExpression2, base.VisitNode(n.Child2), op.WithTies);
				name = this._limitAliases.Next();
			}
			else
			{
				relOpInfo = this.EnterExpressionBindingScope(n.Child0);
				List<DbSortClause> sortOrder = this.VisitSortKeys(op.Keys);
				this.ExitExpressionBindingScope(relOpInfo);
				if (!flag && !flag2)
				{
					dbExpression = this.CreateLimitExpression(relOpInfo.CreateBinding().Skip(sortOrder, this.VisitChild(n, 1)), this.VisitChild(n, 2), op.WithTies);
					name = this._limitAliases.Next();
				}
				else if (!flag && flag2)
				{
					dbExpression = relOpInfo.CreateBinding().Skip(sortOrder, this.VisitChild(n, 1));
					name = this._skipAliases.Next();
				}
				else if (flag && !flag2)
				{
					dbExpression = this.CreateLimitExpression(relOpInfo.CreateBinding().Sort(sortOrder), this.VisitChild(n, 2), op.WithTies);
					name = this._limitAliases.Next();
				}
			}
			this.PublishRelOp(name, dbExpression, relOpInfo.PublishedVars);
			return dbExpression;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001B3D4 File Offset: 0x000195D4
		public override DbExpression Visit(GroupByOp op, Node n)
		{
			CTreeGenerator.VarInfoList varInfoList = new CTreeGenerator.VarInfoList();
			CTreeGenerator.GroupByScope groupByScope = this.EnterGroupByScope(n.Child0);
			this.EnterVarDefListScope(n.Child1);
			AliasGenerator aliasGenerator = new AliasGenerator("K");
			List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>();
			List<Var> list2 = new List<Var>(op.Outputs);
			foreach (Var var in op.Keys)
			{
				string text = aliasGenerator.Next();
				list.Add(new KeyValuePair<string, DbExpression>(text, this.ResolveVar(var)));
				CTreeGenerator.VarInfo varInfo = new CTreeGenerator.VarInfo(var);
				varInfo.PrependProperty(text);
				varInfoList.Add(varInfo);
				list2.Remove(var);
			}
			this.ExitVarDefScope();
			groupByScope.SwitchToGroupReference();
			Dictionary<Var, DbAggregate> dictionary = new Dictionary<Var, DbAggregate>();
			Node child = n.Child2;
			PlanCompiler.Assert(child.Op is VarDefListOp, "Invalid Aggregates VarDefListOp Node encountered in GroupByOp");
			foreach (Node node in child.Children)
			{
				VarDefOp varDefOp = node.Op as VarDefOp;
				PlanCompiler.Assert(varDefOp != null, "Non-VarDefOp Node encountered as child of Aggregates VarDefListOp Node");
				Var var2 = varDefOp.Var;
				PlanCompiler.Assert(var2 is ComputedVar, "Non-ComputedVar encountered in Aggregate VarDefOp");
				Node child2 = node.Child0;
				DbExpression argument = base.VisitNode(child2.Child0);
				AggregateOp aggregateOp = child2.Op as AggregateOp;
				PlanCompiler.Assert(aggregateOp != null, "Non-Aggregate Node encountered as child of Aggregate VarDefOp Node");
				DbFunctionAggregate value;
				if (aggregateOp.IsDistinctAggregate)
				{
					value = aggregateOp.AggFunc.AggregateDistinct(argument);
				}
				else
				{
					value = aggregateOp.AggFunc.Aggregate(argument);
				}
				PlanCompiler.Assert(list2.Contains(var2), "Defined aggregate Var not in Output Aggregate Vars list?");
				dictionary.Add(var2, value);
			}
			this.ExitGroupByScope(groupByScope);
			AliasGenerator aliasGenerator2 = new AliasGenerator("A");
			List<KeyValuePair<string, DbAggregate>> list3 = new List<KeyValuePair<string, DbAggregate>>();
			foreach (Var var3 in list2)
			{
				string text2 = aliasGenerator2.Next();
				list3.Add(new KeyValuePair<string, DbAggregate>(text2, dictionary[var3]));
				CTreeGenerator.VarInfo varInfo2 = new CTreeGenerator.VarInfo(var3);
				varInfo2.PrependProperty(text2);
				varInfoList.Add(varInfo2);
			}
			DbExpression dbExpression = groupByScope.Binding.GroupBy(list, list3);
			this.PublishRelOp(this._groupByAliases.Next(), dbExpression, varInfoList);
			return dbExpression;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(GroupByIntoOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001B684 File Offset: 0x00019884
		private CTreeGenerator.RelOpInfo VisitJoinInput(Node joinInputNode)
		{
			CTreeGenerator.RelOpInfo result;
			if (joinInputNode.Op.OpType == OpType.Filter && joinInputNode.Child0.Op.OpType == OpType.ScanTable)
			{
				ScanTableOp scanTableOp = (ScanTableOp)joinInputNode.Child0.Op;
				if (scanTableOp.Table.ReferencedColumns.IsEmpty)
				{
					result = this.BuildEmptyProjection(joinInputNode);
				}
				else
				{
					result = this.BuildProjection(joinInputNode, scanTableOp.Table.ReferencedColumns);
				}
			}
			else
			{
				result = this.EnterExpressionBindingScope(joinInputNode, false);
			}
			return result;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001B700 File Offset: 0x00019900
		private DbExpression VisitBinaryJoin(Node joinNode, DbExpressionKind joinKind)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.VisitJoinInput(joinNode.Child0);
			CTreeGenerator.RelOpInfo relOpInfo2 = this.VisitJoinInput(joinNode.Child1);
			bool wasPushed = false;
			DbExpression joinCondition;
			if (joinNode.Children.Count > 2)
			{
				wasPushed = true;
				this.PushExpressionBindingScope(relOpInfo);
				this.PushExpressionBindingScope(relOpInfo2);
				joinCondition = base.VisitNode(joinNode.Child2);
			}
			else
			{
				joinCondition = DbExpressionBuilder.True;
			}
			DbExpression dbExpression = DbExpressionBuilder.CreateJoinExpressionByKind(joinKind, joinCondition, relOpInfo.CreateBinding(), relOpInfo2.CreateBinding());
			CTreeGenerator.VarInfoList varInfoList = new CTreeGenerator.VarInfoList();
			this.ExitExpressionBindingScope(relOpInfo2, wasPushed);
			relOpInfo2.PublishedVars.PrependProperty(relOpInfo2.PublisherName);
			varInfoList.AddRange(relOpInfo2.PublishedVars);
			this.ExitExpressionBindingScope(relOpInfo, wasPushed);
			relOpInfo.PublishedVars.PrependProperty(relOpInfo.PublisherName);
			varInfoList.AddRange(relOpInfo.PublishedVars);
			this.PublishRelOp(this._joinAliases.Next(), dbExpression, varInfoList);
			return dbExpression;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001B7E0 File Offset: 0x000199E0
		public override DbExpression Visit(CrossJoinOp op, Node n)
		{
			List<DbExpressionBinding> list = new List<DbExpressionBinding>();
			CTreeGenerator.VarInfoList varInfoList = new CTreeGenerator.VarInfoList();
			foreach (Node joinInputNode in n.Children)
			{
				CTreeGenerator.RelOpInfo relOpInfo = this.VisitJoinInput(joinInputNode);
				list.Add(relOpInfo.CreateBinding());
				this.ExitExpressionBindingScope(relOpInfo, false);
				relOpInfo.PublishedVars.PrependProperty(relOpInfo.PublisherName);
				varInfoList.AddRange(relOpInfo.PublishedVars);
			}
			DbExpression dbExpression = DbExpressionBuilder.CrossJoin(list);
			this.PublishRelOp(this._joinAliases.Next(), dbExpression, varInfoList);
			return dbExpression;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001B898 File Offset: 0x00019A98
		public override DbExpression Visit(InnerJoinOp op, Node n)
		{
			return this.VisitBinaryJoin(n, DbExpressionKind.InnerJoin);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001B8A3 File Offset: 0x00019AA3
		public override DbExpression Visit(LeftOuterJoinOp op, Node n)
		{
			return this.VisitBinaryJoin(n, DbExpressionKind.LeftOuterJoin);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001B8AE File Offset: 0x00019AAE
		public override DbExpression Visit(FullOuterJoinOp op, Node n)
		{
			return this.VisitBinaryJoin(n, DbExpressionKind.FullOuterJoin);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001B8BC File Offset: 0x00019ABC
		private DbExpression VisitApply(Node applyNode, DbExpressionKind applyKind)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.EnterExpressionBindingScope(applyNode.Child0);
			CTreeGenerator.RelOpInfo relOpInfo2 = this.EnterExpressionBindingScope(applyNode.Child1, false);
			DbExpression dbExpression = DbExpressionBuilder.CreateApplyExpressionByKind(applyKind, relOpInfo.CreateBinding(), relOpInfo2.CreateBinding());
			this.ExitExpressionBindingScope(relOpInfo2, false);
			this.ExitExpressionBindingScope(relOpInfo);
			relOpInfo.PublishedVars.PrependProperty(relOpInfo.PublisherName);
			relOpInfo2.PublishedVars.PrependProperty(relOpInfo2.PublisherName);
			CTreeGenerator.VarInfoList varInfoList = new CTreeGenerator.VarInfoList();
			varInfoList.AddRange(relOpInfo.PublishedVars);
			varInfoList.AddRange(relOpInfo2.PublishedVars);
			this.PublishRelOp(this._applyAliases.Next(), dbExpression, varInfoList);
			return dbExpression;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001B95B File Offset: 0x00019B5B
		public override DbExpression Visit(CrossApplyOp op, Node n)
		{
			return this.VisitApply(n, DbExpressionKind.CrossApply);
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001B965 File Offset: 0x00019B65
		public override DbExpression Visit(OuterApplyOp op, Node n)
		{
			return this.VisitApply(n, DbExpressionKind.OuterApply);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001B970 File Offset: 0x00019B70
		private DbExpression VisitSetOpArgument(Node argNode, VarVec outputVars, VarMap argVars)
		{
			List<Var> list = new List<Var>();
			CTreeGenerator.RelOpInfo relOpInfo;
			if (outputVars.IsEmpty)
			{
				relOpInfo = this.BuildEmptyProjection(argNode);
			}
			else
			{
				foreach (Var key in outputVars)
				{
					list.Add(argVars[key]);
				}
				relOpInfo = this.BuildProjection(argNode, list);
			}
			return relOpInfo.Publisher;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001B9E8 File Offset: 0x00019BE8
		private DbExpression VisitSetOp(SetOp op, Node n, AliasGenerator alias, Func<DbExpression, DbExpression, DbExpression> setOpExpressionBuilder)
		{
			CTreeGenerator.AssertBinary(n);
			DbExpression dbExpression = this.VisitSetOpArgument(n.Child0, op.Outputs, op.VarMap[0]);
			DbExpression dbExpression2 = this.VisitSetOpArgument(n.Child1, op.Outputs, op.VarMap[1]);
			CollectionType edmType = TypeHelpers.GetEdmType<CollectionType>(TypeHelpers.GetCommonTypeUsage(dbExpression.ResultType, dbExpression2.ResultType));
			IEnumerator<EdmProperty> enumerator = null;
			RowType rowType = null;
			if (TypeHelpers.TryGetEdmType<RowType>(edmType.TypeUsage, out rowType))
			{
				enumerator = rowType.Properties.GetEnumerator();
			}
			CTreeGenerator.VarInfoList varInfoList = new CTreeGenerator.VarInfoList();
			foreach (Var target in op.Outputs)
			{
				CTreeGenerator.VarInfo varInfo = new CTreeGenerator.VarInfo(target);
				if (rowType != null)
				{
					if (!enumerator.MoveNext())
					{
						PlanCompiler.Assert(false, "Record columns don't match output vars");
					}
					varInfo.PrependProperty(enumerator.Current.Name);
				}
				varInfoList.Add(varInfo);
			}
			DbExpression dbExpression3 = setOpExpressionBuilder(dbExpression, dbExpression2);
			this.PublishRelOp(alias.Next(), dbExpression3, varInfoList);
			return dbExpression3;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001BB10 File Offset: 0x00019D10
		public override DbExpression Visit(UnionAllOp op, Node n)
		{
			return this.VisitSetOp(op, n, this._unionAllAliases, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.UnionAll));
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001BB2C File Offset: 0x00019D2C
		public override DbExpression Visit(IntersectOp op, Node n)
		{
			return this.VisitSetOp(op, n, this._intersectAliases, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Intersect));
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001BB48 File Offset: 0x00019D48
		public override DbExpression Visit(ExceptOp op, Node n)
		{
			return this.VisitSetOp(op, n, this._exceptAliases, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Except));
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(DerefOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001BB64 File Offset: 0x00019D64
		public override DbExpression Visit(DistinctOp op, Node n)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.BuildProjection(n.Child0, op.Keys);
			DbExpression dbExpression = relOpInfo.Publisher.Distinct();
			this.PublishRelOp(this._distinctAliases.Next(), dbExpression, relOpInfo.PublishedVars);
			return dbExpression;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001BBAC File Offset: 0x00019DAC
		public override DbExpression Visit(SingleRowOp op, Node n)
		{
			CTreeGenerator.RelOpInfo relOpInfo;
			DbExpression dbExpression;
			if (n.Child0.Op.OpType != OpType.Project)
			{
				ExtendedNodeInfo extendedNodeInfo = this._iqtCommand.GetExtendedNodeInfo(n.Child0);
				if (extendedNodeInfo.Definitions.IsEmpty)
				{
					relOpInfo = this.BuildEmptyProjection(n.Child0);
				}
				else
				{
					relOpInfo = this.BuildProjection(n.Child0, extendedNodeInfo.Definitions);
				}
				dbExpression = relOpInfo.Publisher;
			}
			else
			{
				dbExpression = base.VisitNode(n.Child0);
				this.AssertRelOp(dbExpression);
				relOpInfo = this.ConsumeRelOp(dbExpression);
			}
			DbElementExpression item = dbExpression.Element();
			DbNewInstanceExpression dbNewInstanceExpression = DbExpressionBuilder.NewCollection(new List<DbExpression>
			{
				item
			});
			this.PublishRelOp(this._elementAliases.Next(), dbNewInstanceExpression, relOpInfo.PublishedVars);
			return dbNewInstanceExpression;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001BC70 File Offset: 0x00019E70
		public override DbExpression Visit(SingleRowTableOp op, Node n)
		{
			DbExpression[] elements = new DbConstantExpression[]
			{
				DbExpressionBuilder.Constant(1)
			};
			DbNewInstanceExpression dbNewInstanceExpression = DbExpressionBuilder.NewCollection(elements);
			this.PublishRelOp(this._singleRowTableAliases.Next(), dbNewInstanceExpression, new CTreeGenerator.VarInfoList());
			return dbNewInstanceExpression;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001BCB2 File Offset: 0x00019EB2
		public override DbExpression Visit(VarDefOp op, Node n)
		{
			PlanCompiler.Assert(false, "Unexpected VarDefOp");
			throw EntityUtil.NotSupported(Strings.Iqt_CTGen_UnexpectedVarDef);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001BCC9 File Offset: 0x00019EC9
		public override DbExpression Visit(VarDefListOp op, Node n)
		{
			PlanCompiler.Assert(false, "Unexpected VarDefListOp");
			throw EntityUtil.NotSupported(Strings.Iqt_CTGen_UnexpectedVarDefList);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001BCE0 File Offset: 0x00019EE0
		public override DbExpression Visit(PhysicalProjectOp op, Node n)
		{
			PlanCompiler.Assert(n.Children.Count == 1, "more than one input to physicalProjectOp?");
			VarList varList = new VarList();
			foreach (Var item in op.Outputs)
			{
				if (!varList.Contains(item))
				{
					varList.Add(item);
				}
			}
			op.Outputs.Clear();
			op.Outputs.AddRange(varList);
			CTreeGenerator.RelOpInfo relOpInfo = this.BuildProjection(n.Child0, op.Outputs);
			return relOpInfo.Publisher;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(SingleStreamNestOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00013A81 File Offset: 0x00011C81
		public override DbExpression Visit(MultiStreamNestOp op, Node n)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x04000772 RID: 1906
		private Command _iqtCommand;

		// Token: 0x04000773 RID: 1907
		private DbQueryCommandTree _queryTree;

		// Token: 0x04000774 RID: 1908
		private Dictionary<ParameterVar, DbParameterReferenceExpression> _addedParams = new Dictionary<ParameterVar, DbParameterReferenceExpression>();

		// Token: 0x04000775 RID: 1909
		private Stack<CTreeGenerator.IqtVarScope> _bindingScopes = new Stack<CTreeGenerator.IqtVarScope>();

		// Token: 0x04000776 RID: 1910
		private Stack<CTreeGenerator.VarDefScope> _varScopes = new Stack<CTreeGenerator.VarDefScope>();

		// Token: 0x04000777 RID: 1911
		private Dictionary<DbExpression, CTreeGenerator.RelOpInfo> _relOpState = new Dictionary<DbExpression, CTreeGenerator.RelOpInfo>();

		// Token: 0x04000778 RID: 1912
		private AliasGenerator _applyAliases = new AliasGenerator("Apply");

		// Token: 0x04000779 RID: 1913
		private AliasGenerator _distinctAliases = new AliasGenerator("Distinct");

		// Token: 0x0400077A RID: 1914
		private AliasGenerator _exceptAliases = new AliasGenerator("Except");

		// Token: 0x0400077B RID: 1915
		private AliasGenerator _extentAliases = new AliasGenerator("Extent");

		// Token: 0x0400077C RID: 1916
		private AliasGenerator _filterAliases = new AliasGenerator("Filter");

		// Token: 0x0400077D RID: 1917
		private AliasGenerator _groupByAliases = new AliasGenerator("GroupBy");

		// Token: 0x0400077E RID: 1918
		private AliasGenerator _intersectAliases = new AliasGenerator("Intersect");

		// Token: 0x0400077F RID: 1919
		private AliasGenerator _joinAliases = new AliasGenerator("Join");

		// Token: 0x04000780 RID: 1920
		private AliasGenerator _projectAliases = new AliasGenerator("Project");

		// Token: 0x04000781 RID: 1921
		private AliasGenerator _sortAliases = new AliasGenerator("Sort");

		// Token: 0x04000782 RID: 1922
		private AliasGenerator _unionAllAliases = new AliasGenerator("UnionAll");

		// Token: 0x04000783 RID: 1923
		private AliasGenerator _elementAliases = new AliasGenerator("Element");

		// Token: 0x04000784 RID: 1924
		private AliasGenerator _singleRowTableAliases = new AliasGenerator("SingleRowTable");

		// Token: 0x04000785 RID: 1925
		private AliasGenerator _limitAliases = new AliasGenerator("Limit");

		// Token: 0x04000786 RID: 1926
		private AliasGenerator _skipAliases = new AliasGenerator("Skip");

		// Token: 0x02000464 RID: 1124
		private class VarInfo
		{
			// Token: 0x17000ABA RID: 2746
			// (get) Token: 0x06003AE5 RID: 15077 RVA: 0x000DEF91 File Offset: 0x000DD191
			internal Var Var
			{
				get
				{
					return this._var;
				}
			}

			// Token: 0x17000ABB RID: 2747
			// (get) Token: 0x06003AE6 RID: 15078 RVA: 0x000DEF99 File Offset: 0x000DD199
			internal List<string> PropertyPath
			{
				get
				{
					return this._propertyChain;
				}
			}

			// Token: 0x06003AE7 RID: 15079 RVA: 0x000DEFA1 File Offset: 0x000DD1A1
			internal VarInfo(Var target)
			{
				this._var = target;
			}

			// Token: 0x06003AE8 RID: 15080 RVA: 0x000DEFBB File Offset: 0x000DD1BB
			internal void PrependProperty(string propName)
			{
				this._propertyChain.Insert(0, propName);
			}

			// Token: 0x04001953 RID: 6483
			private Var _var;

			// Token: 0x04001954 RID: 6484
			private List<string> _propertyChain = new List<string>();
		}

		// Token: 0x02000465 RID: 1125
		private class VarInfoList : List<CTreeGenerator.VarInfo>
		{
			// Token: 0x06003AE9 RID: 15081 RVA: 0x000DEFCA File Offset: 0x000DD1CA
			internal VarInfoList()
			{
			}

			// Token: 0x06003AEA RID: 15082 RVA: 0x000DEFD2 File Offset: 0x000DD1D2
			internal VarInfoList(IEnumerable<CTreeGenerator.VarInfo> elements) : base(elements)
			{
			}

			// Token: 0x06003AEB RID: 15083 RVA: 0x000DEFDC File Offset: 0x000DD1DC
			internal void PrependProperty(string propName)
			{
				foreach (CTreeGenerator.VarInfo varInfo in this)
				{
					varInfo.PropertyPath.Insert(0, propName);
				}
			}

			// Token: 0x06003AEC RID: 15084 RVA: 0x000DF030 File Offset: 0x000DD230
			internal bool TryGetInfo(Var targetVar, out CTreeGenerator.VarInfo varInfo)
			{
				varInfo = null;
				foreach (CTreeGenerator.VarInfo varInfo2 in this)
				{
					if (varInfo2.Var == targetVar)
					{
						varInfo = varInfo2;
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x02000466 RID: 1126
		private abstract class IqtVarScope
		{
			// Token: 0x06003AED RID: 15085
			internal abstract bool TryResolveVar(Var targetVar, out DbExpression resultExpr);
		}

		// Token: 0x02000467 RID: 1127
		private abstract class BindingScope : CTreeGenerator.IqtVarScope
		{
			// Token: 0x06003AEF RID: 15087 RVA: 0x000DF090 File Offset: 0x000DD290
			internal BindingScope(IEnumerable<CTreeGenerator.VarInfo> boundVars)
			{
				this._definedVars = new CTreeGenerator.VarInfoList(boundVars);
			}

			// Token: 0x17000ABC RID: 2748
			// (get) Token: 0x06003AF0 RID: 15088 RVA: 0x000DF0A4 File Offset: 0x000DD2A4
			internal CTreeGenerator.VarInfoList PublishedVars
			{
				get
				{
					return this._definedVars;
				}
			}

			// Token: 0x06003AF1 RID: 15089 RVA: 0x000DF0AC File Offset: 0x000DD2AC
			internal override bool TryResolveVar(Var targetVar, out DbExpression resultExpr)
			{
				resultExpr = null;
				CTreeGenerator.VarInfo varInfo = null;
				if (this._definedVars.TryGetInfo(targetVar, out varInfo))
				{
					resultExpr = this.BindingReference;
					foreach (string propertyName in varInfo.PropertyPath)
					{
						resultExpr = resultExpr.Property(propertyName);
					}
					return true;
				}
				return false;
			}

			// Token: 0x17000ABD RID: 2749
			// (get) Token: 0x06003AF2 RID: 15090
			protected abstract DbVariableReferenceExpression BindingReference { get; }

			// Token: 0x04001955 RID: 6485
			private readonly CTreeGenerator.VarInfoList _definedVars;
		}

		// Token: 0x02000468 RID: 1128
		private class RelOpInfo : CTreeGenerator.BindingScope
		{
			// Token: 0x06003AF3 RID: 15091 RVA: 0x000DF124 File Offset: 0x000DD324
			internal RelOpInfo(string bindingName, DbExpression publisher, IEnumerable<CTreeGenerator.VarInfo> publishedVars) : base(publishedVars)
			{
				PlanCompiler.Assert(TypeSemantics.IsCollectionType(publisher.ResultType), "non-collection type used as RelOpInfo publisher");
				this._binding = publisher.BindAs(bindingName);
			}

			// Token: 0x17000ABE RID: 2750
			// (get) Token: 0x06003AF4 RID: 15092 RVA: 0x000DF14F File Offset: 0x000DD34F
			internal string PublisherName
			{
				get
				{
					return this._binding.VariableName;
				}
			}

			// Token: 0x17000ABF RID: 2751
			// (get) Token: 0x06003AF5 RID: 15093 RVA: 0x000DF15C File Offset: 0x000DD35C
			internal DbExpression Publisher
			{
				get
				{
					return this._binding.Expression;
				}
			}

			// Token: 0x06003AF6 RID: 15094 RVA: 0x000DF169 File Offset: 0x000DD369
			internal DbExpressionBinding CreateBinding()
			{
				return this._binding;
			}

			// Token: 0x17000AC0 RID: 2752
			// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x000DF171 File Offset: 0x000DD371
			protected override DbVariableReferenceExpression BindingReference
			{
				get
				{
					return this._binding.Variable;
				}
			}

			// Token: 0x04001956 RID: 6486
			private readonly DbExpressionBinding _binding;
		}

		// Token: 0x02000469 RID: 1129
		private class GroupByScope : CTreeGenerator.BindingScope
		{
			// Token: 0x06003AF8 RID: 15096 RVA: 0x000DF17E File Offset: 0x000DD37E
			internal GroupByScope(DbGroupExpressionBinding binding, IEnumerable<CTreeGenerator.VarInfo> publishedVars) : base(publishedVars)
			{
				this._binding = binding;
			}

			// Token: 0x17000AC1 RID: 2753
			// (get) Token: 0x06003AF9 RID: 15097 RVA: 0x000DF18E File Offset: 0x000DD38E
			internal DbGroupExpressionBinding Binding
			{
				get
				{
					return this._binding;
				}
			}

			// Token: 0x06003AFA RID: 15098 RVA: 0x000DF196 File Offset: 0x000DD396
			internal void SwitchToGroupReference()
			{
				PlanCompiler.Assert(!this._referenceGroup, "SwitchToGroupReference called more than once on the same GroupByScope?");
				this._referenceGroup = true;
			}

			// Token: 0x17000AC2 RID: 2754
			// (get) Token: 0x06003AFB RID: 15099 RVA: 0x000DF1B2 File Offset: 0x000DD3B2
			protected override DbVariableReferenceExpression BindingReference
			{
				get
				{
					if (!this._referenceGroup)
					{
						return this._binding.Variable;
					}
					return this._binding.GroupVariable;
				}
			}

			// Token: 0x04001957 RID: 6487
			private readonly DbGroupExpressionBinding _binding;

			// Token: 0x04001958 RID: 6488
			private bool _referenceGroup;
		}

		// Token: 0x0200046A RID: 1130
		private class VarDefScope : CTreeGenerator.IqtVarScope
		{
			// Token: 0x06003AFC RID: 15100 RVA: 0x000DF1D3 File Offset: 0x000DD3D3
			internal VarDefScope(Dictionary<Var, DbExpression> definedVars)
			{
				this._definedVars = definedVars;
			}

			// Token: 0x06003AFD RID: 15101 RVA: 0x000DF1E4 File Offset: 0x000DD3E4
			internal override bool TryResolveVar(Var targetVar, out DbExpression resultExpr)
			{
				resultExpr = null;
				DbExpression dbExpression = null;
				if (this._definedVars.TryGetValue(targetVar, out dbExpression))
				{
					resultExpr = dbExpression;
					return true;
				}
				return false;
			}

			// Token: 0x04001959 RID: 6489
			private Dictionary<Var, DbExpression> _definedVars;
		}
	}
}
