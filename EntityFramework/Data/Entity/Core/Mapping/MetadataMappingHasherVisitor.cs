using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003C4 RID: 964
	internal class MetadataMappingHasherVisitor : BaseMetadataMappingVisitor
	{
		// Token: 0x0600231E RID: 8990 RVA: 0x000A4307 File Offset: 0x000A2507
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		private MetadataMappingHasherVisitor(double mappingVersion, bool sortSequence) : base(sortSequence)
		{
			this.m_MappingVersion = mappingVersion;
			this.m_hashSourceBuilder = new CompressingHashBuilder(MetadataHelper.CreateMetadataHashAlgorithm(this.m_MappingVersion));
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x000A4338 File Offset: 0x000A2538
		protected override void Visit(EntityContainerMapping entityContainerMapping)
		{
			this.m_MappingVersion = entityContainerMapping.StorageMappingItemCollection.MappingVersion;
			this.m_EdmItemCollection = entityContainerMapping.StorageMappingItemCollection.EdmItemCollection;
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(entityContainerMapping, out objectIndex))
			{
				return;
			}
			if (this.m_itemsAlreadySeen.Count > 1)
			{
				this.Clean();
				this.Visit(entityContainerMapping);
				return;
			}
			this.AddObjectStartDumpToHashBuilder(entityContainerMapping, objectIndex);
			this.AddObjectContentToHashBuilder(entityContainerMapping.Identity);
			this.AddV2ObjectContentToHashBuilder(entityContainerMapping.GenerateUpdateViews, this.m_MappingVersion);
			base.Visit(entityContainerMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x000A43C8 File Offset: 0x000A25C8
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

		// Token: 0x06002321 RID: 8993 RVA: 0x000A4404 File Offset: 0x000A2604
		protected override void Visit(EntitySetBaseMapping setMapping)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(setMapping, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(setMapping, objectIndex);
			base.Visit(setMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x000A4434 File Offset: 0x000A2634
		protected override void Visit(TypeMapping typeMapping)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(typeMapping, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(typeMapping, objectIndex);
			base.Visit(typeMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000A4464 File Offset: 0x000A2664
		protected override void Visit(MappingFragment mappingFragment)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(mappingFragment, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(mappingFragment, objectIndex);
			this.AddV2ObjectContentToHashBuilder(mappingFragment.IsSQueryDistinct, this.m_MappingVersion);
			base.Visit(mappingFragment);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x000A44A9 File Offset: 0x000A26A9
		protected override void Visit(PropertyMapping propertyMapping)
		{
			base.Visit(propertyMapping);
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x000A44B4 File Offset: 0x000A26B4
		protected override void Visit(ComplexPropertyMapping complexPropertyMapping)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(complexPropertyMapping, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(complexPropertyMapping, objectIndex);
			base.Visit(complexPropertyMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x000A44E4 File Offset: 0x000A26E4
		protected override void Visit(ComplexTypeMapping complexTypeMapping)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(complexTypeMapping, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(complexTypeMapping, objectIndex);
			base.Visit(complexTypeMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x000A4514 File Offset: 0x000A2714
		protected override void Visit(ConditionPropertyMapping conditionPropertyMapping)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(conditionPropertyMapping, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(conditionPropertyMapping, objectIndex);
			this.AddObjectContentToHashBuilder(conditionPropertyMapping.IsNull);
			this.AddObjectContentToHashBuilder(conditionPropertyMapping.Value);
			base.Visit(conditionPropertyMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x000A4560 File Offset: 0x000A2760
		protected override void Visit(ScalarPropertyMapping scalarPropertyMapping)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(scalarPropertyMapping, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(scalarPropertyMapping, objectIndex);
			base.Visit(scalarPropertyMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x000A458E File Offset: 0x000A278E
		protected override void Visit(EntitySetBase entitySetBase)
		{
			base.Visit(entitySetBase);
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000A45BC File Offset: 0x000A27BC
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
			IEnumerable<EdmType> sequence = from type in MetadataHelper.GetTypeAndSubtypesOf(entitySet.ElementType, this.m_EdmItemCollection, false)
			where type != entitySet.ElementType
			select type;
			foreach (EdmType edmType in base.GetSequence<EdmType>(sequence, (EdmType it) => it.Identity))
			{
				this.Visit(edmType);
			}
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x000A46C8 File Offset: 0x000A28C8
		protected override void Visit(AssociationSet associationSet)
		{
			int objectIndex;
			if (!this.AddObjectToSeenListAndHashBuilder(associationSet, out objectIndex))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(associationSet, objectIndex);
			this.AddObjectContentToHashBuilder(associationSet.Identity);
			this.AddObjectContentToHashBuilder(associationSet.Schema);
			this.AddObjectContentToHashBuilder(associationSet.Table);
			base.Visit(associationSet);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x000A471C File Offset: 0x000A291C
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

		// Token: 0x0600232D RID: 9005 RVA: 0x000A4768 File Offset: 0x000A2968
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

		// Token: 0x0600232E RID: 9006 RVA: 0x000A47A4 File Offset: 0x000A29A4
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

		// Token: 0x0600232F RID: 9007 RVA: 0x000A47F0 File Offset: 0x000A29F0
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

		// Token: 0x06002330 RID: 9008 RVA: 0x000A4869 File Offset: 0x000A2A69
		protected override void Visit(NavigationProperty navigationProperty)
		{
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000A486C File Offset: 0x000A2A6C
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

		// Token: 0x06002332 RID: 9010 RVA: 0x000A48C8 File Offset: 0x000A2AC8
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

		// Token: 0x06002333 RID: 9011 RVA: 0x000A4948 File Offset: 0x000A2B48
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

		// Token: 0x06002334 RID: 9012 RVA: 0x000A4984 File Offset: 0x000A2B84
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

		// Token: 0x06002335 RID: 9013 RVA: 0x000A4A04 File Offset: 0x000A2C04
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

		// Token: 0x06002336 RID: 9014 RVA: 0x000A4A32 File Offset: 0x000A2C32
		protected override void Visit(RelationshipType relationshipType)
		{
			base.Visit(relationshipType);
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000A4A3B File Offset: 0x000A2C3B
		protected override void Visit(EdmType edmType)
		{
			base.Visit(edmType);
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x000A4A44 File Offset: 0x000A2C44
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

		// Token: 0x06002339 RID: 9017 RVA: 0x000A4A8C File Offset: 0x000A2C8C
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

		// Token: 0x0600233A RID: 9018 RVA: 0x000A4AD4 File Offset: 0x000A2CD4
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

		// Token: 0x0600233B RID: 9019 RVA: 0x000A4B10 File Offset: 0x000A2D10
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

		// Token: 0x0600233C RID: 9020 RVA: 0x000A4B4A File Offset: 0x000A2D4A
		protected override void Visit(EntityTypeBase entityTypeBase)
		{
			base.Visit(entityTypeBase);
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x000A4B54 File Offset: 0x000A2D54
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

		// Token: 0x0600233E RID: 9022 RVA: 0x000A4BAD File Offset: 0x000A2DAD
		protected override void Visit(EdmFunction edmFunction)
		{
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x000A4BB0 File Offset: 0x000A2DB0
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

		// Token: 0x06002340 RID: 9024 RVA: 0x000A4BFC File Offset: 0x000A2DFC
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

		// Token: 0x06002341 RID: 9025 RVA: 0x000A4C44 File Offset: 0x000A2E44
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

		// Token: 0x06002342 RID: 9026 RVA: 0x000A4C8F File Offset: 0x000A2E8F
		protected override void Visit(DbProviderManifest providerManifest)
		{
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x000A4C91 File Offset: 0x000A2E91
		internal string HashValue
		{
			get
			{
				return this.m_hashSourceBuilder.ComputeHash();
			}
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x000A4C9E File Offset: 0x000A2E9E
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		private void Clean()
		{
			this.m_hashSourceBuilder = new CompressingHashBuilder(MetadataHelper.CreateMetadataHashAlgorithm(this.m_MappingVersion));
			this.m_instanceNumber = 0;
			this.m_itemsAlreadySeen = new Dictionary<object, int>();
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x000A4CC8 File Offset: 0x000A2EC8
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

		// Token: 0x06002346 RID: 9030 RVA: 0x000A4D04 File Offset: 0x000A2F04
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
				this.AddSeenObjectToHashBuilder(instanceIndex);
				this.AddObjectEndDumpToHashBuilder();
				return false;
			}
			return true;
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x000A4D32 File Offset: 0x000A2F32
		private void AddSeenObjectToHashBuilder(int instanceIndex)
		{
			this.m_hashSourceBuilder.AppendLine("Instance Reference: " + instanceIndex);
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x000A4D4F File Offset: 0x000A2F4F
		private void AddObjectStartDumpToHashBuilder(object o, int objectIndex)
		{
			this.m_hashSourceBuilder.AppendObjectStartDump(o, objectIndex);
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x000A4D5E File Offset: 0x000A2F5E
		private void AddObjectEndDumpToHashBuilder()
		{
			this.m_hashSourceBuilder.AppendObjectEndDump();
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x000A4D6C File Offset: 0x000A2F6C
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

		// Token: 0x0600234B RID: 9035 RVA: 0x000A4DC0 File Offset: 0x000A2FC0
		private void AddV2ObjectContentToHashBuilder(object content, double version)
		{
			if (version >= 2.0)
			{
				this.AddObjectContentToHashBuilder(content);
			}
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x000A4DD8 File Offset: 0x000A2FD8
		internal static string GetMappingClosureHash(double mappingVersion, EntityContainerMapping entityContainerMapping, bool sortSequence = true)
		{
			MetadataMappingHasherVisitor metadataMappingHasherVisitor = new MetadataMappingHasherVisitor(mappingVersion, sortSequence);
			metadataMappingHasherVisitor.Visit(entityContainerMapping);
			return metadataMappingHasherVisitor.HashValue;
		}

		// Token: 0x04000C60 RID: 3168
		private CompressingHashBuilder m_hashSourceBuilder;

		// Token: 0x04000C61 RID: 3169
		private Dictionary<object, int> m_itemsAlreadySeen = new Dictionary<object, int>();

		// Token: 0x04000C62 RID: 3170
		private int m_instanceNumber;

		// Token: 0x04000C63 RID: 3171
		private EdmItemCollection m_EdmItemCollection;

		// Token: 0x04000C64 RID: 3172
		private double m_MappingVersion;
	}
}
