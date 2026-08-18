using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Common.Utils.Boolean;
using System.Data.Entity;
using System.Data.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Data.Mapping.ViewGeneration.Utils;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Validation
{
	// Token: 0x0200027B RID: 635
	internal class RewritingValidator
	{
		// Token: 0x06002668 RID: 9832 RVA: 0x00092754 File Offset: 0x00090954
		internal RewritingValidator(ViewgenContext context, CellTreeNode basicView)
		{
			this._viewgenContext = context;
			this._basicView = basicView;
			this._domainMap = this._viewgenContext.MemberMaps.UpdateDomainMap;
			this._keyAttributes = MemberPath.GetKeyMembers(this._viewgenContext.Extent, this._domainMap);
			this._errorLog = new ErrorLog();
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x000927B4 File Offset: 0x000909B4
		internal void Validate()
		{
			Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode> memberValueTrees = this.CreateMemberValueTrees(false);
			Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode> memberValueTrees2 = this.CreateMemberValueTrees(true);
			RewritingValidator.WhereClauseVisitor whereClauseVisitor = new RewritingValidator.WhereClauseVisitor(this._basicView, memberValueTrees);
			RewritingValidator.WhereClauseVisitor whereClauseVisitor2 = new RewritingValidator.WhereClauseVisitor(this._basicView, memberValueTrees2);
			foreach (LeftCellWrapper leftCellWrapper in this._viewgenContext.AllWrappersForExtent)
			{
				Cell onlyInputCell = leftCellWrapper.OnlyInputCell;
				CellTreeNode cellTreeNode = new LeafCellTreeNode(this._viewgenContext, leftCellWrapper);
				CellTreeNode cellTreeNode2 = whereClauseVisitor2.GetCellTreeNode(onlyInputCell.SQuery.WhereClause);
				if (cellTreeNode2 != null)
				{
					CellTreeNode cellTreeNode3;
					if (cellTreeNode2 != this._basicView)
					{
						cellTreeNode3 = new OpCellTreeNode(this._viewgenContext, CellTreeOpType.IJ, new CellTreeNode[]
						{
							cellTreeNode2,
							this._basicView
						});
					}
					else
					{
						cellTreeNode3 = this._basicView;
					}
					BoolExpression inExtentCondition = BoolExpression.CreateLiteral(leftCellWrapper.CreateRoleBoolean(), this._viewgenContext.MemberMaps.QueryDomainMap);
					BoolExpression extraConstraint;
					if (!this.CheckEquivalence(cellTreeNode.RightFragmentQuery, cellTreeNode3.RightFragmentQuery, inExtentCondition, out extraConstraint))
					{
						string p = StringUtil.FormatInvariant("{0}", new object[]
						{
							this._viewgenContext.Extent
						});
						cellTreeNode.RightFragmentQuery.Condition.ExpensiveSimplify();
						cellTreeNode3.RightFragmentQuery.Condition.ExpensiveSimplify();
						string message = Strings.ViewGen_CQ_PartitionConstraint(p);
						this.ReportConstraintViolation(message, extraConstraint, ViewGenErrorCode.PartitionConstraintViolation, cellTreeNode.GetLeaves().Concat(cellTreeNode3.GetLeaves()));
					}
					CellTreeNode cellTreeNode4 = whereClauseVisitor.GetCellTreeNode(onlyInputCell.SQuery.WhereClause);
					if (cellTreeNode4 != null)
					{
						RewritingValidator.DomainConstraintVisitor.CheckConstraints(cellTreeNode4, leftCellWrapper, this._viewgenContext, this._errorLog);
						if (this._errorLog.Count > 0)
						{
							continue;
						}
						this.CheckConstraintsOnProjectedConditionMembers(memberValueTrees, leftCellWrapper, cellTreeNode3, inExtentCondition);
						if (this._errorLog.Count > 0)
						{
							continue;
						}
					}
					this.CheckConstraintsOnNonNullableMembers(memberValueTrees, leftCellWrapper, cellTreeNode3, inExtentCondition);
				}
			}
			if (this._errorLog.Count > 0)
			{
				ExceptionHelpers.ThrowMappingException(this._errorLog, this._viewgenContext.Config);
			}
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x000929D8 File Offset: 0x00090BD8
		private bool CheckEquivalence(FragmentQuery cQuery, FragmentQuery sQuery, BoolExpression inExtentCondition, out BoolExpression unsatisfiedConstraint)
		{
			FragmentQuery fragmentQuery = this._viewgenContext.RightFragmentQP.Difference(cQuery, sQuery);
			FragmentQuery fragmentQuery2 = this._viewgenContext.RightFragmentQP.Difference(sQuery, cQuery);
			FragmentQuery fragmentQuery3 = FragmentQuery.Create(BoolExpression.CreateAnd(new BoolExpression[]
			{
				fragmentQuery.Condition,
				inExtentCondition
			}));
			FragmentQuery fragmentQuery4 = FragmentQuery.Create(BoolExpression.CreateAnd(new BoolExpression[]
			{
				fragmentQuery2.Condition,
				inExtentCondition
			}));
			unsatisfiedConstraint = null;
			bool flag = true;
			bool flag2 = true;
			if (this._viewgenContext.RightFragmentQP.IsSatisfiable(fragmentQuery3))
			{
				unsatisfiedConstraint = fragmentQuery3.Condition;
				flag = false;
			}
			if (this._viewgenContext.RightFragmentQP.IsSatisfiable(fragmentQuery4))
			{
				unsatisfiedConstraint = fragmentQuery4.Condition;
				flag2 = false;
			}
			if (flag && flag2)
			{
				return true;
			}
			unsatisfiedConstraint.ExpensiveSimplify();
			return false;
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x00092AA4 File Offset: 0x00090CA4
		private void ReportConstraintViolation(string message, BoolExpression extraConstraint, ViewGenErrorCode errorCode, IEnumerable<LeftCellWrapper> relevantWrappers)
		{
			if (ErrorPatternMatcher.FindMappingErrors(this._viewgenContext, this._domainMap, this._errorLog))
			{
				return;
			}
			extraConstraint.ExpensiveSimplify();
			HashSet<LeftCellWrapper> hashSet = new HashSet<LeftCellWrapper>(relevantWrappers);
			List<LeftCellWrapper> list = new List<LeftCellWrapper>(hashSet);
			list.Sort(LeftCellWrapper.OriginalCellIdComparer);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(message);
			RewritingValidator.EntityConfigurationToUserString(extraConstraint, stringBuilder);
			this._errorLog.AddEntry(new ErrorLog.Record(true, errorCode, stringBuilder.ToString(), hashSet, ""));
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x00092B20 File Offset: 0x00090D20
		private Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode> CreateMemberValueTrees(bool complementElse)
		{
			Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode> dictionary = new Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode>();
			foreach (MemberPath memberPath in this._domainMap.ConditionMembers(this._viewgenContext.Extent))
			{
				List<Constant> list = new List<Constant>(this._domainMap.GetDomain(memberPath));
				OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this._viewgenContext, CellTreeOpType.Union);
				for (int i = 0; i < list.Count; i++)
				{
					Constant constant = list[i];
					RewritingValidator.MemberValueBinding key = new RewritingValidator.MemberValueBinding(memberPath, constant);
					FragmentQuery query = QueryRewriter.CreateMemberConditionQuery(memberPath, constant, this._keyAttributes, this._domainMap);
					Tile<FragmentQuery> tile;
					if (this._viewgenContext.TryGetCachedRewriting(query, out tile))
					{
						CellTreeNode cellTreeNode = QueryRewriter.TileToCellTree(tile, this._viewgenContext);
						dictionary[key] = cellTreeNode;
						if (i < list.Count - 1)
						{
							opCellTreeNode.Add(cellTreeNode);
						}
					}
				}
				if (complementElse && list.Count > 1)
				{
					Constant value = list[list.Count - 1];
					RewritingValidator.MemberValueBinding key2 = new RewritingValidator.MemberValueBinding(memberPath, value);
					dictionary[key2] = new OpCellTreeNode(this._viewgenContext, CellTreeOpType.LASJ, new CellTreeNode[]
					{
						this._basicView,
						opCellTreeNode
					});
				}
			}
			return dictionary;
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x00092C7C File Offset: 0x00090E7C
		private void CheckConstraintsOnProjectedConditionMembers(Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode> memberValueTrees, LeftCellWrapper wrapper, CellTreeNode sQueryTree, BoolExpression inExtentCondition)
		{
			foreach (MemberPath memberPath in this._domainMap.ConditionMembers(this._viewgenContext.Extent))
			{
				int slotNum = this._viewgenContext.MemberMaps.ProjectedSlotMap.IndexOf(memberPath);
				MemberProjectedSlot memberProjectedSlot = wrapper.RightCellQuery.ProjectedSlotAt(slotNum) as MemberProjectedSlot;
				if (memberProjectedSlot != null)
				{
					foreach (Constant constant in this._domainMap.GetDomain(memberPath))
					{
						CellTreeNode cellTreeNode;
						if (memberValueTrees.TryGetValue(new RewritingValidator.MemberValueBinding(memberPath, constant), out cellTreeNode))
						{
							BoolExpression whereClause = RewritingValidator.PropagateCellConstantsToWhereClause(wrapper, wrapper.RightCellQuery.WhereClause, constant, memberPath, this._viewgenContext.MemberMaps);
							FragmentQuery cQuery = FragmentQuery.Create(whereClause);
							CellTreeNode cellTreeNode2 = (sQueryTree == this._basicView) ? cellTreeNode : new OpCellTreeNode(this._viewgenContext, CellTreeOpType.IJ, new CellTreeNode[]
							{
								cellTreeNode,
								sQueryTree
							});
							BoolExpression extraConstraint;
							if (!this.CheckEquivalence(cQuery, cellTreeNode2.RightFragmentQuery, inExtentCondition, out extraConstraint))
							{
								string message = Strings.ViewGen_CQ_DomainConstraint(memberProjectedSlot.ToUserString());
								this.ReportConstraintViolation(message, extraConstraint, ViewGenErrorCode.DomainConstraintViolation, cellTreeNode2.GetLeaves().Concat(new LeftCellWrapper[]
								{
									wrapper
								}));
							}
						}
					}
				}
			}
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x00092E18 File Offset: 0x00091018
		internal static BoolExpression PropagateCellConstantsToWhereClause(LeftCellWrapper wrapper, BoolExpression expression, Constant constant, MemberPath member, MemberMaps memberMaps)
		{
			MemberProjectedSlot csideMappedSlotForSMember = wrapper.GetCSideMappedSlotForSMember(member);
			if (csideMappedSlotForSMember == null)
			{
				return expression;
			}
			IEnumerable<Constant> domain = memberMaps.QueryDomainMap.GetDomain(csideMappedSlotForSMember.MemberPath);
			Set<Constant> set = new Set<Constant>(Constant.EqualityComparer);
			if (constant is NegatedConstant)
			{
				set.Unite(domain);
				set.Difference(((NegatedConstant)constant).Elements);
			}
			else
			{
				set.Add(constant);
			}
			MemberRestriction literal = new ScalarRestriction(csideMappedSlotForSMember.MemberPath, set, domain);
			return BoolExpression.CreateAnd(new BoolExpression[]
			{
				expression,
				BoolExpression.CreateLiteral(literal, memberMaps.QueryDomainMap)
			});
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x00092EAC File Offset: 0x000910AC
		private static FragmentQuery AddNullConditionOnCSideFragment(LeftCellWrapper wrapper, MemberPath member, MemberMaps memberMaps)
		{
			MemberProjectedSlot csideMappedSlotForSMember = wrapper.GetCSideMappedSlotForSMember(member);
			if (csideMappedSlotForSMember == null || !csideMappedSlotForSMember.MemberPath.IsNullable)
			{
				return null;
			}
			BoolExpression whereClause = wrapper.RightCellQuery.WhereClause;
			IEnumerable<Constant> domain = memberMaps.QueryDomainMap.GetDomain(csideMappedSlotForSMember.MemberPath);
			Set<Constant> set = new Set<Constant>(Constant.EqualityComparer);
			set.Add(Constant.Null);
			MemberRestriction literal = new ScalarRestriction(csideMappedSlotForSMember.MemberPath, set, domain);
			BoolExpression whereClause2 = BoolExpression.CreateAnd(new BoolExpression[]
			{
				whereClause,
				BoolExpression.CreateLiteral(literal, memberMaps.QueryDomainMap)
			});
			return FragmentQuery.Create(whereClause2);
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x00092F40 File Offset: 0x00091140
		private void CheckConstraintsOnNonNullableMembers(Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode> memberValueTrees, LeftCellWrapper wrapper, CellTreeNode sQueryTree, BoolExpression inExtentCondition)
		{
			foreach (MemberPath memberPath in this._domainMap.NonConditionMembers(this._viewgenContext.Extent))
			{
				bool flag = memberPath.EdmType is SimpleType;
				if (!memberPath.IsNullable && flag)
				{
					FragmentQuery fragmentQuery = RewritingValidator.AddNullConditionOnCSideFragment(wrapper, memberPath, this._viewgenContext.MemberMaps);
					if (fragmentQuery != null && this._viewgenContext.RightFragmentQP.IsSatisfiable(fragmentQuery))
					{
						this._errorLog.AddEntry(new ErrorLog.Record(true, ViewGenErrorCode.NullableMappingForNonNullableColumn, Strings.Viewgen_NullableMappingForNonNullableColumn(wrapper.LeftExtent.ToString(), memberPath.ToFullString()), wrapper.Cells, ""));
					}
				}
			}
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x00093018 File Offset: 0x00091218
		internal static void EntityConfigurationToUserString(BoolExpression condition, StringBuilder builder)
		{
			RewritingValidator.EntityConfigurationToUserString(condition, builder, true);
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x00093022 File Offset: 0x00091222
		internal static void EntityConfigurationToUserString(BoolExpression condition, StringBuilder builder, bool writeRoundTrippingMessage)
		{
			condition.AsUserString(builder, "PK", writeRoundTrippingMessage);
		}

		// Token: 0x040011CC RID: 4556
		private ViewgenContext _viewgenContext;

		// Token: 0x040011CD RID: 4557
		private MemberDomainMap _domainMap;

		// Token: 0x040011CE RID: 4558
		private CellTreeNode _basicView;

		// Token: 0x040011CF RID: 4559
		private IEnumerable<MemberPath> _keyAttributes;

		// Token: 0x040011D0 RID: 4560
		private ErrorLog _errorLog;

		// Token: 0x020005A5 RID: 1445
		private class WhereClauseVisitor : Visitor<DomainConstraint<BoolLiteral, Constant>, CellTreeNode>
		{
			// Token: 0x06004057 RID: 16471 RVA: 0x000ECAF5 File Offset: 0x000EACF5
			internal WhereClauseVisitor(CellTreeNode topLevelTree, Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode> memberValueTrees)
			{
				this._topLevelTree = topLevelTree;
				this._memberValueTrees = memberValueTrees;
				this._viewgenContext = topLevelTree.ViewgenContext;
			}

			// Token: 0x06004058 RID: 16472 RVA: 0x000ECB17 File Offset: 0x000EAD17
			internal CellTreeNode GetCellTreeNode(BoolExpression whereClause)
			{
				return whereClause.Tree.Accept<CellTreeNode>(this);
			}

			// Token: 0x06004059 RID: 16473 RVA: 0x000ECB28 File Offset: 0x000EAD28
			internal override CellTreeNode VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				IEnumerable<CellTreeNode> enumerable = this.AcceptChildren(expression.Children);
				OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this._viewgenContext, CellTreeOpType.IJ);
				foreach (CellTreeNode cellTreeNode in enumerable)
				{
					if (cellTreeNode == null)
					{
						return null;
					}
					if (cellTreeNode != this._topLevelTree)
					{
						opCellTreeNode.Add(cellTreeNode);
					}
				}
				if (opCellTreeNode.Children.Count != 0)
				{
					return opCellTreeNode;
				}
				return this._topLevelTree;
			}

			// Token: 0x0600405A RID: 16474 RVA: 0x000ECBB4 File Offset: 0x000EADB4
			internal override CellTreeNode VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this._topLevelTree;
			}

			// Token: 0x0600405B RID: 16475 RVA: 0x000ECBBC File Offset: 0x000EADBC
			internal override CellTreeNode VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				MemberRestriction memberRestriction = (MemberRestriction)expression.Identifier.Variable.Identifier;
				Set<Constant> range = expression.Identifier.Range;
				OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this._viewgenContext, CellTreeOpType.Union);
				CellTreeNode cellTreeNode = null;
				foreach (Constant value in range)
				{
					if (this.TryGetCellTreeNode(memberRestriction.RestrictedMemberSlot.MemberPath, value, out cellTreeNode))
					{
						opCellTreeNode.Add(cellTreeNode);
					}
				}
				int count = opCellTreeNode.Children.Count;
				if (count == 0)
				{
					return null;
				}
				if (count != 1)
				{
					return opCellTreeNode;
				}
				return cellTreeNode;
			}

			// Token: 0x0600405C RID: 16476 RVA: 0x00072E1F File Offset: 0x0007101F
			internal override CellTreeNode VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600405D RID: 16477 RVA: 0x00072E1F File Offset: 0x0007101F
			internal override CellTreeNode VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600405E RID: 16478 RVA: 0x00072E1F File Offset: 0x0007101F
			internal override CellTreeNode VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600405F RID: 16479 RVA: 0x000ECC74 File Offset: 0x000EAE74
			private bool TryGetCellTreeNode(MemberPath memberPath, Constant value, out CellTreeNode singleNode)
			{
				return this._memberValueTrees.TryGetValue(new RewritingValidator.MemberValueBinding(memberPath, value), out singleNode);
			}

			// Token: 0x06004060 RID: 16480 RVA: 0x000ECC89 File Offset: 0x000EAE89
			private IEnumerable<CellTreeNode> AcceptChildren(IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> children)
			{
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr in children)
				{
					yield return boolExpr.Accept<CellTreeNode>(this);
				}
				IEnumerator<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x04001CE0 RID: 7392
			private ViewgenContext _viewgenContext;

			// Token: 0x04001CE1 RID: 7393
			private CellTreeNode _topLevelTree;

			// Token: 0x04001CE2 RID: 7394
			private Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode> _memberValueTrees;
		}

		// Token: 0x020005A6 RID: 1446
		internal class DomainConstraintVisitor : CellTreeNode.SimpleCellTreeVisitor<bool, bool>
		{
			// Token: 0x06004061 RID: 16481 RVA: 0x000ECCA0 File Offset: 0x000EAEA0
			private DomainConstraintVisitor(LeftCellWrapper wrapper, ViewgenContext context, ErrorLog errorLog)
			{
				this.m_wrapper = wrapper;
				this.m_viewgenContext = context;
				this.m_errorLog = errorLog;
			}

			// Token: 0x06004062 RID: 16482 RVA: 0x000ECCC0 File Offset: 0x000EAEC0
			internal static void CheckConstraints(CellTreeNode node, LeftCellWrapper wrapper, ViewgenContext context, ErrorLog errorLog)
			{
				RewritingValidator.DomainConstraintVisitor visitor = new RewritingValidator.DomainConstraintVisitor(wrapper, context, errorLog);
				node.Accept<bool, bool>(visitor, true);
			}

			// Token: 0x06004063 RID: 16483 RVA: 0x000ECCE0 File Offset: 0x000EAEE0
			internal override bool VisitLeaf(LeafCellTreeNode node, bool dummy)
			{
				CellQuery rightCellQuery = this.m_wrapper.RightCellQuery;
				CellQuery rightCellQuery2 = node.LeftCellWrapper.RightCellQuery;
				List<MemberPath> list = new List<MemberPath>();
				if (rightCellQuery != rightCellQuery2)
				{
					for (int i = 0; i < rightCellQuery.NumProjectedSlots; i++)
					{
						MemberProjectedSlot memberProjectedSlot = rightCellQuery.ProjectedSlotAt(i) as MemberProjectedSlot;
						if (memberProjectedSlot != null)
						{
							MemberProjectedSlot memberProjectedSlot2 = rightCellQuery2.ProjectedSlotAt(i) as MemberProjectedSlot;
							if (memberProjectedSlot2 != null)
							{
								MemberPath memberPath = this.m_viewgenContext.MemberMaps.ProjectedSlotMap[i];
								if (!memberPath.IsPartOfKey && !MemberPath.EqualityComparer.Equals(memberProjectedSlot.MemberPath, memberProjectedSlot2.MemberPath))
								{
									list.Add(memberPath);
								}
							}
						}
					}
				}
				if (list.Count > 0)
				{
					string p = MemberPath.PropertiesToUserString(list, false);
					string message = Strings.ViewGen_NonKeyProjectedWithOverlappingPartitions(p);
					ErrorLog.Record record = new ErrorLog.Record(true, ViewGenErrorCode.NonKeyProjectedWithOverlappingPartitions, message, new LeftCellWrapper[]
					{
						this.m_wrapper,
						node.LeftCellWrapper
					}, string.Empty);
					this.m_errorLog.AddEntry(record);
				}
				return true;
			}

			// Token: 0x06004064 RID: 16484 RVA: 0x000ECDDC File Offset: 0x000EAFDC
			internal override bool VisitOpNode(OpCellTreeNode node, bool dummy)
			{
				if (node.OpType == CellTreeOpType.LASJ)
				{
					node.Children[0].Accept<bool, bool>(this, dummy);
				}
				else
				{
					foreach (CellTreeNode cellTreeNode in node.Children)
					{
						cellTreeNode.Accept<bool, bool>(this, dummy);
					}
				}
				return true;
			}

			// Token: 0x04001CE3 RID: 7395
			private LeftCellWrapper m_wrapper;

			// Token: 0x04001CE4 RID: 7396
			private ViewgenContext m_viewgenContext;

			// Token: 0x04001CE5 RID: 7397
			private ErrorLog m_errorLog;
		}

		// Token: 0x020005A7 RID: 1447
		private struct MemberValueBinding : IEquatable<RewritingValidator.MemberValueBinding>
		{
			// Token: 0x06004065 RID: 16485 RVA: 0x000ECE54 File Offset: 0x000EB054
			public MemberValueBinding(MemberPath member, Constant value)
			{
				this.Member = member;
				this.Value = value;
			}

			// Token: 0x06004066 RID: 16486 RVA: 0x000ECE64 File Offset: 0x000EB064
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}={1}", new object[]
				{
					this.Member,
					this.Value
				});
			}

			// Token: 0x06004067 RID: 16487 RVA: 0x000ECE8D File Offset: 0x000EB08D
			public bool Equals(RewritingValidator.MemberValueBinding other)
			{
				return MemberPath.EqualityComparer.Equals(this.Member, other.Member) && Constant.EqualityComparer.Equals(this.Value, other.Value);
			}

			// Token: 0x04001CE6 RID: 7398
			internal readonly MemberPath Member;

			// Token: 0x04001CE7 RID: 7399
			internal readonly Constant Value;
		}
	}
}
