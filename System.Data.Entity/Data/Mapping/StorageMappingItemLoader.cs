using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.EntityModel.SchemaObjectModel;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Data.Mapping
{
	// Token: 0x0200024D RID: 589
	internal class StorageMappingItemLoader
	{
		// Token: 0x060024B0 RID: 9392 RVA: 0x00085388 File Offset: 0x00083588
		internal StorageMappingItemLoader(XmlReader reader, StorageMappingItemCollection storageMappingItemCollection, string fileName, Dictionary<EdmMember, KeyValuePair<TypeUsage, TypeUsage>> scalarMemberMappings)
		{
			this.m_storageMappingItemCollection = storageMappingItemCollection;
			this.m_alias = new Dictionary<string, string>(StringComparer.Ordinal);
			if (fileName != null)
			{
				this.m_sourceLocation = fileName;
			}
			else
			{
				this.m_sourceLocation = null;
			}
			this.m_parsingErrors = new List<EdmSchemaError>();
			this.m_scalarMemberMappings = scalarMemberMappings;
			this.m_containerMapping = this.LoadMappingItems(reader);
			if (this.m_currentNamespaceUri != null)
			{
				if (this.m_currentNamespaceUri == "urn:schemas-microsoft-com:windows:storage:mapping:CS")
				{
					this.m_version = 1.0;
					return;
				}
				if (this.m_currentNamespaceUri == "http://schemas.microsoft.com/ado/2008/09/mapping/cs")
				{
					this.m_version = 2.0;
					return;
				}
				this.m_version = 3.0;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060024B1 RID: 9393 RVA: 0x00085440 File Offset: 0x00083640
		internal double MappingVersion
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x00085448 File Offset: 0x00083648
		internal IList<EdmSchemaError> ParsingErrors
		{
			get
			{
				return this.m_parsingErrors;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060024B3 RID: 9395 RVA: 0x00085450 File Offset: 0x00083650
		internal bool HasQueryViews
		{
			get
			{
				return this.m_hasQueryViews;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060024B4 RID: 9396 RVA: 0x00085458 File Offset: 0x00083658
		internal StorageEntityContainerMapping ContainerMapping
		{
			get
			{
				return this.m_containerMapping;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060024B5 RID: 9397 RVA: 0x00085460 File Offset: 0x00083660
		private EdmItemCollection EdmItemCollection
		{
			get
			{
				return this.m_storageMappingItemCollection.EdmItemCollection;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x0008546D File Offset: 0x0008366D
		private StoreItemCollection StoreItemCollection
		{
			get
			{
				return this.m_storageMappingItemCollection.StoreItemCollection;
			}
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x0008547C File Offset: 0x0008367C
		private StorageEntityContainerMapping LoadMappingItems(XmlReader innerReader)
		{
			XmlReader schemaValidatingReader = this.GetSchemaValidatingReader(innerReader);
			try
			{
				XPathDocument xpathDocument = new XPathDocument(schemaValidatingReader);
				if (this.m_parsingErrors.Count != 0 && !MetadataHelper.CheckIfAllErrorsAreWarnings(this.m_parsingErrors))
				{
					return null;
				}
				XPathNavigator xpathNavigator = xpathDocument.CreateNavigator();
				return this.LoadMappingItems(xpathNavigator.Clone());
			}
			catch (XmlException ex)
			{
				EdmSchemaError item = new EdmSchemaError(Strings.Mapping_InvalidMappingSchema_Parsing(ex.Message), 2024, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, ex.LineNumber, ex.LinePosition);
				this.m_parsingErrors.Add(item);
			}
			return null;
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x00085520 File Offset: 0x00083720
		private StorageEntityContainerMapping LoadMappingItems(XPathNavigator nav)
		{
			if (!this.MoveToRootElement(nav) || nav.NodeType != XPathNodeType.Element)
			{
				StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_Invalid_CSRootElementMissing("urn:schemas-microsoft-com:windows:storage:mapping:CS", "http://schemas.microsoft.com/ado/2008/09/mapping/cs", "http://schemas.microsoft.com/ado/2009/11/mapping/cs"), StorageMappingErrorCode.RootMappingElementMissing, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
				return null;
			}
			StorageEntityContainerMapping result = this.LoadMappingChildNodes(nav.Clone());
			if (this.m_parsingErrors.Count != 0 && !MetadataHelper.CheckIfAllErrorsAreWarnings(this.m_parsingErrors))
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x0008559C File Offset: 0x0008379C
		private bool MoveToRootElement(XPathNavigator nav)
		{
			if (nav.MoveToChild("Mapping", "http://schemas.microsoft.com/ado/2009/11/mapping/cs"))
			{
				this.m_currentNamespaceUri = "http://schemas.microsoft.com/ado/2009/11/mapping/cs";
				return true;
			}
			if (nav.MoveToChild("Mapping", "http://schemas.microsoft.com/ado/2008/09/mapping/cs"))
			{
				this.m_currentNamespaceUri = "http://schemas.microsoft.com/ado/2008/09/mapping/cs";
				return true;
			}
			if (nav.MoveToChild("Mapping", "urn:schemas-microsoft-com:windows:storage:mapping:CS"))
			{
				this.m_currentNamespaceUri = "urn:schemas-microsoft-com:windows:storage:mapping:CS";
				return true;
			}
			return false;
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x00085608 File Offset: 0x00083808
		private StorageEntityContainerMapping LoadMappingChildNodes(XPathNavigator nav)
		{
			bool flag;
			if (nav.MoveToChild("Alias", this.m_currentNamespaceUri))
			{
				do
				{
					this.m_alias.Add(StorageMappingItemLoader.GetAttributeValue(nav.Clone(), "Key"), StorageMappingItemLoader.GetAttributeValue(nav.Clone(), "Value"));
				}
				while (nav.MoveToNext("Alias", this.m_currentNamespaceUri));
				flag = nav.MoveToNext(XPathNodeType.Element);
			}
			else
			{
				flag = nav.MoveToChild(XPathNodeType.Element);
			}
			return flag ? this.LoadEntityContainerMapping(nav.Clone()) : null;
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x0008568C File Offset: 0x0008388C
		private StorageEntityContainerMapping LoadEntityContainerMapping(XPathNavigator nav)
		{
			IXmlLineInfo xmlLineInfo = (IXmlLineInfo)nav;
			string attributeValue = StorageMappingItemLoader.GetAttributeValue(nav.Clone(), "CdmEntityContainer");
			string attributeValue2 = StorageMappingItemLoader.GetAttributeValue(nav.Clone(), "StorageEntityContainer");
			bool boolAttributeValue = this.GetBoolAttributeValue(nav.Clone(), "GenerateUpdateViews", true);
			StorageEntityContainerMapping storageEntityContainerMapping;
			System.Data.Metadata.Edm.EntityContainer storageEntityContainer;
			if (this.m_storageMappingItemCollection.TryGetItem<StorageEntityContainerMapping>(attributeValue, out storageEntityContainerMapping))
			{
				System.Data.Metadata.Edm.EntityContainer edmEntityContainer = storageEntityContainerMapping.EdmEntityContainer;
				storageEntityContainer = storageEntityContainerMapping.StorageEntityContainer;
				if (attributeValue2 != storageEntityContainer.Name)
				{
					StorageMappingItemLoader.AddToSchemaErrors(Strings.StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping(attributeValue2, storageEntityContainer.Name, edmEntityContainer.Name), StorageMappingErrorCode.StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					return null;
				}
			}
			else
			{
				if (this.m_storageMappingItemCollection.ContainsStorageEntityContainer(attributeValue2))
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_AlreadyMapped_StorageEntityContainer), attributeValue2, StorageMappingErrorCode.AlreadyMappedStorageEntityContainer, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					return null;
				}
				System.Data.Metadata.Edm.EntityContainer edmEntityContainer;
				this.EdmItemCollection.TryGetEntityContainer(attributeValue, out edmEntityContainer);
				if (edmEntityContainer == null)
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_EntityContainer), attributeValue, StorageMappingErrorCode.InvalidEntityContainer, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
				}
				this.StoreItemCollection.TryGetEntityContainer(attributeValue2, out storageEntityContainer);
				if (storageEntityContainer == null)
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_StorageEntityContainer), attributeValue2, StorageMappingErrorCode.InvalidEntityContainer, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
				}
				if (edmEntityContainer == null || storageEntityContainer == null)
				{
					return null;
				}
				storageEntityContainerMapping = new StorageEntityContainerMapping(edmEntityContainer, storageEntityContainer, this.m_storageMappingItemCollection, boolAttributeValue, boolAttributeValue);
				storageEntityContainerMapping.StartLineNumber = xmlLineInfo.LineNumber;
				storageEntityContainerMapping.StartLinePosition = xmlLineInfo.LinePosition;
			}
			this.LoadEntityContainerMappingChildNodes(nav.Clone(), storageEntityContainerMapping, storageEntityContainer);
			return storageEntityContainerMapping;
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x00085824 File Offset: 0x00083A24
		private void LoadEntityContainerMappingChildNodes(XPathNavigator nav, StorageEntityContainerMapping entityContainerMapping, System.Data.Metadata.Edm.EntityContainer storageEntityContainerType)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			bool flag = false;
			if (nav.MoveToChild(XPathNodeType.Element))
			{
				do
				{
					string localName = nav.LocalName;
					if (!(localName == "EntitySetMapping"))
					{
						if (!(localName == "AssociationSetMapping"))
						{
							if (!(localName == "FunctionImportMapping"))
							{
								StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_Container_SubElement, StorageMappingErrorCode.SetMappingExpected, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
							}
							else
							{
								this.LoadFunctionImportMapping(nav.Clone(), entityContainerMapping, storageEntityContainerType);
							}
						}
						else
						{
							this.LoadAssociationSetMapping(nav.Clone(), entityContainerMapping, storageEntityContainerType);
						}
					}
					else
					{
						this.LoadEntitySetMapping(nav.Clone(), entityContainerMapping, storageEntityContainerType);
						flag = true;
					}
				}
				while (nav.MoveToNext(XPathNodeType.Element));
			}
			if (entityContainerMapping.EdmEntityContainer.BaseEntitySets.Count != 0 && !flag)
			{
				StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.ViewGen_Missing_Sets_Mapping), entityContainerMapping.EdmEntityContainer.Name, StorageMappingErrorCode.EmptyContainerMapping, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return;
			}
			this.ValidateFunctionAssociationFunctionMappingUnique(nav.Clone(), entityContainerMapping);
			this.ValidateModificationFunctionMappingConsistentForAssociations(nav.Clone(), entityContainerMapping);
			this.ValidateQueryViewsClosure(nav.Clone(), entityContainerMapping);
			this.ValidateEntitySetFunctionMappingClosure(nav.Clone(), entityContainerMapping);
			entityContainerMapping.SourceLocation = this.m_sourceLocation;
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x00085958 File Offset: 0x00083B58
		private void ValidateModificationFunctionMappingConsistentForAssociations(XPathNavigator nav, StorageEntityContainerMapping entityContainerMapping)
		{
			foreach (StorageSetMapping storageSetMapping in entityContainerMapping.EntitySetMaps)
			{
				StorageEntitySetMapping storageEntitySetMapping = (StorageEntitySetMapping)storageSetMapping;
				if (storageEntitySetMapping.ModificationFunctionMappings.Count > 0)
				{
					Set<AssociationSetEnd> expectedEnds = new Set<AssociationSetEnd>(storageEntitySetMapping.ImplicitlyMappedAssociationSetEnds).MakeReadOnly();
					foreach (StorageEntityTypeModificationFunctionMapping storageEntityTypeModificationFunctionMapping in storageEntitySetMapping.ModificationFunctionMappings)
					{
						if (storageEntityTypeModificationFunctionMapping.DeleteFunctionMapping != null)
						{
							this.ValidateModificationFunctionMappingConsistentForAssociations(nav, storageEntitySetMapping, storageEntityTypeModificationFunctionMapping, storageEntityTypeModificationFunctionMapping.DeleteFunctionMapping, expectedEnds, "DeleteFunction");
						}
						if (storageEntityTypeModificationFunctionMapping.InsertFunctionMapping != null)
						{
							this.ValidateModificationFunctionMappingConsistentForAssociations(nav, storageEntitySetMapping, storageEntityTypeModificationFunctionMapping, storageEntityTypeModificationFunctionMapping.InsertFunctionMapping, expectedEnds, "InsertFunction");
						}
						if (storageEntityTypeModificationFunctionMapping.UpdateFunctionMapping != null)
						{
							this.ValidateModificationFunctionMappingConsistentForAssociations(nav, storageEntitySetMapping, storageEntityTypeModificationFunctionMapping, storageEntityTypeModificationFunctionMapping.UpdateFunctionMapping, expectedEnds, "UpdateFunction");
						}
					}
				}
			}
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x00085A64 File Offset: 0x00083C64
		private void ValidateModificationFunctionMappingConsistentForAssociations(XPathNavigator nav, StorageEntitySetMapping entitySetMapping, StorageEntityTypeModificationFunctionMapping entityTypeMapping, StorageModificationFunctionMapping functionMapping, Set<AssociationSetEnd> expectedEnds, string elementName)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			Set<AssociationSetEnd> set = new Set<AssociationSetEnd>(functionMapping.CollocatedAssociationSetEnds);
			set.MakeReadOnly();
			foreach (AssociationSetEnd associationSetEnd in expectedEnds)
			{
				if (MetadataHelper.IsAssociationValidForEntityType(associationSetEnd, entityTypeMapping.EntityType) && !set.Contains(associationSetEnd))
				{
					StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_ModificationFunction_AssociationSetNotMappedForOperation(entitySetMapping.Set.Name, associationSetEnd.ParentAssociationSet.Name, elementName, entityTypeMapping.EntityType.FullName), StorageMappingErrorCode.InvalidModificationFunctionMappingAssociationSetNotMappedForOperation, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				}
			}
			foreach (AssociationSetEnd associationSetEnd2 in set)
			{
				if (!MetadataHelper.IsAssociationValidForEntityType(associationSetEnd2, entityTypeMapping.EntityType))
				{
					StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_ModificationFunction_AssociationEndMappingInvalidForEntityType(entityTypeMapping.EntityType.FullName, associationSetEnd2.ParentAssociationSet.Name, MetadataHelper.GetEntityTypeForEnd(MetadataHelper.GetOppositeEnd(associationSetEnd2).CorrespondingAssociationEndMember).FullName), StorageMappingErrorCode.InvalidModificationFunctionMappingAssociationEndMappingInvalidForEntityType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				}
			}
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x00085BB0 File Offset: 0x00083DB0
		private void ValidateFunctionAssociationFunctionMappingUnique(XPathNavigator nav, StorageEntityContainerMapping entityContainerMapping)
		{
			Dictionary<EntitySetBase, int> dictionary = new Dictionary<EntitySetBase, int>();
			foreach (StorageSetMapping storageSetMapping in entityContainerMapping.EntitySetMaps)
			{
				StorageEntitySetMapping storageEntitySetMapping = (StorageEntitySetMapping)storageSetMapping;
				if (storageEntitySetMapping.ModificationFunctionMappings.Count > 0)
				{
					Set<EntitySetBase> set = new Set<EntitySetBase>();
					foreach (AssociationSetEnd associationSetEnd in storageEntitySetMapping.ImplicitlyMappedAssociationSetEnds)
					{
						set.Add(associationSetEnd.ParentAssociationSet);
					}
					foreach (EntitySetBase key in set)
					{
						StorageMappingItemLoader.IncrementCount<EntitySetBase>(dictionary, key);
					}
				}
			}
			foreach (StorageSetMapping storageSetMapping2 in entityContainerMapping.RelationshipSetMaps)
			{
				StorageAssociationSetMapping storageAssociationSetMapping = (StorageAssociationSetMapping)storageSetMapping2;
				if (storageAssociationSetMapping.ModificationFunctionMapping != null)
				{
					StorageMappingItemLoader.IncrementCount<EntitySetBase>(dictionary, storageAssociationSetMapping.Set);
				}
			}
			List<string> list = new List<string>();
			foreach (KeyValuePair<EntitySetBase, int> keyValuePair in dictionary)
			{
				if (keyValuePair.Value > 1)
				{
					list.Add(keyValuePair.Key.Name);
				}
			}
			if (0 < list.Count)
			{
				StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AssociationSetAmbiguous), StringUtil.ToCommaSeparatedString(list), StorageMappingErrorCode.AmbiguousModificationFunctionMappingForAssociationSet, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
			}
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x00085D90 File Offset: 0x00083F90
		private static void IncrementCount<T>(Dictionary<T, int> counts, T key)
		{
			int num;
			if (counts.TryGetValue(key, out num))
			{
				num++;
			}
			else
			{
				num = 1;
			}
			counts[key] = num;
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x00085DB8 File Offset: 0x00083FB8
		private void ValidateEntitySetFunctionMappingClosure(XPathNavigator nav, StorageEntityContainerMapping entityContainerMapping)
		{
			KeyToListMap<EntitySet, StorageSetMapping> keyToListMap = new KeyToListMap<EntitySet, StorageSetMapping>(EqualityComparer<EntitySet>.Default);
			foreach (StorageSetMapping storageSetMapping in entityContainerMapping.AllSetMaps)
			{
				foreach (StorageTypeMapping storageTypeMapping in storageSetMapping.TypeMappings)
				{
					foreach (StorageMappingFragment storageMappingFragment in storageTypeMapping.MappingFragments)
					{
						keyToListMap.Add(storageMappingFragment.TableSet, storageSetMapping);
					}
				}
			}
			Set<EntitySetBase> implicitMappedAssociationSets = new Set<EntitySetBase>();
			foreach (StorageSetMapping storageSetMapping2 in entityContainerMapping.EntitySetMaps)
			{
				StorageEntitySetMapping storageEntitySetMapping = (StorageEntitySetMapping)storageSetMapping2;
				if (storageEntitySetMapping.ModificationFunctionMappings.Count > 0)
				{
					foreach (AssociationSetEnd associationSetEnd in storageEntitySetMapping.ImplicitlyMappedAssociationSetEnds)
					{
						implicitMappedAssociationSets.Add(associationSetEnd.ParentAssociationSet);
					}
				}
			}
			Func<StorageSetMapping, bool> <>9__0;
			Func<StorageSetMapping, bool> <>9__1;
			foreach (EntitySet key in keyToListMap.Keys)
			{
				IEnumerable<StorageSetMapping> source = keyToListMap.ListForKey(key);
				Func<StorageSetMapping, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((StorageSetMapping s) => s.HasModificationFunctionMapping || implicitMappedAssociationSets.Any((EntitySetBase aset) => aset == s.Set)));
				}
				if (source.Any(predicate))
				{
					IEnumerable<StorageSetMapping> source2 = keyToListMap.ListForKey(key);
					Func<StorageSetMapping, bool> predicate2;
					if ((predicate2 = <>9__1) == null)
					{
						predicate2 = (<>9__1 = ((StorageSetMapping s) => !s.HasModificationFunctionMapping && !implicitMappedAssociationSets.Any((EntitySetBase aset) => aset == s.Set)));
					}
					if (source2.Any(predicate2))
					{
						StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_MissingSetClosure), StringUtil.ToCommaSeparatedString(from s in keyToListMap.ListForKey(key)
						where !s.HasModificationFunctionMapping
						select s.Set.Name), StorageMappingErrorCode.MissingSetClosureInModificationFunctionMapping, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
					}
				}
			}
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x00086060 File Offset: 0x00084260
		private static void ValidateClosureAmongSets(StorageEntityContainerMapping entityContainerMapping, Set<EntitySetBase> sets, Set<EntitySetBase> additionalSetsInClosure)
		{
			bool flag;
			do
			{
				flag = false;
				List<EntitySetBase> list = new List<EntitySetBase>();
				foreach (EntitySetBase entitySetBase in additionalSetsInClosure)
				{
					AssociationSet associationSet = entitySetBase as AssociationSet;
					if (associationSet != null && !associationSet.ElementType.IsForeignKey)
					{
						foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
						{
							if (!additionalSetsInClosure.Contains(associationSetEnd.EntitySet))
							{
								list.Add(associationSetEnd.EntitySet);
							}
						}
					}
				}
				foreach (EntitySetBase entitySetBase2 in entityContainerMapping.EdmEntityContainer.BaseEntitySets)
				{
					AssociationSet associationSet2 = entitySetBase2 as AssociationSet;
					if (associationSet2 != null && !associationSet2.ElementType.IsForeignKey && !additionalSetsInClosure.Contains(associationSet2))
					{
						foreach (AssociationSetEnd associationSetEnd2 in associationSet2.AssociationSetEnds)
						{
							if (additionalSetsInClosure.Contains(associationSetEnd2.EntitySet))
							{
								list.Add(associationSet2);
								break;
							}
						}
					}
				}
				if (0 < list.Count)
				{
					flag = true;
					additionalSetsInClosure.AddRange(list);
				}
			}
			while (flag);
			additionalSetsInClosure.Subtract(sets);
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x00086204 File Offset: 0x00084404
		private void ValidateQueryViewsClosure(XPathNavigator nav, StorageEntityContainerMapping entityContainerMapping)
		{
			if (!this.m_hasQueryViews)
			{
				return;
			}
			Set<EntitySetBase> set = new Set<EntitySetBase>();
			Set<EntitySetBase> set2 = new Set<EntitySetBase>();
			foreach (StorageSetMapping storageSetMapping in entityContainerMapping.AllSetMaps)
			{
				if (storageSetMapping.QueryView != null)
				{
					set.Add(storageSetMapping.Set);
				}
			}
			set2.AddRange(set);
			StorageMappingItemLoader.ValidateClosureAmongSets(entityContainerMapping, set, set2);
			if (0 < set2.Count)
			{
				StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_Invalid_Query_Views_MissingSetClosure), StringUtil.ToCommaSeparatedString(set2), StorageMappingErrorCode.MissingSetClosureInQueryViews, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
			}
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000862BC File Offset: 0x000844BC
		private void LoadEntitySetMapping(XPathNavigator nav, StorageEntityContainerMapping entityContainerMapping, System.Data.Metadata.Edm.EntityContainer storageEntityContainerType)
		{
			string aliasResolvedAttributeValue = this.GetAliasResolvedAttributeValue(nav.Clone(), "Name");
			string attributeValue = StorageMappingItemLoader.GetAttributeValue(nav.Clone(), "TypeName");
			string aliasResolvedAttributeValue2 = this.GetAliasResolvedAttributeValue(nav.Clone(), "StoreEntitySet");
			bool boolAttributeValue = this.GetBoolAttributeValue(nav.Clone(), "MakeColumnsDistinct", false);
			StorageEntitySetMapping storageEntitySetMapping = (StorageEntitySetMapping)entityContainerMapping.GetEntitySetMapping(aliasResolvedAttributeValue);
			IXmlLineInfo xmlLineInfo = (IXmlLineInfo)nav;
			EntitySet entitySet;
			if (storageEntitySetMapping == null)
			{
				if (!entityContainerMapping.EdmEntityContainer.TryGetEntitySetByName(aliasResolvedAttributeValue, false, out entitySet))
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Entity_Set), aliasResolvedAttributeValue, StorageMappingErrorCode.InvalidEntitySet, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					return;
				}
				storageEntitySetMapping = new StorageEntitySetMapping(entitySet, entityContainerMapping);
			}
			else
			{
				entitySet = (EntitySet)storageEntitySetMapping.Set;
			}
			storageEntitySetMapping.StartLineNumber = xmlLineInfo.LineNumber;
			storageEntitySetMapping.StartLinePosition = xmlLineInfo.LinePosition;
			entityContainerMapping.AddEntitySetMapping(storageEntitySetMapping);
			if (string.IsNullOrEmpty(attributeValue))
			{
				if (nav.MoveToChild(XPathNodeType.Element))
				{
					for (;;)
					{
						string localName = nav.LocalName;
						if (!(localName == "EntityTypeMapping"))
						{
							if (!(localName == "QueryView"))
							{
								StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_TypeMapping_QueryView, StorageMappingErrorCode.InvalidContent, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
							}
							else
							{
								if (!string.IsNullOrEmpty(aliasResolvedAttributeValue2))
								{
									break;
								}
								if (!this.LoadQueryView(nav.Clone(), storageEntitySetMapping))
								{
									return;
								}
							}
						}
						else
						{
							aliasResolvedAttributeValue2 = this.GetAliasResolvedAttributeValue(nav.Clone(), "StoreEntitySet");
							this.LoadEntityTypeMapping(nav.Clone(), storageEntitySetMapping, aliasResolvedAttributeValue2, storageEntityContainerType, false, entityContainerMapping.GenerateUpdateViews);
						}
						if (!nav.MoveToNext(XPathNodeType.Element))
						{
							goto Block_8;
						}
					}
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_TableName_QueryView), aliasResolvedAttributeValue, StorageMappingErrorCode.TableNameAttributeWithQueryView, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					return;
					Block_8:;
				}
			}
			else
			{
				this.LoadEntityTypeMapping(nav.Clone(), storageEntitySetMapping, aliasResolvedAttributeValue2, storageEntityContainerType, boolAttributeValue, entityContainerMapping.GenerateUpdateViews);
			}
			this.ValidateAllEntityTypesHaveFunctionMapping(nav.Clone(), storageEntitySetMapping);
			if (storageEntitySetMapping.HasNoContent)
			{
				StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Emtpty_SetMap), entitySet.Name, StorageMappingErrorCode.EmptySetMapping, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
			}
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000864D0 File Offset: 0x000846D0
		private void ValidateAllEntityTypesHaveFunctionMapping(XPathNavigator nav, StorageEntitySetMapping setMapping)
		{
			Set<EdmType> set = new Set<EdmType>();
			foreach (StorageEntityTypeModificationFunctionMapping storageEntityTypeModificationFunctionMapping in setMapping.ModificationFunctionMappings)
			{
				set.Add(storageEntityTypeModificationFunctionMapping.EntityType);
			}
			if (0 < set.Count)
			{
				Set<EdmType> set2 = new Set<EdmType>(MetadataHelper.GetTypeAndSubtypesOf(setMapping.Set.ElementType, this.EdmItemCollection, false));
				set2.Subtract(set);
				Set<EdmType> set3 = new Set<EdmType>();
				foreach (EdmType edmType in set2)
				{
					EntityType entityType = (EntityType)edmType;
					if (entityType.Abstract)
					{
						set3.Add(entityType);
					}
				}
				set2.Subtract(set3);
				if (0 < set2.Count)
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_MissingEntityType), StringUtil.ToCommaSeparatedString(set2), StorageMappingErrorCode.MissingModificationFunctionMappingForEntityType, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
				}
			}
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000865F0 File Offset: 0x000847F0
		private bool TryParseEntityTypeAttribute(XPathNavigator nav, EntityType rootEntityType, Func<EntityType, string> typeNotAssignableMessage, out Set<EntityType> isOfTypeEntityTypes, out Set<EntityType> entityTypes)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			string attributeValue = StorageMappingItemLoader.GetAttributeValue(nav.Clone(), "TypeName");
			isOfTypeEntityTypes = new Set<EntityType>();
			entityTypes = new Set<EntityType>();
			IEnumerable<string> enumerable = from s in attributeValue.Split(new char[]
			{
				';'
			})
			select s.Trim();
			foreach (string text in enumerable)
			{
				bool flag = text.StartsWith("IsTypeOf(", StringComparison.Ordinal);
				string text2;
				if (flag)
				{
					if (!text.EndsWith(")", StringComparison.Ordinal))
					{
						StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_InvalidContent_IsTypeOfNotTerminated, StorageMappingErrorCode.InvalidEntityType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
						return false;
					}
					text2 = text.Substring("IsTypeOf(".Length);
					text2 = text2.Substring(0, text2.Length - ")".Length).Trim();
				}
				else
				{
					text2 = text;
				}
				text2 = this.GetAliasResolvedValue(text2);
				EntityType entityType;
				if (!this.EdmItemCollection.TryGetItem<EntityType>(text2, out entityType))
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Entity_Type), text2, StorageMappingErrorCode.InvalidEntityType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					return false;
				}
				if (!Helper.IsAssignableFrom(rootEntityType, entityType))
				{
					StorageMappingItemLoader.AddToSchemaErrorWithMessage(typeNotAssignableMessage(entityType), StorageMappingErrorCode.InvalidEntityType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					return false;
				}
				if (entityType.Abstract)
				{
					if (!flag)
					{
						StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_AbstractEntity_Type), entityType.FullName, StorageMappingErrorCode.MappingOfAbstractType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
						return false;
					}
					IEnumerable<EdmType> typeAndSubtypesOf = MetadataHelper.GetTypeAndSubtypesOf(entityType, this.EdmItemCollection, false);
					if (!typeAndSubtypesOf.GetEnumerator().MoveNext())
					{
						StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_AbstractEntity_IsOfType), entityType.FullName, StorageMappingErrorCode.MappingOfAbstractType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
						return false;
					}
				}
				if (flag)
				{
					isOfTypeEntityTypes.Add(entityType);
				}
				else
				{
					entityTypes.Add(entityType);
				}
			}
			return true;
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x0008684C File Offset: 0x00084A4C
		private void LoadEntityTypeMapping(XPathNavigator nav, StorageEntitySetMapping entitySetMapping, string tableName, System.Data.Metadata.Edm.EntityContainer storageEntityContainerType, bool distinctFlagAboveType, bool generateUpdateViews)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			StorageEntityTypeMapping storageEntityTypeMapping = new StorageEntityTypeMapping(entitySetMapping);
			EntityType rootEntityType = (EntityType)entitySetMapping.Set.ElementType;
			Set<EntityType> set;
			Set<EntityType> set2;
			if (!this.TryParseEntityTypeAttribute(nav.Clone(), rootEntityType, (EntityType e) => Strings.Mapping_InvalidContent_Entity_Type_For_Entity_Set(e.FullName, rootEntityType.FullName, entitySetMapping.Set.Name), out set, out set2))
			{
				return;
			}
			foreach (EntityType type in set2)
			{
				storageEntityTypeMapping.AddType(type);
			}
			foreach (EntityType type2 in set)
			{
				storageEntityTypeMapping.AddIsOfType(type2);
			}
			if (string.IsNullOrEmpty(tableName))
			{
				if (!nav.MoveToChild(XPathNodeType.Element))
				{
					return;
				}
				do
				{
					if (nav.LocalName == "ModificationFunctionMapping")
					{
						entitySetMapping.HasModificationFunctionMapping = true;
						this.LoadEntityTypeModificationFunctionMapping(nav.Clone(), entitySetMapping, storageEntityTypeMapping);
					}
					else if (nav.LocalName != "MappingFragment")
					{
						StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_Table_Expected, StorageMappingErrorCode.TableMappingFragmentExpected, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					}
					else
					{
						bool boolAttributeValue = this.GetBoolAttributeValue(nav.Clone(), "MakeColumnsDistinct", false);
						if (generateUpdateViews && boolAttributeValue)
						{
							StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_DistinctFlagInReadWriteContainer, StorageMappingErrorCode.DistinctFragmentInReadWriteContainer, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
						}
						tableName = this.GetAliasResolvedAttributeValue(nav.Clone(), "StoreEntitySet");
						StorageMappingFragment storageMappingFragment = this.LoadMappingFragment(nav.Clone(), storageEntityTypeMapping, tableName, storageEntityContainerType, boolAttributeValue);
						if (storageMappingFragment != null)
						{
							storageEntityTypeMapping.AddFragment(storageMappingFragment);
						}
					}
				}
				while (nav.MoveToNext(XPathNodeType.Element));
			}
			else
			{
				if (nav.LocalName == "ModificationFunctionMapping")
				{
					StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_ModificationFunction_In_Table_Context, StorageMappingErrorCode.InvalidTableNameAttributeWithModificationFunctionMapping, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				}
				if (generateUpdateViews && distinctFlagAboveType)
				{
					StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_DistinctFlagInReadWriteContainer, StorageMappingErrorCode.DistinctFragmentInReadWriteContainer, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				}
				StorageMappingFragment storageMappingFragment2 = this.LoadMappingFragment(nav.Clone(), storageEntityTypeMapping, tableName, storageEntityContainerType, distinctFlagAboveType);
				if (storageMappingFragment2 != null)
				{
					storageEntityTypeMapping.AddFragment(storageMappingFragment2);
				}
			}
			entitySetMapping.AddTypeMapping(storageEntityTypeMapping);
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x00086AB0 File Offset: 0x00084CB0
		private void LoadEntityTypeModificationFunctionMapping(XPathNavigator nav, StorageEntitySetMapping entitySetMapping, StorageEntityTypeMapping entityTypeMapping)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			if (entityTypeMapping.IsOfTypes.Count != 0 || entityTypeMapping.Types.Count != 1)
			{
				StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_ModificationFunction_Multiple_Types, StorageMappingErrorCode.InvalidModificationFunctionMappingForMultipleTypes, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return;
			}
			EntityType entityType = (EntityType)entityTypeMapping.Types[0];
			if (entityType.Abstract)
			{
				StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_AbstractEntity_FunctionMapping), entityType.FullName, StorageMappingErrorCode.MappingOfAbstractType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return;
			}
			foreach (StorageEntityTypeModificationFunctionMapping storageEntityTypeModificationFunctionMapping in entitySetMapping.ModificationFunctionMappings)
			{
				if (storageEntityTypeModificationFunctionMapping.EntityType.Equals(entityType))
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_RedundantEntityTypeMapping), entityType.Name, StorageMappingErrorCode.RedundantEntityTypeMappingInModificationFunctionMapping, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					return;
				}
			}
			StorageMappingItemLoader.ModificationFunctionMappingLoader modificationFunctionMappingLoader = new StorageMappingItemLoader.ModificationFunctionMappingLoader(this, entitySetMapping.Set);
			StorageModificationFunctionMapping storageModificationFunctionMapping = null;
			StorageModificationFunctionMapping storageModificationFunctionMapping2 = null;
			StorageModificationFunctionMapping storageModificationFunctionMapping3 = null;
			if (nav.MoveToChild(XPathNodeType.Element))
			{
				do
				{
					string localName = nav.LocalName;
					if (!(localName == "DeleteFunction"))
					{
						if (!(localName == "InsertFunction"))
						{
							if (localName == "UpdateFunction")
							{
								storageModificationFunctionMapping3 = modificationFunctionMappingLoader.LoadEntityTypeModificationFunctionMapping(nav.Clone(), entitySetMapping.Set, true, true, entityType);
							}
						}
						else
						{
							storageModificationFunctionMapping2 = modificationFunctionMappingLoader.LoadEntityTypeModificationFunctionMapping(nav.Clone(), entitySetMapping.Set, true, false, entityType);
						}
					}
					else
					{
						storageModificationFunctionMapping = modificationFunctionMappingLoader.LoadEntityTypeModificationFunctionMapping(nav.Clone(), entitySetMapping.Set, false, true, entityType);
					}
				}
				while (nav.MoveToNext(XPathNodeType.Element));
			}
			IEnumerable<StorageModificationFunctionParameterBinding> enumerable = new List<StorageModificationFunctionParameterBinding>();
			if (storageModificationFunctionMapping != null)
			{
				enumerable = Helper.Concat<StorageModificationFunctionParameterBinding>(new IEnumerable<StorageModificationFunctionParameterBinding>[]
				{
					enumerable,
					storageModificationFunctionMapping.ParameterBindings
				});
			}
			if (storageModificationFunctionMapping2 != null)
			{
				enumerable = Helper.Concat<StorageModificationFunctionParameterBinding>(new IEnumerable<StorageModificationFunctionParameterBinding>[]
				{
					enumerable,
					storageModificationFunctionMapping2.ParameterBindings
				});
			}
			if (storageModificationFunctionMapping3 != null)
			{
				enumerable = Helper.Concat<StorageModificationFunctionParameterBinding>(new IEnumerable<StorageModificationFunctionParameterBinding>[]
				{
					enumerable,
					storageModificationFunctionMapping3.ParameterBindings
				});
			}
			Dictionary<AssociationSet, AssociationEndMember> dictionary = new Dictionary<AssociationSet, AssociationEndMember>();
			foreach (StorageModificationFunctionParameterBinding storageModificationFunctionParameterBinding in enumerable)
			{
				if (storageModificationFunctionParameterBinding.MemberPath.AssociationSetEnd != null)
				{
					AssociationSet parentAssociationSet = storageModificationFunctionParameterBinding.MemberPath.AssociationSetEnd.ParentAssociationSet;
					AssociationEndMember correspondingAssociationEndMember = storageModificationFunctionParameterBinding.MemberPath.AssociationSetEnd.CorrespondingAssociationEndMember;
					AssociationEndMember associationEndMember;
					if (dictionary.TryGetValue(parentAssociationSet, out associationEndMember) && associationEndMember != correspondingAssociationEndMember)
					{
						StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_ModificationFunction_MultipleEndsOfAssociationMapped(correspondingAssociationEndMember.Name, associationEndMember.Name, parentAssociationSet.Name), StorageMappingErrorCode.InvalidModificationFunctionMappingMultipleEndsOfAssociationMapped, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
						return;
					}
					dictionary[parentAssociationSet] = correspondingAssociationEndMember;
				}
			}
			StorageEntityTypeModificationFunctionMapping modificationFunctionMapping = new StorageEntityTypeModificationFunctionMapping(entityType, storageModificationFunctionMapping, storageModificationFunctionMapping2, storageModificationFunctionMapping3);
			entitySetMapping.AddModificationFunctionMapping(modificationFunctionMapping);
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x00086DAC File Offset: 0x00084FAC
		private bool LoadQueryView(XPathNavigator nav, StorageSetMapping setMapping)
		{
			string value = nav.Value;
			string text = StorageMappingItemLoader.GetAttributeValue(nav.Clone(), "TypeName");
			if (text != null)
			{
				text = text.Trim();
			}
			if (setMapping.QueryView == null)
			{
				if (text != null)
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo((object val) => Strings.Mapping_TypeName_For_First_QueryView, setMapping.Set.Name, StorageMappingErrorCode.TypeNameForFirstQueryView, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
					return false;
				}
				if (string.IsNullOrEmpty(value))
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_Empty_QueryView), setMapping.Set.Name, StorageMappingErrorCode.EmptyQueryView, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
					return false;
				}
				setMapping.QueryView = value;
				this.m_hasQueryViews = true;
				return true;
			}
			else
			{
				if (text == null || text.Trim().Length == 0)
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_QueryView_TypeName_Not_Defined), setMapping.Set.Name, StorageMappingErrorCode.NoTypeNameForTypeSpecificQueryView, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
					return false;
				}
				EntityType rootEntityType = (EntityType)setMapping.Set.ElementType;
				Set<EntityType> set;
				Set<EntityType> set2;
				if (!this.TryParseEntityTypeAttribute(nav.Clone(), rootEntityType, (EntityType e) => Strings.Mapping_InvalidContent_Entity_Type_For_Entity_Set(e.FullName, rootEntityType.FullName, setMapping.Set.Name), out set, out set2))
				{
					return false;
				}
				EntityType entityType;
				bool flag;
				if (set.Count == 1)
				{
					entityType = set.First<EntityType>();
					flag = true;
				}
				else
				{
					if (set2.Count != 1)
					{
						StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_QueryViewMultipleTypeInTypeName), setMapping.Set.ToString(), StorageMappingErrorCode.TypeNameContainsMultipleTypesForQueryView, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
						return false;
					}
					entityType = set2.First<EntityType>();
					flag = false;
				}
				if (flag && setMapping.Set.ElementType.EdmEquals(entityType))
				{
					StorageMappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_QueryView_For_Base_Type), entityType.ToString(), setMapping.Set.ToString(), StorageMappingErrorCode.IsTypeOfQueryViewForBaseType, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
					return false;
				}
				if (string.IsNullOrEmpty(value))
				{
					if (flag)
					{
						StorageMappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_Empty_QueryView_OfType), entityType.Name, setMapping.Set.Name, StorageMappingErrorCode.EmptyQueryView, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
						return false;
					}
					StorageMappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_Empty_QueryView_OfTypeOnly), setMapping.Set.Name, entityType.Name, StorageMappingErrorCode.EmptyQueryView, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
					return false;
				}
				else
				{
					Pair<EntitySetBase, Pair<EntityTypeBase, bool>> key = new Pair<EntitySetBase, Pair<EntityTypeBase, bool>>(setMapping.Set, new Pair<EntityTypeBase, bool>(entityType, flag));
					if (setMapping.ContainsTypeSpecificQueryView(key))
					{
						EdmSchemaError item;
						if (flag)
						{
							item = new EdmSchemaError(Strings.Mapping_QueryView_Duplicate_OfType(setMapping.Set, entityType), 2082, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, ((IXmlLineInfo)nav).LineNumber, ((IXmlLineInfo)nav).LinePosition);
						}
						else
						{
							item = new EdmSchemaError(Strings.Mapping_QueryView_Duplicate_OfTypeOnly(setMapping.Set, entityType), 2082, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, ((IXmlLineInfo)nav).LineNumber, ((IXmlLineInfo)nav).LinePosition);
						}
						this.m_parsingErrors.Add(item);
						return false;
					}
					setMapping.AddTypeSpecificQueryView(key, value);
					return true;
				}
			}
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x00087150 File Offset: 0x00085350
		private void LoadAssociationSetMapping(XPathNavigator nav, StorageEntityContainerMapping entityContainerMapping, System.Data.Metadata.Edm.EntityContainer storageEntityContainerType)
		{
			IXmlLineInfo xmlLineInfo = (IXmlLineInfo)nav;
			string aliasResolvedAttributeValue = this.GetAliasResolvedAttributeValue(nav.Clone(), "Name");
			string aliasResolvedAttributeValue2 = this.GetAliasResolvedAttributeValue(nav.Clone(), "TypeName");
			string aliasResolvedAttributeValue3 = this.GetAliasResolvedAttributeValue(nav.Clone(), "StoreEntitySet");
			RelationshipSet relationshipSet;
			entityContainerMapping.EdmEntityContainer.TryGetRelationshipSetByName(aliasResolvedAttributeValue, false, out relationshipSet);
			AssociationSet associationSet = relationshipSet as AssociationSet;
			if (associationSet == null)
			{
				StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Association_Set), aliasResolvedAttributeValue, StorageMappingErrorCode.InvalidAssociationSet, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
				return;
			}
			if (associationSet.ElementType.IsForeignKey)
			{
				System.Data.Metadata.Edm.ReferentialConstraint referentialConstraint = associationSet.ElementType.ReferentialConstraints.Single<System.Data.Metadata.Edm.ReferentialConstraint>();
				IEnumerable<EdmMember> dependentKeys = MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)referentialConstraint.ToRole).KeyMembers;
				if (associationSet.ElementType.ReferentialConstraints.Single<System.Data.Metadata.Edm.ReferentialConstraint>().ToProperties.All((EdmProperty p) => dependentKeys.Contains(p)))
				{
					EdmSchemaError edmSchemaError = StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_ForeignKey_Association_Set_PKtoPK), aliasResolvedAttributeValue, StorageMappingErrorCode.InvalidAssociationSet, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					edmSchemaError.Severity = EdmSchemaErrorSeverity.Warning;
					return;
				}
				StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_ForeignKey_Association_Set), aliasResolvedAttributeValue, StorageMappingErrorCode.InvalidAssociationSet, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
				return;
			}
			else
			{
				if (entityContainerMapping.ContainsAssociationSetMapping(associationSet))
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_Duplicate_CdmAssociationSet_StorageMap), aliasResolvedAttributeValue, StorageMappingErrorCode.DuplicateSetMapping, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					return;
				}
				StorageAssociationSetMapping storageAssociationSetMapping = new StorageAssociationSetMapping(associationSet, entityContainerMapping);
				storageAssociationSetMapping.StartLineNumber = xmlLineInfo.LineNumber;
				storageAssociationSetMapping.StartLinePosition = xmlLineInfo.LinePosition;
				if (!nav.MoveToChild(XPathNodeType.Element))
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Emtpty_SetMap), associationSet.Name, StorageMappingErrorCode.EmptySetMapping, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					return;
				}
				entityContainerMapping.AddAssociationSetMapping(storageAssociationSetMapping);
				if (nav.LocalName == "QueryView")
				{
					if (!string.IsNullOrEmpty(aliasResolvedAttributeValue3))
					{
						StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_TableName_QueryView), aliasResolvedAttributeValue, StorageMappingErrorCode.TableNameAttributeWithQueryView, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
						return;
					}
					if (!this.LoadQueryView(nav.Clone(), storageAssociationSetMapping))
					{
						return;
					}
					if (!nav.MoveToNext(XPathNodeType.Element))
					{
						return;
					}
				}
				if (nav.LocalName == "EndProperty" || nav.LocalName == "ModificationFunctionMapping")
				{
					if (string.IsNullOrEmpty(aliasResolvedAttributeValue2))
					{
						StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_Association_Type_Empty, StorageMappingErrorCode.InvalidAssociationType, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
						return;
					}
					this.LoadAssociationTypeMapping(nav.Clone(), storageAssociationSetMapping, aliasResolvedAttributeValue2, aliasResolvedAttributeValue3, storageEntityContainerType);
					return;
				}
				else
				{
					if (nav.LocalName == "Condition")
					{
						StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_AssociationSet_Condition), aliasResolvedAttributeValue, StorageMappingErrorCode.InvalidContent, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
						return;
					}
					return;
				}
			}
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x00087424 File Offset: 0x00085624
		private void LoadFunctionImportMapping(XPathNavigator nav, StorageEntityContainerMapping entityContainerMapping, System.Data.Metadata.Edm.EntityContainer storageEntityContainerType)
		{
			IXmlLineInfo xmlLineInfo = (IXmlLineInfo)nav.Clone();
			EdmFunction edmFunction;
			if (!this.TryGetFunctionImportStoreFunction(nav, out edmFunction))
			{
				return;
			}
			EdmFunction edmFunction2;
			if (!this.TryGetFunctionImportModelFunction(nav, entityContainerMapping, out edmFunction2))
			{
				return;
			}
			if (!edmFunction2.IsComposableAttribute && edmFunction.IsComposableAttribute)
			{
				StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_TargetFunctionMustBeNonComposable(edmFunction2.FullName, edmFunction.FullName), StorageMappingErrorCode.MappingFunctionImportTargetFunctionMustBeNonComposable, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
				return;
			}
			if (edmFunction2.IsComposableAttribute && !edmFunction.IsComposableAttribute)
			{
				StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_TargetFunctionMustBeComposable(edmFunction2.FullName, edmFunction.FullName), StorageMappingErrorCode.MappingFunctionImportTargetFunctionMustBeComposable, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
				return;
			}
			this.ValidateFunctionImportMappingParameters(nav, edmFunction, edmFunction2);
			List<List<FunctionImportStructuralTypeMapping>> list = new List<List<FunctionImportStructuralTypeMapping>>();
			if (nav.MoveToChild(XPathNodeType.Element))
			{
				int num = 0;
				do
				{
					if (nav.LocalName == "ResultMapping")
					{
						List<FunctionImportStructuralTypeMapping> functionImportMappingResultMapping = this.GetFunctionImportMappingResultMapping(nav.Clone(), xmlLineInfo, edmFunction, edmFunction2, num, list);
						list.Add(functionImportMappingResultMapping);
					}
					num++;
				}
				while (nav.MoveToNext(XPathNodeType.Element));
			}
			if (list.Count > 0 && list.Count != edmFunction2.ReturnParameters.Count)
			{
				StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_FunctionImport_ResultMappingCountDoesNotMatchResultCount(edmFunction2.Identity), StorageMappingErrorCode.FunctionResultMappingCountMismatch, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
				return;
			}
			if (!edmFunction2.IsComposableAttribute)
			{
				FunctionImportMappingNonComposable functionImportMappingNonComposable = new FunctionImportMappingNonComposable(edmFunction2, edmFunction, list, this.EdmItemCollection);
				foreach (FunctionImportStructuralTypeMappingKB functionImportStructuralTypeMappingKB in functionImportMappingNonComposable.ResultMappings)
				{
					functionImportStructuralTypeMappingKB.ValidateTypeConditions(false, this.m_parsingErrors, this.m_sourceLocation);
				}
				for (int i = 0; i < functionImportMappingNonComposable.ResultMappings.Count; i++)
				{
					EntityType entityType;
					if (MetadataHelper.TryGetFunctionImportReturnType<EntityType>(edmFunction2, i, out entityType) && entityType.Abstract && functionImportMappingNonComposable.GetResultMapping(i).NormalizedEntityTypeMappings.Count == 0)
					{
						StorageMappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_FunctionImport_ImplicitMappingForAbstractReturnType), entityType.FullName, edmFunction2.Identity, StorageMappingErrorCode.MappingOfAbstractType, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					}
				}
				entityContainerMapping.AddFunctionImportMapping(edmFunction2, functionImportMappingNonComposable);
				return;
			}
			EdmFunction edmFunction3 = this.StoreItemCollection.ConvertToCTypeFunction(edmFunction);
			RowType tvfReturnType = TypeHelpers.GetTvfReturnType(edmFunction3);
			RowType tvfReturnType2 = TypeHelpers.GetTvfReturnType(edmFunction);
			if (tvfReturnType == null)
			{
				StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_FunctionImport_ResultMapping_InvalidSType(edmFunction2.Identity), StorageMappingErrorCode.MappingFunctionImportTVFExpected, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
				return;
			}
			List<FunctionImportStructuralTypeMapping> typeMappings = (list.Count > 0) ? list[0] : new List<FunctionImportStructuralTypeMapping>();
			FunctionImportMappingComposable mapping = null;
			EdmType edmType;
			if (MetadataHelper.TryGetFunctionImportReturnType<EdmType>(edmFunction2, 0, out edmType))
			{
				if (Helper.IsStructuralType(edmType))
				{
					if (!this.TryCreateFunctionImportMappingComposableWithStructuralResult(edmFunction2, edmFunction3, typeMappings, (StructuralType)edmType, tvfReturnType, tvfReturnType2, xmlLineInfo, out mapping))
					{
						return;
					}
				}
				else if (!this.TryCreateFunctionImportMappingComposableWithScalarResult(edmFunction2, edmFunction3, edmFunction, edmType, tvfReturnType, tvfReturnType2, xmlLineInfo, out mapping))
				{
					return;
				}
			}
			entityContainerMapping.AddFunctionImportMapping(edmFunction2, mapping);
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x00087700 File Offset: 0x00085900
		private bool TryGetFunctionImportStoreFunction(XPathNavigator nav, out EdmFunction targetFunction)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			targetFunction = null;
			string aliasResolvedAttributeValue = this.GetAliasResolvedAttributeValue(nav.Clone(), "FunctionName");
			ReadOnlyCollection<EdmFunction> functions = this.StoreItemCollection.GetFunctions(aliasResolvedAttributeValue);
			if (functions.Count == 0)
			{
				StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_StoreFunctionDoesNotExist(aliasResolvedAttributeValue), StorageMappingErrorCode.MappingFunctionImportStoreFunctionDoesNotExist, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return false;
			}
			if (functions.Count > 1)
			{
				StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_FunctionAmbiguous(aliasResolvedAttributeValue), StorageMappingErrorCode.MappingFunctionImportStoreFunctionAmbiguous, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return false;
			}
			targetFunction = functions.Single<EdmFunction>();
			return true;
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x00087790 File Offset: 0x00085990
		private bool TryGetFunctionImportModelFunction(XPathNavigator nav, StorageEntityContainerMapping entityContainerMapping, out EdmFunction functionImport)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			string aliasResolvedAttributeValue = this.GetAliasResolvedAttributeValue(nav.Clone(), "FunctionImportName");
			System.Data.Metadata.Edm.EntityContainer edmEntityContainer = entityContainerMapping.EdmEntityContainer;
			functionImport = null;
			foreach (EdmFunction edmFunction in edmEntityContainer.FunctionImports)
			{
				if (edmFunction.Name == aliasResolvedAttributeValue)
				{
					functionImport = edmFunction;
					break;
				}
			}
			if (functionImport == null)
			{
				StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_FunctionImportDoesNotExist(aliasResolvedAttributeValue, entityContainerMapping.EdmEntityContainer.Name), StorageMappingErrorCode.MappingFunctionImportFunctionImportDoesNotExist, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return false;
			}
			FunctionImportMapping functionImportMapping;
			if (entityContainerMapping.TryGetFunctionImportMapping(functionImport, out functionImportMapping))
			{
				StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_FunctionImportMappedMultipleTimes(aliasResolvedAttributeValue), StorageMappingErrorCode.MappingFunctionImportFunctionImportMappedMultipleTimes, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return false;
			}
			return true;
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x00087870 File Offset: 0x00085A70
		private void ValidateFunctionImportMappingParameters(XPathNavigator nav, EdmFunction targetFunction, EdmFunction functionImport)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			foreach (FunctionParameter functionParameter in targetFunction.Parameters)
			{
				FunctionParameter functionParameter2;
				if (!functionImport.Parameters.TryGetValue(functionParameter.Name, false, out functionParameter2))
				{
					StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_TargetParameterHasNoCorrespondingImportParameter(functionParameter.Name), StorageMappingErrorCode.MappingFunctionImportTargetParameterHasNoCorrespondingImportParameter, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				}
				else
				{
					if (functionParameter.Mode != functionParameter2.Mode)
					{
						StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_IncompatibleParameterMode(functionParameter.Name, functionParameter.Mode, functionParameter2.Mode), StorageMappingErrorCode.MappingFunctionImportIncompatibleParameterMode, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					}
					PrimitiveType primitiveType = Helper.AsPrimitive(functionParameter2.TypeUsage.EdmType);
					if (Helper.IsSpatialType(primitiveType))
					{
						primitiveType = Helper.GetSpatialNormalizedPrimitiveType(primitiveType);
					}
					PrimitiveType primitiveType2 = (PrimitiveType)this.StoreItemCollection.StoreProviderManifest.GetEdmType(functionParameter.TypeUsage).EdmType;
					if (primitiveType2 == null)
					{
						StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_ProviderReturnsNullType(functionParameter.Name), StorageMappingErrorCode.MappingStoreProviderReturnsNullEdmType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
						return;
					}
					if (primitiveType2.PrimitiveTypeKind != primitiveType.PrimitiveTypeKind)
					{
						string errorMessage = Helper.IsEnumType(functionParameter2.TypeUsage.EdmType) ? Strings.Mapping_FunctionImport_IncompatibleEnumParameterType(functionParameter.Name, primitiveType2.Name, functionParameter2.TypeUsage.EdmType.FullName, Helper.GetUnderlyingEdmTypeForEnumType(functionParameter2.TypeUsage.EdmType).Name) : Strings.Mapping_FunctionImport_IncompatibleParameterType(functionParameter.Name, primitiveType2.Name, primitiveType.Name);
						StorageMappingItemLoader.AddToSchemaErrorWithMessage(errorMessage, StorageMappingErrorCode.MappingFunctionImportIncompatibleParameterType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					}
				}
			}
			foreach (FunctionParameter functionParameter3 in functionImport.Parameters)
			{
				FunctionParameter functionParameter4;
				if (!targetFunction.Parameters.TryGetValue(functionParameter3.Name, false, out functionParameter4))
				{
					StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_ImportParameterHasNoCorrespondingTargetParameter(functionParameter3.Name), StorageMappingErrorCode.MappingFunctionImportImportParameterHasNoCorrespondingTargetParameter, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				}
			}
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x00087ADC File Offset: 0x00085CDC
		private List<FunctionImportStructuralTypeMapping> GetFunctionImportMappingResultMapping(XPathNavigator nav, IXmlLineInfo functionImportMappingLineInfo, EdmFunction targetFunction, EdmFunction functionImport, int resultSetIndex, List<List<FunctionImportStructuralTypeMapping>> typeMappingsList)
		{
			List<FunctionImportStructuralTypeMapping> list = new List<FunctionImportStructuralTypeMapping>();
			if (nav.MoveToChild(XPathNodeType.Element))
			{
				do
				{
					EntitySet entitySet = (functionImport.EntitySets.Count > resultSetIndex) ? functionImport.EntitySets[resultSetIndex] : null;
					if (nav.LocalName == "EntityTypeMapping")
					{
						EntityType resultEntityType;
						if (MetadataHelper.TryGetFunctionImportReturnType<EntityType>(functionImport, resultSetIndex, out resultEntityType))
						{
							if (entitySet == null)
							{
								StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_FunctionImport_EntityTypeMappingForFunctionNotReturningEntitySet("EntityTypeMapping", functionImport.Identity), StorageMappingErrorCode.MappingFunctionImportEntityTypeMappingForFunctionNotReturningEntitySet, this.m_sourceLocation, functionImportMappingLineInfo, this.m_parsingErrors);
							}
							FunctionImportEntityTypeMapping item;
							if (this.TryLoadFunctionImportEntityTypeMapping(nav.Clone(), resultEntityType, (EntityType e) => Strings.Mapping_FunctionImport_InvalidContentEntityTypeForEntitySet(e.FullName, resultEntityType.FullName, entitySet.Name, functionImport.Identity), out item))
							{
								list.Add(item);
							}
						}
						else
						{
							StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_FunctionImport_ResultMapping_InvalidCTypeETExpected(functionImport.Identity), StorageMappingErrorCode.MappingFunctionImportUnexpectedEntityTypeMapping, this.m_sourceLocation, functionImportMappingLineInfo, this.m_parsingErrors);
						}
					}
					else if (nav.LocalName == "ComplexTypeMapping")
					{
						ComplexType resultComplexType;
						if (MetadataHelper.TryGetFunctionImportReturnType<ComplexType>(functionImport, resultSetIndex, out resultComplexType))
						{
							FunctionImportComplexTypeMapping item2;
							if (this.TryLoadFunctionImportComplexTypeMapping(nav.Clone(), resultComplexType, functionImport, out item2))
							{
								list.Add(item2);
							}
						}
						else
						{
							StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_FunctionImport_ResultMapping_InvalidCTypeCTExpected(functionImport.Identity), StorageMappingErrorCode.MappingFunctionImportUnexpectedComplexTypeMapping, this.m_sourceLocation, functionImportMappingLineInfo, this.m_parsingErrors);
						}
					}
				}
				while (nav.MoveToNext(XPathNodeType.Element));
			}
			return list;
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x00087CA0 File Offset: 0x00085EA0
		private bool TryLoadFunctionImportComplexTypeMapping(XPathNavigator nav, ComplexType resultComplexType, EdmFunction functionImport, out FunctionImportComplexTypeMapping typeMapping)
		{
			typeMapping = null;
			LineInfo lineInfo = new LineInfo(nav);
			ComplexType complexType;
			if (!this.TryParseComplexTypeAttribute(nav, resultComplexType, functionImport, out complexType))
			{
				return false;
			}
			Collection<FunctionImportReturnTypePropertyMapping> collection = new Collection<FunctionImportReturnTypePropertyMapping>();
			if (!this.LoadFunctionImportStructuralType(nav.Clone(), new List<StructuralType>
			{
				complexType
			}, collection, null))
			{
				return false;
			}
			typeMapping = new FunctionImportComplexTypeMapping(complexType, collection, lineInfo);
			return true;
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x00087CF8 File Offset: 0x00085EF8
		private bool TryParseComplexTypeAttribute(XPathNavigator nav, ComplexType resultComplexType, EdmFunction functionImport, out ComplexType complexType)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			string text = StorageMappingItemLoader.GetAttributeValue(nav.Clone(), "TypeName");
			text = this.GetAliasResolvedValue(text);
			if (!this.EdmItemCollection.TryGetItem<ComplexType>(text, out complexType))
			{
				StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Complex_Type), text, StorageMappingErrorCode.InvalidComplexType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return false;
			}
			if (!Helper.IsAssignableFrom(resultComplexType, complexType))
			{
				StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_ResultMapping_MappedTypeDoesNotMatchReturnType(functionImport.Identity, complexType.FullName), StorageMappingErrorCode.InvalidComplexType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return false;
			}
			return true;
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x00087D98 File Offset: 0x00085F98
		private bool TryLoadFunctionImportEntityTypeMapping(XPathNavigator nav, EntityType resultEntityType, Func<EntityType, string> registerEntityTypeMismatchError, out FunctionImportEntityTypeMapping typeMapping)
		{
			typeMapping = null;
			LineInfo lineInfo = new LineInfo(nav);
			string attributeValue = StorageMappingItemLoader.GetAttributeValue(nav.Clone(), "TypeName");
			Set<EntityType> set;
			Set<EntityType> set2;
			if (!this.TryParseEntityTypeAttribute(nav.Clone(), resultEntityType, registerEntityTypeMismatchError, out set, out set2))
			{
				return false;
			}
			IEnumerable<StructuralType> currentTypes = set.Concat(set2).Distinct<EntityType>().OfType<StructuralType>();
			Collection<FunctionImportReturnTypePropertyMapping> collection = new Collection<FunctionImportReturnTypePropertyMapping>();
			List<FunctionImportEntityTypeMappingCondition> conditions = new List<FunctionImportEntityTypeMappingCondition>();
			if (!this.LoadFunctionImportStructuralType(nav.Clone(), currentTypes, collection, conditions))
			{
				return false;
			}
			typeMapping = new FunctionImportEntityTypeMapping(set, set2, conditions, collection, lineInfo);
			return true;
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x00087E20 File Offset: 0x00086020
		private bool LoadFunctionImportStructuralType(XPathNavigator nav, IEnumerable<StructuralType> currentTypes, Collection<FunctionImportReturnTypePropertyMapping> columnRenameMappings, List<FunctionImportEntityTypeMappingCondition> conditions)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav.Clone();
			if (nav.MoveToChild(XPathNodeType.Element))
			{
				do
				{
					if (nav.LocalName == "ScalarProperty")
					{
						this.LoadFunctionImportStructuralTypeMappingScalarProperty(nav, columnRenameMappings, currentTypes);
					}
					if (nav.LocalName == "Condition")
					{
						this.LoadFunctionImportEntityTypeMappingCondition(nav, conditions);
					}
				}
				while (nav.MoveToNext(XPathNodeType.Element));
			}
			bool flag = false;
			if (conditions != null)
			{
				HashSet<string> hashSet = new HashSet<string>();
				foreach (FunctionImportEntityTypeMappingCondition functionImportEntityTypeMappingCondition in conditions)
				{
					if (!hashSet.Add(functionImportEntityTypeMappingCondition.ColumnName))
					{
						StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_InvalidContent_Duplicate_Condition_Member(functionImportEntityTypeMappingCondition.ColumnName), StorageMappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
						flag = true;
					}
				}
			}
			return !flag;
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x00087F04 File Offset: 0x00086104
		private void LoadFunctionImportStructuralTypeMappingScalarProperty(XPathNavigator nav, Collection<FunctionImportReturnTypePropertyMapping> columnRenameMappings, IEnumerable<StructuralType> currentTypes)
		{
			LineInfo lineInfo = new LineInfo(nav);
			string memberName = this.GetAliasResolvedAttributeValue(nav.Clone(), "Name");
			string aliasResolvedAttributeValue = this.GetAliasResolvedAttributeValue(nav.Clone(), "ColumnName");
			if (!currentTypes.All((StructuralType t) => t.Members.Contains(memberName)))
			{
				StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_InvalidContent_Cdm_Member(memberName), StorageMappingErrorCode.InvalidEdmMember, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
			}
			if (columnRenameMappings.Any((FunctionImportReturnTypePropertyMapping m) => m.CMember == memberName))
			{
				StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_InvalidContent_Duplicate_Cdm_Member(memberName), StorageMappingErrorCode.DuplicateMemberMapping, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return;
			}
			columnRenameMappings.Add(new FunctionImportReturnTypeScalarPropertyMapping(memberName, aliasResolvedAttributeValue, lineInfo));
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x00087FC8 File Offset: 0x000861C8
		private bool TryCreateFunctionImportMappingComposableWithStructuralResult(EdmFunction functionImport, EdmFunction cTypeTargetFunction, List<FunctionImportStructuralTypeMapping> typeMappings, StructuralType structuralResultType, RowType cTypeTvfElementType, RowType sTypeTvfElementType, IXmlLineInfo lineInfo, out FunctionImportMappingComposable mapping)
		{
			mapping = null;
			StructuralType structuralType;
			if (typeMappings.Count == 0 && MetadataHelper.TryGetFunctionImportReturnType<StructuralType>(functionImport, 0, out structuralType))
			{
				if (structuralType.Abstract)
				{
					StorageMappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_FunctionImport_ImplicitMappingForAbstractReturnType), structuralType.FullName, functionImport.Identity, StorageMappingErrorCode.MappingOfAbstractType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					return false;
				}
				if (structuralType.BuiltInTypeKind == BuiltInTypeKind.EntityType)
				{
					typeMappings.Add(new FunctionImportEntityTypeMapping(Enumerable.Empty<EntityType>(), new EntityType[]
					{
						(EntityType)structuralType
					}, Enumerable.Empty<FunctionImportEntityTypeMappingCondition>(), new Collection<FunctionImportReturnTypePropertyMapping>(), new LineInfo(lineInfo)));
				}
				else
				{
					typeMappings.Add(new FunctionImportComplexTypeMapping((ComplexType)structuralType, new Collection<FunctionImportReturnTypePropertyMapping>(), new LineInfo(lineInfo)));
				}
			}
			FunctionImportStructuralTypeMappingKB functionImportStructuralTypeMappingKB = new FunctionImportStructuralTypeMappingKB(typeMappings, this.EdmItemCollection);
			List<Tuple<StructuralType, List<StorageConditionPropertyMapping>, List<StoragePropertyMapping>>> list = new List<Tuple<StructuralType, List<StorageConditionPropertyMapping>, List<StoragePropertyMapping>>>();
			EdmProperty[] targetFunctionKeys = null;
			ComplexType complexType;
			if (functionImportStructuralTypeMappingKB.MappedEntityTypes.Count > 0)
			{
				if (!functionImportStructuralTypeMappingKB.ValidateTypeConditions(true, this.m_parsingErrors, this.m_sourceLocation))
				{
					return false;
				}
				for (int i = 0; i < functionImportStructuralTypeMappingKB.MappedEntityTypes.Count; i++)
				{
					List<StorageConditionPropertyMapping> item;
					List<StoragePropertyMapping> item2;
					if (this.TryConvertToEntityTypeConditionsAndPropertyMappings(functionImport, functionImportStructuralTypeMappingKB, i, cTypeTvfElementType, sTypeTvfElementType, lineInfo, out item, out item2))
					{
						list.Add(Tuple.Create<StructuralType, List<StorageConditionPropertyMapping>, List<StoragePropertyMapping>>(functionImportStructuralTypeMappingKB.MappedEntityTypes[i], item, item2));
					}
				}
				if (list.Count < functionImportStructuralTypeMappingKB.MappedEntityTypes.Count)
				{
					return false;
				}
				if (!StorageMappingItemLoader.TryInferTVFKeys(list, out targetFunctionKeys))
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_FunctionImport_CannotInferTargetFunctionKeys), functionImport.Identity, StorageMappingErrorCode.MappingFunctionImportCannotInferTargetFunctionKeys, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					return false;
				}
			}
			else if (MetadataHelper.TryGetFunctionImportReturnType<ComplexType>(functionImport, 0, out complexType))
			{
				List<StoragePropertyMapping> item3;
				if (!this.TryConvertToProperyMappings(complexType, cTypeTvfElementType, sTypeTvfElementType, functionImport, functionImportStructuralTypeMappingKB, lineInfo, out item3))
				{
					return false;
				}
				list.Add(Tuple.Create<StructuralType, List<StorageConditionPropertyMapping>, List<StoragePropertyMapping>>(complexType, new List<StorageConditionPropertyMapping>(), item3));
			}
			mapping = new FunctionImportMappingComposable(functionImport, cTypeTargetFunction, list, targetFunctionKeys, this.m_storageMappingItemCollection, this.m_sourceLocation, new LineInfo(lineInfo));
			return true;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000881B0 File Offset: 0x000863B0
		internal static bool TryInferTVFKeys(List<Tuple<StructuralType, List<StorageConditionPropertyMapping>, List<StoragePropertyMapping>>> structuralTypeMappings, out EdmProperty[] keys)
		{
			keys = null;
			foreach (Tuple<StructuralType, List<StorageConditionPropertyMapping>, List<StoragePropertyMapping>> tuple in structuralTypeMappings)
			{
				EdmProperty[] array;
				if (!StorageMappingItemLoader.TryInferTVFKeysForEntityType((EntityType)tuple.Item1, tuple.Item3, out array))
				{
					keys = null;
					return false;
				}
				if (keys == null)
				{
					keys = array;
				}
				else
				{
					for (int i = 0; i < keys.Length; i++)
					{
						if (!keys[i].EdmEquals(array[i]))
						{
							keys = null;
							return false;
						}
					}
				}
			}
			for (int j = 0; j < keys.Length; j++)
			{
				if (keys[j].Nullable)
				{
					keys = null;
					return false;
				}
			}
			return true;
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x00088278 File Offset: 0x00086478
		private static bool TryInferTVFKeysForEntityType(EntityType entityType, List<StoragePropertyMapping> propertyMappings, out EdmProperty[] keys)
		{
			keys = new EdmProperty[entityType.KeyMembers.Count];
			for (int i = 0; i < keys.Length; i++)
			{
				StorageScalarPropertyMapping storageScalarPropertyMapping = propertyMappings[entityType.Properties.IndexOf((EdmProperty)entityType.KeyMembers[i])] as StorageScalarPropertyMapping;
				if (storageScalarPropertyMapping == null)
				{
					keys = null;
					return false;
				}
				keys[i] = storageScalarPropertyMapping.ColumnProperty;
			}
			return true;
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000882E4 File Offset: 0x000864E4
		private bool TryCreateFunctionImportMappingComposableWithScalarResult(EdmFunction functionImport, EdmFunction cTypeTargetFunction, EdmFunction sTypeTargetFunction, EdmType scalarResultType, RowType cTypeTvfElementType, RowType sTypeTvfElementType, IXmlLineInfo lineInfo, out FunctionImportMappingComposable mapping)
		{
			mapping = null;
			if (cTypeTvfElementType.Properties.Count > 1)
			{
				StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_FunctionImport_ScalarMappingToMulticolumnTVF(functionImport.Identity, sTypeTargetFunction.Identity), StorageMappingErrorCode.MappingFunctionImportScalarMappingToMulticolumnTVF, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return false;
			}
			if (!this.ValidateFunctionImportMappingResultTypeCompatibility(TypeUsage.Create(scalarResultType), cTypeTvfElementType.Properties[0].TypeUsage))
			{
				StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_FunctionImport_ScalarMappingTypeMismatch(functionImport.ReturnParameter.TypeUsage.EdmType.FullName, functionImport.Identity, sTypeTargetFunction.ReturnParameter.TypeUsage.EdmType.FullName, sTypeTargetFunction.Identity), StorageMappingErrorCode.MappingFunctionImportScalarMappingTypeMismatch, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return false;
			}
			mapping = new FunctionImportMappingComposable(functionImport, cTypeTargetFunction, null, null, this.m_storageMappingItemCollection, this.m_sourceLocation, new LineInfo(lineInfo));
			return true;
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000883C8 File Offset: 0x000865C8
		private bool ValidateFunctionImportMappingResultTypeCompatibility(TypeUsage cSpaceMemberType, TypeUsage sSpaceMemberType)
		{
			TypeUsage typeUsage = StorageMappingItemLoader.ResolveTypeUsageForEnums(cSpaceMemberType);
			bool flag = TypeSemantics.IsStructurallyEqualOrPromotableTo(sSpaceMemberType, typeUsage);
			bool flag2 = TypeSemantics.IsStructurallyEqualOrPromotableTo(typeUsage, sSpaceMemberType);
			return flag || flag2;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000883F4 File Offset: 0x000865F4
		private void LoadFunctionImportEntityTypeMappingCondition(XPathNavigator nav, List<FunctionImportEntityTypeMappingCondition> conditions)
		{
			LineInfo lineInfo = new LineInfo(nav);
			string aliasResolvedAttributeValue = this.GetAliasResolvedAttributeValue(nav.Clone(), "ColumnName");
			string aliasResolvedAttributeValue2 = this.GetAliasResolvedAttributeValue(nav.Clone(), "Value");
			string aliasResolvedAttributeValue3 = this.GetAliasResolvedAttributeValue(nav.Clone(), "IsNull");
			if (aliasResolvedAttributeValue3 != null && aliasResolvedAttributeValue2 != null)
			{
				StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_ConditionMapping_Both_Values, StorageMappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return;
			}
			if (aliasResolvedAttributeValue3 == null && aliasResolvedAttributeValue2 == null)
			{
				StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_ConditionMapping_Either_Values, StorageMappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return;
			}
			if (aliasResolvedAttributeValue3 != null)
			{
				bool isNull = Convert.ToBoolean(aliasResolvedAttributeValue3, CultureInfo.InvariantCulture);
				conditions.Add(new FunctionImportEntityTypeMappingConditionIsNull(aliasResolvedAttributeValue, isNull, lineInfo));
				return;
			}
			XPathNavigator xpathNavigator = nav.Clone();
			xpathNavigator.MoveToAttribute("Value", string.Empty);
			conditions.Add(new FunctionImportEntityTypeMappingConditionValue(aliasResolvedAttributeValue, xpathNavigator, lineInfo));
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000884D0 File Offset: 0x000866D0
		private bool TryConvertToEntityTypeConditionsAndPropertyMappings(EdmFunction functionImport, FunctionImportStructuralTypeMappingKB functionImportKB, int typeID, RowType cTypeTvfElementType, RowType sTypeTvfElementType, IXmlLineInfo navLineInfo, out List<StorageConditionPropertyMapping> typeConditions, out List<StoragePropertyMapping> propertyMappings)
		{
			StorageMappingItemLoader.<>c__DisplayClass59_0 CS$<>8__locals1 = new StorageMappingItemLoader.<>c__DisplayClass59_0();
			CS$<>8__locals1.typeID = typeID;
			CS$<>8__locals1.<>4__this = this;
			EntityType structuralType = functionImportKB.MappedEntityTypes[CS$<>8__locals1.typeID];
			typeConditions = new List<StorageConditionPropertyMapping>();
			bool flag = false;
			IEnumerable<FunctionImportNormalizedEntityTypeMapping> normalizedEntityTypeMappings = functionImportKB.NormalizedEntityTypeMappings;
			Func<FunctionImportNormalizedEntityTypeMapping, bool> predicate;
			if ((predicate = CS$<>8__locals1.<>9__0) == null)
			{
				predicate = (CS$<>8__locals1.<>9__0 = ((FunctionImportNormalizedEntityTypeMapping f) => f.ImpliedEntityTypes[CS$<>8__locals1.typeID]));
			}
			foreach (FunctionImportNormalizedEntityTypeMapping functionImportNormalizedEntityTypeMapping in normalizedEntityTypeMappings.Where(predicate))
			{
				using (IEnumerator<FunctionImportEntityTypeMappingCondition> enumerator2 = (from c in functionImportNormalizedEntityTypeMapping.ColumnConditions
				where c != null
				select c).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						StorageMappingItemLoader.<>c__DisplayClass59_1 CS$<>8__locals2 = new StorageMappingItemLoader.<>c__DisplayClass59_1();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						CS$<>8__locals2.condition = enumerator2.Current;
						EdmProperty column;
						if (sTypeTvfElementType.Properties.TryGetValue(CS$<>8__locals2.condition.ColumnName, false, out column))
						{
							object obj;
							bool? isNull;
							if (CS$<>8__locals2.condition.ConditionValue.IsSentinel)
							{
								obj = null;
								if (CS$<>8__locals2.condition.ConditionValue == ValueCondition.IsNull)
								{
									isNull = new bool?(true);
								}
								else
								{
									isNull = new bool?(false);
								}
							}
							else
							{
								EdmProperty edmProperty = cTypeTvfElementType.Properties[column.Name];
								PrimitiveType primitiveType = (PrimitiveType)edmProperty.TypeUsage.EdmType;
								obj = ((FunctionImportEntityTypeMappingConditionValue)CS$<>8__locals2.condition).GetConditionValue(primitiveType.ClrEquivalentType, delegate()
								{
									StorageMappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_InvalidContent_ConditionMapping_InvalidPrimitiveTypeKind), column.Name, column.TypeUsage.EdmType.FullName, StorageMappingErrorCode.ConditionError, CS$<>8__locals2.CS$<>8__locals1.<>4__this.m_sourceLocation, CS$<>8__locals2.condition.LineInfo, CS$<>8__locals2.CS$<>8__locals1.<>4__this.m_parsingErrors);
								}, delegate()
								{
									StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_ConditionValueTypeMismatch, StorageMappingErrorCode.ConditionError, CS$<>8__locals2.CS$<>8__locals1.<>4__this.m_sourceLocation, CS$<>8__locals2.condition.LineInfo, CS$<>8__locals2.CS$<>8__locals1.<>4__this.m_parsingErrors);
								});
								if (obj == null)
								{
									flag = true;
									continue;
								}
								isNull = null;
							}
							typeConditions.Add(new StorageConditionPropertyMapping(null, column, obj, isNull));
						}
						else
						{
							StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Column), CS$<>8__locals2.condition.ColumnName, StorageMappingErrorCode.InvalidStorageMember, this.m_sourceLocation, CS$<>8__locals2.condition.LineInfo, this.m_parsingErrors);
						}
					}
				}
			}
			flag |= !this.TryConvertToProperyMappings(structuralType, cTypeTvfElementType, sTypeTvfElementType, functionImport, functionImportKB, navLineInfo, out propertyMappings);
			return !flag;
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x00088780 File Offset: 0x00086980
		private bool TryConvertToProperyMappings(StructuralType structuralType, RowType cTypeTvfElementType, RowType sTypeTvfElementType, EdmFunction functionImport, FunctionImportStructuralTypeMappingKB functionImportKB, IXmlLineInfo navLineInfo, out List<StoragePropertyMapping> propertyMappings)
		{
			propertyMappings = new List<StoragePropertyMapping>();
			bool flag = false;
			foreach (object obj in TypeHelpers.GetAllStructuralMembers(structuralType))
			{
				EdmProperty edmProperty = (EdmProperty)obj;
				if (!Helper.IsScalarType(edmProperty.TypeUsage.EdmType))
				{
					EdmSchemaError item = new EdmSchemaError(Strings.Mapping_Invalid_CSide_ScalarProperty(edmProperty.Name), 2085, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, navLineInfo.LineNumber, navLineInfo.LinePosition);
					this.m_parsingErrors.Add(item);
					flag = true;
				}
				else
				{
					IXmlLineInfo xmlLineInfo = null;
					FunctionImportReturnTypeStructuralTypeColumnRenameMapping functionImportReturnTypeStructuralTypeColumnRenameMapping;
					bool flag2;
					string text;
					if (functionImportKB.ReturnTypeColumnsRenameMapping.TryGetValue(edmProperty.Name, out functionImportReturnTypeStructuralTypeColumnRenameMapping))
					{
						flag2 = true;
						text = functionImportReturnTypeStructuralTypeColumnRenameMapping.GetRename(structuralType, out xmlLineInfo);
					}
					else
					{
						flag2 = false;
						text = edmProperty.Name;
					}
					xmlLineInfo = ((xmlLineInfo != null && xmlLineInfo.HasLineInfo()) ? xmlLineInfo : navLineInfo);
					EdmProperty edmProperty2;
					if (sTypeTvfElementType.Properties.TryGetValue(text, false, out edmProperty2))
					{
						EdmProperty edmProperty3 = cTypeTvfElementType.Properties[text];
						if (this.ValidateFunctionImportMappingResultTypeCompatibility(edmProperty.TypeUsage, edmProperty3.TypeUsage))
						{
							propertyMappings.Add(new StorageScalarPropertyMapping(edmProperty, edmProperty2));
						}
						else
						{
							EdmSchemaError item2 = new EdmSchemaError(this.GetInvalidMemberMappingErrorMessage(edmProperty, edmProperty2), 2019, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
							this.m_parsingErrors.Add(item2);
						}
					}
					else if (flag2)
					{
						StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Column), text, StorageMappingErrorCode.InvalidStorageMember, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					}
					else
					{
						EdmSchemaError item3 = new EdmSchemaError(Strings.Mapping_FunctionImport_PropertyNotMapped(edmProperty.Name, structuralType.FullName, functionImport.Identity), 2104, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
						this.m_parsingErrors.Add(item3);
						flag = true;
					}
				}
			}
			return !flag;
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x00088988 File Offset: 0x00086B88
		private void LoadAssociationTypeMapping(XPathNavigator nav, StorageAssociationSetMapping associationSetMapping, string associationTypeName, string tableName, System.Data.Metadata.Edm.EntityContainer storageEntityContainerType)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			AssociationType associationType;
			this.EdmItemCollection.TryGetItem<AssociationType>(associationTypeName, out associationType);
			if (associationType == null)
			{
				StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Association_Type), associationTypeName, StorageMappingErrorCode.InvalidAssociationType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return;
			}
			if (!associationSetMapping.Set.ElementType.Equals(associationType))
			{
				StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_Invalid_Association_Type_For_Association_Set(associationTypeName, associationSetMapping.Set.ElementType.FullName, associationSetMapping.Set.Name), StorageMappingErrorCode.DuplicateTypeMapping, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return;
			}
			StorageAssociationTypeMapping storageAssociationTypeMapping = new StorageAssociationTypeMapping(associationType, associationSetMapping);
			associationSetMapping.AddTypeMapping(storageAssociationTypeMapping);
			if (string.IsNullOrEmpty(tableName) && associationSetMapping.QueryView == null)
			{
				StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_Table_Expected, StorageMappingErrorCode.InvalidTable, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return;
			}
			StorageMappingFragment storageMappingFragment = this.LoadAssociationMappingFragment(nav.Clone(), associationSetMapping, storageAssociationTypeMapping, tableName, storageEntityContainerType);
			if (storageMappingFragment != null)
			{
				storageAssociationTypeMapping.AddFragment(storageMappingFragment);
			}
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x00088A7C File Offset: 0x00086C7C
		private void LoadAssociationTypeModificationFunctionMapping(XPathNavigator nav, StorageAssociationSetMapping associationSetMapping, StorageAssociationTypeMapping associationTypeMapping)
		{
			StorageMappingItemLoader.ModificationFunctionMappingLoader modificationFunctionMappingLoader = new StorageMappingItemLoader.ModificationFunctionMappingLoader(this, associationSetMapping.Set);
			StorageModificationFunctionMapping deleteFunctionMapping = null;
			StorageModificationFunctionMapping insertFunctionMapping = null;
			if (nav.MoveToChild(XPathNodeType.Element))
			{
				do
				{
					string localName = nav.LocalName;
					if (!(localName == "DeleteFunction"))
					{
						if (localName == "InsertFunction")
						{
							insertFunctionMapping = modificationFunctionMappingLoader.LoadAssociationSetModificationFunctionMapping(nav.Clone(), associationSetMapping.Set, true);
						}
					}
					else
					{
						deleteFunctionMapping = modificationFunctionMappingLoader.LoadAssociationSetModificationFunctionMapping(nav.Clone(), associationSetMapping.Set, false);
					}
				}
				while (nav.MoveToNext(XPathNodeType.Element));
			}
			associationSetMapping.ModificationFunctionMapping = new StorageAssociationSetModificationFunctionMapping((AssociationSet)associationSetMapping.Set, deleteFunctionMapping, insertFunctionMapping);
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x00088B14 File Offset: 0x00086D14
		private StorageMappingFragment LoadMappingFragment(XPathNavigator nav, StorageEntityTypeMapping typeMapping, string tableName, System.Data.Metadata.Edm.EntityContainer storageEntityContainerType, bool distinctFlag)
		{
			IXmlLineInfo navLineInfo = (IXmlLineInfo)nav;
			if (typeMapping.SetMapping.QueryView != null)
			{
				StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_QueryView_PropertyMaps), typeMapping.SetMapping.Set.Name, StorageMappingErrorCode.PropertyMapsWithQueryView, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
				return null;
			}
			EntitySet entitySet;
			storageEntityContainerType.TryGetEntitySetByName(tableName, false, out entitySet);
			if (entitySet == null)
			{
				StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Table), tableName, StorageMappingErrorCode.InvalidTable, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
				return null;
			}
			EntityType elementType = entitySet.ElementType;
			StorageMappingFragment storageMappingFragment = new StorageMappingFragment(entitySet, typeMapping, distinctFlag);
			storageMappingFragment.StartLineNumber = navLineInfo.LineNumber;
			storageMappingFragment.StartLinePosition = navLineInfo.LinePosition;
			if (nav.MoveToChild(XPathNodeType.Element))
			{
				Action<EdmMember> <>9__0;
				do
				{
					EdmType containerType = null;
					string attributeValue = StorageMappingItemLoader.GetAttributeValue(nav.Clone(), "Name");
					if (attributeValue != null)
					{
						containerType = typeMapping.GetContainerType(attributeValue);
					}
					string localName = nav.LocalName;
					if (!(localName == "ScalarProperty"))
					{
						if (!(localName == "ComplexProperty"))
						{
							if (!(localName == "Condition"))
							{
								StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_General, StorageMappingErrorCode.InvalidContent, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
							}
							else
							{
								StorageConditionPropertyMapping storageConditionPropertyMapping = this.LoadConditionPropertyMapping(nav.Clone(), containerType, elementType.Properties);
								if (storageConditionPropertyMapping != null)
								{
									StorageMappingFragment storageMappingFragment2 = storageMappingFragment;
									StorageConditionPropertyMapping conditionPropertyMap = storageConditionPropertyMapping;
									Action<EdmMember> duplicateMemberConditionError;
									if ((duplicateMemberConditionError = <>9__0) == null)
									{
										duplicateMemberConditionError = (<>9__0 = delegate(EdmMember member)
										{
											StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Duplicate_Condition_Member), member.Name, StorageMappingErrorCode.ConditionError, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
										});
									}
									storageMappingFragment2.AddConditionProperty(conditionPropertyMap, duplicateMemberConditionError);
								}
							}
						}
						else
						{
							StorageComplexPropertyMapping storageComplexPropertyMapping = this.LoadComplexPropertyMapping(nav.Clone(), containerType, elementType.Properties);
							if (storageComplexPropertyMapping != null)
							{
								storageMappingFragment.AddProperty(storageComplexPropertyMapping);
							}
						}
					}
					else
					{
						StorageScalarPropertyMapping storageScalarPropertyMapping = this.LoadScalarPropertyMapping(nav.Clone(), containerType, elementType.Properties);
						if (storageScalarPropertyMapping != null)
						{
							storageMappingFragment.AddProperty(storageScalarPropertyMapping);
						}
					}
				}
				while (nav.MoveToNext(XPathNodeType.Element));
			}
			nav.MoveToChild(XPathNodeType.Element);
			return storageMappingFragment;
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x00088D20 File Offset: 0x00086F20
		private StorageMappingFragment LoadAssociationMappingFragment(XPathNavigator nav, StorageAssociationSetMapping setMapping, StorageAssociationTypeMapping typeMapping, string tableName, System.Data.Metadata.Edm.EntityContainer storageEntityContainerType)
		{
			IXmlLineInfo navLineInfo = (IXmlLineInfo)nav;
			StorageMappingFragment storageMappingFragment = null;
			EntityType entityType = null;
			if (setMapping.QueryView == null)
			{
				EntitySet entitySet;
				storageEntityContainerType.TryGetEntitySetByName(tableName, false, out entitySet);
				if (entitySet == null)
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Table), tableName, StorageMappingErrorCode.InvalidTable, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
					return null;
				}
				entityType = entitySet.ElementType;
				storageMappingFragment = new StorageMappingFragment(entitySet, typeMapping, false);
				storageMappingFragment.StartLineNumber = setMapping.StartLineNumber;
				storageMappingFragment.StartLinePosition = setMapping.StartLinePosition;
			}
			Action<EdmMember> <>9__0;
			for (;;)
			{
				string localName = nav.LocalName;
				if (!(localName == "EndProperty"))
				{
					if (!(localName == "Condition"))
					{
						if (!(localName == "ModificationFunctionMapping"))
						{
							StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_General, StorageMappingErrorCode.InvalidContent, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
						}
						else
						{
							setMapping.HasModificationFunctionMapping = true;
							this.LoadAssociationTypeModificationFunctionMapping(nav.Clone(), setMapping, typeMapping);
						}
					}
					else
					{
						if (setMapping.QueryView != null)
						{
							goto Block_8;
						}
						StorageConditionPropertyMapping storageConditionPropertyMapping = this.LoadConditionPropertyMapping(nav.Clone(), null, entityType.Properties);
						if (storageConditionPropertyMapping != null)
						{
							StorageMappingFragment storageMappingFragment2 = storageMappingFragment;
							StorageConditionPropertyMapping conditionPropertyMap = storageConditionPropertyMapping;
							Action<EdmMember> duplicateMemberConditionError;
							if ((duplicateMemberConditionError = <>9__0) == null)
							{
								duplicateMemberConditionError = (<>9__0 = delegate(EdmMember member)
								{
									StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Duplicate_Condition_Member), member.Name, StorageMappingErrorCode.ConditionError, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
								});
							}
							storageMappingFragment2.AddConditionProperty(conditionPropertyMap, duplicateMemberConditionError);
						}
					}
				}
				else
				{
					if (setMapping.QueryView != null)
					{
						break;
					}
					string aliasResolvedAttributeValue = this.GetAliasResolvedAttributeValue(nav.Clone(), "Name");
					EdmMember edmMember = null;
					typeMapping.AssociationType.Members.TryGetValue(aliasResolvedAttributeValue, false, out edmMember);
					AssociationEndMember associationEndMember = edmMember as AssociationEndMember;
					if (associationEndMember == null)
					{
						StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_End), aliasResolvedAttributeValue, StorageMappingErrorCode.InvalidEdmMember, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
					}
					else
					{
						storageMappingFragment.AddProperty(this.LoadEndPropertyMapping(nav.Clone(), associationEndMember, entityType));
					}
				}
				if (!nav.MoveToNext(XPathNodeType.Element))
				{
					return storageMappingFragment;
				}
			}
			StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_QueryView_PropertyMaps), setMapping.Set.Name, StorageMappingErrorCode.PropertyMapsWithQueryView, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
			return null;
			Block_8:
			StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_QueryView_PropertyMaps), setMapping.Set.Name, StorageMappingErrorCode.PropertyMapsWithQueryView, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
			return null;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x00088F7C File Offset: 0x0008717C
		private StorageScalarPropertyMapping LoadScalarPropertyMapping(XPathNavigator nav, EdmType containerType, ReadOnlyMetadataCollection<EdmProperty> tableProperties)
		{
			IXmlLineInfo xmlLineInfo = (IXmlLineInfo)nav;
			string aliasResolvedAttributeValue = this.GetAliasResolvedAttributeValue(nav.Clone(), "Name");
			EdmProperty edmProperty = null;
			if (!string.IsNullOrEmpty(aliasResolvedAttributeValue) && (containerType == null || !Helper.IsCollectionType(containerType)))
			{
				if (containerType != null)
				{
					if (Helper.IsRefType(containerType))
					{
						RefType refType = (RefType)containerType;
						((EntityType)refType.ElementType).Properties.TryGetValue(aliasResolvedAttributeValue, false, out edmProperty);
					}
					else
					{
						EdmMember edmMember;
						(containerType as StructuralType).Members.TryGetValue(aliasResolvedAttributeValue, false, out edmMember);
						edmProperty = (edmMember as EdmProperty);
					}
				}
				if (edmProperty == null)
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Cdm_Member), aliasResolvedAttributeValue, StorageMappingErrorCode.InvalidEdmMember, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
				}
			}
			string aliasResolvedAttributeValue2 = this.GetAliasResolvedAttributeValue(nav.Clone(), "ColumnName");
			EdmProperty edmProperty2;
			tableProperties.TryGetValue(aliasResolvedAttributeValue2, false, out edmProperty2);
			if (edmProperty2 == null)
			{
				StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Column), aliasResolvedAttributeValue2, StorageMappingErrorCode.InvalidStorageMember, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
			}
			if (edmProperty == null || edmProperty2 == null)
			{
				return null;
			}
			if (!Helper.IsScalarType(edmProperty.TypeUsage.EdmType))
			{
				EdmSchemaError item = new EdmSchemaError(Strings.Mapping_Invalid_CSide_ScalarProperty(edmProperty.Name), 2085, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
				this.m_parsingErrors.Add(item);
				return null;
			}
			this.ValidateAndUpdateScalarMemberMapping(edmProperty, edmProperty2, xmlLineInfo);
			return new StorageScalarPropertyMapping(edmProperty, edmProperty2);
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000890E0 File Offset: 0x000872E0
		private StorageComplexPropertyMapping LoadComplexPropertyMapping(XPathNavigator nav, EdmType containerType, ReadOnlyMetadataCollection<EdmProperty> tableProperties)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			CollectionType collectionType = containerType as CollectionType;
			string aliasResolvedAttributeValue = this.GetAliasResolvedAttributeValue(nav.Clone(), "Name");
			EdmProperty edmProperty = null;
			EdmType edmType = null;
			string aliasResolvedAttributeValue2 = this.GetAliasResolvedAttributeValue(nav.Clone(), "TypeName");
			StructuralType structuralType = containerType as StructuralType;
			if (string.IsNullOrEmpty(aliasResolvedAttributeValue2))
			{
				if (collectionType == null)
				{
					if (structuralType != null)
					{
						EdmMember edmMember;
						structuralType.Members.TryGetValue(aliasResolvedAttributeValue, false, out edmMember);
						edmProperty = (edmMember as EdmProperty);
						if (edmProperty == null)
						{
							StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Cdm_Member), aliasResolvedAttributeValue, StorageMappingErrorCode.InvalidEdmMember, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
						}
						edmType = edmProperty.TypeUsage.EdmType;
					}
					else
					{
						StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Cdm_Member), aliasResolvedAttributeValue, StorageMappingErrorCode.InvalidEdmMember, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					}
				}
				else
				{
					edmType = collectionType.TypeUsage.EdmType;
				}
			}
			else
			{
				if (containerType != null)
				{
					EdmMember edmMember2;
					structuralType.Members.TryGetValue(aliasResolvedAttributeValue, false, out edmMember2);
					edmProperty = (edmMember2 as EdmProperty);
				}
				if (edmProperty == null)
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Cdm_Member), aliasResolvedAttributeValue, StorageMappingErrorCode.InvalidEdmMember, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				}
				this.EdmItemCollection.TryGetItem<EdmType>(aliasResolvedAttributeValue2, out edmType);
				edmType = (edmType as ComplexType);
				if (edmType == null)
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Complex_Type), aliasResolvedAttributeValue2, StorageMappingErrorCode.InvalidComplexType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				}
			}
			StorageComplexPropertyMapping storageComplexPropertyMapping = new StorageComplexPropertyMapping(edmProperty);
			XPathNavigator xpathNavigator = nav.Clone();
			bool flag = false;
			if (xpathNavigator.MoveToChild(XPathNodeType.Element) && xpathNavigator.LocalName == "ComplexTypeMapping")
			{
				flag = true;
			}
			if (edmProperty == null || edmType == null)
			{
				return null;
			}
			if (flag)
			{
				nav.MoveToChild(XPathNodeType.Element);
				do
				{
					storageComplexPropertyMapping.AddTypeMapping(this.LoadComplexTypeMapping(nav.Clone(), null, tableProperties));
				}
				while (nav.MoveToNext(XPathNodeType.Element));
			}
			else
			{
				storageComplexPropertyMapping.AddTypeMapping(this.LoadComplexTypeMapping(nav.Clone(), edmType, tableProperties));
			}
			return storageComplexPropertyMapping;
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000892DC File Offset: 0x000874DC
		private StorageComplexTypeMapping LoadComplexTypeMapping(XPathNavigator nav, EdmType type, ReadOnlyMetadataCollection<EdmProperty> tableType)
		{
			bool isPartial = false;
			string attributeValue = StorageMappingItemLoader.GetAttributeValue(nav.Clone(), "IsPartial");
			if (!string.IsNullOrEmpty(attributeValue))
			{
				isPartial = Convert.ToBoolean(attributeValue, CultureInfo.InvariantCulture);
			}
			StorageComplexTypeMapping storageComplexTypeMapping = new StorageComplexTypeMapping(isPartial);
			if (type != null)
			{
				storageComplexTypeMapping.AddType(type as ComplexType);
			}
			else
			{
				string text = this.GetAliasResolvedAttributeValue(nav.Clone(), "TypeName");
				int num = text.IndexOf(';');
				do
				{
					string text2;
					if (num != -1)
					{
						text2 = text.Substring(0, num);
						text = text.Substring(num + 1, text.Length - (num + 1));
					}
					else
					{
						text2 = text;
						text = string.Empty;
					}
					int num2 = text2.IndexOf("IsTypeOf(", StringComparison.Ordinal);
					if (num2 == 0)
					{
						text2 = text2.Substring("IsTypeOf(".Length, text2.Length - ("IsTypeOf(".Length + 1));
						text2 = this.GetAliasResolvedValue(text2);
					}
					else
					{
						text2 = this.GetAliasResolvedValue(text2);
					}
					ComplexType complexType;
					this.EdmItemCollection.TryGetItem<ComplexType>(text2, out complexType);
					if (complexType == null)
					{
						StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Complex_Type), text2, StorageMappingErrorCode.InvalidComplexType, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
						num = text.IndexOf(';');
					}
					else
					{
						if (num2 == 0)
						{
							storageComplexTypeMapping.AddIsOfType(complexType);
						}
						else
						{
							storageComplexTypeMapping.AddType(complexType);
						}
						num = text.IndexOf(';');
					}
				}
				while (text.Length != 0);
			}
			if (nav.MoveToChild(XPathNodeType.Element))
			{
				Action<EdmMember> <>9__0;
				for (;;)
				{
					EdmType ownerType = storageComplexTypeMapping.GetOwnerType(StorageMappingItemLoader.GetAttributeValue(nav.Clone(), "Name"));
					string localName = nav.LocalName;
					if (!(localName == "ScalarProperty"))
					{
						if (!(localName == "ComplexProperty"))
						{
							if (!(localName == "Condition"))
							{
								break;
							}
							StorageConditionPropertyMapping storageConditionPropertyMapping = this.LoadConditionPropertyMapping(nav.Clone(), ownerType, tableType);
							if (storageConditionPropertyMapping != null)
							{
								StorageComplexTypeMapping storageComplexTypeMapping2 = storageComplexTypeMapping;
								StorageConditionPropertyMapping conditionPropertyMap = storageConditionPropertyMapping;
								Action<EdmMember> duplicateMemberConditionError;
								if ((duplicateMemberConditionError = <>9__0) == null)
								{
									duplicateMemberConditionError = (<>9__0 = delegate(EdmMember member)
									{
										StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Duplicate_Condition_Member), member.Name, StorageMappingErrorCode.ConditionError, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
									});
								}
								storageComplexTypeMapping2.AddConditionProperty(conditionPropertyMap, duplicateMemberConditionError);
							}
						}
						else
						{
							StorageComplexPropertyMapping storageComplexPropertyMapping = this.LoadComplexPropertyMapping(nav.Clone(), ownerType, tableType);
							if (storageComplexPropertyMapping != null)
							{
								storageComplexTypeMapping.AddProperty(storageComplexPropertyMapping);
							}
						}
					}
					else
					{
						StorageScalarPropertyMapping storageScalarPropertyMapping = this.LoadScalarPropertyMapping(nav.Clone(), ownerType, tableType);
						if (storageScalarPropertyMapping != null)
						{
							storageComplexTypeMapping.AddProperty(storageScalarPropertyMapping);
						}
					}
					if (!nav.MoveToNext(XPathNodeType.Element))
					{
						return storageComplexTypeMapping;
					}
				}
				throw Error.NotSupported();
			}
			return storageComplexTypeMapping;
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x00089580 File Offset: 0x00087780
		private StorageEndPropertyMapping LoadEndPropertyMapping(XPathNavigator nav, AssociationEndMember end, EntityType tableType)
		{
			StorageEndPropertyMapping storageEndPropertyMapping = new StorageEndPropertyMapping(null);
			storageEndPropertyMapping.EndMember = end;
			nav.MoveToChild(XPathNodeType.Element);
			StorageScalarPropertyMapping storageScalarPropertyMapping;
			for (;;)
			{
				string localName = nav.LocalName;
				if (localName == "ScalarProperty")
				{
					RefType refType = end.TypeUsage.EdmType as RefType;
					EntityTypeBase elementType = refType.ElementType;
					storageScalarPropertyMapping = this.LoadScalarPropertyMapping(nav.Clone(), elementType, tableType.Properties);
					if (storageScalarPropertyMapping != null)
					{
						if (!elementType.KeyMembers.Contains(storageScalarPropertyMapping.EdmProperty))
						{
							break;
						}
						storageEndPropertyMapping.AddProperty(storageScalarPropertyMapping);
					}
				}
				if (!nav.MoveToNext(XPathNodeType.Element))
				{
					return storageEndPropertyMapping;
				}
			}
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_EndProperty), storageScalarPropertyMapping.EdmProperty.Name, StorageMappingErrorCode.InvalidEdmMember, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
			return null;
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x0008964C File Offset: 0x0008784C
		private StorageConditionPropertyMapping LoadConditionPropertyMapping(XPathNavigator nav, EdmType containerType, ReadOnlyMetadataCollection<EdmProperty> tableProperties)
		{
			string aliasResolvedAttributeValue = this.GetAliasResolvedAttributeValue(nav.Clone(), "Name");
			string aliasResolvedAttributeValue2 = this.GetAliasResolvedAttributeValue(nav.Clone(), "ColumnName");
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			if (aliasResolvedAttributeValue != null && aliasResolvedAttributeValue2 != null)
			{
				StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_ConditionMapping_Both_Members, StorageMappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return null;
			}
			if (aliasResolvedAttributeValue == null && aliasResolvedAttributeValue2 == null)
			{
				StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_ConditionMapping_Either_Members, StorageMappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return null;
			}
			EdmProperty edmProperty = null;
			if (aliasResolvedAttributeValue != null && containerType != null)
			{
				EdmMember edmMember;
				((StructuralType)containerType).Members.TryGetValue(aliasResolvedAttributeValue, false, out edmMember);
				edmProperty = (edmMember as EdmProperty);
			}
			EdmProperty edmProperty2 = null;
			if (aliasResolvedAttributeValue2 != null)
			{
				tableProperties.TryGetValue(aliasResolvedAttributeValue2, false, out edmProperty2);
			}
			EdmProperty edmProperty3 = (edmProperty2 != null) ? edmProperty2 : edmProperty;
			if (edmProperty3 == null)
			{
				StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_ConditionMapping_InvalidMember), (aliasResolvedAttributeValue2 != null) ? aliasResolvedAttributeValue2 : aliasResolvedAttributeValue, StorageMappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return null;
			}
			bool? isNull = null;
			object obj = null;
			string attributeValue = StorageMappingItemLoader.GetAttributeValue(nav.Clone(), "IsNull");
			EdmType edmType = edmProperty3.TypeUsage.EdmType;
			if (Helper.IsPrimitiveType(edmType))
			{
				TypeUsage typeUsage;
				if (edmProperty3.DeclaringType.DataSpace == DataSpace.SSpace)
				{
					typeUsage = this.StoreItemCollection.StoreProviderManifest.GetEdmType(edmProperty3.TypeUsage);
					if (typeUsage == null)
					{
						StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_ProviderReturnsNullType(edmProperty3.Name), StorageMappingErrorCode.MappingStoreProviderReturnsNullEdmType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
						return null;
					}
				}
				else
				{
					typeUsage = edmProperty3.TypeUsage;
				}
				PrimitiveType primitiveType = (PrimitiveType)typeUsage.EdmType;
				Type clrEquivalentType = primitiveType.ClrEquivalentType;
				PrimitiveTypeKind primitiveTypeKind = primitiveType.PrimitiveTypeKind;
				if (attributeValue == null && !StorageMappingItemLoader.IsTypeSupportedForCondition(primitiveTypeKind))
				{
					StorageMappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_InvalidContent_ConditionMapping_InvalidPrimitiveTypeKind), edmProperty3.Name, edmType.FullName, StorageMappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					return null;
				}
				if (!StorageMappingItemLoader.TryGetTypedAttributeValue(nav.Clone(), "Value", clrEquivalentType, this.m_sourceLocation, this.m_parsingErrors, out obj))
				{
					return null;
				}
			}
			else
			{
				if (!Helper.IsEnumType(edmType))
				{
					StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_ConditionMapping_NonScalar, StorageMappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					return null;
				}
				obj = StorageMappingItemLoader.GetEnumAttributeValue(nav.Clone(), "Value", (EnumType)edmType, this.m_sourceLocation, this.m_parsingErrors);
			}
			if (attributeValue != null && obj != null)
			{
				StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_ConditionMapping_Both_Values, StorageMappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return null;
			}
			if (attributeValue == null && obj == null)
			{
				StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_ConditionMapping_Either_Values, StorageMappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return null;
			}
			if (attributeValue != null)
			{
				isNull = new bool?(Convert.ToBoolean(attributeValue, CultureInfo.InvariantCulture));
			}
			if (edmProperty2 != null && (edmProperty2.IsStoreGeneratedComputed || edmProperty2.IsStoreGeneratedIdentity))
			{
				StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_ConditionMapping_Computed), edmProperty2.Name, StorageMappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return null;
			}
			return new StorageConditionPropertyMapping(edmProperty, edmProperty2, obj, isNull);
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x00089958 File Offset: 0x00087B58
		internal static bool IsTypeSupportedForCondition(PrimitiveTypeKind primitiveTypeKind)
		{
			switch (primitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
			case PrimitiveTypeKind.DateTime:
			case PrimitiveTypeKind.Decimal:
			case PrimitiveTypeKind.Double:
			case PrimitiveTypeKind.Guid:
			case PrimitiveTypeKind.Single:
			case PrimitiveTypeKind.Time:
			case PrimitiveTypeKind.DateTimeOffset:
				return false;
			case PrimitiveTypeKind.Boolean:
			case PrimitiveTypeKind.Byte:
			case PrimitiveTypeKind.SByte:
			case PrimitiveTypeKind.Int16:
			case PrimitiveTypeKind.Int32:
			case PrimitiveTypeKind.Int64:
			case PrimitiveTypeKind.String:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000899B0 File Offset: 0x00087BB0
		private static XmlSchemaSet GetOrCreateSchemaSet()
		{
			if (StorageMappingItemLoader.s_mappingXmlSchema == null)
			{
				XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
				StorageMappingItemLoader.AddResourceXsdToSchemaSet(xmlSchemaSet, "System.Data.Resources.CSMSL_1.xsd");
				StorageMappingItemLoader.AddResourceXsdToSchemaSet(xmlSchemaSet, "System.Data.Resources.CSMSL_2.xsd");
				StorageMappingItemLoader.AddResourceXsdToSchemaSet(xmlSchemaSet, "System.Data.Resources.CSMSL_3.xsd");
				Interlocked.CompareExchange<XmlSchemaSet>(ref StorageMappingItemLoader.s_mappingXmlSchema, xmlSchemaSet, null);
			}
			return StorageMappingItemLoader.s_mappingXmlSchema;
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x00089A00 File Offset: 0x00087C00
		private static void AddResourceXsdToSchemaSet(XmlSchemaSet set, string resourceName)
		{
			using (XmlReader xmlResource = DbProviderServices.GetXmlResource(resourceName))
			{
				XmlSchema schema = XmlSchema.Read(xmlResource, null);
				set.Add(schema);
			}
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x00089A40 File Offset: 0x00087C40
		private static void AddToSchemaErrors(string message, StorageMappingErrorCode errorCode, string location, IXmlLineInfo lineInfo, IList<EdmSchemaError> parsingErrors)
		{
			EdmSchemaError item = new EdmSchemaError(message, (int)errorCode, EdmSchemaErrorSeverity.Error, location, lineInfo.LineNumber, lineInfo.LinePosition);
			parsingErrors.Add(item);
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x00089A6C File Offset: 0x00087C6C
		private static EdmSchemaError AddToSchemaErrorsWithMemberInfo(Func<object, string> messageFormat, string errorMember, StorageMappingErrorCode errorCode, string location, IXmlLineInfo lineInfo, IList<EdmSchemaError> parsingErrors)
		{
			EdmSchemaError edmSchemaError = new EdmSchemaError(messageFormat(errorMember), (int)errorCode, EdmSchemaErrorSeverity.Error, location, lineInfo.LineNumber, lineInfo.LinePosition);
			parsingErrors.Add(edmSchemaError);
			return edmSchemaError;
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x00089AA0 File Offset: 0x00087CA0
		private static void AddToSchemaErrorWithMemberAndStructure(Func<object, object, string> messageFormat, string errorMember, string errorStructure, StorageMappingErrorCode errorCode, string location, IXmlLineInfo lineInfo, IList<EdmSchemaError> parsingErrors)
		{
			EdmSchemaError item = new EdmSchemaError(messageFormat(errorMember, errorStructure), (int)errorCode, EdmSchemaErrorSeverity.Error, location, lineInfo.LineNumber, lineInfo.LinePosition);
			parsingErrors.Add(item);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x00089AD8 File Offset: 0x00087CD8
		private static void AddToSchemaErrorWithMessage(string errorMessage, StorageMappingErrorCode errorCode, string location, IXmlLineInfo lineInfo, IList<EdmSchemaError> parsingErrors)
		{
			EdmSchemaError item = new EdmSchemaError(errorMessage, (int)errorCode, EdmSchemaErrorSeverity.Error, location, lineInfo.LineNumber, lineInfo.LinePosition);
			parsingErrors.Add(item);
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x00089B03 File Offset: 0x00087D03
		private string GetAliasResolvedAttributeValue(XPathNavigator nav, string attributeName)
		{
			return this.GetAliasResolvedValue(StorageMappingItemLoader.GetAttributeValue(nav, attributeName));
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x00089B14 File Offset: 0x00087D14
		private bool GetBoolAttributeValue(XPathNavigator nav, string attributeName, bool defaultValue)
		{
			bool result = defaultValue;
			object typedAttributeValue = Helper.GetTypedAttributeValue(nav, attributeName, typeof(bool));
			if (typedAttributeValue != null)
			{
				result = (bool)typedAttributeValue;
			}
			return result;
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x00089B40 File Offset: 0x00087D40
		private static string GetAttributeValue(XPathNavigator nav, string attributeName)
		{
			return Helper.GetAttributeValue(nav, attributeName);
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x00089B4C File Offset: 0x00087D4C
		private static bool TryGetTypedAttributeValue(XPathNavigator nav, string attributeName, Type clrType, string sourceLocation, IList<EdmSchemaError> parsingErrors, out object value)
		{
			value = null;
			try
			{
				value = Helper.GetTypedAttributeValue(nav, attributeName, clrType);
			}
			catch (FormatException)
			{
				StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_ConditionValueTypeMismatch, StorageMappingErrorCode.ConditionError, sourceLocation, (IXmlLineInfo)nav, parsingErrors);
				return false;
			}
			return true;
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x00089B9C File Offset: 0x00087D9C
		private static EnumMember GetEnumAttributeValue(XPathNavigator nav, string attributeName, EnumType enumType, string sourceLocation, IList<EdmSchemaError> parsingErrors)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			string attributeValue = StorageMappingItemLoader.GetAttributeValue(nav, attributeName);
			if (string.IsNullOrEmpty(attributeValue))
			{
				StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_Enum_EmptyValue), enumType.FullName, StorageMappingErrorCode.InvalidEnumValue, sourceLocation, lineInfo, parsingErrors);
			}
			EnumMember result;
			if (!enumType.Members.TryGetValue(attributeValue, false, out result))
			{
				StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_Enum_InvalidValue), attributeValue, StorageMappingErrorCode.InvalidEnumValue, sourceLocation, lineInfo, parsingErrors);
			}
			return result;
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x00089C14 File Offset: 0x00087E14
		private string GetAliasResolvedValue(string aliasedString)
		{
			if (aliasedString == null || aliasedString.Length == 0)
			{
				return aliasedString;
			}
			int num = aliasedString.LastIndexOf('.');
			if (num == -1)
			{
				return aliasedString;
			}
			string key = aliasedString.Substring(0, num);
			string text;
			this.m_alias.TryGetValue(key, out text);
			if (text != null)
			{
				aliasedString = text + aliasedString.Substring(num);
			}
			return aliasedString;
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x00089C68 File Offset: 0x00087E68
		private XmlReader GetSchemaValidatingReader(XmlReader innerReader)
		{
			XmlReaderSettings xmlReaderSettings = this.GetXmlReaderSettings();
			return XmlReader.Create(innerReader, xmlReaderSettings);
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x00089C88 File Offset: 0x00087E88
		private XmlReaderSettings GetXmlReaderSettings()
		{
			XmlReaderSettings xmlReaderSettings = Schema.CreateEdmStandardXmlReaderSettings();
			xmlReaderSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
			xmlReaderSettings.ValidationEventHandler += this.XsdValidationCallBack;
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			xmlReaderSettings.Schemas = StorageMappingItemLoader.GetOrCreateSchemaSet();
			return xmlReaderSettings;
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x00089CD0 File Offset: 0x00087ED0
		private void XsdValidationCallBack(object sender, ValidationEventArgs args)
		{
			if (args.Severity != XmlSeverityType.Warning)
			{
				string schemaLocation = null;
				if (!string.IsNullOrEmpty(args.Exception.SourceUri))
				{
					schemaLocation = Helper.GetFileNameFromUri(new Uri(args.Exception.SourceUri));
				}
				EdmSchemaErrorSeverity severity = EdmSchemaErrorSeverity.Error;
				if (args.Severity == XmlSeverityType.Warning)
				{
					severity = EdmSchemaErrorSeverity.Warning;
				}
				EdmSchemaError item = new EdmSchemaError(Strings.Mapping_InvalidMappingSchema_validation(args.Exception.Message), 2025, severity, schemaLocation, args.Exception.LineNumber, args.Exception.LinePosition);
				this.m_parsingErrors.Add(item);
			}
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x00089D5C File Offset: 0x00087F5C
		private void ValidateAndUpdateScalarMemberMapping(EdmProperty member, EdmProperty columnMember, IXmlLineInfo lineInfo)
		{
			KeyValuePair<TypeUsage, TypeUsage> keyValuePair;
			if (!this.m_scalarMemberMappings.TryGetValue(member, out keyValuePair))
			{
				int count = this.m_parsingErrors.Count;
				TypeUsage typeUsage = Helper.ValidateAndConvertTypeUsage(member, columnMember, lineInfo, this.m_sourceLocation, this.m_parsingErrors, this.StoreItemCollection);
				if (typeUsage != null)
				{
					this.m_scalarMemberMappings.Add(member, new KeyValuePair<TypeUsage, TypeUsage>(typeUsage, columnMember.TypeUsage));
					return;
				}
				if (count == this.m_parsingErrors.Count)
				{
					EdmSchemaError item = new EdmSchemaError(this.GetInvalidMemberMappingErrorMessage(member, columnMember), 2019, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, lineInfo.LineNumber, lineInfo.LinePosition);
					this.m_parsingErrors.Add(item);
					return;
				}
			}
			else
			{
				TypeUsage value = keyValuePair.Value;
				TypeUsage modelTypeUsage = columnMember.TypeUsage.GetModelTypeUsage();
				if (columnMember.TypeUsage.EdmType != value.EdmType)
				{
					EdmSchemaError item2 = new EdmSchemaError(Strings.Mapping_StoreTypeMismatch_ScalarPropertyMapping(member.Name, value.EdmType.Name), 2039, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, lineInfo.LineNumber, lineInfo.LinePosition);
					this.m_parsingErrors.Add(item2);
					return;
				}
				if (!TypeSemantics.IsSubTypeOf(StorageMappingItemLoader.ResolveTypeUsageForEnums(member.TypeUsage), modelTypeUsage))
				{
					EdmSchemaError item3 = new EdmSchemaError(this.GetInvalidMemberMappingErrorMessage(member, columnMember), 2019, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, lineInfo.LineNumber, lineInfo.LinePosition);
					this.m_parsingErrors.Add(item3);
				}
			}
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x00089EBC File Offset: 0x000880BC
		private string GetInvalidMemberMappingErrorMessage(EdmMember cSpaceMember, EdmMember sSpaceMember)
		{
			EdmType edmType = cSpaceMember.TypeUsage.EdmType;
			object p = ((edmType != null) ? edmType.ToString() : null) + this.GetFacetsForDisplay(cSpaceMember.TypeUsage);
			object name = cSpaceMember.Name;
			object fullName = cSpaceMember.DeclaringType.FullName;
			EdmType edmType2 = sSpaceMember.TypeUsage.EdmType;
			return Strings.Mapping_Invalid_Member_Mapping(p, name, fullName, ((edmType2 != null) ? edmType2.ToString() : null) + this.GetFacetsForDisplay(sSpaceMember.TypeUsage), sSpaceMember.Name, sSpaceMember.DeclaringType.FullName);
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x00089F40 File Offset: 0x00088140
		private string GetFacetsForDisplay(TypeUsage typeUsage)
		{
			ReadOnlyMetadataCollection<Facet> facets = typeUsage.Facets;
			if (facets == null || facets.Count == 0)
			{
				return string.Empty;
			}
			int count = facets.Count;
			StringBuilder stringBuilder = new StringBuilder("[");
			for (int i = 0; i < count - 1; i++)
			{
				stringBuilder.AppendFormat("{0}={1},", facets[i].Name, facets[i].Value ?? string.Empty);
			}
			stringBuilder.AppendFormat("{0}={1}]", facets[count - 1].Name, facets[count - 1].Value ?? string.Empty);
			return stringBuilder.ToString();
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x00089FE9 File Offset: 0x000881E9
		private static TypeUsage ResolveTypeUsageForEnums(TypeUsage typeUsage)
		{
			if (!Helper.IsEnumType(typeUsage.EdmType))
			{
				return typeUsage;
			}
			return TypeUsage.Create(Helper.GetUnderlyingEdmTypeForEnumType(typeUsage.EdmType), typeUsage.Facets);
		}

		// Token: 0x040010AF RID: 4271
		private Dictionary<string, string> m_alias;

		// Token: 0x040010B0 RID: 4272
		private StorageMappingItemCollection m_storageMappingItemCollection;

		// Token: 0x040010B1 RID: 4273
		private string m_sourceLocation;

		// Token: 0x040010B2 RID: 4274
		private List<EdmSchemaError> m_parsingErrors;

		// Token: 0x040010B3 RID: 4275
		private Dictionary<EdmMember, KeyValuePair<TypeUsage, TypeUsage>> m_scalarMemberMappings;

		// Token: 0x040010B4 RID: 4276
		private bool m_hasQueryViews;

		// Token: 0x040010B5 RID: 4277
		private string m_currentNamespaceUri;

		// Token: 0x040010B6 RID: 4278
		private StorageEntityContainerMapping m_containerMapping;

		// Token: 0x040010B7 RID: 4279
		private double m_version;

		// Token: 0x040010B8 RID: 4280
		private static XmlSchemaSet s_mappingXmlSchema;

		// Token: 0x02000577 RID: 1399
		private class ModificationFunctionMappingLoader
		{
			// Token: 0x06003FC1 RID: 16321 RVA: 0x000EB100 File Offset: 0x000E9300
			internal ModificationFunctionMappingLoader(StorageMappingItemLoader parentLoader, EntitySetBase extent)
			{
				this.m_parentLoader = EntityUtil.CheckArgumentNull<StorageMappingItemLoader>(parentLoader, "parentLoader");
				this.m_modelContainer = EntityUtil.CheckArgumentNull<EntitySetBase>(extent, "extent").EntityContainer;
				this.m_edmItemCollection = parentLoader.EdmItemCollection;
				this.m_storeItemCollection = parentLoader.StoreItemCollection;
				this.m_entitySet = (extent as EntitySet);
				if (this.m_entitySet == null)
				{
					this.m_associationSet = (AssociationSet)extent;
				}
				this.m_seenParameters = new Set<FunctionParameter>();
				this.m_members = new Stack<EdmMember>();
			}

			// Token: 0x06003FC2 RID: 16322 RVA: 0x000EB188 File Offset: 0x000E9388
			internal StorageModificationFunctionMapping LoadEntityTypeModificationFunctionMapping(XPathNavigator nav, EntitySetBase entitySet, bool allowCurrentVersion, bool allowOriginalVersion, EntityType entityType)
			{
				FunctionParameter rowsAffectedParameter;
				this.m_function = this.LoadAndValidateFunctionMetadata(nav.Clone(), out rowsAffectedParameter);
				if (this.m_function == null)
				{
					return null;
				}
				this.m_allowCurrentVersion = allowCurrentVersion;
				this.m_allowOriginalVersion = allowOriginalVersion;
				IEnumerable<StorageModificationFunctionParameterBinding> parameterBindings = this.LoadParameterBindings(nav.Clone(), entityType);
				IEnumerable<StorageModificationFunctionResultBinding> resultBindings = this.LoadResultBindings(nav.Clone(), entityType);
				return new StorageModificationFunctionMapping(entitySet, entityType, this.m_function, parameterBindings, rowsAffectedParameter, resultBindings);
			}

			// Token: 0x06003FC3 RID: 16323 RVA: 0x000EB1F4 File Offset: 0x000E93F4
			internal StorageModificationFunctionMapping LoadAssociationSetModificationFunctionMapping(XPathNavigator nav, EntitySetBase entitySet, bool isInsert)
			{
				FunctionParameter rowsAffectedParameter;
				this.m_function = this.LoadAndValidateFunctionMetadata(nav.Clone(), out rowsAffectedParameter);
				if (this.m_function == null)
				{
					return null;
				}
				if (isInsert)
				{
					this.m_allowCurrentVersion = true;
					this.m_allowOriginalVersion = false;
				}
				else
				{
					this.m_allowCurrentVersion = false;
					this.m_allowOriginalVersion = true;
				}
				IEnumerable<StorageModificationFunctionParameterBinding> parameterBindings = this.LoadParameterBindings(nav.Clone(), this.m_associationSet.ElementType);
				return new StorageModificationFunctionMapping(entitySet, entitySet.ElementType, this.m_function, parameterBindings, rowsAffectedParameter, null);
			}

			// Token: 0x06003FC4 RID: 16324 RVA: 0x000EB270 File Offset: 0x000E9470
			private IEnumerable<StorageModificationFunctionResultBinding> LoadResultBindings(XPathNavigator nav, EntityType entityType)
			{
				List<StorageModificationFunctionResultBinding> list = new List<StorageModificationFunctionResultBinding>();
				IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
				if (nav.MoveToChild(XPathNodeType.Element))
				{
					string aliasResolvedAttributeValue;
					for (;;)
					{
						if (nav.LocalName == "ResultBinding")
						{
							aliasResolvedAttributeValue = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "Name");
							string aliasResolvedAttributeValue2 = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "ColumnName");
							EdmProperty property = null;
							if (aliasResolvedAttributeValue == null || !entityType.Properties.TryGetValue(aliasResolvedAttributeValue, false, out property))
							{
								break;
							}
							StorageModificationFunctionResultBinding item = new StorageModificationFunctionResultBinding(aliasResolvedAttributeValue2, property);
							list.Add(item);
						}
						if (!nav.MoveToNext(XPathNodeType.Element))
						{
							goto IL_CD;
						}
					}
					StorageMappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_ModificationFunction_PropertyNotFound), aliasResolvedAttributeValue, entityType.Name, StorageMappingErrorCode.InvalidEdmMember, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return new List<StorageModificationFunctionResultBinding>();
				}
				IL_CD:
				KeyToListMap<EdmProperty, string> keyToListMap = new KeyToListMap<EdmProperty, string>(EqualityComparer<EdmProperty>.Default);
				foreach (StorageModificationFunctionResultBinding storageModificationFunctionResultBinding in list)
				{
					keyToListMap.Add(storageModificationFunctionResultBinding.Property, storageModificationFunctionResultBinding.ColumnName);
				}
				foreach (EdmProperty edmProperty in keyToListMap.Keys)
				{
					ReadOnlyCollection<string> readOnlyCollection = keyToListMap.ListForKey(edmProperty);
					if (1 < readOnlyCollection.Count)
					{
						StorageMappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_ModificationFunction_AmbiguousResultBinding), edmProperty.Name, StringUtil.ToCommaSeparatedString(readOnlyCollection), StorageMappingErrorCode.AmbiguousResultBindingInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
						return new List<StorageModificationFunctionResultBinding>();
					}
				}
				return list;
			}

			// Token: 0x06003FC5 RID: 16325 RVA: 0x000EB43C File Offset: 0x000E963C
			private IEnumerable<StorageModificationFunctionParameterBinding> LoadParameterBindings(XPathNavigator nav, StructuralType type)
			{
				List<StorageModificationFunctionParameterBinding> result = new List<StorageModificationFunctionParameterBinding>(this.LoadParameterBindings(nav.Clone(), type, false));
				Set<FunctionParameter> set = new Set<FunctionParameter>(this.m_function.Parameters);
				set.Subtract(this.m_seenParameters);
				if (set.Count != 0)
				{
					StorageMappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_ModificationFunction_MissingParameter), this.m_function.FullName, StringUtil.ToCommaSeparatedString(set), StorageMappingErrorCode.InvalidParameterInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, (IXmlLineInfo)nav, this.m_parentLoader.m_parsingErrors);
					return new List<StorageModificationFunctionParameterBinding>();
				}
				return result;
			}

			// Token: 0x06003FC6 RID: 16326 RVA: 0x000EB4CC File Offset: 0x000E96CC
			private IEnumerable<StorageModificationFunctionParameterBinding> LoadParameterBindings(XPathNavigator nav, StructuralType type, bool restrictToKeyMembers)
			{
				if (nav.MoveToChild(XPathNodeType.Element))
				{
					for (;;)
					{
						string localName = nav.LocalName;
						if (!(localName == "ScalarProperty"))
						{
							if (!(localName == "ComplexProperty"))
							{
								if (!(localName == "AssociationEnd"))
								{
									if (localName == "EndProperty")
									{
										AssociationSetEnd associationSetEnd = this.LoadEndProperty(nav.Clone());
										if (associationSetEnd != null)
										{
											this.m_members.Push(associationSetEnd.CorrespondingAssociationEndMember);
											foreach (StorageModificationFunctionParameterBinding storageModificationFunctionParameterBinding in this.LoadParameterBindings(nav.Clone(), associationSetEnd.EntitySet.ElementType, true))
											{
												yield return storageModificationFunctionParameterBinding;
											}
											IEnumerator<StorageModificationFunctionParameterBinding> enumerator = null;
											this.m_members.Pop();
										}
									}
								}
								else
								{
									AssociationSetEnd associationSetEnd2 = this.LoadAssociationEnd(nav.Clone());
									if (associationSetEnd2 != null)
									{
										this.m_members.Push(associationSetEnd2.CorrespondingAssociationEndMember);
										this.m_associationSetNavigation = associationSetEnd2.ParentAssociationSet;
										foreach (StorageModificationFunctionParameterBinding storageModificationFunctionParameterBinding2 in this.LoadParameterBindings(nav.Clone(), associationSetEnd2.EntitySet.ElementType, true))
										{
											yield return storageModificationFunctionParameterBinding2;
										}
										IEnumerator<StorageModificationFunctionParameterBinding> enumerator = null;
										this.m_associationSetNavigation = null;
										this.m_members.Pop();
									}
								}
							}
							else
							{
								ComplexType type2;
								EdmMember edmMember = this.LoadComplexTypeProperty(nav.Clone(), type, out type2);
								if (edmMember != null)
								{
									this.m_members.Push(edmMember);
									foreach (StorageModificationFunctionParameterBinding storageModificationFunctionParameterBinding3 in this.LoadParameterBindings(nav.Clone(), type2, restrictToKeyMembers))
									{
										yield return storageModificationFunctionParameterBinding3;
									}
									IEnumerator<StorageModificationFunctionParameterBinding> enumerator = null;
									this.m_members.Pop();
								}
							}
						}
						else
						{
							StorageModificationFunctionParameterBinding storageModificationFunctionParameterBinding4 = this.LoadScalarPropertyParameterBinding(nav.Clone(), type, restrictToKeyMembers);
							if (storageModificationFunctionParameterBinding4 == null)
							{
								break;
							}
							yield return storageModificationFunctionParameterBinding4;
						}
						if (!nav.MoveToNext(XPathNodeType.Element))
						{
							goto IL_318;
						}
					}
					yield break;
				}
				IL_318:
				yield break;
				yield break;
			}

			// Token: 0x06003FC7 RID: 16327 RVA: 0x000EB4F4 File Offset: 0x000E96F4
			private AssociationSetEnd LoadAssociationEnd(XPathNavigator nav)
			{
				IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
				string aliasResolvedAttributeValue = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "AssociationSet");
				string aliasResolvedAttributeValue2 = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "From");
				string aliasResolvedAttributeValue3 = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "To");
				RelationshipSet relationshipSet = null;
				if (aliasResolvedAttributeValue == null || !this.m_modelContainer.TryGetRelationshipSetByName(aliasResolvedAttributeValue, false, out relationshipSet) || BuiltInTypeKind.AssociationSet != relationshipSet.BuiltInTypeKind)
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AssociationSetDoesNotExist), aliasResolvedAttributeValue, StorageMappingErrorCode.InvalidAssociationSet, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				AssociationSet associationSet = (AssociationSet)relationshipSet;
				AssociationSetEnd associationSetEnd = null;
				if (aliasResolvedAttributeValue2 == null || !associationSet.AssociationSetEnds.TryGetValue(aliasResolvedAttributeValue2, false, out associationSetEnd))
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AssociationSetRoleDoesNotExist), aliasResolvedAttributeValue2, StorageMappingErrorCode.InvalidAssociationSetRoleInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				AssociationSetEnd associationSetEnd2 = null;
				if (aliasResolvedAttributeValue3 == null || !associationSet.AssociationSetEnds.TryGetValue(aliasResolvedAttributeValue3, false, out associationSetEnd2))
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AssociationSetRoleDoesNotExist), aliasResolvedAttributeValue3, StorageMappingErrorCode.InvalidAssociationSetRoleInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				if (!associationSetEnd.EntitySet.Equals(this.m_entitySet))
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AssociationSetFromRoleIsNotEntitySet), aliasResolvedAttributeValue2, StorageMappingErrorCode.InvalidAssociationSetRoleInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				if (associationSetEnd2.CorrespondingAssociationEndMember.RelationshipMultiplicity != RelationshipMultiplicity.One && associationSetEnd2.CorrespondingAssociationEndMember.RelationshipMultiplicity != RelationshipMultiplicity.ZeroOrOne)
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AssociationSetCardinality), aliasResolvedAttributeValue3, StorageMappingErrorCode.InvalidAssociationSetCardinalityInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				if (associationSet.ElementType.IsForeignKey)
				{
					System.Data.Metadata.Edm.ReferentialConstraint referentialConstraint = associationSet.ElementType.ReferentialConstraints.Single<System.Data.Metadata.Edm.ReferentialConstraint>();
					EdmSchemaError edmSchemaError = StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AssociationEndMappingForeignKeyAssociation), aliasResolvedAttributeValue3, StorageMappingErrorCode.InvalidModificationFunctionMappingAssociationEndForeignKey, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					if (associationSetEnd.CorrespondingAssociationEndMember != referentialConstraint.ToRole || !referentialConstraint.ToProperties.All((EdmProperty p) => this.m_entitySet.ElementType.KeyMembers.Contains(p)))
					{
						return null;
					}
					edmSchemaError.Severity = EdmSchemaErrorSeverity.Warning;
				}
				return associationSetEnd2;
			}

			// Token: 0x06003FC8 RID: 16328 RVA: 0x000EB758 File Offset: 0x000E9958
			private AssociationSetEnd LoadEndProperty(XPathNavigator nav)
			{
				string aliasResolvedAttributeValue = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "Name");
				AssociationSetEnd result = null;
				if (aliasResolvedAttributeValue == null || !this.m_associationSet.AssociationSetEnds.TryGetValue(aliasResolvedAttributeValue, false, out result))
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AssociationSetRoleDoesNotExist), aliasResolvedAttributeValue, StorageMappingErrorCode.InvalidAssociationSetRoleInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, (IXmlLineInfo)nav, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				return result;
			}

			// Token: 0x06003FC9 RID: 16329 RVA: 0x000EB7D0 File Offset: 0x000E99D0
			private EdmMember LoadComplexTypeProperty(XPathNavigator nav, StructuralType type, out ComplexType complexType)
			{
				IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
				string aliasResolvedAttributeValue = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "Name");
				string aliasResolvedAttributeValue2 = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "TypeName");
				EdmMember edmMember = null;
				if (aliasResolvedAttributeValue == null || !type.Members.TryGetValue(aliasResolvedAttributeValue, false, out edmMember))
				{
					StorageMappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_ModificationFunction_PropertyNotFound), aliasResolvedAttributeValue, type.Name, StorageMappingErrorCode.InvalidEdmMember, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					complexType = null;
					return null;
				}
				complexType = null;
				if (aliasResolvedAttributeValue2 == null || !this.m_edmItemCollection.TryGetItem<ComplexType>(aliasResolvedAttributeValue2, out complexType))
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_ComplexTypeNotFound), aliasResolvedAttributeValue2, StorageMappingErrorCode.InvalidComplexType, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				if (!edmMember.TypeUsage.EdmType.Equals(complexType) && !Helper.IsSubtypeOf(edmMember.TypeUsage.EdmType, complexType))
				{
					StorageMappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_ModificationFunction_WrongComplexType), aliasResolvedAttributeValue2, edmMember.Name, StorageMappingErrorCode.InvalidComplexType, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				return edmMember;
			}

			// Token: 0x06003FCA RID: 16330 RVA: 0x000EB908 File Offset: 0x000E9B08
			private StorageModificationFunctionParameterBinding LoadScalarPropertyParameterBinding(XPathNavigator nav, StructuralType type, bool restrictToKeyMembers)
			{
				IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
				string aliasResolvedAttributeValue = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "ParameterName");
				string aliasResolvedAttributeValue2 = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "Name");
				string aliasResolvedAttributeValue3 = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "Version");
				bool flag;
				if (aliasResolvedAttributeValue3 == null)
				{
					if (!this.m_allowOriginalVersion)
					{
						flag = true;
					}
					else
					{
						if (this.m_allowCurrentVersion)
						{
							StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_ModificationFunction_MissingVersion, StorageMappingErrorCode.MissingVersionInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
							return null;
						}
						flag = false;
					}
				}
				else
				{
					flag = (aliasResolvedAttributeValue3 == "Current");
				}
				if (flag && !this.m_allowCurrentVersion)
				{
					StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_ModificationFunction_VersionMustBeOriginal, StorageMappingErrorCode.InvalidVersionInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				if (!flag && !this.m_allowOriginalVersion)
				{
					StorageMappingItemLoader.AddToSchemaErrors(Strings.Mapping_ModificationFunction_VersionMustBeCurrent, StorageMappingErrorCode.InvalidVersionInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				FunctionParameter functionParameter = null;
				if (aliasResolvedAttributeValue == null || !this.m_function.Parameters.TryGetValue(aliasResolvedAttributeValue, false, out functionParameter))
				{
					StorageMappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_ModificationFunction_ParameterNotFound), aliasResolvedAttributeValue, this.m_function.Name, StorageMappingErrorCode.InvalidParameterInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				EdmMember edmMember = null;
				if (restrictToKeyMembers)
				{
					if (aliasResolvedAttributeValue2 == null || !((EntityType)type).KeyMembers.TryGetValue(aliasResolvedAttributeValue2, false, out edmMember))
					{
						StorageMappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_ModificationFunction_PropertyNotKey), aliasResolvedAttributeValue2, type.Name, StorageMappingErrorCode.InvalidEdmMember, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
						return null;
					}
				}
				else if (aliasResolvedAttributeValue2 == null || !type.Members.TryGetValue(aliasResolvedAttributeValue2, false, out edmMember))
				{
					StorageMappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_ModificationFunction_PropertyNotFound), aliasResolvedAttributeValue2, type.Name, StorageMappingErrorCode.InvalidEdmMember, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				if (this.m_seenParameters.Contains(functionParameter))
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_ParameterBoundTwice), aliasResolvedAttributeValue, StorageMappingErrorCode.ParameterBoundTwiceInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				int count = this.m_parentLoader.m_parsingErrors.Count;
				if (Helper.ValidateAndConvertTypeUsage(edmMember, lineInfo, this.m_parentLoader.m_sourceLocation, edmMember.TypeUsage, functionParameter.TypeUsage, this.m_parentLoader.m_parsingErrors, this.m_storeItemCollection) == null && count == this.m_parentLoader.m_parsingErrors.Count)
				{
					StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_ModificationFunction_PropertyParameterTypeMismatch(edmMember.TypeUsage.EdmType, edmMember.Name, edmMember.DeclaringType.FullName, functionParameter.TypeUsage.EdmType, functionParameter.Name, this.m_function.FullName), StorageMappingErrorCode.InvalidModificationFunctionMappingPropertyParameterTypeMismatch, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
				}
				this.m_members.Push(edmMember);
				IEnumerable<EdmMember> members = this.m_members;
				AssociationSet associationSetNavigation = this.m_associationSetNavigation;
				if (this.m_members.Last<EdmMember>().BuiltInTypeKind == BuiltInTypeKind.AssociationEndMember)
				{
					AssociationEndMember associationEndMember = (AssociationEndMember)this.m_members.Last<EdmMember>();
					AssociationType associationType = (AssociationType)associationEndMember.DeclaringType;
					if (associationType.IsForeignKey)
					{
						System.Data.Metadata.Edm.ReferentialConstraint referentialConstraint = associationType.ReferentialConstraints.Single<System.Data.Metadata.Edm.ReferentialConstraint>();
						if (referentialConstraint.FromRole == associationEndMember)
						{
							int index = referentialConstraint.FromProperties.IndexOf((EdmProperty)this.m_members.First<EdmMember>());
							members = new EdmMember[]
							{
								referentialConstraint.ToProperties[index]
							};
							associationSetNavigation = null;
						}
					}
				}
				StorageModificationFunctionParameterBinding result = new StorageModificationFunctionParameterBinding(functionParameter, new StorageModificationFunctionMemberPath(members, associationSetNavigation), flag);
				this.m_members.Pop();
				this.m_seenParameters.Add(functionParameter);
				return result;
			}

			// Token: 0x06003FCB RID: 16331 RVA: 0x000EBCF4 File Offset: 0x000E9EF4
			private EdmFunction LoadAndValidateFunctionMetadata(XPathNavigator nav, out FunctionParameter rowsAffectedParameter)
			{
				IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
				this.m_seenParameters.Clear();
				string aliasResolvedAttributeValue = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "FunctionName");
				rowsAffectedParameter = null;
				ReadOnlyCollection<EdmFunction> functions = this.m_storeItemCollection.GetFunctions(aliasResolvedAttributeValue);
				if (functions.Count == 0)
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_UnknownFunction), aliasResolvedAttributeValue, StorageMappingErrorCode.InvalidModificationFunctionMappingUnknownFunction, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				if (1 < functions.Count)
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AmbiguousFunction), aliasResolvedAttributeValue, StorageMappingErrorCode.InvalidModificationFunctionMappingAmbiguousFunction, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				EdmFunction edmFunction = functions[0];
				if (MetadataHelper.IsComposable(edmFunction))
				{
					StorageMappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_NotValidFunction), aliasResolvedAttributeValue, StorageMappingErrorCode.InvalidModificationFunctionMappingNotValidFunction, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				string attributeValue = StorageMappingItemLoader.GetAttributeValue(nav, "RowsAffectedParameter");
				if (!string.IsNullOrEmpty(attributeValue))
				{
					if (!edmFunction.Parameters.TryGetValue(attributeValue, false, out rowsAffectedParameter))
					{
						StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_RowsAffectedParameterDoesNotExist(attributeValue, edmFunction.FullName), StorageMappingErrorCode.MappingFunctionImportRowsAffectedParameterDoesNotExist, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
						return null;
					}
					if (ParameterMode.Out != rowsAffectedParameter.Mode && ParameterMode.InOut != rowsAffectedParameter.Mode)
					{
						StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_RowsAffectedParameterHasWrongMode(attributeValue, rowsAffectedParameter.Mode, ParameterMode.Out, ParameterMode.InOut), StorageMappingErrorCode.MappingFunctionImportRowsAffectedParameterHasWrongMode, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
						return null;
					}
					PrimitiveType primitiveType = (PrimitiveType)rowsAffectedParameter.TypeUsage.EdmType;
					if (!TypeSemantics.IsIntegerNumericType(rowsAffectedParameter.TypeUsage))
					{
						StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_RowsAffectedParameterHasWrongType(attributeValue, primitiveType.PrimitiveTypeKind), StorageMappingErrorCode.MappingFunctionImportRowsAffectedParameterHasWrongType, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
						return null;
					}
					this.m_seenParameters.Add(rowsAffectedParameter);
				}
				foreach (FunctionParameter functionParameter in edmFunction.Parameters)
				{
					if (functionParameter.Mode != ParameterMode.In && attributeValue != functionParameter.Name)
					{
						StorageMappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_ModificationFunction_NotValidFunctionParameter(aliasResolvedAttributeValue, functionParameter.Name, "RowsAffectedParameter"), StorageMappingErrorCode.InvalidModificationFunctionMappingNotValidFunctionParameter, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
						return null;
					}
				}
				return edmFunction;
			}

			// Token: 0x04001C6A RID: 7274
			private readonly StorageMappingItemLoader m_parentLoader;

			// Token: 0x04001C6B RID: 7275
			private EdmFunction m_function;

			// Token: 0x04001C6C RID: 7276
			private readonly EntitySet m_entitySet;

			// Token: 0x04001C6D RID: 7277
			private readonly AssociationSet m_associationSet;

			// Token: 0x04001C6E RID: 7278
			private readonly System.Data.Metadata.Edm.EntityContainer m_modelContainer;

			// Token: 0x04001C6F RID: 7279
			private readonly EdmItemCollection m_edmItemCollection;

			// Token: 0x04001C70 RID: 7280
			private readonly StoreItemCollection m_storeItemCollection;

			// Token: 0x04001C71 RID: 7281
			private bool m_allowCurrentVersion;

			// Token: 0x04001C72 RID: 7282
			private bool m_allowOriginalVersion;

			// Token: 0x04001C73 RID: 7283
			private readonly Set<FunctionParameter> m_seenParameters;

			// Token: 0x04001C74 RID: 7284
			private readonly Stack<EdmMember> m_members;

			// Token: 0x04001C75 RID: 7285
			private AssociationSet m_associationSetNavigation;
		}
	}
}
