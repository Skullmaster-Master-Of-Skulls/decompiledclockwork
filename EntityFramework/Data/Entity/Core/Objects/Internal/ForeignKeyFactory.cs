using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000588 RID: 1416
	internal class ForeignKeyFactory
	{
		// Token: 0x06003751 RID: 14161 RVA: 0x00106321 File Offset: 0x00104521
		public static bool IsConceptualNullKey(EntityKey key)
		{
			return !(key == null) && string.Equals(key.EntityContainerName, "EntityHasNullForeignKey") && string.Equals(key.EntitySetName, "EntityHasNullForeignKey");
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x00106352 File Offset: 0x00104552
		public static bool IsConceptualNullKeyChanged(EntityKey conceptualNullKey, EntityKey realKey)
		{
			return realKey == null || !EntityKey.InternalEquals(conceptualNullKey, realKey, false);
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x0010636C File Offset: 0x0010456C
		public static EntityKey CreateConceptualNullKey(EntityKey originalKey)
		{
			return new EntityKey("EntityHasNullForeignKey.EntityHasNullForeignKey", originalKey.EntityKeyValues);
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x0010638C File Offset: 0x0010458C
		public static EntityKey CreateKeyFromForeignKeyValues(EntityEntry dependentEntry, RelatedEnd relatedEnd)
		{
			ReferentialConstraint constraint = ((AssociationType)relatedEnd.RelationMetadata).ReferentialConstraints.First<ReferentialConstraint>();
			return ForeignKeyFactory.CreateKeyFromForeignKeyValues(dependentEntry, constraint, relatedEnd.GetTargetEntitySetFromRelationshipSet(), false);
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x001063C0 File Offset: 0x001045C0
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

		// Token: 0x0400153F RID: 5439
		private const string s_NullPart = "EntityHasNullForeignKey";

		// Token: 0x04001540 RID: 5440
		private const string s_NullForeignKey = "EntityHasNullForeignKey.EntityHasNullForeignKey";
	}
}
