using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Mapping;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000205 RID: 517
	[CLSCompliant(false)]
	public sealed class ObjectItemCollection : ItemCollection
	{
		// Token: 0x06002247 RID: 8775 RVA: 0x0007891C File Offset: 0x00076B1C
		public ObjectItemCollection() : base(DataSpace.OSpace)
		{
			foreach (PrimitiveType primitiveType in ClrProviderManifest.Instance.GetStoreTypes())
			{
				base.AddInternal(primitiveType);
				this._primitiveTypeMaps.Add(primitiveType);
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x000789AC File Offset: 0x00076BAC
		internal object LoadAssemblyLock
		{
			get
			{
				return this._loadAssemblyLock;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x000789B4 File Offset: 0x00076BB4
		internal static IList<Assembly> ViewGenerationAssemblies
		{
			get
			{
				return AssemblyCache.ViewGenerationAssemblies;
			}
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x000789BB File Offset: 0x00076BBB
		internal static bool IsCompiledViewGenAttributePresent(Assembly assembly)
		{
			return assembly.IsDefined(typeof(EntityViewGenerationAttribute), false);
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x000789D0 File Offset: 0x00076BD0
		internal void ImplicitLoadAllReferencedAssemblies(Assembly assembly, EdmItemCollection edmItemCollection)
		{
			if (!MetadataAssemblyHelper.ShouldFilterAssembly(assembly))
			{
				bool loadReferencedAssemblies = true;
				ObjectItemCollection.LoadAssemblyFromCache(this, assembly, loadReferencedAssemblies, edmItemCollection, null);
			}
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x000789F4 File Offset: 0x00076BF4
		internal void ImplicitLoadViewsFromAllReferencedAssemblies(Assembly assembly)
		{
			if (MetadataAssemblyHelper.ShouldFilterAssembly(assembly))
			{
				return;
			}
			lock (this)
			{
				ObjectItemCollection.CollectIfViewGenAssembly(assembly);
				foreach (Assembly assembly2 in MetadataAssemblyHelper.GetNonSystemReferencedAssemblies(assembly))
				{
					ObjectItemCollection.CollectIfViewGenAssembly(assembly2);
				}
			}
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x00078A74 File Offset: 0x00076C74
		public void LoadFromAssembly(Assembly assembly)
		{
			this.ExplicitLoadFromAssembly(assembly, null, null);
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x00078A7F File Offset: 0x00076C7F
		public void LoadFromAssembly(Assembly assembly, EdmItemCollection edmItemCollection, Action<string> logLoadMessage)
		{
			EntityUtil.CheckArgumentNull<Assembly>(assembly, "assembly");
			EntityUtil.CheckArgumentNull<EdmItemCollection>(edmItemCollection, "edmItemCollection");
			EntityUtil.CheckArgumentNull<Action<string>>(logLoadMessage, "logLoadMessage");
			this.ExplicitLoadFromAssembly(assembly, edmItemCollection, logLoadMessage);
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x00078AAE File Offset: 0x00076CAE
		public void LoadFromAssembly(Assembly assembly, EdmItemCollection edmItemCollection)
		{
			EntityUtil.CheckArgumentNull<Assembly>(assembly, "assembly");
			EntityUtil.CheckArgumentNull<EdmItemCollection>(edmItemCollection, "edmItemCollection");
			this.ExplicitLoadFromAssembly(assembly, edmItemCollection, null);
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x00078AD1 File Offset: 0x00076CD1
		internal void ExplicitLoadFromAssembly(Assembly assembly, EdmItemCollection edmItemCollection, Action<string> logLoadMessage)
		{
			ObjectItemCollection.LoadAssemblyFromCache(this, assembly, false, edmItemCollection, logLoadMessage);
			if (ObjectItemCollection.IsCompiledViewGenAttributePresent(assembly) && !ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent(assembly))
			{
				ObjectItemCollection.CollectIfViewGenAssembly(assembly);
			}
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x00078AF4 File Offset: 0x00076CF4
		internal void ImplicitLoadFromAssembly(Assembly assembly, EdmItemCollection edmItemCollection)
		{
			if (!MetadataAssemblyHelper.ShouldFilterAssembly(assembly))
			{
				this.ExplicitLoadFromAssembly(assembly, edmItemCollection, null);
			}
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x00078B08 File Offset: 0x00076D08
		internal bool ImplicitLoadAssemblyForType(Type type, EdmItemCollection edmItemCollection)
		{
			bool flag = !MetadataAssemblyHelper.ShouldFilterAssembly(type.Assembly) && ObjectItemCollection.LoadAssemblyFromCache(this, type.Assembly, false, edmItemCollection, null);
			if (type.IsGenericType)
			{
				foreach (Type type2 in type.GetGenericArguments())
				{
					flag |= this.ImplicitLoadAssemblyForType(type2, edmItemCollection);
				}
			}
			return flag;
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x00078B64 File Offset: 0x00076D64
		internal AssociationType GetRelationshipType(Type entityClrType, string relationshipName)
		{
			AssociationType result;
			if (base.TryGetItem<AssociationType>(relationshipName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x00078B80 File Offset: 0x00076D80
		internal static Dictionary<string, EdmType> LoadTypesExpensiveWay(Assembly assembly)
		{
			Dictionary<string, EdmType> result = null;
			KnownAssembliesSet knownAssemblies = new KnownAssembliesSet();
			List<EdmItemError> list;
			AssemblyCache.LoadAssembly(assembly, false, knownAssemblies, out result, out list);
			if (list.Count != 0)
			{
				throw EntityUtil.InvalidSchemaEncountered(Helper.CombineErrorMessage(list));
			}
			return result;
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x00078BB8 File Offset: 0x00076DB8
		internal static AssociationType GetRelationshipTypeExpensiveWay(Type entityClrType, string relationshipName)
		{
			Dictionary<string, EdmType> dictionary = ObjectItemCollection.LoadTypesExpensiveWay(entityClrType.Assembly);
			EdmType edmType;
			if (dictionary != null && dictionary.TryGetValue(relationshipName, out edmType) && Helper.IsRelationshipType(edmType))
			{
				return (AssociationType)edmType;
			}
			return null;
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x00078BEF File Offset: 0x00076DEF
		internal static IEnumerable<AssociationType> GetAllRelationshipTypesExpensiveWay(Assembly assembly)
		{
			Dictionary<string, EdmType> dictionary = ObjectItemCollection.LoadTypesExpensiveWay(assembly);
			if (dictionary != null)
			{
				foreach (EdmType edmType in dictionary.Values)
				{
					if (Helper.IsAssociationType(edmType))
					{
						yield return (AssociationType)edmType;
					}
				}
				Dictionary<string, EdmType>.ValueCollection.Enumerator enumerator = default(Dictionary<string, EdmType>.ValueCollection.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x00078C00 File Offset: 0x00076E00
		private static bool LoadAssemblyFromCache(ObjectItemCollection objectItemCollection, Assembly assembly, bool loadReferencedAssemblies, EdmItemCollection edmItemCollection, Action<string> logLoadMessage)
		{
			KnownAssemblyEntry knownAssemblyEntry;
			if (objectItemCollection._knownAssemblies.TryGetKnownAssembly(assembly, objectItemCollection._loaderCookie, edmItemCollection, out knownAssemblyEntry))
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
			object loadAssemblyLock = objectItemCollection.LoadAssemblyLock;
			bool result;
			lock (loadAssemblyLock)
			{
				if (objectItemCollection._knownAssemblies.TryGetKnownAssembly(assembly, objectItemCollection._loaderCookie, edmItemCollection, out knownAssemblyEntry) && (!loadReferencedAssemblies || knownAssemblyEntry.ReferencedAssembliesAreLoaded))
				{
					result = true;
				}
				else
				{
					KnownAssembliesSet knownAssembliesSet;
					if (objectItemCollection != null)
					{
						knownAssembliesSet = new KnownAssembliesSet(objectItemCollection._knownAssemblies);
					}
					else
					{
						knownAssembliesSet = new KnownAssembliesSet();
					}
					Dictionary<string, EdmType> dictionary;
					List<EdmItemError> list;
					AssemblyCache.LoadAssembly(assembly, loadReferencedAssemblies, knownAssembliesSet, edmItemCollection, logLoadMessage, ref objectItemCollection._loaderCookie, out dictionary, out list);
					if (list.Count != 0)
					{
						throw EntityUtil.InvalidSchemaEncountered(Helper.CombineErrorMessage(list));
					}
					if (dictionary.Count != 0)
					{
						List<GlobalItem> list2 = new List<GlobalItem>();
						foreach (EdmType edmType in dictionary.Values)
						{
							list2.Add(edmType);
							string text = "";
							try
							{
								if (Helper.IsEntityType(edmType))
								{
									text = ((ClrEntityType)edmType).CSpaceTypeName;
									objectItemCollection._ocMapping.Add(text, edmType);
								}
								else if (Helper.IsComplexType(edmType))
								{
									text = ((ClrComplexType)edmType).CSpaceTypeName;
									objectItemCollection._ocMapping.Add(text, edmType);
								}
								else if (Helper.IsEnumType(edmType))
								{
									text = ((ClrEnumType)edmType).CSpaceTypeName;
									objectItemCollection._ocMapping.Add(text, edmType);
								}
							}
							catch (ArgumentException innerException)
							{
								throw new MappingException(Strings.Mapping_CannotMapCLRTypeMultipleTimes(text), innerException);
							}
						}
						objectItemCollection.AtomicAddRange(list2);
					}
					objectItemCollection._knownAssemblies = knownAssembliesSet;
					foreach (Assembly assembly2 in knownAssembliesSet.Assemblies)
					{
						ObjectItemCollection.CollectIfViewGenAssembly(assembly2);
					}
					result = (dictionary.Count != 0);
				}
			}
			return result;
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x00078E6C File Offset: 0x0007706C
		private static void CollectIfViewGenAssembly(Assembly assembly)
		{
			if (assembly.IsDefined(typeof(EntityViewGenerationAttribute), false) && !AssemblyCache.ViewGenerationAssemblies.Contains(assembly))
			{
				AssemblyCache.ViewGenerationAssemblies.Add(assembly);
			}
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x00078E99 File Offset: 0x00077099
		public IEnumerable<PrimitiveType> GetPrimitiveTypes()
		{
			return this._primitiveTypeMaps.GetTypes();
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x00078EA6 File Offset: 0x000770A6
		public Type GetClrType(StructuralType objectSpaceType)
		{
			return ObjectItemCollection.GetClrType(objectSpaceType);
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x00078EAE File Offset: 0x000770AE
		public bool TryGetClrType(StructuralType objectSpaceType, out Type clrType)
		{
			return ObjectItemCollection.TryGetClrType(objectSpaceType, out clrType);
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x00078EA6 File Offset: 0x000770A6
		public Type GetClrType(EnumType objectSpaceType)
		{
			return ObjectItemCollection.GetClrType(objectSpaceType);
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x00078EAE File Offset: 0x000770AE
		public bool TryGetClrType(EnumType objectSpaceType, out Type clrType)
		{
			return ObjectItemCollection.TryGetClrType(objectSpaceType, out clrType);
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x00078EB8 File Offset: 0x000770B8
		private static Type GetClrType(EdmType objectSpaceType)
		{
			Type result;
			if (!ObjectItemCollection.TryGetClrType(objectSpaceType, out result))
			{
				throw EntityUtil.Argument(Strings.FailedToFindClrTypeMapping(objectSpaceType.Identity));
			}
			return result;
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x00078EE4 File Offset: 0x000770E4
		private static bool TryGetClrType(EdmType objectSpaceType, out Type clrType)
		{
			EntityUtil.CheckArgumentNull<EdmType>(objectSpaceType, "objectSpaceType");
			if (objectSpaceType.DataSpace != DataSpace.OSpace)
			{
				throw EntityUtil.Argument(Strings.ArgumentMustBeOSpaceType, "objectSpaceType");
			}
			clrType = null;
			if (Helper.IsEntityType(objectSpaceType) || Helper.IsComplexType(objectSpaceType) || Helper.IsEnumType(objectSpaceType))
			{
				clrType = objectSpaceType.ClrType;
			}
			return clrType != null;
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x00078F40 File Offset: 0x00077140
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

		// Token: 0x06002261 RID: 8801 RVA: 0x00078F7A File Offset: 0x0007717A
		internal bool TryGetOSpaceType(EdmType cspaceType, out EdmType edmType)
		{
			if (Helper.IsEntityType(cspaceType) || Helper.IsComplexType(cspaceType) || Helper.IsEnumType(cspaceType))
			{
				return this._ocMapping.TryGetValue(cspaceType.Identity, out edmType);
			}
			return base.TryGetItem<EdmType>(cspaceType.Identity, out edmType);
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x00078FB4 File Offset: 0x000771B4
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

		// Token: 0x06002263 RID: 8803 RVA: 0x00079003 File Offset: 0x00077203
		public override ReadOnlyCollection<T> GetItems<T>()
		{
			return base.InternalGetItems(typeof(T)) as ReadOnlyCollection<T>;
		}

		// Token: 0x04000EE7 RID: 3815
		private readonly CacheForPrimitiveTypes _primitiveTypeMaps = new CacheForPrimitiveTypes();

		// Token: 0x04000EE8 RID: 3816
		private KnownAssembliesSet _knownAssemblies = new KnownAssembliesSet();

		// Token: 0x04000EE9 RID: 3817
		private Dictionary<string, EdmType> _ocMapping = new Dictionary<string, EdmType>();

		// Token: 0x04000EEA RID: 3818
		private object _loaderCookie;

		// Token: 0x04000EEB RID: 3819
		private object _loadAssemblyLock = new object();
	}
}
