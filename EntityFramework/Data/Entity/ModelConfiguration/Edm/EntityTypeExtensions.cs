using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000816 RID: 2070
	internal static class EntityTypeExtensions
	{
		// Token: 0x06005CFC RID: 23804 RVA: 0x0019140E File Offset: 0x0018F60E
		public static void AddColumn(this EntityType table, EdmProperty column)
		{
			column.SetPreferredName(column.Name);
			column.Name = table.Properties.UniquifyName(column.Name);
			table.AddMember(column);
		}

		// Token: 0x06005CFD RID: 23805 RVA: 0x0019143A File Offset: 0x0018F63A
		public static void SetConfiguration(this EntityType table, object configuration)
		{
			table.GetMetadataProperties().SetConfiguration(configuration);
		}

		// Token: 0x06005CFE RID: 23806 RVA: 0x00191448 File Offset: 0x0018F648
		public static DatabaseName GetTableName(this EntityType table)
		{
			return (DatabaseName)table.Annotations.GetAnnotation("TableName");
		}

		// Token: 0x06005CFF RID: 23807 RVA: 0x0019145F File Offset: 0x0018F65F
		public static void SetTableName(this EntityType table, DatabaseName tableName)
		{
			table.GetMetadataProperties().SetAnnotation("TableName", tableName);
		}

		// Token: 0x06005D00 RID: 23808 RVA: 0x00191472 File Offset: 0x0018F672
		internal static IEnumerable<EntityType> ToHierarchy(this EntityType edmType)
		{
			return EdmType.SafeTraverseHierarchy<EntityType>(edmType);
		}

		// Token: 0x06005D01 RID: 23809 RVA: 0x00191484 File Offset: 0x0018F684
		public static IEnumerable<EdmProperty> GetValidKey(this EntityType entityType)
		{
			List<EdmProperty> list = null;
			List<EntityType> list2 = entityType.ToHierarchy().ToList<EntityType>();
			for (int i = list2.Count - 1; i >= 0; i--)
			{
				EntityType entityType2 = list2[i];
				if (entityType2.BaseType == null && entityType2.KeyProperties.Count > 0)
				{
					if (list != null)
					{
						return Enumerable.Empty<EdmProperty>();
					}
					list = new List<EdmProperty>();
					HashSet<EdmProperty> hashSet = new HashSet<EdmProperty>();
					HashSet<string> hashSet2 = new HashSet<string>();
					HashSet<EdmProperty> hashSet3 = new HashSet<EdmProperty>(from p in entityType2.DeclaredProperties
					where p != null
					select p);
					for (int j = 0; j < entityType2.KeyProperties.Count; j++)
					{
						EdmProperty edmProperty = entityType2.KeyProperties[j];
						if (edmProperty == null || hashSet.Contains(edmProperty) || !hashSet3.Contains(edmProperty) || string.IsNullOrEmpty(edmProperty.Name) || string.IsNullOrWhiteSpace(edmProperty.Name) || hashSet2.Contains(edmProperty.Name))
						{
							return Enumerable.Empty<EdmProperty>();
						}
						list.Add(edmProperty);
						hashSet.Add(edmProperty);
						hashSet2.Add(edmProperty.Name);
					}
				}
			}
			return list ?? Enumerable.Empty<EdmProperty>();
		}

		// Token: 0x06005D02 RID: 23810 RVA: 0x001915D0 File Offset: 0x0018F7D0
		public static List<EdmProperty> GetKeyProperties(this EntityType entityType)
		{
			HashSet<EntityType> visitedTypes = new HashSet<EntityType>();
			List<EdmProperty> list = new List<EdmProperty>();
			EntityTypeExtensions.GetKeyProperties(visitedTypes, entityType, list);
			return list;
		}

		// Token: 0x06005D03 RID: 23811 RVA: 0x001915F4 File Offset: 0x0018F7F4
		private static void GetKeyProperties(HashSet<EntityType> visitedTypes, EntityType visitingType, List<EdmProperty> keyProperties)
		{
			if (visitedTypes.Contains(visitingType))
			{
				return;
			}
			visitedTypes.Add(visitingType);
			if (visitingType.BaseType != null)
			{
				EntityTypeExtensions.GetKeyProperties(visitedTypes, (EntityType)visitingType.BaseType, keyProperties);
				return;
			}
			ReadOnlyMetadataCollection<EdmProperty> keyProperties2 = visitingType.KeyProperties;
			if (keyProperties2.Count > 0)
			{
				keyProperties.AddRange(keyProperties2);
			}
		}

		// Token: 0x06005D04 RID: 23812 RVA: 0x00191648 File Offset: 0x0018F848
		public static EntityType GetRootType(this EntityType entityType)
		{
			EdmType edmType = entityType;
			while (edmType.BaseType != null)
			{
				edmType = edmType.BaseType;
			}
			return (EntityType)edmType;
		}

		// Token: 0x06005D05 RID: 23813 RVA: 0x0019166E File Offset: 0x0018F86E
		public static bool IsAncestorOf(this EntityType ancestor, EntityType entityType)
		{
			while (entityType != null)
			{
				if (entityType.BaseType == ancestor)
				{
					return true;
				}
				entityType = (EntityType)entityType.BaseType;
			}
			return false;
		}

		// Token: 0x06005D06 RID: 23814 RVA: 0x0019168E File Offset: 0x0018F88E
		public static IEnumerable<EdmProperty> KeyProperties(this EntityType entityType)
		{
			return entityType.GetRootType().KeyProperties;
		}

		// Token: 0x06005D07 RID: 23815 RVA: 0x0019169B File Offset: 0x0018F89B
		public static object GetConfiguration(this EntityType entityType)
		{
			return entityType.Annotations.GetConfiguration();
		}

		// Token: 0x06005D08 RID: 23816 RVA: 0x001916A8 File Offset: 0x0018F8A8
		public static Type GetClrType(this EntityType entityType)
		{
			return entityType.Annotations.GetClrType();
		}

		// Token: 0x06005D09 RID: 23817 RVA: 0x0019193C File Offset: 0x0018FB3C
		public static IEnumerable<EntityType> TypeHierarchyIterator(this EntityType entityType, EdmModel model)
		{
			yield return entityType;
			IEnumerable<EntityType> derivedEntityTypes = model.GetDerivedTypes(entityType);
			if (derivedEntityTypes != null)
			{
				foreach (EntityType derivedEntityType in derivedEntityTypes)
				{
					foreach (EntityType derivedEntityType2 in derivedEntityType.TypeHierarchyIterator(model))
					{
						yield return derivedEntityType2;
					}
				}
			}
			yield break;
		}

		// Token: 0x06005D0A RID: 23818 RVA: 0x00191960 File Offset: 0x0018FB60
		public static EdmProperty AddComplexProperty(this EntityType entityType, string name, ComplexType complexType)
		{
			EdmProperty edmProperty = EdmProperty.CreateComplex(name, complexType);
			entityType.AddMember(edmProperty);
			return edmProperty;
		}

		// Token: 0x06005D0B RID: 23819 RVA: 0x00191998 File Offset: 0x0018FB98
		public static EdmProperty GetDeclaredPrimitiveProperty(this EntityType entityType, PropertyInfo propertyInfo)
		{
			return entityType.GetDeclaredPrimitiveProperties().SingleOrDefault((EdmProperty p) => p.GetClrPropertyInfo().IsSameAs(propertyInfo));
		}

		// Token: 0x06005D0C RID: 23820 RVA: 0x001919D1 File Offset: 0x0018FBD1
		public static IEnumerable<EdmProperty> GetDeclaredPrimitiveProperties(this EntityType entityType)
		{
			return from p in entityType.DeclaredProperties
			where p.IsUnderlyingPrimitiveType
			select p;
		}

		// Token: 0x06005D0D RID: 23821 RVA: 0x001919FC File Offset: 0x0018FBFC
		public static NavigationProperty AddNavigationProperty(this EntityType entityType, string name, AssociationType associationType)
		{
			EntityType entityType2 = associationType.TargetEnd.GetEntityType();
			EdmType edmType = associationType.TargetEnd.RelationshipMultiplicity.IsMany() ? entityType2.GetCollectionType() : entityType2;
			NavigationProperty navigationProperty = new NavigationProperty(name, TypeUsage.Create(edmType))
			{
				RelationshipType = associationType,
				FromEndMember = associationType.SourceEnd,
				ToEndMember = associationType.TargetEnd
			};
			entityType.AddMember(navigationProperty);
			return navigationProperty;
		}

		// Token: 0x06005D0E RID: 23822 RVA: 0x00191A84 File Offset: 0x0018FC84
		public static NavigationProperty GetNavigationProperty(this EntityType entityType, PropertyInfo propertyInfo)
		{
			return entityType.NavigationProperties.SingleOrDefault((NavigationProperty np) => np.GetClrPropertyInfo().IsSameAs(propertyInfo));
		}

		// Token: 0x06005D0F RID: 23823 RVA: 0x00191AF0 File Offset: 0x0018FCF0
		public static bool IsRootOfSet(this EntityType entityType, IEnumerable<EntityType> set)
		{
			return set.All((EntityType et) => et == entityType || entityType.IsAncestorOf(et) || et.GetRootType() != entityType.GetRootType());
		}

		// Token: 0x040024D4 RID: 9428
		private const string TableNameAnnotation = "TableName";
	}
}
