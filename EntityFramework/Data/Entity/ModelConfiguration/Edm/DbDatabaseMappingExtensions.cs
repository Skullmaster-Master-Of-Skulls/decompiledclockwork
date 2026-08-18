using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x0200080D RID: 2061
	internal static class DbDatabaseMappingExtensions
	{
		// Token: 0x06005CB0 RID: 23728 RVA: 0x001901FB File Offset: 0x0018E3FB
		public static DbDatabaseMapping Initialize(this DbDatabaseMapping databaseMapping, EdmModel model, EdmModel database)
		{
			databaseMapping.Model = model;
			databaseMapping.Database = database;
			databaseMapping.AddEntityContainerMapping(new EntityContainerMapping(model.Containers.Single<EntityContainer>()));
			return databaseMapping;
		}

		// Token: 0x06005CB1 RID: 23729 RVA: 0x00190244 File Offset: 0x0018E444
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by test code.")]
		public static MetadataWorkspace ToMetadataWorkspace(this DbDatabaseMapping databaseMapping)
		{
			EdmItemCollection itemCollection = new EdmItemCollection(databaseMapping.Model);
			StoreItemCollection storeItemCollection = new StoreItemCollection(databaseMapping.Database);
			StorageMappingItemCollection storageMappingItemCollection = databaseMapping.ToStorageMappingItemCollection(itemCollection, storeItemCollection);
			MetadataWorkspace metadataWorkspace = new MetadataWorkspace(() => itemCollection, () => storeItemCollection, () => storageMappingItemCollection);
			new CodeFirstOSpaceLoader(null).LoadTypes(itemCollection, (ObjectItemCollection)metadataWorkspace.GetItemCollection(DataSpace.OSpace));
			return metadataWorkspace;
		}

		// Token: 0x06005CB2 RID: 23730 RVA: 0x001902DC File Offset: 0x0018E4DC
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public static StorageMappingItemCollection ToStorageMappingItemCollection(this DbDatabaseMapping databaseMapping, EdmItemCollection itemCollection, StoreItemCollection storeItemCollection)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings
			{
				Indent = true
			}))
			{
				new MslSerializer().Serialize(databaseMapping, xmlWriter);
			}
			StorageMappingItemCollection result;
			using (XmlReader xmlReader = XmlReader.Create(new StringReader(stringBuilder.ToString())))
			{
				result = new StorageMappingItemCollection(itemCollection, storeItemCollection, new XmlReader[]
				{
					xmlReader
				});
			}
			return result;
		}

		// Token: 0x06005CB3 RID: 23731 RVA: 0x00190378 File Offset: 0x0018E578
		public static EntityTypeMapping GetEntityTypeMapping(this DbDatabaseMapping databaseMapping, EntityType entityType)
		{
			IList<EntityTypeMapping> entityTypeMappings = databaseMapping.GetEntityTypeMappings(entityType);
			if (entityTypeMappings.Count <= 1)
			{
				return entityTypeMappings.FirstOrDefault<EntityTypeMapping>();
			}
			return entityTypeMappings.SingleOrDefault((EntityTypeMapping m) => m.IsHierarchyMapping);
		}

		// Token: 0x06005CB4 RID: 23732 RVA: 0x001903C0 File Offset: 0x0018E5C0
		public static IList<EntityTypeMapping> GetEntityTypeMappings(this DbDatabaseMapping databaseMapping, EntityType entityType)
		{
			List<EntityTypeMapping> list = new List<EntityTypeMapping>();
			foreach (EntitySetMapping entitySetMapping in databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().EntitySetMappings)
			{
				foreach (EntityTypeMapping entityTypeMapping in entitySetMapping.EntityTypeMappings)
				{
					if (entityTypeMapping.EntityType == entityType)
					{
						list.Add(entityTypeMapping);
					}
				}
			}
			return list;
		}

		// Token: 0x06005CB5 RID: 23733 RVA: 0x0019046C File Offset: 0x0018E66C
		public static EntityTypeMapping GetEntityTypeMapping(this DbDatabaseMapping databaseMapping, Type clrType)
		{
			List<EntityTypeMapping> list = new List<EntityTypeMapping>();
			foreach (EntitySetMapping entitySetMapping in databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().EntitySetMappings)
			{
				foreach (EntityTypeMapping entityTypeMapping in entitySetMapping.EntityTypeMappings)
				{
					if (entityTypeMapping.GetClrType() == clrType)
					{
						list.Add(entityTypeMapping);
					}
				}
			}
			if (list.Count <= 1)
			{
				return list.FirstOrDefault<EntityTypeMapping>();
			}
			return list.SingleOrDefault((EntityTypeMapping m) => m.IsHierarchyMapping);
		}

		// Token: 0x06005CB6 RID: 23734 RVA: 0x00190818 File Offset: 0x0018EA18
		public static IEnumerable<Tuple<ColumnMappingBuilder, EntityType>> GetComplexPropertyMappings(this DbDatabaseMapping databaseMapping, Type complexType)
		{
			return from esm in databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().EntitySetMappings
			from etm in esm.EntityTypeMappings
			from etmf in etm.MappingFragments
			from epm in etmf.ColumnMappings
			where epm.PropertyPath.Any((EdmProperty p) => p.IsComplexType && p.ComplexType.GetClrType() == complexType)
			select Tuple.Create<ColumnMappingBuilder, EntityType>(epm, etmf.Table);
		}

		// Token: 0x06005CB7 RID: 23735 RVA: 0x00190ACC File Offset: 0x0018ECCC
		public static IEnumerable<ModificationFunctionParameterBinding> GetComplexParameterBindings(this DbDatabaseMapping databaseMapping, Type complexType)
		{
			return from esm in databaseMapping.GetEntitySetMappings()
			from mfm in esm.ModificationFunctionMappings
			from pb in mfm.PrimaryParameterBindings
			where pb.MemberPath.Members.OfType<EdmProperty>().Any((EdmProperty p) => p.IsComplexType && p.ComplexType.GetClrType() == complexType)
			select pb;
		}

		// Token: 0x06005CB8 RID: 23736 RVA: 0x00190BB8 File Offset: 0x0018EDB8
		public static EntitySetMapping GetEntitySetMapping(this DbDatabaseMapping databaseMapping, EntitySet entitySet)
		{
			return databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().EntitySetMappings.SingleOrDefault((EntitySetMapping e) => e.EntitySet == entitySet);
		}

		// Token: 0x06005CB9 RID: 23737 RVA: 0x00190BF3 File Offset: 0x0018EDF3
		public static IEnumerable<EntitySetMapping> GetEntitySetMappings(this DbDatabaseMapping databaseMapping)
		{
			return databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().EntitySetMappings;
		}

		// Token: 0x06005CBA RID: 23738 RVA: 0x00190C05 File Offset: 0x0018EE05
		public static IEnumerable<AssociationSetMapping> GetAssociationSetMappings(this DbDatabaseMapping databaseMapping)
		{
			return databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().AssociationSetMappings;
		}

		// Token: 0x06005CBB RID: 23739 RVA: 0x00190C18 File Offset: 0x0018EE18
		public static EntitySetMapping AddEntitySetMapping(this DbDatabaseMapping databaseMapping, EntitySet entitySet)
		{
			EntitySetMapping entitySetMapping = new EntitySetMapping(entitySet, null);
			databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>().AddSetMapping(entitySetMapping);
			return entitySetMapping;
		}

		// Token: 0x06005CBC RID: 23740 RVA: 0x00190C40 File Offset: 0x0018EE40
		public static AssociationSetMapping AddAssociationSetMapping(this DbDatabaseMapping databaseMapping, AssociationSet associationSet, EntitySet entitySet)
		{
			EntityContainerMapping entityContainerMapping = databaseMapping.EntityContainerMappings.Single<EntityContainerMapping>();
			AssociationSetMapping associationSetMapping = new AssociationSetMapping(associationSet, entitySet, entityContainerMapping).Initialize();
			entityContainerMapping.AddSetMapping(associationSetMapping);
			return associationSetMapping;
		}
	}
}
