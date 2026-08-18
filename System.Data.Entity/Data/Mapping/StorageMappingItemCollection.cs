using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.EntityModel.SchemaObjectModel;
using System.Data.Mapping.Update.Internal;
using System.Data.Mapping.ViewGeneration;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace System.Data.Mapping
{
	// Token: 0x0200024C RID: 588
	[CLSCompliant(false)]
	public class StorageMappingItemCollection : MappingItemCollection
	{
		// Token: 0x06002490 RID: 9360 RVA: 0x00084620 File Offset: 0x00082820
		public StorageMappingItemCollection(EdmItemCollection edmCollection, StoreItemCollection storeCollection, params string[] filePaths) : base(DataSpace.CSSpace)
		{
			EntityUtil.CheckArgumentNull<EdmItemCollection>(edmCollection, "edmCollection");
			EntityUtil.CheckArgumentNull<StoreItemCollection>(storeCollection, "storeCollection");
			EntityUtil.CheckArgumentNull<string[]>(filePaths, "filePaths");
			this.m_edmCollection = edmCollection;
			this.m_storeItemCollection = storeCollection;
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

		// Token: 0x06002491 RID: 9361 RVA: 0x000846C4 File Offset: 0x000828C4
		public StorageMappingItemCollection(EdmItemCollection edmCollection, StoreItemCollection storeCollection, IEnumerable<XmlReader> xmlReaders) : base(DataSpace.CSSpace)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromXmlReaders(xmlReaders);
			this.Init(edmCollection, storeCollection, metadataArtifactLoader.GetReaders(), metadataArtifactLoader.GetPaths(), true);
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x00084718 File Offset: 0x00082918
		internal StorageMappingItemCollection(EdmItemCollection edmCollection, StoreItemCollection storeCollection, IEnumerable<XmlReader> xmlReaders, List<string> filePaths, out IList<EdmSchemaError> errors) : base(DataSpace.CSSpace)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentContainsNull<XmlReader>(ref xmlReaders, "xmlReaders");
			errors = this.Init(edmCollection, storeCollection, xmlReaders, filePaths, false);
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x0008476A File Offset: 0x0008296A
		internal StorageMappingItemCollection(EdmItemCollection edmCollection, StoreItemCollection storeCollection, IEnumerable<XmlReader> xmlReaders, List<string> filePaths) : base(DataSpace.CSSpace)
		{
			this.Init(edmCollection, storeCollection, xmlReaders, filePaths, true);
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x00084798 File Offset: 0x00082998
		private IList<EdmSchemaError> Init(EdmItemCollection edmCollection, StoreItemCollection storeCollection, IEnumerable<XmlReader> xmlReaders, List<string> filePaths, bool throwOnError)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentNull<EdmItemCollection>(edmCollection, "edmCollection");
			EntityUtil.CheckArgumentNull<StoreItemCollection>(storeCollection, "storeCollection");
			this.m_edmCollection = edmCollection;
			this.m_storeItemCollection = storeCollection;
			Dictionary<EntitySetBase, GeneratedView> userDefinedQueryViewsDict;
			Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView> userDefinedQueryViewsOfTypeDict;
			this.m_viewDictionary = new StorageMappingItemCollection.ViewDictionary(this, ref userDefinedQueryViewsDict, ref userDefinedQueryViewsOfTypeDict);
			List<EdmSchemaError> list = new List<EdmSchemaError>();
			if (this.m_edmCollection.EdmVersion != 0.0 && this.m_storeItemCollection.StoreSchemaVersion != 0.0 && this.m_edmCollection.EdmVersion != this.m_storeItemCollection.StoreSchemaVersion)
			{
				list.Add(new EdmSchemaError(Strings.Mapping_DifferentEdmStoreVersion, 2102, EdmSchemaErrorSeverity.Error));
			}
			else
			{
				double expectedVersion = (this.m_edmCollection.EdmVersion != 0.0) ? this.m_edmCollection.EdmVersion : this.m_storeItemCollection.StoreSchemaVersion;
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

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002495 RID: 9365 RVA: 0x000848CC File Offset: 0x00082ACC
		internal MetadataWorkspace Workspace
		{
			get
			{
				if (this.m_workspace == null)
				{
					this.m_workspace = new MetadataWorkspace();
					this.m_workspace.RegisterItemCollection(this.m_edmCollection);
					this.m_workspace.RegisterItemCollection(this.m_storeItemCollection);
					this.m_workspace.RegisterItemCollection(this);
				}
				return this.m_workspace;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06002496 RID: 9366 RVA: 0x00084920 File Offset: 0x00082B20
		internal EdmItemCollection EdmItemCollection
		{
			get
			{
				return this.m_edmCollection;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002497 RID: 9367 RVA: 0x00084928 File Offset: 0x00082B28
		public double MappingVersion
		{
			get
			{
				return this.m_mappingVersion;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002498 RID: 9368 RVA: 0x00084930 File Offset: 0x00082B30
		internal StoreItemCollection StoreItemCollection
		{
			get
			{
				return this.m_storeItemCollection;
			}
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x00084938 File Offset: 0x00082B38
		internal override Map GetMap(string identity, DataSpace typeSpace, bool ignoreCase)
		{
			EntityUtil.CheckArgumentNull<string>(identity, "identity");
			if (typeSpace != DataSpace.CSpace)
			{
				throw EntityUtil.InvalidOperation(Strings.Mapping_Storage_InvalidSpace(typeSpace));
			}
			return base.GetItem<Map>(identity, ignoreCase);
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x00084963 File Offset: 0x00082B63
		internal override bool TryGetMap(string identity, DataSpace typeSpace, bool ignoreCase, out Map map)
		{
			if (typeSpace != DataSpace.CSpace)
			{
				throw EntityUtil.InvalidOperation(Strings.Mapping_Storage_InvalidSpace(typeSpace));
			}
			return base.TryGetItem<Map>(identity, ignoreCase, out map);
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x0008181A File Offset: 0x0007FA1A
		internal override Map GetMap(string identity, DataSpace typeSpace)
		{
			return this.GetMap(identity, typeSpace, false);
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x00081825 File Offset: 0x0007FA25
		internal override bool TryGetMap(string identity, DataSpace typeSpace, out Map map)
		{
			return this.TryGetMap(identity, typeSpace, false, out map);
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x00084984 File Offset: 0x00082B84
		internal override Map GetMap(GlobalItem item)
		{
			EntityUtil.CheckArgumentNull<GlobalItem>(item, "item");
			DataSpace dataSpace = item.DataSpace;
			if (dataSpace != DataSpace.CSpace)
			{
				throw EntityUtil.InvalidOperation(Strings.Mapping_Storage_InvalidSpace(dataSpace));
			}
			return this.GetMap(item.Identity, dataSpace);
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000849C8 File Offset: 0x00082BC8
		internal override bool TryGetMap(GlobalItem item, out Map map)
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

		// Token: 0x0600249F RID: 9375 RVA: 0x000849FC File Offset: 0x00082BFC
		internal Dictionary<EntitySetBase, string> GenerateEntitySetViews(out IList<EdmSchemaError> errors)
		{
			Dictionary<EntitySetBase, string> dictionary = new Dictionary<EntitySetBase, string>();
			errors = new List<EdmSchemaError>();
			foreach (Map map in this.GetItems<Map>())
			{
				StorageEntityContainerMapping storageEntityContainerMapping = map as StorageEntityContainerMapping;
				if (storageEntityContainerMapping != null)
				{
					if (!storageEntityContainerMapping.HasViews)
					{
						return dictionary;
					}
					if (!storageEntityContainerMapping.HasMappingFragments())
					{
						errors.Add(new EdmSchemaError(Strings.Mapping_AllQueryViewAtCompileTime(storageEntityContainerMapping.Identity), 2088, EdmSchemaErrorSeverity.Warning));
					}
					else
					{
						ViewGenResults viewGenResults = ViewgenGatekeeper.GenerateViewsFromMapping(storageEntityContainerMapping, new ConfigViewGenerator
						{
							GenerateEsql = true
						});
						if (viewGenResults.HasErrors)
						{
							((List<EdmSchemaError>)errors).AddRange(viewGenResults.Errors);
						}
						KeyToListMap<EntitySetBase, GeneratedView> views = viewGenResults.Views;
						foreach (KeyValuePair<EntitySetBase, List<GeneratedView>> keyValuePair in views.KeyValuePairs)
						{
							List<GeneratedView> value = keyValuePair.Value;
							dictionary.Add(keyValuePair.Key, value[0].eSQL);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x00084B38 File Offset: 0x00082D38
		internal ReadOnlyCollection<EdmMember> GetInterestingMembers(EntitySetBase entitySet, EntityTypeBase entityType, StorageMappingItemCollection.InterestingMembersKind interestingMembersKind)
		{
			Tuple<EntitySetBase, EntityTypeBase, StorageMappingItemCollection.InterestingMembersKind> key = new Tuple<EntitySetBase, EntityTypeBase, StorageMappingItemCollection.InterestingMembersKind>(entitySet, entityType, interestingMembersKind);
			return this._cachedInterestingMembers.GetOrAdd(key, this.FindInterestingMembers(entitySet, entityType, interestingMembersKind));
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x00084B64 File Offset: 0x00082D64
		private ReadOnlyCollection<EdmMember> FindInterestingMembers(EntitySetBase entitySet, EntityTypeBase entityType, StorageMappingItemCollection.InterestingMembersKind interestingMembersKind)
		{
			List<EdmMember> list = new List<EdmMember>();
			foreach (StorageTypeMapping storageTypeMapping in MappingMetadataHelper.GetMappingsForEntitySetAndSuperTypes(this, entitySet.EntityContainer, entitySet, entityType))
			{
				StorageAssociationTypeMapping storageAssociationTypeMapping = storageTypeMapping as StorageAssociationTypeMapping;
				if (storageAssociationTypeMapping != null)
				{
					StorageMappingItemCollection.FindInterestingAssociationMappingMembers(storageAssociationTypeMapping, list);
				}
				else
				{
					StorageMappingItemCollection.FindInterestingEntityMappingMembers((StorageEntityTypeMapping)storageTypeMapping, interestingMembersKind, list);
				}
			}
			if (interestingMembersKind != StorageMappingItemCollection.InterestingMembersKind.RequiredOriginalValueMembers)
			{
				this.FindForeignKeyProperties(entitySet, entityType, list);
			}
			foreach (StorageEntityTypeModificationFunctionMapping functionMappings2 in from functionMappings in MappingMetadataHelper.GetModificationFunctionMappingsForEntitySetAndType(this, entitySet.EntityContainer, entitySet, entityType)
			where functionMappings.UpdateFunctionMapping != null
			select functionMappings)
			{
				StorageMappingItemCollection.FindInterestingFunctionMappingMembers(functionMappings2, interestingMembersKind, ref list);
			}
			return new ReadOnlyCollection<EdmMember>(list.Distinct<EdmMember>().ToList<EdmMember>());
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x00084C68 File Offset: 0x00082E68
		private static void FindInterestingAssociationMappingMembers(StorageAssociationTypeMapping associationTypeMapping, List<EdmMember> interestingMembers)
		{
			interestingMembers.AddRange(from epm in associationTypeMapping.MappingFragments.SelectMany((StorageMappingFragment m) => m.AllProperties).OfType<StorageEndPropertyMapping>()
			select epm.EndMember);
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x00084CD0 File Offset: 0x00082ED0
		private static void FindInterestingEntityMappingMembers(StorageEntityTypeMapping entityTypeMapping, StorageMappingItemCollection.InterestingMembersKind interestingMembersKind, List<EdmMember> interestingMembers)
		{
			foreach (StoragePropertyMapping storagePropertyMapping in entityTypeMapping.MappingFragments.SelectMany((StorageMappingFragment mf) => mf.AllProperties))
			{
				StorageScalarPropertyMapping storageScalarPropertyMapping = storagePropertyMapping as StorageScalarPropertyMapping;
				StorageComplexPropertyMapping storageComplexPropertyMapping = storagePropertyMapping as StorageComplexPropertyMapping;
				StorageConditionPropertyMapping storageConditionPropertyMapping = storagePropertyMapping as StorageConditionPropertyMapping;
				if (storageScalarPropertyMapping != null && storageScalarPropertyMapping.EdmProperty != null)
				{
					if (MetadataHelper.IsPartOfEntityTypeKey(storageScalarPropertyMapping.EdmProperty))
					{
						if (interestingMembersKind == StorageMappingItemCollection.InterestingMembersKind.RequiredOriginalValueMembers)
						{
							interestingMembers.Add(storageScalarPropertyMapping.EdmProperty);
						}
					}
					else if (MetadataHelper.GetConcurrencyMode(storageScalarPropertyMapping.EdmProperty) == ConcurrencyMode.Fixed)
					{
						interestingMembers.Add(storageScalarPropertyMapping.EdmProperty);
					}
				}
				else if (storageComplexPropertyMapping != null)
				{
					if (interestingMembersKind == StorageMappingItemCollection.InterestingMembersKind.PartialUpdate || MetadataHelper.GetConcurrencyMode(storageComplexPropertyMapping.EdmProperty) == ConcurrencyMode.Fixed || StorageMappingItemCollection.HasFixedConcurrencyModeInAnyChildProperty(storageComplexPropertyMapping))
					{
						interestingMembers.Add(storageComplexPropertyMapping.EdmProperty);
					}
				}
				else if (storageConditionPropertyMapping != null && storageConditionPropertyMapping.EdmProperty != null)
				{
					interestingMembers.Add(storageConditionPropertyMapping.EdmProperty);
				}
			}
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x00084DE0 File Offset: 0x00082FE0
		private static bool HasFixedConcurrencyModeInAnyChildProperty(StorageComplexPropertyMapping complexMapping)
		{
			foreach (StoragePropertyMapping storagePropertyMapping in complexMapping.TypeMappings.SelectMany((StorageComplexTypeMapping m) => m.AllProperties))
			{
				StorageScalarPropertyMapping storageScalarPropertyMapping = storagePropertyMapping as StorageScalarPropertyMapping;
				StorageComplexPropertyMapping storageComplexPropertyMapping = storagePropertyMapping as StorageComplexPropertyMapping;
				if (storageScalarPropertyMapping != null && MetadataHelper.GetConcurrencyMode(storageScalarPropertyMapping.EdmProperty) == ConcurrencyMode.Fixed)
				{
					return true;
				}
				if (storageComplexPropertyMapping != null && (MetadataHelper.GetConcurrencyMode(storageComplexPropertyMapping.EdmProperty) == ConcurrencyMode.Fixed || StorageMappingItemCollection.HasFixedConcurrencyModeInAnyChildProperty(storageComplexPropertyMapping)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x00084E90 File Offset: 0x00083090
		private void FindForeignKeyProperties(EntitySetBase entitySetBase, EntityTypeBase entityType, List<EdmMember> interestingMembers)
		{
			EntitySet entitySet = entitySetBase as EntitySet;
			if (entitySet != null && entitySet.HasForeignKeyRelationships)
			{
				interestingMembers.AddRange(from p in MetadataHelper.GetTypeAndParentTypesOf(entityType, this.m_edmCollection, true).SelectMany((EdmType e) => ((EntityType)e).Properties)
				where entitySet.ForeignKeyDependents.SelectMany((Tuple<AssociationSet, System.Data.Metadata.Edm.ReferentialConstraint> fk) => fk.Item2.ToProperties).Contains(p)
				select p);
			}
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x00084F0C File Offset: 0x0008310C
		private static void FindInterestingFunctionMappingMembers(StorageEntityTypeModificationFunctionMapping functionMappings, StorageMappingItemCollection.InterestingMembersKind interestingMembersKind, ref List<EdmMember> interestingMembers)
		{
			if (interestingMembersKind == StorageMappingItemCollection.InterestingMembersKind.PartialUpdate)
			{
				interestingMembers.AddRange(from p in functionMappings.UpdateFunctionMapping.ParameterBindings
				select p.MemberPath.Members.Last<EdmMember>());
				return;
			}
			foreach (StorageModificationFunctionParameterBinding storageModificationFunctionParameterBinding in from p in functionMappings.UpdateFunctionMapping.ParameterBindings
			where !p.IsCurrent
			select p)
			{
				interestingMembers.Add(storageModificationFunctionParameterBinding.MemberPath.Members.Last<EdmMember>());
			}
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x00084FD0 File Offset: 0x000831D0
		internal GeneratedView GetGeneratedView(EntitySetBase extent, MetadataWorkspace workspace)
		{
			return this.m_viewDictionary.GetGeneratedView(extent, workspace, this);
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x00084FE0 File Offset: 0x000831E0
		private void AddInternal(Map storageMap)
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

		// Token: 0x060024A9 RID: 9385 RVA: 0x00085028 File Offset: 0x00083228
		internal bool ContainsStorageEntityContainer(string storageEntityContainerName)
		{
			ReadOnlyCollection<StorageEntityContainerMapping> items = this.GetItems<StorageEntityContainerMapping>();
			return items.Any((StorageEntityContainerMapping map) => map.StorageEntityContainer.Name.Equals(storageEntityContainerName, StringComparison.Ordinal));
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x0008505C File Offset: 0x0008325C
		private List<EdmSchemaError> LoadItems(IEnumerable<XmlReader> xmlReaders, List<string> mappingSchemaUris, Dictionary<EntitySetBase, GeneratedView> userDefinedQueryViewsDict, Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView> userDefinedQueryViewsOfTypeDict, double expectedVersion)
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
				StorageMappingItemLoader storageMappingItemLoader = new StorageMappingItemLoader(xmlReader, this, fileName, this.m_memberMappings);
				list.AddRange(storageMappingItemLoader.ParsingErrors);
				this.CheckIsSameVersion(expectedVersion, storageMappingItemLoader.MappingVersion, list);
				StorageEntityContainerMapping containerMapping = storageMappingItemLoader.ContainerMapping;
				if (storageMappingItemLoader.HasQueryViews && containerMapping != null)
				{
					StorageMappingItemCollection.CompileUserDefinedQueryViews(containerMapping, userDefinedQueryViewsDict, userDefinedQueryViewsOfTypeDict, list);
				}
				if (MetadataHelper.CheckIfAllErrorsAreWarnings(list) && !base.Contains(containerMapping))
				{
					this.AddInternal(containerMapping);
				}
			}
			StorageMappingItemCollection.CheckForDuplicateItems(this.EdmItemCollection, this.StoreItemCollection, list);
			return list;
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x00085148 File Offset: 0x00083348
		private static void CompileUserDefinedQueryViews(StorageEntityContainerMapping entityContainerMapping, Dictionary<EntitySetBase, GeneratedView> userDefinedQueryViewsDict, Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView> userDefinedQueryViewsOfTypeDict, IList<EdmSchemaError> errors)
		{
			ConfigViewGenerator config = new ConfigViewGenerator();
			foreach (StorageSetMapping storageSetMapping in entityContainerMapping.AllSetMaps)
			{
				GeneratedView value;
				if (storageSetMapping.QueryView != null && !userDefinedQueryViewsDict.TryGetValue(storageSetMapping.Set, out value))
				{
					if (GeneratedView.TryParseUserSpecifiedView(storageSetMapping, storageSetMapping.Set.ElementType, storageSetMapping.QueryView, true, entityContainerMapping.StorageMappingItemCollection, config, errors, out value))
					{
						userDefinedQueryViewsDict.Add(storageSetMapping.Set, value);
					}
					foreach (Pair<EntitySetBase, Pair<EntityTypeBase, bool>> pair in storageSetMapping.GetTypeSpecificQVKeys())
					{
						if (GeneratedView.TryParseUserSpecifiedView(storageSetMapping, pair.Second.First, storageSetMapping.GetTypeSpecificQueryView(pair), pair.Second.Second, entityContainerMapping.StorageMappingItemCollection, config, errors, out value))
						{
							userDefinedQueryViewsOfTypeDict.Add(pair, value);
						}
					}
				}
			}
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x00085260 File Offset: 0x00083460
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

		// Token: 0x060024AD RID: 9389 RVA: 0x000852E2 File Offset: 0x000834E2
		internal ViewLoader GetUpdateViewLoader()
		{
			if (this._viewLoader == null)
			{
				this._viewLoader = new ViewLoader(this);
			}
			return this._viewLoader;
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000852FE File Offset: 0x000834FE
		internal bool TryGetGeneratedViewOfType(MetadataWorkspace workspace, EntitySetBase entity, EntityTypeBase type, bool includeSubtypes, out GeneratedView generatedView)
		{
			return this.m_viewDictionary.TryGetGeneratedViewOfType(workspace, entity, type, includeSubtypes, out generatedView);
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x00085314 File Offset: 0x00083514
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

		// Token: 0x040010A7 RID: 4263
		private EdmItemCollection m_edmCollection;

		// Token: 0x040010A8 RID: 4264
		private StoreItemCollection m_storeItemCollection;

		// Token: 0x040010A9 RID: 4265
		private StorageMappingItemCollection.ViewDictionary m_viewDictionary;

		// Token: 0x040010AA RID: 4266
		private double m_mappingVersion;

		// Token: 0x040010AB RID: 4267
		private MetadataWorkspace m_workspace;

		// Token: 0x040010AC RID: 4268
		private Dictionary<EdmMember, KeyValuePair<TypeUsage, TypeUsage>> m_memberMappings = new Dictionary<EdmMember, KeyValuePair<TypeUsage, TypeUsage>>();

		// Token: 0x040010AD RID: 4269
		private ViewLoader _viewLoader;

		// Token: 0x040010AE RID: 4270
		private ConcurrentDictionary<Tuple<EntitySetBase, EntityTypeBase, StorageMappingItemCollection.InterestingMembersKind>, ReadOnlyCollection<EdmMember>> _cachedInterestingMembers = new ConcurrentDictionary<Tuple<EntitySetBase, EntityTypeBase, StorageMappingItemCollection.InterestingMembersKind>, ReadOnlyCollection<EdmMember>>();

		// Token: 0x02000570 RID: 1392
		internal enum InterestingMembersKind
		{
			// Token: 0x04001C53 RID: 7251
			RequiredOriginalValueMembers,
			// Token: 0x04001C54 RID: 7252
			FullUpdate,
			// Token: 0x04001C55 RID: 7253
			PartialUpdate
		}

		// Token: 0x02000571 RID: 1393
		// (Invoke) Token: 0x06003F9C RID: 16284
		internal delegate bool TryGetUserDefinedQueryView(EntitySetBase extent, out GeneratedView generatedView);

		// Token: 0x02000572 RID: 1394
		// (Invoke) Token: 0x06003FA0 RID: 16288
		internal delegate bool TryGetUserDefinedQueryViewOfType(Pair<EntitySetBase, Pair<EntityTypeBase, bool>> extent, out GeneratedView generatedView);

		// Token: 0x02000573 RID: 1395
		internal class ViewDictionary
		{
			// Token: 0x06003FA3 RID: 16291 RVA: 0x000EA7D8 File Offset: 0x000E89D8
			internal ViewDictionary(StorageMappingItemCollection storageMappingItemCollection, out Dictionary<EntitySetBase, GeneratedView> userDefinedQueryViewsDict, out Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView> userDefinedQueryViewsOfTypeDict)
			{
				this.m_storageMappingItemCollection = storageMappingItemCollection;
				this.m_generatedViewsMemoizer = new Memoizer<System.Data.Metadata.Edm.EntityContainer, Dictionary<EntitySetBase, GeneratedView>>(new Func<System.Data.Metadata.Edm.EntityContainer, Dictionary<EntitySetBase, GeneratedView>>(this.SerializedGetGeneratedViews), null);
				this.m_generatedViewOfTypeMemoizer = new Memoizer<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView>(new Func<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView>(this.SerializedGeneratedViewOfType), Pair<EntitySetBase, Pair<EntityTypeBase, bool>>.PairComparer.Instance);
				userDefinedQueryViewsDict = new Dictionary<EntitySetBase, GeneratedView>(EqualityComparer<EntitySetBase>.Default);
				userDefinedQueryViewsOfTypeDict = new Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView>(Pair<EntitySetBase, Pair<EntityTypeBase, bool>>.PairComparer.Instance);
				this.TryGetUserDefinedQueryView = new StorageMappingItemCollection.TryGetUserDefinedQueryView(userDefinedQueryViewsDict.TryGetValue);
				this.TryGetUserDefinedQueryViewOfType = new StorageMappingItemCollection.TryGetUserDefinedQueryViewOfType(userDefinedQueryViewsOfTypeDict.TryGetValue);
			}

			// Token: 0x06003FA4 RID: 16292 RVA: 0x000EA878 File Offset: 0x000E8A78
			private Dictionary<EntitySetBase, GeneratedView> SerializedGetGeneratedViews(System.Data.Metadata.Edm.EntityContainer container)
			{
				StorageEntityContainerMapping entityContainerMap = MappingMetadataHelper.GetEntityContainerMap(this.m_storageMappingItemCollection, container);
				System.Data.Metadata.Edm.EntityContainer arg = (container.DataSpace == DataSpace.CSpace) ? entityContainerMap.StorageEntityContainer : entityContainerMap.EdmEntityContainer;
				Dictionary<EntitySetBase, GeneratedView> dictionary;
				if (this.m_generatedViewsMemoizer.TryGetValue(arg, out dictionary))
				{
					return dictionary;
				}
				dictionary = new Dictionary<EntitySetBase, GeneratedView>();
				if (!entityContainerMap.HasViews)
				{
					return dictionary;
				}
				if (this.m_generatedViewsMode)
				{
					if (ObjectItemCollection.ViewGenerationAssemblies != null && ObjectItemCollection.ViewGenerationAssemblies.Count > 0)
					{
						this.SerializedCollectViewsFromObjectCollection(this.m_storageMappingItemCollection.Workspace, dictionary);
					}
					else
					{
						this.SerializedCollectViewsFromReferencedAssemblies(this.m_storageMappingItemCollection.Workspace, dictionary);
					}
				}
				if (dictionary.Count == 0)
				{
					this.m_generatedViewsMode = false;
					this.SerializedGenerateViews(entityContainerMap, dictionary);
				}
				return dictionary;
			}

			// Token: 0x06003FA5 RID: 16293 RVA: 0x000EA928 File Offset: 0x000E8B28
			private void SerializedGenerateViews(StorageEntityContainerMapping entityContainerMap, Dictionary<EntitySetBase, GeneratedView> resultDictionary)
			{
				ViewGenResults viewGenResults = ViewgenGatekeeper.GenerateViewsFromMapping(entityContainerMap, StorageMappingItemCollection.ViewDictionary.m_config);
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

			// Token: 0x06003FA6 RID: 16294 RVA: 0x000EA9CC File Offset: 0x000E8BCC
			private bool TryGenerateQueryViewOfType(System.Data.Metadata.Edm.EntityContainer entityContainer, EntitySetBase entity, EntityTypeBase type, bool includeSubtypes, out GeneratedView generatedView)
			{
				if (type.Abstract)
				{
					generatedView = null;
					return false;
				}
				StorageEntityContainerMapping entityContainerMap = MappingMetadataHelper.GetEntityContainerMap(this.m_storageMappingItemCollection, entityContainer);
				bool flag;
				ViewGenResults viewGenResults = ViewgenGatekeeper.GenerateTypeSpecificQueryView(entityContainerMap, StorageMappingItemCollection.ViewDictionary.m_config, entity, type, includeSubtypes, out flag);
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

			// Token: 0x06003FA7 RID: 16295 RVA: 0x000EAA40 File Offset: 0x000E8C40
			internal bool TryGetGeneratedViewOfType(MetadataWorkspace workspace, EntitySetBase entity, EntityTypeBase type, bool includeSubtypes, out GeneratedView generatedView)
			{
				Pair<EntitySetBase, Pair<EntityTypeBase, bool>> arg = new Pair<EntitySetBase, Pair<EntityTypeBase, bool>>(entity, new Pair<EntityTypeBase, bool>(type, includeSubtypes));
				generatedView = this.m_generatedViewOfTypeMemoizer.Evaluate(arg);
				return generatedView != null;
			}

			// Token: 0x06003FA8 RID: 16296 RVA: 0x000EAA74 File Offset: 0x000E8C74
			private GeneratedView SerializedGeneratedViewOfType(Pair<EntitySetBase, Pair<EntityTypeBase, bool>> arg)
			{
				GeneratedView result;
				if (this.TryGetUserDefinedQueryViewOfType(arg, out result))
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

			// Token: 0x06003FA9 RID: 16297 RVA: 0x000EAAC8 File Offset: 0x000E8CC8
			internal GeneratedView GetGeneratedView(EntitySetBase extent, MetadataWorkspace workspace, StorageMappingItemCollection storageMappingItemCollection)
			{
				GeneratedView result;
				if (this.TryGetUserDefinedQueryView(extent, out result))
				{
					return result;
				}
				if (extent.BuiltInTypeKind == BuiltInTypeKind.AssociationSet)
				{
					AssociationSet aSet = (AssociationSet)extent;
					if (aSet.ElementType.IsForeignKey)
					{
						if (StorageMappingItemCollection.ViewDictionary.m_config.IsViewTracing)
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
						System.Data.Metadata.Edm.ReferentialConstraint rc = aSet.ElementType.ReferentialConstraints.Single<System.Data.Metadata.Edm.ReferentialConstraint>();
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
						return GeneratedView.CreateGeneratedViewForFKAssociationSet(aSet, aSet.ElementType, new DbQueryCommandTree(workspace, DataSpace.SSpace, dbExpression), storageMappingItemCollection, StorageMappingItemCollection.ViewDictionary.m_config);
					}
				}
				Dictionary<EntitySetBase, GeneratedView> dictionary = this.m_generatedViewsMemoizer.Evaluate(extent.EntityContainer);
				if (!dictionary.TryGetValue(extent, out result))
				{
					throw EntityUtil.InvalidOperation(Strings.Mapping_Views_For_Extent_Not_Generated((extent.EntityContainer.DataSpace == DataSpace.SSpace) ? "Table" : "EntitySet", extent.Name));
				}
				return result;
			}

			// Token: 0x06003FAA RID: 16298 RVA: 0x000EACF0 File Offset: 0x000E8EF0
			private void SerializedCollectViewsFromObjectCollection(MetadataWorkspace workspace, Dictionary<EntitySetBase, GeneratedView> extentMappingViews)
			{
				IList<Assembly> viewGenerationAssemblies = ObjectItemCollection.ViewGenerationAssemblies;
				if (viewGenerationAssemblies != null)
				{
					foreach (Assembly assembly in viewGenerationAssemblies)
					{
						object[] customAttributes = assembly.GetCustomAttributes(typeof(EntityViewGenerationAttribute), false);
						if (customAttributes != null && customAttributes.Length != 0)
						{
							foreach (EntityViewGenerationAttribute entityViewGenerationAttribute in customAttributes)
							{
								Type viewGenerationType = entityViewGenerationAttribute.ViewGenerationType;
								if (!viewGenerationType.IsSubclassOf(typeof(EntityViewContainer)))
								{
									throw EntityUtil.InvalidOperation(Strings.Generated_View_Type_Super_Class("Edm_EntityMappingGeneratedViews.ViewsForBaseEntitySets"));
								}
								EntityViewContainer entityViewContainer = Activator.CreateInstance(viewGenerationType) as EntityViewContainer;
								this.SerializedAddGeneratedViewsInEntityViewContainer(workspace, entityViewContainer, extentMappingViews);
							}
						}
					}
				}
			}

			// Token: 0x06003FAB RID: 16299 RVA: 0x000EADC4 File Offset: 0x000E8FC4
			private void SerializedAddGeneratedViewsInEntityViewContainer(MetadataWorkspace workspace, EntityViewContainer entityViewContainer, Dictionary<EntitySetBase, GeneratedView> extentMappingViews)
			{
				StorageEntityContainerMapping entityContainerMapping;
				if (!this.TryGetCorrespondingStorageEntityContainerMapping(entityViewContainer, workspace.GetItemCollection(DataSpace.CSSpace).GetItems<StorageEntityContainerMapping>(), out entityContainerMapping))
				{
					return;
				}
				if (!this.SerializedVerifyHashOverMmClosure(entityContainerMapping, entityViewContainer))
				{
					throw new MappingException(Strings.ViewGen_HashOnMappingClosure_Not_Matching(entityViewContainer.EdmEntityContainerName));
				}
				if (this.VerifyViewsHaveNotChanged(workspace, entityViewContainer))
				{
					this.SerializedAddGeneratedViews(workspace, entityViewContainer, extentMappingViews);
					return;
				}
				throw new InvalidOperationException(Strings.Generated_Views_Changed);
			}

			// Token: 0x06003FAC RID: 16300 RVA: 0x000EAE24 File Offset: 0x000E9024
			private bool TryGetCorrespondingStorageEntityContainerMapping(EntityViewContainer viewContainer, IEnumerable<StorageEntityContainerMapping> storageEntityContainerMappingList, out StorageEntityContainerMapping storageEntityContainerMapping)
			{
				storageEntityContainerMapping = null;
				foreach (StorageEntityContainerMapping storageEntityContainerMapping2 in storageEntityContainerMappingList)
				{
					if (storageEntityContainerMapping2.EdmEntityContainer.Name == viewContainer.EdmEntityContainerName && storageEntityContainerMapping2.StorageEntityContainer.Name == viewContainer.StoreEntityContainerName)
					{
						storageEntityContainerMapping = storageEntityContainerMapping2;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06003FAD RID: 16301 RVA: 0x000EAEA4 File Offset: 0x000E90A4
			private bool SerializedVerifyHashOverMmClosure(StorageEntityContainerMapping entityContainerMapping, EntityViewContainer entityViewContainer)
			{
				return MetadataMappingHasherVisitor.GetMappingClosureHash(this.m_storageMappingItemCollection.MappingVersion, entityContainerMapping) == entityViewContainer.HashOverMappingClosure;
			}

			// Token: 0x06003FAE RID: 16302 RVA: 0x000EAEC8 File Offset: 0x000E90C8
			private bool VerifyViewsHaveNotChanged(MetadataWorkspace workspace, EntityViewContainer viewContainer)
			{
				StorageMappingItemCollection storageMappingItemCollection = workspace.GetItemCollection(DataSpace.CSSpace) as StorageMappingItemCollection;
				string a = MetadataHelper.GenerateHashForAllExtentViewsContent(storageMappingItemCollection.MappingVersion, viewContainer.ExtentViews);
				string hashOverAllExtentViews = viewContainer.HashOverAllExtentViews;
				return !(a != hashOverAllExtentViews);
			}

			// Token: 0x06003FAF RID: 16303 RVA: 0x000EAF08 File Offset: 0x000E9108
			private void SerializedAddGeneratedViews(MetadataWorkspace workspace, EntityViewContainer viewContainer, Dictionary<EntitySetBase, GeneratedView> extentMappingViews)
			{
				foreach (KeyValuePair<string, string> keyValuePair in viewContainer.ExtentViews)
				{
					System.Data.Metadata.Edm.EntityContainer entityContainer = null;
					EntitySetBase entitySetBase = null;
					string key = keyValuePair.Key;
					int num = key.LastIndexOf('.');
					if (num != -1)
					{
						string identity = key.Substring(0, num);
						string identity2 = key.Substring(key.LastIndexOf('.') + 1);
						if (!workspace.TryGetItem<System.Data.Metadata.Edm.EntityContainer>(identity, DataSpace.CSpace, out entityContainer))
						{
							workspace.TryGetItem<System.Data.Metadata.Edm.EntityContainer>(identity, DataSpace.SSpace, out entityContainer);
						}
						if (entityContainer != null)
						{
							entityContainer.BaseEntitySets.TryGetValue(identity2, false, out entitySetBase);
						}
					}
					if (entitySetBase == null)
					{
						throw new MappingException(Strings.Generated_Views_Invalid_Extent(key));
					}
					GeneratedView value;
					if (!extentMappingViews.TryGetValue(entitySetBase, out value))
					{
						value = GeneratedView.CreateGeneratedView(entitySetBase, null, null, keyValuePair.Value, this.m_storageMappingItemCollection, new ConfigViewGenerator());
						extentMappingViews.Add(entitySetBase, value);
					}
				}
			}

			// Token: 0x06003FB0 RID: 16304 RVA: 0x000EB000 File Offset: 0x000E9200
			private void SerializedCollectViewsFromReferencedAssemblies(MetadataWorkspace workspace, Dictionary<EntitySetBase, GeneratedView> extentMappingViews)
			{
				ItemCollection itemCollection;
				if (!workspace.TryGetItemCollection(DataSpace.OSpace, out itemCollection))
				{
					ObjectItemCollection objectItemCollection = new ObjectItemCollection();
					itemCollection = objectItemCollection;
					Assembly entryAssembly = Assembly.GetEntryAssembly();
					if (entryAssembly != null)
					{
						objectItemCollection.ImplicitLoadViewsFromAllReferencedAssemblies(entryAssembly);
					}
				}
				this.SerializedCollectViewsFromObjectCollection(workspace, extentMappingViews);
			}

			// Token: 0x04001C56 RID: 7254
			private readonly StorageMappingItemCollection.TryGetUserDefinedQueryView TryGetUserDefinedQueryView;

			// Token: 0x04001C57 RID: 7255
			private readonly StorageMappingItemCollection.TryGetUserDefinedQueryViewOfType TryGetUserDefinedQueryViewOfType;

			// Token: 0x04001C58 RID: 7256
			private StorageMappingItemCollection m_storageMappingItemCollection;

			// Token: 0x04001C59 RID: 7257
			private static ConfigViewGenerator m_config = new ConfigViewGenerator();

			// Token: 0x04001C5A RID: 7258
			private List<Assembly> m_knownViewGenAssemblies = new List<Assembly>();

			// Token: 0x04001C5B RID: 7259
			private bool m_generatedViewsMode = true;

			// Token: 0x04001C5C RID: 7260
			private readonly Memoizer<System.Data.Metadata.Edm.EntityContainer, Dictionary<EntitySetBase, GeneratedView>> m_generatedViewsMemoizer;

			// Token: 0x04001C5D RID: 7261
			private readonly Memoizer<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView> m_generatedViewOfTypeMemoizer;
		}
	}
}
