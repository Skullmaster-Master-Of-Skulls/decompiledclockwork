using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000511 RID: 1297
	internal static class AssemblyCache
	{
		// Token: 0x060030EE RID: 12526 RVA: 0x000EA55E File Offset: 0x000E875E
		internal static LockedAssemblyCache AquireLockedAssemblyCache()
		{
			return new LockedAssemblyCache(AssemblyCache._assemblyCacheLock, AssemblyCache._globalAssemblyCache);
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000EA570 File Offset: 0x000E8770
		internal static void LoadAssembly(Assembly assembly, bool loadReferencedAssemblies, KnownAssembliesSet knownAssemblies, out Dictionary<string, EdmType> typesInLoading, out List<EdmItemError> errors)
		{
			object obj = null;
			AssemblyCache.LoadAssembly(assembly, loadReferencedAssemblies, knownAssemblies, null, null, ref obj, out typesInLoading, out errors);
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x000EA590 File Offset: 0x000E8790
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

		// Token: 0x060030F1 RID: 12529 RVA: 0x000EA6EC File Offset: 0x000E88EC
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

		// Token: 0x060030F2 RID: 12530 RVA: 0x000EA7A8 File Offset: 0x000E89A8
		private static void UpdateCache(EdmItemCollection edmItemCollection, Dictionary<Assembly, MutableAssemblyCacheEntry> assemblies)
		{
			foreach (KeyValuePair<Assembly, MutableAssemblyCacheEntry> keyValuePair in assemblies)
			{
				edmItemCollection.ConventionalOcCache.AddAssemblyToOcCacheFromAssemblyCache(keyValuePair.Key, new ImmutableAssemblyCacheEntry(keyValuePair.Value));
			}
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x000EA810 File Offset: 0x000E8A10
		private static void UpdateCache(LockedAssemblyCache lockedAssemblyCache, Dictionary<Assembly, MutableAssemblyCacheEntry> assemblies)
		{
			foreach (KeyValuePair<Assembly, MutableAssemblyCacheEntry> keyValuePair in assemblies)
			{
				lockedAssemblyCache.Add(keyValuePair.Key, new ImmutableAssemblyCacheEntry(keyValuePair.Value));
			}
		}

		// Token: 0x04001282 RID: 4738
		private static readonly Dictionary<Assembly, ImmutableAssemblyCacheEntry> _globalAssemblyCache = new Dictionary<Assembly, ImmutableAssemblyCacheEntry>();

		// Token: 0x04001283 RID: 4739
		private static readonly object _assemblyCacheLock = new object();
	}
}
