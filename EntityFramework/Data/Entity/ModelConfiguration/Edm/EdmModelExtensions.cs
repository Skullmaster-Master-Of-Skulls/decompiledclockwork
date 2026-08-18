using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm.Services;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000817 RID: 2071
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal static class EdmModelExtensions
	{
		// Token: 0x06005D12 RID: 23826 RVA: 0x00191B1C File Offset: 0x0018FD1C
		public static EntityType AddTable(this EdmModel database, string name)
		{
			string text = database.EntityTypes.UniquifyName(name);
			EntityType entityType = new EntityType(text, "CodeFirstDatabaseSchema", DataSpace.SSpace);
			database.AddItem(entityType);
			database.AddEntitySet(entityType.Name, entityType, text);
			return entityType;
		}

		// Token: 0x06005D13 RID: 23827 RVA: 0x00191B5C File Offset: 0x0018FD5C
		public static EntityType AddTable(this EdmModel database, string name, EntityType pkSource)
		{
			EntityType entityType = database.AddTable(name);
			foreach (EdmProperty tableColumn in pkSource.KeyProperties)
			{
				entityType.AddKeyMember(tableColumn.Clone());
			}
			return entityType;
		}

		// Token: 0x06005D14 RID: 23828 RVA: 0x00191BC0 File Offset: 0x0018FDC0
		public static EdmFunction AddFunction(this EdmModel database, string name, EdmFunctionPayload functionPayload)
		{
			string name2 = database.Functions.UniquifyName(name);
			EdmFunction edmFunction = new EdmFunction(name2, "CodeFirstDatabaseSchema", DataSpace.SSpace, functionPayload);
			database.AddItem(edmFunction);
			return edmFunction;
		}

		// Token: 0x06005D15 RID: 23829 RVA: 0x00191BF0 File Offset: 0x0018FDF0
		public static EntityType FindTableByName(this EdmModel database, DatabaseName tableName)
		{
			IList<EntityType> list = (database.EntityTypes as IList<EntityType>) ?? database.EntityTypes.ToList<EntityType>();
			for (int i = 0; i < list.Count; i++)
			{
				EntityType entityType = list[i];
				DatabaseName tableName2 = entityType.GetTableName();
				if ((tableName2 != null) ? tableName2.Equals(tableName) : (string.Equals(entityType.Name, tableName.Name, StringComparison.Ordinal) && tableName.Schema == null))
				{
					return entityType;
				}
			}
			return null;
		}

		// Token: 0x06005D16 RID: 23830 RVA: 0x00191DFC File Offset: 0x0018FFFC
		public static bool HasCascadeDeletePath(this EdmModel model, EntityType sourceEntityType, EntityType targetEntityType)
		{
			return (from a in model.AssociationTypes
			from ae in a.Members.Cast<AssociationEndMember>()
			where ae.GetEntityType() == sourceEntityType && ae.DeleteBehavior == OperationAction.Cascade
			select a.GetOtherEnd(ae).GetEntityType()).Any((EntityType et) => et == targetEntityType || model.HasCascadeDeletePath(et, targetEntityType));
		}

		// Token: 0x06005D17 RID: 23831 RVA: 0x00191EC4 File Offset: 0x001900C4
		public static IEnumerable<Type> GetClrTypes(this EdmModel model)
		{
			return (from e in model.EntityTypes
			select e.GetClrType()).Union(from ct in model.ComplexTypes
			select ct.GetClrType());
		}

		// Token: 0x06005D18 RID: 23832 RVA: 0x00191F28 File Offset: 0x00190128
		public static NavigationProperty GetNavigationProperty(this EdmModel model, PropertyInfo propertyInfo)
		{
			IList<EntityType> list = (model.EntityTypes as IList<EntityType>) ?? model.EntityTypes.ToList<EntityType>();
			for (int i = 0; i < list.Count; i++)
			{
				NavigationProperty navigationProperty = list[i].GetNavigationProperty(propertyInfo);
				if (navigationProperty != null)
				{
					return navigationProperty;
				}
			}
			return null;
		}

		// Token: 0x06005D19 RID: 23833 RVA: 0x00191F78 File Offset: 0x00190178
		public static void ValidateAndSerializeCsdl(this EdmModel model, XmlWriter writer)
		{
			List<DataModelErrorEventArgs> list = model.SerializeAndGetCsdlErrors(writer);
			if (list.Count > 0)
			{
				throw new ModelValidationException(list);
			}
		}

		// Token: 0x06005D1A RID: 23834 RVA: 0x00191FB4 File Offset: 0x001901B4
		private static List<DataModelErrorEventArgs> SerializeAndGetCsdlErrors(this EdmModel model, XmlWriter writer)
		{
			List<DataModelErrorEventArgs> validationErrors = new List<DataModelErrorEventArgs>();
			CsdlSerializer csdlSerializer = new CsdlSerializer();
			csdlSerializer.OnError += delegate(object s, DataModelErrorEventArgs e)
			{
				validationErrors.Add(e);
			};
			csdlSerializer.Serialize(model, writer, null);
			return validationErrors;
		}

		// Token: 0x06005D1B RID: 23835 RVA: 0x00191FFA File Offset: 0x001901FA
		public static DbDatabaseMapping GenerateDatabaseMapping(this EdmModel model, DbProviderInfo providerInfo, DbProviderManifest providerManifest)
		{
			return new DatabaseMappingGenerator(providerInfo, providerManifest).Generate(model);
		}

		// Token: 0x06005D1C RID: 23836 RVA: 0x00192009 File Offset: 0x00190209
		public static EdmType GetStructuralOrEnumType(this EdmModel model, string name)
		{
			return model.GetStructuralType(name) ?? model.GetEnumType(name);
		}

		// Token: 0x06005D1D RID: 23837 RVA: 0x0019201D File Offset: 0x0019021D
		public static EdmType GetStructuralType(this EdmModel model, string name)
		{
			return model.GetEntityType(name) ?? model.GetComplexType(name);
		}

		// Token: 0x06005D1E RID: 23838 RVA: 0x0019204C File Offset: 0x0019024C
		public static EntityType GetEntityType(this EdmModel model, string name)
		{
			return model.EntityTypes.SingleOrDefault((EntityType e) => e.Name == name);
		}

		// Token: 0x06005D1F RID: 23839 RVA: 0x00192080 File Offset: 0x00190280
		public static EntityType GetEntityType(this EdmModel model, Type clrType)
		{
			IList<EntityType> list = (model.EntityTypes as IList<EntityType>) ?? model.EntityTypes.ToList<EntityType>();
			for (int i = 0; i < list.Count; i++)
			{
				EntityType entityType = list[i];
				if (entityType.GetClrType() == clrType)
				{
					return entityType;
				}
			}
			return null;
		}

		// Token: 0x06005D20 RID: 23840 RVA: 0x001920F0 File Offset: 0x001902F0
		public static ComplexType GetComplexType(this EdmModel model, string name)
		{
			return model.ComplexTypes.SingleOrDefault((ComplexType e) => e.Name == name);
		}

		// Token: 0x06005D21 RID: 23841 RVA: 0x0019213C File Offset: 0x0019033C
		public static ComplexType GetComplexType(this EdmModel model, Type clrType)
		{
			return model.ComplexTypes.SingleOrDefault((ComplexType e) => e.GetClrType() == clrType);
		}

		// Token: 0x06005D22 RID: 23842 RVA: 0x00192188 File Offset: 0x00190388
		public static EnumType GetEnumType(this EdmModel model, string name)
		{
			return model.EnumTypes.SingleOrDefault((EnumType e) => e.Name == name);
		}

		// Token: 0x06005D23 RID: 23843 RVA: 0x001921BC File Offset: 0x001903BC
		public static EntityType AddEntityType(this EdmModel model, string name, string modelNamespace = null)
		{
			EntityType entityType = new EntityType(name, modelNamespace ?? "CodeFirstNamespace", DataSpace.CSpace);
			model.AddItem(entityType);
			return entityType;
		}

		// Token: 0x06005D24 RID: 23844 RVA: 0x00192200 File Offset: 0x00190400
		public static EntitySet GetEntitySet(this EdmModel model, EntityType entityType)
		{
			return model.GetEntitySets().SingleOrDefault((EntitySet e) => e.ElementType == entityType.GetRootType());
		}

		// Token: 0x06005D25 RID: 23845 RVA: 0x0019224C File Offset: 0x0019044C
		public static AssociationSet GetAssociationSet(this EdmModel model, AssociationType associationType)
		{
			return model.Containers.Single<EntityContainer>().AssociationSets.SingleOrDefault((AssociationSet a) => a.ElementType == associationType);
		}

		// Token: 0x06005D26 RID: 23846 RVA: 0x00192287 File Offset: 0x00190487
		public static IEnumerable<EntitySet> GetEntitySets(this EdmModel model)
		{
			return model.Containers.Single<EntityContainer>().EntitySets;
		}

		// Token: 0x06005D27 RID: 23847 RVA: 0x0019229C File Offset: 0x0019049C
		public static EntitySet AddEntitySet(this EdmModel model, string name, EntityType elementType, string table = null)
		{
			EntitySet entitySet = new EntitySet(name, null, table, null, elementType);
			model.Containers.Single<EntityContainer>().AddEntitySetBase(entitySet);
			return entitySet;
		}

		// Token: 0x06005D28 RID: 23848 RVA: 0x001922C8 File Offset: 0x001904C8
		public static ComplexType AddComplexType(this EdmModel model, string name, string modelNamespace = null)
		{
			ComplexType complexType = new ComplexType(name, modelNamespace ?? "CodeFirstNamespace", DataSpace.CSpace);
			model.AddItem(complexType);
			return complexType;
		}

		// Token: 0x06005D29 RID: 23849 RVA: 0x001922F0 File Offset: 0x001904F0
		public static EnumType AddEnumType(this EdmModel model, string name, string modelNamespace = null)
		{
			EnumType enumType = new EnumType(name, modelNamespace ?? "CodeFirstNamespace", PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Int32), false, DataSpace.CSpace);
			model.AddItem(enumType);
			return enumType;
		}

		// Token: 0x06005D2A RID: 23850 RVA: 0x0019233C File Offset: 0x0019053C
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by test code.")]
		public static AssociationType GetAssociationType(this EdmModel model, string name)
		{
			return model.AssociationTypes.SingleOrDefault((AssociationType a) => a.Name == name);
		}

		// Token: 0x06005D2B RID: 23851 RVA: 0x001923D8 File Offset: 0x001905D8
		public static IEnumerable<AssociationType> GetAssociationTypesBetween(this EdmModel model, EntityType first, EntityType second)
		{
			return from a in model.AssociationTypes
			where (a.SourceEnd.GetEntityType() == first && a.TargetEnd.GetEntityType() == second) || (a.SourceEnd.GetEntityType() == second && a.TargetEnd.GetEntityType() == first)
			select a;
		}

		// Token: 0x06005D2C RID: 23852 RVA: 0x00192410 File Offset: 0x00190610
		public static AssociationType AddAssociationType(this EdmModel model, string name, EntityType sourceEntityType, RelationshipMultiplicity sourceAssociationEndKind, EntityType targetEntityType, RelationshipMultiplicity targetAssociationEndKind, string modelNamespace = null)
		{
			AssociationType associationType = new AssociationType(name, modelNamespace ?? "CodeFirstNamespace", false, DataSpace.CSpace)
			{
				SourceEnd = new AssociationEndMember(name + "_Source", sourceEntityType.GetReferenceType(), sourceAssociationEndKind),
				TargetEnd = new AssociationEndMember(name + "_Target", targetEntityType.GetReferenceType(), targetAssociationEndKind)
			};
			model.AddAssociationType(associationType);
			return associationType;
		}

		// Token: 0x06005D2D RID: 23853 RVA: 0x00192477 File Offset: 0x00190677
		public static void AddAssociationType(this EdmModel model, AssociationType associationType)
		{
			model.AddItem(associationType);
		}

		// Token: 0x06005D2E RID: 23854 RVA: 0x00192480 File Offset: 0x00190680
		public static void AddAssociationSet(this EdmModel model, AssociationSet associationSet)
		{
			model.Containers.Single<EntityContainer>().AddEntitySetBase(associationSet);
		}

		// Token: 0x06005D2F RID: 23855 RVA: 0x001924AC File Offset: 0x001906AC
		public static void RemoveEntityType(this EdmModel model, EntityType entityType)
		{
			model.RemoveItem(entityType);
			EntityContainer entityContainer = model.Containers.Single<EntityContainer>();
			EntitySet entitySet = entityContainer.EntitySets.SingleOrDefault((EntitySet a) => a.ElementType == entityType);
			if (entitySet != null)
			{
				entityContainer.RemoveEntitySetBase(entitySet);
			}
		}

		// Token: 0x06005D30 RID: 23856 RVA: 0x00192518 File Offset: 0x00190718
		public static void ReplaceEntitySet(this EdmModel model, EntityType entityType, EntitySet newSet)
		{
			EntityContainer entityContainer = model.Containers.Single<EntityContainer>();
			EntitySet entitySet = entityContainer.EntitySets.SingleOrDefault((EntitySet a) => a.ElementType == entityType);
			if (entitySet != null)
			{
				entityContainer.RemoveEntitySetBase(entitySet);
				if (newSet != null)
				{
					foreach (AssociationSet associationSet in model.Containers.Single<EntityContainer>().AssociationSets)
					{
						if (associationSet.SourceSet == entitySet)
						{
							associationSet.SourceSet = newSet;
						}
						if (associationSet.TargetSet == entitySet)
						{
							associationSet.TargetSet = newSet;
						}
					}
				}
			}
		}

		// Token: 0x06005D31 RID: 23857 RVA: 0x001925E8 File Offset: 0x001907E8
		public static void RemoveAssociationType(this EdmModel model, AssociationType associationType)
		{
			model.RemoveItem(associationType);
			EntityContainer entityContainer = model.Containers.Single<EntityContainer>();
			AssociationSet associationSet = entityContainer.AssociationSets.SingleOrDefault((AssociationSet a) => a.ElementType == associationType);
			if (associationSet != null)
			{
				entityContainer.RemoveEntitySetBase(associationSet);
			}
		}

		// Token: 0x06005D32 RID: 23858 RVA: 0x0019263C File Offset: 0x0019083C
		public static AssociationSet AddAssociationSet(this EdmModel model, string name, AssociationType associationType)
		{
			AssociationSet associationSet = new AssociationSet(name, associationType)
			{
				SourceSet = model.GetEntitySet(associationType.SourceEnd.GetEntityType()),
				TargetSet = model.GetEntitySet(associationType.TargetEnd.GetEntityType())
			};
			model.Containers.Single<EntityContainer>().AddEntitySetBase(associationSet);
			return associationSet;
		}

		// Token: 0x06005D33 RID: 23859 RVA: 0x001926AC File Offset: 0x001908AC
		public static IEnumerable<EntityType> GetDerivedTypes(this EdmModel model, EntityType entityType)
		{
			return from et in model.EntityTypes
			where et.BaseType == entityType
			select et;
		}

		// Token: 0x06005D34 RID: 23860 RVA: 0x001926E0 File Offset: 0x001908E0
		public static IEnumerable<EntityType> GetSelfAndAllDerivedTypes(this EdmModel model, EntityType entityType)
		{
			List<EntityType> list = new List<EntityType>();
			EdmModelExtensions.AddSelfAndAllDerivedTypes(model, entityType, list);
			return list;
		}

		// Token: 0x06005D35 RID: 23861 RVA: 0x00192714 File Offset: 0x00190914
		private static void AddSelfAndAllDerivedTypes(EdmModel model, EntityType entityType, List<EntityType> entityTypes)
		{
			entityTypes.Add(entityType);
			foreach (EntityType entityType2 in from et in model.EntityTypes
			where et.BaseType == entityType
			select et)
			{
				EdmModelExtensions.AddSelfAndAllDerivedTypes(model, entityType2, entityTypes);
			}
		}

		// Token: 0x040024D7 RID: 9431
		public const string DefaultSchema = "dbo";

		// Token: 0x040024D8 RID: 9432
		public const string DefaultModelNamespace = "CodeFirstNamespace";

		// Token: 0x040024D9 RID: 9433
		public const string DefaultStoreNamespace = "CodeFirstDatabaseSchema";
	}
}
