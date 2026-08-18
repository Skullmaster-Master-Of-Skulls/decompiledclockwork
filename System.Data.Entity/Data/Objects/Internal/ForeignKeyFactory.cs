using System;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Linq;

namespace System.Data.Objects.Internal
{
	// Token: 0x0200016C RID: 364
	internal class ForeignKeyFactory
	{
		// Token: 0x06001AE9 RID: 6889 RVA: 0x0005BCF1 File Offset: 0x00059EF1
		public static bool IsConceptualNullKey(EntityKey key)
		{
			return !(key == null) && string.Equals(key.EntityContainerName, "EntityHasNullForeignKey") && string.Equals(key.EntitySetName, "EntityHasNullForeignKey");
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0005BD22 File Offset: 0x00059F22
		public static bool IsConceptualNullKeyChanged(EntityKey conceptualNullKey, EntityKey realKey)
		{
			return realKey == null || !EntityKey.InternalEquals(conceptualNullKey, realKey, false);
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x0005BD3C File Offset: 0x00059F3C
		public static EntityKey CreateConceptualNullKey(EntityKey originalKey)
		{
			return new EntityKey("EntityHasNullForeignKey.EntityHasNullForeignKey", originalKey.EntityKeyValues);
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x0005BD5C File Offset: 0x00059F5C
		public static EntityKey CreateKeyFromForeignKeyValues(EntityEntry dependentEntry, RelatedEnd relatedEnd)
		{
			ReferentialConstraint constraint = ((AssociationType)relatedEnd.RelationMetadata).ReferentialConstraints.First<ReferentialConstraint>();
			return ForeignKeyFactory.CreateKeyFromForeignKeyValues(dependentEntry, constraint, relatedEnd.GetTargetEntitySetFromRelationshipSet(), false);
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x0005BD90 File Offset: 0x00059F90
		public static EntityKey CreateKeyFromForeignKeyValues(EntityEntry dependentEntry, ReferentialConstraint constraint, EntitySet principalEntitySet, bool useOriginalValues)
		{
			ReadOnlyMetadataCollection<EdmProperty> toProperties = constraint.ToProperties;
			int count = toProperties.Count;
			if (count != 1)
			{
				string[] keyMemberNames = principalEntitySet.ElementType.KeyMemberNames;
				object[] array = new object[count];
				ReadOnlyMetadataCollection<EdmProperty> fromProperties = constraint.FromProperties;
				for (int i = 0; i < count; i++)
				{
					object obj = useOriginalValues ? dependentEntry.GetOriginalEntityValue(toProperties[i].Name) : dependentEntry.GetCurrentEntityValue(toProperties[i].Name);
					if (obj == DBNull.Value)
					{
						return null;
					}
					int num = Array.IndexOf<string>(keyMemberNames, fromProperties[i].Name);
					array[num] = obj;
				}
				return new EntityKey(principalEntitySet, array);
			}
			object obj2 = useOriginalValues ? dependentEntry.GetOriginalEntityValue(toProperties.First<EdmProperty>().Name) : dependentEntry.GetCurrentEntityValue(toProperties.First<EdmProperty>().Name);
			if (obj2 != DBNull.Value)
			{
				return new EntityKey(principalEntitySet, obj2);
			}
			return null;
		}

		// Token: 0x04000B31 RID: 2865
		private const string s_NullPart = "EntityHasNullForeignKey";

		// Token: 0x04000B32 RID: 2866
		private const string s_NullForeignKey = "EntityHasNullForeignKey.EntityHasNullForeignKey";
	}
}
