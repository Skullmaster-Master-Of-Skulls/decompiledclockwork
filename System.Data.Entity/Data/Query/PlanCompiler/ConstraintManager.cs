using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200004C RID: 76
	internal class ConstraintManager
	{
		// Token: 0x06000610 RID: 1552 RVA: 0x0001A000 File Offset: 0x00018200
		internal bool IsParentChildRelationship(EntitySetBase table1, EntitySetBase table2, out List<ForeignKeyConstraint> constraints)
		{
			this.LoadRelationships(table1.EntityContainer);
			this.LoadRelationships(table2.EntityContainer);
			ExtentPair key = new ExtentPair(table1, table2);
			return this.m_parentChildRelationships.TryGetValue(key, out constraints);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001A03C File Offset: 0x0001823C
		internal void LoadRelationships(EntityContainer entityContainer)
		{
			if (this.m_entityContainerMap.ContainsKey(entityContainer))
			{
				return;
			}
			foreach (EntitySetBase entitySetBase in entityContainer.BaseEntitySets)
			{
				RelationshipSet relationshipSet = entitySetBase as RelationshipSet;
				if (relationshipSet != null)
				{
					RelationshipType elementType = relationshipSet.ElementType;
					AssociationType associationType = elementType as AssociationType;
					if (associationType != null && ConstraintManager.IsBinary(elementType))
					{
						foreach (ReferentialConstraint constraint in associationType.ReferentialConstraints)
						{
							ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(elementType, relationshipSet, constraint);
							List<ForeignKeyConstraint> list;
							if (!this.m_parentChildRelationships.TryGetValue(foreignKeyConstraint.Pair, out list))
							{
								list = new List<ForeignKeyConstraint>();
								this.m_parentChildRelationships[foreignKeyConstraint.Pair] = list;
							}
							list.Add(foreignKeyConstraint);
						}
					}
				}
			}
			this.m_entityContainerMap[entityContainer] = entityContainer;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001A158 File Offset: 0x00018358
		internal ConstraintManager()
		{
			this.m_entityContainerMap = new Dictionary<EntityContainer, EntityContainer>();
			this.m_parentChildRelationships = new Dictionary<ExtentPair, List<ForeignKeyConstraint>>();
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001A178 File Offset: 0x00018378
		private static bool IsBinary(RelationshipType relationshipType)
		{
			int num = 0;
			foreach (EdmMember edmMember in relationshipType.Members)
			{
				if (edmMember is RelationshipEndMember)
				{
					num++;
					if (num > 2)
					{
						return false;
					}
				}
			}
			return num == 2;
		}

		// Token: 0x04000770 RID: 1904
		private Dictionary<EntityContainer, EntityContainer> m_entityContainerMap;

		// Token: 0x04000771 RID: 1905
		private Dictionary<ExtentPair, List<ForeignKeyConstraint>> m_parentChildRelationships;
	}
}
