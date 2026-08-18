using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x0200041E RID: 1054
	internal class CellCreator : InternalBase
	{
		// Token: 0x060026D3 RID: 9939 RVA: 0x000BBF63 File Offset: 0x000BA163
		internal CellCreator(EntityContainerMapping containerMapping)
		{
			this.m_containerMapping = containerMapping;
			this.m_identifiers = new CqlIdentifiers();
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060026D4 RID: 9940 RVA: 0x000BBF7D File Offset: 0x000BA17D
		internal CqlIdentifiers Identifiers
		{
			get
			{
				return this.m_identifiers;
			}
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x000BBF88 File Offset: 0x000BA188
		internal List<Cell> GenerateCells()
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

		// Token: 0x060026D6 RID: 9942 RVA: 0x000BC0E8 File Offset: 0x000BA2E8
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private void ExpandCells(List<Cell> cells)
		{
			Set<MemberPath> set = new Set<MemberPath>();
			using (List<Cell>.Enumerator enumerator = cells.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Cell cell = enumerator.Current;
					foreach (MemberPath element in from member in cell.SQuery.GetProjectedMembers()
					where CellCreator.IsBooleanMember(member)
					select member into boolMember
					where (from restriction in cell.SQuery.GetConjunctsFromWhereClause()
					where restriction.Domain.Values.Contains(Constant.NotNull)
					select restriction.RestrictedMemberSlot.MemberPath).Contains(boolMember)
					select boolMember)
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
					foreach (MemberPath memberPath in set)
					{
						IEnumerable<MemberPath> elements = from pos in cell.SQuery.GetProjectedPositions(memberPath)
						select ((MemberProjectedSlot)cell.CQuery.ProjectedSlotAt(pos)).MemberPath;
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

		// Token: 0x060026D7 RID: 9943 RVA: 0x000BC464 File Offset: 0x000BA664
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

		// Token: 0x060026D8 RID: 9944 RVA: 0x000BC6E8 File Offset: 0x000BA8E8
		private void ExtractCells(List<Cell> cells)
		{
			foreach (EntitySetBaseMapping entitySetBaseMapping in this.m_containerMapping.AllSetMaps)
			{
				foreach (TypeMapping typeMapping in entitySetBaseMapping.TypeMappings)
				{
					EntityTypeMapping entityTypeMapping = typeMapping as EntityTypeMapping;
					Set<EdmType> set = new Set<EdmType>();
					if (entityTypeMapping != null)
					{
						set.AddRange(entityTypeMapping.Types);
						foreach (EntityTypeBase type in entityTypeMapping.IsOfTypes)
						{
							IEnumerable<EdmType> typeAndSubtypesOf = MetadataHelper.GetTypeAndSubtypesOf(type, this.m_containerMapping.StorageMappingItemCollection.EdmItemCollection, false);
							set.AddRange(typeAndSubtypesOf);
						}
					}
					EntitySetBase set2 = entitySetBaseMapping.Set;
					foreach (MappingFragment fragmentMap in typeMapping.MappingFragments)
					{
						this.ExtractCellsFromTableFragment(set2, fragmentMap, set, cells);
					}
				}
			}
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x000BC848 File Offset: 0x000BAA48
		private void ExtractCellsFromTableFragment(EntitySetBase extent, MappingFragment fragmentMap, Set<EdmType> allTypes, List<Cell> cells)
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

		// Token: 0x060026DA RID: 9946 RVA: 0x000BC908 File Offset: 0x000BAB08
		private void ExtractProperties(IEnumerable<PropertyMapping> properties, MemberPath cNode, List<ProjectedSlot> cSlots, ref BoolExpression cQueryWhereClause, MemberPath sRootExtent, List<ProjectedSlot> sSlots, ref BoolExpression sQueryWhereClause)
		{
			foreach (PropertyMapping propertyMapping in properties)
			{
				ScalarPropertyMapping scalarPropertyMapping = propertyMapping as ScalarPropertyMapping;
				ComplexPropertyMapping complexPropertyMapping = propertyMapping as ComplexPropertyMapping;
				EndPropertyMapping endPropertyMapping = propertyMapping as EndPropertyMapping;
				ConditionPropertyMapping conditionPropertyMapping = propertyMapping as ConditionPropertyMapping;
				if (scalarPropertyMapping != null)
				{
					MemberPath node = new MemberPath(cNode, scalarPropertyMapping.Property);
					MemberPath node2 = new MemberPath(sRootExtent, scalarPropertyMapping.Column);
					cSlots.Add(new MemberProjectedSlot(node));
					sSlots.Add(new MemberProjectedSlot(node2));
				}
				if (complexPropertyMapping != null)
				{
					foreach (ComplexTypeMapping complexTypeMapping in complexPropertyMapping.TypeMappings)
					{
						MemberPath memberPath = new MemberPath(cNode, complexPropertyMapping.Property);
						Set<EdmType> set = new Set<EdmType>();
						IEnumerable<EdmType> elements = Helpers.AsSuperTypeList<ComplexType, EdmType>(complexTypeMapping.Types);
						set.AddRange(elements);
						foreach (EdmType type in complexTypeMapping.IsOfTypes)
						{
							set.AddRange(MetadataHelper.GetTypeAndSubtypesOf(type, this.m_containerMapping.StorageMappingItemCollection.EdmItemCollection, false));
						}
						BoolExpression boolExpression = BoolExpression.CreateLiteral(new TypeRestriction(memberPath, set), null);
						cQueryWhereClause = BoolExpression.CreateAnd(new BoolExpression[]
						{
							cQueryWhereClause,
							boolExpression
						});
						this.ExtractProperties(complexTypeMapping.AllProperties, memberPath, cSlots, ref cQueryWhereClause, sRootExtent, sSlots, ref sQueryWhereClause);
					}
				}
				if (endPropertyMapping != null)
				{
					MemberPath cNode2 = new MemberPath(cNode, endPropertyMapping.AssociationEnd);
					this.ExtractProperties(endPropertyMapping.PropertyMappings, cNode2, cSlots, ref cQueryWhereClause, sRootExtent, sSlots, ref sQueryWhereClause);
				}
				if (conditionPropertyMapping != null)
				{
					if (conditionPropertyMapping.Column != null)
					{
						BoolExpression conditionExpression = CellCreator.GetConditionExpression(sRootExtent, conditionPropertyMapping);
						sQueryWhereClause = BoolExpression.CreateAnd(new BoolExpression[]
						{
							sQueryWhereClause,
							conditionExpression
						});
					}
					else
					{
						BoolExpression conditionExpression2 = CellCreator.GetConditionExpression(cNode, conditionPropertyMapping);
						cQueryWhereClause = BoolExpression.CreateAnd(new BoolExpression[]
						{
							cQueryWhereClause,
							conditionExpression2
						});
					}
				}
			}
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x000BCB70 File Offset: 0x000BAD70
		private static BoolExpression GetConditionExpression(MemberPath member, ConditionPropertyMapping conditionMap)
		{
			EdmMember edmMember = (conditionMap.Column != null) ? conditionMap.Column : conditionMap.Property;
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

		// Token: 0x060026DC RID: 9948 RVA: 0x000BCC04 File Offset: 0x000BAE04
		private static bool IsBooleanMember(MemberPath path)
		{
			PrimitiveType primitiveType = path.EdmType as PrimitiveType;
			return primitiveType != null && primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Boolean;
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x000BCC2B File Offset: 0x000BAE2B
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append("CellCreator");
		}

		// Token: 0x04000E9B RID: 3739
		private readonly EntityContainerMapping m_containerMapping;

		// Token: 0x04000E9C RID: 3740
		private int m_currentCellNumber;

		// Token: 0x04000E9D RID: 3741
		private readonly CqlIdentifiers m_identifiers;
	}
}
