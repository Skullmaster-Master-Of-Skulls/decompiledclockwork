using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Mapping.ViewGeneration.Utils;
using System.Data.Mapping.ViewGeneration.Validation;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002A5 RID: 677
	internal class CellQuery : InternalBase
	{
		// Token: 0x0600283A RID: 10298 RVA: 0x0009BCE1 File Offset: 0x00099EE1
		internal CellQuery(List<ProjectedSlot> slots, BoolExpression whereClause, MemberPath rootMember, CellQuery.SelectDistinct eliminateDuplicates) : this(slots.ToArray(), whereClause, new List<BoolExpression>(), eliminateDuplicates, rootMember)
		{
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x0009BCF8 File Offset: 0x00099EF8
		internal CellQuery(ProjectedSlot[] projectedSlots, BoolExpression whereClause, List<BoolExpression> boolExprs, CellQuery.SelectDistinct elimDupl, MemberPath rootMember)
		{
			this.m_boolExprs = boolExprs;
			this.m_projectedSlots = projectedSlots;
			this.m_whereClause = whereClause;
			this.m_originalWhereClause = whereClause;
			this.m_selectDistinct = elimDupl;
			this.m_extentMemberPath = rootMember;
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x0009BD2C File Offset: 0x00099F2C
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

		// Token: 0x0600283D RID: 10301 RVA: 0x0009BD93 File Offset: 0x00099F93
		private CellQuery(CellQuery existing, ProjectedSlot[] newSlots) : this(newSlots, existing.m_whereClause, existing.m_boolExprs, existing.m_selectDistinct, existing.m_extentMemberPath)
		{
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600283E RID: 10302 RVA: 0x0009BDB4 File Offset: 0x00099FB4
		internal CellQuery.SelectDistinct SelectDistinctFlag
		{
			get
			{
				return this.m_selectDistinct;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x0600283F RID: 10303 RVA: 0x0009BDBC File Offset: 0x00099FBC
		internal EntitySetBase Extent
		{
			get
			{
				return this.m_extentMemberPath.Extent;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06002840 RID: 10304 RVA: 0x0009BDD6 File Offset: 0x00099FD6
		internal int NumProjectedSlots
		{
			get
			{
				return this.m_projectedSlots.Length;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06002841 RID: 10305 RVA: 0x0009BDE0 File Offset: 0x00099FE0
		internal ProjectedSlot[] ProjectedSlots
		{
			get
			{
				return this.m_projectedSlots;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x0009BDE8 File Offset: 0x00099FE8
		internal List<BoolExpression> BoolVars
		{
			get
			{
				return this.m_boolExprs;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06002843 RID: 10307 RVA: 0x0009BDF0 File Offset: 0x00099FF0
		internal int NumBoolVars
		{
			get
			{
				return this.m_boolExprs.Count;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06002844 RID: 10308 RVA: 0x0009BDFD File Offset: 0x00099FFD
		internal BoolExpression WhereClause
		{
			get
			{
				return this.m_whereClause;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06002845 RID: 10309 RVA: 0x0009BE05 File Offset: 0x0009A005
		internal MemberPath SourceExtentMemberPath
		{
			get
			{
				return this.m_extentMemberPath;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06002846 RID: 10310 RVA: 0x0009BE0D File Offset: 0x0009A00D
		internal BasicCellRelation BasicCellRelation
		{
			get
			{
				return this.m_basicCellRelation;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06002847 RID: 10311 RVA: 0x0009BE15 File Offset: 0x0009A015
		internal IEnumerable<MemberRestriction> Conditions
		{
			get
			{
				return this.GetConjunctsFromOriginalWhereClause();
			}
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x0009BE1D File Offset: 0x0009A01D
		internal ProjectedSlot ProjectedSlotAt(int slotNum)
		{
			return this.m_projectedSlots[slotNum];
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x0009BE28 File Offset: 0x0009A028
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
			return new ErrorLog.Record(true, ViewGenErrorCode.DuplicateCPropertiesMapped, stringBuilder.ToString(), sourceCell, string.Empty);
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x0009BF94 File Offset: 0x0009A194
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

		// Token: 0x0600284B RID: 10315 RVA: 0x0009BFF8 File Offset: 0x0009A1F8
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
							IEnumerable<AssociationSet> source = associationsForEntitySet;
							Func<AssociationSet, bool> predicate;
							Func<AssociationSet, bool> <>9__0;
							if ((predicate = <>9__0) == null)
							{
								Func<AssociationSetEnd, bool> <>9__1;
								predicate = (<>9__0 = delegate(AssociationSet association)
								{
									IEnumerable<AssociationSetEnd> associationSetEnds = association.AssociationSetEnds;
									Func<AssociationSetEnd, bool> predicate3;
									if ((predicate3 = <>9__1) == null)
									{
										predicate3 = (<>9__1 = ((AssociationSetEnd end) => end.CorrespondingAssociationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One && MetadataHelper.GetOppositeEnd(end).EntitySet.EdmEquals(rightExtent)));
									}
									return associationSetEnds.Any(predicate3);
								});
							}
							using (IEnumerator<AssociationSet> enumerator2 = source.Where(predicate).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									AssociationSet association = enumerator2.Current;
									Func<Cell, bool> predicate2;
									Func<Cell, bool> <>9__2;
									if ((predicate2 = <>9__2) == null)
									{
										predicate2 = (<>9__2 = ((Cell c) => c.GetRightQuery(target).Extent.EdmEquals(association)));
									}
									foreach (Cell cell in associationSets.Where(predicate2))
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
			return new ErrorLog.Record(true, ViewGenErrorCode.NotNullNoProjectedSlot, stringBuilder.ToString(), sourceCell, string.Empty);
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x0009C26C File Offset: 0x0009A46C
		internal void FixMissingSlotAsDefaultConstant(int slotNumber, ConstantProjectedSlot slot)
		{
			this.m_projectedSlots[slotNumber] = slot;
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x0009C278 File Offset: 0x0009A478
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

		// Token: 0x0600284E RID: 10318 RVA: 0x0009C2F8 File Offset: 0x0009A4F8
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

		// Token: 0x0600284F RID: 10319 RVA: 0x0009C344 File Offset: 0x0009A544
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
					return new ErrorLog.Record(true, errorCode, message, ownerCell, string.Empty);
				}
			}
			return null;
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x0009C4F4 File Offset: 0x0009A6F4
		internal IEnumerable<MemberPath> GetProjectedMembers()
		{
			foreach (MemberProjectedSlot memberProjectedSlot in this.GetMemberProjectedSlots())
			{
				yield return memberProjectedSlot.MemberPath;
			}
			IEnumerator<MemberProjectedSlot> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x0009C504 File Offset: 0x0009A704
		private IEnumerable<MemberProjectedSlot> GetMemberProjectedSlots()
		{
			foreach (ProjectedSlot projectedSlot in this.m_projectedSlots)
			{
				MemberProjectedSlot memberProjectedSlot = projectedSlot as MemberProjectedSlot;
				if (memberProjectedSlot != null)
				{
					yield return memberProjectedSlot;
				}
			}
			ProjectedSlot[] array = null;
			yield break;
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x0009C514 File Offset: 0x0009A714
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

		// Token: 0x06002853 RID: 10323 RVA: 0x0009C58C File Offset: 0x0009A78C
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

		// Token: 0x06002854 RID: 10324 RVA: 0x0009C5C4 File Offset: 0x0009A7C4
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

		// Token: 0x06002855 RID: 10325 RVA: 0x0009C618 File Offset: 0x0009A818
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

		// Token: 0x06002856 RID: 10326 RVA: 0x0009C688 File Offset: 0x0009A888
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

		// Token: 0x06002857 RID: 10327 RVA: 0x0009C6DC File Offset: 0x0009A8DC
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

		// Token: 0x06002858 RID: 10328 RVA: 0x0009C794 File Offset: 0x0009A994
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

		// Token: 0x06002859 RID: 10329 RVA: 0x0009C8BC File Offset: 0x0009AABC
		internal BoolExpression GetBoolVar(int varNum)
		{
			return this.m_boolExprs[varNum];
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x0009C8CC File Offset: 0x0009AACC
		internal void InitializeBoolExpressions(int numBoolVars, int cellNum)
		{
			this.m_boolExprs = new List<BoolExpression>(numBoolVars);
			for (int i = 0; i < numBoolVars; i++)
			{
				this.m_boolExprs.Add(null);
			}
			this.m_boolExprs[cellNum] = BoolExpression.True;
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x0009C90E File Offset: 0x0009AB0E
		internal IEnumerable<MemberRestriction> GetConjunctsFromWhereClause()
		{
			return this.GetConjunctsFromWhereClause(this.m_whereClause);
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x0009C91C File Offset: 0x0009AB1C
		internal IEnumerable<MemberRestriction> GetConjunctsFromOriginalWhereClause()
		{
			return this.GetConjunctsFromWhereClause(this.m_originalWhereClause);
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x0009C92A File Offset: 0x0009AB2A
		private IEnumerable<MemberRestriction> GetConjunctsFromWhereClause(BoolExpression whereClause)
		{
			foreach (BoolExpression boolExpression in whereClause.Atoms)
			{
				if (!boolExpression.IsTrue)
				{
					MemberRestriction memberRestriction = boolExpression.AsLiteral as MemberRestriction;
					yield return memberRestriction;
				}
			}
			IEnumerator<BoolExpression> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x0009C93C File Offset: 0x0009AB3C
		internal void WhereClauseToUserString(StringBuilder builder, MetadataWorkspace workspace)
		{
			bool flag = true;
			foreach (MemberRestriction memberRestriction in this.GetConjunctsFromWhereClause())
			{
				if (!flag)
				{
					builder.Append(Strings.ViewGen_AND);
				}
				memberRestriction.ToUserString(false, builder, workspace);
			}
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x0009C99C File Offset: 0x0009AB9C
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

		// Token: 0x06002860 RID: 10336 RVA: 0x0009C9E4 File Offset: 0x0009ABE4
		internal void CreateBasicCellRelation(ViewCellRelation viewCellRelation)
		{
			List<MemberProjectedSlot> allQuerySlots = this.GetAllQuerySlots();
			this.m_basicCellRelation = new BasicCellRelation(this, viewCellRelation, allQuerySlots);
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x0009CA08 File Offset: 0x0009AC08
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

		// Token: 0x06002862 RID: 10338 RVA: 0x0009CAB4 File Offset: 0x0009ACB4
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

		// Token: 0x06002863 RID: 10339 RVA: 0x0009B045 File Offset: 0x00099245
		public override string ToString()
		{
			return this.ToFullString();
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x0009CB70 File Offset: 0x0009AD70
		internal string ToESqlString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\n\tSELECT ");
			if (this.m_selectDistinct == CellQuery.SelectDistinct.Yes)
			{
				stringBuilder.Append("DISTINCT ");
			}
			foreach (ProjectedSlot projectedSlot in this.m_projectedSlots)
			{
				MemberProjectedSlot memberProjectedSlot = projectedSlot as MemberProjectedSlot;
				StructuralType declaringType = memberProjectedSlot.MemberPath.LeafEdmMember.DeclaringType;
				StringBuilder stringBuilder2 = new StringBuilder();
				memberProjectedSlot.MemberPath.AsEsql(stringBuilder2, "e");
				stringBuilder.AppendFormat("{0}, ", stringBuilder2.ToString());
			}
			stringBuilder.Remove(stringBuilder.Length - 2, 2);
			stringBuilder.Append("\n\tFROM ");
			EntitySetBase extent = this.m_extentMemberPath.Extent;
			CqlWriter.AppendEscapedQualifiedName(stringBuilder, extent.EntityContainer.Name, extent.Name);
			stringBuilder.Append(" AS e");
			if (!this.m_whereClause.IsTrue)
			{
				stringBuilder.Append("\n\tWHERE ");
				StringBuilder stringBuilder3 = new StringBuilder();
				this.m_whereClause.AsEsql(stringBuilder3, "e");
				stringBuilder.Append(stringBuilder3.ToString());
			}
			stringBuilder.Append("\n    ");
			return stringBuilder.ToString();
		}

		// Token: 0x04001248 RID: 4680
		private List<BoolExpression> m_boolExprs;

		// Token: 0x04001249 RID: 4681
		private ProjectedSlot[] m_projectedSlots;

		// Token: 0x0400124A RID: 4682
		private BoolExpression m_whereClause;

		// Token: 0x0400124B RID: 4683
		private BoolExpression m_originalWhereClause;

		// Token: 0x0400124C RID: 4684
		private CellQuery.SelectDistinct m_selectDistinct;

		// Token: 0x0400124D RID: 4685
		private MemberPath m_extentMemberPath;

		// Token: 0x0400124E RID: 4686
		private BasicCellRelation m_basicCellRelation;

		// Token: 0x020005DE RID: 1502
		internal enum SelectDistinct
		{
			// Token: 0x04001D83 RID: 7555
			Yes,
			// Token: 0x04001D84 RID: 7556
			No
		}
	}
}
