using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees;
using System.Data.Common.EntitySql;
using System.Data.Common.QueryCache;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping;
using System.Data.Mapping.Update.Internal;
using System.Data.Mapping.ViewGeneration;
using System.Data.Objects.DataClasses;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000202 RID: 514
	public sealed class MetadataWorkspace
	{
		// Token: 0x060021BE RID: 8638 RVA: 0x00076F3D File Offset: 0x0007513D
		public MetadataWorkspace()
		{
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x00076F50 File Offset: 0x00075150
		public MetadataWorkspace(IEnumerable<string> paths, IEnumerable<Assembly> assembliesToConsider)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<string>>(paths, "paths");
			EntityUtil.CheckArgumentContainsNull<string>(ref paths, "paths");
			EntityUtil.CheckArgumentNull<IEnumerable<Assembly>>(assembliesToConsider, "assembliesToConsider");
			EntityUtil.CheckArgumentContainsNull<Assembly>(ref assembliesToConsider, "assembliesToConsider");
			Func<AssemblyName, Assembly> resolveReference = delegate(AssemblyName referenceName)
			{
				foreach (Assembly assembly in assembliesToConsider)
				{
					if (AssemblyName.ReferenceMatchesDefinition(referenceName, new AssemblyName(assembly.FullName)))
					{
						return assembly;
					}
				}
				throw EntityUtil.Argument(Strings.AssemblyMissingFromAssembliesToConsider(referenceName.FullName), "assembliesToConsider");
			};
			this.CreateMetadataWorkspaceWithResolver(paths, () => assembliesToConsider, resolveReference);
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x00076FD8 File Offset: 0x000751D8
		private void CreateMetadataWorkspaceWithResolver(IEnumerable<string> paths, Func<IEnumerable<Assembly>> wildcardAssemblies, Func<AssemblyName, Assembly> resolveReference)
		{
			MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromFilePaths(paths.ToArray<string>(), "", new CustomAssemblyResolver(wildcardAssemblies, resolveReference));
			DataSpace spaceToGet = DataSpace.CSpace;
			using (DisposableCollectionWrapper<XmlReader> disposableCollectionWrapper = new DisposableCollectionWrapper<XmlReader>(metadataArtifactLoader.CreateReaders(spaceToGet)))
			{
				if (disposableCollectionWrapper.Any<XmlReader>())
				{
					this._itemsCSpace = new EdmItemCollection(disposableCollectionWrapper, metadataArtifactLoader.GetPaths(spaceToGet));
				}
			}
			spaceToGet = DataSpace.SSpace;
			using (DisposableCollectionWrapper<XmlReader> disposableCollectionWrapper2 = new DisposableCollectionWrapper<XmlReader>(metadataArtifactLoader.CreateReaders(spaceToGet)))
			{
				if (disposableCollectionWrapper2.Any<XmlReader>())
				{
					this._itemsSSpace = new StoreItemCollection(disposableCollectionWrapper2, metadataArtifactLoader.GetPaths(spaceToGet));
				}
			}
			spaceToGet = DataSpace.CSSpace;
			using (DisposableCollectionWrapper<XmlReader> disposableCollectionWrapper3 = new DisposableCollectionWrapper<XmlReader>(metadataArtifactLoader.CreateReaders(spaceToGet)))
			{
				if (disposableCollectionWrapper3.Any<XmlReader>() && this._itemsCSpace != null && this._itemsSSpace != null)
				{
					this._itemsCSSpace = new StorageMappingItemCollection(this._itemsCSpace, this._itemsSSpace, disposableCollectionWrapper3, metadataArtifactLoader.GetPaths(spaceToGet));
				}
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x000770E8 File Offset: 0x000752E8
		private static IEnumerable<double> SupportedEdmVersions
		{
			get
			{
				yield return 0.0;
				yield return 1.0;
				yield return 2.0;
				yield return 3.0;
				yield break;
			}
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x000770FE File Offset: 0x000752FE
		public EntitySqlParser CreateEntitySqlParser()
		{
			return new EntitySqlParser(new ModelPerspective(this));
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x0007710B File Offset: 0x0007530B
		public DbQueryCommandTree CreateQueryCommandTree(DbExpression query)
		{
			return new DbQueryCommandTree(this, DataSpace.CSpace, query);
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x00077118 File Offset: 0x00075318
		[CLSCompliant(false)]
		public ItemCollection GetItemCollection(DataSpace dataSpace)
		{
			return this.GetItemCollection(dataSpace, true);
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x00077130 File Offset: 0x00075330
		[CLSCompliant(false)]
		public void RegisterItemCollection(ItemCollection collection)
		{
			EntityUtil.CheckArgumentNull<ItemCollection>(collection, "collection");
			ItemCollection itemCollection;
			try
			{
				switch (collection.DataSpace)
				{
				case DataSpace.OSpace:
					if ((itemCollection = this._itemsOSpace) == null)
					{
						this._itemsOSpace = (ObjectItemCollection)collection;
						goto IL_13C;
					}
					goto IL_13C;
				case DataSpace.CSpace:
				{
					if ((itemCollection = this._itemsCSpace) != null)
					{
						goto IL_13C;
					}
					EdmItemCollection edmItemCollection = (EdmItemCollection)collection;
					if (!MetadataWorkspace.SupportedEdmVersions.Contains(edmItemCollection.EdmVersion))
					{
						throw EntityUtil.InvalidOperation(Strings.EdmVersionNotSupportedByRuntime(edmItemCollection.EdmVersion, Helper.GetCommaDelimitedString(from e in MetadataWorkspace.SupportedEdmVersions
						where e != 0.0
						select e.ToString(CultureInfo.InvariantCulture))));
					}
					this.CheckAndSetItemCollectionVersionInWorkSpace(collection);
					this._itemsCSpace = edmItemCollection;
					goto IL_13C;
				}
				case DataSpace.SSpace:
					if ((itemCollection = this._itemsSSpace) == null)
					{
						this.CheckAndSetItemCollectionVersionInWorkSpace(collection);
						this._itemsSSpace = (StoreItemCollection)collection;
						goto IL_13C;
					}
					goto IL_13C;
				case DataSpace.CSSpace:
					if ((itemCollection = this._itemsCSSpace) == null)
					{
						this.CheckAndSetItemCollectionVersionInWorkSpace(collection);
						this._itemsCSSpace = (StorageMappingItemCollection)collection;
						goto IL_13C;
					}
					goto IL_13C;
				}
				if ((itemCollection = this._itemsOCSpace) == null)
				{
					this._itemsOCSpace = (DefaultObjectMappingItemCollection)collection;
				}
				IL_13C:;
			}
			catch (InvalidCastException)
			{
				throw EntityUtil.InvalidCollectionForMapping(collection.DataSpace);
			}
			if (itemCollection != null)
			{
				throw EntityUtil.ItemCollectionAlreadyRegistered(collection.DataSpace);
			}
			if (collection.DataSpace == DataSpace.CSpace && this._itemsCSSpace != null && this._itemsCSSpace.EdmItemCollection != collection)
			{
				throw EntityUtil.InvalidCollectionSpecified(collection.DataSpace);
			}
			if (collection.DataSpace == DataSpace.SSpace && this._itemsCSSpace != null && this._itemsCSSpace.StoreItemCollection != collection)
			{
				throw EntityUtil.InvalidCollectionSpecified(collection.DataSpace);
			}
			if (collection.DataSpace == DataSpace.CSSpace)
			{
				if (this._itemsCSpace != null && this._itemsCSSpace.EdmItemCollection != this._itemsCSpace)
				{
					throw EntityUtil.InvalidCollectionSpecified(collection.DataSpace);
				}
				if (this._itemsSSpace != null && this._itemsCSSpace.StoreItemCollection != this._itemsSSpace)
				{
					throw EntityUtil.InvalidCollectionSpecified(collection.DataSpace);
				}
			}
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x00077360 File Offset: 0x00075560
		private void CheckAndSetItemCollectionVersionInWorkSpace(ItemCollection itemCollectionToRegister)
		{
			double num = 0.0;
			string itemCollectionType = null;
			switch (itemCollectionToRegister.DataSpace)
			{
			case DataSpace.CSpace:
				num = ((EdmItemCollection)itemCollectionToRegister).EdmVersion;
				itemCollectionType = "EdmItemCollection";
				break;
			case DataSpace.SSpace:
				num = ((StoreItemCollection)itemCollectionToRegister).StoreSchemaVersion;
				itemCollectionType = "StoreItemCollection";
				break;
			case DataSpace.CSSpace:
				num = ((StorageMappingItemCollection)itemCollectionToRegister).MappingVersion;
				itemCollectionType = "StorageMappingItemCollection";
				break;
			}
			if (num != this._schemaVersion && num != 0.0 && this._schemaVersion != 0.0)
			{
				throw EntityUtil.DifferentSchemaVersionInCollection(itemCollectionType, num, this._schemaVersion);
			}
			this._schemaVersion = num;
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x0007740F File Offset: 0x0007560F
		internal void AddMetadataEntryToken(object token)
		{
			if (this._cacheTokens == null)
			{
				this._cacheTokens = new List<object>();
			}
			this._cacheTokens.Add(token);
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x00077430 File Offset: 0x00075630
		public void LoadFromAssembly(Assembly assembly)
		{
			this.LoadFromAssembly(assembly, null);
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x0007743C File Offset: 0x0007563C
		public void LoadFromAssembly(Assembly assembly, Action<string> logLoadMessage)
		{
			EntityUtil.CheckArgumentNull<Assembly>(assembly, "assembly");
			ObjectItemCollection collection = (ObjectItemCollection)this.GetItemCollection(DataSpace.OSpace);
			this.ExplicitLoadFromAssembly(assembly, collection, logLoadMessage);
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x0007746C File Offset: 0x0007566C
		private void ExplicitLoadFromAssembly(Assembly assembly, ObjectItemCollection collection, Action<string> logLoadMessage)
		{
			ItemCollection itemCollection;
			if (!this.TryGetItemCollection(DataSpace.CSpace, out itemCollection))
			{
				itemCollection = null;
			}
			collection.ExplicitLoadFromAssembly(assembly, (EdmItemCollection)itemCollection, logLoadMessage);
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x00077494 File Offset: 0x00075694
		private void ImplicitLoadFromAssembly(Assembly assembly, ObjectItemCollection collection)
		{
			if (!MetadataAssemblyHelper.ShouldFilterAssembly(assembly))
			{
				this.ExplicitLoadFromAssembly(assembly, collection, null);
			}
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x000774A8 File Offset: 0x000756A8
		internal void ImplicitLoadAssemblyForType(Type type, Assembly callingAssembly)
		{
			ItemCollection itemCollection;
			if (this.TryGetItemCollection(DataSpace.OSpace, out itemCollection))
			{
				ObjectItemCollection objectItemCollection = (ObjectItemCollection)itemCollection;
				ItemCollection itemCollection2;
				this.TryGetItemCollection(DataSpace.CSpace, out itemCollection2);
				if (!objectItemCollection.ImplicitLoadAssemblyForType(type, (EdmItemCollection)itemCollection2) && null != callingAssembly)
				{
					if (!ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent(callingAssembly) && !this._foundAssemblyWithAttribute)
					{
						if (!MetadataAssemblyHelper.GetNonSystemReferencedAssemblies(callingAssembly).Any((Assembly a) => ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent(a)))
						{
							this.ImplicitLoadFromAssembly(callingAssembly, objectItemCollection);
							return;
						}
					}
					this._foundAssemblyWithAttribute = true;
					objectItemCollection.ImplicitLoadAllReferencedAssemblies(callingAssembly, (EdmItemCollection)itemCollection2);
					return;
				}
			}
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x00077548 File Offset: 0x00075748
		internal void ImplicitLoadFromEntityType(EntityType type, Assembly callingAssembly)
		{
			Map map;
			if (!this.TryGetMap(type, DataSpace.OCSpace, out map))
			{
				this.ImplicitLoadAssemblyForType(typeof(IEntityWithKey), callingAssembly);
				ObjectItemCollection objectItemCollection = this.GetItemCollection(DataSpace.OSpace) as ObjectItemCollection;
				EdmType edmType;
				if (objectItemCollection == null || !objectItemCollection.TryGetOSpaceType(type, out edmType))
				{
					throw new InvalidOperationException(Strings.Mapping_Object_InvalidType(type.Identity));
				}
			}
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x000775A0 File Offset: 0x000757A0
		public T GetItem<T>(string identity, DataSpace dataSpace) where T : GlobalItem
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetItem<T>(identity, false);
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x000775C0 File Offset: 0x000757C0
		public bool TryGetItem<T>(string identity, DataSpace space, out T item) where T : GlobalItem
		{
			item = default(T);
			ItemCollection itemCollection = this.GetItemCollection(space, false);
			return itemCollection != null && itemCollection.TryGetItem<T>(identity, false, out item);
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x000775EC File Offset: 0x000757EC
		public T GetItem<T>(string identity, bool ignoreCase, DataSpace dataSpace) where T : GlobalItem
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetItem<T>(identity, ignoreCase);
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x0007760C File Offset: 0x0007580C
		public bool TryGetItem<T>(string identity, bool ignoreCase, DataSpace dataSpace, out T item) where T : GlobalItem
		{
			item = default(T);
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetItem<T>(identity, ignoreCase, out item);
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x0007763C File Offset: 0x0007583C
		public ReadOnlyCollection<T> GetItems<T>(DataSpace dataSpace) where T : GlobalItem
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetItems<T>();
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x00077658 File Offset: 0x00075858
		public EdmType GetType(string name, string namespaceName, DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetType(name, namespaceName, false);
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x00077678 File Offset: 0x00075878
		public bool TryGetType(string name, string namespaceName, DataSpace dataSpace, out EdmType type)
		{
			type = null;
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetType(name, namespaceName, false, out type);
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x000776A4 File Offset: 0x000758A4
		public EdmType GetType(string name, string namespaceName, bool ignoreCase, DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetType(name, namespaceName, ignoreCase);
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x000776C4 File Offset: 0x000758C4
		public bool TryGetType(string name, string namespaceName, bool ignoreCase, DataSpace dataSpace, out EdmType type)
		{
			type = null;
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetType(name, namespaceName, ignoreCase, out type);
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x000776F0 File Offset: 0x000758F0
		public EntityContainer GetEntityContainer(string name, DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetEntityContainer(name);
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x00077710 File Offset: 0x00075910
		public bool TryGetEntityContainer(string name, DataSpace dataSpace, out EntityContainer entityContainer)
		{
			entityContainer = null;
			EntityUtil.GenericCheckArgumentNull<string>(name, "name");
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetEntityContainer(name, out entityContainer);
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x00077744 File Offset: 0x00075944
		public EntityContainer GetEntityContainer(string name, bool ignoreCase, DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetEntityContainer(name, ignoreCase);
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x00077764 File Offset: 0x00075964
		public bool TryGetEntityContainer(string name, bool ignoreCase, DataSpace dataSpace, out EntityContainer entityContainer)
		{
			entityContainer = null;
			EntityUtil.GenericCheckArgumentNull<string>(name, "name");
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetEntityContainer(name, ignoreCase, out entityContainer);
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x00077799 File Offset: 0x00075999
		public ReadOnlyCollection<EdmFunction> GetFunctions(string name, string namespaceName, DataSpace dataSpace)
		{
			return this.GetFunctions(name, namespaceName, dataSpace, false);
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x000777A8 File Offset: 0x000759A8
		public ReadOnlyCollection<EdmFunction> GetFunctions(string name, string namespaceName, DataSpace dataSpace, bool ignoreCase)
		{
			EntityUtil.CheckStringArgument(name, "name");
			EntityUtil.CheckStringArgument(namespaceName, "namespaceName");
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetFunctions(namespaceName + "." + name, ignoreCase);
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x000777E8 File Offset: 0x000759E8
		internal bool TryGetFunction(string name, string namespaceName, TypeUsage[] parameterTypes, bool ignoreCase, DataSpace dataSpace, out EdmFunction function)
		{
			function = null;
			EntityUtil.GenericCheckArgumentNull<string>(name, "name");
			EntityUtil.GenericCheckArgumentNull<string>(namespaceName, "namespaceName");
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetFunction(namespaceName + "." + name, parameterTypes, ignoreCase, out function);
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x00077838 File Offset: 0x00075A38
		public ReadOnlyCollection<PrimitiveType> GetPrimitiveTypes(DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetItems<PrimitiveType>();
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x00077854 File Offset: 0x00075A54
		public ReadOnlyCollection<GlobalItem> GetItems(DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetItems<GlobalItem>();
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x00077870 File Offset: 0x00075A70
		internal PrimitiveType GetMappedPrimitiveType(PrimitiveTypeKind primitiveTypeKind, DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetMappedPrimitiveType(primitiveTypeKind);
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x00077890 File Offset: 0x00075A90
		internal bool TryGetMap(string typeIdentity, DataSpace typeSpace, bool ignoreCase, DataSpace mappingSpace, out Map map)
		{
			map = null;
			ItemCollection itemCollection = this.GetItemCollection(mappingSpace, false);
			return itemCollection != null && ((MappingItemCollection)itemCollection).TryGetMap(typeIdentity, typeSpace, ignoreCase, out map);
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x000778C0 File Offset: 0x00075AC0
		internal Map GetMap(string identity, DataSpace typeSpace, DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return ((MappingItemCollection)itemCollection).GetMap(identity, typeSpace);
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x000778E4 File Offset: 0x00075AE4
		internal Map GetMap(GlobalItem item, DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return ((MappingItemCollection)itemCollection).GetMap(item);
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x00077908 File Offset: 0x00075B08
		internal bool TryGetMap(GlobalItem item, DataSpace dataSpace, out Map map)
		{
			map = null;
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && ((MappingItemCollection)itemCollection).TryGetMap(item, out map);
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x00077934 File Offset: 0x00075B34
		private ItemCollection RegisterDefaultObjectMappingItemCollection()
		{
			EdmItemCollection itemsCSpace = this._itemsCSpace;
			ObjectItemCollection itemsOSpace = this._itemsOSpace;
			if (itemsCSpace != null && itemsOSpace != null)
			{
				this.RegisterItemCollection(new DefaultObjectMappingItemCollection(itemsCSpace, itemsOSpace));
			}
			return this._itemsOCSpace;
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x00077968 File Offset: 0x00075B68
		[CLSCompliant(false)]
		public bool TryGetItemCollection(DataSpace dataSpace, out ItemCollection collection)
		{
			collection = this.GetItemCollection(dataSpace, false);
			return collection != null;
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x0007797C File Offset: 0x00075B7C
		internal ItemCollection GetItemCollection(DataSpace dataSpace, bool required)
		{
			ItemCollection itemCollection;
			switch (dataSpace)
			{
			case DataSpace.OSpace:
				itemCollection = this._itemsOSpace;
				break;
			case DataSpace.CSpace:
				itemCollection = this._itemsCSpace;
				break;
			case DataSpace.SSpace:
				itemCollection = this._itemsSSpace;
				break;
			case DataSpace.OCSpace:
				itemCollection = (this._itemsOCSpace ?? this.RegisterDefaultObjectMappingItemCollection());
				break;
			case DataSpace.CSSpace:
				itemCollection = this._itemsCSSpace;
				break;
			default:
				itemCollection = null;
				break;
			}
			if (required && itemCollection == null)
			{
				throw EntityUtil.NoCollectionForSpace(dataSpace);
			}
			return itemCollection;
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x000779EE File Offset: 0x00075BEE
		public StructuralType GetObjectSpaceType(StructuralType edmSpaceType)
		{
			return this.GetObjectSpaceType<StructuralType>(edmSpaceType);
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x000779F7 File Offset: 0x00075BF7
		public bool TryGetObjectSpaceType(StructuralType edmSpaceType, out StructuralType objectSpaceType)
		{
			return this.TryGetObjectSpaceType<StructuralType>(edmSpaceType, out objectSpaceType);
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x00077A01 File Offset: 0x00075C01
		public EnumType GetObjectSpaceType(EnumType edmSpaceType)
		{
			return this.GetObjectSpaceType<EnumType>(edmSpaceType);
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x00077A0A File Offset: 0x00075C0A
		public bool TryGetObjectSpaceType(EnumType edmSpaceType, out EnumType objectSpaceType)
		{
			return this.TryGetObjectSpaceType<EnumType>(edmSpaceType, out objectSpaceType);
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x00077A14 File Offset: 0x00075C14
		private T GetObjectSpaceType<T>(T edmSpaceType) where T : EdmType
		{
			T result;
			if (!this.TryGetObjectSpaceType<T>(edmSpaceType, out result))
			{
				throw EntityUtil.Argument(Strings.FailedToFindOSpaceTypeMapping(edmSpaceType.Identity));
			}
			return result;
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x00077A44 File Offset: 0x00075C44
		private bool TryGetObjectSpaceType<T>(T edmSpaceType, out T objectSpaceType) where T : EdmType
		{
			EntityUtil.CheckArgumentNull<T>(edmSpaceType, "edmSpaceType");
			if (edmSpaceType.DataSpace != DataSpace.CSpace)
			{
				throw EntityUtil.Argument(Strings.ArgumentMustBeCSpaceType, "edmSpaceType");
			}
			objectSpaceType = default(T);
			Map map;
			if (this.TryGetMap(edmSpaceType, DataSpace.OCSpace, out map))
			{
				ObjectTypeMapping objectTypeMapping = map as ObjectTypeMapping;
				if (objectTypeMapping != null)
				{
					objectSpaceType = (T)((object)objectTypeMapping.ClrType);
				}
			}
			return objectSpaceType != null;
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x00077ABC File Offset: 0x00075CBC
		public StructuralType GetEdmSpaceType(StructuralType objectSpaceType)
		{
			return this.GetEdmSpaceType<StructuralType>(objectSpaceType);
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x00077AC5 File Offset: 0x00075CC5
		public bool TryGetEdmSpaceType(StructuralType objectSpaceType, out StructuralType edmSpaceType)
		{
			return this.TryGetEdmSpaceType<StructuralType>(objectSpaceType, out edmSpaceType);
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x00077ACF File Offset: 0x00075CCF
		public EnumType GetEdmSpaceType(EnumType objectSpaceType)
		{
			return this.GetEdmSpaceType<EnumType>(objectSpaceType);
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x00077AD8 File Offset: 0x00075CD8
		public bool TryGetEdmSpaceType(EnumType objectSpaceType, out EnumType edmSpaceType)
		{
			return this.TryGetEdmSpaceType<EnumType>(objectSpaceType, out edmSpaceType);
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x00077AE4 File Offset: 0x00075CE4
		private T GetEdmSpaceType<T>(T objectSpaceType) where T : EdmType
		{
			T result;
			if (!this.TryGetEdmSpaceType<T>(objectSpaceType, out result))
			{
				throw EntityUtil.Argument(Strings.FailedToFindCSpaceTypeMapping(objectSpaceType.Identity));
			}
			return result;
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x00077B14 File Offset: 0x00075D14
		private bool TryGetEdmSpaceType<T>(T objectSpaceType, out T edmSpaceType) where T : EdmType
		{
			EntityUtil.CheckArgumentNull<T>(objectSpaceType, "objectSpaceType");
			if (objectSpaceType.DataSpace != DataSpace.OSpace)
			{
				throw EntityUtil.Argument(Strings.ArgumentMustBeOSpaceType, "objectSpaceType");
			}
			edmSpaceType = default(T);
			Map map;
			if (this.TryGetMap(objectSpaceType, DataSpace.OCSpace, out map))
			{
				ObjectTypeMapping objectTypeMapping = map as ObjectTypeMapping;
				if (objectTypeMapping != null)
				{
					edmSpaceType = (T)((object)objectTypeMapping.EdmType);
				}
			}
			return edmSpaceType != null;
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x00077B8B File Offset: 0x00075D8B
		internal DbQueryCommandTree GetCqtView(EntitySetBase extent)
		{
			return this.GetGeneratedView(extent).GetCommandTree();
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x00077B9C File Offset: 0x00075D9C
		internal GeneratedView GetGeneratedView(EntitySetBase extent)
		{
			ItemCollection itemCollection = this.GetItemCollection(DataSpace.CSSpace, true);
			return ((StorageMappingItemCollection)itemCollection).GetGeneratedView(extent, this);
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x00077BC0 File Offset: 0x00075DC0
		internal bool TryGetGeneratedViewOfType(EntitySetBase extent, EntityTypeBase type, bool includeSubtypes, out GeneratedView generatedView)
		{
			ItemCollection itemCollection = this.GetItemCollection(DataSpace.CSSpace, true);
			return ((StorageMappingItemCollection)itemCollection).TryGetGeneratedViewOfType(this, extent, type, includeSubtypes, out generatedView);
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x00077BE8 File Offset: 0x00075DE8
		internal DbLambda GetGeneratedFunctionDefinition(EdmFunction function)
		{
			ItemCollection itemCollection = this.GetItemCollection(DataSpace.CSpace, true);
			return ((EdmItemCollection)itemCollection).GetGeneratedFunctionDefinition(function);
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x00077C0C File Offset: 0x00075E0C
		internal bool TryGetFunctionImportMapping(EdmFunction functionImport, out FunctionImportMapping targetFunctionMapping)
		{
			ReadOnlyCollection<StorageEntityContainerMapping> items = this.GetItems<StorageEntityContainerMapping>(DataSpace.CSSpace);
			foreach (StorageEntityContainerMapping storageEntityContainerMapping in items)
			{
				if (storageEntityContainerMapping.TryGetFunctionImportMapping(functionImport, out targetFunctionMapping))
				{
					return true;
				}
			}
			targetFunctionMapping = null;
			return false;
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x00077C6C File Offset: 0x00075E6C
		internal ViewLoader GetUpdateViewLoader()
		{
			if (this._itemsCSSpace != null)
			{
				return this._itemsCSSpace.GetUpdateViewLoader();
			}
			return null;
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x00077C84 File Offset: 0x00075E84
		internal TypeUsage GetOSpaceTypeUsage(TypeUsage edmSpaceTypeUsage)
		{
			EntityUtil.CheckArgumentNull<TypeUsage>(edmSpaceTypeUsage, "edmSpaceTypeUsage");
			EdmType edmType;
			if (Helper.IsPrimitiveType(edmSpaceTypeUsage.EdmType))
			{
				ItemCollection itemCollection = this.GetItemCollection(DataSpace.OSpace, true);
				edmType = itemCollection.GetMappedPrimitiveType(((PrimitiveType)edmSpaceTypeUsage.EdmType).PrimitiveTypeKind);
			}
			else
			{
				ItemCollection itemCollection2 = this.GetItemCollection(DataSpace.OCSpace, true);
				Map map = ((DefaultObjectMappingItemCollection)itemCollection2).GetMap(edmSpaceTypeUsage.EdmType);
				edmType = ((ObjectTypeMapping)map).ClrType;
			}
			return TypeUsage.Create(edmType, edmSpaceTypeUsage.Facets);
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x00077D08 File Offset: 0x00075F08
		internal bool IsItemCollectionAlreadyRegistered(DataSpace dataSpace)
		{
			ItemCollection itemCollection;
			return this.TryGetItemCollection(dataSpace, out itemCollection);
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x00077D20 File Offset: 0x00075F20
		internal bool IsMetadataWorkspaceCSCompatible(MetadataWorkspace other)
		{
			return this._itemsCSSpace.MetadataEquals(other._itemsCSSpace);
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x00077D40 File Offset: 0x00075F40
		public static void ClearCache()
		{
			MetadataCache.Clear();
			ObjectItemCollection.ViewGenerationAssemblies.Clear();
			using (LockedAssemblyCache lockedAssemblyCache = AssemblyCache.AquireLockedAssemblyCache())
			{
				lockedAssemblyCache.Clear();
			}
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x00077D84 File Offset: 0x00075F84
		internal MetadataWorkspace ShallowCopy()
		{
			MetadataWorkspace metadataWorkspace = (MetadataWorkspace)base.MemberwiseClone();
			if (metadataWorkspace._cacheTokens != null)
			{
				metadataWorkspace._cacheTokens = new List<object>(metadataWorkspace._cacheTokens);
			}
			return metadataWorkspace;
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x00077DB7 File Offset: 0x00075FB7
		internal TypeUsage GetCanonicalModelTypeUsage(PrimitiveTypeKind primitiveTypeKind)
		{
			return EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(primitiveTypeKind);
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x00077DC4 File Offset: 0x00075FC4
		internal PrimitiveType GetModelPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			return EdmProviderManifest.Instance.GetPrimitiveType(primitiveTypeKind);
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x00077DD1 File Offset: 0x00075FD1
		[Obsolete("Use MetadataWorkspace.GetRelevantMembersForUpdate(EntitySetBase, EntityTypeBase, bool) instead")]
		public IEnumerable<EdmMember> GetRequiredOriginalValueMembers(EntitySetBase entitySet, EntityTypeBase entityType)
		{
			return this.GetInterestingMembers(entitySet, entityType, StorageMappingItemCollection.InterestingMembersKind.RequiredOriginalValueMembers);
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x00077DDC File Offset: 0x00075FDC
		public ReadOnlyCollection<EdmMember> GetRelevantMembersForUpdate(EntitySetBase entitySet, EntityTypeBase entityType, bool partialUpdateSupported)
		{
			return this.GetInterestingMembers(entitySet, entityType, partialUpdateSupported ? StorageMappingItemCollection.InterestingMembersKind.PartialUpdate : StorageMappingItemCollection.InterestingMembersKind.FullUpdate);
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x00077DF0 File Offset: 0x00075FF0
		private ReadOnlyCollection<EdmMember> GetInterestingMembers(EntitySetBase entitySet, EntityTypeBase entityType, StorageMappingItemCollection.InterestingMembersKind interestingMembersKind)
		{
			EntityUtil.CheckArgumentNull<EntitySetBase>(entitySet, "entitySet");
			EntityUtil.CheckArgumentNull<EntityTypeBase>(entityType, "entityType");
			if (entitySet.EntityContainer.DataSpace != DataSpace.CSpace)
			{
				AssociationSet associationSet = entitySet as AssociationSet;
				if (associationSet != null)
				{
					throw EntityUtil.AssociationSetNotInCSpace(entitySet.Name);
				}
				throw EntityUtil.EntitySetNotInCSpace(entitySet.Name);
			}
			else
			{
				if (entitySet.ElementType.IsAssignableFrom(entityType))
				{
					StorageMappingItemCollection storageMappingItemCollection = (StorageMappingItemCollection)this.GetItemCollection(DataSpace.CSSpace, true);
					return storageMappingItemCollection.GetInterestingMembers(entitySet, entityType, interestingMembersKind);
				}
				AssociationSet associationSet2 = entitySet as AssociationSet;
				if (associationSet2 != null)
				{
					throw EntityUtil.TypeNotInAssociationSet(entitySet.Name, entitySet.ElementType.FullName, entityType.FullName);
				}
				throw EntityUtil.TypeNotInEntitySet(entitySet.Name, entitySet.ElementType.FullName, entityType.FullName);
			}
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x00077EAE File Offset: 0x000760AE
		internal QueryCacheManager GetQueryCacheManager()
		{
			return this._itemsSSpace.QueryCacheManager;
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06002205 RID: 8709 RVA: 0x00077EBB File Offset: 0x000760BB
		internal Guid MetadataWorkspaceId
		{
			get
			{
				if (object.Equals(Guid.Empty, this._metadataWorkspaceId))
				{
					this._metadataWorkspaceId = Guid.NewGuid();
				}
				return this._metadataWorkspaceId;
			}
		}

		// Token: 0x04000ED3 RID: 3795
		private EdmItemCollection _itemsCSpace;

		// Token: 0x04000ED4 RID: 3796
		private StoreItemCollection _itemsSSpace;

		// Token: 0x04000ED5 RID: 3797
		private ObjectItemCollection _itemsOSpace;

		// Token: 0x04000ED6 RID: 3798
		private StorageMappingItemCollection _itemsCSSpace;

		// Token: 0x04000ED7 RID: 3799
		private DefaultObjectMappingItemCollection _itemsOCSpace;

		// Token: 0x04000ED8 RID: 3800
		private List<object> _cacheTokens;

		// Token: 0x04000ED9 RID: 3801
		private bool _foundAssemblyWithAttribute;

		// Token: 0x04000EDA RID: 3802
		private double _schemaVersion;

		// Token: 0x04000EDB RID: 3803
		private Guid _metadataWorkspaceId = Guid.Empty;

		// Token: 0x04000EDC RID: 3804
		public static readonly double MaximumEdmVersionSupported = MetadataWorkspace.SupportedEdmVersions.Last<double>();
	}
}
