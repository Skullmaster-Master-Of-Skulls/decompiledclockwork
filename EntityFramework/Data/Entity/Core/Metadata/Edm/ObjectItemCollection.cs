using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000510 RID: 1296
	public class ObjectItemCollection : ItemCollection
	{
		// Token: 0x060030D3 RID: 12499 RVA: 0x000E9F49 File Offset: 0x000E8149
		public ObjectItemCollection() : this(null)
		{
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x000E9F54 File Offset: 0x000E8154
		internal ObjectItemCollection(KnownAssembliesSet knownAssembliesSet = null) : base(DataSpace.OSpace)
		{
			this._knownAssemblies = (knownAssembliesSet ?? new KnownAssembliesSet());
			foreach (PrimitiveType primitiveType in ClrProviderManifest.Instance.GetStoreTypes())
			{
				base.AddInternal(primitiveType);
				this._primitiveTypeMaps.Add(primitiveType);
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060030D5 RID: 12501 RVA: 0x000E9FF4 File Offset: 0x000E81F4
		// (set) Token: 0x060030D6 RID: 12502 RVA: 0x000E9FFC File Offset: 0x000E81FC
		internal bool OSpaceTypesLoaded { get; set; }

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060030D7 RID: 12503 RVA: 0x000EA005 File Offset: 0x000E8205
		internal object LoadAssemblyLock
		{
			get
			{
				return this._loadAssemblyLock;
			}
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000EA00D File Offset: 0x000E820D
		internal void ImplicitLoadAllReferencedAssemblies(Assembly assembly, EdmItemCollection edmItemCollection)
		{
			if (!MetadataAssemblyHelper.ShouldFilterAssembly(assembly))
			{
				this.LoadAssemblyFromCache(assembly, true, edmItemCollection, null);
			}
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000EA022 File Offset: 0x000E8222
		public void LoadFromAssembly(Assembly assembly)
		{
			this.ExplicitLoadFromAssembly(assembly, null, null);
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000EA02D File Offset: 0x000E822D
		public void LoadFromAssembly(Assembly assembly, EdmItemCollection edmItemCollection, Action<string> logLoadMessage)
		{
			Check.NotNull<Assembly>(assembly, "assembly");
			Check.NotNull<EdmItemCollection>(edmItemCollection, "edmItemCollection");
			Check.NotNull<Action<string>>(logLoadMessage, "logLoadMessage");
			this.ExplicitLoadFromAssembly(assembly, edmItemCollection, logLoadMessage);
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000EA05C File Offset: 0x000E825C
		public void LoadFromAssembly(Assembly assembly, EdmItemCollection edmItemCollection)
		{
			Check.NotNull<Assembly>(assembly, "assembly");
			Check.NotNull<EdmItemCollection>(edmItemCollection, "edmItemCollection");
			this.ExplicitLoadFromAssembly(assembly, edmItemCollection, null);
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000EA07F File Offset: 0x000E827F
		internal void ExplicitLoadFromAssembly(Assembly assembly, EdmItemCollection edmItemCollection, Action<string> logLoadMessage)
		{
			this.LoadAssemblyFromCache(assembly, false, edmItemCollection, logLoadMessage);
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000EA08C File Offset: 0x000E828C
		internal bool ImplicitLoadAssemblyForType(Type type, EdmItemCollection edmItemCollection)
		{
			bool flag = false;
			if (!MetadataAssemblyHelper.ShouldFilterAssembly(type.Assembly()))
			{
				flag = this.LoadAssemblyFromCache(type.Assembly(), false, edmItemCollection, null);
			}
			if (type.IsGenericType())
			{
				foreach (Type type2 in type.GetGenericArguments())
				{
					flag |= this.ImplicitLoadAssemblyForType(type2, edmItemCollection);
				}
			}
			return flag;
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000EA0E8 File Offset: 0x000E82E8
		internal AssociationType GetRelationshipType(string relationshipName)
		{
			AssociationType result;
			if (base.TryGetItem<AssociationType>(relationshipName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000EA160 File Offset: 0x000E8360
		private bool LoadAssemblyFromCache(Assembly assembly, bool loadReferencedAssemblies, EdmItemCollection edmItemCollection, Action<string> logLoadMessage)
		{
			if (this.OSpaceTypesLoaded)
			{
				return true;
			}
			if (edmItemCollection != null)
			{
				ReadOnlyCollection<EntityContainer> items = edmItemCollection.GetItems<EntityContainer>();
				if (items.Any<EntityContainer>())
				{
					if (items.All((EntityContainer c) => c.Annotations.Any((MetadataProperty a) => a.Name == "http://schemas.microsoft.com/ado/2013/11/edm/customannotation:UseClrTypes" && ((string)a.Value).ToUpperInvariant() == "TRUE")))
					{
						lock (this.LoadAssemblyLock)
						{
							if (!this.OSpaceTypesLoaded)
							{
								new CodeFirstOSpaceLoader(null).LoadTypes(edmItemCollection, this);
							}
							return true;
						}
					}
				}
			}
			KnownAssemblyEntry knownAssemblyEntry;
			if (this._knownAssemblies.TryGetKnownAssembly(assembly, this._loaderCookie, edmItemCollection, out knownAssemblyEntry))
			{
				if (!loadReferencedAssemblies)
				{
					return knownAssemblyEntry.CacheEntry.TypesInAssembly.Count != 0;
				}
				if (knownAssemblyEntry.ReferencedAssembliesAreLoaded)
				{
					return true;
				}
			}
			bool result;
			lock (this.LoadAssemblyLock)
			{
				if (this._knownAssemblies.TryGetKnownAssembly(assembly, this._loaderCookie, edmItemCollection, out knownAssemblyEntry) && (!loadReferencedAssemblies || knownAssemblyEntry.ReferencedAssembliesAreLoaded))
				{
					result = true;
				}
				else
				{
					KnownAssembliesSet knownAssemblies = new KnownAssembliesSet(this._knownAssemblies);
					Dictionary<string, EdmType> dictionary;
					List<EdmItemError> list;
					AssemblyCache.LoadAssembly(assembly, loadReferencedAssemblies, knownAssemblies, edmItemCollection, logLoadMessage, ref this._loaderCookie, out dictionary, out list);
					if (list.Count != 0)
					{
						throw EntityUtil.InvalidSchemaEncountered(Helper.CombineErrorMessage(list));
					}
					if (dictionary.Count != 0)
					{
						this.AddLoadedTypes(dictionary);
					}
					this._knownAssemblies = knownAssemblies;
					result = (dictionary.Count != 0);
				}
			}
			return result;
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000EA2E8 File Offset: 0x000E84E8
		internal virtual void AddLoadedTypes(Dictionary<string, EdmType> typesInLoading)
		{
			List<GlobalItem> list = new List<GlobalItem>();
			foreach (EdmType edmType in typesInLoading.Values)
			{
				list.Add(edmType);
				string text = "";
				try
				{
					if (Helper.IsEntityType(edmType))
					{
						text = ((ClrEntityType)edmType).CSpaceTypeName;
						this._ocMapping.Add(text, edmType);
					}
					else if (Helper.IsComplexType(edmType))
					{
						text = ((ClrComplexType)edmType).CSpaceTypeName;
						this._ocMapping.Add(text, edmType);
					}
					else if (Helper.IsEnumType(edmType))
					{
						text = ((ClrEnumType)edmType).CSpaceTypeName;
						this._ocMapping.Add(text, edmType);
					}
				}
				catch (ArgumentException innerException)
				{
					throw new MappingException(Strings.Mapping_CannotMapCLRTypeMultipleTimes(text), innerException);
				}
			}
			base.AddRange(list);
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000EA3D8 File Offset: 0x000E85D8
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public IEnumerable<PrimitiveType> GetPrimitiveTypes()
		{
			return this._primitiveTypeMaps.GetTypes();
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000EA3E5 File Offset: 0x000E85E5
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public Type GetClrType(StructuralType objectSpaceType)
		{
			return ObjectItemCollection.GetClrType(objectSpaceType);
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000EA3ED File Offset: 0x000E85ED
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public bool TryGetClrType(StructuralType objectSpaceType, out Type clrType)
		{
			return ObjectItemCollection.TryGetClrType(objectSpaceType, out clrType);
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000EA3F6 File Offset: 0x000E85F6
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public Type GetClrType(EnumType objectSpaceType)
		{
			return ObjectItemCollection.GetClrType(objectSpaceType);
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x000EA3FE File Offset: 0x000E85FE
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public bool TryGetClrType(EnumType objectSpaceType, out Type clrType)
		{
			return ObjectItemCollection.TryGetClrType(objectSpaceType, out clrType);
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000EA408 File Offset: 0x000E8608
		private static Type GetClrType(EdmType objectSpaceType)
		{
			Type result;
			if (!ObjectItemCollection.TryGetClrType(objectSpaceType, out result))
			{
				throw new ArgumentException(Strings.FailedToFindClrTypeMapping(objectSpaceType.Identity));
			}
			return result;
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x000EA434 File Offset: 0x000E8634
		private static bool TryGetClrType(EdmType objectSpaceType, out Type clrType)
		{
			if (objectSpaceType.DataSpace != DataSpace.OSpace)
			{
				throw new ArgumentException(Strings.ArgumentMustBeOSpaceType, "objectSpaceType");
			}
			clrType = null;
			if (Helper.IsEntityType(objectSpaceType) || Helper.IsComplexType(objectSpaceType) || Helper.IsEnumType(objectSpaceType))
			{
				clrType = objectSpaceType.ClrType;
			}
			return clrType != null;
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000EA484 File Offset: 0x000E8684
		internal override PrimitiveType GetMappedPrimitiveType(PrimitiveTypeKind modelType)
		{
			if (Helper.IsGeometricTypeKind(modelType))
			{
				modelType = PrimitiveTypeKind.Geometry;
			}
			else if (Helper.IsGeographicTypeKind(modelType))
			{
				modelType = PrimitiveTypeKind.Geography;
			}
			PrimitiveType result = null;
			this._primitiveTypeMaps.TryGetType(modelType, null, out result);
			return result;
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000EA4BE File Offset: 0x000E86BE
		internal bool TryGetOSpaceType(EdmType cspaceType, out EdmType edmType)
		{
			if (Helper.IsEntityType(cspaceType) || Helper.IsComplexType(cspaceType) || Helper.IsEnumType(cspaceType))
			{
				return this._ocMapping.TryGetValue(cspaceType.Identity, out edmType);
			}
			return base.TryGetItem<EdmType>(cspaceType.Identity, out edmType);
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000EA4F8 File Offset: 0x000E86F8
		internal static string TryGetMappingCSpaceTypeIdentity(EdmType edmType)
		{
			if (Helper.IsEntityType(edmType))
			{
				return ((ClrEntityType)edmType).CSpaceTypeName;
			}
			if (Helper.IsComplexType(edmType))
			{
				return ((ClrComplexType)edmType).CSpaceTypeName;
			}
			if (Helper.IsEnumType(edmType))
			{
				return ((ClrEnumType)edmType).CSpaceTypeName;
			}
			return edmType.Identity;
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000EA547 File Offset: 0x000E8747
		public override ReadOnlyCollection<T> GetItems<T>()
		{
			return base.InternalGetItems(typeof(T)) as ReadOnlyCollection<T>;
		}

		// Token: 0x0400127A RID: 4730
		private readonly CacheForPrimitiveTypes _primitiveTypeMaps = new CacheForPrimitiveTypes();

		// Token: 0x0400127B RID: 4731
		private KnownAssembliesSet _knownAssemblies = new KnownAssembliesSet();

		// Token: 0x0400127C RID: 4732
		private readonly Dictionary<string, EdmType> _ocMapping = new Dictionary<string, EdmType>();

		// Token: 0x0400127D RID: 4733
		private object _loaderCookie;

		// Token: 0x0400127E RID: 4734
		private readonly object _loadAssemblyLock = new object();
	}
}
