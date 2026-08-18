using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Data.Mapping.ViewGeneration.Utils;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Validation
{
	// Token: 0x0200027E RID: 638
	internal class ErrorPatternMatcher
	{
		// Token: 0x0600267F RID: 9855 RVA: 0x0009324C File Offset: 0x0009144C
		private ErrorPatternMatcher(ViewgenContext context, MemberDomainMap domainMap, ErrorLog errorLog)
		{
			this.m_viewgenContext = context;
			this.m_domainMap = domainMap;
			this.m_keyAttributes = MemberPath.GetKeyMembers(context.Extent, domainMap);
			this.m_errorLog = errorLog;
			this.m_originalErrorCount = this.m_errorLog.Count;
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x0009328C File Offset: 0x0009148C
		public static bool FindMappingErrors(ViewgenContext context, MemberDomainMap domainMap, ErrorLog errorLog)
		{
			if (context.ViewTarget == ViewTarget.QueryView && !context.Config.IsValidationEnabled)
			{
				return false;
			}
			ErrorPatternMatcher errorPatternMatcher = new ErrorPatternMatcher(context, domainMap, errorLog);
			errorPatternMatcher.MatchMissingMappingErrors();
			errorPatternMatcher.MatchConditionErrors();
			errorPatternMatcher.MatchSplitErrors();
			if (errorPatternMatcher.m_errorLog.Count == errorPatternMatcher.m_originalErrorCount)
			{
				errorPatternMatcher.MatchPartitionErrors();
			}
			if (errorPatternMatcher.m_errorLog.Count > errorPatternMatcher.m_originalErrorCount)
			{
				ExceptionHelpers.ThrowMappingException(errorPatternMatcher.m_errorLog, errorPatternMatcher.m_viewgenContext.Config);
			}
			return false;
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x00093310 File Offset: 0x00091510
		private void MatchMissingMappingErrors()
		{
			if (this.m_viewgenContext.ViewTarget == ViewTarget.QueryView)
			{
				Set<EdmType> set = new Set<EdmType>(MetadataHelper.GetTypeAndSubtypesOf(this.m_viewgenContext.Extent.ElementType, this.m_viewgenContext.EdmItemCollection, false));
				foreach (LeftCellWrapper leftCellWrapper in this.m_viewgenContext.AllWrappersForExtent)
				{
					foreach (Cell cell in leftCellWrapper.Cells)
					{
						foreach (MemberRestriction memberRestriction in cell.CQuery.Conditions)
						{
							foreach (Constant constant in memberRestriction.Domain.Values)
							{
								TypeConstant typeConstant = constant as TypeConstant;
								if (typeConstant != null)
								{
									set.Remove(typeConstant.EdmType);
								}
							}
						}
					}
				}
				if (set.Count > 0)
				{
					this.m_errorLog.AddEntry(new ErrorLog.Record(true, ViewGenErrorCode.ErrorPatternMissingMappingError, Strings.ViewGen_Missing_Type_Mapping(this.BuildCommaSeparatedErrorString<EdmType>(set)), this.m_viewgenContext.AllWrappersForExtent, ""));
				}
			}
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x000934B4 File Offset: 0x000916B4
		private static bool HasNotNullCondition(CellQuery cellQuery, MemberPath member)
		{
			foreach (MemberRestriction memberRestriction in cellQuery.GetConjunctsFromWhereClause())
			{
				if (memberRestriction.RestrictedMemberSlot.MemberPath.Equals(member))
				{
					if (memberRestriction.Domain.Values.Contains(Constant.NotNull))
					{
						return true;
					}
					foreach (NegatedConstant negatedConstant in from cellConstant in memberRestriction.Domain.Values
					select cellConstant as NegatedConstant into negated
					where negated != null
					select negated)
					{
						if (negatedConstant.Elements.Contains(Constant.Null))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x000935D4 File Offset: 0x000917D4
		private static bool IsMemberPartOfNotNullCondition(IEnumerable<LeftCellWrapper> wrappers, MemberPath leftMember, ViewTarget viewTarget)
		{
			Func<MemberPath, bool> <>9__0;
			foreach (LeftCellWrapper leftCellWrapper in wrappers)
			{
				CellQuery leftQuery = leftCellWrapper.OnlyInputCell.GetLeftQuery(viewTarget);
				if (ErrorPatternMatcher.HasNotNullCondition(leftQuery, leftMember))
				{
					return true;
				}
				CellQuery rightQuery = leftCellWrapper.OnlyInputCell.GetRightQuery(viewTarget);
				IEnumerable<MemberPath> projectedMembers = leftQuery.GetProjectedMembers();
				Func<MemberPath, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((MemberPath path) => !path.Equals(leftMember)));
				}
				int num = projectedMembers.TakeWhile(predicate).Count<MemberPath>();
				if (num < leftQuery.GetProjectedMembers().Count<MemberPath>())
				{
					MemberPath memberPath = ((MemberProjectedSlot)rightQuery.ProjectedSlotAt(num)).MemberPath;
					if (ErrorPatternMatcher.HasNotNullCondition(rightQuery, memberPath))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000936C4 File Offset: 0x000918C4
		private void MatchConditionErrors()
		{
			List<LeftCellWrapper> allWrappersForExtent = this.m_viewgenContext.AllWrappersForExtent;
			Set<MemberPath> set = new Set<MemberPath>();
			Set<Dictionary<MemberPath, Set<Constant>>> set2 = new Set<Dictionary<MemberPath, Set<Constant>>>(new ConditionComparer());
			Dictionary<Dictionary<MemberPath, Set<Constant>>, LeftCellWrapper> dictionary = new Dictionary<Dictionary<MemberPath, Set<Constant>>, LeftCellWrapper>(new ConditionComparer());
			foreach (LeftCellWrapper leftCellWrapper in allWrappersForExtent)
			{
				Dictionary<MemberPath, Set<Constant>> dictionary2 = new Dictionary<MemberPath, Set<Constant>>();
				CellQuery leftQuery = leftCellWrapper.OnlyInputCell.GetLeftQuery(this.m_viewgenContext.ViewTarget);
				foreach (MemberRestriction memberRestriction in leftQuery.GetConjunctsFromWhereClause())
				{
					MemberPath memberPath = memberRestriction.RestrictedMemberSlot.MemberPath;
					if (this.m_domainMap.IsConditionMember(memberPath))
					{
						ScalarRestriction scalarRestriction = memberRestriction as ScalarRestriction;
						if (scalarRestriction != null && !set.Contains(memberPath) && !leftCellWrapper.OnlyInputCell.CQuery.WhereClause.Equals(leftCellWrapper.OnlyInputCell.SQuery.WhereClause) && !ErrorPatternMatcher.IsMemberPartOfNotNullCondition(allWrappersForExtent, memberPath, this.m_viewgenContext.ViewTarget))
						{
							this.CheckThatConditionMemberIsNotMapped(memberPath, allWrappersForExtent, set);
						}
						if (this.m_viewgenContext.ViewTarget == ViewTarget.UpdateView && scalarRestriction != null && memberPath.IsNullable && ErrorPatternMatcher.IsMemberPartOfNotNullCondition(new LeftCellWrapper[]
						{
							leftCellWrapper
						}, memberPath, this.m_viewgenContext.ViewTarget))
						{
							MemberPath rightMemberPath = this.GetRightMemberPath(memberPath, leftCellWrapper);
							if (rightMemberPath != null && rightMemberPath.IsNullable && !ErrorPatternMatcher.IsMemberPartOfNotNullCondition(new LeftCellWrapper[]
							{
								leftCellWrapper
							}, rightMemberPath, this.m_viewgenContext.ViewTarget))
							{
								this.m_errorLog.AddEntry(new ErrorLog.Record(true, ViewGenErrorCode.ErrorPatternConditionError, Strings.Viewgen_ErrorPattern_NotNullConditionMappedToNullableMember(memberPath, rightMemberPath), leftCellWrapper.OnlyInputCell, ""));
							}
						}
						foreach (Constant element in memberRestriction.Domain.Values)
						{
							Set<Constant> set3;
							if (!dictionary2.TryGetValue(memberPath, out set3))
							{
								set3 = new Set<Constant>(Constant.EqualityComparer);
								dictionary2.Add(memberPath, set3);
							}
							set3.Add(element);
						}
					}
				}
				if (dictionary2.Count > 0)
				{
					if (set2.Contains(dictionary2))
					{
						if (!this.RightSideEqual(dictionary[dictionary2], leftCellWrapper))
						{
							this.m_errorLog.AddEntry(new ErrorLog.Record(true, ViewGenErrorCode.ErrorPatternConditionError, Strings.Viewgen_ErrorPattern_DuplicateConditionValue(this.BuildCommaSeparatedErrorString<MemberPath>(dictionary2.Keys)), this.ToIEnum(dictionary[dictionary2].OnlyInputCell, leftCellWrapper.OnlyInputCell), ""));
						}
					}
					else
					{
						set2.Add(dictionary2);
						dictionary.Add(dictionary2, leftCellWrapper);
					}
				}
			}
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000939D8 File Offset: 0x00091BD8
		private MemberPath GetRightMemberPath(MemberPath conditionMember, LeftCellWrapper leftCellWrapper)
		{
			CellQuery rightQuery = leftCellWrapper.OnlyInputCell.GetRightQuery(ViewTarget.QueryView);
			List<int> projectedPositions = rightQuery.GetProjectedPositions(conditionMember);
			if (projectedPositions.Count != 1)
			{
				return null;
			}
			int slotNum = projectedPositions.First<int>();
			CellQuery leftQuery = leftCellWrapper.OnlyInputCell.GetLeftQuery(ViewTarget.QueryView);
			return ((MemberProjectedSlot)leftQuery.ProjectedSlotAt(slotNum)).MemberPath;
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x00093A2C File Offset: 0x00091C2C
		private void MatchSplitErrors()
		{
			List<LeftCellWrapper> allWrappersForExtent = this.m_viewgenContext.AllWrappersForExtent;
			IEnumerable<LeftCellWrapper> enumerable = from r in allWrappersForExtent
			where !(r.LeftExtent is AssociationSet) && !(r.RightCellQuery.Extent is AssociationSet)
			select r;
			if (this.m_viewgenContext.ViewTarget == ViewTarget.UpdateView && enumerable.Any<LeftCellWrapper>())
			{
				LeftCellWrapper leftCellWrapper = enumerable.First<LeftCellWrapper>();
				EntitySetBase extent = leftCellWrapper.RightCellQuery.Extent;
				foreach (LeftCellWrapper leftCellWrapper2 in enumerable)
				{
					if (!leftCellWrapper2.RightCellQuery.Extent.EdmEquals(extent) && !this.RightSideEqual(leftCellWrapper2, leftCellWrapper))
					{
						this.m_errorLog.AddEntry(new ErrorLog.Record(true, ViewGenErrorCode.ErrorPatternSplittingError, Strings.Viewgen_ErrorPattern_TableMappedToMultipleES(leftCellWrapper2.LeftExtent.ToString(), leftCellWrapper2.RightCellQuery.Extent.ToString(), extent.ToString()), leftCellWrapper2.Cells.First<Cell>(), ""));
					}
				}
			}
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x00093B44 File Offset: 0x00091D44
		private void MatchPartitionErrors()
		{
			List<LeftCellWrapper> allWrappersForExtent = this.m_viewgenContext.AllWrappersForExtent;
			int num = 0;
			foreach (LeftCellWrapper leftCellWrapper in allWrappersForExtent)
			{
				foreach (LeftCellWrapper leftCellWrapper2 in allWrappersForExtent.Skip(++num))
				{
					FragmentQuery fragmentQuery = this.CreateRightFragmentQuery(leftCellWrapper);
					FragmentQuery fragmentQuery2 = this.CreateRightFragmentQuery(leftCellWrapper2);
					bool flag = this.CompareS(ErrorPatternMatcher.ComparisonOP.IsDisjointFrom, this.m_viewgenContext, leftCellWrapper, leftCellWrapper2, fragmentQuery, fragmentQuery2);
					bool flag2 = this.CompareC(ErrorPatternMatcher.ComparisonOP.IsDisjointFrom, this.m_viewgenContext, leftCellWrapper, leftCellWrapper2, fragmentQuery, fragmentQuery2);
					bool flag3;
					bool flag4;
					bool flag5;
					if (flag)
					{
						if (flag2)
						{
							continue;
						}
						flag3 = this.CompareC(ErrorPatternMatcher.ComparisonOP.IsContainedIn, this.m_viewgenContext, leftCellWrapper, leftCellWrapper2, fragmentQuery, fragmentQuery2);
						flag4 = this.CompareC(ErrorPatternMatcher.ComparisonOP.IsContainedIn, this.m_viewgenContext, leftCellWrapper2, leftCellWrapper, fragmentQuery2, fragmentQuery);
						flag5 = (flag3 && flag4);
						StringBuilder stringBuilder = new StringBuilder();
						if (flag5)
						{
							stringBuilder.Append(Strings.Viewgen_ErrorPattern_Partition_Disj_Eq);
						}
						else if (flag3 || flag4)
						{
							if (this.CSideHasDifferentEntitySets(leftCellWrapper, leftCellWrapper2))
							{
								stringBuilder.Append(Strings.Viewgen_ErrorPattern_Partition_Disj_Subs_Ref);
							}
							else
							{
								stringBuilder.Append(Strings.Viewgen_ErrorPattern_Partition_Disj_Subs);
							}
						}
						else
						{
							stringBuilder.Append(Strings.Viewgen_ErrorPattern_Partition_Disj_Unk);
						}
						this.m_errorLog.AddEntry(new ErrorLog.Record(true, ViewGenErrorCode.ErrorPatternInvalidPartitionError, stringBuilder.ToString(), this.ToIEnum(leftCellWrapper.OnlyInputCell, leftCellWrapper2.OnlyInputCell), ""));
						if (this.FoundTooManyErrors())
						{
							return;
						}
					}
					else
					{
						flag3 = this.CompareC(ErrorPatternMatcher.ComparisonOP.IsContainedIn, this.m_viewgenContext, leftCellWrapper, leftCellWrapper2, fragmentQuery, fragmentQuery2);
						flag4 = this.CompareC(ErrorPatternMatcher.ComparisonOP.IsContainedIn, this.m_viewgenContext, leftCellWrapper2, leftCellWrapper, fragmentQuery2, fragmentQuery);
					}
					bool flag6 = this.CompareS(ErrorPatternMatcher.ComparisonOP.IsContainedIn, this.m_viewgenContext, leftCellWrapper, leftCellWrapper2, fragmentQuery, fragmentQuery2);
					bool flag7 = this.CompareS(ErrorPatternMatcher.ComparisonOP.IsContainedIn, this.m_viewgenContext, leftCellWrapper2, leftCellWrapper, fragmentQuery2, fragmentQuery);
					flag5 = (flag3 && flag4);
					bool flag8 = flag6 && flag7;
					if (flag8)
					{
						if (!flag5)
						{
							StringBuilder stringBuilder2 = new StringBuilder();
							if (flag2)
							{
								stringBuilder2.Append(Strings.Viewgen_ErrorPattern_Partition_Eq_Disj);
							}
							else if (flag3 || flag4)
							{
								if (this.CSideHasDifferentEntitySets(leftCellWrapper, leftCellWrapper2))
								{
									stringBuilder2.Append(Strings.Viewgen_ErrorPattern_Partition_Eq_Subs_Ref);
								}
								else
								{
									if (leftCellWrapper.LeftExtent.Equals(leftCellWrapper2.LeftExtent))
									{
										bool flag9;
										List<EdmType> list;
										this.GetTypesAndConditionForWrapper(leftCellWrapper, out flag9, out list);
										bool flag10;
										List<EdmType> list2;
										this.GetTypesAndConditionForWrapper(leftCellWrapper2, out flag10, out list2);
										if (!flag9 && !flag10 && (list.Except(list2).Count<EdmType>() != 0 || list2.Except(list).Count<EdmType>() != 0) && (!this.CheckForStoreConditions(leftCellWrapper) || !this.CheckForStoreConditions(leftCellWrapper2)))
										{
											IEnumerable<string> list3 = (from it in list
											select it.FullName).Union(from it in list2
											select it.FullName);
											this.m_errorLog.AddEntry(new ErrorLog.Record(true, ViewGenErrorCode.ErrorPatternConditionError, Strings.Viewgen_ErrorPattern_Partition_MultipleTypesMappedToSameTable_WithoutCondition(StringUtil.ToCommaSeparatedString(list3), leftCellWrapper.LeftExtent), this.ToIEnum(leftCellWrapper.OnlyInputCell, leftCellWrapper2.OnlyInputCell), ""));
											return;
										}
									}
									stringBuilder2.Append(Strings.Viewgen_ErrorPattern_Partition_Eq_Subs);
								}
							}
							else if (!this.IsQueryView() && (leftCellWrapper.OnlyInputCell.CQuery.Extent is AssociationSet || leftCellWrapper2.OnlyInputCell.CQuery.Extent is AssociationSet))
							{
								stringBuilder2.Append(Strings.Viewgen_ErrorPattern_Partition_Eq_Unk_Association);
							}
							else
							{
								stringBuilder2.Append(Strings.Viewgen_ErrorPattern_Partition_Eq_Unk);
							}
							this.m_errorLog.AddEntry(new ErrorLog.Record(true, ViewGenErrorCode.ErrorPatternInvalidPartitionError, stringBuilder2.ToString(), this.ToIEnum(leftCellWrapper.OnlyInputCell, leftCellWrapper2.OnlyInputCell), ""));
							if (this.FoundTooManyErrors())
							{
								return;
							}
						}
					}
					else if ((flag6 || flag7) && (!flag6 || !flag3 || flag4) && (!flag7 || !flag4 || flag3))
					{
						StringBuilder stringBuilder3 = new StringBuilder();
						if (flag2)
						{
							stringBuilder3.Append(Strings.Viewgen_ErrorPattern_Partition_Sub_Disj);
						}
						else if (flag5)
						{
							if (this.CSideHasDifferentEntitySets(leftCellWrapper, leftCellWrapper2))
							{
								stringBuilder3.Append(" " + Strings.Viewgen_ErrorPattern_Partition_Sub_Eq_Ref);
							}
							else
							{
								stringBuilder3.Append(Strings.Viewgen_ErrorPattern_Partition_Sub_Eq);
							}
						}
						else
						{
							stringBuilder3.Append(Strings.Viewgen_ErrorPattern_Partition_Sub_Unk);
						}
						this.m_errorLog.AddEntry(new ErrorLog.Record(true, ViewGenErrorCode.ErrorPatternInvalidPartitionError, stringBuilder3.ToString(), this.ToIEnum(leftCellWrapper.OnlyInputCell, leftCellWrapper2.OnlyInputCell), ""));
						if (this.FoundTooManyErrors())
						{
							return;
						}
					}
				}
			}
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x00094048 File Offset: 0x00092248
		private void GetTypesAndConditionForWrapper(LeftCellWrapper wrapper, out bool hasCondition, out List<EdmType> edmTypes)
		{
			hasCondition = false;
			edmTypes = new List<EdmType>();
			foreach (Cell cell in wrapper.Cells)
			{
				foreach (MemberRestriction memberRestriction in cell.CQuery.Conditions)
				{
					foreach (Constant constant in memberRestriction.Domain.Values)
					{
						TypeConstant typeConstant = constant as TypeConstant;
						if (typeConstant != null)
						{
							edmTypes.Add(typeConstant.EdmType);
						}
						else
						{
							hasCondition = true;
						}
					}
				}
			}
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x00094138 File Offset: 0x00092338
		private bool CheckForStoreConditions(LeftCellWrapper wrapper)
		{
			return wrapper.Cells.SelectMany((Cell c) => c.SQuery.Conditions).Any<MemberRestriction>();
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x0009416C File Offset: 0x0009236C
		private void CheckThatConditionMemberIsNotMapped(MemberPath conditionMember, List<LeftCellWrapper> mappingFragments, Set<MemberPath> mappedConditionMembers)
		{
			foreach (LeftCellWrapper leftCellWrapper in mappingFragments)
			{
				foreach (Cell cell in leftCellWrapper.Cells)
				{
					CellQuery leftQuery = cell.GetLeftQuery(this.m_viewgenContext.ViewTarget);
					if (leftQuery.GetProjectedMembers().Contains(conditionMember))
					{
						mappedConditionMembers.Add(conditionMember);
						this.m_errorLog.AddEntry(new ErrorLog.Record(true, ViewGenErrorCode.ErrorPatternConditionError, Strings.Viewgen_ErrorPattern_ConditionMemberIsMapped(conditionMember.ToString()), cell, ""));
					}
				}
			}
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x00094240 File Offset: 0x00092440
		private bool FoundTooManyErrors()
		{
			return this.m_errorLog.Count > this.m_originalErrorCount + 5;
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x00094258 File Offset: 0x00092458
		private string BuildCommaSeparatedErrorString<T>(IEnumerable<T> members)
		{
			StringBuilder stringBuilder = new StringBuilder();
			T t = members.First<T>();
			foreach (T t2 in members)
			{
				if (!t2.Equals(t))
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("'" + t2.ToString() + "'");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x000942F0 File Offset: 0x000924F0
		private bool CSideHasDifferentEntitySets(LeftCellWrapper a, LeftCellWrapper b)
		{
			if (this.IsQueryView())
			{
				return a.LeftExtent == b.LeftExtent;
			}
			return a.RightCellQuery == b.RightCellQuery;
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x00094317 File Offset: 0x00092517
		private bool CompareC(ErrorPatternMatcher.ComparisonOP op, ViewgenContext context, LeftCellWrapper leftWrapper1, LeftCellWrapper leftWrapper2, FragmentQuery rightQuery1, FragmentQuery rightQuery2)
		{
			return this.Compare(true, op, context, leftWrapper1, leftWrapper2, rightQuery1, rightQuery2);
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x00094329 File Offset: 0x00092529
		private bool CompareS(ErrorPatternMatcher.ComparisonOP op, ViewgenContext context, LeftCellWrapper leftWrapper1, LeftCellWrapper leftWrapper2, FragmentQuery rightQuery1, FragmentQuery rightQuery2)
		{
			return this.Compare(false, op, context, leftWrapper1, leftWrapper2, rightQuery1, rightQuery2);
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x0009433C File Offset: 0x0009253C
		private bool Compare(bool lookingForC, ErrorPatternMatcher.ComparisonOP op, ViewgenContext context, LeftCellWrapper leftWrapper1, LeftCellWrapper leftWrapper2, FragmentQuery rightQuery1, FragmentQuery rightQuery2)
		{
			LCWComparer lcwcomparer;
			if ((lookingForC && this.IsQueryView()) || (!lookingForC && !this.IsQueryView()))
			{
				if (op == ErrorPatternMatcher.ComparisonOP.IsContainedIn)
				{
					lcwcomparer = new LCWComparer(context.LeftFragmentQP.IsContainedIn);
				}
				else
				{
					if (op != ErrorPatternMatcher.ComparisonOP.IsDisjointFrom)
					{
						return false;
					}
					lcwcomparer = new LCWComparer(context.LeftFragmentQP.IsDisjointFrom);
				}
				return lcwcomparer(leftWrapper1.FragmentQuery, leftWrapper2.FragmentQuery);
			}
			if (op == ErrorPatternMatcher.ComparisonOP.IsContainedIn)
			{
				lcwcomparer = new LCWComparer(context.RightFragmentQP.IsContainedIn);
			}
			else
			{
				if (op != ErrorPatternMatcher.ComparisonOP.IsDisjointFrom)
				{
					return false;
				}
				lcwcomparer = new LCWComparer(context.RightFragmentQP.IsDisjointFrom);
			}
			return lcwcomparer(rightQuery1, rightQuery2);
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000943E0 File Offset: 0x000925E0
		private bool RightSideEqual(LeftCellWrapper wrapper1, LeftCellWrapper wrapper2)
		{
			FragmentQuery q = this.CreateRightFragmentQuery(wrapper1);
			FragmentQuery q2 = this.CreateRightFragmentQuery(wrapper2);
			return this.m_viewgenContext.RightFragmentQP.IsEquivalentTo(q, q2);
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x0009440F File Offset: 0x0009260F
		private FragmentQuery CreateRightFragmentQuery(LeftCellWrapper wrapper)
		{
			return FragmentQuery.Create(wrapper.OnlyInputCell.CellLabel.ToString(), wrapper.CreateRoleBoolean(), wrapper.OnlyInputCell.GetRightQuery(this.m_viewgenContext.ViewTarget));
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x00094444 File Offset: 0x00092644
		private IEnumerable<Cell> ToIEnum(Cell one, Cell two)
		{
			return new List<Cell>
			{
				one,
				two
			};
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x00094466 File Offset: 0x00092666
		private bool IsQueryView()
		{
			return this.m_viewgenContext.ViewTarget == ViewTarget.QueryView;
		}

		// Token: 0x040011D4 RID: 4564
		private ViewgenContext m_viewgenContext;

		// Token: 0x040011D5 RID: 4565
		private MemberDomainMap m_domainMap;

		// Token: 0x040011D6 RID: 4566
		private IEnumerable<MemberPath> m_keyAttributes;

		// Token: 0x040011D7 RID: 4567
		private ErrorLog m_errorLog;

		// Token: 0x040011D8 RID: 4568
		private int m_originalErrorCount;

		// Token: 0x040011D9 RID: 4569
		private const int NUM_PARTITION_ERR_TO_FIND = 5;

		// Token: 0x020005A8 RID: 1448
		private enum ComparisonOP
		{
			// Token: 0x04001CE9 RID: 7401
			IsContainedIn,
			// Token: 0x04001CEA RID: 7402
			IsDisjointFrom
		}
	}
}
