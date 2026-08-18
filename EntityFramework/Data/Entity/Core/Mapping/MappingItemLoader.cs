using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.SchemaObjectModel;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003E4 RID: 996
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal class MappingItemLoader
	{
		// Token: 0x060024A0 RID: 9376 RVA: 0x000A8CD8 File Offset: 0x000A6ED8
		internal MappingItemLoader(XmlReader reader, StorageMappingItemCollection storageMappingItemCollection, string fileName, Dictionary<EdmMember, KeyValuePair<TypeUsage, TypeUsage>> scalarMemberMappings)
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

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060024A1 RID: 9377 RVA: 0x000A8D90 File Offset: 0x000A6F90
		internal double MappingVersion
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060024A2 RID: 9378 RVA: 0x000A8D98 File Offset: 0x000A6F98
		internal IList<EdmSchemaError> ParsingErrors
		{
			get
			{
				return this.m_parsingErrors;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060024A3 RID: 9379 RVA: 0x000A8DA0 File Offset: 0x000A6FA0
		internal bool HasQueryViews
		{
			get
			{
				return this.m_hasQueryViews;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060024A4 RID: 9380 RVA: 0x000A8DA8 File Offset: 0x000A6FA8
		internal EntityContainerMapping ContainerMapping
		{
			get
			{
				return this.m_containerMapping;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060024A5 RID: 9381 RVA: 0x000A8DB0 File Offset: 0x000A6FB0
		private EdmItemCollection EdmItemCollection
		{
			get
			{
				return this.m_storageMappingItemCollection.EdmItemCollection;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060024A6 RID: 9382 RVA: 0x000A8DBD File Offset: 0x000A6FBD
		private StoreItemCollection StoreItemCollection
		{
			get
			{
				return this.m_storageMappingItemCollection.StoreItemCollection;
			}
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000A8DCC File Offset: 0x000A6FCC
		private EntityContainerMapping LoadMappingItems(XmlReader innerReader)
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

		// Token: 0x060024A8 RID: 9384 RVA: 0x000A8E6C File Offset: 0x000A706C
		private EntityContainerMapping LoadMappingItems(XPathNavigator nav)
		{
			if (!this.MoveToRootElement(nav) || nav.NodeType != XPathNodeType.Element)
			{
				MappingItemLoader.AddToSchemaErrors(Strings.Mapping_Invalid_CSRootElementMissing("urn:schemas-microsoft-com:windows:storage:mapping:CS", "http://schemas.microsoft.com/ado/2008/09/mapping/cs", "http://schemas.microsoft.com/ado/2009/11/mapping/cs"), MappingErrorCode.RootMappingElementMissing, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
				return null;
			}
			EntityContainerMapping result = this.LoadMappingChildNodes(nav.Clone());
			if (this.m_parsingErrors.Count != 0 && !MetadataHelper.CheckIfAllErrorsAreWarnings(this.m_parsingErrors))
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000A8EE8 File Offset: 0x000A70E8
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

		// Token: 0x060024AA RID: 9386 RVA: 0x000A8F54 File Offset: 0x000A7154
		private EntityContainerMapping LoadMappingChildNodes(XPathNavigator nav)
		{
			bool flag;
			if (nav.MoveToChild("Alias", this.m_currentNamespaceUri))
			{
				do
				{
					this.m_alias.Add(MappingItemLoader.GetAttributeValue(nav.Clone(), "Key"), MappingItemLoader.GetAttributeValue(nav.Clone(), "Value"));
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

		// Token: 0x060024AB RID: 9387 RVA: 0x000A8FD8 File Offset: 0x000A71D8
		private EntityContainerMapping LoadEntityContainerMapping(XPathNavigator nav)
		{
			IXmlLineInfo xmlLineInfo = (IXmlLineInfo)nav;
			string attributeValue = MappingItemLoader.GetAttributeValue(nav.Clone(), "CdmEntityContainer");
			string attributeValue2 = MappingItemLoader.GetAttributeValue(nav.Clone(), "StorageEntityContainer");
			bool boolAttributeValue = MappingItemLoader.GetBoolAttributeValue(nav.Clone(), "GenerateUpdateViews", true);
			EntityContainerMapping entityContainerMapping;
			System.Data.Entity.Core.Metadata.Edm.EntityContainer storageEntityContainer;
			if (this.m_storageMappingItemCollection.TryGetItem<EntityContainerMapping>(attributeValue, out entityContainerMapping))
			{
				System.Data.Entity.Core.Metadata.Edm.EntityContainer edmEntityContainer = entityContainerMapping.EdmEntityContainer;
				storageEntityContainer = entityContainerMapping.StorageEntityContainer;
				if (attributeValue2 != storageEntityContainer.Name)
				{
					MappingItemLoader.AddToSchemaErrors(Strings.StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping(attributeValue2, storageEntityContainer.Name, edmEntityContainer.Name), MappingErrorCode.StorageEntityContainerNameMismatchWhileSpecifyingPartialMapping, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					return null;
				}
			}
			else
			{
				if (this.m_storageMappingItemCollection.ContainsStorageEntityContainer(attributeValue2))
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_AlreadyMapped_StorageEntityContainer), attributeValue2, MappingErrorCode.AlreadyMappedStorageEntityContainer, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					return null;
				}
				System.Data.Entity.Core.Metadata.Edm.EntityContainer edmEntityContainer;
				this.EdmItemCollection.TryGetEntityContainer(attributeValue, out edmEntityContainer);
				if (edmEntityContainer == null)
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_EntityContainer), attributeValue, MappingErrorCode.InvalidEntityContainer, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
				}
				this.StoreItemCollection.TryGetEntityContainer(attributeValue2, out storageEntityContainer);
				if (storageEntityContainer == null)
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_StorageEntityContainer), attributeValue2, MappingErrorCode.InvalidEntityContainer, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
				}
				if (edmEntityContainer == null || storageEntityContainer == null)
				{
					return null;
				}
				entityContainerMapping = new EntityContainerMapping(edmEntityContainer, storageEntityContainer, this.m_storageMappingItemCollection, boolAttributeValue, boolAttributeValue);
				entityContainerMapping.StartLineNumber = xmlLineInfo.LineNumber;
				entityContainerMapping.StartLinePosition = xmlLineInfo.LinePosition;
			}
			this.LoadEntityContainerMappingChildNodes(nav.Clone(), entityContainerMapping, storageEntityContainer);
			return entityContainerMapping;
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000A9170 File Offset: 0x000A7370
		private void LoadEntityContainerMappingChildNodes(XPathNavigator nav, EntityContainerMapping entityContainerMapping, System.Data.Entity.Core.Metadata.Edm.EntityContainer storageEntityContainerType)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			bool flag = false;
			if (nav.MoveToChild(XPathNodeType.Element))
			{
				for (;;)
				{
					string localName;
					if ((localName = nav.LocalName) == null)
					{
						goto IL_79;
					}
					if (!(localName == "EntitySetMapping"))
					{
						if (!(localName == "AssociationSetMapping"))
						{
							if (!(localName == "FunctionImportMapping"))
							{
								goto IL_79;
							}
							this.LoadFunctionImportMapping(nav.Clone(), entityContainerMapping);
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
					IL_95:
					if (!nav.MoveToNext(XPathNodeType.Element))
					{
						break;
					}
					continue;
					IL_79:
					MappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_Container_SubElement, MappingErrorCode.SetMappingExpected, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					goto IL_95;
				}
			}
			if (entityContainerMapping.EdmEntityContainer.BaseEntitySets.Count != 0 && !flag)
			{
				MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.ViewGen_Missing_Sets_Mapping), entityContainerMapping.EdmEntityContainer.Name, MappingErrorCode.EmptyContainerMapping, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return;
			}
			this.ValidateFunctionAssociationFunctionMappingUnique(nav.Clone(), entityContainerMapping);
			this.ValidateModificationFunctionMappingConsistentForAssociations(nav.Clone(), entityContainerMapping);
			this.ValidateQueryViewsClosure(nav.Clone(), entityContainerMapping);
			this.ValidateEntitySetFunctionMappingClosure(nav.Clone(), entityContainerMapping);
			entityContainerMapping.SourceLocation = this.m_sourceLocation;
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000A92A4 File Offset: 0x000A74A4
		private void ValidateModificationFunctionMappingConsistentForAssociations(XPathNavigator nav, EntityContainerMapping entityContainerMapping)
		{
			foreach (EntitySetBaseMapping entitySetBaseMapping in entityContainerMapping.EntitySetMaps)
			{
				EntitySetMapping entitySetMapping = (EntitySetMapping)entitySetBaseMapping;
				if (entitySetMapping.ModificationFunctionMappings.Count > 0)
				{
					Set<AssociationSetEnd> expectedEnds = new Set<AssociationSetEnd>(entitySetMapping.ImplicitlyMappedAssociationSetEnds).MakeReadOnly();
					foreach (EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping in entitySetMapping.ModificationFunctionMappings)
					{
						if (entityTypeModificationFunctionMapping.DeleteFunctionMapping != null)
						{
							this.ValidateModificationFunctionMappingConsistentForAssociations(nav, entitySetMapping, entityTypeModificationFunctionMapping, entityTypeModificationFunctionMapping.DeleteFunctionMapping, expectedEnds, "DeleteFunction");
						}
						if (entityTypeModificationFunctionMapping.InsertFunctionMapping != null)
						{
							this.ValidateModificationFunctionMappingConsistentForAssociations(nav, entitySetMapping, entityTypeModificationFunctionMapping, entityTypeModificationFunctionMapping.InsertFunctionMapping, expectedEnds, "InsertFunction");
						}
						if (entityTypeModificationFunctionMapping.UpdateFunctionMapping != null)
						{
							this.ValidateModificationFunctionMappingConsistentForAssociations(nav, entitySetMapping, entityTypeModificationFunctionMapping, entityTypeModificationFunctionMapping.UpdateFunctionMapping, expectedEnds, "UpdateFunction");
						}
					}
				}
			}
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000A93A8 File Offset: 0x000A75A8
		private void ValidateModificationFunctionMappingConsistentForAssociations(XPathNavigator nav, EntitySetMapping entitySetMapping, EntityTypeModificationFunctionMapping entityTypeMapping, ModificationFunctionMapping functionMapping, Set<AssociationSetEnd> expectedEnds, string elementName)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			Set<AssociationSetEnd> set = new Set<AssociationSetEnd>(functionMapping.CollocatedAssociationSetEnds);
			set.MakeReadOnly();
			foreach (AssociationSetEnd associationSetEnd in expectedEnds)
			{
				if (MetadataHelper.IsAssociationValidForEntityType(associationSetEnd, entityTypeMapping.EntityType) && !set.Contains(associationSetEnd))
				{
					MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_ModificationFunction_AssociationSetNotMappedForOperation(entitySetMapping.Set.Name, associationSetEnd.ParentAssociationSet.Name, elementName, entityTypeMapping.EntityType.FullName), MappingErrorCode.InvalidModificationFunctionMappingAssociationSetNotMappedForOperation, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				}
			}
			foreach (AssociationSetEnd associationSetEnd2 in set)
			{
				if (!MetadataHelper.IsAssociationValidForEntityType(associationSetEnd2, entityTypeMapping.EntityType))
				{
					MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_ModificationFunction_AssociationEndMappingInvalidForEntityType(entityTypeMapping.EntityType.FullName, associationSetEnd2.ParentAssociationSet.Name, MetadataHelper.GetEntityTypeForEnd(MetadataHelper.GetOppositeEnd(associationSetEnd2).CorrespondingAssociationEndMember).FullName), MappingErrorCode.InvalidModificationFunctionMappingAssociationEndMappingInvalidForEntityType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				}
			}
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000A94F0 File Offset: 0x000A76F0
		private void ValidateFunctionAssociationFunctionMappingUnique(XPathNavigator nav, EntityContainerMapping entityContainerMapping)
		{
			Dictionary<EntitySetBase, int> dictionary = new Dictionary<EntitySetBase, int>();
			foreach (EntitySetBaseMapping entitySetBaseMapping in entityContainerMapping.EntitySetMaps)
			{
				EntitySetMapping entitySetMapping = (EntitySetMapping)entitySetBaseMapping;
				if (entitySetMapping.ModificationFunctionMappings.Count > 0)
				{
					Set<EntitySetBase> set = new Set<EntitySetBase>();
					foreach (AssociationSetEnd associationSetEnd in entitySetMapping.ImplicitlyMappedAssociationSetEnds)
					{
						set.Add(associationSetEnd.ParentAssociationSet);
					}
					foreach (EntitySetBase key in set)
					{
						MappingItemLoader.IncrementCount<EntitySetBase>(dictionary, key);
					}
				}
			}
			foreach (EntitySetBaseMapping entitySetBaseMapping2 in entityContainerMapping.RelationshipSetMaps)
			{
				AssociationSetMapping associationSetMapping = (AssociationSetMapping)entitySetBaseMapping2;
				if (associationSetMapping.ModificationFunctionMapping != null)
				{
					MappingItemLoader.IncrementCount<EntitySetBase>(dictionary, associationSetMapping.Set);
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
				MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AssociationSetAmbiguous), StringUtil.ToCommaSeparatedString(list), MappingErrorCode.AmbiguousModificationFunctionMappingForAssociationSet, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
			}
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000A96D4 File Offset: 0x000A78D4
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

		// Token: 0x060024B1 RID: 9393 RVA: 0x000A97E0 File Offset: 0x000A79E0
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private void ValidateEntitySetFunctionMappingClosure(XPathNavigator nav, EntityContainerMapping entityContainerMapping)
		{
			KeyToListMap<EntitySet, EntitySetBaseMapping> keyToListMap = new KeyToListMap<EntitySet, EntitySetBaseMapping>(EqualityComparer<EntitySet>.Default);
			foreach (EntitySetBaseMapping entitySetBaseMapping in entityContainerMapping.AllSetMaps)
			{
				foreach (TypeMapping typeMapping in entitySetBaseMapping.TypeMappings)
				{
					foreach (MappingFragment mappingFragment in typeMapping.MappingFragments)
					{
						keyToListMap.Add(mappingFragment.TableSet, entitySetBaseMapping);
					}
				}
			}
			Set<EntitySetBase> implicitMappedAssociationSets = new Set<EntitySetBase>();
			foreach (EntitySetBaseMapping entitySetBaseMapping2 in entityContainerMapping.EntitySetMaps)
			{
				EntitySetMapping entitySetMapping = (EntitySetMapping)entitySetBaseMapping2;
				if (entitySetMapping.ModificationFunctionMappings.Count > 0)
				{
					foreach (AssociationSetEnd associationSetEnd in entitySetMapping.ImplicitlyMappedAssociationSetEnds)
					{
						implicitMappedAssociationSets.Add(associationSetEnd.ParentAssociationSet);
					}
				}
			}
			foreach (EntitySet key in keyToListMap.Keys)
			{
				if (keyToListMap.ListForKey(key).Any((EntitySetBaseMapping s) => s.HasModificationFunctionMapping || implicitMappedAssociationSets.Any((EntitySetBase aset) => aset == s.Set)))
				{
					if (keyToListMap.ListForKey(key).Any((EntitySetBaseMapping s) => !s.HasModificationFunctionMapping && !implicitMappedAssociationSets.Any((EntitySetBase aset) => aset == s.Set)))
					{
						MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_MissingSetClosure), StringUtil.ToCommaSeparatedString(from s in keyToListMap.ListForKey(key)
						where !s.HasModificationFunctionMapping
						select s.Set.Name), MappingErrorCode.MissingSetClosureInModificationFunctionMapping, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
					}
				}
			}
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000A9A74 File Offset: 0x000A7C74
		private static void ValidateClosureAmongSets(EntityContainerMapping entityContainerMapping, Set<EntitySetBase> sets, Set<EntitySetBase> additionalSetsInClosure)
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

		// Token: 0x060024B3 RID: 9395 RVA: 0x000A9C14 File Offset: 0x000A7E14
		private void ValidateQueryViewsClosure(XPathNavigator nav, EntityContainerMapping entityContainerMapping)
		{
			if (!this.m_hasQueryViews)
			{
				return;
			}
			Set<EntitySetBase> set = new Set<EntitySetBase>();
			Set<EntitySetBase> set2 = new Set<EntitySetBase>();
			foreach (EntitySetBaseMapping entitySetBaseMapping in entityContainerMapping.AllSetMaps)
			{
				if (entitySetBaseMapping.QueryView != null)
				{
					set.Add(entitySetBaseMapping.Set);
				}
			}
			set2.AddRange(set);
			MappingItemLoader.ValidateClosureAmongSets(entityContainerMapping, set, set2);
			if (0 < set2.Count)
			{
				MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_Invalid_Query_Views_MissingSetClosure), StringUtil.ToCommaSeparatedString(set2), MappingErrorCode.MissingSetClosureInQueryViews, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
			}
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000A9CCC File Offset: 0x000A7ECC
		private void LoadEntitySetMapping(XPathNavigator nav, EntityContainerMapping entityContainerMapping, System.Data.Entity.Core.Metadata.Edm.EntityContainer storageEntityContainerType)
		{
			string aliasResolvedAttributeValue = this.GetAliasResolvedAttributeValue(nav.Clone(), "Name");
			string attributeValue = MappingItemLoader.GetAttributeValue(nav.Clone(), "TypeName");
			string aliasResolvedAttributeValue2 = this.GetAliasResolvedAttributeValue(nav.Clone(), "StoreEntitySet");
			bool boolAttributeValue = MappingItemLoader.GetBoolAttributeValue(nav.Clone(), "MakeColumnsDistinct", false);
			EntitySetMapping entitySetMapping = (EntitySetMapping)entityContainerMapping.GetEntitySetMapping(aliasResolvedAttributeValue);
			IXmlLineInfo xmlLineInfo = (IXmlLineInfo)nav;
			EntitySet entitySet;
			if (entitySetMapping == null)
			{
				if (!entityContainerMapping.EdmEntityContainer.TryGetEntitySetByName(aliasResolvedAttributeValue, false, out entitySet))
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Entity_Set), aliasResolvedAttributeValue, MappingErrorCode.InvalidEntitySet, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					return;
				}
				entitySetMapping = new EntitySetMapping(entitySet, entityContainerMapping);
			}
			else
			{
				entitySet = (EntitySet)entitySetMapping.Set;
			}
			entitySetMapping.StartLineNumber = xmlLineInfo.LineNumber;
			entitySetMapping.StartLinePosition = xmlLineInfo.LinePosition;
			entityContainerMapping.AddSetMapping(entitySetMapping);
			if (string.IsNullOrEmpty(attributeValue))
			{
				if (nav.MoveToChild(XPathNodeType.Element))
				{
					for (;;)
					{
						string localName;
						if ((localName = nav.LocalName) == null)
						{
							goto IL_185;
						}
						if (!(localName == "EntityTypeMapping"))
						{
							if (!(localName == "QueryView"))
							{
								goto IL_185;
							}
							if (!string.IsNullOrEmpty(aliasResolvedAttributeValue2))
							{
								break;
							}
							if (!this.LoadQueryView(nav.Clone(), entitySetMapping))
							{
								return;
							}
						}
						else
						{
							aliasResolvedAttributeValue2 = this.GetAliasResolvedAttributeValue(nav.Clone(), "StoreEntitySet");
							this.LoadEntityTypeMapping(nav.Clone(), entitySetMapping, aliasResolvedAttributeValue2, storageEntityContainerType, false, entityContainerMapping.GenerateUpdateViews);
						}
						IL_1A2:
						if (!nav.MoveToNext(XPathNodeType.Element))
						{
							goto Block_9;
						}
						continue;
						IL_185:
						MappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_TypeMapping_QueryView, MappingErrorCode.InvalidContent, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
						goto IL_1A2;
					}
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_TableName_QueryView), aliasResolvedAttributeValue, MappingErrorCode.TableNameAttributeWithQueryView, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					return;
					Block_9:;
				}
			}
			else
			{
				this.LoadEntityTypeMapping(nav.Clone(), entitySetMapping, aliasResolvedAttributeValue2, storageEntityContainerType, boolAttributeValue, entityContainerMapping.GenerateUpdateViews);
			}
			this.ValidateAllEntityTypesHaveFunctionMapping(nav.Clone(), entitySetMapping);
			if (entitySetMapping.HasNoContent)
			{
				MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Emtpty_SetMap), entitySet.Name, MappingErrorCode.EmptySetMapping, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
			}
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000A9EE4 File Offset: 0x000A80E4
		private void ValidateAllEntityTypesHaveFunctionMapping(XPathNavigator nav, EntitySetMapping setMapping)
		{
			Set<EdmType> set = new Set<EdmType>();
			foreach (EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping in setMapping.ModificationFunctionMappings)
			{
				set.Add(entityTypeModificationFunctionMapping.EntityType);
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
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_MissingEntityType), StringUtil.ToCommaSeparatedString(set2), MappingErrorCode.MissingModificationFunctionMappingForEntityType, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
				}
			}
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000AA00C File Offset: 0x000A820C
		private bool TryParseEntityTypeAttribute(XPathNavigator nav, EntityType rootEntityType, Func<EntityType, string> typeNotAssignableMessage, out Set<EntityType> isOfTypeEntityTypes, out Set<EntityType> entityTypes)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			string attributeValue = MappingItemLoader.GetAttributeValue(nav.Clone(), "TypeName");
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
						MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_InvalidContent_IsTypeOfNotTerminated, MappingErrorCode.InvalidEntityType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
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
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Entity_Type), text2, MappingErrorCode.InvalidEntityType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					return false;
				}
				if (!Helper.IsAssignableFrom(rootEntityType, entityType))
				{
					MappingItemLoader.AddToSchemaErrorWithMessage(typeNotAssignableMessage(entityType), MappingErrorCode.InvalidEntityType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					return false;
				}
				if (entityType.Abstract)
				{
					if (!flag)
					{
						MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_AbstractEntity_Type), entityType.FullName, MappingErrorCode.MappingOfAbstractType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
						return false;
					}
					IEnumerable<EdmType> typeAndSubtypesOf = MetadataHelper.GetTypeAndSubtypesOf(entityType, this.EdmItemCollection, false);
					if (!typeAndSubtypesOf.GetEnumerator().MoveNext())
					{
						MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_AbstractEntity_IsOfType), entityType.FullName, MappingErrorCode.MappingOfAbstractType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
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

		// Token: 0x060024B7 RID: 9399 RVA: 0x000AA298 File Offset: 0x000A8498
		private void LoadEntityTypeMapping(XPathNavigator nav, EntitySetMapping entitySetMapping, string tableName, System.Data.Entity.Core.Metadata.Edm.EntityContainer storageEntityContainerType, bool distinctFlagAboveType, bool generateUpdateViews)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			EntityTypeMapping entityTypeMapping = new EntityTypeMapping(entitySetMapping);
			EntityType rootEntityType = (EntityType)entitySetMapping.Set.ElementType;
			Set<EntityType> set;
			Set<EntityType> set2;
			if (!this.TryParseEntityTypeAttribute(nav.Clone(), rootEntityType, (EntityType e) => Strings.Mapping_InvalidContent_Entity_Type_For_Entity_Set(e.FullName, rootEntityType.FullName, entitySetMapping.Set.Name), out set, out set2))
			{
				return;
			}
			foreach (EntityType type in set2)
			{
				entityTypeMapping.AddType(type);
			}
			foreach (EntityType type2 in set)
			{
				entityTypeMapping.AddIsOfType(type2);
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
						this.LoadEntityTypeModificationFunctionMapping(nav.Clone(), entitySetMapping, entityTypeMapping);
					}
					else if (nav.LocalName != "MappingFragment")
					{
						MappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_Table_Expected, MappingErrorCode.TableMappingFragmentExpected, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					}
					else
					{
						bool boolAttributeValue = MappingItemLoader.GetBoolAttributeValue(nav.Clone(), "MakeColumnsDistinct", false);
						if (generateUpdateViews && boolAttributeValue)
						{
							MappingItemLoader.AddToSchemaErrors(Strings.Mapping_DistinctFlagInReadWriteContainer, MappingErrorCode.DistinctFragmentInReadWriteContainer, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
						}
						tableName = this.GetAliasResolvedAttributeValue(nav.Clone(), "StoreEntitySet");
						MappingFragment mappingFragment = this.LoadMappingFragment(nav.Clone(), entityTypeMapping, tableName, storageEntityContainerType, boolAttributeValue);
						if (mappingFragment != null)
						{
							entityTypeMapping.AddFragment(mappingFragment);
						}
					}
				}
				while (nav.MoveToNext(XPathNodeType.Element));
			}
			else
			{
				if (nav.LocalName == "ModificationFunctionMapping")
				{
					MappingItemLoader.AddToSchemaErrors(Strings.Mapping_ModificationFunction_In_Table_Context, MappingErrorCode.InvalidTableNameAttributeWithModificationFunctionMapping, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				}
				if (generateUpdateViews && distinctFlagAboveType)
				{
					MappingItemLoader.AddToSchemaErrors(Strings.Mapping_DistinctFlagInReadWriteContainer, MappingErrorCode.DistinctFragmentInReadWriteContainer, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				}
				MappingFragment mappingFragment2 = this.LoadMappingFragment(nav.Clone(), entityTypeMapping, tableName, storageEntityContainerType, distinctFlagAboveType);
				if (mappingFragment2 != null)
				{
					entityTypeMapping.AddFragment(mappingFragment2);
				}
			}
			entitySetMapping.AddTypeMapping(entityTypeMapping);
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000AA504 File Offset: 0x000A8704
		private void LoadEntityTypeModificationFunctionMapping(XPathNavigator nav, EntitySetMapping entitySetMapping, EntityTypeMapping entityTypeMapping)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			if (entityTypeMapping.IsOfTypes.Count != 0 || entityTypeMapping.Types.Count != 1)
			{
				MappingItemLoader.AddToSchemaErrors(Strings.Mapping_ModificationFunction_Multiple_Types, MappingErrorCode.InvalidModificationFunctionMappingForMultipleTypes, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return;
			}
			EntityType entityType = (EntityType)entityTypeMapping.Types[0];
			if (entityType.Abstract)
			{
				MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_AbstractEntity_FunctionMapping), entityType.FullName, MappingErrorCode.MappingOfAbstractType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return;
			}
			foreach (EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping in entitySetMapping.ModificationFunctionMappings)
			{
				if (entityTypeModificationFunctionMapping.EntityType.Equals(entityType))
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_RedundantEntityTypeMapping), entityType.Name, MappingErrorCode.RedundantEntityTypeMappingInModificationFunctionMapping, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					return;
				}
			}
			MappingItemLoader.ModificationFunctionMappingLoader modificationFunctionMappingLoader = new MappingItemLoader.ModificationFunctionMappingLoader(this, entitySetMapping.Set);
			ModificationFunctionMapping modificationFunctionMapping = null;
			ModificationFunctionMapping modificationFunctionMapping2 = null;
			ModificationFunctionMapping modificationFunctionMapping3 = null;
			if (nav.MoveToChild(XPathNodeType.Element))
			{
				do
				{
					string localName;
					if ((localName = nav.LocalName) != null)
					{
						if (!(localName == "DeleteFunction"))
						{
							if (!(localName == "InsertFunction"))
							{
								if (localName == "UpdateFunction")
								{
									modificationFunctionMapping3 = modificationFunctionMappingLoader.LoadEntityTypeModificationFunctionMapping(nav.Clone(), entitySetMapping.Set, true, true, entityType);
								}
							}
							else
							{
								modificationFunctionMapping2 = modificationFunctionMappingLoader.LoadEntityTypeModificationFunctionMapping(nav.Clone(), entitySetMapping.Set, true, false, entityType);
							}
						}
						else
						{
							modificationFunctionMapping = modificationFunctionMappingLoader.LoadEntityTypeModificationFunctionMapping(nav.Clone(), entitySetMapping.Set, false, true, entityType);
						}
					}
				}
				while (nav.MoveToNext(XPathNodeType.Element));
			}
			IEnumerable<ModificationFunctionParameterBinding> enumerable = new List<ModificationFunctionParameterBinding>();
			if (modificationFunctionMapping != null)
			{
				enumerable = Helper.Concat<ModificationFunctionParameterBinding>(new IEnumerable<ModificationFunctionParameterBinding>[]
				{
					enumerable,
					modificationFunctionMapping.ParameterBindings
				});
			}
			if (modificationFunctionMapping2 != null)
			{
				enumerable = Helper.Concat<ModificationFunctionParameterBinding>(new IEnumerable<ModificationFunctionParameterBinding>[]
				{
					enumerable,
					modificationFunctionMapping2.ParameterBindings
				});
			}
			if (modificationFunctionMapping3 != null)
			{
				enumerable = Helper.Concat<ModificationFunctionParameterBinding>(new IEnumerable<ModificationFunctionParameterBinding>[]
				{
					enumerable,
					modificationFunctionMapping3.ParameterBindings
				});
			}
			Dictionary<AssociationSet, AssociationEndMember> dictionary = new Dictionary<AssociationSet, AssociationEndMember>();
			foreach (ModificationFunctionParameterBinding modificationFunctionParameterBinding in enumerable)
			{
				if (modificationFunctionParameterBinding.MemberPath.AssociationSetEnd != null)
				{
					AssociationSet parentAssociationSet = modificationFunctionParameterBinding.MemberPath.AssociationSetEnd.ParentAssociationSet;
					AssociationEndMember correspondingAssociationEndMember = modificationFunctionParameterBinding.MemberPath.AssociationSetEnd.CorrespondingAssociationEndMember;
					AssociationEndMember associationEndMember;
					if (dictionary.TryGetValue(parentAssociationSet, out associationEndMember) && associationEndMember != correspondingAssociationEndMember)
					{
						MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_ModificationFunction_MultipleEndsOfAssociationMapped(correspondingAssociationEndMember.Name, associationEndMember.Name, parentAssociationSet.Name), MappingErrorCode.InvalidModificationFunctionMappingMultipleEndsOfAssociationMapped, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
						return;
					}
					dictionary[parentAssociationSet] = correspondingAssociationEndMember;
				}
			}
			EntityTypeModificationFunctionMapping modificationFunctionMapping4 = new EntityTypeModificationFunctionMapping(entityType, modificationFunctionMapping, modificationFunctionMapping2, modificationFunctionMapping3);
			entitySetMapping.AddModificationFunctionMapping(modificationFunctionMapping4);
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000AA85C File Offset: 0x000A8A5C
		private bool LoadQueryView(XPathNavigator nav, EntitySetBaseMapping setMapping)
		{
			string value = nav.Value;
			string text = MappingItemLoader.GetAttributeValue(nav.Clone(), "TypeName");
			if (text != null)
			{
				text = text.Trim();
			}
			IXmlLineInfo xmlLineInfo = nav as IXmlLineInfo;
			if (setMapping.QueryView == null)
			{
				if (text != null)
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo((object val) => Strings.Mapping_TypeName_For_First_QueryView, setMapping.Set.Name, MappingErrorCode.TypeNameForFirstQueryView, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					return false;
				}
				if (string.IsNullOrEmpty(value))
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_Empty_QueryView), setMapping.Set.Name, MappingErrorCode.EmptyQueryView, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
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
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_QueryView_TypeName_Not_Defined), setMapping.Set.Name, MappingErrorCode.NoTypeNameForTypeSpecificQueryView, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
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
						MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_QueryViewMultipleTypeInTypeName), setMapping.Set.ToString(), MappingErrorCode.TypeNameContainsMultipleTypesForQueryView, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
						return false;
					}
					entityType = set2.First<EntityType>();
					flag = false;
				}
				if (flag && setMapping.Set.ElementType.EdmEquals(entityType))
				{
					MappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_QueryView_For_Base_Type), entityType.ToString(), setMapping.Set.ToString(), MappingErrorCode.IsTypeOfQueryViewForBaseType, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					return false;
				}
				if (string.IsNullOrEmpty(value))
				{
					if (flag)
					{
						MappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_Empty_QueryView_OfType), entityType.Name, setMapping.Set.Name, MappingErrorCode.EmptyQueryView, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
						return false;
					}
					MappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_Empty_QueryView_OfTypeOnly), setMapping.Set.Name, entityType.Name, MappingErrorCode.EmptyQueryView, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
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
							item = new EdmSchemaError(Strings.Mapping_QueryView_Duplicate_OfType(setMapping.Set, entityType), 2082, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
						}
						else
						{
							item = new EdmSchemaError(Strings.Mapping_QueryView_Duplicate_OfTypeOnly(setMapping.Set, entityType), 2082, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
						}
						this.m_parsingErrors.Add(item);
						return false;
					}
					setMapping.AddTypeSpecificQueryView(key, value);
					return true;
				}
			}
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000AAC00 File Offset: 0x000A8E00
		private void LoadAssociationSetMapping(XPathNavigator nav, EntityContainerMapping entityContainerMapping, System.Data.Entity.Core.Metadata.Edm.EntityContainer storageEntityContainerType)
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
				MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Association_Set), aliasResolvedAttributeValue, MappingErrorCode.InvalidAssociationSet, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
				return;
			}
			if (associationSet.ElementType.IsForeignKey)
			{
				System.Data.Entity.Core.Metadata.Edm.ReferentialConstraint referentialConstraint = associationSet.ElementType.ReferentialConstraints.Single<System.Data.Entity.Core.Metadata.Edm.ReferentialConstraint>();
				IEnumerable<EdmMember> dependentKeys = MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)referentialConstraint.ToRole).KeyMembers;
				if (associationSet.ElementType.ReferentialConstraints.Single<System.Data.Entity.Core.Metadata.Edm.ReferentialConstraint>().ToProperties.All((EdmProperty p) => dependentKeys.Contains(p)))
				{
					EdmSchemaError edmSchemaError = MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_ForeignKey_Association_Set_PKtoPK), aliasResolvedAttributeValue, MappingErrorCode.InvalidAssociationSet, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					edmSchemaError.Severity = EdmSchemaErrorSeverity.Warning;
					return;
				}
				MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_ForeignKey_Association_Set), aliasResolvedAttributeValue, MappingErrorCode.InvalidAssociationSet, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
				return;
			}
			else
			{
				if (entityContainerMapping.ContainsAssociationSetMapping(associationSet))
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_Duplicate_CdmAssociationSet_StorageMap), aliasResolvedAttributeValue, MappingErrorCode.DuplicateSetMapping, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					return;
				}
				AssociationSetMapping associationSetMapping = new AssociationSetMapping(associationSet, entityContainerMapping);
				associationSetMapping.StartLineNumber = xmlLineInfo.LineNumber;
				associationSetMapping.StartLinePosition = xmlLineInfo.LinePosition;
				if (!nav.MoveToChild(XPathNodeType.Element))
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Emtpty_SetMap), associationSet.Name, MappingErrorCode.EmptySetMapping, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					return;
				}
				entityContainerMapping.AddSetMapping(associationSetMapping);
				if (nav.LocalName == "QueryView")
				{
					if (!string.IsNullOrEmpty(aliasResolvedAttributeValue3))
					{
						MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_TableName_QueryView), aliasResolvedAttributeValue, MappingErrorCode.TableNameAttributeWithQueryView, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
						return;
					}
					if (!this.LoadQueryView(nav.Clone(), associationSetMapping))
					{
						return;
					}
					if (!nav.MoveToNext(XPathNodeType.Element))
					{
						return;
					}
				}
				if (!(nav.LocalName == "EndProperty") && !(nav.LocalName == "ModificationFunctionMapping"))
				{
					if (nav.LocalName == "Condition")
					{
						MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_AssociationSet_Condition), aliasResolvedAttributeValue, MappingErrorCode.InvalidContent, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					}
					return;
				}
				if (string.IsNullOrEmpty(aliasResolvedAttributeValue2))
				{
					MappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_Association_Type_Empty, MappingErrorCode.InvalidAssociationType, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					return;
				}
				this.LoadAssociationTypeMapping(nav.Clone(), associationSetMapping, aliasResolvedAttributeValue2, aliasResolvedAttributeValue3, storageEntityContainerType);
				return;
			}
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000AAED4 File Offset: 0x000A90D4
		private void LoadFunctionImportMapping(XPathNavigator nav, EntityContainerMapping entityContainerMapping)
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
				MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_TargetFunctionMustBeNonComposable(edmFunction2.FullName, edmFunction.FullName), MappingErrorCode.MappingFunctionImportTargetFunctionMustBeNonComposable, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
				return;
			}
			if (edmFunction2.IsComposableAttribute && !edmFunction.IsComposableAttribute)
			{
				MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_TargetFunctionMustBeComposable(edmFunction2.FullName, edmFunction.FullName), MappingErrorCode.MappingFunctionImportTargetFunctionMustBeComposable, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
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
						List<FunctionImportStructuralTypeMapping> functionImportMappingResultMapping = this.GetFunctionImportMappingResultMapping(nav.Clone(), xmlLineInfo, edmFunction2, num);
						list.Add(functionImportMappingResultMapping);
					}
					num++;
				}
				while (nav.MoveToNext(XPathNodeType.Element));
			}
			if (list.Count > 0 && list.Count != edmFunction2.ReturnParameters.Count)
			{
				MappingItemLoader.AddToSchemaErrors(Strings.Mapping_FunctionImport_ResultMappingCountDoesNotMatchResultCount(edmFunction2.Identity), MappingErrorCode.FunctionResultMappingCountMismatch, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
				return;
			}
			if (!edmFunction2.IsComposableAttribute)
			{
				FunctionImportMappingNonComposable functionImportMappingNonComposable = new FunctionImportMappingNonComposable(edmFunction2, edmFunction, list, this.EdmItemCollection);
				foreach (FunctionImportStructuralTypeMappingKB functionImportStructuralTypeMappingKB in functionImportMappingNonComposable.InternalResultMappings)
				{
					functionImportStructuralTypeMappingKB.ValidateTypeConditions(false, this.m_parsingErrors, this.m_sourceLocation);
				}
				for (int i = 0; i < functionImportMappingNonComposable.InternalResultMappings.Count; i++)
				{
					EntityType entityType;
					if (MetadataHelper.TryGetFunctionImportReturnType<EntityType>(edmFunction2, i, out entityType) && entityType.Abstract && functionImportMappingNonComposable.GetResultMapping(i).NormalizedEntityTypeMappings.Count == 0)
					{
						MappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_FunctionImport_ImplicitMappingForAbstractReturnType), entityType.FullName, edmFunction2.Identity, MappingErrorCode.MappingOfAbstractType, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
					}
				}
				entityContainerMapping.AddFunctionImportMapping(functionImportMappingNonComposable);
				return;
			}
			EdmFunction edmFunction3 = this.StoreItemCollection.ConvertToCTypeFunction(edmFunction);
			RowType tvfReturnType = TypeHelpers.GetTvfReturnType(edmFunction3);
			RowType tvfReturnType2 = TypeHelpers.GetTvfReturnType(edmFunction);
			if (tvfReturnType == null)
			{
				MappingItemLoader.AddToSchemaErrors(Strings.Mapping_FunctionImport_ResultMapping_InvalidSType(edmFunction2.Identity), MappingErrorCode.MappingFunctionImportTVFExpected, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
				return;
			}
			List<FunctionImportStructuralTypeMapping> typeMappings = (list.Count > 0) ? list[0] : new List<FunctionImportStructuralTypeMapping>();
			FunctionImportMappingComposable functionImportMapping = null;
			EdmType edmType;
			if (MetadataHelper.TryGetFunctionImportReturnType<EdmType>(edmFunction2, 0, out edmType))
			{
				FunctionImportMappingComposableHelper functionImportMappingComposableHelper = new FunctionImportMappingComposableHelper(entityContainerMapping, this.m_sourceLocation, this.m_parsingErrors);
				if (Helper.IsStructuralType(edmType))
				{
					if (!functionImportMappingComposableHelper.TryCreateFunctionImportMappingComposableWithStructuralResult(edmFunction2, edmFunction3, typeMappings, tvfReturnType, tvfReturnType2, xmlLineInfo, out functionImportMapping))
					{
						return;
					}
				}
				else if (!functionImportMappingComposableHelper.TryCreateFunctionImportMappingComposableWithScalarResult(edmFunction2, edmFunction3, edmFunction, edmType, tvfReturnType, xmlLineInfo, out functionImportMapping))
				{
					return;
				}
			}
			entityContainerMapping.AddFunctionImportMapping(functionImportMapping);
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000AB1B8 File Offset: 0x000A93B8
		private bool TryGetFunctionImportStoreFunction(XPathNavigator nav, out EdmFunction targetFunction)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			targetFunction = null;
			string aliasResolvedAttributeValue = this.GetAliasResolvedAttributeValue(nav.Clone(), "FunctionName");
			ReadOnlyCollection<EdmFunction> functions = this.StoreItemCollection.GetFunctions(aliasResolvedAttributeValue);
			if (functions.Count == 0)
			{
				MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_StoreFunctionDoesNotExist(aliasResolvedAttributeValue), MappingErrorCode.MappingFunctionImportStoreFunctionDoesNotExist, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return false;
			}
			if (functions.Count > 1)
			{
				MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_FunctionAmbiguous(aliasResolvedAttributeValue), MappingErrorCode.MappingFunctionImportStoreFunctionAmbiguous, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return false;
			}
			targetFunction = functions.Single<EdmFunction>();
			return true;
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000AB248 File Offset: 0x000A9448
		private bool TryGetFunctionImportModelFunction(XPathNavigator nav, EntityContainerMapping entityContainerMapping, out EdmFunction functionImport)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			string aliasResolvedAttributeValue = this.GetAliasResolvedAttributeValue(nav.Clone(), "FunctionImportName");
			System.Data.Entity.Core.Metadata.Edm.EntityContainer edmEntityContainer = entityContainerMapping.EdmEntityContainer;
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
				MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_FunctionImportDoesNotExist(aliasResolvedAttributeValue, entityContainerMapping.EdmEntityContainer.Name), MappingErrorCode.MappingFunctionImportFunctionImportDoesNotExist, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return false;
			}
			FunctionImportMapping functionImportMapping;
			if (entityContainerMapping.TryGetFunctionImportMapping(functionImport, out functionImportMapping))
			{
				MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_FunctionImportMappedMultipleTimes(aliasResolvedAttributeValue), MappingErrorCode.MappingFunctionImportFunctionImportMappedMultipleTimes, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return false;
			}
			return true;
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000AB328 File Offset: 0x000A9528
		private void ValidateFunctionImportMappingParameters(XPathNavigator nav, EdmFunction targetFunction, EdmFunction functionImport)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			foreach (FunctionParameter functionParameter in targetFunction.Parameters)
			{
				FunctionParameter functionParameter2;
				if (!functionImport.Parameters.TryGetValue(functionParameter.Name, false, out functionParameter2))
				{
					MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_TargetParameterHasNoCorrespondingImportParameter(functionParameter.Name), MappingErrorCode.MappingFunctionImportTargetParameterHasNoCorrespondingImportParameter, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				}
				else
				{
					if (functionParameter.Mode != functionParameter2.Mode)
					{
						MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_IncompatibleParameterMode(functionParameter.Name, functionParameter.Mode, functionParameter2.Mode), MappingErrorCode.MappingFunctionImportIncompatibleParameterMode, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					}
					PrimitiveType primitiveType = Helper.AsPrimitive(functionParameter2.TypeUsage.EdmType);
					if (Helper.IsSpatialType(primitiveType))
					{
						primitiveType = Helper.GetSpatialNormalizedPrimitiveType(primitiveType);
					}
					PrimitiveType primitiveType2 = (PrimitiveType)this.StoreItemCollection.ProviderManifest.GetEdmType(functionParameter.TypeUsage).EdmType;
					if (primitiveType2 == null)
					{
						MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_ProviderReturnsNullType(functionParameter.Name), MappingErrorCode.MappingStoreProviderReturnsNullEdmType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
						return;
					}
					if (primitiveType2.PrimitiveTypeKind != primitiveType.PrimitiveTypeKind)
					{
						string errorMessage = Helper.IsEnumType(functionParameter2.TypeUsage.EdmType) ? Strings.Mapping_FunctionImport_IncompatibleEnumParameterType(functionParameter.Name, primitiveType2.Name, functionParameter2.TypeUsage.EdmType.FullName, Helper.GetUnderlyingEdmTypeForEnumType(functionParameter2.TypeUsage.EdmType).Name) : Strings.Mapping_FunctionImport_IncompatibleParameterType(functionParameter.Name, primitiveType2.Name, primitiveType.Name);
						MappingItemLoader.AddToSchemaErrorWithMessage(errorMessage, MappingErrorCode.MappingFunctionImportIncompatibleParameterType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					}
				}
			}
			foreach (FunctionParameter functionParameter3 in functionImport.Parameters)
			{
				FunctionParameter functionParameter4;
				if (!targetFunction.Parameters.TryGetValue(functionParameter3.Name, false, out functionParameter4))
				{
					MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_ImportParameterHasNoCorrespondingTargetParameter(functionParameter3.Name), MappingErrorCode.MappingFunctionImportImportParameterHasNoCorrespondingTargetParameter, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				}
			}
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000AB5E0 File Offset: 0x000A97E0
		private List<FunctionImportStructuralTypeMapping> GetFunctionImportMappingResultMapping(XPathNavigator nav, IXmlLineInfo functionImportMappingLineInfo, EdmFunction functionImport, int resultSetIndex)
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
								MappingItemLoader.AddToSchemaErrors(Strings.Mapping_FunctionImport_EntityTypeMappingForFunctionNotReturningEntitySet("EntityTypeMapping", functionImport.Identity), MappingErrorCode.MappingFunctionImportEntityTypeMappingForFunctionNotReturningEntitySet, this.m_sourceLocation, functionImportMappingLineInfo, this.m_parsingErrors);
							}
							FunctionImportEntityTypeMapping item;
							if (this.TryLoadFunctionImportEntityTypeMapping(nav.Clone(), resultEntityType, (EntityType e) => Strings.Mapping_FunctionImport_InvalidContentEntityTypeForEntitySet(e.FullName, resultEntityType.FullName, entitySet.Name, functionImport.Identity), out item))
							{
								list.Add(item);
							}
						}
						else
						{
							MappingItemLoader.AddToSchemaErrors(Strings.Mapping_FunctionImport_ResultMapping_InvalidCTypeETExpected(functionImport.Identity), MappingErrorCode.MappingFunctionImportUnexpectedEntityTypeMapping, this.m_sourceLocation, functionImportMappingLineInfo, this.m_parsingErrors);
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
							MappingItemLoader.AddToSchemaErrors(Strings.Mapping_FunctionImport_ResultMapping_InvalidCTypeCTExpected(functionImport.Identity), MappingErrorCode.MappingFunctionImportUnexpectedComplexTypeMapping, this.m_sourceLocation, functionImportMappingLineInfo, this.m_parsingErrors);
						}
					}
				}
				while (nav.MoveToNext(XPathNodeType.Element));
			}
			return list;
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000AB79C File Offset: 0x000A999C
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

		// Token: 0x060024C1 RID: 9409 RVA: 0x000AB7F8 File Offset: 0x000A99F8
		private bool TryParseComplexTypeAttribute(XPathNavigator nav, ComplexType resultComplexType, EdmFunction functionImport, out ComplexType complexType)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			string text = MappingItemLoader.GetAttributeValue(nav.Clone(), "TypeName");
			text = this.GetAliasResolvedValue(text);
			if (!this.EdmItemCollection.TryGetItem<ComplexType>(text, out complexType))
			{
				MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Complex_Type), text, MappingErrorCode.InvalidComplexType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return false;
			}
			if (!Helper.IsAssignableFrom(resultComplexType, complexType))
			{
				MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_ResultMapping_MappedTypeDoesNotMatchReturnType(functionImport.Identity, complexType.FullName), MappingErrorCode.InvalidComplexType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return false;
			}
			return true;
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000AB894 File Offset: 0x000A9A94
		private bool TryLoadFunctionImportEntityTypeMapping(XPathNavigator nav, EntityType resultEntityType, Func<EntityType, string> registerEntityTypeMismatchError, out FunctionImportEntityTypeMapping typeMapping)
		{
			typeMapping = null;
			LineInfo lineInfo = new LineInfo(nav);
			MappingItemLoader.GetAttributeValue(nav.Clone(), "TypeName");
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

		// Token: 0x060024C3 RID: 9411 RVA: 0x000AB918 File Offset: 0x000A9B18
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
						MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_InvalidContent_Duplicate_Condition_Member(functionImportEntityTypeMappingCondition.ColumnName), MappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
						flag = true;
					}
				}
			}
			return !flag;
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000ABA28 File Offset: 0x000A9C28
		private void LoadFunctionImportStructuralTypeMappingScalarProperty(XPathNavigator nav, Collection<FunctionImportReturnTypePropertyMapping> columnRenameMappings, IEnumerable<StructuralType> currentTypes)
		{
			LineInfo lineInfo = new LineInfo(nav);
			string memberName = this.GetAliasResolvedAttributeValue(nav.Clone(), "Name");
			string aliasResolvedAttributeValue = this.GetAliasResolvedAttributeValue(nav.Clone(), "ColumnName");
			if (!currentTypes.All((StructuralType t) => t.Members.Contains(memberName)))
			{
				MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_InvalidContent_Cdm_Member(memberName), MappingErrorCode.InvalidEdmMember, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
			}
			if (columnRenameMappings.Any((FunctionImportReturnTypePropertyMapping m) => m.CMember == memberName))
			{
				MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_InvalidContent_Duplicate_Cdm_Member(memberName), MappingErrorCode.DuplicateMemberMapping, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return;
			}
			columnRenameMappings.Add(new FunctionImportReturnTypeScalarPropertyMapping(memberName, aliasResolvedAttributeValue, lineInfo));
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000ABAEC File Offset: 0x000A9CEC
		private void LoadFunctionImportEntityTypeMappingCondition(XPathNavigator nav, List<FunctionImportEntityTypeMappingCondition> conditions)
		{
			LineInfo lineInfo = new LineInfo(nav);
			string aliasResolvedAttributeValue = this.GetAliasResolvedAttributeValue(nav.Clone(), "ColumnName");
			string aliasResolvedAttributeValue2 = this.GetAliasResolvedAttributeValue(nav.Clone(), "Value");
			string aliasResolvedAttributeValue3 = this.GetAliasResolvedAttributeValue(nav.Clone(), "IsNull");
			if (aliasResolvedAttributeValue3 != null && aliasResolvedAttributeValue2 != null)
			{
				MappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_ConditionMapping_Both_Values, MappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return;
			}
			if (aliasResolvedAttributeValue3 == null && aliasResolvedAttributeValue2 == null)
			{
				MappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_ConditionMapping_Either_Values, MappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
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

		// Token: 0x060024C6 RID: 9414 RVA: 0x000ABBC8 File Offset: 0x000A9DC8
		private void LoadAssociationTypeMapping(XPathNavigator nav, AssociationSetMapping associationSetMapping, string associationTypeName, string tableName, System.Data.Entity.Core.Metadata.Edm.EntityContainer storageEntityContainerType)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			AssociationType associationType;
			this.EdmItemCollection.TryGetItem<AssociationType>(associationTypeName, out associationType);
			if (associationType == null)
			{
				MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Association_Type), associationTypeName, MappingErrorCode.InvalidAssociationType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return;
			}
			if (!associationSetMapping.Set.ElementType.Equals(associationType))
			{
				MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_Invalid_Association_Type_For_Association_Set(associationTypeName, associationSetMapping.Set.ElementType.FullName, associationSetMapping.Set.Name), MappingErrorCode.DuplicateTypeMapping, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return;
			}
			AssociationTypeMapping associationTypeMapping = new AssociationTypeMapping(associationType, associationSetMapping);
			associationSetMapping.AssociationTypeMapping = associationTypeMapping;
			if (string.IsNullOrEmpty(tableName) && associationSetMapping.QueryView == null)
			{
				MappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_Table_Expected, MappingErrorCode.InvalidTable, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return;
			}
			MappingFragment mappingFragment = this.LoadAssociationMappingFragment(nav.Clone(), associationSetMapping, associationTypeMapping, tableName, storageEntityContainerType);
			if (mappingFragment != null)
			{
				associationTypeMapping.MappingFragment = mappingFragment;
			}
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000ABCBC File Offset: 0x000A9EBC
		private void LoadAssociationTypeModificationFunctionMapping(XPathNavigator nav, AssociationSetMapping associationSetMapping)
		{
			MappingItemLoader.ModificationFunctionMappingLoader modificationFunctionMappingLoader = new MappingItemLoader.ModificationFunctionMappingLoader(this, associationSetMapping.Set);
			ModificationFunctionMapping deleteFunctionMapping = null;
			ModificationFunctionMapping insertFunctionMapping = null;
			if (nav.MoveToChild(XPathNodeType.Element))
			{
				do
				{
					string localName;
					if ((localName = nav.LocalName) != null)
					{
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
				}
				while (nav.MoveToNext(XPathNodeType.Element));
			}
			associationSetMapping.ModificationFunctionMapping = new AssociationSetModificationFunctionMapping((AssociationSet)associationSetMapping.Set, deleteFunctionMapping, insertFunctionMapping);
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000ABD98 File Offset: 0x000A9F98
		private MappingFragment LoadMappingFragment(XPathNavigator nav, EntityTypeMapping typeMapping, string tableName, System.Data.Entity.Core.Metadata.Edm.EntityContainer storageEntityContainerType, bool distinctFlag)
		{
			IXmlLineInfo navLineInfo = (IXmlLineInfo)nav;
			if (typeMapping.SetMapping.QueryView != null)
			{
				MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_QueryView_PropertyMaps), typeMapping.SetMapping.Set.Name, MappingErrorCode.PropertyMapsWithQueryView, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
				return null;
			}
			EntitySet entitySet;
			storageEntityContainerType.TryGetEntitySetByName(tableName, false, out entitySet);
			if (entitySet == null)
			{
				MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Table), tableName, MappingErrorCode.InvalidTable, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
				return null;
			}
			EntityType elementType = entitySet.ElementType;
			MappingFragment mappingFragment = new MappingFragment(entitySet, typeMapping, distinctFlag);
			mappingFragment.StartLineNumber = navLineInfo.LineNumber;
			mappingFragment.StartLinePosition = navLineInfo.LinePosition;
			if (nav.MoveToChild(XPathNodeType.Element))
			{
				for (;;)
				{
					EdmType containerType = null;
					string attributeValue = MappingItemLoader.GetAttributeValue(nav.Clone(), "Name");
					if (attributeValue != null)
					{
						containerType = typeMapping.GetContainerType(attributeValue);
					}
					string localName;
					if ((localName = nav.LocalName) == null)
					{
						goto IL_1C4;
					}
					if (!(localName == "ScalarProperty"))
					{
						if (!(localName == "ComplexProperty"))
						{
							if (!(localName == "Condition"))
							{
								goto IL_1C4;
							}
							ConditionPropertyMapping conditionPropertyMapping = this.LoadConditionPropertyMapping(nav.Clone(), containerType, elementType.Properties);
							if (conditionPropertyMapping != null)
							{
								mappingFragment.AddConditionProperty(conditionPropertyMapping, delegate(EdmMember member)
								{
									MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Duplicate_Condition_Member), member.Name, MappingErrorCode.ConditionError, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
								});
							}
						}
						else
						{
							ComplexPropertyMapping complexPropertyMapping = this.LoadComplexPropertyMapping(nav.Clone(), containerType, elementType.Properties);
							if (complexPropertyMapping != null)
							{
								mappingFragment.AddPropertyMapping(complexPropertyMapping);
							}
						}
					}
					else
					{
						ScalarPropertyMapping scalarPropertyMapping = this.LoadScalarPropertyMapping(nav.Clone(), containerType, elementType.Properties);
						if (scalarPropertyMapping != null)
						{
							mappingFragment.AddPropertyMapping(scalarPropertyMapping);
						}
					}
					IL_1E6:
					if (!nav.MoveToNext(XPathNodeType.Element))
					{
						break;
					}
					continue;
					IL_1C4:
					MappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_General, MappingErrorCode.InvalidContent, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
					goto IL_1E6;
				}
			}
			nav.MoveToChild(XPathNodeType.Element);
			return mappingFragment;
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000ABFE4 File Offset: 0x000AA1E4
		private MappingFragment LoadAssociationMappingFragment(XPathNavigator nav, AssociationSetMapping setMapping, AssociationTypeMapping typeMapping, string tableName, System.Data.Entity.Core.Metadata.Edm.EntityContainer storageEntityContainerType)
		{
			IXmlLineInfo navLineInfo = (IXmlLineInfo)nav;
			MappingFragment mappingFragment = null;
			EntityType entityType = null;
			if (setMapping.QueryView == null)
			{
				EntitySet entitySet;
				storageEntityContainerType.TryGetEntitySetByName(tableName, false, out entitySet);
				if (entitySet == null)
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Table), tableName, MappingErrorCode.InvalidTable, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
					return null;
				}
				entityType = entitySet.ElementType;
				mappingFragment = new MappingFragment(entitySet, typeMapping, false);
				mappingFragment.StartLineNumber = setMapping.StartLineNumber;
				mappingFragment.StartLinePosition = setMapping.StartLinePosition;
			}
			for (;;)
			{
				string localName;
				if ((localName = nav.LocalName) == null)
				{
					goto IL_221;
				}
				if (!(localName == "EndProperty"))
				{
					if (!(localName == "Condition"))
					{
						if (!(localName == "ModificationFunctionMapping"))
						{
							goto IL_221;
						}
						setMapping.HasModificationFunctionMapping = true;
						this.LoadAssociationTypeModificationFunctionMapping(nav.Clone(), setMapping);
					}
					else
					{
						if (setMapping.QueryView != null)
						{
							goto Block_9;
						}
						ConditionPropertyMapping conditionPropertyMapping = this.LoadConditionPropertyMapping(nav.Clone(), null, entityType.Properties);
						if (conditionPropertyMapping != null)
						{
							mappingFragment.AddConditionProperty(conditionPropertyMapping, delegate(EdmMember member)
							{
								MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Duplicate_Condition_Member), member.Name, MappingErrorCode.ConditionError, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
							});
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
						MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_End), aliasResolvedAttributeValue, MappingErrorCode.InvalidEdmMember, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
					}
					else
					{
						mappingFragment.AddPropertyMapping(this.LoadEndPropertyMapping(nav.Clone(), associationEndMember, entityType));
					}
				}
				IL_243:
				if (!nav.MoveToNext(XPathNodeType.Element))
				{
					return mappingFragment;
				}
				continue;
				IL_221:
				MappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_General, MappingErrorCode.InvalidContent, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
				goto IL_243;
			}
			MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_QueryView_PropertyMaps), setMapping.Set.Name, MappingErrorCode.PropertyMapsWithQueryView, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
			return null;
			Block_9:
			MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_QueryView_PropertyMaps), setMapping.Set.Name, MappingErrorCode.PropertyMapsWithQueryView, this.m_sourceLocation, navLineInfo, this.m_parsingErrors);
			return null;
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000AC244 File Offset: 0x000AA444
		private ScalarPropertyMapping LoadScalarPropertyMapping(XPathNavigator nav, EdmType containerType, ReadOnlyMetadataCollection<EdmProperty> tableProperties)
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
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Cdm_Member), aliasResolvedAttributeValue, MappingErrorCode.InvalidEdmMember, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
				}
			}
			string aliasResolvedAttributeValue2 = this.GetAliasResolvedAttributeValue(nav.Clone(), "ColumnName");
			EdmProperty edmProperty2;
			tableProperties.TryGetValue(aliasResolvedAttributeValue2, false, out edmProperty2);
			if (edmProperty2 == null)
			{
				MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Column), aliasResolvedAttributeValue2, MappingErrorCode.InvalidStorageMember, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
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
			return new ScalarPropertyMapping(edmProperty, edmProperty2);
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000AC3A8 File Offset: 0x000AA5A8
		private ComplexPropertyMapping LoadComplexPropertyMapping(XPathNavigator nav, EdmType containerType, ReadOnlyMetadataCollection<EdmProperty> tableProperties)
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
							MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Cdm_Member), aliasResolvedAttributeValue, MappingErrorCode.InvalidEdmMember, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
						}
						edmType = edmProperty.TypeUsage.EdmType;
					}
					else
					{
						MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Cdm_Member), aliasResolvedAttributeValue, MappingErrorCode.InvalidEdmMember, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
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
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Cdm_Member), aliasResolvedAttributeValue, MappingErrorCode.InvalidEdmMember, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				}
				this.EdmItemCollection.TryGetItem<EdmType>(aliasResolvedAttributeValue2, out edmType);
				edmType = (edmType as ComplexType);
				if (edmType == null)
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Complex_Type), aliasResolvedAttributeValue2, MappingErrorCode.InvalidComplexType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				}
			}
			ComplexPropertyMapping complexPropertyMapping = new ComplexPropertyMapping(edmProperty);
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
					complexPropertyMapping.AddTypeMapping(this.LoadComplexTypeMapping(nav.Clone(), null, tableProperties));
				}
				while (nav.MoveToNext(XPathNodeType.Element));
			}
			else
			{
				complexPropertyMapping.AddTypeMapping(this.LoadComplexTypeMapping(nav.Clone(), edmType, tableProperties));
			}
			return complexPropertyMapping;
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000AC5EC File Offset: 0x000AA7EC
		private ComplexTypeMapping LoadComplexTypeMapping(XPathNavigator nav, EdmType type, ReadOnlyMetadataCollection<EdmProperty> tableType)
		{
			bool isPartial = false;
			string attributeValue = MappingItemLoader.GetAttributeValue(nav.Clone(), "IsPartial");
			if (!string.IsNullOrEmpty(attributeValue))
			{
				isPartial = Convert.ToBoolean(attributeValue, CultureInfo.InvariantCulture);
			}
			ComplexTypeMapping complexTypeMapping = new ComplexTypeMapping(isPartial);
			if (type != null)
			{
				complexTypeMapping.AddType(type as ComplexType);
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
						MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Complex_Type), text2, MappingErrorCode.InvalidComplexType, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
						num = text.IndexOf(';');
					}
					else
					{
						if (num2 == 0)
						{
							complexTypeMapping.AddIsOfType(complexType);
						}
						else
						{
							complexTypeMapping.AddType(complexType);
						}
						num = text.IndexOf(';');
					}
				}
				while (text.Length != 0);
			}
			if (nav.MoveToChild(XPathNodeType.Element))
			{
				for (;;)
				{
					EdmType ownerType = complexTypeMapping.GetOwnerType(MappingItemLoader.GetAttributeValue(nav.Clone(), "Name"));
					string localName;
					if ((localName = nav.LocalName) == null)
					{
						break;
					}
					if (!(localName == "ScalarProperty"))
					{
						if (!(localName == "ComplexProperty"))
						{
							if (!(localName == "Condition"))
							{
								break;
							}
							ConditionPropertyMapping conditionPropertyMapping = this.LoadConditionPropertyMapping(nav.Clone(), ownerType, tableType);
							if (conditionPropertyMapping != null)
							{
								complexTypeMapping.AddConditionProperty(conditionPropertyMapping, delegate(EdmMember member)
								{
									MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Duplicate_Condition_Member), member.Name, MappingErrorCode.ConditionError, this.m_sourceLocation, (IXmlLineInfo)nav, this.m_parsingErrors);
								});
							}
						}
						else
						{
							ComplexPropertyMapping complexPropertyMapping = this.LoadComplexPropertyMapping(nav.Clone(), ownerType, tableType);
							if (complexPropertyMapping != null)
							{
								complexTypeMapping.AddPropertyMapping(complexPropertyMapping);
							}
						}
					}
					else
					{
						ScalarPropertyMapping scalarPropertyMapping = this.LoadScalarPropertyMapping(nav.Clone(), ownerType, tableType);
						if (scalarPropertyMapping != null)
						{
							complexTypeMapping.AddPropertyMapping(scalarPropertyMapping);
						}
					}
					if (!nav.MoveToNext(XPathNodeType.Element))
					{
						return complexTypeMapping;
					}
				}
				throw Error.NotSupported();
			}
			return complexTypeMapping;
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000AC890 File Offset: 0x000AAA90
		private EndPropertyMapping LoadEndPropertyMapping(XPathNavigator nav, AssociationEndMember end, EntityType tableType)
		{
			EndPropertyMapping endPropertyMapping = new EndPropertyMapping
			{
				AssociationEnd = end
			};
			nav.MoveToChild(XPathNodeType.Element);
			ScalarPropertyMapping scalarPropertyMapping;
			for (;;)
			{
				string localName;
				if ((localName = nav.LocalName) != null && localName == "ScalarProperty")
				{
					RefType refType = end.TypeUsage.EdmType as RefType;
					EntityTypeBase elementType = refType.ElementType;
					scalarPropertyMapping = this.LoadScalarPropertyMapping(nav.Clone(), elementType, tableType.Properties);
					if (scalarPropertyMapping != null)
					{
						if (!elementType.KeyMembers.Contains(scalarPropertyMapping.Property))
						{
							break;
						}
						endPropertyMapping.AddPropertyMapping(scalarPropertyMapping);
					}
				}
				if (!nav.MoveToNext(XPathNodeType.Element))
				{
					return endPropertyMapping;
				}
			}
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_EndProperty), scalarPropertyMapping.Property.Name, MappingErrorCode.InvalidEdmMember, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
			return null;
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000AC968 File Offset: 0x000AAB68
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private ConditionPropertyMapping LoadConditionPropertyMapping(XPathNavigator nav, EdmType containerType, ReadOnlyMetadataCollection<EdmProperty> tableProperties)
		{
			string aliasResolvedAttributeValue = this.GetAliasResolvedAttributeValue(nav.Clone(), "Name");
			string aliasResolvedAttributeValue2 = this.GetAliasResolvedAttributeValue(nav.Clone(), "ColumnName");
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			if (aliasResolvedAttributeValue != null && aliasResolvedAttributeValue2 != null)
			{
				MappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_ConditionMapping_Both_Members, MappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return null;
			}
			if (aliasResolvedAttributeValue == null && aliasResolvedAttributeValue2 == null)
			{
				MappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_ConditionMapping_Either_Members, MappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
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
				MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_ConditionMapping_InvalidMember), (aliasResolvedAttributeValue2 != null) ? aliasResolvedAttributeValue2 : aliasResolvedAttributeValue, MappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return null;
			}
			bool? flag = null;
			object obj = null;
			string attributeValue = MappingItemLoader.GetAttributeValue(nav.Clone(), "IsNull");
			EdmType edmType = edmProperty3.TypeUsage.EdmType;
			if (Helper.IsPrimitiveType(edmType))
			{
				TypeUsage typeUsage;
				if (edmProperty3.DeclaringType.DataSpace == DataSpace.SSpace)
				{
					typeUsage = this.StoreItemCollection.ProviderManifest.GetEdmType(edmProperty3.TypeUsage);
					if (typeUsage == null)
					{
						MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_ProviderReturnsNullType(edmProperty3.Name), MappingErrorCode.MappingStoreProviderReturnsNullEdmType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
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
				if (attributeValue == null && !MappingItemLoader.IsTypeSupportedForCondition(primitiveTypeKind))
				{
					MappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_InvalidContent_ConditionMapping_InvalidPrimitiveTypeKind), edmProperty3.Name, edmType.FullName, MappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					return null;
				}
				if (!MappingItemLoader.TryGetTypedAttributeValue(nav.Clone(), "Value", clrEquivalentType, this.m_sourceLocation, this.m_parsingErrors, out obj))
				{
					return null;
				}
			}
			else
			{
				if (!Helper.IsEnumType(edmType))
				{
					MappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_ConditionMapping_NonScalar, MappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					return null;
				}
				obj = MappingItemLoader.GetEnumAttributeValue(nav.Clone(), "Value", (EnumType)edmType, this.m_sourceLocation, this.m_parsingErrors);
			}
			if (attributeValue != null && obj != null)
			{
				MappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_ConditionMapping_Both_Values, MappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return null;
			}
			if (attributeValue == null && obj == null)
			{
				MappingItemLoader.AddToSchemaErrors(Strings.Mapping_InvalidContent_ConditionMapping_Either_Values, MappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return null;
			}
			if (attributeValue != null)
			{
				flag = new bool?(Convert.ToBoolean(attributeValue, CultureInfo.InvariantCulture));
			}
			if (edmProperty2 != null && (edmProperty2.IsStoreGeneratedComputed || edmProperty2.IsStoreGeneratedIdentity))
			{
				MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_ConditionMapping_Computed), edmProperty2.Name, MappingErrorCode.ConditionError, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return null;
			}
			if (obj == null)
			{
				return new IsNullConditionMapping(edmProperty3, flag.Value);
			}
			return new ValueConditionMapping(edmProperty3, obj);
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000ACC80 File Offset: 0x000AAE80
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

		// Token: 0x060024D0 RID: 9424 RVA: 0x000ACCD8 File Offset: 0x000AAED8
		private static XmlSchemaSet GetOrCreateSchemaSet()
		{
			if (MappingItemLoader.s_mappingXmlSchema == null)
			{
				XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
				MappingItemLoader.AddResourceXsdToSchemaSet(xmlSchemaSet, "System.Data.Resources.CSMSL_1.xsd");
				MappingItemLoader.AddResourceXsdToSchemaSet(xmlSchemaSet, "System.Data.Resources.CSMSL_2.xsd");
				MappingItemLoader.AddResourceXsdToSchemaSet(xmlSchemaSet, "System.Data.Resources.CSMSL_3.xsd");
				Interlocked.CompareExchange<XmlSchemaSet>(ref MappingItemLoader.s_mappingXmlSchema, xmlSchemaSet, null);
			}
			return MappingItemLoader.s_mappingXmlSchema;
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000ACD28 File Offset: 0x000AAF28
		private static void AddResourceXsdToSchemaSet(XmlSchemaSet set, string resourceName)
		{
			using (XmlReader xmlResource = DbProviderServices.GetXmlResource(resourceName))
			{
				XmlSchema schema = XmlSchema.Read(xmlResource, null);
				set.Add(schema);
			}
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x000ACD68 File Offset: 0x000AAF68
		internal static void AddToSchemaErrors(string message, MappingErrorCode errorCode, string location, IXmlLineInfo lineInfo, IList<EdmSchemaError> parsingErrors)
		{
			EdmSchemaError item = new EdmSchemaError(message, (int)errorCode, EdmSchemaErrorSeverity.Error, location, lineInfo.LineNumber, lineInfo.LinePosition);
			parsingErrors.Add(item);
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000ACD94 File Offset: 0x000AAF94
		internal static EdmSchemaError AddToSchemaErrorsWithMemberInfo(Func<object, string> messageFormat, string errorMember, MappingErrorCode errorCode, string location, IXmlLineInfo lineInfo, IList<EdmSchemaError> parsingErrors)
		{
			EdmSchemaError edmSchemaError = new EdmSchemaError(messageFormat(errorMember), (int)errorCode, EdmSchemaErrorSeverity.Error, location, lineInfo.LineNumber, lineInfo.LinePosition);
			parsingErrors.Add(edmSchemaError);
			return edmSchemaError;
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000ACDC8 File Offset: 0x000AAFC8
		internal static void AddToSchemaErrorWithMemberAndStructure(Func<object, object, string> messageFormat, string errorMember, string errorStructure, MappingErrorCode errorCode, string location, IXmlLineInfo lineInfo, IList<EdmSchemaError> parsingErrors)
		{
			EdmSchemaError item = new EdmSchemaError(messageFormat(errorMember, errorStructure), (int)errorCode, EdmSchemaErrorSeverity.Error, location, lineInfo.LineNumber, lineInfo.LinePosition);
			parsingErrors.Add(item);
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000ACE00 File Offset: 0x000AB000
		private static void AddToSchemaErrorWithMessage(string errorMessage, MappingErrorCode errorCode, string location, IXmlLineInfo lineInfo, IList<EdmSchemaError> parsingErrors)
		{
			EdmSchemaError item = new EdmSchemaError(errorMessage, (int)errorCode, EdmSchemaErrorSeverity.Error, location, lineInfo.LineNumber, lineInfo.LinePosition);
			parsingErrors.Add(item);
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000ACE2B File Offset: 0x000AB02B
		private string GetAliasResolvedAttributeValue(XPathNavigator nav, string attributeName)
		{
			return this.GetAliasResolvedValue(MappingItemLoader.GetAttributeValue(nav, attributeName));
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000ACE3C File Offset: 0x000AB03C
		private static bool GetBoolAttributeValue(XPathNavigator nav, string attributeName, bool defaultValue)
		{
			bool result = defaultValue;
			object typedAttributeValue = Helper.GetTypedAttributeValue(nav, attributeName, typeof(bool));
			if (typedAttributeValue != null)
			{
				result = (bool)typedAttributeValue;
			}
			return result;
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000ACE68 File Offset: 0x000AB068
		private static string GetAttributeValue(XPathNavigator nav, string attributeName)
		{
			return Helper.GetAttributeValue(nav, attributeName);
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000ACE74 File Offset: 0x000AB074
		private static bool TryGetTypedAttributeValue(XPathNavigator nav, string attributeName, Type clrType, string sourceLocation, IList<EdmSchemaError> parsingErrors, out object value)
		{
			value = null;
			try
			{
				value = Helper.GetTypedAttributeValue(nav, attributeName, clrType);
			}
			catch (FormatException)
			{
				MappingItemLoader.AddToSchemaErrors(Strings.Mapping_ConditionValueTypeMismatch, MappingErrorCode.ConditionError, sourceLocation, (IXmlLineInfo)nav, parsingErrors);
				return false;
			}
			return true;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000ACEC4 File Offset: 0x000AB0C4
		private static EnumMember GetEnumAttributeValue(XPathNavigator nav, string attributeName, EnumType enumType, string sourceLocation, IList<EdmSchemaError> parsingErrors)
		{
			IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
			string attributeValue = MappingItemLoader.GetAttributeValue(nav, attributeName);
			if (string.IsNullOrEmpty(attributeValue))
			{
				MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_Enum_EmptyValue), enumType.FullName, MappingErrorCode.InvalidEnumValue, sourceLocation, lineInfo, parsingErrors);
			}
			EnumMember result;
			if (!enumType.Members.TryGetValue(attributeValue, false, out result))
			{
				MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_Enum_InvalidValue), attributeValue, MappingErrorCode.InvalidEnumValue, sourceLocation, lineInfo, parsingErrors);
			}
			return result;
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000ACF3C File Offset: 0x000AB13C
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

		// Token: 0x060024DC RID: 9436 RVA: 0x000ACF90 File Offset: 0x000AB190
		private XmlReader GetSchemaValidatingReader(XmlReader innerReader)
		{
			XmlReaderSettings xmlReaderSettings = this.GetXmlReaderSettings();
			return XmlReader.Create(innerReader, xmlReaderSettings);
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000ACFB0 File Offset: 0x000AB1B0
		private XmlReaderSettings GetXmlReaderSettings()
		{
			XmlReaderSettings xmlReaderSettings = Schema.CreateEdmStandardXmlReaderSettings();
			xmlReaderSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
			xmlReaderSettings.ValidationEventHandler += this.XsdValidationCallBack;
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			xmlReaderSettings.Schemas = MappingItemLoader.GetOrCreateSchemaSet();
			return xmlReaderSettings;
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000ACFF8 File Offset: 0x000AB1F8
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

		// Token: 0x060024DF RID: 9439 RVA: 0x000AD084 File Offset: 0x000AB284
		private void ValidateAndUpdateScalarMemberMapping(EdmProperty member, EdmProperty columnMember, IXmlLineInfo lineInfo)
		{
			KeyValuePair<TypeUsage, TypeUsage> keyValuePair;
			if (!this.m_scalarMemberMappings.TryGetValue(member, out keyValuePair))
			{
				int count = this.m_parsingErrors.Count;
				TypeUsage typeUsage = Helper.ValidateAndConvertTypeUsage(member, columnMember);
				if (typeUsage != null)
				{
					this.m_scalarMemberMappings.Add(member, new KeyValuePair<TypeUsage, TypeUsage>(typeUsage, columnMember.TypeUsage));
					return;
				}
				if (count == this.m_parsingErrors.Count)
				{
					EdmSchemaError item = new EdmSchemaError(MappingItemLoader.GetInvalidMemberMappingErrorMessage(member, columnMember), 2019, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, lineInfo.LineNumber, lineInfo.LinePosition);
					this.m_parsingErrors.Add(item);
					return;
				}
			}
			else
			{
				TypeUsage value = keyValuePair.Value;
				TypeUsage modelTypeUsage = columnMember.TypeUsage.ModelTypeUsage;
				if (!object.ReferenceEquals(columnMember.TypeUsage.EdmType, value.EdmType))
				{
					EdmSchemaError item2 = new EdmSchemaError(Strings.Mapping_StoreTypeMismatch_ScalarPropertyMapping(member.Name, value.EdmType.Name), 2039, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, lineInfo.LineNumber, lineInfo.LinePosition);
					this.m_parsingErrors.Add(item2);
					return;
				}
				if (!TypeSemantics.IsSubTypeOf(MappingItemLoader.ResolveTypeUsageForEnums(member.TypeUsage), modelTypeUsage))
				{
					EdmSchemaError item3 = new EdmSchemaError(MappingItemLoader.GetInvalidMemberMappingErrorMessage(member, columnMember), 2019, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, lineInfo.LineNumber, lineInfo.LinePosition);
					this.m_parsingErrors.Add(item3);
				}
			}
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000AD1D0 File Offset: 0x000AB3D0
		internal static string GetInvalidMemberMappingErrorMessage(EdmMember cSpaceMember, EdmMember sSpaceMember)
		{
			return Strings.Mapping_Invalid_Member_Mapping(cSpaceMember.TypeUsage.EdmType + MappingItemLoader.GetFacetsForDisplay(cSpaceMember.TypeUsage), cSpaceMember.Name, cSpaceMember.DeclaringType.FullName, sSpaceMember.TypeUsage.EdmType + MappingItemLoader.GetFacetsForDisplay(sSpaceMember.TypeUsage), sSpaceMember.Name, sSpaceMember.DeclaringType.FullName);
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000AD23C File Offset: 0x000AB43C
		private static string GetFacetsForDisplay(TypeUsage typeUsage)
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

		// Token: 0x060024E2 RID: 9442 RVA: 0x000AD2E5 File Offset: 0x000AB4E5
		internal static TypeUsage ResolveTypeUsageForEnums(TypeUsage typeUsage)
		{
			if (!Helper.IsEnumType(typeUsage.EdmType))
			{
				return typeUsage;
			}
			return TypeUsage.Create(Helper.GetUnderlyingEdmTypeForEnumType(typeUsage.EdmType), typeUsage.Facets);
		}

		// Token: 0x04000D44 RID: 3396
		private readonly Dictionary<string, string> m_alias;

		// Token: 0x04000D45 RID: 3397
		private readonly StorageMappingItemCollection m_storageMappingItemCollection;

		// Token: 0x04000D46 RID: 3398
		private readonly string m_sourceLocation;

		// Token: 0x04000D47 RID: 3399
		private readonly List<EdmSchemaError> m_parsingErrors;

		// Token: 0x04000D48 RID: 3400
		private readonly Dictionary<EdmMember, KeyValuePair<TypeUsage, TypeUsage>> m_scalarMemberMappings;

		// Token: 0x04000D49 RID: 3401
		private bool m_hasQueryViews;

		// Token: 0x04000D4A RID: 3402
		private string m_currentNamespaceUri;

		// Token: 0x04000D4B RID: 3403
		private readonly EntityContainerMapping m_containerMapping;

		// Token: 0x04000D4C RID: 3404
		private readonly double m_version;

		// Token: 0x04000D4D RID: 3405
		private static XmlSchemaSet s_mappingXmlSchema;

		// Token: 0x020003E5 RID: 997
		private class ModificationFunctionMappingLoader
		{
			// Token: 0x060024E7 RID: 9447 RVA: 0x000AD30C File Offset: 0x000AB50C
			internal ModificationFunctionMappingLoader(MappingItemLoader parentLoader, EntitySetBase extent)
			{
				this.m_parentLoader = parentLoader;
				this.m_modelContainer = extent.EntityContainer;
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

			// Token: 0x060024E8 RID: 9448 RVA: 0x000AD380 File Offset: 0x000AB580
			internal ModificationFunctionMapping LoadEntityTypeModificationFunctionMapping(XPathNavigator nav, EntitySetBase entitySet, bool allowCurrentVersion, bool allowOriginalVersion, EntityType entityType)
			{
				FunctionParameter rowsAffectedParameter;
				this.m_function = this.LoadAndValidateFunctionMetadata(nav.Clone(), out rowsAffectedParameter);
				if (this.m_function == null)
				{
					return null;
				}
				this.m_allowCurrentVersion = allowCurrentVersion;
				this.m_allowOriginalVersion = allowOriginalVersion;
				IEnumerable<ModificationFunctionParameterBinding> parameterBindings = this.LoadParameterBindings(nav.Clone(), entityType);
				IEnumerable<ModificationFunctionResultBinding> resultBindings = this.LoadResultBindings(nav.Clone(), entityType);
				return new ModificationFunctionMapping(entitySet, entityType, this.m_function, parameterBindings, rowsAffectedParameter, resultBindings);
			}

			// Token: 0x060024E9 RID: 9449 RVA: 0x000AD3EC File Offset: 0x000AB5EC
			internal ModificationFunctionMapping LoadAssociationSetModificationFunctionMapping(XPathNavigator nav, EntitySetBase entitySet, bool isInsert)
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
				IEnumerable<ModificationFunctionParameterBinding> parameterBindings = this.LoadParameterBindings(nav.Clone(), this.m_associationSet.ElementType);
				return new ModificationFunctionMapping(entitySet, entitySet.ElementType, this.m_function, parameterBindings, rowsAffectedParameter, null);
			}

			// Token: 0x060024EA RID: 9450 RVA: 0x000AD468 File Offset: 0x000AB668
			private IEnumerable<ModificationFunctionResultBinding> LoadResultBindings(XPathNavigator nav, EntityType entityType)
			{
				List<ModificationFunctionResultBinding> list = new List<ModificationFunctionResultBinding>();
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
							ModificationFunctionResultBinding item = new ModificationFunctionResultBinding(aliasResolvedAttributeValue2, property);
							list.Add(item);
						}
						if (!nav.MoveToNext(XPathNodeType.Element))
						{
							goto IL_CB;
						}
					}
					MappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_ModificationFunction_PropertyNotFound), aliasResolvedAttributeValue, entityType.Name, MappingErrorCode.InvalidEdmMember, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return new List<ModificationFunctionResultBinding>();
				}
				IL_CB:
				KeyToListMap<EdmProperty, string> keyToListMap = new KeyToListMap<EdmProperty, string>(EqualityComparer<EdmProperty>.Default);
				foreach (ModificationFunctionResultBinding modificationFunctionResultBinding in list)
				{
					keyToListMap.Add(modificationFunctionResultBinding.Property, modificationFunctionResultBinding.ColumnName);
				}
				foreach (EdmProperty edmProperty in keyToListMap.Keys)
				{
					ReadOnlyCollection<string> readOnlyCollection = keyToListMap.ListForKey(edmProperty);
					if (1 < readOnlyCollection.Count)
					{
						MappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_ModificationFunction_AmbiguousResultBinding), edmProperty.Name, StringUtil.ToCommaSeparatedString(readOnlyCollection), MappingErrorCode.AmbiguousResultBindingInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
						return new List<ModificationFunctionResultBinding>();
					}
				}
				return list;
			}

			// Token: 0x060024EB RID: 9451 RVA: 0x000AD638 File Offset: 0x000AB838
			private IEnumerable<ModificationFunctionParameterBinding> LoadParameterBindings(XPathNavigator nav, StructuralType type)
			{
				List<ModificationFunctionParameterBinding> result = new List<ModificationFunctionParameterBinding>(this.LoadParameterBindings(nav.Clone(), type, false));
				Set<FunctionParameter> set = new Set<FunctionParameter>(this.m_function.Parameters);
				set.Subtract(this.m_seenParameters);
				if (set.Count != 0)
				{
					MappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_ModificationFunction_MissingParameter), this.m_function.FullName, StringUtil.ToCommaSeparatedString(set), MappingErrorCode.InvalidParameterInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, (IXmlLineInfo)nav, this.m_parentLoader.m_parsingErrors);
					return new List<ModificationFunctionParameterBinding>();
				}
				return result;
			}

			// Token: 0x060024EC RID: 9452 RVA: 0x000ADC3C File Offset: 0x000ABE3C
			private IEnumerable<ModificationFunctionParameterBinding> LoadParameterBindings(XPathNavigator nav, StructuralType type, bool restrictToKeyMembers)
			{
				if (nav.MoveToChild(XPathNodeType.Element))
				{
					do
					{
						string localName;
						if ((localName = nav.LocalName) != null)
						{
							if (!(localName == "ScalarProperty"))
							{
								if (!(localName == "ComplexProperty"))
								{
									if (!(localName == "AssociationEnd"))
									{
										if (localName == "EndProperty")
										{
											AssociationSetEnd end = this.LoadEndProperty(nav.Clone());
											if (end != null)
											{
												this.m_members.Push(end.CorrespondingAssociationEndMember);
												foreach (ModificationFunctionParameterBinding binding in this.LoadParameterBindings(nav.Clone(), end.EntitySet.ElementType, true))
												{
													yield return binding;
												}
												this.m_members.Pop();
											}
										}
									}
									else
									{
										AssociationSetEnd toEnd = this.LoadAssociationEnd(nav.Clone());
										if (toEnd != null)
										{
											this.m_members.Push(toEnd.CorrespondingAssociationEndMember);
											this.m_associationSetNavigation = toEnd.ParentAssociationSet;
											foreach (ModificationFunctionParameterBinding binding2 in this.LoadParameterBindings(nav.Clone(), toEnd.EntitySet.ElementType, true))
											{
												yield return binding2;
											}
											this.m_associationSetNavigation = null;
											this.m_members.Pop();
										}
									}
								}
								else
								{
									ComplexType complexType;
									EdmMember property = this.LoadComplexTypeProperty(nav.Clone(), type, out complexType);
									if (property != null)
									{
										this.m_members.Push(property);
										foreach (ModificationFunctionParameterBinding binding3 in this.LoadParameterBindings(nav.Clone(), complexType, restrictToKeyMembers))
										{
											yield return binding3;
										}
										this.m_members.Pop();
									}
								}
							}
							else
							{
								ModificationFunctionParameterBinding binding4 = this.LoadScalarPropertyParameterBinding(nav.Clone(), type, restrictToKeyMembers);
								if (binding4 == null)
								{
									break;
								}
								yield return binding4;
							}
						}
					}
					while (nav.MoveToNext(XPathNodeType.Element));
				}
				yield break;
			}

			// Token: 0x060024ED RID: 9453 RVA: 0x000ADC88 File Offset: 0x000ABE88
			private AssociationSetEnd LoadAssociationEnd(XPathNavigator nav)
			{
				IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
				string aliasResolvedAttributeValue = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "AssociationSet");
				string aliasResolvedAttributeValue2 = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "From");
				string aliasResolvedAttributeValue3 = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "To");
				RelationshipSet relationshipSet = null;
				if (aliasResolvedAttributeValue == null || !this.m_modelContainer.TryGetRelationshipSetByName(aliasResolvedAttributeValue, false, out relationshipSet) || BuiltInTypeKind.AssociationSet != relationshipSet.BuiltInTypeKind)
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AssociationSetDoesNotExist), aliasResolvedAttributeValue, MappingErrorCode.InvalidAssociationSet, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				AssociationSet associationSet = (AssociationSet)relationshipSet;
				AssociationSetEnd associationSetEnd = null;
				if (aliasResolvedAttributeValue2 == null || !associationSet.AssociationSetEnds.TryGetValue(aliasResolvedAttributeValue2, false, out associationSetEnd))
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AssociationSetRoleDoesNotExist), aliasResolvedAttributeValue2, MappingErrorCode.InvalidAssociationSetRoleInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				AssociationSetEnd associationSetEnd2 = null;
				if (aliasResolvedAttributeValue3 == null || !associationSet.AssociationSetEnds.TryGetValue(aliasResolvedAttributeValue3, false, out associationSetEnd2))
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AssociationSetRoleDoesNotExist), aliasResolvedAttributeValue3, MappingErrorCode.InvalidAssociationSetRoleInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				if (!associationSetEnd.EntitySet.Equals(this.m_entitySet))
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AssociationSetFromRoleIsNotEntitySet), aliasResolvedAttributeValue2, MappingErrorCode.InvalidAssociationSetRoleInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				if (associationSetEnd2.CorrespondingAssociationEndMember.RelationshipMultiplicity != RelationshipMultiplicity.One && associationSetEnd2.CorrespondingAssociationEndMember.RelationshipMultiplicity != RelationshipMultiplicity.ZeroOrOne)
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AssociationSetCardinality), aliasResolvedAttributeValue3, MappingErrorCode.InvalidAssociationSetCardinalityInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				if (associationSet.ElementType.IsForeignKey)
				{
					System.Data.Entity.Core.Metadata.Edm.ReferentialConstraint referentialConstraint = associationSet.ElementType.ReferentialConstraints.Single<System.Data.Entity.Core.Metadata.Edm.ReferentialConstraint>();
					EdmSchemaError edmSchemaError = MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AssociationEndMappingForeignKeyAssociation), aliasResolvedAttributeValue3, MappingErrorCode.InvalidModificationFunctionMappingAssociationEndForeignKey, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					if (associationSetEnd.CorrespondingAssociationEndMember == referentialConstraint.ToRole)
					{
						if (referentialConstraint.ToProperties.All((EdmProperty p) => this.m_entitySet.ElementType.KeyMembers.Contains(p)))
						{
							edmSchemaError.Severity = EdmSchemaErrorSeverity.Warning;
							return associationSetEnd2;
						}
					}
					return null;
				}
				return associationSetEnd2;
			}

			// Token: 0x060024EE RID: 9454 RVA: 0x000ADEF8 File Offset: 0x000AC0F8
			private AssociationSetEnd LoadEndProperty(XPathNavigator nav)
			{
				string aliasResolvedAttributeValue = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "Name");
				AssociationSetEnd result = null;
				if (aliasResolvedAttributeValue == null || !this.m_associationSet.AssociationSetEnds.TryGetValue(aliasResolvedAttributeValue, false, out result))
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AssociationSetRoleDoesNotExist), aliasResolvedAttributeValue, MappingErrorCode.InvalidAssociationSetRoleInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, (IXmlLineInfo)nav, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				return result;
			}

			// Token: 0x060024EF RID: 9455 RVA: 0x000ADF70 File Offset: 0x000AC170
			private EdmMember LoadComplexTypeProperty(XPathNavigator nav, StructuralType type, out ComplexType complexType)
			{
				IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
				string aliasResolvedAttributeValue = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "Name");
				string aliasResolvedAttributeValue2 = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "TypeName");
				EdmMember edmMember = null;
				if (aliasResolvedAttributeValue == null || !type.Members.TryGetValue(aliasResolvedAttributeValue, false, out edmMember))
				{
					MappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_ModificationFunction_PropertyNotFound), aliasResolvedAttributeValue, type.Name, MappingErrorCode.InvalidEdmMember, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					complexType = null;
					return null;
				}
				complexType = null;
				if (aliasResolvedAttributeValue2 == null || !this.m_edmItemCollection.TryGetItem<ComplexType>(aliasResolvedAttributeValue2, out complexType))
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_ComplexTypeNotFound), aliasResolvedAttributeValue2, MappingErrorCode.InvalidComplexType, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				if (!edmMember.TypeUsage.EdmType.Equals(complexType) && !Helper.IsSubtypeOf(edmMember.TypeUsage.EdmType, complexType))
				{
					MappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_ModificationFunction_WrongComplexType), aliasResolvedAttributeValue2, edmMember.Name, MappingErrorCode.InvalidComplexType, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				return edmMember;
			}

			// Token: 0x060024F0 RID: 9456 RVA: 0x000AE0A8 File Offset: 0x000AC2A8
			[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
			private ModificationFunctionParameterBinding LoadScalarPropertyParameterBinding(XPathNavigator nav, StructuralType type, bool restrictToKeyMembers)
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
							MappingItemLoader.AddToSchemaErrors(Strings.Mapping_ModificationFunction_MissingVersion, MappingErrorCode.MissingVersionInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
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
					MappingItemLoader.AddToSchemaErrors(Strings.Mapping_ModificationFunction_VersionMustBeOriginal, MappingErrorCode.InvalidVersionInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				if (!flag && !this.m_allowOriginalVersion)
				{
					MappingItemLoader.AddToSchemaErrors(Strings.Mapping_ModificationFunction_VersionMustBeCurrent, MappingErrorCode.InvalidVersionInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				FunctionParameter functionParameter = null;
				if (aliasResolvedAttributeValue == null || !this.m_function.Parameters.TryGetValue(aliasResolvedAttributeValue, false, out functionParameter))
				{
					MappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_ModificationFunction_ParameterNotFound), aliasResolvedAttributeValue, this.m_function.Name, MappingErrorCode.InvalidParameterInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				EdmMember edmMember = null;
				if (restrictToKeyMembers)
				{
					if (aliasResolvedAttributeValue2 == null || !((EntityType)type).KeyMembers.TryGetValue(aliasResolvedAttributeValue2, false, out edmMember))
					{
						MappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_ModificationFunction_PropertyNotKey), aliasResolvedAttributeValue2, type.Name, MappingErrorCode.InvalidEdmMember, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
						return null;
					}
				}
				else if (aliasResolvedAttributeValue2 == null || !type.Members.TryGetValue(aliasResolvedAttributeValue2, false, out edmMember))
				{
					MappingItemLoader.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_ModificationFunction_PropertyNotFound), aliasResolvedAttributeValue2, type.Name, MappingErrorCode.InvalidEdmMember, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				if (this.m_seenParameters.Contains(functionParameter))
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_ParameterBoundTwice), aliasResolvedAttributeValue, MappingErrorCode.ParameterBoundTwiceInModificationFunctionMapping, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				int count = this.m_parentLoader.m_parsingErrors.Count;
				if (Helper.ValidateAndConvertTypeUsage(edmMember.TypeUsage, functionParameter.TypeUsage) == null && count == this.m_parentLoader.m_parsingErrors.Count)
				{
					MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_ModificationFunction_PropertyParameterTypeMismatch(edmMember.TypeUsage.EdmType, edmMember.Name, edmMember.DeclaringType.FullName, functionParameter.TypeUsage.EdmType, functionParameter.Name, this.m_function.FullName), MappingErrorCode.InvalidModificationFunctionMappingPropertyParameterTypeMismatch, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
				}
				this.m_members.Push(edmMember);
				IEnumerable<EdmMember> members = this.m_members;
				AssociationSet associationSet = this.m_associationSetNavigation;
				if (this.m_members.Last<EdmMember>().BuiltInTypeKind == BuiltInTypeKind.AssociationEndMember)
				{
					AssociationEndMember associationEndMember = (AssociationEndMember)this.m_members.Last<EdmMember>();
					AssociationType associationType = (AssociationType)associationEndMember.DeclaringType;
					if (associationType.IsForeignKey)
					{
						System.Data.Entity.Core.Metadata.Edm.ReferentialConstraint referentialConstraint = associationType.ReferentialConstraints.Single<System.Data.Entity.Core.Metadata.Edm.ReferentialConstraint>();
						if (referentialConstraint.FromRole == associationEndMember)
						{
							int index = referentialConstraint.FromProperties.IndexOf((EdmProperty)this.m_members.First<EdmMember>());
							members = new EdmMember[]
							{
								referentialConstraint.ToProperties[index]
							};
							associationSet = null;
						}
					}
				}
				ModificationFunctionParameterBinding result = new ModificationFunctionParameterBinding(functionParameter, new ModificationFunctionMemberPath(members, associationSet), flag);
				this.m_members.Pop();
				this.m_seenParameters.Add(functionParameter);
				return result;
			}

			// Token: 0x060024F1 RID: 9457 RVA: 0x000AE47C File Offset: 0x000AC67C
			private EdmFunction LoadAndValidateFunctionMetadata(XPathNavigator nav, out FunctionParameter rowsAffectedParameter)
			{
				IXmlLineInfo lineInfo = (IXmlLineInfo)nav;
				this.m_seenParameters.Clear();
				string aliasResolvedAttributeValue = this.m_parentLoader.GetAliasResolvedAttributeValue(nav.Clone(), "FunctionName");
				rowsAffectedParameter = null;
				ReadOnlyCollection<EdmFunction> functions = this.m_storeItemCollection.GetFunctions(aliasResolvedAttributeValue);
				if (functions.Count == 0)
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_UnknownFunction), aliasResolvedAttributeValue, MappingErrorCode.InvalidModificationFunctionMappingUnknownFunction, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				if (1 < functions.Count)
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_AmbiguousFunction), aliasResolvedAttributeValue, MappingErrorCode.InvalidModificationFunctionMappingAmbiguousFunction, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				EdmFunction edmFunction = functions[0];
				if (MetadataHelper.IsComposable(edmFunction))
				{
					MappingItemLoader.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_ModificationFunction_NotValidFunction), aliasResolvedAttributeValue, MappingErrorCode.InvalidModificationFunctionMappingNotValidFunction, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
					return null;
				}
				string attributeValue = MappingItemLoader.GetAttributeValue(nav, "RowsAffectedParameter");
				if (!string.IsNullOrEmpty(attributeValue))
				{
					if (!edmFunction.Parameters.TryGetValue(attributeValue, false, out rowsAffectedParameter))
					{
						MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_RowsAffectedParameterDoesNotExist(attributeValue, edmFunction.FullName), MappingErrorCode.MappingFunctionImportRowsAffectedParameterDoesNotExist, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
						return null;
					}
					if (ParameterMode.Out != rowsAffectedParameter.Mode && ParameterMode.InOut != rowsAffectedParameter.Mode)
					{
						MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_RowsAffectedParameterHasWrongMode(attributeValue, rowsAffectedParameter.Mode, ParameterMode.Out, ParameterMode.InOut), MappingErrorCode.MappingFunctionImportRowsAffectedParameterHasWrongMode, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
						return null;
					}
					PrimitiveType primitiveType = (PrimitiveType)rowsAffectedParameter.TypeUsage.EdmType;
					if (!TypeSemantics.IsIntegerNumericType(rowsAffectedParameter.TypeUsage))
					{
						MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_FunctionImport_RowsAffectedParameterHasWrongType(attributeValue, primitiveType.PrimitiveTypeKind), MappingErrorCode.MappingFunctionImportRowsAffectedParameterHasWrongType, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
						return null;
					}
					this.m_seenParameters.Add(rowsAffectedParameter);
				}
				foreach (FunctionParameter functionParameter in edmFunction.Parameters)
				{
					if (functionParameter.Mode != ParameterMode.In && attributeValue != functionParameter.Name)
					{
						MappingItemLoader.AddToSchemaErrorWithMessage(Strings.Mapping_ModificationFunction_NotValidFunctionParameter(aliasResolvedAttributeValue, functionParameter.Name, "RowsAffectedParameter"), MappingErrorCode.InvalidModificationFunctionMappingNotValidFunctionParameter, this.m_parentLoader.m_sourceLocation, lineInfo, this.m_parentLoader.m_parsingErrors);
						return null;
					}
				}
				return edmFunction;
			}

			// Token: 0x04000D52 RID: 3410
			private readonly MappingItemLoader m_parentLoader;

			// Token: 0x04000D53 RID: 3411
			private EdmFunction m_function;

			// Token: 0x04000D54 RID: 3412
			private readonly EntitySet m_entitySet;

			// Token: 0x04000D55 RID: 3413
			private readonly AssociationSet m_associationSet;

			// Token: 0x04000D56 RID: 3414
			private readonly System.Data.Entity.Core.Metadata.Edm.EntityContainer m_modelContainer;

			// Token: 0x04000D57 RID: 3415
			private readonly EdmItemCollection m_edmItemCollection;

			// Token: 0x04000D58 RID: 3416
			private readonly StoreItemCollection m_storeItemCollection;

			// Token: 0x04000D59 RID: 3417
			private bool m_allowCurrentVersion;

			// Token: 0x04000D5A RID: 3418
			private bool m_allowOriginalVersion;

			// Token: 0x04000D5B RID: 3419
			private readonly Set<FunctionParameter> m_seenParameters;

			// Token: 0x04000D5C RID: 3420
			private readonly Stack<EdmMember> m_members;

			// Token: 0x04000D5D RID: 3421
			private AssociationSet m_associationSetNavigation;
		}
	}
}
