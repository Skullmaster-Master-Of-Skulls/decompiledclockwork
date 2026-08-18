using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200051F RID: 1311
	internal class ObjectItemLoadingSessionData
	{
		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x000EC7B6 File Offset: 0x000EA9B6
		internal virtual Dictionary<string, EdmType> TypesInLoading
		{
			get
			{
				return this._typesInLoading;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x000EC7BE File Offset: 0x000EA9BE
		internal Dictionary<Assembly, MutableAssemblyCacheEntry> AssembliesLoaded
		{
			get
			{
				return this._listOfAssembliesLoaded;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06003162 RID: 12642 RVA: 0x000EC7C6 File Offset: 0x000EA9C6
		internal virtual List<EdmItemError> EdmItemErrors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06003163 RID: 12643 RVA: 0x000EC7CE File Offset: 0x000EA9CE
		internal KnownAssembliesSet KnownAssemblies
		{
			get
			{
				return this._knownAssemblies;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06003164 RID: 12644 RVA: 0x000EC7D6 File Offset: 0x000EA9D6
		internal LockedAssemblyCache LockedAssemblyCache
		{
			get
			{
				return this._lockedAssemblyCache;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06003165 RID: 12645 RVA: 0x000EC7DE File Offset: 0x000EA9DE
		internal EdmItemCollection EdmItemCollection
		{
			get
			{
				return this._edmItemCollection;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06003166 RID: 12646 RVA: 0x000EC7E6 File Offset: 0x000EA9E6
		internal virtual Dictionary<EdmType, EdmType> CspaceToOspace
		{
			get
			{
				return this._cspaceToOspace;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x000EC7EE File Offset: 0x000EA9EE
		// (set) Token: 0x06003168 RID: 12648 RVA: 0x000EC7F6 File Offset: 0x000EA9F6
		internal bool ConventionBasedRelationshipsAreLoaded { get; set; }

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06003169 RID: 12649 RVA: 0x000EC7FF File Offset: 0x000EA9FF
		internal virtual LoadMessageLogger LoadMessageLogger
		{
			get
			{
				return this._loadMessageLogger;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x000EC808 File Offset: 0x000EAA08
		internal Dictionary<string, KeyValuePair<EdmType, int>> ConventionCSpaceTypeNames
		{
			get
			{
				if (this._edmItemCollection != null && this._conventionCSpaceTypeNames == null)
				{
					this._conventionCSpaceTypeNames = new Dictionary<string, KeyValuePair<EdmType, int>>();
					foreach (EdmType edmType in this._edmItemCollection.GetItems<EdmType>())
					{
						if ((edmType is StructuralType && edmType.BuiltInTypeKind != BuiltInTypeKind.AssociationType) || Helper.IsEnumType(edmType))
						{
							KeyValuePair<EdmType, int> value;
							if (this._conventionCSpaceTypeNames.TryGetValue(edmType.Name, out value))
							{
								this._conventionCSpaceTypeNames[edmType.Name] = new KeyValuePair<EdmType, int>(value.Key, value.Value + 1);
							}
							else
							{
								value = new KeyValuePair<EdmType, int>(edmType, 1);
								this._conventionCSpaceTypeNames.Add(edmType.Name, value);
							}
						}
					}
				}
				return this._conventionCSpaceTypeNames;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600316B RID: 12651 RVA: 0x000EC8F4 File Offset: 0x000EAAF4
		// (set) Token: 0x0600316C RID: 12652 RVA: 0x000EC8FC File Offset: 0x000EAAFC
		internal Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader> ObjectItemAssemblyLoaderFactory
		{
			get
			{
				return this._loaderFactory;
			}
			set
			{
				if (this._loaderFactory != value)
				{
					this._loaderFactory = value;
				}
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600316D RID: 12653 RVA: 0x000EC913 File Offset: 0x000EAB13
		internal object LoaderCookie
		{
			get
			{
				if (this._originalLoaderCookie != null)
				{
					return this._originalLoaderCookie;
				}
				return this._loaderFactory;
			}
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x000EC92A File Offset: 0x000EAB2A
		internal ObjectItemLoadingSessionData()
		{
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x000EC954 File Offset: 0x000EAB54
		internal ObjectItemLoadingSessionData(KnownAssembliesSet knownAssemblies, LockedAssemblyCache lockedAssemblyCache, EdmItemCollection edmItemCollection, Action<string> logLoadMessage, object loaderCookie)
		{
			this._typesInLoading = new Dictionary<string, EdmType>(StringComparer.Ordinal);
			this._errors = new List<EdmItemError>();
			this._knownAssemblies = knownAssemblies;
			this._lockedAssemblyCache = lockedAssemblyCache;
			this._edmItemCollection = edmItemCollection;
			this._loadMessageLogger = new LoadMessageLogger(logLoadMessage);
			this._cspaceToOspace = new Dictionary<EdmType, EdmType>();
			this._loaderFactory = (Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>)loaderCookie;
			this._originalLoaderCookie = loaderCookie;
			if (this._loaderFactory == new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemConventionAssemblyLoader.Create) && this._edmItemCollection != null)
			{
				foreach (KnownAssemblyEntry knownAssemblyEntry in this._knownAssemblies.GetEntries(this._loaderFactory, edmItemCollection))
				{
					foreach (EdmType edmType in knownAssemblyEntry.CacheEntry.TypesInAssembly.OfType<EdmType>())
					{
						if (Helper.IsEntityType(edmType))
						{
							ClrEntityType clrEntityType = (ClrEntityType)edmType;
							this._cspaceToOspace.Add(this._edmItemCollection.GetItem<StructuralType>(clrEntityType.CSpaceTypeName), clrEntityType);
						}
						else if (Helper.IsComplexType(edmType))
						{
							ClrComplexType clrComplexType = (ClrComplexType)edmType;
							this._cspaceToOspace.Add(this._edmItemCollection.GetItem<StructuralType>(clrComplexType.CSpaceTypeName), clrComplexType);
						}
						else if (Helper.IsEnumType(edmType))
						{
							ClrEnumType clrEnumType = (ClrEnumType)edmType;
							this._cspaceToOspace.Add(this._edmItemCollection.GetItem<EnumType>(clrEnumType.CSpaceTypeName), clrEnumType);
						}
						else
						{
							this._cspaceToOspace.Add(this._edmItemCollection.GetItem<StructuralType>(edmType.FullName), edmType);
						}
					}
				}
			}
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x000ECB68 File Offset: 0x000EAD68
		internal void RegisterForLevel1PostSessionProcessing(ObjectItemAssemblyLoader loader)
		{
			this._loadersThatNeedLevel1PostSessionProcessing.Add(loader);
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x000ECB77 File Offset: 0x000EAD77
		internal void RegisterForLevel2PostSessionProcessing(ObjectItemAssemblyLoader loader)
		{
			this._loadersThatNeedLevel2PostSessionProcessing.Add(loader);
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x000ECB88 File Offset: 0x000EAD88
		internal void CompleteSession()
		{
			foreach (ObjectItemAssemblyLoader objectItemAssemblyLoader in this._loadersThatNeedLevel1PostSessionProcessing)
			{
				objectItemAssemblyLoader.OnLevel1SessionProcessing();
			}
			foreach (ObjectItemAssemblyLoader objectItemAssemblyLoader2 in this._loadersThatNeedLevel2PostSessionProcessing)
			{
				objectItemAssemblyLoader2.OnLevel2SessionProcessing();
			}
		}

		// Token: 0x0400129E RID: 4766
		private Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader> _loaderFactory;

		// Token: 0x0400129F RID: 4767
		private readonly Dictionary<string, EdmType> _typesInLoading;

		// Token: 0x040012A0 RID: 4768
		private readonly LoadMessageLogger _loadMessageLogger;

		// Token: 0x040012A1 RID: 4769
		private readonly List<EdmItemError> _errors;

		// Token: 0x040012A2 RID: 4770
		private readonly Dictionary<Assembly, MutableAssemblyCacheEntry> _listOfAssembliesLoaded = new Dictionary<Assembly, MutableAssemblyCacheEntry>();

		// Token: 0x040012A3 RID: 4771
		private readonly KnownAssembliesSet _knownAssemblies;

		// Token: 0x040012A4 RID: 4772
		private readonly LockedAssemblyCache _lockedAssemblyCache;

		// Token: 0x040012A5 RID: 4773
		private readonly HashSet<ObjectItemAssemblyLoader> _loadersThatNeedLevel1PostSessionProcessing = new HashSet<ObjectItemAssemblyLoader>();

		// Token: 0x040012A6 RID: 4774
		private readonly HashSet<ObjectItemAssemblyLoader> _loadersThatNeedLevel2PostSessionProcessing = new HashSet<ObjectItemAssemblyLoader>();

		// Token: 0x040012A7 RID: 4775
		private readonly EdmItemCollection _edmItemCollection;

		// Token: 0x040012A8 RID: 4776
		private Dictionary<string, KeyValuePair<EdmType, int>> _conventionCSpaceTypeNames;

		// Token: 0x040012A9 RID: 4777
		private readonly Dictionary<EdmType, EdmType> _cspaceToOspace;

		// Token: 0x040012AA RID: 4778
		private readonly object _originalLoaderCookie;
	}
}
