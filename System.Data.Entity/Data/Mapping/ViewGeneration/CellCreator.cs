using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration
{
	// Token: 0x02000261 RID: 609
	internal class CellCreator : InternalBase
	{
		// Token: 0x060025A4 RID: 9636 RVA: 0x0008D3B5 File Offset: 0x0008B5B5
		internal CellCreator(StorageEntityContainerMapping containerMapping)
		{
			this.m_containerMapping = containerMapping;
			this.m_identifiers = new CqlIdentifiers();
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x0008D3CF File Offset: 0x0008B5CF
		internal CqlIdentifiers Identifiers
		{
			get
			{
				return this.m_identifiers;
			}
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x0008D3D8 File Offset: 0x0008B5D8
		internal List<Cell> GenerateCells(ConfigViewGenerator config)
		{
			List<Cell> list = new List<Cell>();
			this.ExtractCells(list);
			this.ExpandCells(list);
			this.m_identifiers.AddIdentifier(this.m_containerMapping.EdmEntityContainer.Name);
			this.m_identifiers.AddIdentifier(this.m_containerMapping.StorageEntityContainer.Name);
			foreach (Cell cell in list)
			{
				cell.GetIdentifiers(this.m_identifiers);
			}
			return list;
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x0008D478 File Offset: 0x0008B678
		private void ExpandCells(List<Cell> cells)
		{
			Set<MemberPath> set = new Set<MemberPath>();
			using (List<Cell>.Enumerator enumerator = cells.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Cell cell = enumerator.Current;
					IEnumerable<MemberPath> source = from member in cell.SQuery.GetProjectedMembers()
					where CellCreator.IsBooleanMember(member)
					select member;
					Func<MemberPath, bool> predicate;
					Func<MemberPath, bool> <>9__1;
					if ((predicate = <>9__1) == null)
					{
						predicate = (<>9__1 = ((MemberPath boolMember) => (from restriction in cell.SQuery.GetConjunctsFromWhereClause()
						where restriction.Domain.Values.Contains(Constant.NotNull)
						select restriction.RestrictedMemberSlot.MemberPath).Contains(boolMember)));
					}
					foreach (MemberPath element in source.Where(predicate))
					{
						set.Add(element);
					}
				}
			}
			Dictionary<MemberPath, Set<MemberPath>> dictionary = new Dictionary<MemberPath, Set<MemberPath>>();
			using (List<Cell>.Enumerator enumerator3 = cells.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					Cell cell = enumerator3.Current;
					Func<int, MemberPath> <>9__4;
					foreach (MemberPath memberPath in set)
					{
						IEnumerable<int> projectedPositions = cell.SQuery.GetProjectedPositions(memberPath);
						Func<int, MemberPath> selector;
						if ((selector = <>9__4) == null)
						{
							selector = (<>9__4 = ((int pos) => ((MemberProjectedSlot)cell.CQuery.ProjectedSlotAt(pos)).MemberPath));
						}
						IEnumerable<MemberPath> elements = projectedPositions.Select(selector);
						Set<MemberPath> set2 = null;
						if (!dictionary.TryGetValue(memberPath, out set2))
						{
							set2 = new Set<MemberPath>();
							dictionary[memberPath] = set2;
						}
						set2.AddRange(elements);
					}
				}
			}
			foreach (Cell cell2 in cells.ToArray())
			{
				foreach (MemberPath memberPath2 in set)
				{
					Set<MemberPath> second = dictionary[memberPath2];
					if (cell2.SQuery.GetProjectedMembers().Contains(memberPath2))
					{
						Cell item = null;
						if (this.TryCreateAdditionalCellWithCondition(cell2, memberPath2, true, ViewTarget.UpdateView, out item))
						{
							cells.Add(item);
						}
						if (this.TryCreateAdditionalCellWithCondition(cell2, memberPath2, false, ViewTarget.UpdateView, out item))
						{
							cells.Add(item);
						}
					}
					else
					{
						foreach (MemberPath memberToExpand in cell2.CQuery.GetProjectedMembers().Intersect(second))
						{
							Cell item2 = null;
							if (this.TryCreateAdditionalCellWithCondition(cell2, memberToExpand, true, ViewTarget.QueryView, out item2))
							{
								cells.Add(item2);
							}
							if (this.TryCreateAdditionalCellWithCondition(cell2, memberToExpand, false, ViewTarget.QueryView, out item2))
							{
								cells.Add(item2);
							}
						}
					}
				}
			}
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x0008D794 File Offset: 0x0008B994
		private bool TryCreateAdditionalCellWithCondition(Cell originalCell, MemberPath memberToExpand, bool conditionValue, ViewTarget viewTarget, out Cell result)
		{
			result = null;
			MemberPath sourceExtentMemberPath = originalCell.GetLeftQuery(viewTarget).SourceExtentMemberPath;
			MemberPath sourceExtentMemberPath2 = originalCell.GetRightQuery(viewTarget).SourceExtentMemberPath;
			int slotNum = originalCell.GetLeftQuery(viewTarget).GetProjectedMembers().TakeWhile((MemberPath path) => !path.Equals(memberToExpand)).Count<MemberPath>();
			MemberProjectedSlot memberProjectedSlot = (MemberProjectedSlot)originalCell.GetRightQuery(viewTarget).ProjectedSlotAt(slotNum);
			MemberPath rightSidePath = memberProjectedSlot.MemberPath;
			List<ProjectedSlot> list = new List<ProjectedSlot>();
			List<ProjectedSlot> list2 = new List<ProjectedSlot>();
			ScalarConstant negatedCondition = new ScalarConstant(!conditionValue);
			if ((from restriction in originalCell.GetLeftQuery(viewTarget).Conditions
			where restriction.RestrictedMemberSlot.MemberPath.Equals(memberToExpand)
			where restriction.Domain.Values.Contains(negatedCondition)
			select restriction).Any<MemberRestriction>() || (from restriction in originalCell.GetRightQuery(viewTarget).Conditions
			where restriction.RestrictedMemberSlot.MemberPath.Equals(rightSidePath)
			where restriction.Domain.Values.Contains(negatedCondition)
			select restriction).Any<MemberRestriction>())
			{
				return false;
			}
			for (int i = 0; i < originalCell.GetLeftQuery(viewTarget).NumProjectedSlots; i++)
			{
				list.Add(originalCell.GetLeftQuery(viewTarget).ProjectedSlotAt(i));
			}
			for (int j = 0; j < originalCell.GetRightQuery(viewTarget).NumProjectedSlots; j++)
			{
				list2.Add(originalCell.GetRightQuery(viewTarget).ProjectedSlotAt(j));
			}
			BoolExpression boolExpression = BoolExpression.CreateLiteral(new ScalarRestriction(memberToExpand, new ScalarConstant(conditionValue)), null);
			boolExpression = BoolExpression.CreateAnd(new BoolExpression[]
			{
				originalCell.GetLeftQuery(viewTarget).WhereClause,
				boolExpression
			});
			BoolExpression boolExpression2 = BoolExpression.CreateLiteral(new ScalarRestriction(rightSidePath, new ScalarConstant(conditionValue)), null);
			boolExpression2 = BoolExpression.CreateAnd(new BoolExpression[]
			{
				originalCell.GetRightQuery(viewTarget).WhereClause,
				boolExpression2
			});
			CellQuery cellQuery = new CellQuery(list2, boolExpression2, sourceExtentMemberPath2, originalCell.GetRightQuery(viewTarget).SelectDistinctFlag);
			CellQuery cellQuery2 = new CellQuery(list, boolExpression, sourceExtentMemberPath, originalCell.GetLeftQuery(viewTarget).SelectDistinctFlag);
			Cell cell;
			if (viewTarget == ViewTarget.UpdateView)
			{
				cell = Cell.CreateCS(cellQuery, cellQuery2, originalCell.CellLabel, this.m_currentCellNumber);
			}
			else
			{
				cell = Cell.CreateCS(cellQuery2, cellQuery, originalCell.CellLabel, this.m_currentCellNumber);
			}
			this.m_currentCellNumber++;
			result = cell;
			return true;
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x0008DA04 File Offset: 0x0008BC04
		private void ExtractCells(List<Cell> cells)
		{
			foreach (StorageSetMapping storageSetMapping in this.m_containerMapping.AllSetMaps)
			{
				foreach (StorageTypeMapping storageTypeMapping in storageSetMapping.TypeMappings)
				{
					StorageEntityTypeMapping storageEntityTypeMapping = storageTypeMapping as StorageEntityTypeMapping;
					Set<EdmType> set = new Set<EdmType>();
					if (storageEntityTypeMapping != null)
					{
						set.AddRange(storageEntityTypeMapping.Types);
						foreach (EdmType type in storageEntityTypeMapping.IsOfTypes)
						{
							IEnumerable<EdmType> typeAndSubtypesOf = MetadataHelper.GetTypeAndSubtypesOf(type, this.m_containerMapping.StorageMappingItemCollection.EdmItemCollection, false);
							set.AddRange(typeAndSubtypesOf);
						}
					}
					EntitySetBase set2 = storageSetMapping.Set;
					foreach (StorageMappingFragment fragmentMap in storageTypeMapping.MappingFragments)
					{
						this.ExtractCellsFromTableFragment(set2, fragmentMap, set, cells);
					}
				}
			}
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x0008DB90 File Offset: 0x0008BD90
		private void ExtractCellsFromTableFragment(EntitySetBase extent, StorageMappingFragment fragmentMap, Set<EdmType> allTypes, List<Cell> cells)
		{
			MemberPath memberPath = new MemberPath(extent);
			BoolExpression whereClause = BoolExpression.True;
			List<ProjectedSlot> list = new List<ProjectedSlot>();
			if (allTypes.Count > 0)
			{
				whereClause = BoolExpression.CreateLiteral(new TypeRestriction(memberPath, allTypes), null);
			}
			MemberPath memberPath2 = new MemberPath(fragmentMap.TableSet);
			BoolExpression @true = BoolExpression.True;
			List<ProjectedSlot> list2 = new List<ProjectedSlot>();
			this.ExtractProperties(fragmentMap.AllProperties, memberPath, list, ref whereClause, memberPath2, list2, ref @true);
			CellQuery cQuery = new CellQuery(list, whereClause, memberPath, CellQuery.SelectDistinct.No);
			CellQuery sQuery = new CellQuery(list2, @true, memberPath2, fragmentMap.IsSQueryDistinct ? CellQuery.SelectDistinct.Yes : CellQuery.SelectDistinct.No);
			CellLabel label = new CellLabel(fragmentMap);
			Cell item = Cell.CreateCS(cQuery, sQuery, label, this.m_currentCellNumber);
			this.m_currentCellNumber++;
			cells.Add(item);
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x0008DC50 File Offset: 0x0008BE50
		private void ExtractProperties(IEnumerable<StoragePropertyMapping> properties, MemberPath cNode, List<ProjectedSlot> cSlots, ref BoolExpression cQueryWhereClause, MemberPath sRootExtent, List<ProjectedSlot> sSlots, ref BoolExpression sQueryWhereClause)
		{
			foreach (StoragePropertyMapping storagePropertyMapping in properties)
			{
				StorageScalarPropertyMapping storageScalarPropertyMapping = storagePropertyMapping as StorageScalarPropertyMapping;
				StorageComplexPropertyMapping storageComplexPropertyMapping = storagePropertyMapping as StorageComplexPropertyMapping;
				StorageEndPropertyMapping storageEndPropertyMapping = storagePropertyMapping as StorageEndPropertyMapping;
				StorageConditionPropertyMapping storageConditionPropertyMapping = storagePropertyMapping as StorageConditionPropertyMapping;
				if (storageScalarPropertyMapping != null)
				{
					MemberPath node = new MemberPath(cNode, storageScalarPropertyMapping.EdmProperty);
					MemberPath node2 = new MemberPath(sRootExtent, storageScalarPropertyMapping.ColumnProperty);
					cSlots.Add(new MemberProjectedSlot(node));
					sSlots.Add(new MemberProjectedSlot(node2));
				}
				if (storageComplexPropertyMapping != null)
				{
					foreach (StorageComplexTypeMapping storageComplexTypeMapping in storageComplexPropertyMapping.TypeMappings)
					{
						MemberPath memberPath = new MemberPath(cNode, storageComplexPropertyMapping.EdmProperty);
						Set<EdmType> set = new Set<EdmType>();
						IEnumerable<EdmType> elements = Helpers.AsSuperTypeList<ComplexType, EdmType>(storageComplexTypeMapping.Types);
						set.AddRange(elements);
						foreach (EdmType type in storageComplexTypeMapping.IsOfTypes)
						{
							set.AddRange(MetadataHelper.GetTypeAndSubtypesOf(type, this.m_containerMapping.StorageMappingItemCollection.EdmItemCollection, false));
						}
						BoolExpression boolExpression = BoolExpression.CreateLiteral(new TypeRestriction(memberPath, set), null);
						cQueryWhereClause = BoolExpression.CreateAnd(new BoolExpression[]
						{
							cQueryWhereClause,
							boolExpression
						});
						this.ExtractProperties(storageComplexTypeMapping.AllProperties, memberPath, cSlots, ref cQueryWhereClause, sRootExtent, sSlots, ref sQueryWhereClause);
					}
				}
				if (storageEndPropertyMapping != null)
				{
					MemberPath cNode2 = new MemberPath(cNode, storageEndPropertyMapping.EndMember);
					this.ExtractProperties(storageEndPropertyMapping.Properties, cNode2, cSlots, ref cQueryWhereClause, sRootExtent, sSlots, ref sQueryWhereClause);
				}
				if (storageConditionPropertyMapping != null)
				{
					if (storageConditionPropertyMapping.ColumnProperty != null)
					{
						BoolExpression conditionExpression = CellCreator.GetConditionExpression(sRootExtent, storageConditionPropertyMapping);
						sQueryWhereClause = BoolExpression.CreateAnd(new BoolExpression[]
						{
							sQueryWhereClause,
							conditionExpression
						});
					}
					else
					{
						BoolExpression conditionExpression2 = CellCreator.GetConditionExpression(cNode, storageConditionPropertyMapping);
						cQueryWhereClause = BoolExpression.CreateAnd(new BoolExpression[]
						{
							cQueryWhereClause,
							conditionExpression2
						});
					}
				}
			}
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x0008DEA8 File Offset: 0x0008C0A8
		private static BoolExpression GetConditionExpression(MemberPath member, StorageConditionPropertyMapping conditionMap)
		{
			EdmMember edmMember = (conditionMap.ColumnProperty != null) ? conditionMap.ColumnProperty : conditionMap.EdmProperty;
			MemberPath member2 = new MemberPath(member, edmMember);
			MemberRestriction literal;
			if (conditionMap.IsNull != null)
			{
				Constant value = conditionMap.IsNull.Value ? Constant.Null : Constant.NotNull;
				if (MetadataHelper.IsNonRefSimpleMember(edmMember))
				{
					literal = new ScalarRestriction(member2, value);
				}
				else
				{
					literal = new TypeRestriction(member2, value);
				}
			}
			else
			{
				literal = new ScalarRestriction(member2, new ScalarConstant(conditionMap.Value));
			}
			return BoolExpression.CreateLiteral(literal, null);
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x0008DF3C File Offset: 0x0008C13C
		private static bool IsBooleanMember(MemberPath path)
		{
			PrimitiveType primitiveType = path.EdmType as PrimitiveType;
			return primitiveType != null && primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Boolean;
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x0008DF63 File Offset: 0x0008C163
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append("CellCreator");
		}

		// Token: 0x04001145 RID: 4421
		private StorageEntityContainerMapping m_containerMapping;

		// Token: 0x04001146 RID: 4422
		private int m_currentCellNumber;

		// Token: 0x04001147 RID: 4423
		private CqlIdentifiers m_identifiers;
	}
}
