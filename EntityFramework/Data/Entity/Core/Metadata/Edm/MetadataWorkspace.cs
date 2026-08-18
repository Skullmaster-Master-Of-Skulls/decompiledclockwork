using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Core.Common.QueryCache;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Mapping.Update.Internal;
using System.Data.Entity.Core.Mapping.ViewGeneration;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200050E RID: 1294
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	public class MetadataWorkspace
	{
		// Token: 0x0600307D RID: 12413 RVA: 0x000E8778 File Offset: 0x000E6978
		public MetadataWorkspace()
		{
			this._itemsOSpace = new Lazy<ObjectItemCollection>(() => new ObjectItemCollection(), true);
			this.MetadataOptimization = new MetadataOptimization(this);
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000E8834 File Offset: 0x000E6A34
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "c")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
		public MetadataWorkspace(Func<EdmItemCollection> cSpaceLoader, Func<StoreItemCollection> sSpaceLoader, Func<StorageMappingItemCollection> csMappingLoader, Func<ObjectItemCollection> oSpaceLoader)
		{
			MetadataWorkspace <>4__this = this;
			Check.NotNull<Func<EdmItemCollection>>(cSpaceLoader, "cSpaceLoader");
			Check.NotNull<Func<StoreItemCollection>>(sSpaceLoader, "sSpaceLoader");
			Check.NotNull<Func<StorageMappingItemCollection>>(csMappingLoader, "csMappingLoader");
			Check.NotNull<Func<ObjectItemCollection>>(oSpaceLoader, "oSpaceLoader");
			this._itemsCSpace = new Lazy<EdmItemCollection>(() => <>4__this.LoadAndCheckItemCollection<EdmItemCollection>(cSpaceLoader), true);
			this._itemsSSpace = new Lazy<StoreItemCollection>(() => <>4__this.LoadAndCheckItemCollection<StoreItemCollection>(sSpaceLoader), true);
			this._itemsOSpace = new Lazy<ObjectItemCollection>(oSpaceLoader, true);
			this._itemsCSSpace = new Lazy<StorageMappingItemCollection>(() => <>4__this.LoadAndCheckItemCollection<StorageMappingItemCollection>(csMappingLoader), true);
			this._itemsOCSpace = new Lazy<DefaultObjectMappingItemCollection>(() => new DefaultObjectMappingItemCollection(this._itemsCSpace.Value, this._itemsOSpace.Value), true);
			this.MetadataOptimization = new MetadataOptimization(this);
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000E89C8 File Offset: 0x000E6BC8
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "c")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
		public MetadataWorkspace(Func<EdmItemCollection> cSpaceLoader, Func<StoreItemCollection> sSpaceLoader, Func<StorageMappingItemCollection> csMappingLoader)
		{
			MetadataWorkspace <>4__this = this;
			Check.NotNull<Func<EdmItemCollection>>(cSpaceLoader, "cSpaceLoader");
			Check.NotNull<Func<StoreItemCollection>>(sSpaceLoader, "sSpaceLoader");
			Check.NotNull<Func<StorageMappingItemCollection>>(csMappingLoader, "csMappingLoader");
			this._itemsCSpace = new Lazy<EdmItemCollection>(() => <>4__this.LoadAndCheckItemCollection<EdmItemCollection>(cSpaceLoader), true);
			this._itemsSSpace = new Lazy<StoreItemCollection>(() => <>4__this.LoadAndCheckItemCollection<StoreItemCollection>(sSpaceLoader), true);
			this._itemsOSpace = new Lazy<ObjectItemCollection>(() => new ObjectItemCollection(), true);
			this._itemsCSSpace = new Lazy<StorageMappingItemCollection>(() => <>4__this.LoadAndCheckItemCollection<StorageMappingItemCollection>(csMappingLoader), true);
			this._itemsOCSpace = new Lazy<DefaultObjectMappingItemCollection>(() => new DefaultObjectMappingItemCollection(this._itemsCSpace.Value, this._itemsOSpace.Value), true);
			this.MetadataOptimization = new MetadataOptimization(this);
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x000E8B88 File Offset: 0x000E6D88
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		public MetadataWorkspace(IEnumerable<string> paths, IEnumerable<Assembly> assembliesToConsider)
		{
			Check.NotNull<IEnumerable<string>>(paths, "paths");
			Check.NotNull<IEnumerable<Assembly>>(assembliesToConsider, "assembliesToConsider");
			EntityUtil.CheckArgumentContainsNull<string>(ref paths, "paths");
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
				throw new ArgumentException(Strings.AssemblyMissingFromAssembliesToConsider(referenceName.FullName), "assembliesToConsider");
			};
			this.CreateMetadataWorkspaceWithResolver(paths, () => assembliesToConsider, resolveReference);
			this.MetadataOptimization = new MetadataOptimization(this);
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000E8C88 File Offset: 0x000E6E88
		private void CreateMetadataWorkspaceWithResolver(IEnumerable<string> paths, Func<IEnumerable<Assembly>> wildcardAssemblies, Func<AssemblyName, Assembly> resolveReference)
		{
			MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromFilePaths(paths.ToArray<string>(), "", new CustomAssemblyResolver(wildcardAssemblies, resolveReference));
			this._itemsOSpace = new Lazy<ObjectItemCollection>(() => new ObjectItemCollection(), true);
			using (DisposableCollectionWrapper<XmlReader> disposableCollectionWrapper = new DisposableCollectionWrapper<XmlReader>(metadataArtifactLoader.CreateReaders(DataSpace.CSpace)))
			{
				if (disposableCollectionWrapper.Any<XmlReader>())
				{
					EdmItemCollection itemCollection = new EdmItemCollection(disposableCollectionWrapper, metadataArtifactLoader.GetPaths(DataSpace.CSpace), false);
					this._itemsCSpace = new Lazy<EdmItemCollection>(() => itemCollection, true);
					this._itemsOCSpace = new Lazy<DefaultObjectMappingItemCollection>(() => new DefaultObjectMappingItemCollection(itemCollection, this._itemsOSpace.Value), true);
				}
			}
			using (DisposableCollectionWrapper<XmlReader> disposableCollectionWrapper2 = new DisposableCollectionWrapper<XmlReader>(metadataArtifactLoader.CreateReaders(DataSpace.SSpace)))
			{
				if (disposableCollectionWrapper2.Any<XmlReader>())
				{
					StoreItemCollection itemCollection = new StoreItemCollection(disposableCollectionWrapper2, metadataArtifactLoader.GetPaths(DataSpace.SSpace));
					this._itemsSSpace = new Lazy<StoreItemCollection>(() => itemCollection, true);
				}
			}
			using (DisposableCollectionWrapper<XmlReader> disposableCollectionWrapper3 = new DisposableCollectionWrapper<XmlReader>(metadataArtifactLoader.CreateReaders(DataSpace.CSSpace)))
			{
				if (disposableCollectionWrapper3.Any<XmlReader>() && this._itemsCSpace != null && this._itemsSSpace != null)
				{
					StorageMappingItemCollection mapping = new StorageMappingItemCollection(this._itemsCSpace.Value, this._itemsSSpace.Value, disposableCollectionWrapper3, metadataArtifactLoader.GetPaths(DataSpace.CSSpace));
					this._itemsCSSpace = new Lazy<StorageMappingItemCollection>(() => mapping, true);
				}
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x000E8F78 File Offset: 0x000E7178
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

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06003083 RID: 12419 RVA: 0x000E8F8E File Offset: 0x000E718E
		public static double MaximumEdmVersionSupported
		{
			get
			{
				return MetadataWorkspace._maximumEdmVersionSupported;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x000E8F95 File Offset: 0x000E7195
		internal virtual Guid MetadataWorkspaceId
		{
			get
			{
				return this._metadataWorkspaceId;
			}
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x000E8F9D File Offset: 0x000E719D
		public virtual EntitySqlParser CreateEntitySqlParser()
		{
			return new EntitySqlParser(new ModelPerspective(this));
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x000E8FAA File Offset: 0x000E71AA
		public virtual DbQueryCommandTree CreateQueryCommandTree(DbExpression query)
		{
			return new DbQueryCommandTree(this, DataSpace.CSpace, query);
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x000E8FB4 File Offset: 0x000E71B4
		public virtual ItemCollection GetItemCollection(DataSpace dataSpace)
		{
			return this.GetItemCollection(dataSpace, true);
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x000E9078 File Offset: 0x000E7278
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		[Obsolete("Construct MetadataWorkspace using constructor that accepts metadata loading delegates.")]
		public virtual void RegisterItemCollection(ItemCollection collection)
		{
			Check.NotNull<ItemCollection>(collection, "collection");
			try
			{
				switch (collection.DataSpace)
				{
				case DataSpace.OSpace:
					this._itemsOSpace = new Lazy<ObjectItemCollection>(() => (ObjectItemCollection)collection, true);
					if (this._itemsOCSpace == null && this._itemsCSpace != null)
					{
						this._itemsOCSpace = new Lazy<DefaultObjectMappingItemCollection>(() => new DefaultObjectMappingItemCollection(this._itemsCSpace.Value, this._itemsOSpace.Value));
						goto IL_227;
					}
					goto IL_227;
				case DataSpace.CSpace:
				{
					EdmItemCollection edmCollection = (EdmItemCollection)collection;
					if (!MetadataWorkspace.SupportedEdmVersions.Contains(edmCollection.EdmVersion))
					{
						throw new InvalidOperationException(Strings.EdmVersionNotSupportedByRuntime(edmCollection.EdmVersion, Helper.GetCommaDelimitedString(from e in MetadataWorkspace.SupportedEdmVersions
						where e != 0.0
						select e.ToString(CultureInfo.InvariantCulture))));
					}
					this.CheckAndSetItemCollectionVersionInWorkSpace(collection);
					this._itemsCSpace = new Lazy<EdmItemCollection>(() => edmCollection, true);
					if (this._itemsOCSpace == null)
					{
						this._itemsOCSpace = new Lazy<DefaultObjectMappingItemCollection>(() => new DefaultObjectMappingItemCollection(edmCollection, this._itemsOSpace.Value));
						goto IL_227;
					}
					goto IL_227;
				}
				case DataSpace.SSpace:
					this.CheckAndSetItemCollectionVersionInWorkSpace(collection);
					this._itemsSSpace = new Lazy<StoreItemCollection>(() => (StoreItemCollection)collection, true);
					goto IL_227;
				case DataSpace.CSSpace:
					this.CheckAndSetItemCollectionVersionInWorkSpace(collection);
					this._itemsCSSpace = new Lazy<StorageMappingItemCollection>(() => (StorageMappingItemCollection)collection, true);
					goto IL_227;
				}
				this._itemsOCSpace = new Lazy<DefaultObjectMappingItemCollection>(() => (DefaultObjectMappingItemCollection)collection, true);
				IL_227:;
			}
			catch (InvalidCastException)
			{
				throw new MetadataException(Strings.InvalidCollectionForMapping(collection.DataSpace.ToString()));
			}
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x000E92EC File Offset: 0x000E74EC
		private T LoadAndCheckItemCollection<T>(Func<T> itemCollectionLoader) where T : ItemCollection
		{
			T t = itemCollectionLoader();
			if (t != null)
			{
				this.CheckAndSetItemCollectionVersionInWorkSpace(t);
			}
			return t;
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000E9318 File Offset: 0x000E7518
		private void CheckAndSetItemCollectionVersionInWorkSpace(ItemCollection itemCollectionToRegister)
		{
			double num = 0.0;
			string p = null;
			switch (itemCollectionToRegister.DataSpace)
			{
			case DataSpace.CSpace:
				num = ((EdmItemCollection)itemCollectionToRegister).EdmVersion;
				p = "EdmItemCollection";
				break;
			case DataSpace.SSpace:
				num = ((StoreItemCollection)itemCollectionToRegister).StoreSchemaVersion;
				p = "StoreItemCollection";
				break;
			case DataSpace.CSSpace:
				num = ((StorageMappingItemCollection)itemCollectionToRegister).MappingVersion;
				p = "StorageMappingItemCollection";
				break;
			}
			lock (this._schemaVersionLock)
			{
				if (num != this._schemaVersion && num != 0.0 && this._schemaVersion != 0.0)
				{
					throw new MetadataException(Strings.DifferentSchemaVersionInCollection(p, num, this._schemaVersion));
				}
				this._schemaVersion = num;
			}
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000E9408 File Offset: 0x000E7608
		public virtual void LoadFromAssembly(Assembly assembly)
		{
			this.LoadFromAssembly(assembly, null);
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x000E9414 File Offset: 0x000E7614
		public virtual void LoadFromAssembly(Assembly assembly, Action<string> logLoadMessage)
		{
			Check.NotNull<Assembly>(assembly, "assembly");
			ObjectItemCollection collection = (ObjectItemCollection)this.GetItemCollection(DataSpace.OSpace);
			this.ExplicitLoadFromAssembly(assembly, collection, logLoadMessage);
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x000E9444 File Offset: 0x000E7644
		private void ExplicitLoadFromAssembly(Assembly assembly, ObjectItemCollection collection, Action<string> logLoadMessage)
		{
			ItemCollection itemCollection;
			if (!this.TryGetItemCollection(DataSpace.CSpace, out itemCollection))
			{
				itemCollection = null;
			}
			collection.ExplicitLoadFromAssembly(assembly, (EdmItemCollection)itemCollection, logLoadMessage);
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000E946C File Offset: 0x000E766C
		private void ImplicitLoadFromAssembly(Assembly assembly, ObjectItemCollection collection)
		{
			if (!MetadataAssemblyHelper.ShouldFilterAssembly(assembly))
			{
				this.ExplicitLoadFromAssembly(assembly, collection, null);
			}
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x000E9480 File Offset: 0x000E7680
		internal virtual void ImplicitLoadAssemblyForType(Type type, Assembly callingAssembly)
		{
			ItemCollection itemCollection;
			if (this.TryGetItemCollection(DataSpace.OSpace, out itemCollection))
			{
				ObjectItemCollection objectItemCollection = (ObjectItemCollection)itemCollection;
				ItemCollection itemCollection2;
				this.TryGetItemCollection(DataSpace.CSpace, out itemCollection2);
				EdmItemCollection edmItemCollection = (EdmItemCollection)itemCollection2;
				if (!objectItemCollection.ImplicitLoadAssemblyForType(type, edmItemCollection) && null != callingAssembly)
				{
					if (ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent(callingAssembly) || this._foundAssemblyWithAttribute || MetadataAssemblyHelper.GetNonSystemReferencedAssemblies(callingAssembly).Any(new Func<Assembly, bool>(ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent)))
					{
						this._foundAssemblyWithAttribute = true;
						objectItemCollection.ImplicitLoadAllReferencedAssemblies(callingAssembly, edmItemCollection);
						return;
					}
					this.ImplicitLoadFromAssembly(callingAssembly, objectItemCollection);
				}
			}
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x000E9504 File Offset: 0x000E7704
		internal virtual void ImplicitLoadFromEntityType(EntityType type, Assembly callingAssembly)
		{
			MappingBase mappingBase;
			if (!this.TryGetMap(type, DataSpace.OCSpace, out mappingBase))
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

		// Token: 0x06003091 RID: 12433 RVA: 0x000E955C File Offset: 0x000E775C
		public virtual T GetItem<T>(string identity, DataSpace dataSpace) where T : GlobalItem
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetItem<T>(identity, false);
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x000E957C File Offset: 0x000E777C
		public virtual bool TryGetItem<T>(string identity, DataSpace space, out T item) where T : GlobalItem
		{
			item = default(T);
			ItemCollection itemCollection = this.GetItemCollection(space, false);
			return itemCollection != null && itemCollection.TryGetItem<T>(identity, false, out item);
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x000E95A8 File Offset: 0x000E77A8
		public virtual T GetItem<T>(string identity, bool ignoreCase, DataSpace dataSpace) where T : GlobalItem
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetItem<T>(identity, ignoreCase);
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000E95C8 File Offset: 0x000E77C8
		public virtual bool TryGetItem<T>(string identity, bool ignoreCase, DataSpace dataSpace, out T item) where T : GlobalItem
		{
			item = default(T);
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetItem<T>(identity, ignoreCase, out item);
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000E95F8 File Offset: 0x000E77F8
		public virtual ReadOnlyCollection<T> GetItems<T>(DataSpace dataSpace) where T : GlobalItem
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetItems<T>();
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x000E9614 File Offset: 0x000E7814
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "GetType")]
		public virtual EdmType GetType(string name, string namespaceName, DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetType(name, namespaceName, false);
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x000E9634 File Offset: 0x000E7834
		public virtual bool TryGetType(string name, string namespaceName, DataSpace dataSpace, out EdmType type)
		{
			type = null;
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetType(name, namespaceName, false, out type);
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000E9660 File Offset: 0x000E7860
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "GetType")]
		public virtual EdmType GetType(string name, string namespaceName, bool ignoreCase, DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetType(name, namespaceName, ignoreCase);
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x000E9680 File Offset: 0x000E7880
		public virtual bool TryGetType(string name, string namespaceName, bool ignoreCase, DataSpace dataSpace, out EdmType type)
		{
			type = null;
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetType(name, namespaceName, ignoreCase, out type);
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000E96AC File Offset: 0x000E78AC
		public virtual EntityContainer GetEntityContainer(string name, DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetEntityContainer(name);
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x000E96CC File Offset: 0x000E78CC
		public virtual bool TryGetEntityContainer(string name, DataSpace dataSpace, out EntityContainer entityContainer)
		{
			entityContainer = null;
			Check.NotNull<string>(name, "name");
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetEntityContainer(name, out entityContainer);
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x000E9700 File Offset: 0x000E7900
		public virtual EntityContainer GetEntityContainer(string name, bool ignoreCase, DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetEntityContainer(name, ignoreCase);
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x000E9720 File Offset: 0x000E7920
		public virtual bool TryGetEntityContainer(string name, bool ignoreCase, DataSpace dataSpace, out EntityContainer entityContainer)
		{
			entityContainer = null;
			Check.NotNull<string>(name, "name");
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetEntityContainer(name, ignoreCase, out entityContainer);
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x000E9755 File Offset: 0x000E7955
		public virtual ReadOnlyCollection<EdmFunction> GetFunctions(string name, string namespaceName, DataSpace dataSpace)
		{
			return this.GetFunctions(name, namespaceName, dataSpace, false);
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x000E9764 File Offset: 0x000E7964
		public virtual ReadOnlyCollection<EdmFunction> GetFunctions(string name, string namespaceName, DataSpace dataSpace, bool ignoreCase)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(namespaceName, "namespaceName");
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetFunctions(namespaceName + "." + name, ignoreCase);
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x000E97A8 File Offset: 0x000E79A8
		internal virtual bool TryGetFunction(string name, string namespaceName, TypeUsage[] parameterTypes, bool ignoreCase, DataSpace dataSpace, out EdmFunction function)
		{
			function = null;
			Check.NotNull<string>(name, "name");
			Check.NotNull<string>(namespaceName, "namespaceName");
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetFunction(namespaceName + "." + name, parameterTypes, ignoreCase, out function);
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x000E97F8 File Offset: 0x000E79F8
		public virtual ReadOnlyCollection<PrimitiveType> GetPrimitiveTypes(DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetItems<PrimitiveType>();
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000E9814 File Offset: 0x000E7A14
		public virtual ReadOnlyCollection<GlobalItem> GetItems(DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetItems<GlobalItem>();
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000E9830 File Offset: 0x000E7A30
		internal virtual PrimitiveType GetMappedPrimitiveType(PrimitiveTypeKind primitiveTypeKind, DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return itemCollection.GetMappedPrimitiveType(primitiveTypeKind);
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000E9850 File Offset: 0x000E7A50
		internal virtual bool TryGetMap(string typeIdentity, DataSpace typeSpace, bool ignoreCase, DataSpace mappingSpace, out MappingBase map)
		{
			map = null;
			ItemCollection itemCollection = this.GetItemCollection(mappingSpace, false);
			return itemCollection != null && ((MappingItemCollection)itemCollection).TryGetMap(typeIdentity, typeSpace, ignoreCase, out map);
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000E9880 File Offset: 0x000E7A80
		internal virtual MappingBase GetMap(string identity, DataSpace typeSpace, DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return ((MappingItemCollection)itemCollection).GetMap(identity, typeSpace);
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x000E98A4 File Offset: 0x000E7AA4
		internal virtual MappingBase GetMap(GlobalItem item, DataSpace dataSpace)
		{
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, true);
			return ((MappingItemCollection)itemCollection).GetMap(item);
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x000E98C8 File Offset: 0x000E7AC8
		internal virtual bool TryGetMap(GlobalItem item, DataSpace dataSpace, out MappingBase map)
		{
			map = null;
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && ((MappingItemCollection)itemCollection).TryGetMap(item, out map);
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000E98F3 File Offset: 0x000E7AF3
		public virtual bool TryGetItemCollection(DataSpace dataSpace, out ItemCollection collection)
		{
			collection = this.GetItemCollection(dataSpace, false);
			return null != collection;
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000E9908 File Offset: 0x000E7B08
		internal virtual ItemCollection GetItemCollection(DataSpace dataSpace, bool required)
		{
			ItemCollection itemCollection;
			switch (dataSpace)
			{
			case DataSpace.OSpace:
				itemCollection = this._itemsOSpace.Value;
				break;
			case DataSpace.CSpace:
				itemCollection = ((this._itemsCSpace == null) ? null : this._itemsCSpace.Value);
				break;
			case DataSpace.SSpace:
				itemCollection = ((this._itemsSSpace == null) ? null : this._itemsSSpace.Value);
				break;
			case DataSpace.OCSpace:
				itemCollection = ((this._itemsOCSpace == null) ? null : this._itemsOCSpace.Value);
				break;
			case DataSpace.CSSpace:
				itemCollection = ((this._itemsCSSpace == null) ? null : this._itemsCSSpace.Value);
				break;
			default:
				itemCollection = null;
				break;
			}
			if (required && itemCollection == null)
			{
				throw new InvalidOperationException(Strings.NoCollectionForSpace(dataSpace.ToString()));
			}
			return itemCollection;
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000E99C6 File Offset: 0x000E7BC6
		public virtual StructuralType GetObjectSpaceType(StructuralType edmSpaceType)
		{
			return this.GetObjectSpaceType<StructuralType>(edmSpaceType);
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000E99CF File Offset: 0x000E7BCF
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public virtual bool TryGetObjectSpaceType(StructuralType edmSpaceType, out StructuralType objectSpaceType)
		{
			return this.TryGetObjectSpaceType<StructuralType>(edmSpaceType, out objectSpaceType);
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000E99D9 File Offset: 0x000E7BD9
		public virtual EnumType GetObjectSpaceType(EnumType edmSpaceType)
		{
			return this.GetObjectSpaceType<EnumType>(edmSpaceType);
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000E99E2 File Offset: 0x000E7BE2
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public virtual bool TryGetObjectSpaceType(EnumType edmSpaceType, out EnumType objectSpaceType)
		{
			return this.TryGetObjectSpaceType<EnumType>(edmSpaceType, out objectSpaceType);
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x000E99EC File Offset: 0x000E7BEC
		private T GetObjectSpaceType<T>(T edmSpaceType) where T : EdmType
		{
			T result;
			if (!this.TryGetObjectSpaceType<T>(edmSpaceType, out result))
			{
				throw new ArgumentException(Strings.FailedToFindOSpaceTypeMapping(edmSpaceType.Identity));
			}
			return result;
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000E9A20 File Offset: 0x000E7C20
		private bool TryGetObjectSpaceType<T>(T edmSpaceType, out T objectSpaceType) where T : EdmType
		{
			if (edmSpaceType.DataSpace != DataSpace.CSpace)
			{
				throw new ArgumentException(Strings.ArgumentMustBeCSpaceType, "edmSpaceType");
			}
			objectSpaceType = default(T);
			MappingBase mappingBase;
			if (this.TryGetMap(edmSpaceType, DataSpace.OCSpace, out mappingBase))
			{
				ObjectTypeMapping objectTypeMapping = mappingBase as ObjectTypeMapping;
				if (objectTypeMapping != null)
				{
					objectSpaceType = (T)((object)objectTypeMapping.ClrType);
				}
			}
			return objectSpaceType != null;
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000E9A91 File Offset: 0x000E7C91
		public virtual StructuralType GetEdmSpaceType(StructuralType objectSpaceType)
		{
			return this.GetEdmSpaceType<StructuralType>(objectSpaceType);
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000E9A9A File Offset: 0x000E7C9A
		public virtual bool TryGetEdmSpaceType(StructuralType objectSpaceType, out StructuralType edmSpaceType)
		{
			return this.TryGetEdmSpaceType<StructuralType>(objectSpaceType, out edmSpaceType);
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x000E9AA4 File Offset: 0x000E7CA4
		public virtual EnumType GetEdmSpaceType(EnumType objectSpaceType)
		{
			return this.GetEdmSpaceType<EnumType>(objectSpaceType);
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000E9AAD File Offset: 0x000E7CAD
		public virtual bool TryGetEdmSpaceType(EnumType objectSpaceType, out EnumType edmSpaceType)
		{
			return this.TryGetEdmSpaceType<EnumType>(objectSpaceType, out edmSpaceType);
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000E9AB8 File Offset: 0x000E7CB8
		private T GetEdmSpaceType<T>(T objectSpaceType) where T : EdmType
		{
			T result;
			if (!this.TryGetEdmSpaceType<T>(objectSpaceType, out result))
			{
				throw new ArgumentException(Strings.FailedToFindCSpaceTypeMapping(objectSpaceType.Identity));
			}
			return result;
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000E9AEC File Offset: 0x000E7CEC
		private bool TryGetEdmSpaceType<T>(T objectSpaceType, out T edmSpaceType) where T : EdmType
		{
			if (objectSpaceType.DataSpace != DataSpace.OSpace)
			{
				throw new ArgumentException(Strings.ArgumentMustBeOSpaceType, "objectSpaceType");
			}
			edmSpaceType = default(T);
			MappingBase mappingBase;
			if (this.TryGetMap(objectSpaceType, DataSpace.OCSpace, out mappingBase))
			{
				ObjectTypeMapping objectTypeMapping = mappingBase as ObjectTypeMapping;
				if (objectTypeMapping != null)
				{
					edmSpaceType = (T)((object)objectTypeMapping.EdmType);
				}
			}
			return edmSpaceType != null;
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000E9B5C File Offset: 0x000E7D5C
		internal virtual DbQueryCommandTree GetCqtView(EntitySetBase extent)
		{
			return this.GetGeneratedView(extent).GetCommandTree();
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x000E9B6C File Offset: 0x000E7D6C
		internal virtual GeneratedView GetGeneratedView(EntitySetBase extent)
		{
			ItemCollection itemCollection = this.GetItemCollection(DataSpace.CSSpace, true);
			return ((StorageMappingItemCollection)itemCollection).GetGeneratedView(extent, this);
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000E9B90 File Offset: 0x000E7D90
		internal virtual bool TryGetGeneratedViewOfType(EntitySetBase extent, EntityTypeBase type, bool includeSubtypes, out GeneratedView generatedView)
		{
			ItemCollection itemCollection = this.GetItemCollection(DataSpace.CSSpace, true);
			return ((StorageMappingItemCollection)itemCollection).TryGetGeneratedViewOfType(extent, type, includeSubtypes, out generatedView);
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x000E9BB8 File Offset: 0x000E7DB8
		internal virtual DbLambda GetGeneratedFunctionDefinition(EdmFunction function)
		{
			ItemCollection itemCollection = this.GetItemCollection(DataSpace.CSpace, true);
			return ((EdmItemCollection)itemCollection).GetGeneratedFunctionDefinition(function);
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000E9BDC File Offset: 0x000E7DDC
		internal virtual bool TryGetFunctionImportMapping(EdmFunction functionImport, out FunctionImportMapping targetFunctionMapping)
		{
			ReadOnlyCollection<EntityContainerMapping> items = this.GetItems<EntityContainerMapping>(DataSpace.CSSpace);
			foreach (EntityContainerMapping entityContainerMapping in items)
			{
				if (entityContainerMapping.TryGetFunctionImportMapping(functionImport, out targetFunctionMapping))
				{
					return true;
				}
			}
			targetFunctionMapping = null;
			return false;
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000E9C3C File Offset: 0x000E7E3C
		internal virtual ViewLoader GetUpdateViewLoader()
		{
			if (this._itemsCSSpace == null || this._itemsCSSpace.Value == null)
			{
				return null;
			}
			return this._itemsCSSpace.Value.GetUpdateViewLoader();
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000E9C68 File Offset: 0x000E7E68
		internal virtual TypeUsage GetOSpaceTypeUsage(TypeUsage edmSpaceTypeUsage)
		{
			EdmType edmType;
			if (Helper.IsPrimitiveType(edmSpaceTypeUsage.EdmType))
			{
				ItemCollection itemCollection = this.GetItemCollection(DataSpace.OSpace, true);
				edmType = itemCollection.GetMappedPrimitiveType(((PrimitiveType)edmSpaceTypeUsage.EdmType).PrimitiveTypeKind);
			}
			else
			{
				ItemCollection itemCollection2 = this.GetItemCollection(DataSpace.OCSpace, true);
				MappingBase map = ((DefaultObjectMappingItemCollection)itemCollection2).GetMap(edmSpaceTypeUsage.EdmType);
				edmType = ((ObjectTypeMapping)map).ClrType;
			}
			return TypeUsage.Create(edmType, edmSpaceTypeUsage.Facets);
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000E9CE0 File Offset: 0x000E7EE0
		internal virtual bool IsItemCollectionAlreadyRegistered(DataSpace dataSpace)
		{
			ItemCollection itemCollection;
			return this.TryGetItemCollection(dataSpace, out itemCollection);
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000E9CF8 File Offset: 0x000E7EF8
		internal virtual bool IsMetadataWorkspaceCSCompatible(MetadataWorkspace other)
		{
			return this.GetItemCollection(DataSpace.CSSpace, false).MetadataEquals(other.GetItemCollection(DataSpace.CSSpace, false));
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x000E9D1C File Offset: 0x000E7F1C
		public static void ClearCache()
		{
			MetadataCache.Instance.Clear();
			using (LockedAssemblyCache lockedAssemblyCache = AssemblyCache.AquireLockedAssemblyCache())
			{
				lockedAssemblyCache.Clear();
			}
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000E9D5C File Offset: 0x000E7F5C
		internal static TypeUsage GetCanonicalModelTypeUsage(PrimitiveTypeKind primitiveTypeKind)
		{
			return EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(primitiveTypeKind);
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x000E9D69 File Offset: 0x000E7F69
		internal static PrimitiveType GetModelPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			return EdmProviderManifest.Instance.GetPrimitiveType(primitiveTypeKind);
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000E9D76 File Offset: 0x000E7F76
		[Obsolete("Use MetadataWorkspace.GetRelevantMembersForUpdate(EntitySetBase, EntityTypeBase, bool) instead")]
		public virtual IEnumerable<EdmMember> GetRequiredOriginalValueMembers(EntitySetBase entitySet, EntityTypeBase entityType)
		{
			return this.GetInterestingMembers(entitySet, entityType, StorageMappingItemCollection.InterestingMembersKind.RequiredOriginalValueMembers);
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000E9D81 File Offset: 0x000E7F81
		public virtual ReadOnlyCollection<EdmMember> GetRelevantMembersForUpdate(EntitySetBase entitySet, EntityTypeBase entityType, bool partialUpdateSupported)
		{
			return this.GetInterestingMembers(entitySet, entityType, partialUpdateSupported ? StorageMappingItemCollection.InterestingMembersKind.PartialUpdate : StorageMappingItemCollection.InterestingMembersKind.FullUpdate);
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x000E9D94 File Offset: 0x000E7F94
		private ReadOnlyCollection<EdmMember> GetInterestingMembers(EntitySetBase entitySet, EntityTypeBase entityType, StorageMappingItemCollection.InterestingMembersKind interestingMembersKind)
		{
			AssociationSet associationSet = entitySet as AssociationSet;
			if (entitySet.EntityContainer.DataSpace != DataSpace.CSpace)
			{
				if (associationSet != null)
				{
					throw new ArgumentException(Strings.EntitySetNotInCSPace(entitySet.Name));
				}
				throw new ArgumentException(Strings.EntitySetNotInCSPace(entitySet.Name));
			}
			else
			{
				if (entitySet.ElementType.IsAssignableFrom(entityType))
				{
					StorageMappingItemCollection storageMappingItemCollection = (StorageMappingItemCollection)this.GetItemCollection(DataSpace.CSSpace, true);
					return storageMappingItemCollection.GetInterestingMembers(entitySet, entityType, interestingMembersKind);
				}
				if (associationSet != null)
				{
					throw new ArgumentException(Strings.TypeNotInAssociationSet(entityType.FullName, entitySet.ElementType.FullName, entitySet.Name));
				}
				throw new ArgumentException(Strings.TypeNotInEntitySet(entityType.FullName, entitySet.ElementType.FullName, entitySet.Name));
			}
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000E9E47 File Offset: 0x000E8047
		internal virtual QueryCacheManager GetQueryCacheManager()
		{
			return this._itemsSSpace.Value.QueryCacheManager;
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000E9E59 File Offset: 0x000E8059
		internal bool TryDetermineCSpaceModelType<T>(out EdmType modelEdmType)
		{
			return this.TryDetermineCSpaceModelType(typeof(T), out modelEdmType);
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000E9E6C File Offset: 0x000E806C
		internal virtual bool TryDetermineCSpaceModelType(Type type, out EdmType modelEdmType)
		{
			Type nonNullableType = TypeSystem.GetNonNullableType(type);
			this.ImplicitLoadAssemblyForType(nonNullableType, Assembly.GetCallingAssembly());
			ObjectItemCollection objectItemCollection = (ObjectItemCollection)this.GetItemCollection(DataSpace.OSpace);
			EdmType item;
			MappingBase mappingBase;
			if (objectItemCollection.TryGetItem<EdmType>(nonNullableType.FullNameWithNesting(), out item) && this.TryGetMap(item, DataSpace.OCSpace, out mappingBase))
			{
				ObjectTypeMapping objectTypeMapping = (ObjectTypeMapping)mappingBase;
				modelEdmType = objectTypeMapping.EdmType;
				return true;
			}
			modelEdmType = null;
			return false;
		}

		// Token: 0x0400126A RID: 4714
		private Lazy<EdmItemCollection> _itemsCSpace;

		// Token: 0x0400126B RID: 4715
		private Lazy<StoreItemCollection> _itemsSSpace;

		// Token: 0x0400126C RID: 4716
		private Lazy<ObjectItemCollection> _itemsOSpace;

		// Token: 0x0400126D RID: 4717
		private Lazy<StorageMappingItemCollection> _itemsCSSpace;

		// Token: 0x0400126E RID: 4718
		private Lazy<DefaultObjectMappingItemCollection> _itemsOCSpace;

		// Token: 0x0400126F RID: 4719
		private bool _foundAssemblyWithAttribute;

		// Token: 0x04001270 RID: 4720
		private double _schemaVersion;

		// Token: 0x04001271 RID: 4721
		private readonly object _schemaVersionLock = new object();

		// Token: 0x04001272 RID: 4722
		private readonly Guid _metadataWorkspaceId = Guid.NewGuid();

		// Token: 0x04001273 RID: 4723
		internal readonly MetadataOptimization MetadataOptimization;

		// Token: 0x04001274 RID: 4724
		private static readonly double _maximumEdmVersionSupported = MetadataWorkspace.SupportedEdmVersions.Last<double>();
	}
}
