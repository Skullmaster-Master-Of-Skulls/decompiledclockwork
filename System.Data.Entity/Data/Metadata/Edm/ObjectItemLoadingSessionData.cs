using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x0200021F RID: 543
	internal sealed class ObjectItemLoadingSessionData
	{
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x0007F5AC File Offset: 0x0007D7AC
		internal Dictionary<string, EdmType> TypesInLoading
		{
			get
			{
				return this._typesInLoading;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x0007F5B4 File Offset: 0x0007D7B4
		internal Dictionary<Assembly, MutableAssemblyCacheEntry> AssembliesLoaded
		{
			get
			{
				return this._listOfAssembliesLoaded;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x0007F5BC File Offset: 0x0007D7BC
		internal List<EdmItemError> EdmItemErrors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x0007F5C4 File Offset: 0x0007D7C4
		internal KnownAssembliesSet KnownAssemblies
		{
			get
			{
				return this._knownAssemblies;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x0007F5CC File Offset: 0x0007D7CC
		internal LockedAssemblyCache LockedAssemblyCache
		{
			get
			{
				return this._lockedAssemblyCache;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x0007F5D4 File Offset: 0x0007D7D4
		internal EdmItemCollection EdmItemCollection
		{
			get
			{
				return this._edmItemCollection;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06002382 RID: 9090 RVA: 0x0007F5DC File Offset: 0x0007D7DC
		internal Dictionary<EdmType, EdmType> CspaceToOspace
		{
			get
			{
				return this._cspaceToOspace;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x0007F5E4 File Offset: 0x0007D7E4
		// (set) Token: 0x06002384 RID: 9092 RVA: 0x0007F5EC File Offset: 0x0007D7EC
		internal bool ConventionBasedRelationshipsAreLoaded
		{
			get
			{
				return this._conventionBasedRelationshipsAreLoaded;
			}
			set
			{
				this._conventionBasedRelationshipsAreLoaded = value;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x0007F5F5 File Offset: 0x0007D7F5
		internal LoadMessageLogger LoadMessageLogger
		{
			get
			{
				return this._loadMessageLogger;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x0007F600 File Offset: 0x0007D800
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

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x0007F6E4 File Offset: 0x0007D8E4
		// (set) Token: 0x06002388 RID: 9096 RVA: 0x0007F6EC File Offset: 0x0007D8EC
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

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002389 RID: 9097 RVA: 0x0007F703 File Offset: 0x0007D903
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

		// Token: 0x0600238A RID: 9098 RVA: 0x0007F71C File Offset: 0x0007D91C
		internal ObjectItemLoadingSessionData(KnownAssembliesSet knownAssemblies, LockedAssemblyCache lockedAssemblyCache, EdmItemCollection edmItemCollection, Action<string> logLoadMessage, object loaderCookie)
		{
			this._typesInLoading = new Dictionary<string, EdmType>(StringComparer.Ordinal);
			this._errors = new List<EdmItemError>();
			this._knownAssemblies = knownAssemblies;
			this._lockedAssemblyCache = lockedAssemblyCache;
			this._loadersThatNeedLevel1PostSessionProcessing = new HashSet<ObjectItemAssemblyLoader>();
			this._loadersThatNeedLevel2PostSessionProcessing = new HashSet<ObjectItemAssemblyLoader>();
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

		// Token: 0x0600238B RID: 9099 RVA: 0x0007F92C File Offset: 0x0007DB2C
		internal void RegisterForLevel1PostSessionProcessing(ObjectItemAssemblyLoader loader)
		{
			this._loadersThatNeedLevel1PostSessionProcessing.Add(loader);
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x0007F93B File Offset: 0x0007DB3B
		internal void RegisterForLevel2PostSessionProcessing(ObjectItemAssemblyLoader loader)
		{
			this._loadersThatNeedLevel2PostSessionProcessing.Add(loader);
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x0007F94C File Offset: 0x0007DB4C
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

		// Token: 0x04000FAF RID: 4015
		private Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader> _loaderFactory;

		// Token: 0x04000FB0 RID: 4016
		private readonly Dictionary<string, EdmType> _typesInLoading;

		// Token: 0x04000FB1 RID: 4017
		private bool _conventionBasedRelationshipsAreLoaded;

		// Token: 0x04000FB2 RID: 4018
		private LoadMessageLogger _loadMessageLogger;

		// Token: 0x04000FB3 RID: 4019
		private readonly List<EdmItemError> _errors;

		// Token: 0x04000FB4 RID: 4020
		private readonly Dictionary<Assembly, MutableAssemblyCacheEntry> _listOfAssembliesLoaded = new Dictionary<Assembly, MutableAssemblyCacheEntry>();

		// Token: 0x04000FB5 RID: 4021
		private readonly KnownAssembliesSet _knownAssemblies;

		// Token: 0x04000FB6 RID: 4022
		private readonly LockedAssemblyCache _lockedAssemblyCache;

		// Token: 0x04000FB7 RID: 4023
		private readonly HashSet<ObjectItemAssemblyLoader> _loadersThatNeedLevel1PostSessionProcessing;

		// Token: 0x04000FB8 RID: 4024
		private readonly HashSet<ObjectItemAssemblyLoader> _loadersThatNeedLevel2PostSessionProcessing;

		// Token: 0x04000FB9 RID: 4025
		private readonly EdmItemCollection _edmItemCollection;

		// Token: 0x04000FBA RID: 4026
		private Dictionary<string, KeyValuePair<EdmType, int>> _conventionCSpaceTypeNames;

		// Token: 0x04000FBB RID: 4027
		private Dictionary<EdmType, EdmType> _cspaceToOspace;

		// Token: 0x04000FBC RID: 4028
		private object _originalLoaderCookie;
	}
}
