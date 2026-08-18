using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000660 RID: 1632
	internal class ConstraintManager
	{
		// Token: 0x06003FB6 RID: 16310 RVA: 0x00123B44 File Offset: 0x00121D44
		internal bool IsParentChildRelationship(EntitySetBase table1, EntitySetBase table2, out List<ForeignKeyConstraint> constraints)
		{
			this.LoadRelationships(table1.EntityContainer);
			this.LoadRelationships(table2.EntityContainer);
			ExtentPair key = new ExtentPair(table1, table2);
			return this.m_parentChildRelationships.TryGetValue(key, out constraints);
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x00123B80 File Offset: 0x00121D80
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
							ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(relationshipSet, constraint);
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

		// Token: 0x06003FB8 RID: 16312 RVA: 0x00123C98 File Offset: 0x00121E98
		internal ConstraintManager()
		{
			this.m_entityContainerMap = new Dictionary<EntityContainer, EntityContainer>();
			this.m_parentChildRelationships = new Dictionary<ExtentPair, List<ForeignKeyConstraint>>();
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x00123CB8 File Offset: 0x00121EB8
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

		// Token: 0x040017C0 RID: 6080
		private readonly Dictionary<EntityContainer, EntityContainer> m_entityContainerMap;

		// Token: 0x040017C1 RID: 6081
		private readonly Dictionary<ExtentPair, List<ForeignKeyConstraint>> m_parentChildRelationships;
	}
}
