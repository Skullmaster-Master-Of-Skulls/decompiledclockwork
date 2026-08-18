using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;

namespace System.Data.Mapping
{
	// Token: 0x02000254 RID: 596
	internal class MetadataMappingHasherVisitor : BaseMetadataMappingVisitor
	{
		// Token: 0x0600253F RID: 9535 RVA: 0x0008AF10 File Offset: 0x00089110
		private MetadataMappingHasherVisitor(double mappingVersion)
		{
			this.m_MappingVersion = mappingVersion;
			this.m_hashSourceBuilder = new CompressingHashBuilder(MetadataHelper.CreateMetadataHashAlgorithm(this.m_MappingVersion));
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x0008AF40 File Offset: 0x00089140
		protected override void Visit(StorageEntityContainerMapping storageEntityContainerMapping)
		{
			this.m_MappingVersion = storageEntityContainerMapping.StorageMappingItemCollection.MappingVersion;
			this.m_EdmVersion = storageEntityContainerMapping.StorageMappingItemCollection.EdmItemCollection.EdmVersion;
			this.m_EdmItemCollection = storageEntityContainerMapping.StorageMappingItemCollection.EdmItemCollection;
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(storageEntityContainerMapping, out objectIndex))
			{
				return;
			}
			if (this.m_itemsAlreadySeen.Count > 1)
			{
				this.Clean();
				this.Visit(storageEntityContainerMapping);
				return;
			}
			this.AddObjectStartDumpToHashBuilder(storageEntityContainerMapping, objectIndex);
			this.AddObjectContentToHashBuilder(storageEntityContainerMapping.Identity);
			this.AddV2ObjectContentToHashBuilder(storageEntityContainerMapping.GenerateUpdateViews, this.m_MappingVersion);
			base.Visit(storageEntityContainerMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x0008AFE8 File Offset: 0x000891E8
		protected override void Visit(EntityContainer entityContainer)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(entityContainer, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(entityContainer, objectIndex);
			this.AddObjectContentToHashBuilder(entityContainer.Identity);
			base.Visit(entityContainer);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x0008B024 File Offset: 0x00089224
		protected override void Visit(StorageSetMapping storageSetMapping)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(storageSetMapping, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(storageSetMapping, objectIndex);
			base.Visit(storageSetMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x0008B054 File Offset: 0x00089254
		protected override void Visit(StorageTypeMapping storageTypeMapping)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(storageTypeMapping, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(storageTypeMapping, objectIndex);
			base.Visit(storageTypeMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x0008B084 File Offset: 0x00089284
		protected override void Visit(StorageMappingFragment storageMappingFragment)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(storageMappingFragment, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(storageMappingFragment, objectIndex);
			this.AddV2ObjectContentToHashBuilder(storageMappingFragment.IsSQueryDistinct, this.m_MappingVersion);
			base.Visit(storageMappingFragment);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x0008B0C9 File Offset: 0x000892C9
		protected override void Visit(StoragePropertyMapping storagePropertyMapping)
		{
			base.Visit(storagePropertyMapping);
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x0008B0D4 File Offset: 0x000892D4
		protected override void Visit(StorageComplexPropertyMapping storageComplexPropertyMapping)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(storageComplexPropertyMapping, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(storageComplexPropertyMapping, objectIndex);
			base.Visit(storageComplexPropertyMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x0008B104 File Offset: 0x00089304
		protected override void Visit(StorageComplexTypeMapping storageComplexTypeMapping)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(storageComplexTypeMapping, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(storageComplexTypeMapping, objectIndex);
			base.Visit(storageComplexTypeMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x0008B134 File Offset: 0x00089334
		protected override void Visit(StorageConditionPropertyMapping storageConditionPropertyMapping)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(storageConditionPropertyMapping, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(storageConditionPropertyMapping, objectIndex);
			this.AddObjectContentToHashBuilder(storageConditionPropertyMapping.IsNull);
			this.AddObjectContentToHashBuilder(storageConditionPropertyMapping.Value);
			base.Visit(storageConditionPropertyMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x0008B180 File Offset: 0x00089380
		protected override void Visit(StorageScalarPropertyMapping storageScalarPropertyMapping)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(storageScalarPropertyMapping, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(storageScalarPropertyMapping, objectIndex);
			base.Visit(storageScalarPropertyMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x0008B1AE File Offset: 0x000893AE
		protected override void Visit(EntitySetBase entitySetBase)
		{
			base.Visit(entitySetBase);
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x0008B1B8 File Offset: 0x000893B8
		protected override void Visit(EntitySet entitySet)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(entitySet, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(entitySet, objectIndex);
			this.AddObjectContentToHashBuilder(entitySet.Name);
			this.AddObjectContentToHashBuilder(entitySet.Schema);
			this.AddObjectContentToHashBuilder(entitySet.Table);
			base.Visit(entitySet);
			IEnumerable<EdmType> typeAndSubtypesOf = MetadataHelper.GetTypeAndSubtypesOf(entitySet.ElementType, this.m_EdmItemCollection, false);
			Func<EdmType, bool> <>9__0;
			Func<EdmType, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((EdmType type) => type != entitySet.ElementType));
			}
			foreach (EdmType edmType in typeAndSubtypesOf.Where(predicate))
			{
				this.Visit(edmType);
			}
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x0008B2AC File Offset: 0x000894AC
		protected override void Visit(AssociationSet associationSet)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(associationSet, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(associationSet, objectIndex);
			this.AddObjectContentToHashBuilder(associationSet.CachedProviderSql);
			this.AddObjectContentToHashBuilder(associationSet.Identity);
			this.AddObjectContentToHashBuilder(associationSet.Schema);
			this.AddObjectContentToHashBuilder(associationSet.Table);
			base.Visit(associationSet);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x0008B30C File Offset: 0x0008950C
		protected override void Visit(EntityType entityType)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(entityType, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(entityType, objectIndex);
			this.AddObjectContentToHashBuilder(entityType.Abstract);
			this.AddObjectContentToHashBuilder(entityType.Identity);
			base.Visit(entityType);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x0008B358 File Offset: 0x00089558
		protected override void Visit(AssociationSetEnd associationSetEnd)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(associationSetEnd, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(associationSetEnd, objectIndex);
			this.AddObjectContentToHashBuilder(associationSetEnd.Identity);
			base.Visit(associationSetEnd);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x0008B394 File Offset: 0x00089594
		protected override void Visit(AssociationType associationType)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(associationType, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(associationType, objectIndex);
			this.AddObjectContentToHashBuilder(associationType.Abstract);
			this.AddObjectContentToHashBuilder(associationType.Identity);
			base.Visit(associationType);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x0008B3E0 File Offset: 0x000895E0
		protected override void Visit(EdmProperty edmProperty)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(edmProperty, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(edmProperty, objectIndex);
			this.AddObjectContentToHashBuilder(edmProperty.DefaultValue);
			this.AddObjectContentToHashBuilder(edmProperty.Identity);
			this.AddObjectContentToHashBuilder(edmProperty.IsStoreGeneratedComputed);
			this.AddObjectContentToHashBuilder(edmProperty.IsStoreGeneratedIdentity);
			this.AddObjectContentToHashBuilder(edmProperty.Nullable);
			base.Visit(edmProperty);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected override void Visit(NavigationProperty navigationProperty)
		{
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x0008B45C File Offset: 0x0008965C
		protected override void Visit(EdmMember edmMember)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(edmMember, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(edmMember, objectIndex);
			this.AddObjectContentToHashBuilder(edmMember.Identity);
			this.AddObjectContentToHashBuilder(edmMember.IsStoreGeneratedComputed);
			this.AddObjectContentToHashBuilder(edmMember.IsStoreGeneratedIdentity);
			base.Visit(edmMember);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x0008B4B8 File Offset: 0x000896B8
		protected override void Visit(AssociationEndMember associationEndMember)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(associationEndMember, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(associationEndMember, objectIndex);
			this.AddObjectContentToHashBuilder(associationEndMember.DeleteBehavior);
			this.AddObjectContentToHashBuilder(associationEndMember.Identity);
			this.AddObjectContentToHashBuilder(associationEndMember.IsStoreGeneratedComputed);
			this.AddObjectContentToHashBuilder(associationEndMember.IsStoreGeneratedIdentity);
			this.AddObjectContentToHashBuilder(associationEndMember.RelationshipMultiplicity);
			base.Visit(associationEndMember);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x0008B538 File Offset: 0x00089738
		protected override void Visit(ReferentialConstraint referentialConstraint)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(referentialConstraint, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(referentialConstraint, objectIndex);
			this.AddObjectContentToHashBuilder(referentialConstraint.Identity);
			base.Visit(referentialConstraint);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x0008B574 File Offset: 0x00089774
		protected override void Visit(RelationshipEndMember relationshipEndMember)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(relationshipEndMember, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(relationshipEndMember, objectIndex);
			this.AddObjectContentToHashBuilder(relationshipEndMember.DeleteBehavior);
			this.AddObjectContentToHashBuilder(relationshipEndMember.Identity);
			this.AddObjectContentToHashBuilder(relationshipEndMember.IsStoreGeneratedComputed);
			this.AddObjectContentToHashBuilder(relationshipEndMember.IsStoreGeneratedIdentity);
			this.AddObjectContentToHashBuilder(relationshipEndMember.RelationshipMultiplicity);
			base.Visit(relationshipEndMember);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x0008B5F4 File Offset: 0x000897F4
		protected override void Visit(TypeUsage typeUsage)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(typeUsage, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(typeUsage, objectIndex);
			base.Visit(typeUsage);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x0008B622 File Offset: 0x00089822
		protected override void Visit(RelationshipType relationshipType)
		{
			base.Visit(relationshipType);
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x0008B62B File Offset: 0x0008982B
		protected override void Visit(EdmType edmType)
		{
			base.Visit(edmType);
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x0008B634 File Offset: 0x00089834
		protected override void Visit(EnumType enumType)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(enumType, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(enumType, objectIndex);
			this.AddObjectContentToHashBuilder(enumType.Identity);
			this.Visit(enumType.UnderlyingType);
			base.Visit(enumType);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x0008B67C File Offset: 0x0008987C
		protected override void Visit(EnumMember enumMember)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(enumMember, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(enumMember, objectIndex);
			this.AddObjectContentToHashBuilder(enumMember.Name);
			this.AddObjectContentToHashBuilder(enumMember.Value);
			base.Visit(enumMember);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x0008B6C4 File Offset: 0x000898C4
		protected override void Visit(CollectionType collectionType)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(collectionType, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(collectionType, objectIndex);
			this.AddObjectContentToHashBuilder(collectionType.Identity);
			base.Visit(collectionType);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x0008B700 File Offset: 0x00089900
		protected override void Visit(RefType refType)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(refType, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(refType, objectIndex);
			this.AddObjectContentToHashBuilder(refType.Identity);
			base.Visit(refType);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x0008B73A File Offset: 0x0008993A
		protected override void Visit(EntityTypeBase entityTypeBase)
		{
			base.Visit(entityTypeBase);
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x0008B744 File Offset: 0x00089944
		protected override void Visit(Facet facet)
		{
			if (facet.Name != "Nullable")
			{
				return;
			}
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(facet, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(facet, objectIndex);
			this.AddObjectContentToHashBuilder(facet.Identity);
			this.AddObjectContentToHashBuilder(facet.Value);
			base.Visit(facet);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected override void Visit(EdmFunction edmFunction)
		{
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x0008B7A0 File Offset: 0x000899A0
		protected override void Visit(ComplexType complexType)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(complexType, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(complexType, objectIndex);
			this.AddObjectContentToHashBuilder(complexType.Abstract);
			this.AddObjectContentToHashBuilder(complexType.Identity);
			base.Visit(complexType);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x0008B7EC File Offset: 0x000899EC
		protected override void Visit(PrimitiveType primitiveType)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(primitiveType, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(primitiveType, objectIndex);
			this.AddObjectContentToHashBuilder(primitiveType.Name);
			this.AddObjectContentToHashBuilder(primitiveType.NamespaceName);
			base.Visit(primitiveType);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x0008B834 File Offset: 0x00089A34
		protected override void Visit(FunctionParameter functionParameter)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(functionParameter, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(functionParameter, objectIndex);
			this.AddObjectContentToHashBuilder(functionParameter.Identity);
			this.AddObjectContentToHashBuilder(functionParameter.Mode);
			base.Visit(functionParameter);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected override void Visit(DbProviderManifest providerManifest)
		{
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x0008B87F File Offset: 0x00089A7F
		internal string HashValue
		{
			get
			{
				return this.m_hashSourceBuilder.ComputeHash();
			}
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x0008B88C File Offset: 0x00089A8C
		private void Clean()
		{
			this.m_hashSourceBuilder = new CompressingHashBuilder(MetadataHelper.CreateMetadataHashAlgorithm(this.m_MappingVersion));
			this.m_instanceNumber = 0;
			this.m_itemsAlreadySeen = new Dictionary<object, int>();
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x0008B8B6 File Offset: 0x00089AB6
		private bool TryAddSeenItem(object o, out int indexSeen)
		{
			if (!this.m_itemsAlreadySeen.TryGetValue(o, out indexSeen))
			{
				this.m_itemsAlreadySeen.Add(o, this.m_instanceNumber);
				indexSeen = this.m_instanceNumber;
				this.m_instanceNumber++;
				return true;
			}
			return false;
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x0008B8F2 File Offset: 0x00089AF2
		private bool AddObjectToSeenListAndHashBuilder(object o, out int instanceIndex)
		{
			if (o == null)
			{
				instanceIndex = -1;
				return false;
			}
			if (!this.TryAddSeenItem(o, out instanceIndex))
			{
				this.AddObjectStartDumpToHashBuilder(o, instanceIndex);
				this.AddSeenObjectToHashBuilder(o, instanceIndex);
				this.AddObjectEndDumpToHashBuilder();
				return false;
			}
			return true;
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x0008B921 File Offset: 0x00089B21
		private void AddSeenObjectToHashBuilder(object o, int instanceIndex)
		{
			this.m_hashSourceBuilder.AppendLine("Instance Reference: " + instanceIndex.ToString());
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x0008B93F File Offset: 0x00089B3F
		private void AddObjectStartDumpToHashBuilder(object o, int objectIndex)
		{
			this.m_hashSourceBuilder.AppendObjectStartDump(o, objectIndex);
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x0008B94E File Offset: 0x00089B4E
		private void AddObjectEndDumpToHashBuilder()
		{
			this.m_hashSourceBuilder.AppendObjectEndDump();
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x0008B95C File Offset: 0x00089B5C
		private void AddObjectContentToHashBuilder(object content)
		{
			if (content == null)
			{
				this.m_hashSourceBuilder.AppendLine("NULL");
				return;
			}
			IFormattable formattable = content as IFormattable;
			if (formattable != null)
			{
				this.m_hashSourceBuilder.AppendLine(formattable.ToString(null, CultureInfo.InvariantCulture));
				return;
			}
			this.m_hashSourceBuilder.AppendLine(content.ToString());
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x0008B9B0 File Offset: 0x00089BB0
		private void AddV2ObjectContentToHashBuilder(object content, double version)
		{
			if (version >= 2.0)
			{
				this.AddObjectContentToHashBuilder(content);
			}
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x0008B9C8 File Offset: 0x00089BC8
		internal static string GetMappingClosureHash(double mappingVersion, StorageEntityContainerMapping storageEntityContainerMapping)
		{
			MetadataMappingHasherVisitor metadataMappingHasherVisitor = new MetadataMappingHasherVisitor(mappingVersion);
			metadataMappingHasherVisitor.Visit(storageEntityContainerMapping);
			return metadataMappingHasherVisitor.HashValue;
		}

		// Token: 0x04001119 RID: 4377
		private CompressingHashBuilder m_hashSourceBuilder;

		// Token: 0x0400111A RID: 4378
		private Dictionary<object, int> m_itemsAlreadySeen = new Dictionary<object, int>();

		// Token: 0x0400111B RID: 4379
		private int m_instanceNumber;

		// Token: 0x0400111C RID: 4380
		private EdmItemCollection m_EdmItemCollection;

		// Token: 0x0400111D RID: 4381
		private double m_EdmVersion;

		// Token: 0x0400111E RID: 4382
		private double m_MappingVersion;
	}
}
