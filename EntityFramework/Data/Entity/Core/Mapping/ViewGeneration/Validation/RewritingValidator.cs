using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000441 RID: 1089
	internal class RewritingValidator
	{
		// Token: 0x06002822 RID: 10274 RVA: 0x000C3CF4 File Offset: 0x000C1EF4
		internal RewritingValidator(ViewgenContext context, CellTreeNode basicView)
		{
			this._viewgenContext = context;
			this._basicView = basicView;
			this._domainMap = this._viewgenContext.MemberMaps.UpdateDomainMap;
			this._keyAttributes = MemberPath.GetKeyMembers(this._viewgenContext.Extent, this._domainMap);
			this._errorLog = new ErrorLog();
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x000C3D54 File Offset: 0x000C1F54
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
					this.CheckConstraintsOnNonNullableMembers(leftCellWrapper);
				}
			}
			if (this._errorLog.Count > 0)
			{
				ExceptionHelpers.ThrowMappingException(this._errorLog, this._viewgenContext.Config);
			}
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000C3F80 File Offset: 0x000C2180
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

		// Token: 0x06002825 RID: 10277 RVA: 0x000C4058 File Offset: 0x000C2258
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
			this._errorLog.AddEntry(new ErrorLog.Record(errorCode, stringBuilder.ToString(), hashSet, ""));
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x000C40D4 File Offset: 0x000C22D4
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

		// Token: 0x06002827 RID: 10279 RVA: 0x000C4238 File Offset: 0x000C2438
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

		// Token: 0x06002828 RID: 10280 RVA: 0x000C43E0 File Offset: 0x000C25E0
		internal static BoolExpression PropagateCellConstantsToWhereClause(LeftCellWrapper wrapper, BoolExpression expression, Constant constant, MemberPath member, MemberMaps memberMaps)
		{
			MemberProjectedSlot csideMappedSlotForSMember = wrapper.GetCSideMappedSlotForSMember(member);
			if (csideMappedSlotForSMember == null)
			{
				return expression;
			}
			NegatedConstant negatedConstant = constant as NegatedConstant;
			IEnumerable<Constant> domain = memberMaps.QueryDomainMap.GetDomain(csideMappedSlotForSMember.MemberPath);
			Set<Constant> set = new Set<Constant>(Constant.EqualityComparer);
			if (negatedConstant != null)
			{
				set.Unite(domain);
				set.Difference(negatedConstant.Elements);
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

		// Token: 0x06002829 RID: 10281 RVA: 0x000C4478 File Offset: 0x000C2678
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

		// Token: 0x0600282A RID: 10282 RVA: 0x000C4510 File Offset: 0x000C2710
		private void CheckConstraintsOnNonNullableMembers(LeftCellWrapper wrapper)
		{
			foreach (MemberPath memberPath in this._domainMap.NonConditionMembers(this._viewgenContext.Extent))
			{
				bool flag = memberPath.EdmType is SimpleType;
				if (!memberPath.IsNullable && flag)
				{
					FragmentQuery fragmentQuery = RewritingValidator.AddNullConditionOnCSideFragment(wrapper, memberPath, this._viewgenContext.MemberMaps);
					if (fragmentQuery != null && this._viewgenContext.RightFragmentQP.IsSatisfiable(fragmentQuery))
					{
						this._errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.NullableMappingForNonNullableColumn, Strings.Viewgen_NullableMappingForNonNullableColumn(wrapper.LeftExtent.ToString(), memberPath.ToFullString()), wrapper.Cells, ""));
					}
				}
			}
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x000C45E8 File Offset: 0x000C27E8
		internal static void EntityConfigurationToUserString(BoolExpression condition, StringBuilder builder)
		{
			RewritingValidator.EntityConfigurationToUserString(condition, builder, true);
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x000C45F2 File Offset: 0x000C27F2
		internal static void EntityConfigurationToUserString(BoolExpression condition, StringBuilder builder, bool writeRoundTrippingMessage)
		{
			condition.AsUserString(builder, "PK", writeRoundTrippingMessage);
		}

		// Token: 0x04000F25 RID: 3877
		private readonly ViewgenContext _viewgenContext;

		// Token: 0x04000F26 RID: 3878
		private readonly MemberDomainMap _domainMap;

		// Token: 0x04000F27 RID: 3879
		private readonly CellTreeNode _basicView;

		// Token: 0x04000F28 RID: 3880
		private readonly IEnumerable<MemberPath> _keyAttributes;

		// Token: 0x04000F29 RID: 3881
		private readonly ErrorLog _errorLog;

		// Token: 0x02000442 RID: 1090
		private class WhereClauseVisitor : Visitor<DomainConstraint<BoolLiteral, Constant>, CellTreeNode>
		{
			// Token: 0x0600282D RID: 10285 RVA: 0x000C4602 File Offset: 0x000C2802
			internal WhereClauseVisitor(CellTreeNode topLevelTree, Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode> memberValueTrees)
			{
				this._topLevelTree = topLevelTree;
				this._memberValueTrees = memberValueTrees;
				this._viewgenContext = topLevelTree.ViewgenContext;
			}

			// Token: 0x0600282E RID: 10286 RVA: 0x000C4624 File Offset: 0x000C2824
			internal CellTreeNode GetCellTreeNode(BoolExpression whereClause)
			{
				return whereClause.Tree.Accept<CellTreeNode>(this);
			}

			// Token: 0x0600282F RID: 10287 RVA: 0x000C4634 File Offset: 0x000C2834
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

			// Token: 0x06002830 RID: 10288 RVA: 0x000C46C4 File Offset: 0x000C28C4
			internal override CellTreeNode VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this._topLevelTree;
			}

			// Token: 0x06002831 RID: 10289 RVA: 0x000C46CC File Offset: 0x000C28CC
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
				switch (opCellTreeNode.Children.Count)
				{
				case 0:
					return null;
				case 1:
					return cellTreeNode;
				default:
					return opCellTreeNode;
				}
			}

			// Token: 0x06002832 RID: 10290 RVA: 0x000C4788 File Offset: 0x000C2988
			internal override CellTreeNode VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06002833 RID: 10291 RVA: 0x000C478F File Offset: 0x000C298F
			internal override CellTreeNode VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06002834 RID: 10292 RVA: 0x000C4796 File Offset: 0x000C2996
			internal override CellTreeNode VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06002835 RID: 10293 RVA: 0x000C479D File Offset: 0x000C299D
			private bool TryGetCellTreeNode(MemberPath memberPath, Constant value, out CellTreeNode singleNode)
			{
				return this._memberValueTrees.TryGetValue(new RewritingValidator.MemberValueBinding(memberPath, value), out singleNode);
			}

			// Token: 0x06002836 RID: 10294 RVA: 0x000C4950 File Offset: 0x000C2B50
			private IEnumerable<CellTreeNode> AcceptChildren(IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> children)
			{
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> child in children)
				{
					yield return child.Accept<CellTreeNode>(this);
				}
				yield break;
			}

			// Token: 0x04000F2A RID: 3882
			private readonly ViewgenContext _viewgenContext;

			// Token: 0x04000F2B RID: 3883
			private readonly CellTreeNode _topLevelTree;

			// Token: 0x04000F2C RID: 3884
			private readonly Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode> _memberValueTrees;
		}

		// Token: 0x0200044A RID: 1098
		internal class DomainConstraintVisitor : CellTreeNode.SimpleCellTreeVisitor<bool, bool>
		{
			// Token: 0x06002874 RID: 10356 RVA: 0x000C526F File Offset: 0x000C346F
			private DomainConstraintVisitor(LeftCellWrapper wrapper, ViewgenContext context, ErrorLog errorLog)
			{
				this.m_wrapper = wrapper;
				this.m_viewgenContext = context;
				this.m_errorLog = errorLog;
			}

			// Token: 0x06002875 RID: 10357 RVA: 0x000C528C File Offset: 0x000C348C
			internal static void CheckConstraints(CellTreeNode node, LeftCellWrapper wrapper, ViewgenContext context, ErrorLog errorLog)
			{
				RewritingValidator.DomainConstraintVisitor visitor = new RewritingValidator.DomainConstraintVisitor(wrapper, context, errorLog);
				node.Accept<bool, bool>(visitor, true);
			}

			// Token: 0x06002876 RID: 10358 RVA: 0x000C52AC File Offset: 0x000C34AC
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
					ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.NonKeyProjectedWithOverlappingPartitions, message, new LeftCellWrapper[]
					{
						this.m_wrapper,
						node.LeftCellWrapper
					}, string.Empty);
					this.m_errorLog.AddEntry(record);
				}
				return true;
			}

			// Token: 0x06002877 RID: 10359 RVA: 0x000C53B0 File Offset: 0x000C35B0
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

			// Token: 0x04000F2F RID: 3887
			private readonly LeftCellWrapper m_wrapper;

			// Token: 0x04000F30 RID: 3888
			private readonly ViewgenContext m_viewgenContext;

			// Token: 0x04000F31 RID: 3889
			private readonly ErrorLog m_errorLog;
		}

		// Token: 0x0200044B RID: 1099
		private struct MemberValueBinding : IEquatable<RewritingValidator.MemberValueBinding>
		{
			// Token: 0x06002878 RID: 10360 RVA: 0x000C5428 File Offset: 0x000C3628
			public MemberValueBinding(MemberPath member, Constant value)
			{
				this.Member = member;
				this.Value = value;
			}

			// Token: 0x06002879 RID: 10361 RVA: 0x000C5438 File Offset: 0x000C3638
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}={1}", new object[]
				{
					this.Member,
					this.Value
				});
			}

			// Token: 0x0600287A RID: 10362 RVA: 0x000C546E File Offset: 0x000C366E
			public bool Equals(RewritingValidator.MemberValueBinding other)
			{
				return MemberPath.EqualityComparer.Equals(this.Member, other.Member) && Constant.EqualityComparer.Equals(this.Value, other.Value);
			}

			// Token: 0x04000F32 RID: 3890
			internal readonly MemberPath Member;

			// Token: 0x04000F33 RID: 3891
			internal readonly Constant Value;
		}
	}
}
