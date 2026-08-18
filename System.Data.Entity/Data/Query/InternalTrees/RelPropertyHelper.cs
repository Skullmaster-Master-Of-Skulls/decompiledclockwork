using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000096 RID: 150
	internal sealed class RelPropertyHelper
	{
		// Token: 0x060009EE RID: 2542 RVA: 0x00035BE0 File Offset: 0x00033DE0
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

		// Token: 0x060009EF RID: 2543 RVA: 0x00035C58 File Offset: 0x00033E58
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

		// Token: 0x060009F0 RID: 2544 RVA: 0x00035CAC File Offset: 0x00033EAC
		internal RelPropertyHelper(MetadataWorkspace ws, HashSet<RelProperty> interestingRelProperties)
		{
			this._relPropertyMap = new Dictionary<EntityTypeBase, List<RelProperty>>();
			this._interestingRelProperties = interestingRelProperties;
			foreach (RelationshipType relationshipType in ws.GetItems<RelationshipType>(DataSpace.CSpace))
			{
				this.ProcessRelationship(relationshipType);
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00035D14 File Offset: 0x00033F14
		internal IEnumerable<RelProperty> GetDeclaredOnlyRelProperties(EntityTypeBase entityType)
		{
			List<RelProperty> list;
			if (this._relPropertyMap.TryGetValue(entityType, out list))
			{
				foreach (RelProperty relProperty in list)
				{
					yield return relProperty;
				}
				List<RelProperty>.Enumerator enumerator = default(List<RelProperty>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00035D2B File Offset: 0x00033F2B
		internal IEnumerable<RelProperty> GetRelProperties(EntityTypeBase entityType)
		{
			IEnumerator<RelProperty> enumerator;
			if (entityType.BaseType != null)
			{
				foreach (RelProperty relProperty in this.GetRelProperties(entityType.BaseType as EntityTypeBase))
				{
					yield return relProperty;
				}
				enumerator = null;
			}
			foreach (RelProperty relProperty2 in this.GetDeclaredOnlyRelProperties(entityType))
			{
				yield return relProperty2;
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x040008AA RID: 2218
		private Dictionary<EntityTypeBase, List<RelProperty>> _relPropertyMap;

		// Token: 0x040008AB RID: 2219
		private HashSet<RelProperty> _interestingRelProperties;
	}
}
