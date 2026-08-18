using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000214 RID: 532
	internal static class AssemblyCache
	{
		// Token: 0x0600230B RID: 8971 RVA: 0x0007C848 File Offset: 0x0007AA48
		internal static LockedAssemblyCache AquireLockedAssemblyCache()
		{
			return new LockedAssemblyCache(AssemblyCache._assemblyCacheLock, AssemblyCache.s_globalAssemblyCache);
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x0007C85C File Offset: 0x0007AA5C
		internal static void LoadAssembly(Assembly assembly, bool loadReferencedAssemblies, KnownAssembliesSet knownAssemblies, out Dictionary<string, EdmType> typesInLoading, out List<EdmItemError> errors)
		{
			object obj = null;
			AssemblyCache.LoadAssembly(assembly, loadReferencedAssemblies, knownAssemblies, null, null, ref obj, out typesInLoading, out errors);
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x0007C87C File Offset: 0x0007AA7C
		internal static void LoadAssembly(Assembly assembly, bool loadReferencedAssemblies, KnownAssembliesSet knownAssemblies, EdmItemCollection edmItemCollection, Action<string> logLoadMessage, ref object loaderCookie, out Dictionary<string, EdmType> typesInLoading, out List<EdmItemError> errors)
		{
			typesInLoading = null;
			errors = null;
			using (LockedAssemblyCache lockedAssemblyCache = AssemblyCache.AquireLockedAssemblyCache())
			{
				ObjectItemLoadingSessionData objectItemLoadingSessionData = new ObjectItemLoadingSessionData(knownAssemblies, lockedAssemblyCache, edmItemCollection, logLoadMessage, loaderCookie);
				AssemblyCache.LoadAssembly(assembly, loadReferencedAssemblies, objectItemLoadingSessionData);
				loaderCookie = objectItemLoadingSessionData.LoaderCookie;
				objectItemLoadingSessionData.CompleteSession();
				if (objectItemLoadingSessionData.EdmItemErrors.Count == 0)
				{
					new EdmValidator
					{
						SkipReadOnlyItems = true
					}.Validate<EdmType>(objectItemLoadingSessionData.TypesInLoading.Values, objectItemLoadingSessionData.EdmItemErrors);
					if (objectItemLoadingSessionData.EdmItemErrors.Count == 0)
					{
						if (ObjectItemAssemblyLoader.IsAttributeLoader(objectItemLoadingSessionData.ObjectItemAssemblyLoaderFactory))
						{
							AssemblyCache.UpdateCache(lockedAssemblyCache, objectItemLoadingSessionData.AssembliesLoaded);
						}
						else if (objectItemLoadingSessionData.EdmItemCollection != null && ObjectItemAssemblyLoader.IsConventionLoader(objectItemLoadingSessionData.ObjectItemAssemblyLoaderFactory))
						{
							AssemblyCache.UpdateCache(objectItemLoadingSessionData.EdmItemCollection, objectItemLoadingSessionData.AssembliesLoaded);
						}
					}
				}
				if (objectItemLoadingSessionData.TypesInLoading.Count > 0)
				{
					foreach (EdmType edmType in objectItemLoadingSessionData.TypesInLoading.Values)
					{
						edmType.SetReadOnly();
					}
				}
				typesInLoading = objectItemLoadingSessionData.TypesInLoading;
				errors = objectItemLoadingSessionData.EdmItemErrors;
			}
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x0007C9D8 File Offset: 0x0007ABD8
		private static void LoadAssembly(Assembly assembly, bool loadReferencedAssemblies, ObjectItemLoadingSessionData loadingData)
		{
			KnownAssemblyEntry knownAssemblyEntry;
			bool flag;
			if (loadingData.KnownAssemblies.TryGetKnownAssembly(assembly, loadingData.ObjectItemAssemblyLoaderFactory, loadingData.EdmItemCollection, out knownAssemblyEntry))
			{
				flag = (!knownAssemblyEntry.ReferencedAssembliesAreLoaded && loadReferencedAssemblies);
			}
			else
			{
				ObjectItemAssemblyLoader objectItemAssemblyLoader = ObjectItemAssemblyLoader.CreateLoader(assembly, loadingData);
				objectItemAssemblyLoader.Load();
				flag = loadReferencedAssemblies;
			}
			if (flag)
			{
				if ((knownAssemblyEntry == null && loadingData.KnownAssemblies.TryGetKnownAssembly(assembly, loadingData.ObjectItemAssemblyLoaderFactory, loadingData.EdmItemCollection, out knownAssemblyEntry)) || knownAssemblyEntry != null)
				{
					knownAssemblyEntry.ReferencedAssembliesAreLoaded = true;
				}
				foreach (Assembly assembly2 in MetadataAssemblyHelper.GetNonSystemReferencedAssemblies(assembly))
				{
					AssemblyCache.LoadAssembly(assembly2, loadReferencedAssemblies, loadingData);
				}
			}
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x0007CA90 File Offset: 0x0007AC90
		private static void UpdateCache(EdmItemCollection edmItemCollection, Dictionary<Assembly, MutableAssemblyCacheEntry> assemblies)
		{
			foreach (KeyValuePair<Assembly, MutableAssemblyCacheEntry> keyValuePair in assemblies)
			{
				edmItemCollection.ConventionalOcCache.AddAssemblyToOcCacheFromAssemblyCache(keyValuePair.Key, new ImmutableAssemblyCacheEntry(keyValuePair.Value));
			}
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x0007CAF8 File Offset: 0x0007ACF8
		private static void UpdateCache(LockedAssemblyCache lockedAssemblyCache, Dictionary<Assembly, MutableAssemblyCacheEntry> assemblies)
		{
			foreach (KeyValuePair<Assembly, MutableAssemblyCacheEntry> keyValuePair in assemblies)
			{
				lockedAssemblyCache.Add(keyValuePair.Key, new ImmutableAssemblyCacheEntry(keyValuePair.Value));
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x0007CB58 File Offset: 0x0007AD58
		internal static IList<Assembly> ViewGenerationAssemblies
		{
			get
			{
				return AssemblyCache.s_viewGenAssemblies;
			}
		}

		// Token: 0x04000F98 RID: 3992
		private static readonly Dictionary<Assembly, ImmutableAssemblyCacheEntry> s_globalAssemblyCache = new Dictionary<Assembly, ImmutableAssemblyCacheEntry>();

		// Token: 0x04000F99 RID: 3993
		private static object _assemblyCacheLock = new object();

		// Token: 0x04000F9A RID: 3994
		private static IList<Assembly> s_viewGenAssemblies = new ThreadSafeList<Assembly>();
	}
}
