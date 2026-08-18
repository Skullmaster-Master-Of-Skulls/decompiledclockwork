using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000495 RID: 1173
	internal class ErrorPatternMatcher
	{
		// Token: 0x06002B38 RID: 11064 RVA: 0x000D0DE4 File Offset: 0x000CEFE4
		private ErrorPatternMatcher(ViewgenContext context, MemberDomainMap domainMap, ErrorLog errorLog)
		{
			this.m_viewgenContext = context;
			this.m_domainMap = domainMap;
			MemberPath.GetKeyMembers(context.Extent, domainMap);
			this.m_errorLog = errorLog;
			this.m_originalErrorCount = this.m_errorLog.Count;
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x000D0E20 File Offset: 0x000CF020
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

		// Token: 0x06002B3A RID: 11066 RVA: 0x000D0EA4 File Offset: 0x000CF0A4
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
					this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternMissingMappingError, Strings.ViewGen_Missing_Type_Mapping(ErrorPatternMatcher.BuildCommaSeparatedErrorString<EdmType>(set)), this.m_viewgenContext.AllWrappersForExtent, ""));
				}
			}
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x000D105C File Offset: 0x000CF25C
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

		// Token: 0x06002B3C RID: 11068 RVA: 0x000D1194 File Offset: 0x000CF394
		private static bool IsMemberPartOfNotNullCondition(IEnumerable<LeftCellWrapper> wrappers, MemberPath leftMember, ViewTarget viewTarget)
		{
			foreach (LeftCellWrapper leftCellWrapper in wrappers)
			{
				CellQuery leftQuery = leftCellWrapper.OnlyInputCell.GetLeftQuery(viewTarget);
				if (ErrorPatternMatcher.HasNotNullCondition(leftQuery, leftMember))
				{
					return true;
				}
				CellQuery rightQuery = leftCellWrapper.OnlyInputCell.GetRightQuery(viewTarget);
				int num = leftQuery.GetProjectedMembers().TakeWhile((MemberPath path) => !path.Equals(leftMember)).Count<MemberPath>();
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

		// Token: 0x06002B3D RID: 11069 RVA: 0x000D127C File Offset: 0x000CF47C
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
							MemberPath rightMemberPath = ErrorPatternMatcher.GetRightMemberPath(memberPath, leftCellWrapper);
							if (rightMemberPath != null && rightMemberPath.IsNullable && !ErrorPatternMatcher.IsMemberPartOfNotNullCondition(new LeftCellWrapper[]
							{
								leftCellWrapper
							}, rightMemberPath, this.m_viewgenContext.ViewTarget))
							{
								this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternConditionError, Strings.Viewgen_ErrorPattern_NotNullConditionMappedToNullableMember(memberPath, rightMemberPath), leftCellWrapper.OnlyInputCell, ""));
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
							this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternConditionError, Strings.Viewgen_ErrorPattern_DuplicateConditionValue(ErrorPatternMatcher.BuildCommaSeparatedErrorString<MemberPath>(dictionary2.Keys)), ErrorPatternMatcher.ToIEnum(dictionary[dictionary2].OnlyInputCell, leftCellWrapper.OnlyInputCell), ""));
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

		// Token: 0x06002B3E RID: 11070 RVA: 0x000D1594 File Offset: 0x000CF794
		private static MemberPath GetRightMemberPath(MemberPath conditionMember, LeftCellWrapper leftCellWrapper)
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

		// Token: 0x06002B3F RID: 11071 RVA: 0x000D1610 File Offset: 0x000CF810
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
						this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternSplittingError, Strings.Viewgen_ErrorPattern_TableMappedToMultipleES(leftCellWrapper2.LeftExtent.ToString(), leftCellWrapper2.RightCellQuery.Extent.ToString(), extent.ToString()), leftCellWrapper2.Cells.First<Cell>(), ""));
					}
				}
			}
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x000D1734 File Offset: 0x000CF934
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
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
						this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternInvalidPartitionError, stringBuilder.ToString(), ErrorPatternMatcher.ToIEnum(leftCellWrapper.OnlyInputCell, leftCellWrapper2.OnlyInputCell), ""));
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
										ErrorPatternMatcher.GetTypesAndConditionForWrapper(leftCellWrapper, out flag9, out list);
										bool flag10;
										List<EdmType> list2;
										ErrorPatternMatcher.GetTypesAndConditionForWrapper(leftCellWrapper2, out flag10, out list2);
										if (!flag9 && !flag10 && (list.Except(list2).Count<EdmType>() != 0 || list2.Except(list).Count<EdmType>() != 0) && (!ErrorPatternMatcher.CheckForStoreConditions(leftCellWrapper) || !ErrorPatternMatcher.CheckForStoreConditions(leftCellWrapper2)))
										{
											IEnumerable<string> list3 = (from it in list
											select it.FullName).Union(from it in list2
											select it.FullName);
											this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternConditionError, Strings.Viewgen_ErrorPattern_Partition_MultipleTypesMappedToSameTable_WithoutCondition(StringUtil.ToCommaSeparatedString(list3), leftCellWrapper.LeftExtent), ErrorPatternMatcher.ToIEnum(leftCellWrapper.OnlyInputCell, leftCellWrapper2.OnlyInputCell), ""));
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
							this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternInvalidPartitionError, stringBuilder2.ToString(), ErrorPatternMatcher.ToIEnum(leftCellWrapper.OnlyInputCell, leftCellWrapper2.OnlyInputCell), ""));
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
						this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternInvalidPartitionError, stringBuilder3.ToString(), ErrorPatternMatcher.ToIEnum(leftCellWrapper.OnlyInputCell, leftCellWrapper2.OnlyInputCell), ""));
						if (this.FoundTooManyErrors())
						{
							return;
						}
					}
				}
			}
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x000D1C24 File Offset: 0x000CFE24
		private static void GetTypesAndConditionForWrapper(LeftCellWrapper wrapper, out bool hasCondition, out List<EdmType> edmTypes)
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

		// Token: 0x06002B42 RID: 11074 RVA: 0x000D1D29 File Offset: 0x000CFF29
		private static bool CheckForStoreConditions(LeftCellWrapper wrapper)
		{
			return wrapper.Cells.SelectMany((Cell c) => c.SQuery.Conditions).Any<MemberRestriction>();
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x000D1D58 File Offset: 0x000CFF58
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
						this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.ErrorPatternConditionError, Strings.Viewgen_ErrorPattern_ConditionMemberIsMapped(conditionMember.ToString()), cell, ""));
					}
				}
			}
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x000D1E2C File Offset: 0x000D002C
		private bool FoundTooManyErrors()
		{
			return this.m_errorLog.Count > this.m_originalErrorCount + 5;
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x000D1E44 File Offset: 0x000D0044
		private static string BuildCommaSeparatedErrorString<T>(IEnumerable<T> members)
		{
			StringBuilder stringBuilder = new StringBuilder();
			T t = members.First<T>();
			foreach (T t2 in members)
			{
				if (!t2.Equals(t))
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("'" + t2 + "'");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000D1ED8 File Offset: 0x000D00D8
		private bool CSideHasDifferentEntitySets(LeftCellWrapper a, LeftCellWrapper b)
		{
			if (this.IsQueryView())
			{
				return a.LeftExtent == b.LeftExtent;
			}
			return a.RightCellQuery == b.RightCellQuery;
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x000D1EFF File Offset: 0x000D00FF
		private bool CompareC(ErrorPatternMatcher.ComparisonOP op, ViewgenContext context, LeftCellWrapper leftWrapper1, LeftCellWrapper leftWrapper2, FragmentQuery rightQuery1, FragmentQuery rightQuery2)
		{
			return this.Compare(true, op, context, leftWrapper1, leftWrapper2, rightQuery1, rightQuery2);
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x000D1F11 File Offset: 0x000D0111
		private bool CompareS(ErrorPatternMatcher.ComparisonOP op, ViewgenContext context, LeftCellWrapper leftWrapper1, LeftCellWrapper leftWrapper2, FragmentQuery rightQuery1, FragmentQuery rightQuery2)
		{
			return this.Compare(false, op, context, leftWrapper1, leftWrapper2, rightQuery1, rightQuery2);
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x000D1F24 File Offset: 0x000D0124
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

		// Token: 0x06002B4A RID: 11082 RVA: 0x000D1FC8 File Offset: 0x000D01C8
		private bool RightSideEqual(LeftCellWrapper wrapper1, LeftCellWrapper wrapper2)
		{
			FragmentQuery q = this.CreateRightFragmentQuery(wrapper1);
			FragmentQuery q2 = this.CreateRightFragmentQuery(wrapper2);
			return this.m_viewgenContext.RightFragmentQP.IsEquivalentTo(q, q2);
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x000D1FF7 File Offset: 0x000D01F7
		private FragmentQuery CreateRightFragmentQuery(LeftCellWrapper wrapper)
		{
			return FragmentQuery.Create(wrapper.OnlyInputCell.CellLabel.ToString(), wrapper.CreateRoleBoolean(), wrapper.OnlyInputCell.GetRightQuery(this.m_viewgenContext.ViewTarget));
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000D202C File Offset: 0x000D022C
		private static IEnumerable<Cell> ToIEnum(Cell one, Cell two)
		{
			return new List<Cell>
			{
				one,
				two
			};
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x000D204E File Offset: 0x000D024E
		private bool IsQueryView()
		{
			return this.m_viewgenContext.ViewTarget == ViewTarget.QueryView;
		}

		// Token: 0x04000FF5 RID: 4085
		private const int NUM_PARTITION_ERR_TO_FIND = 5;

		// Token: 0x04000FF6 RID: 4086
		private readonly ViewgenContext m_viewgenContext;

		// Token: 0x04000FF7 RID: 4087
		private readonly MemberDomainMap m_domainMap;

		// Token: 0x04000FF8 RID: 4088
		private readonly ErrorLog m_errorLog;

		// Token: 0x04000FF9 RID: 4089
		private readonly int m_originalErrorCount;

		// Token: 0x02000496 RID: 1174
		private enum ComparisonOP
		{
			// Token: 0x04001001 RID: 4097
			IsContainedIn,
			// Token: 0x04001002 RID: 4098
			IsDisjointFrom
		}
	}
}
