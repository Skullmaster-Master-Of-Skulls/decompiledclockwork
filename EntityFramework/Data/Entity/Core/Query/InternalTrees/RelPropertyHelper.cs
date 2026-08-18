using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000640 RID: 1600
	internal sealed class RelPropertyHelper
	{
		// Token: 0x06003ECD RID: 16077 RVA: 0x0011FB40 File Offset: 0x0011DD40
		private void AddRelProperty(AssociationType associationType, AssociationEndMember fromEnd, AssociationEndMember toEnd)
		{
			if (toEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many)
			{
				return;
			}
			RelProperty item = new RelProperty(associationType, fromEnd, toEnd);
			if (this._interestingRelProperties == null || !this._interestingRelProperties.Contains(item))
			{
				return;
			}
			EntityTypeBase elementType = ((RefType)fromEnd.TypeUsage.EdmType).ElementType;
			List<RelProperty> list;
			if (!this._relPropertyMap.TryGetValue(elementType, out list))
			{
				list = new List<RelProperty>();
				this._relPropertyMap[elementType] = list;
			}
			list.Add(item);
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x0011FBB8 File Offset: 0x0011DDB8
		private void ProcessRelationship(RelationshipType relationshipType)
		{
			AssociationType associationType = relationshipType as AssociationType;
			if (associationType == null)
			{
				return;
			}
			if (associationType.AssociationEndMembers.Count != 2)
			{
				return;
			}
			AssociationEndMember associationEndMember = associationType.AssociationEndMembers[0];
			AssociationEndMember associationEndMember2 = associationType.AssociationEndMembers[1];
			this.AddRelProperty(associationType, associationEndMember, associationEndMember2);
			this.AddRelProperty(associationType, associationEndMember2, associationEndMember);
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x0011FC0C File Offset: 0x0011DE0C
		internal RelPropertyHelper(MetadataWorkspace ws, HashSet<RelProperty> interestingRelProperties)
		{
			this._relPropertyMap = new Dictionary<EntityTypeBase, List<RelProperty>>();
			this._interestingRelProperties = interestingRelProperties;
			foreach (RelationshipType relationshipType in ws.GetItems<RelationshipType>(DataSpace.CSpace))
			{
				this.ProcessRelationship(relationshipType);
			}
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x0011FE20 File Offset: 0x0011E020
		internal IEnumerable<RelProperty> GetDeclaredOnlyRelProperties(EntityTypeBase entityType)
		{
			List<RelProperty> relProperties;
			if (this._relPropertyMap.TryGetValue(entityType, out relProperties))
			{
				foreach (RelProperty p in relProperties)
				{
					yield return p;
				}
			}
			yield break;
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x001200C0 File Offset: 0x0011E2C0
		internal IEnumerable<RelProperty> GetRelProperties(EntityTypeBase entityType)
		{
			if (entityType.BaseType != null)
			{
				foreach (RelProperty p in this.GetRelProperties(entityType.BaseType as EntityTypeBase))
				{
					yield return p;
				}
			}
			foreach (RelProperty p2 in this.GetDeclaredOnlyRelProperties(entityType))
			{
				yield return p2;
			}
			yield break;
		}

		// Token: 0x0400177C RID: 6012
		private readonly Dictionary<EntityTypeBase, List<RelProperty>> _relPropertyMap;

		// Token: 0x0400177D RID: 6013
		private readonly HashSet<RelProperty> _interestingRelProperties;
	}
}
