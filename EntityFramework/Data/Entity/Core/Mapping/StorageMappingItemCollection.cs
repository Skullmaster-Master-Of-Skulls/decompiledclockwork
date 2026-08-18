using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.Update.Internal;
using System.Data.Entity.Core.Mapping.ViewGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.SchemaObjectModel;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Xml;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003DF RID: 991
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	public class StorageMappingItemCollection : MappingItemCollection
	{
		// Token: 0x0600245C RID: 9308 RVA: 0x000A73D1 File Offset: 0x000A55D1
		internal StorageMappingItemCollection() : base(DataSpace.CSSpace)
		{
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x000A73F0 File Offset: 0x000A55F0
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public StorageMappingItemCollection(EdmItemCollection edmCollection, StoreItemCollection storeCollection, params string[] filePaths) : base(DataSpace.CSSpace)
		{
			Check.NotNull<EdmItemCollection>(edmCollection, "edmCollection");
			Check.NotNull<StoreItemCollection>(storeCollection, "storeCollection");
			Check.NotNull<string[]>(filePaths, "filePaths");
			this._edmCollection = edmCollection;
			this._storeItemCollection = storeCollection;
			List<XmlReader> list = null;
			try
			{
				MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromFilePaths(filePaths, ".msl");
				list = metadataArtifactLoader.CreateReaders(DataSpace.CSSpace);
				this.Init(edmCollection, storeCollection, list, metadataArtifactLoader.GetPaths(DataSpace.CSSpace), true);
			}
			finally
			{
				if (list != null)
				{
					Helper.DisposeXmlReaders(list);
				}
			}
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000A7494 File Offset: 0x000A5694
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public StorageMappingItemCollection(EdmItemCollection edmCollection, StoreItemCollection storeCollection, IEnumerable<XmlReader> xmlReaders) : base(DataSpace.CSSpace)
		{
			Check.NotNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromXmlReaders(xmlReaders);
			this.Init(edmCollection, storeCollection, metadataArtifactLoader.GetReaders(), metadataArtifactLoader.GetPaths(), true);
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000A74E7 File Offset: 0x000A56E7
		private StorageMappingItemCollection(EdmItemCollection edmItemCollection, StoreItemCollection storeItemCollection, IEnumerable<XmlReader> xmlReaders, IList<string> filePaths, out IList<EdmSchemaError> errors) : base(DataSpace.CSSpace)
		{
			errors = this.Init(edmItemCollection, storeItemCollection, xmlReaders, filePaths, false);
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000A7515 File Offset: 0x000A5715
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		internal StorageMappingItemCollection(EdmItemCollection edmCollection, StoreItemCollection storeCollection, IEnumerable<XmlReader> xmlReaders, IList<string> filePaths) : base(DataSpace.CSSpace)
		{
			this.Init(edmCollection, storeCollection, xmlReaders, filePaths, true);
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000A7544 File Offset: 0x000A5744
		private IList<EdmSchemaError> Init(EdmItemCollection edmCollection, StoreItemCollection storeCollection, IEnumerable<XmlReader> xmlReaders, IList<string> filePaths, bool throwOnError)
		{
			this._edmCollection = edmCollection;
			this._storeItemCollection = storeCollection;
			Dictionary<EntitySetBase, GeneratedView> userDefinedQueryViewsDict;
			Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView> userDefinedQueryViewsOfTypeDict;
			this.m_viewDictionary = new StorageMappingItemCollection.ViewDictionary(this, ref userDefinedQueryViewsDict, ref userDefinedQueryViewsOfTypeDict);
			List<EdmSchemaError> list = new List<EdmSchemaError>();
			if (this._edmCollection.EdmVersion != 0.0 && this._storeItemCollection.StoreSchemaVersion != 0.0 && this._edmCollection.EdmVersion != this._storeItemCollection.StoreSchemaVersion)
			{
				list.Add(new EdmSchemaError(Strings.Mapping_DifferentEdmStoreVersion, 2102, EdmSchemaErrorSeverity.Error));
			}
			else
			{
				double expectedVersion = (this._edmCollection.EdmVersion != 0.0) ? this._edmCollection.EdmVersion : this._storeItemCollection.StoreSchemaVersion;
				list.AddRange(this.LoadItems(xmlReaders, filePaths, userDefinedQueryViewsDict, userDefinedQueryViewsOfTypeDict, expectedVersion));
			}
			if (list.Count > 0 && throwOnError && !MetadataHelper.CheckIfAllErrorsAreWarnings(list))
			{
				throw new MappingException(string.Format(CultureInfo.CurrentCulture, EntityRes.GetString("InvalidSchemaEncountered"), new object[]
				{
					Helper.CombineErrorMessage(list)
				}));
			}
			return list;
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x000A7656 File Offset: 0x000A5856
		// (set) Token: 0x06002463 RID: 9315 RVA: 0x000A765E File Offset: 0x000A585E
		public DbMappingViewCacheFactory MappingViewCacheFactory
		{
			get
			{
				return this._mappingViewCacheFactory;
			}
			set
			{
				Check.NotNull<DbMappingViewCacheFactory>(value, "value");
				Interlocked.CompareExchange<DbMappingViewCacheFactory>(ref this._mappingViewCacheFactory, value, null);
				if (!this._mappingViewCacheFactory.Equals(value))
				{
					throw new ArgumentException(Strings.MappingViewCacheFactory_MustNotChange, "value");
				}
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x000A76AC File Offset: 0x000A58AC
		internal MetadataWorkspace Workspace
		{
			get
			{
				if (this._workspace == null)
				{
					this._workspace = new MetadataWorkspace(() => this._edmCollection, () => this._storeItemCollection, () => this);
				}
				return this._workspace;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06002465 RID: 9317 RVA: 0x000A770B File Offset: 0x000A590B
		internal EdmItemCollection EdmItemCollection
		{
			get
			{
				return this._edmCollection;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x000A7713 File Offset: 0x000A5913
		public double MappingVersion
		{
			get
			{
				return this.m_mappingVersion;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06002467 RID: 9319 RVA: 0x000A771B File Offset: 0x000A591B
		internal StoreItemCollection StoreItemCollection
		{
			get
			{
				return this._storeItemCollection;
			}
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000A7723 File Offset: 0x000A5923
		internal override MappingBase GetMap(string identity, DataSpace typeSpace, bool ignoreCase)
		{
			if (typeSpace != DataSpace.CSpace)
			{
				throw new InvalidOperationException(Strings.Mapping_Storage_InvalidSpace(typeSpace));
			}
			return base.GetItem<MappingBase>(identity, ignoreCase);
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000A7742 File Offset: 0x000A5942
		internal override bool TryGetMap(string identity, DataSpace typeSpace, bool ignoreCase, out MappingBase map)
		{
			if (typeSpace != DataSpace.CSpace)
			{
				throw new InvalidOperationException(Strings.Mapping_Storage_InvalidSpace(typeSpace));
			}
			return base.TryGetItem<MappingBase>(identity, ignoreCase, out map);
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000A7763 File Offset: 0x000A5963
		internal override MappingBase GetMap(string identity, DataSpace typeSpace)
		{
			return this.GetMap(identity, typeSpace, false);
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000A776E File Offset: 0x000A596E
		internal override bool TryGetMap(string identity, DataSpace typeSpace, out MappingBase map)
		{
			return this.TryGetMap(identity, typeSpace, false, out map);
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000A777C File Offset: 0x000A597C
		internal override MappingBase GetMap(GlobalItem item)
		{
			DataSpace dataSpace = item.DataSpace;
			if (dataSpace != DataSpace.CSpace)
			{
				throw new InvalidOperationException(Strings.Mapping_Storage_InvalidSpace(dataSpace));
			}
			return this.GetMap(item.Identity, dataSpace);
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000A77B4 File Offset: 0x000A59B4
		internal override bool TryGetMap(GlobalItem item, out MappingBase map)
		{
			if (item == null)
			{
				map = null;
				return false;
			}
			DataSpace dataSpace = item.DataSpace;
			if (dataSpace != DataSpace.CSpace)
			{
				map = null;
				return false;
			}
			return this.TryGetMap(item.Identity, dataSpace, out map);
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000A77E8 File Offset: 0x000A59E8
		internal ReadOnlyCollection<EdmMember> GetInterestingMembers(EntitySetBase entitySet, EntityTypeBase entityType, StorageMappingItemCollection.InterestingMembersKind interestingMembersKind)
		{
			Tuple<EntitySetBase, EntityTypeBase, StorageMappingItemCollection.InterestingMembersKind> key = new Tuple<EntitySetBase, EntityTypeBase, StorageMappingItemCollection.InterestingMembersKind>(entitySet, entityType, interestingMembersKind);
			return this._cachedInterestingMembers.GetOrAdd(key, this.FindInterestingMembers(entitySet, entityType, interestingMembersKind));
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000A7824 File Offset: 0x000A5A24
		private ReadOnlyCollection<EdmMember> FindInterestingMembers(EntitySetBase entitySet, EntityTypeBase entityType, StorageMappingItemCollection.InterestingMembersKind interestingMembersKind)
		{
			List<EdmMember> list = new List<EdmMember>();
			foreach (TypeMapping typeMapping in MappingMetadataHelper.GetMappingsForEntitySetAndSuperTypes(this, entitySet.EntityContainer, entitySet, entityType))
			{
				AssociationTypeMapping associationTypeMapping = typeMapping as AssociationTypeMapping;
				if (associationTypeMapping != null)
				{
					StorageMappingItemCollection.FindInterestingAssociationMappingMembers(associationTypeMapping, list);
				}
				else
				{
					StorageMappingItemCollection.FindInterestingEntityMappingMembers((EntityTypeMapping)typeMapping, interestingMembersKind, list);
				}
			}
			if (interestingMembersKind != StorageMappingItemCollection.InterestingMembersKind.RequiredOriginalValueMembers)
			{
				StorageMappingItemCollection.FindForeignKeyProperties(entitySet, entityType, list);
			}
			foreach (EntityTypeModificationFunctionMapping functionMappings2 in from functionMappings in MappingMetadataHelper.GetModificationFunctionMappingsForEntitySetAndType(this, entitySet.EntityContainer, entitySet, entityType)
			where functionMappings.UpdateFunctionMapping != null
			select functionMappings)
			{
				StorageMappingItemCollection.FindInterestingFunctionMappingMembers(functionMappings2, interestingMembersKind, ref list);
			}
			return new ReadOnlyCollection<EdmMember>(list.Distinct<EdmMember>().ToList<EdmMember>());
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000A7938 File Offset: 0x000A5B38
		private static void FindInterestingAssociationMappingMembers(AssociationTypeMapping associationTypeMapping, List<EdmMember> interestingMembers)
		{
			interestingMembers.AddRange(from epm in associationTypeMapping.MappingFragments.SelectMany((MappingFragment m) => m.AllProperties).OfType<EndPropertyMapping>()
			select epm.AssociationEnd);
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000A79A4 File Offset: 0x000A5BA4
		private static void FindInterestingEntityMappingMembers(EntityTypeMapping entityTypeMapping, StorageMappingItemCollection.InterestingMembersKind interestingMembersKind, List<EdmMember> interestingMembers)
		{
			foreach (PropertyMapping propertyMapping in entityTypeMapping.MappingFragments.SelectMany((MappingFragment mf) => mf.AllProperties))
			{
				ScalarPropertyMapping scalarPropertyMapping = propertyMapping as ScalarPropertyMapping;
				ComplexPropertyMapping complexPropertyMapping = propertyMapping as ComplexPropertyMapping;
				ConditionPropertyMapping conditionPropertyMapping = propertyMapping as ConditionPropertyMapping;
				if (scalarPropertyMapping != null && scalarPropertyMapping.Property != null)
				{
					if (MetadataHelper.IsPartOfEntityTypeKey(scalarPropertyMapping.Property))
					{
						if (interestingMembersKind == StorageMappingItemCollection.InterestingMembersKind.RequiredOriginalValueMembers)
						{
							interestingMembers.Add(scalarPropertyMapping.Property);
						}
					}
					else if (MetadataHelper.GetConcurrencyMode(scalarPropertyMapping.Property) == ConcurrencyMode.Fixed)
					{
						interestingMembers.Add(scalarPropertyMapping.Property);
					}
				}
				else if (complexPropertyMapping != null)
				{
					if (interestingMembersKind == StorageMappingItemCollection.InterestingMembersKind.PartialUpdate || MetadataHelper.GetConcurrencyMode(complexPropertyMapping.Property) == ConcurrencyMode.Fixed || StorageMappingItemCollection.HasFixedConcurrencyModeInAnyChildProperty(complexPropertyMapping))
					{
						interestingMembers.Add(complexPropertyMapping.Property);
					}
				}
				else if (conditionPropertyMapping != null && conditionPropertyMapping.Property != null)
				{
					interestingMembers.Add(conditionPropertyMapping.Property);
				}
			}
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000A7ABC File Offset: 0x000A5CBC
		private static bool HasFixedConcurrencyModeInAnyChildProperty(ComplexPropertyMapping complexMapping)
		{
			foreach (PropertyMapping propertyMapping in complexMapping.TypeMappings.SelectMany((ComplexTypeMapping m) => m.AllProperties))
			{
				ScalarPropertyMapping scalarPropertyMapping = propertyMapping as ScalarPropertyMapping;
				ComplexPropertyMapping complexPropertyMapping = propertyMapping as ComplexPropertyMapping;
				if (scalarPropertyMapping != null && MetadataHelper.GetConcurrencyMode(scalarPropertyMapping.Property) == ConcurrencyMode.Fixed)
				{
					return true;
				}
				if (complexPropertyMapping != null && (MetadataHelper.GetConcurrencyMode(complexPropertyMapping.Property) == ConcurrencyMode.Fixed || StorageMappingItemCollection.HasFixedConcurrencyModeInAnyChildProperty(complexPropertyMapping)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x000A7BC4 File Offset: 0x000A5DC4
		private static void FindForeignKeyProperties(EntitySetBase entitySetBase, EntityTypeBase entityType, List<EdmMember> interestingMembers)
		{
			EntitySet entitySet = entitySetBase as EntitySet;
			if (entitySet != null && entitySet.HasForeignKeyRelationships)
			{
				interestingMembers.AddRange(from p in MetadataHelper.GetTypeAndParentTypesOf(entityType, true).SelectMany((EdmType e) => ((EntityType)e).Properties)
				where entitySet.ForeignKeyDependents.SelectMany((Tuple<AssociationSet, System.Data.Entity.Core.Metadata.Edm.ReferentialConstraint> fk) => fk.Item2.ToProperties).Contains(p)
				select p);
			}
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000A7C5C File Offset: 0x000A5E5C
		private static void FindInterestingFunctionMappingMembers(EntityTypeModificationFunctionMapping functionMappings, StorageMappingItemCollection.InterestingMembersKind interestingMembersKind, ref List<EdmMember> interestingMembers)
		{
			if (interestingMembersKind == StorageMappingItemCollection.InterestingMembersKind.PartialUpdate)
			{
				interestingMembers.AddRange(from p in functionMappings.UpdateFunctionMapping.ParameterBindings
				select p.MemberPath.Members.Last<EdmMember>());
				return;
			}
			foreach (ModificationFunctionParameterBinding modificationFunctionParameterBinding in from p in functionMappings.UpdateFunctionMapping.ParameterBindings
			where !p.IsCurrent
			select p)
			{
				interestingMembers.Add(modificationFunctionParameterBinding.MemberPath.Members.Last<EdmMember>());
			}
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x000A7D1C File Offset: 0x000A5F1C
		internal GeneratedView GetGeneratedView(EntitySetBase extent, MetadataWorkspace workspace)
		{
			return this.m_viewDictionary.GetGeneratedView(extent, workspace, this);
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000A7D2C File Offset: 0x000A5F2C
		private void AddInternal(MappingBase storageMap)
		{
			storageMap.DataSpace = DataSpace.CSSpace;
			try
			{
				base.AddInternal(storageMap);
			}
			catch (ArgumentException innerException)
			{
				throw new MappingException(Strings.Mapping_Duplicate_Type(storageMap.EdmItem.Identity), innerException);
			}
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000A7D98 File Offset: 0x000A5F98
		internal bool ContainsStorageEntityContainer(string storageEntityContainerName)
		{
			ReadOnlyCollection<EntityContainerMapping> items = this.GetItems<EntityContainerMapping>();
			return items.Any((EntityContainerMapping map) => map.StorageEntityContainer.Name.Equals(storageEntityContainerName, StringComparison.Ordinal));
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000A7DCC File Offset: 0x000A5FCC
		private List<EdmSchemaError> LoadItems(IEnumerable<XmlReader> xmlReaders, IList<string> mappingSchemaUris, Dictionary<EntitySetBase, GeneratedView> userDefinedQueryViewsDict, Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView> userDefinedQueryViewsOfTypeDict, double expectedVersion)
		{
			List<EdmSchemaError> list = new List<EdmSchemaError>();
			int num = -1;
			foreach (XmlReader xmlReader in xmlReaders)
			{
				num++;
				string fileName = null;
				if (mappingSchemaUris == null)
				{
					SchemaManager.TryGetBaseUri(xmlReader, out fileName);
				}
				else
				{
					fileName = mappingSchemaUris[num];
				}
				MappingItemLoader mappingItemLoader = new MappingItemLoader(xmlReader, this, fileName, this.m_memberMappings);
				list.AddRange(mappingItemLoader.ParsingErrors);
				this.CheckIsSameVersion(expectedVersion, mappingItemLoader.MappingVersion, list);
				EntityContainerMapping containerMapping = mappingItemLoader.ContainerMapping;
				if (mappingItemLoader.HasQueryViews && containerMapping != null)
				{
					StorageMappingItemCollection.CompileUserDefinedQueryViews(containerMapping, userDefinedQueryViewsDict, userDefinedQueryViewsOfTypeDict, list);
				}
				if (MetadataHelper.CheckIfAllErrorsAreWarnings(list) && !base.Contains(containerMapping))
				{
					containerMapping.SetReadOnly();
					this.AddInternal(containerMapping);
				}
			}
			StorageMappingItemCollection.CheckForDuplicateItems(this.EdmItemCollection, this.StoreItemCollection, list);
			return list;
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000A7EC0 File Offset: 0x000A60C0
		private static void CompileUserDefinedQueryViews(EntityContainerMapping entityContainerMapping, Dictionary<EntitySetBase, GeneratedView> userDefinedQueryViewsDict, Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView> userDefinedQueryViewsOfTypeDict, IList<EdmSchemaError> errors)
		{
			ConfigViewGenerator config = new ConfigViewGenerator();
			foreach (EntitySetBaseMapping entitySetBaseMapping in entityContainerMapping.AllSetMaps)
			{
				GeneratedView value;
				if (entitySetBaseMapping.QueryView != null && !userDefinedQueryViewsDict.TryGetValue(entitySetBaseMapping.Set, out value))
				{
					if (GeneratedView.TryParseUserSpecifiedView(entitySetBaseMapping, entitySetBaseMapping.Set.ElementType, entitySetBaseMapping.QueryView, true, entityContainerMapping.StorageMappingItemCollection, config, errors, out value))
					{
						userDefinedQueryViewsDict.Add(entitySetBaseMapping.Set, value);
					}
					foreach (Pair<EntitySetBase, Pair<EntityTypeBase, bool>> pair in entitySetBaseMapping.GetTypeSpecificQVKeys())
					{
						if (GeneratedView.TryParseUserSpecifiedView(entitySetBaseMapping, pair.Second.First, entitySetBaseMapping.GetTypeSpecificQueryView(pair), pair.Second.Second, entityContainerMapping.StorageMappingItemCollection, config, errors, out value))
						{
							userDefinedQueryViewsOfTypeDict.Add(pair, value);
						}
					}
				}
			}
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000A7FD8 File Offset: 0x000A61D8
		private void CheckIsSameVersion(double expectedVersion, double currentLoaderVersion, IList<EdmSchemaError> errors)
		{
			if (this.m_mappingVersion == 0.0)
			{
				this.m_mappingVersion = currentLoaderVersion;
			}
			if (expectedVersion != 0.0 && currentLoaderVersion != 0.0 && currentLoaderVersion != expectedVersion)
			{
				errors.Add(new EdmSchemaError(Strings.Mapping_DifferentMappingEdmStoreVersion, 2101, EdmSchemaErrorSeverity.Error));
			}
			if (currentLoaderVersion != this.m_mappingVersion && currentLoaderVersion != 0.0)
			{
				errors.Add(new EdmSchemaError(Strings.CannotLoadDifferentVersionOfSchemaInTheSameItemCollection, 2100, EdmSchemaErrorSeverity.Error));
			}
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x000A805A File Offset: 0x000A625A
		internal ViewLoader GetUpdateViewLoader()
		{
			if (this._viewLoader == null)
			{
				this._viewLoader = new ViewLoader(this);
			}
			return this._viewLoader;
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000A8076 File Offset: 0x000A6276
		internal bool TryGetGeneratedViewOfType(EntitySetBase entity, EntityTypeBase type, bool includeSubtypes, out GeneratedView generatedView)
		{
			return this.m_viewDictionary.TryGetGeneratedViewOfType(entity, type, includeSubtypes, out generatedView);
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000A8088 File Offset: 0x000A6288
		private static void CheckForDuplicateItems(EdmItemCollection edmItemCollection, StoreItemCollection storeItemCollection, List<EdmSchemaError> errorCollection)
		{
			foreach (GlobalItem globalItem in edmItemCollection)
			{
				if (storeItemCollection.Contains(globalItem.Identity))
				{
					errorCollection.Add(new EdmSchemaError(Strings.Mapping_ItemWithSameNameExistsBothInCSpaceAndSSpace(globalItem.Identity), 2070, EdmSchemaErrorSeverity.Error));
				}
			}
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000A8138 File Offset: 0x000A6338
		public string ComputeMappingHashValue(string conceptualModelContainerName, string storeModelContainerName)
		{
			Check.NotEmpty(conceptualModelContainerName, "conceptualModelContainerName");
			Check.NotEmpty(storeModelContainerName, "storeModelContainerName");
			EntityContainerMapping entityContainerMapping = this.GetItems<EntityContainerMapping>().SingleOrDefault((EntityContainerMapping m) => m.EdmEntityContainer.Name == conceptualModelContainerName && m.StorageEntityContainer.Name == storeModelContainerName);
			if (entityContainerMapping == null)
			{
				throw new InvalidOperationException(Strings.HashCalcContainersNotFound(conceptualModelContainerName, storeModelContainerName));
			}
			return MetadataMappingHasherVisitor.GetMappingClosureHash(this.MappingVersion, entityContainerMapping, true);
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x000A81BA File Offset: 0x000A63BA
		public string ComputeMappingHashValue()
		{
			if (this.GetItems<EntityContainerMapping>().Count != 1)
			{
				throw new InvalidOperationException(Strings.HashCalcMultipleContainers);
			}
			return MetadataMappingHasherVisitor.GetMappingClosureHash(this.MappingVersion, this.GetItems<EntityContainerMapping>().Single<EntityContainerMapping>(), true);
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000A8228 File Offset: 0x000A6428
		public Dictionary<EntitySetBase, DbMappingView> GenerateViews(string conceptualModelContainerName, string storeModelContainerName, IList<EdmSchemaError> errors)
		{
			Check.NotEmpty(conceptualModelContainerName, "conceptualModelContainerName");
			Check.NotEmpty(storeModelContainerName, "storeModelContainerName");
			Check.NotNull<IList<EdmSchemaError>>(errors, "errors");
			EntityContainerMapping entityContainerMapping = this.GetItems<EntityContainerMapping>().SingleOrDefault((EntityContainerMapping m) => m.EdmEntityContainer.Name == conceptualModelContainerName && m.StorageEntityContainer.Name == storeModelContainerName);
			if (entityContainerMapping == null)
			{
				throw new InvalidOperationException(Strings.ViewGenContainersNotFound(conceptualModelContainerName, storeModelContainerName));
			}
			return StorageMappingItemCollection.GenerateViews(entityContainerMapping, errors);
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000A82B0 File Offset: 0x000A64B0
		public Dictionary<EntitySetBase, DbMappingView> GenerateViews(IList<EdmSchemaError> errors)
		{
			Check.NotNull<IList<EdmSchemaError>>(errors, "errors");
			if (this.GetItems<EntityContainerMapping>().Count != 1)
			{
				throw new InvalidOperationException(Strings.ViewGenMultipleContainers);
			}
			return StorageMappingItemCollection.GenerateViews(this.GetItems<EntityContainerMapping>().Single<EntityContainerMapping>(), errors);
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x000A8300 File Offset: 0x000A6500
		internal static Dictionary<EntitySetBase, DbMappingView> GenerateViews(EntityContainerMapping containerMapping, IList<EdmSchemaError> errors)
		{
			Dictionary<EntitySetBase, DbMappingView> dictionary = new Dictionary<EntitySetBase, DbMappingView>();
			if (!containerMapping.HasViews)
			{
				return dictionary;
			}
			if (!containerMapping.HasMappingFragments())
			{
				errors.Add(new EdmSchemaError(Strings.Mapping_AllQueryViewAtCompileTime(containerMapping.Identity), 2088, EdmSchemaErrorSeverity.Warning));
				return dictionary;
			}
			ViewGenResults viewGenResults = ViewgenGatekeeper.GenerateViewsFromMapping(containerMapping, new ConfigViewGenerator
			{
				GenerateEsql = true
			});
			if (viewGenResults.HasErrors)
			{
				viewGenResults.Errors.Each(delegate(EdmSchemaError e)
				{
					errors.Add(e);
				});
			}
			foreach (KeyValuePair<EntitySetBase, List<GeneratedView>> keyValuePair in viewGenResults.Views.KeyValuePairs)
			{
				dictionary.Add(keyValuePair.Key, new DbMappingView(keyValuePair.Value[0].eSQL));
			}
			return dictionary;
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x000A8400 File Offset: 0x000A6600
		public static StorageMappingItemCollection Create(EdmItemCollection edmItemCollection, StoreItemCollection storeItemCollection, IEnumerable<XmlReader> xmlReaders, IList<string> filePaths, out IList<EdmSchemaError> errors)
		{
			Check.NotNull<EdmItemCollection>(edmItemCollection, "edmItemCollection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			Check.NotNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentContainsNull<XmlReader>(ref xmlReaders, "xmlReaders");
			StorageMappingItemCollection result = new StorageMappingItemCollection(edmItemCollection, storeItemCollection, xmlReaders, filePaths, ref errors);
			if (errors == null || errors.Count <= 0)
			{
				return result;
			}
			return null;
		}

		// Token: 0x04000D28 RID: 3368
		private EdmItemCollection _edmCollection;

		// Token: 0x04000D29 RID: 3369
		private StoreItemCollection _storeItemCollection;

		// Token: 0x04000D2A RID: 3370
		private StorageMappingItemCollection.ViewDictionary m_viewDictionary;

		// Token: 0x04000D2B RID: 3371
		private double m_mappingVersion;

		// Token: 0x04000D2C RID: 3372
		private MetadataWorkspace _workspace;

		// Token: 0x04000D2D RID: 3373
		private readonly Dictionary<EdmMember, KeyValuePair<TypeUsage, TypeUsage>> m_memberMappings = new Dictionary<EdmMember, KeyValuePair<TypeUsage, TypeUsage>>();

		// Token: 0x04000D2E RID: 3374
		private ViewLoader _viewLoader;

		// Token: 0x04000D2F RID: 3375
		private readonly ConcurrentDictionary<Tuple<EntitySetBase, EntityTypeBase, StorageMappingItemCollection.InterestingMembersKind>, ReadOnlyCollection<EdmMember>> _cachedInterestingMembers = new ConcurrentDictionary<Tuple<EntitySetBase, EntityTypeBase, StorageMappingItemCollection.InterestingMembersKind>, ReadOnlyCollection<EdmMember>>();

		// Token: 0x04000D30 RID: 3376
		private DbMappingViewCacheFactory _mappingViewCacheFactory;

		// Token: 0x020003E0 RID: 992
		// (Invoke) Token: 0x06002490 RID: 9360
		internal delegate bool TryGetUserDefinedQueryView(EntitySetBase extent, out GeneratedView generatedView);

		// Token: 0x020003E1 RID: 993
		// (Invoke) Token: 0x06002494 RID: 9364
		internal delegate bool TryGetUserDefinedQueryViewOfType(Pair<EntitySetBase, Pair<EntityTypeBase, bool>> extent, out GeneratedView generatedView);

		// Token: 0x020003E2 RID: 994
		internal class ViewDictionary
		{
			// Token: 0x06002497 RID: 9367 RVA: 0x000A8460 File Offset: 0x000A6660
			internal ViewDictionary(StorageMappingItemCollection storageMappingItemCollection, out Dictionary<EntitySetBase, GeneratedView> userDefinedQueryViewsDict, out Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView> userDefinedQueryViewsOfTypeDict)
			{
				this._storageMappingItemCollection = storageMappingItemCollection;
				this._generatedViewsMemoizer = new Memoizer<System.Data.Entity.Core.Metadata.Edm.EntityContainer, Dictionary<EntitySetBase, GeneratedView>>(new Func<System.Data.Entity.Core.Metadata.Edm.EntityContainer, Dictionary<EntitySetBase, GeneratedView>>(this.SerializedGetGeneratedViews), null);
				this._generatedViewOfTypeMemoizer = new Memoizer<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView>(new Func<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView>(this.SerializedGeneratedViewOfType), Pair<EntitySetBase, Pair<EntityTypeBase, bool>>.PairComparer.Instance);
				userDefinedQueryViewsDict = new Dictionary<EntitySetBase, GeneratedView>(EqualityComparer<EntitySetBase>.Default);
				userDefinedQueryViewsOfTypeDict = new Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView>(Pair<EntitySetBase, Pair<EntityTypeBase, bool>>.PairComparer.Instance);
				this._tryGetUserDefinedQueryView = new StorageMappingItemCollection.TryGetUserDefinedQueryView(userDefinedQueryViewsDict.TryGetValue);
				this._tryGetUserDefinedQueryViewOfType = new StorageMappingItemCollection.TryGetUserDefinedQueryViewOfType(userDefinedQueryViewsOfTypeDict.TryGetValue);
			}

			// Token: 0x06002498 RID: 9368 RVA: 0x000A84F4 File Offset: 0x000A66F4
			private Dictionary<EntitySetBase, GeneratedView> SerializedGetGeneratedViews(System.Data.Entity.Core.Metadata.Edm.EntityContainer container)
			{
				EntityContainerMapping entityContainerMap = MappingMetadataHelper.GetEntityContainerMap(this._storageMappingItemCollection, container);
				System.Data.Entity.Core.Metadata.Edm.EntityContainer arg = (container.DataSpace == DataSpace.CSpace) ? entityContainerMap.StorageEntityContainer : entityContainerMap.EdmEntityContainer;
				Dictionary<EntitySetBase, GeneratedView> dictionary;
				if (this._generatedViewsMemoizer.TryGetValue(arg, out dictionary))
				{
					return dictionary;
				}
				dictionary = new Dictionary<EntitySetBase, GeneratedView>();
				if (!entityContainerMap.HasViews)
				{
					return dictionary;
				}
				if (this._generatedViewsMode && this._storageMappingItemCollection.MappingViewCacheFactory != null)
				{
					this.SerializedCollectViewsFromCache(entityContainerMap, dictionary);
				}
				if (dictionary.Count == 0)
				{
					this._generatedViewsMode = false;
					StorageMappingItemCollection.ViewDictionary.SerializedGenerateViews(entityContainerMap, dictionary);
				}
				return dictionary;
			}

			// Token: 0x06002499 RID: 9369 RVA: 0x000A857C File Offset: 0x000A677C
			private static void SerializedGenerateViews(EntityContainerMapping entityContainerMap, Dictionary<EntitySetBase, GeneratedView> resultDictionary)
			{
				ViewGenResults viewGenResults = ViewgenGatekeeper.GenerateViewsFromMapping(entityContainerMap, StorageMappingItemCollection.ViewDictionary._config);
				KeyToListMap<EntitySetBase, GeneratedView> views = viewGenResults.Views;
				if (viewGenResults.HasErrors)
				{
					throw new MappingException(Helper.CombineErrorMessage(viewGenResults.Errors));
				}
				foreach (KeyValuePair<EntitySetBase, List<GeneratedView>> keyValuePair in views.KeyValuePairs)
				{
					GeneratedView value;
					if (!resultDictionary.TryGetValue(keyValuePair.Key, out value))
					{
						value = keyValuePair.Value[0];
						resultDictionary.Add(keyValuePair.Key, value);
					}
				}
			}

			// Token: 0x0600249A RID: 9370 RVA: 0x000A8620 File Offset: 0x000A6820
			private bool TryGenerateQueryViewOfType(System.Data.Entity.Core.Metadata.Edm.EntityContainer entityContainer, EntitySetBase entity, EntityTypeBase type, bool includeSubtypes, out GeneratedView generatedView)
			{
				if (type.Abstract)
				{
					generatedView = null;
					return false;
				}
				EntityContainerMapping entityContainerMap = MappingMetadataHelper.GetEntityContainerMap(this._storageMappingItemCollection, entityContainer);
				bool flag;
				ViewGenResults viewGenResults = ViewgenGatekeeper.GenerateTypeSpecificQueryView(entityContainerMap, StorageMappingItemCollection.ViewDictionary._config, entity, type, includeSubtypes, out flag);
				if (!flag)
				{
					generatedView = null;
					return false;
				}
				KeyToListMap<EntitySetBase, GeneratedView> views = viewGenResults.Views;
				if (viewGenResults.HasErrors)
				{
					throw new MappingException(Helper.CombineErrorMessage(viewGenResults.Errors));
				}
				generatedView = views.AllValues.First<GeneratedView>();
				return true;
			}

			// Token: 0x0600249B RID: 9371 RVA: 0x000A8694 File Offset: 0x000A6894
			internal bool TryGetGeneratedViewOfType(EntitySetBase entity, EntityTypeBase type, bool includeSubtypes, out GeneratedView generatedView)
			{
				Pair<EntitySetBase, Pair<EntityTypeBase, bool>> arg = new Pair<EntitySetBase, Pair<EntityTypeBase, bool>>(entity, new Pair<EntityTypeBase, bool>(type, includeSubtypes));
				generatedView = this._generatedViewOfTypeMemoizer.Evaluate(arg);
				return generatedView != null;
			}

			// Token: 0x0600249C RID: 9372 RVA: 0x000A86C8 File Offset: 0x000A68C8
			private GeneratedView SerializedGeneratedViewOfType(Pair<EntitySetBase, Pair<EntityTypeBase, bool>> arg)
			{
				GeneratedView result;
				if (this._tryGetUserDefinedQueryViewOfType(arg, out result))
				{
					return result;
				}
				EntitySetBase first = arg.First;
				EntityTypeBase first2 = arg.Second.First;
				bool second = arg.Second.Second;
				if (!this.TryGenerateQueryViewOfType(first.EntityContainer, first, first2, second, out result))
				{
					result = null;
				}
				return result;
			}

			// Token: 0x0600249D RID: 9373 RVA: 0x000A8994 File Offset: 0x000A6B94
			internal GeneratedView GetGeneratedView(EntitySetBase extent, MetadataWorkspace workspace, StorageMappingItemCollection storageMappingItemCollection)
			{
				GeneratedView result;
				if (this._tryGetUserDefinedQueryView(extent, out result))
				{
					return result;
				}
				if (extent.BuiltInTypeKind == BuiltInTypeKind.AssociationSet)
				{
					AssociationSet aSet = (AssociationSet)extent;
					if (aSet.ElementType.IsForeignKey)
					{
						if (StorageMappingItemCollection.ViewDictionary._config.IsViewTracing)
						{
							Helpers.StringTraceLine(string.Empty);
							Helpers.StringTraceLine(string.Empty);
							Helpers.FormatTraceLine("================= Generating FK Query View for: {0} =================", new object[]
							{
								aSet.Name
							});
							Helpers.StringTraceLine(string.Empty);
							Helpers.StringTraceLine(string.Empty);
						}
						System.Data.Entity.Core.Metadata.Edm.ReferentialConstraint rc = aSet.ElementType.ReferentialConstraints.Single<System.Data.Entity.Core.Metadata.Edm.ReferentialConstraint>();
						EntitySet dependentSet = aSet.AssociationSetEnds[rc.ToRole.Name].EntitySet;
						EntitySet principalSet = aSet.AssociationSetEnds[rc.FromRole.Name].EntitySet;
						DbExpression dbExpression = dependentSet.Scan();
						EntityType dependentType = MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)rc.ToRole);
						EntityType principalType = MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)rc.FromRole);
						if (dependentSet.ElementType.IsBaseTypeOf(dependentType))
						{
							dbExpression = dbExpression.OfType(TypeUsage.Create(dependentType));
						}
						if (rc.FromRole.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne)
						{
							dbExpression = dbExpression.Where(delegate(DbExpression e)
							{
								DbExpression dbExpression2 = null;
								foreach (EdmProperty propertyMetadata in rc.ToProperties)
								{
									DbExpression dbExpression3 = e.Property(propertyMetadata).IsNull().Not();
									dbExpression2 = ((dbExpression2 == null) ? dbExpression3 : dbExpression2.And(dbExpression3));
								}
								return dbExpression2;
							});
						}
						dbExpression = dbExpression.Select(delegate(DbExpression e)
						{
							List<DbExpression> list = new List<DbExpression>();
							foreach (AssociationEndMember associationEndMember in aSet.ElementType.AssociationEndMembers)
							{
								if (associationEndMember.Name == rc.ToRole.Name)
								{
									List<KeyValuePair<string, DbExpression>> list2 = new List<KeyValuePair<string, DbExpression>>();
									foreach (EdmMember edmMember in dependentSet.ElementType.KeyMembers)
									{
										list2.Add(e.Property((EdmProperty)edmMember));
									}
									list.Add(dependentSet.RefFromKey(DbExpressionBuilder.NewRow(list2), dependentType));
								}
								else
								{
									List<KeyValuePair<string, DbExpression>> list3 = new List<KeyValuePair<string, DbExpression>>();
									foreach (EdmMember edmMember2 in principalSet.ElementType.KeyMembers)
									{
										int index = rc.FromProperties.IndexOf((EdmProperty)edmMember2);
										list3.Add(e.Property(rc.ToProperties[index]));
									}
									list.Add(principalSet.RefFromKey(DbExpressionBuilder.NewRow(list3), principalType));
								}
							}
							return TypeUsage.Create(aSet.ElementType).New(list);
						});
						return GeneratedView.CreateGeneratedViewForFKAssociationSet(aSet, aSet.ElementType, new DbQueryCommandTree(workspace, DataSpace.SSpace, dbExpression), storageMappingItemCollection, StorageMappingItemCollection.ViewDictionary._config);
					}
				}
				Dictionary<EntitySetBase, GeneratedView> dictionary = this._generatedViewsMemoizer.Evaluate(extent.EntityContainer);
				if (!dictionary.TryGetValue(extent, out result))
				{
					throw new InvalidOperationException(Strings.Mapping_Views_For_Extent_Not_Generated((extent.EntityContainer.DataSpace == DataSpace.SSpace) ? "Table" : "EntitySet", extent.Name));
				}
				return result;
			}

			// Token: 0x0600249E RID: 9374 RVA: 0x000A8BE0 File Offset: 0x000A6DE0
			private void SerializedCollectViewsFromCache(EntityContainerMapping containerMapping, Dictionary<EntitySetBase, GeneratedView> extentMappingViews)
			{
				DbMappingViewCacheFactory mappingViewCacheFactory = this._storageMappingItemCollection.MappingViewCacheFactory;
				DbMappingViewCache dbMappingViewCache = mappingViewCacheFactory.Create(containerMapping);
				if (dbMappingViewCache == null)
				{
					return;
				}
				string mappingClosureHash = MetadataMappingHasherVisitor.GetMappingClosureHash(containerMapping.StorageMappingItemCollection.MappingVersion, containerMapping, true);
				if (mappingClosureHash != dbMappingViewCache.MappingHashValue)
				{
					throw new MappingException(Strings.ViewGen_HashOnMappingClosure_Not_Matching(dbMappingViewCache.GetType().Name));
				}
				foreach (EntitySetBase entitySetBase in containerMapping.StorageEntityContainer.BaseEntitySets.Union(containerMapping.EdmEntityContainer.BaseEntitySets))
				{
					GeneratedView value;
					if (!extentMappingViews.TryGetValue(entitySetBase, out value))
					{
						DbMappingView view = dbMappingViewCache.GetView(entitySetBase);
						if (view != null)
						{
							value = GeneratedView.CreateGeneratedView(entitySetBase, null, null, view.EntitySql, this._storageMappingItemCollection, new ConfigViewGenerator());
							extentMappingViews.Add(entitySetBase, value);
						}
					}
				}
			}

			// Token: 0x04000D39 RID: 3385
			private readonly StorageMappingItemCollection.TryGetUserDefinedQueryView _tryGetUserDefinedQueryView;

			// Token: 0x04000D3A RID: 3386
			private readonly StorageMappingItemCollection.TryGetUserDefinedQueryViewOfType _tryGetUserDefinedQueryViewOfType;

			// Token: 0x04000D3B RID: 3387
			private readonly StorageMappingItemCollection _storageMappingItemCollection;

			// Token: 0x04000D3C RID: 3388
			private static readonly ConfigViewGenerator _config = new ConfigViewGenerator();

			// Token: 0x04000D3D RID: 3389
			private bool _generatedViewsMode = true;

			// Token: 0x04000D3E RID: 3390
			private readonly Memoizer<System.Data.Entity.Core.Metadata.Edm.EntityContainer, Dictionary<EntitySetBase, GeneratedView>> _generatedViewsMemoizer;

			// Token: 0x04000D3F RID: 3391
			private readonly Memoizer<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView> _generatedViewOfTypeMemoizer;
		}

		// Token: 0x020003E3 RID: 995
		internal enum InterestingMembersKind
		{
			// Token: 0x04000D41 RID: 3393
			RequiredOriginalValueMembers,
			// Token: 0x04000D42 RID: 3394
			FullUpdate,
			// Token: 0x04000D43 RID: 3395
			PartialUpdate
		}
	}
}
