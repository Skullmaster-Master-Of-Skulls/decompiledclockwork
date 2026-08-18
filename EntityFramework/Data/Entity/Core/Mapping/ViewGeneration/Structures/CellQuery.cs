using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000468 RID: 1128
	internal class CellQuery : InternalBase
	{
		// Token: 0x06002963 RID: 10595 RVA: 0x000C83A4 File Offset: 0x000C65A4
		internal CellQuery(List<ProjectedSlot> slots, BoolExpression whereClause, MemberPath rootMember, CellQuery.SelectDistinct eliminateDuplicates) : this(slots.ToArray(), whereClause, new List<BoolExpression>(), eliminateDuplicates, rootMember)
		{
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000C83BB File Offset: 0x000C65BB
		internal CellQuery(ProjectedSlot[] projectedSlots, BoolExpression whereClause, List<BoolExpression> boolExprs, CellQuery.SelectDistinct elimDupl, MemberPath rootMember)
		{
			this.m_boolExprs = boolExprs;
			this.m_projectedSlots = projectedSlots;
			this.m_whereClause = whereClause;
			this.m_originalWhereClause = whereClause;
			this.m_selectDistinct = elimDupl;
			this.m_extentMemberPath = rootMember;
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x000C83F0 File Offset: 0x000C65F0
		internal CellQuery(CellQuery source)
		{
			this.m_basicCellRelation = source.m_basicCellRelation;
			this.m_boolExprs = source.m_boolExprs;
			this.m_selectDistinct = source.m_selectDistinct;
			this.m_extentMemberPath = source.m_extentMemberPath;
			this.m_originalWhereClause = source.m_originalWhereClause;
			this.m_projectedSlots = source.m_projectedSlots;
			this.m_whereClause = source.m_whereClause;
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x000C8457 File Offset: 0x000C6657
		private CellQuery(CellQuery existing, ProjectedSlot[] newSlots) : this(newSlots, existing.m_whereClause, existing.m_boolExprs, existing.m_selectDistinct, existing.m_extentMemberPath)
		{
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06002967 RID: 10599 RVA: 0x000C8478 File Offset: 0x000C6678
		internal CellQuery.SelectDistinct SelectDistinctFlag
		{
			get
			{
				return this.m_selectDistinct;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x000C8480 File Offset: 0x000C6680
		internal EntitySetBase Extent
		{
			get
			{
				return this.m_extentMemberPath.Extent;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06002969 RID: 10601 RVA: 0x000C849A File Offset: 0x000C669A
		internal int NumProjectedSlots
		{
			get
			{
				return this.m_projectedSlots.Length;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600296A RID: 10602 RVA: 0x000C84A4 File Offset: 0x000C66A4
		internal ProjectedSlot[] ProjectedSlots
		{
			get
			{
				return this.m_projectedSlots;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600296B RID: 10603 RVA: 0x000C84AC File Offset: 0x000C66AC
		internal List<BoolExpression> BoolVars
		{
			get
			{
				return this.m_boolExprs;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600296C RID: 10604 RVA: 0x000C84B4 File Offset: 0x000C66B4
		internal int NumBoolVars
		{
			get
			{
				return this.m_boolExprs.Count;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x0600296D RID: 10605 RVA: 0x000C84C1 File Offset: 0x000C66C1
		internal BoolExpression WhereClause
		{
			get
			{
				return this.m_whereClause;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x0600296E RID: 10606 RVA: 0x000C84C9 File Offset: 0x000C66C9
		internal MemberPath SourceExtentMemberPath
		{
			get
			{
				return this.m_extentMemberPath;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600296F RID: 10607 RVA: 0x000C84D1 File Offset: 0x000C66D1
		internal BasicCellRelation BasicCellRelation
		{
			get
			{
				return this.m_basicCellRelation;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06002970 RID: 10608 RVA: 0x000C84D9 File Offset: 0x000C66D9
		internal IEnumerable<MemberRestriction> Conditions
		{
			get
			{
				return this.GetConjunctsFromOriginalWhereClause();
			}
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000C84E1 File Offset: 0x000C66E1
		internal ProjectedSlot ProjectedSlotAt(int slotNum)
		{
			return this.m_projectedSlots[slotNum];
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000C84EC File Offset: 0x000C66EC
		internal ErrorLog.Record CheckForDuplicateFields(CellQuery cQuery, Cell sourceCell)
		{
			KeyToListMap<MemberProjectedSlot, int> keyToListMap = new KeyToListMap<MemberProjectedSlot, int>(ProjectedSlot.EqualityComparer);
			for (int i = 0; i < this.m_projectedSlots.Length; i++)
			{
				ProjectedSlot projectedSlot = this.m_projectedSlots[i];
				MemberProjectedSlot key = projectedSlot as MemberProjectedSlot;
				keyToListMap.Add(key, i);
			}
			StringBuilder stringBuilder = null;
			bool flag = false;
			foreach (MemberProjectedSlot memberProjectedSlot in keyToListMap.Keys)
			{
				ReadOnlyCollection<int> readOnlyCollection = keyToListMap.ListForKey(memberProjectedSlot);
				if (readOnlyCollection.Count > 1 && !cQuery.AreSlotsEquivalentViaRefConstraints(readOnlyCollection))
				{
					flag = true;
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(Strings.ViewGen_Duplicate_CProperties(this.Extent.Name));
						stringBuilder.AppendLine();
					}
					StringBuilder stringBuilder2 = new StringBuilder();
					for (int j = 0; j < readOnlyCollection.Count; j++)
					{
						int num = readOnlyCollection[j];
						if (j != 0)
						{
							stringBuilder2.Append(", ");
						}
						MemberProjectedSlot memberProjectedSlot2 = (MemberProjectedSlot)cQuery.m_projectedSlots[num];
						stringBuilder2.Append(memberProjectedSlot2.ToUserString());
					}
					stringBuilder.AppendLine(Strings.ViewGen_Duplicate_CProperties_IsMapped(memberProjectedSlot.ToUserString(), stringBuilder2.ToString()));
				}
			}
			if (!flag)
			{
				return null;
			}
			return new ErrorLog.Record(ViewGenErrorCode.DuplicateCPropertiesMapped, stringBuilder.ToString(), sourceCell, string.Empty);
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x000C8658 File Offset: 0x000C6858
		private bool AreSlotsEquivalentViaRefConstraints(ReadOnlyCollection<int> cSideSlotIndexes)
		{
			if (!(this.Extent is AssociationSet))
			{
				return false;
			}
			if (cSideSlotIndexes.Count > 2)
			{
				return false;
			}
			MemberProjectedSlot memberProjectedSlot = (MemberProjectedSlot)this.m_projectedSlots[cSideSlotIndexes[0]];
			MemberProjectedSlot memberProjectedSlot2 = (MemberProjectedSlot)this.m_projectedSlots[cSideSlotIndexes[1]];
			return memberProjectedSlot.MemberPath.IsEquivalentViaRefConstraint(memberProjectedSlot2.MemberPath);
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000C8730 File Offset: 0x000C6930
		internal ErrorLog.Record CheckForProjectedNotNullSlots(Cell sourceCell, IEnumerable<Cell> associationSets)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (MemberRestriction memberRestriction in this.Conditions)
			{
				if (memberRestriction.Domain.ContainsNotNull() && MemberProjectedSlot.GetSlotForMember(this.m_projectedSlots, memberRestriction.RestrictedMemberSlot.MemberPath) == null)
				{
					bool flag2 = true;
					if (this.Extent is EntitySet)
					{
						bool flag3 = sourceCell.CQuery == this;
						ViewTarget target = flag3 ? ViewTarget.QueryView : ViewTarget.UpdateView;
						CellQuery cellQuery = flag3 ? sourceCell.SQuery : sourceCell.CQuery;
						EntitySet rightExtent = cellQuery.Extent as EntitySet;
						if (rightExtent != null)
						{
							List<AssociationSet> associationsForEntitySet = MetadataHelper.GetAssociationsForEntitySet(cellQuery.Extent as EntitySet);
							using (IEnumerator<AssociationSet> enumerator2 = (from association in associationsForEntitySet
							where association.AssociationSetEnds.Any((AssociationSetEnd end) => end.CorrespondingAssociationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One && MetadataHelper.GetOppositeEnd(end).EntitySet.EdmEquals(rightExtent))
							select association).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									AssociationSet association = enumerator2.Current;
									foreach (Cell cell in from c in associationSets
									where c.GetRightQuery(target).Extent.EdmEquals(association)
									select c)
									{
										if (MemberProjectedSlot.GetSlotForMember(cell.GetLeftQuery(target).ProjectedSlots, memberRestriction.RestrictedMemberSlot.MemberPath) != null)
										{
											flag2 = false;
										}
									}
								}
							}
						}
					}
					if (flag2)
					{
						stringBuilder.AppendLine(Strings.ViewGen_NotNull_No_Projected_Slot(memberRestriction.RestrictedMemberSlot.MemberPath.PathToString(new bool?(false))));
						flag = true;
					}
				}
			}
			if (!flag)
			{
				return null;
			}
			return new ErrorLog.Record(ViewGenErrorCode.NotNullNoProjectedSlot, stringBuilder.ToString(), sourceCell, string.Empty);
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x000C8988 File Offset: 0x000C6B88
		internal void FixMissingSlotAsDefaultConstant(int slotNumber, ConstantProjectedSlot slot)
		{
			this.m_projectedSlots[slotNumber] = slot;
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x000C8994 File Offset: 0x000C6B94
		internal void CreateFieldAlignedCellQueries(CellQuery otherQuery, MemberProjectionIndex projectedSlotMap, out CellQuery newMainQuery, out CellQuery newOtherQuery)
		{
			int count = projectedSlotMap.Count;
			ProjectedSlot[] array = new ProjectedSlot[count];
			ProjectedSlot[] array2 = new ProjectedSlot[count];
			for (int i = 0; i < this.m_projectedSlots.Length; i++)
			{
				MemberProjectedSlot memberProjectedSlot = this.m_projectedSlots[i] as MemberProjectedSlot;
				int num = projectedSlotMap.IndexOf(memberProjectedSlot.MemberPath);
				array[num] = this.m_projectedSlots[i];
				array2[num] = otherQuery.m_projectedSlots[i];
			}
			newMainQuery = new CellQuery(this, array);
			newOtherQuery = new CellQuery(otherQuery, array2);
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000C8A14 File Offset: 0x000C6C14
		internal Set<MemberPath> GetNonNullSlots()
		{
			Set<MemberPath> set = new Set<MemberPath>(MemberPath.EqualityComparer);
			foreach (ProjectedSlot projectedSlot in this.m_projectedSlots)
			{
				if (projectedSlot != null)
				{
					MemberProjectedSlot memberProjectedSlot = projectedSlot as MemberProjectedSlot;
					set.Add(memberProjectedSlot.MemberPath);
				}
			}
			return set;
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000C8A64 File Offset: 0x000C6C64
		internal ErrorLog.Record VerifyKeysPresent(Cell ownerCell, Func<object, object, string> formatEntitySetMessage, Func<object, object, object, string> formatAssociationSetMessage, ViewGenErrorCode errorCode)
		{
			List<MemberPath> list = new List<MemberPath>(1);
			List<ExtentKey> list2 = new List<ExtentKey>(1);
			if (this.Extent is EntitySet)
			{
				MemberPath memberPath = new MemberPath(this.Extent);
				list.Add(memberPath);
				EntityType entityType = (EntityType)this.Extent.ElementType;
				List<ExtentKey> keysForEntityType = ExtentKey.GetKeysForEntityType(memberPath, entityType);
				list2.Add(keysForEntityType[0]);
			}
			else
			{
				AssociationSet associationSet = (AssociationSet)this.Extent;
				foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
				{
					AssociationEndMember correspondingAssociationEndMember = associationSetEnd.CorrespondingAssociationEndMember;
					MemberPath memberPath2 = new MemberPath(associationSet, correspondingAssociationEndMember);
					list.Add(memberPath2);
					List<ExtentKey> keysForEntityType2 = ExtentKey.GetKeysForEntityType(memberPath2, MetadataHelper.GetEntityTypeForEnd(correspondingAssociationEndMember));
					list2.Add(keysForEntityType2[0]);
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				MemberPath memberPath3 = list[i];
				if (MemberProjectedSlot.GetKeySlots(this.GetMemberProjectedSlots(), memberPath3) == null)
				{
					ExtentKey extentKey = list2[i];
					string message;
					if (this.Extent is EntitySet)
					{
						string arg = MemberPath.PropertiesToUserString(extentKey.KeyFields, true);
						message = formatEntitySetMessage(arg, this.Extent.Name);
					}
					else
					{
						string name = memberPath3.RootEdmMember.Name;
						string arg2 = MemberPath.PropertiesToUserString(extentKey.KeyFields, false);
						message = formatAssociationSetMessage(arg2, name, this.Extent.Name);
					}
					return new ErrorLog.Record(errorCode, message, ownerCell, string.Empty);
				}
			}
			return null;
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000C8DA4 File Offset: 0x000C6FA4
		internal IEnumerable<MemberPath> GetProjectedMembers()
		{
			foreach (MemberProjectedSlot slot in this.GetMemberProjectedSlots())
			{
				yield return slot.MemberPath;
			}
			yield break;
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x000C8F58 File Offset: 0x000C7158
		private IEnumerable<MemberProjectedSlot> GetMemberProjectedSlots()
		{
			foreach (ProjectedSlot slot in this.m_projectedSlots)
			{
				MemberProjectedSlot memberSlot = slot as MemberProjectedSlot;
				if (memberSlot != null)
				{
					yield return memberSlot;
				}
			}
			yield break;
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000C8F78 File Offset: 0x000C7178
		internal List<MemberProjectedSlot> GetAllQuerySlots()
		{
			HashSet<MemberProjectedSlot> hashSet = new HashSet<MemberProjectedSlot>(this.GetMemberProjectedSlots());
			hashSet.Add(new MemberProjectedSlot(this.SourceExtentMemberPath));
			foreach (MemberRestriction memberRestriction in this.Conditions)
			{
				hashSet.Add(memberRestriction.RestrictedMemberSlot);
			}
			return new List<MemberProjectedSlot>(hashSet);
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x000C8FF0 File Offset: 0x000C71F0
		internal int GetProjectedPosition(MemberProjectedSlot slot)
		{
			for (int i = 0; i < this.m_projectedSlots.Length; i++)
			{
				if (ProjectedSlot.EqualityComparer.Equals(slot, this.m_projectedSlots[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x000C9028 File Offset: 0x000C7228
		internal List<int> GetProjectedPositions(MemberPath member)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.m_projectedSlots.Length; i++)
			{
				MemberProjectedSlot memberProjectedSlot = this.m_projectedSlots[i] as MemberProjectedSlot;
				if (memberProjectedSlot != null && MemberPath.EqualityComparer.Equals(member, memberProjectedSlot.MemberPath))
				{
					list.Add(i);
				}
			}
			return list;
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x000C907C File Offset: 0x000C727C
		internal List<int> GetProjectedPositions(IEnumerable<MemberPath> paths)
		{
			List<int> list = new List<int>();
			foreach (MemberPath member in paths)
			{
				List<int> projectedPositions = this.GetProjectedPositions(member);
				if (projectedPositions.Count == 0)
				{
					return null;
				}
				list.Add(projectedPositions[0]);
			}
			return list;
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x000C90EC File Offset: 0x000C72EC
		internal List<int> GetAssociationEndSlots(AssociationEndMember endMember)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.m_projectedSlots.Length; i++)
			{
				MemberProjectedSlot memberProjectedSlot = this.m_projectedSlots[i] as MemberProjectedSlot;
				if (memberProjectedSlot != null && memberProjectedSlot.MemberPath.RootEdmMember.Equals(endMember))
				{
					list.Add(i);
				}
			}
			return list;
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x000C9140 File Offset: 0x000C7340
		internal List<int> GetProjectedPositions(IEnumerable<MemberPath> paths, List<int> slotsToSearchFrom)
		{
			List<int> list = new List<int>();
			foreach (MemberPath member in paths)
			{
				List<int> projectedPositions = this.GetProjectedPositions(member);
				if (projectedPositions.Count == 0)
				{
					return null;
				}
				int num = -1;
				if (projectedPositions.Count > 1)
				{
					for (int i = 0; i < projectedPositions.Count; i++)
					{
						if (slotsToSearchFrom.Contains(projectedPositions[i]))
						{
							num = projectedPositions[i];
						}
					}
					if (num == -1)
					{
						return null;
					}
				}
				else
				{
					num = projectedPositions[0];
				}
				list.Add(num);
			}
			return list;
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x000C91F8 File Offset: 0x000C73F8
		internal void UpdateWhereClause(MemberDomainMap domainMap)
		{
			List<BoolExpression> list = new List<BoolExpression>();
			foreach (BoolExpression boolExpression in this.WhereClause.Atoms)
			{
				BoolLiteral asLiteral = boolExpression.AsLiteral;
				MemberRestriction memberRestriction = asLiteral as MemberRestriction;
				IEnumerable<Constant> domain = domainMap.GetDomain(memberRestriction.RestrictedMemberSlot.MemberPath);
				MemberRestriction memberRestriction2 = memberRestriction.CreateCompleteMemberRestriction(domain);
				ScalarRestriction scalarRestriction = memberRestriction as ScalarRestriction;
				bool flag = scalarRestriction != null && !scalarRestriction.Domain.Contains(Constant.Null) && !scalarRestriction.Domain.Contains(Constant.NotNull) && !scalarRestriction.Domain.Contains(Constant.Undefined);
				if (flag)
				{
					domainMap.AddSentinel(memberRestriction2.RestrictedMemberSlot.MemberPath);
				}
				list.Add(BoolExpression.CreateLiteral(memberRestriction2, domainMap));
				if (flag)
				{
					domainMap.RemoveSentinel(memberRestriction2.RestrictedMemberSlot.MemberPath);
				}
			}
			if (list.Count > 0)
			{
				this.m_whereClause = BoolExpression.CreateAnd(list.ToArray());
			}
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000C9320 File Offset: 0x000C7520
		internal BoolExpression GetBoolVar(int varNum)
		{
			return this.m_boolExprs[varNum];
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000C9330 File Offset: 0x000C7530
		internal void InitializeBoolExpressions(int numBoolVars, int cellNum)
		{
			this.m_boolExprs = new List<BoolExpression>(numBoolVars);
			for (int i = 0; i < numBoolVars; i++)
			{
				this.m_boolExprs.Add(null);
			}
			this.m_boolExprs[cellNum] = BoolExpression.True;
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x000C9372 File Offset: 0x000C7572
		internal IEnumerable<MemberRestriction> GetConjunctsFromWhereClause()
		{
			return CellQuery.GetConjunctsFromWhereClause(this.m_whereClause);
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x000C937F File Offset: 0x000C757F
		internal IEnumerable<MemberRestriction> GetConjunctsFromOriginalWhereClause()
		{
			return CellQuery.GetConjunctsFromWhereClause(this.m_originalWhereClause);
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x000C953C File Offset: 0x000C773C
		private static IEnumerable<MemberRestriction> GetConjunctsFromWhereClause(BoolExpression whereClause)
		{
			foreach (BoolExpression boolExpr in whereClause.Atoms)
			{
				if (!boolExpr.IsTrue)
				{
					MemberRestriction result = boolExpr.AsLiteral as MemberRestriction;
					yield return result;
				}
			}
			yield break;
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x000C955C File Offset: 0x000C775C
		internal void GetIdentifiers(CqlIdentifiers identifiers)
		{
			foreach (ProjectedSlot projectedSlot in this.m_projectedSlots)
			{
				MemberProjectedSlot memberProjectedSlot = projectedSlot as MemberProjectedSlot;
				if (memberProjectedSlot != null)
				{
					memberProjectedSlot.MemberPath.GetIdentifiers(identifiers);
				}
			}
			this.m_extentMemberPath.GetIdentifiers(identifiers);
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x000C95A4 File Offset: 0x000C77A4
		internal void CreateBasicCellRelation(ViewCellRelation viewCellRelation)
		{
			List<MemberProjectedSlot> allQuerySlots = this.GetAllQuerySlots();
			this.m_basicCellRelation = new BasicCellRelation(this, viewCellRelation, allQuerySlots);
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x000C95C8 File Offset: 0x000C77C8
		internal override void ToCompactString(StringBuilder stringBuilder)
		{
			List<BoolExpression> boolExprs = this.m_boolExprs;
			int num = 0;
			bool flag = true;
			foreach (BoolExpression boolExpression in boolExprs)
			{
				if (boolExpression != null)
				{
					if (!flag)
					{
						stringBuilder.Append(",");
					}
					else
					{
						stringBuilder.Append("[");
					}
					StringUtil.FormatStringBuilder(stringBuilder, "C{0}", new object[]
					{
						num
					});
					flag = false;
				}
				num++;
			}
			if (flag)
			{
				this.ToFullString(stringBuilder);
				return;
			}
			stringBuilder.Append("]");
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x000C9678 File Offset: 0x000C7878
		internal override void ToFullString(StringBuilder builder)
		{
			builder.Append("SELECT ");
			if (this.m_selectDistinct == CellQuery.SelectDistinct.Yes)
			{
				builder.Append("DISTINCT ");
			}
			StringUtil.ToSeparatedString(builder, this.m_projectedSlots, ", ", "_");
			if (this.m_boolExprs.Count > 0)
			{
				builder.Append(", Bool[");
				StringUtil.ToSeparatedString(builder, this.m_boolExprs, ", ", "_");
				builder.Append("]");
			}
			builder.Append(" FROM ");
			this.m_extentMemberPath.ToFullString(builder);
			if (!this.m_whereClause.IsTrue)
			{
				builder.Append(" WHERE ");
				this.m_whereClause.ToFullString(builder);
			}
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x000C9734 File Offset: 0x000C7934
		public override string ToString()
		{
			return this.ToFullString();
		}

		// Token: 0x04000F68 RID: 3944
		private List<BoolExpression> m_boolExprs;

		// Token: 0x04000F69 RID: 3945
		private readonly ProjectedSlot[] m_projectedSlots;

		// Token: 0x04000F6A RID: 3946
		private BoolExpression m_whereClause;

		// Token: 0x04000F6B RID: 3947
		private readonly BoolExpression m_originalWhereClause;

		// Token: 0x04000F6C RID: 3948
		private readonly CellQuery.SelectDistinct m_selectDistinct;

		// Token: 0x04000F6D RID: 3949
		private readonly MemberPath m_extentMemberPath;

		// Token: 0x04000F6E RID: 3950
		private BasicCellRelation m_basicCellRelation;

		// Token: 0x02000469 RID: 1129
		internal enum SelectDistinct
		{
			// Token: 0x04000F70 RID: 3952
			Yes,
			// Token: 0x04000F71 RID: 3953
			No
		}
	}
}
