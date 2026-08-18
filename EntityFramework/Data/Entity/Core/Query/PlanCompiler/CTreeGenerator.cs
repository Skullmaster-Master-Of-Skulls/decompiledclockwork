using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000661 RID: 1633
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal class CTreeGenerator : BasicOpVisitorOfT<DbExpression>
	{
		// Token: 0x06003FBA RID: 16314 RVA: 0x00123D20 File Offset: 0x00121F20
		internal static DbCommandTree Generate(Command itree, Node toConvert)
		{
			CTreeGenerator ctreeGenerator = new CTreeGenerator(itree, toConvert);
			return ctreeGenerator._queryTree;
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x00123D3C File Offset: 0x00121F3C
		private CTreeGenerator(Command itree, Node toConvert)
		{
			this._iqtCommand = itree;
			DbExpression query = base.VisitNode(toConvert);
			this._queryTree = DbQueryCommandTree.FromValidExpression(itree.MetadataWorkspace, DataSpace.SSpace, query, true);
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x00123E8E File Offset: 0x0012208E
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "relOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private void AssertRelOp(DbExpression expr)
		{
			PlanCompiler.Assert(this._relOpState.ContainsKey(expr), "not a relOp expression?");
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x00123EA8 File Offset: 0x001220A8
		private CTreeGenerator.RelOpInfo PublishRelOp(string name, DbExpression expr, CTreeGenerator.VarInfoList publishedVars)
		{
			CTreeGenerator.RelOpInfo relOpInfo = new CTreeGenerator.RelOpInfo(name, expr, publishedVars);
			this._relOpState.Add(expr, relOpInfo);
			return relOpInfo;
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x00123ECC File Offset: 0x001220CC
		private CTreeGenerator.RelOpInfo ConsumeRelOp(DbExpression expr)
		{
			this.AssertRelOp(expr);
			CTreeGenerator.RelOpInfo result = this._relOpState[expr];
			this._relOpState.Remove(expr);
			return result;
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x00123EFC File Offset: 0x001220FC
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "Non-RelOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbExpressionBinding")]
		private CTreeGenerator.RelOpInfo VisitAsRelOp(Node inputNode)
		{
			PlanCompiler.Assert(inputNode.Op is RelOp, "Non-RelOp used as DbExpressionBinding Input");
			DbExpression expr = base.VisitNode(inputNode);
			return this.ConsumeRelOp(expr);
		}

		// Token: 0x06003FC0 RID: 16320 RVA: 0x00123F30 File Offset: 0x00122130
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbExpressionBinding")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "RelOpInfo")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private void PushExpressionBindingScope(CTreeGenerator.RelOpInfo inputState)
		{
			PlanCompiler.Assert(inputState != null && inputState.PublisherName != null && inputState.PublishedVars != null, "Invalid RelOpInfo produced by DbExpressionBinding Input");
			this._bindingScopes.Push(inputState);
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x00123F64 File Offset: 0x00122164
		private CTreeGenerator.RelOpInfo EnterExpressionBindingScope(Node inputNode, bool pushScope)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.VisitAsRelOp(inputNode);
			if (pushScope)
			{
				this.PushExpressionBindingScope(relOpInfo);
			}
			return relOpInfo;
		}

		// Token: 0x06003FC2 RID: 16322 RVA: 0x00123F84 File Offset: 0x00122184
		private CTreeGenerator.RelOpInfo EnterExpressionBindingScope(Node inputNode)
		{
			return this.EnterExpressionBindingScope(inputNode, true);
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x00123F90 File Offset: 0x00122190
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ExpressionBindingScope")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ExitExpressionBindingScope")]
		private void ExitExpressionBindingScope(CTreeGenerator.RelOpInfo scope, bool wasPushed)
		{
			if (wasPushed)
			{
				PlanCompiler.Assert(this._bindingScopes.Count > 0, "ExitExpressionBindingScope called on empty ExpressionBindingScope stack");
				CTreeGenerator.RelOpInfo relOpInfo = (CTreeGenerator.RelOpInfo)this._bindingScopes.Pop();
				PlanCompiler.Assert(relOpInfo == scope, "ExitExpressionBindingScope called on incorrect expression");
			}
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x00123FD7 File Offset: 0x001221D7
		private void ExitExpressionBindingScope(CTreeGenerator.RelOpInfo scope)
		{
			this.ExitExpressionBindingScope(scope, true);
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x00123FE4 File Offset: 0x001221E4
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

		// Token: 0x06003FC6 RID: 16326 RVA: 0x00124050 File Offset: 0x00122250
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ExpressionBindingScope")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ExitGroupByScope")]
		private void ExitGroupByScope(CTreeGenerator.GroupByScope scope)
		{
			PlanCompiler.Assert(this._bindingScopes.Count > 0, "ExitGroupByScope called on empty ExpressionBindingScope stack");
			CTreeGenerator.GroupByScope groupByScope = (CTreeGenerator.GroupByScope)this._bindingScopes.Pop();
			PlanCompiler.Assert(groupByScope == scope, "ExitGroupByScope called on incorrect expression");
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x00124094 File Offset: 0x00122294
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "non-VarDefOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "VarDefOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "VarDefListOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x06003FC8 RID: 16328 RVA: 0x00124140 File Offset: 0x00122340
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "EnterVarDefListScope")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "non-VarDefListOp")]
		private void EnterVarDefListScope(Node varDefListNode)
		{
			PlanCompiler.Assert(varDefListNode.Op is VarDefListOp, "EnterVarDefListScope called with non-VarDefListOp");
			this.EnterVarDefScope(varDefListNode.Children);
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x00124166 File Offset: 0x00122366
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ExitVarDefScope")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "VarDefScope")]
		private void ExitVarDefScope()
		{
			PlanCompiler.Assert(this._varScopes.Count > 0, "ExitVarDefScope called on empty VarDefScope stack");
			this._varScopes.Pop();
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x0012418C File Offset: 0x0012238C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "Unresolvable")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "VarType")]
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

		// Token: 0x06003FCB RID: 16331 RVA: 0x001242B4 File Offset: 0x001224B4
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private static void AssertBinary(Node n)
		{
			PlanCompiler.Assert(2 == n.Children.Count, string.Format(CultureInfo.InvariantCulture, "Non-Binary {0} encountered", new object[]
			{
				n.Op.GetType().Name
			}));
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x001242FE File Offset: 0x001224FE
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "VisitChild")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private DbExpression VisitChild(Node n, int index)
		{
			PlanCompiler.Assert(n.Children.Count > index, "VisitChild called with invalid index");
			return base.VisitNode(n.Children[index]);
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x0012432C File Offset: 0x0012252C
		private new List<DbExpression> VisitChildren(Node n)
		{
			List<DbExpression> list = new List<DbExpression>();
			foreach (Node n2 in n.Children)
			{
				list.Add(base.VisitNode(n2));
			}
			return list;
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x0012438C File Offset: 0x0012258C
		protected override DbExpression VisitConstantOp(ConstantBaseOp op, Node n)
		{
			return op.Type.Constant(op.Value);
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x0012439F File Offset: 0x0012259F
		public override DbExpression Visit(ConstantOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x001243A9 File Offset: 0x001225A9
		public override DbExpression Visit(InternalConstantOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x001243B3 File Offset: 0x001225B3
		public override DbExpression Visit(NullOp op, Node n)
		{
			return op.Type.Null();
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x001243C0 File Offset: 0x001225C0
		public override DbExpression Visit(NullSentinelOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x001243CA File Offset: 0x001225CA
		public override DbExpression Visit(ConstantPredicateOp op, Node n)
		{
			return DbExpressionBuilder.True.Equal(op.IsTrue ? DbExpressionBuilder.True : DbExpressionBuilder.False);
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x001243EA File Offset: 0x001225EA
		public override DbExpression Visit(FunctionOp op, Node n)
		{
			return op.Function.Invoke(this.VisitChildren(n));
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x001243FE File Offset: 0x001225FE
		public override DbExpression Visit(PropertyOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x00124405 File Offset: 0x00122605
		public override DbExpression Visit(RelPropertyOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x0012440C File Offset: 0x0012260C
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ArithmeticOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OpType")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x06003FD8 RID: 16344 RVA: 0x001244E8 File Offset: 0x001226E8
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CaseOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x06003FD9 RID: 16345 RVA: 0x0012458C File Offset: 0x0012278C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ComparisonOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OpType")]
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

		// Token: 0x06003FDA RID: 16346 RVA: 0x00124660 File Offset: 0x00122860
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ConditionalOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OpType")]
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
			case OpType.In:
			{
				int count = n.Children.Count;
				List<DbExpression> list = new List<DbExpression>(count - 1);
				for (int i = 1; i < count; i++)
				{
					list.Add(this.VisitChild(n, i));
				}
				dbExpression2 = DbExpressionBuilder.CreateInExpression(dbExpression, list);
				break;
			}
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

		// Token: 0x06003FDB RID: 16347 RVA: 0x00124774 File Offset: 0x00122974
		public override DbExpression Visit(LikeOp op, Node n)
		{
			return this.VisitChild(n, 0).Like(this.VisitChild(n, 1), this.VisitChild(n, 2));
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x00124793 File Offset: 0x00122993
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GroupByOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AggregateOp")]
		public override DbExpression Visit(AggregateOp op, Node n)
		{
			PlanCompiler.Assert(false, "AggregateOp encountered outside of GroupByOp");
			throw new NotSupportedException(Strings.Iqt_CTGen_UnexpectedAggregate);
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x001247AA File Offset: 0x001229AA
		public override DbExpression Visit(NavigateOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x001247B1 File Offset: 0x001229B1
		public override DbExpression Visit(NewEntityOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003FDF RID: 16351 RVA: 0x001247B8 File Offset: 0x001229B8
		public override DbExpression Visit(NewInstanceOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x001247BF File Offset: 0x001229BF
		public override DbExpression Visit(DiscriminatedNewEntityOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x001247C6 File Offset: 0x001229C6
		public override DbExpression Visit(NewMultisetOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x001247CD File Offset: 0x001229CD
		public override DbExpression Visit(NewRecordOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x001247D4 File Offset: 0x001229D4
		public override DbExpression Visit(RefOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x001247DB File Offset: 0x001229DB
		public override DbExpression Visit(VarRefOp op, Node n)
		{
			return this.ResolveVar(op.Var);
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x001247E9 File Offset: 0x001229E9
		public override DbExpression Visit(TreatOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x001247F0 File Offset: 0x001229F0
		public override DbExpression Visit(CastOp op, Node n)
		{
			return this.VisitChild(n, 0).CastTo(op.Type);
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x00124805 File Offset: 0x00122A05
		public override DbExpression Visit(SoftCastOp op, Node n)
		{
			return this.VisitChild(n, 0);
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x0012480F File Offset: 0x00122A0F
		public override DbExpression Visit(IsOfOp op, Node n)
		{
			if (op.IsOfOnly)
			{
				return this.VisitChild(n, 0).IsOfOnly(op.IsOfType);
			}
			return this.VisitChild(n, 0).IsOf(op.IsOfType);
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x00124840 File Offset: 0x00122A40
		public override DbExpression Visit(ExistsOp op, Node n)
		{
			DbExpression dbExpression = base.VisitNode(n.Child0);
			this.ConsumeRelOp(dbExpression);
			return dbExpression.IsEmpty().Not();
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x00124870 File Offset: 0x00122A70
		public override DbExpression Visit(ElementOp op, Node n)
		{
			DbExpression dbExpression = base.VisitNode(n.Child0);
			this.AssertRelOp(dbExpression);
			this.ConsumeRelOp(dbExpression);
			return DbExpressionBuilder.CreateElementExpressionUnwrapSingleProperty(dbExpression);
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x001248A1 File Offset: 0x00122AA1
		public override DbExpression Visit(GetRefKeyOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x001248A8 File Offset: 0x00122AA8
		public override DbExpression Visit(GetEntityRefOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x001248AF File Offset: 0x00122AAF
		public override DbExpression Visit(CollectOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x001248B8 File Offset: 0x00122AB8
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

		// Token: 0x06003FEF RID: 16367 RVA: 0x00124918 File Offset: 0x00122B18
		[SuppressMessage("Microsoft.Globalization", "CA1309:UseOrdinalStringComparison", MessageId = "System.Collections.Generic.Dictionary`2<System.String,System.String>.#ctor(System.Collections.Generic.IEqualityComparer`1<System.String>)")]
		[SuppressMessage("Microsoft.Globalization", "CA1309:UseOrdinalStringComparison", MessageId = "System.Collections.Generic.Dictionary`2<System.String,System.Data.Entity.Core.Common.Utils.AliasGenerator>.#ctor(System.Collections.Generic.IEqualityComparer`1<System.String>)")]
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

		// Token: 0x06003FF0 RID: 16368 RVA: 0x001249F8 File Offset: 0x00122BF8
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

		// Token: 0x06003FF1 RID: 16369 RVA: 0x00124A80 File Offset: 0x00122C80
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ScanTableOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "TableMetadata")]
		public override DbExpression Visit(ScanTableOp op, Node n)
		{
			PlanCompiler.Assert(op.Table.TableMetadata.Extent != null, "Invalid TableMetadata used in ScanTableOp - no Extent specified");
			PlanCompiler.Assert(!n.HasChild0, "views are not expected here");
			CTreeGenerator.VarInfoList tableVars = CTreeGenerator.GetTableVars(op.Table);
			DbExpression dbExpression = op.Table.TableMetadata.Extent.Scan();
			this.PublishRelOp(this._extentAliases.Next(), dbExpression, tableVars);
			return dbExpression;
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x00124AF7 File Offset: 0x00122CF7
		public override DbExpression Visit(ScanViewOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x00124B00 File Offset: 0x00122D00
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "VarDef")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		public override DbExpression Visit(UnnestOp op, Node n)
		{
			PlanCompiler.Assert(n.Child0.Op.OpType == OpType.VarDef, "an un-nest's child must be a VarDef");
			Node child = n.Child0.Child0;
			DbExpression dbExpression = child.Op.Accept<DbExpression>(this, child);
			PlanCompiler.Assert(dbExpression.ResultType.EdmType.BuiltInTypeKind == BuiltInTypeKind.CollectionType, "the input to un-nest must yield a collection after plan compilation");
			CTreeGenerator.VarInfoList tableVars = CTreeGenerator.GetTableVars(op.Table);
			this.PublishRelOp(this._extentAliases.Next(), dbExpression, tableVars);
			return dbExpression;
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x00124B84 File Offset: 0x00122D84
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

		// Token: 0x06003FF5 RID: 16373 RVA: 0x00124C10 File Offset: 0x00122E10
		private CTreeGenerator.RelOpInfo BuildProjection(Node relOpNode, IEnumerable<Var> projectionVars)
		{
			ProjectOp projectOp = relOpNode.Op as ProjectOp;
			DbExpression expr;
			if (projectOp != null)
			{
				expr = this.VisitProject(relOpNode, projectionVars);
			}
			else
			{
				CTreeGenerator.RelOpInfo relOpInfo = this.EnterExpressionBindingScope(relOpNode);
				expr = this.CreateProject(relOpInfo, projectionVars);
				this.ExitExpressionBindingScope(relOpInfo);
			}
			return this.ConsumeRelOp(expr);
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x00124C5C File Offset: 0x00122E5C
		private DbExpression VisitProject(Node n, IEnumerable<Var> varList)
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

		// Token: 0x06003FF7 RID: 16375 RVA: 0x00124CB5 File Offset: 0x00122EB5
		public override DbExpression Visit(ProjectOp op, Node n)
		{
			return this.VisitProject(n, op.Outputs);
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x00124CC4 File Offset: 0x00122EC4
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "FilterOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "non-ScalarOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x06003FF9 RID: 16377 RVA: 0x00124D30 File Offset: 0x00122F30
		private List<DbSortClause> VisitSortKeys(IList<System.Data.Entity.Core.Query.InternalTrees.SortKey> sortKeys)
		{
			VarVec varVec = this._iqtCommand.CreateVarVec();
			List<DbSortClause> list = new List<DbSortClause>();
			foreach (System.Data.Entity.Core.Query.InternalTrees.SortKey sortKey in sortKeys)
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

		// Token: 0x06003FFA RID: 16378 RVA: 0x00124E10 File Offset: 0x00123010
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SortOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		public override DbExpression Visit(SortOp op, Node n)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.EnterExpressionBindingScope(n.Child0);
			PlanCompiler.Assert(!n.HasChild1, "SortOp can have only one child");
			DbExpression dbExpression = relOpInfo.CreateBinding().Sort(this.VisitSortKeys(op.Keys));
			this.ExitExpressionBindingScope(relOpInfo);
			this.PublishRelOp(this._sortAliases.Next(), dbExpression, relOpInfo.PublishedVars);
			return dbExpression;
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x00124E76 File Offset: 0x00123076
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private static DbExpression CreateLimitExpression(DbExpression argument, DbExpression limit, bool withTies)
		{
			PlanCompiler.Assert(!withTies, "Limit with Ties is not currently supported");
			return argument.Limit(limit);
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x00124E90 File Offset: 0x00123090
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ConstrainedSortOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SortKeys")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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
				dbExpression = CTreeGenerator.CreateLimitExpression(dbExpression2, base.VisitNode(n.Child2), op.WithTies);
				name = this._limitAliases.Next();
			}
			else
			{
				relOpInfo = this.EnterExpressionBindingScope(n.Child0);
				List<DbSortClause> sortOrder = this.VisitSortKeys(op.Keys);
				this.ExitExpressionBindingScope(relOpInfo);
				if (!flag && !flag2)
				{
					dbExpression = CTreeGenerator.CreateLimitExpression(relOpInfo.CreateBinding().Skip(sortOrder, this.VisitChild(n, 1)), this.VisitChild(n, 2), op.WithTies);
					name = this._limitAliases.Next();
				}
				else if (!flag && flag2)
				{
					dbExpression = relOpInfo.CreateBinding().Skip(sortOrder, this.VisitChild(n, 1));
					name = this._skipAliases.Next();
				}
				else if (flag && !flag2)
				{
					dbExpression = CTreeGenerator.CreateLimitExpression(relOpInfo.CreateBinding().Sort(sortOrder), this.VisitChild(n, 2), op.WithTies);
					name = this._limitAliases.Next();
				}
			}
			this.PublishRelOp(name, dbExpression, relOpInfo.PublishedVars);
			return dbExpression;
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x0012500C File Offset: 0x0012320C
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "VarDefListOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "Vars")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "VarDefOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "Non-ComputedVar")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "Non-VarDefOp")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GroupByOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
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

		// Token: 0x06003FFE RID: 16382 RVA: 0x001252C0 File Offset: 0x001234C0
		public override DbExpression Visit(GroupByIntoOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x001252C8 File Offset: 0x001234C8
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

		// Token: 0x06004000 RID: 16384 RVA: 0x00125344 File Offset: 0x00123544
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

		// Token: 0x06004001 RID: 16385 RVA: 0x00125424 File Offset: 0x00123624
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

		// Token: 0x06004002 RID: 16386 RVA: 0x001254D8 File Offset: 0x001236D8
		public override DbExpression Visit(InnerJoinOp op, Node n)
		{
			return this.VisitBinaryJoin(n, DbExpressionKind.InnerJoin);
		}

		// Token: 0x06004003 RID: 16387 RVA: 0x001254E3 File Offset: 0x001236E3
		public override DbExpression Visit(LeftOuterJoinOp op, Node n)
		{
			return this.VisitBinaryJoin(n, DbExpressionKind.LeftOuterJoin);
		}

		// Token: 0x06004004 RID: 16388 RVA: 0x001254EE File Offset: 0x001236EE
		public override DbExpression Visit(FullOuterJoinOp op, Node n)
		{
			return this.VisitBinaryJoin(n, DbExpressionKind.FullOuterJoin);
		}

		// Token: 0x06004005 RID: 16389 RVA: 0x001254FC File Offset: 0x001236FC
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

		// Token: 0x06004006 RID: 16390 RVA: 0x0012559B File Offset: 0x0012379B
		public override DbExpression Visit(CrossApplyOp op, Node n)
		{
			return this.VisitApply(n, DbExpressionKind.CrossApply);
		}

		// Token: 0x06004007 RID: 16391 RVA: 0x001255A5 File Offset: 0x001237A5
		public override DbExpression Visit(OuterApplyOp op, Node n)
		{
			return this.VisitApply(n, DbExpressionKind.OuterApply);
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x001255B0 File Offset: 0x001237B0
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

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06004009 RID: 16393 RVA: 0x00125628 File Offset: 0x00123828
		private DbProviderManifest ProviderManifest
		{
			get
			{
				DbProviderManifest result;
				if ((result = this._providerManifest) == null)
				{
					result = (this._providerManifest = ((StoreItemCollection)this._iqtCommand.MetadataWorkspace.GetItemCollection(DataSpace.SSpace)).ProviderManifest);
				}
				return result;
			}
		}

		// Token: 0x0600400A RID: 16394 RVA: 0x00125664 File Offset: 0x00123864
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "vars")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private DbExpression VisitSetOp(SetOp op, Node n, AliasGenerator alias, Func<DbExpression, DbExpression, DbExpression> setOpExpressionBuilder)
		{
			CTreeGenerator.AssertBinary(n);
			bool flag = (op.OpType == OpType.UnionAll || op.OpType == OpType.Intersect) && this.ProviderManifest.SupportsIntersectAndUnionAllFlattening();
			DbExpression dbExpression = (flag && n.Child0.Op.OpType == op.OpType) ? this.VisitSetOp((SetOp)n.Child0.Op, n.Child0, alias, setOpExpressionBuilder) : this.VisitSetOpArgument(n.Child0, op.Outputs, op.VarMap[0]);
			DbExpression dbExpression2 = (flag && n.Child1.Op.OpType == op.OpType) ? this.VisitSetOp((SetOp)n.Child1.Op, n.Child1, alias, setOpExpressionBuilder) : this.VisitSetOpArgument(n.Child1, op.Outputs, op.VarMap[1]);
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

		// Token: 0x0600400B RID: 16395 RVA: 0x00125828 File Offset: 0x00123A28
		public override DbExpression Visit(UnionAllOp op, Node n)
		{
			return this.VisitSetOp(op, n, this._unionAllAliases, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.UnionAll));
		}

		// Token: 0x0600400C RID: 16396 RVA: 0x00125844 File Offset: 0x00123A44
		public override DbExpression Visit(IntersectOp op, Node n)
		{
			return this.VisitSetOp(op, n, this._intersectAliases, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Intersect));
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x00125860 File Offset: 0x00123A60
		public override DbExpression Visit(ExceptOp op, Node n)
		{
			return this.VisitSetOp(op, n, this._exceptAliases, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Except));
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x0012587C File Offset: 0x00123A7C
		public override DbExpression Visit(DerefOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x00125884 File Offset: 0x00123A84
		public override DbExpression Visit(DistinctOp op, Node n)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.BuildProjection(n.Child0, op.Keys);
			DbExpression dbExpression = relOpInfo.Publisher.Distinct();
			this.PublishRelOp(this._distinctAliases.Next(), dbExpression, relOpInfo.PublishedVars);
			return dbExpression;
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x001258CC File Offset: 0x00123ACC
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

		// Token: 0x06004011 RID: 16401 RVA: 0x00125990 File Offset: 0x00123B90
		public override DbExpression Visit(SingleRowTableOp op, Node n)
		{
			DbNewInstanceExpression dbNewInstanceExpression = DbExpressionBuilder.NewCollection(new DbConstantExpression[]
			{
				DbExpressionBuilder.Constant(1)
			});
			this.PublishRelOp(this._singleRowTableAliases.Next(), dbNewInstanceExpression, new CTreeGenerator.VarInfoList());
			return dbNewInstanceExpression;
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x001259D2 File Offset: 0x00123BD2
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "VarDefOp")]
		public override DbExpression Visit(VarDefOp op, Node n)
		{
			PlanCompiler.Assert(false, "Unexpected VarDefOp");
			throw new NotSupportedException(Strings.Iqt_CTGen_UnexpectedVarDef);
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x001259E9 File Offset: 0x00123BE9
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "VarDefListOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		public override DbExpression Visit(VarDefListOp op, Node n)
		{
			PlanCompiler.Assert(false, "Unexpected VarDefListOp");
			throw new NotSupportedException(Strings.Iqt_CTGen_UnexpectedVarDefList);
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x00125A00 File Offset: 0x00123C00
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "physicalProjectOp")]
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

		// Token: 0x06004015 RID: 16405 RVA: 0x00125AAC File Offset: 0x00123CAC
		public override DbExpression Visit(SingleStreamNestOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x00125AB3 File Offset: 0x00123CB3
		public override DbExpression Visit(MultiStreamNestOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040017C2 RID: 6082
		private readonly Command _iqtCommand;

		// Token: 0x040017C3 RID: 6083
		private readonly DbQueryCommandTree _queryTree;

		// Token: 0x040017C4 RID: 6084
		private readonly Dictionary<ParameterVar, DbParameterReferenceExpression> _addedParams = new Dictionary<ParameterVar, DbParameterReferenceExpression>();

		// Token: 0x040017C5 RID: 6085
		private readonly Stack<CTreeGenerator.IqtVarScope> _bindingScopes = new Stack<CTreeGenerator.IqtVarScope>();

		// Token: 0x040017C6 RID: 6086
		private readonly Stack<CTreeGenerator.VarDefScope> _varScopes = new Stack<CTreeGenerator.VarDefScope>();

		// Token: 0x040017C7 RID: 6087
		private readonly Dictionary<DbExpression, CTreeGenerator.RelOpInfo> _relOpState = new Dictionary<DbExpression, CTreeGenerator.RelOpInfo>();

		// Token: 0x040017C8 RID: 6088
		private readonly AliasGenerator _applyAliases = new AliasGenerator("Apply");

		// Token: 0x040017C9 RID: 6089
		private readonly AliasGenerator _distinctAliases = new AliasGenerator("Distinct");

		// Token: 0x040017CA RID: 6090
		private readonly AliasGenerator _exceptAliases = new AliasGenerator("Except");

		// Token: 0x040017CB RID: 6091
		private readonly AliasGenerator _extentAliases = new AliasGenerator("Extent");

		// Token: 0x040017CC RID: 6092
		private readonly AliasGenerator _filterAliases = new AliasGenerator("Filter");

		// Token: 0x040017CD RID: 6093
		private readonly AliasGenerator _groupByAliases = new AliasGenerator("GroupBy");

		// Token: 0x040017CE RID: 6094
		private readonly AliasGenerator _intersectAliases = new AliasGenerator("Intersect");

		// Token: 0x040017CF RID: 6095
		private readonly AliasGenerator _joinAliases = new AliasGenerator("Join");

		// Token: 0x040017D0 RID: 6096
		private readonly AliasGenerator _projectAliases = new AliasGenerator("Project");

		// Token: 0x040017D1 RID: 6097
		private readonly AliasGenerator _sortAliases = new AliasGenerator("Sort");

		// Token: 0x040017D2 RID: 6098
		private readonly AliasGenerator _unionAllAliases = new AliasGenerator("UnionAll");

		// Token: 0x040017D3 RID: 6099
		private readonly AliasGenerator _elementAliases = new AliasGenerator("Element");

		// Token: 0x040017D4 RID: 6100
		private readonly AliasGenerator _singleRowTableAliases = new AliasGenerator("SingleRowTable");

		// Token: 0x040017D5 RID: 6101
		private readonly AliasGenerator _limitAliases = new AliasGenerator("Limit");

		// Token: 0x040017D6 RID: 6102
		private readonly AliasGenerator _skipAliases = new AliasGenerator("Skip");

		// Token: 0x040017D7 RID: 6103
		private DbProviderManifest _providerManifest;

		// Token: 0x02000662 RID: 1634
		private class VarInfo
		{
			// Token: 0x170009D7 RID: 2519
			// (get) Token: 0x06004017 RID: 16407 RVA: 0x00125ABA File Offset: 0x00123CBA
			internal Var Var
			{
				get
				{
					return this._var;
				}
			}

			// Token: 0x170009D8 RID: 2520
			// (get) Token: 0x06004018 RID: 16408 RVA: 0x00125AC2 File Offset: 0x00123CC2
			internal List<string> PropertyPath
			{
				get
				{
					return this._propertyChain;
				}
			}

			// Token: 0x06004019 RID: 16409 RVA: 0x00125ACA File Offset: 0x00123CCA
			internal VarInfo(Var target)
			{
				this._var = target;
			}

			// Token: 0x0600401A RID: 16410 RVA: 0x00125AE4 File Offset: 0x00123CE4
			internal void PrependProperty(string propName)
			{
				this._propertyChain.Insert(0, propName);
			}

			// Token: 0x040017D8 RID: 6104
			private readonly Var _var;

			// Token: 0x040017D9 RID: 6105
			private readonly List<string> _propertyChain = new List<string>();
		}

		// Token: 0x02000663 RID: 1635
		private class VarInfoList : List<CTreeGenerator.VarInfo>
		{
			// Token: 0x0600401B RID: 16411 RVA: 0x00125AF3 File Offset: 0x00123CF3
			internal VarInfoList()
			{
			}

			// Token: 0x0600401C RID: 16412 RVA: 0x00125AFB File Offset: 0x00123CFB
			internal VarInfoList(IEnumerable<CTreeGenerator.VarInfo> elements) : base(elements)
			{
			}

			// Token: 0x0600401D RID: 16413 RVA: 0x00125B04 File Offset: 0x00123D04
			internal void PrependProperty(string propName)
			{
				foreach (CTreeGenerator.VarInfo varInfo in this)
				{
					varInfo.PropertyPath.Insert(0, propName);
				}
			}

			// Token: 0x0600401E RID: 16414 RVA: 0x00125B58 File Offset: 0x00123D58
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

		// Token: 0x02000664 RID: 1636
		private abstract class IqtVarScope
		{
			// Token: 0x0600401F RID: 16415
			internal abstract bool TryResolveVar(Var targetVar, out DbExpression resultExpr);
		}

		// Token: 0x02000665 RID: 1637
		private abstract class BindingScope : CTreeGenerator.IqtVarScope
		{
			// Token: 0x06004021 RID: 16417 RVA: 0x00125BC0 File Offset: 0x00123DC0
			internal BindingScope(IEnumerable<CTreeGenerator.VarInfo> boundVars)
			{
				this._definedVars = new CTreeGenerator.VarInfoList(boundVars);
			}

			// Token: 0x170009D9 RID: 2521
			// (get) Token: 0x06004022 RID: 16418 RVA: 0x00125BD4 File Offset: 0x00123DD4
			internal CTreeGenerator.VarInfoList PublishedVars
			{
				get
				{
					return this._definedVars;
				}
			}

			// Token: 0x06004023 RID: 16419 RVA: 0x00125BDC File Offset: 0x00123DDC
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

			// Token: 0x170009DA RID: 2522
			// (get) Token: 0x06004024 RID: 16420
			protected abstract DbVariableReferenceExpression BindingReference { get; }

			// Token: 0x040017DA RID: 6106
			private readonly CTreeGenerator.VarInfoList _definedVars;
		}

		// Token: 0x02000666 RID: 1638
		private class RelOpInfo : CTreeGenerator.BindingScope
		{
			// Token: 0x06004025 RID: 16421 RVA: 0x00125C54 File Offset: 0x00123E54
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
			[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "RelOpInfo")]
			internal RelOpInfo(string bindingName, DbExpression publisher, IEnumerable<CTreeGenerator.VarInfo> publishedVars) : base(publishedVars)
			{
				PlanCompiler.Assert(TypeSemantics.IsCollectionType(publisher.ResultType), "non-collection type used as RelOpInfo publisher");
				this._binding = publisher.BindAs(bindingName);
			}

			// Token: 0x170009DB RID: 2523
			// (get) Token: 0x06004026 RID: 16422 RVA: 0x00125C7F File Offset: 0x00123E7F
			internal string PublisherName
			{
				get
				{
					return this._binding.VariableName;
				}
			}

			// Token: 0x170009DC RID: 2524
			// (get) Token: 0x06004027 RID: 16423 RVA: 0x00125C8C File Offset: 0x00123E8C
			internal DbExpression Publisher
			{
				get
				{
					return this._binding.Expression;
				}
			}

			// Token: 0x06004028 RID: 16424 RVA: 0x00125C99 File Offset: 0x00123E99
			internal DbExpressionBinding CreateBinding()
			{
				return this._binding;
			}

			// Token: 0x170009DD RID: 2525
			// (get) Token: 0x06004029 RID: 16425 RVA: 0x00125CA1 File Offset: 0x00123EA1
			protected override DbVariableReferenceExpression BindingReference
			{
				get
				{
					return this._binding.Variable;
				}
			}

			// Token: 0x040017DB RID: 6107
			private readonly DbExpressionBinding _binding;
		}

		// Token: 0x02000667 RID: 1639
		private class GroupByScope : CTreeGenerator.BindingScope
		{
			// Token: 0x0600402A RID: 16426 RVA: 0x00125CAE File Offset: 0x00123EAE
			internal GroupByScope(DbGroupExpressionBinding binding, IEnumerable<CTreeGenerator.VarInfo> publishedVars) : base(publishedVars)
			{
				this._binding = binding;
			}

			// Token: 0x170009DE RID: 2526
			// (get) Token: 0x0600402B RID: 16427 RVA: 0x00125CBE File Offset: 0x00123EBE
			internal DbGroupExpressionBinding Binding
			{
				get
				{
					return this._binding;
				}
			}

			// Token: 0x0600402C RID: 16428 RVA: 0x00125CC6 File Offset: 0x00123EC6
			[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SwitchToGroupReference")]
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
			[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GroupByScope")]
			internal void SwitchToGroupReference()
			{
				PlanCompiler.Assert(!this._referenceGroup, "SwitchToGroupReference called more than once on the same GroupByScope?");
				this._referenceGroup = true;
			}

			// Token: 0x170009DF RID: 2527
			// (get) Token: 0x0600402D RID: 16429 RVA: 0x00125CE2 File Offset: 0x00123EE2
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

			// Token: 0x040017DC RID: 6108
			private readonly DbGroupExpressionBinding _binding;

			// Token: 0x040017DD RID: 6109
			private bool _referenceGroup;
		}

		// Token: 0x02000668 RID: 1640
		private class VarDefScope : CTreeGenerator.IqtVarScope
		{
			// Token: 0x0600402E RID: 16430 RVA: 0x00125D03 File Offset: 0x00123F03
			internal VarDefScope(Dictionary<Var, DbExpression> definedVars)
			{
				this._definedVars = definedVars;
			}

			// Token: 0x0600402F RID: 16431 RVA: 0x00125D14 File Offset: 0x00123F14
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

			// Token: 0x040017DE RID: 6110
			private readonly Dictionary<Var, DbExpression> _definedVars;
		}
	}
}
