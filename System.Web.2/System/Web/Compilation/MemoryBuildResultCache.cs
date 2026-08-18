using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000821 RID: 2081
	internal class MemoryBuildResultCache : BuildResultCache
	{
		// Token: 0x06006380 RID: 25472 RVA: 0x0015C753 File Offset: 0x0015A953
		internal MemoryBuildResultCache()
		{
			AppDomain.CurrentDomain.AssemblyLoad += this.OnAssemblyLoad;
		}

		// Token: 0x06006381 RID: 25473 RVA: 0x0015C77C File Offset: 0x0015A97C
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private void OnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
		{
			Assembly loadedAssembly = args.LoadedAssembly;
			if (loadedAssembly.GlobalAssemblyCache)
			{
				return;
			}
			string name = loadedAssembly.GetName().Name;
			if (!StringUtil.StringStartsWith(name, "App_"))
			{
				return;
			}
			foreach (AssemblyName assemblyName in loadedAssembly.GetReferencedAssemblies())
			{
				if (StringUtil.StringStartsWith(assemblyName.Name, "App_"))
				{
					Hashtable dependentAssemblies = this._dependentAssemblies;
					lock (dependentAssemblies)
					{
						ArrayList arrayList = this._dependentAssemblies[assemblyName.Name] as ArrayList;
						if (arrayList == null)
						{
							arrayList = new ArrayList();
							this._dependentAssemblies[assemblyName.Name] = arrayList;
						}
						arrayList.Add(name);
					}
				}
			}
		}

		// Token: 0x06006382 RID: 25474 RVA: 0x0015C858 File Offset: 0x0015AA58
		internal override BuildResult GetBuildResult(string cacheKey, VirtualPath virtualPath, long hashCode, bool ensureIsUpToDate)
		{
			string memoryCacheKey = MemoryBuildResultCache.GetMemoryCacheKey(cacheKey);
			BuildResult buildResult = (BuildResult)HttpRuntime.Cache.InternalCache.Get(memoryCacheKey);
			if (buildResult == null)
			{
				return null;
			}
			if (!buildResult.UsesCacheDependency && !buildResult.IsUpToDate(virtualPath, ensureIsUpToDate))
			{
				HttpRuntime.Cache.InternalCache.Remove(memoryCacheKey);
				return null;
			}
			return buildResult;
		}

		// Token: 0x06006383 RID: 25475 RVA: 0x0015C8B0 File Offset: 0x0015AAB0
		internal override void CacheBuildResult(string cacheKey, BuildResult result, long hashCode, DateTime utcStart)
		{
			ICollection virtualPathDependencies = result.VirtualPathDependencies;
			CacheDependency cacheDependency = null;
			if (virtualPathDependencies != null)
			{
				cacheDependency = result.VirtualPath.GetCacheDependency(virtualPathDependencies, utcStart);
				if (cacheDependency != null)
				{
					result.UsesCacheDependency = true;
				}
			}
			if (!result.CacheToMemory)
			{
				return;
			}
			if (BuildResultCompiledType.UsesDelayLoadType(result))
			{
				return;
			}
			BuildResultCompiledAssemblyBase buildResultCompiledAssemblyBase = result as BuildResultCompiledAssemblyBase;
			if (buildResultCompiledAssemblyBase != null && buildResultCompiledAssemblyBase.ResultAssembly != null && !buildResultCompiledAssemblyBase.UsesExistingAssembly)
			{
				string assemblyCacheKey = BuildResultCache.GetAssemblyCacheKey(buildResultCompiledAssemblyBase.ResultAssembly);
				Assembly left = (Assembly)HttpRuntime.Cache.InternalCache.Get(assemblyCacheKey);
				if (left == null)
				{
					HttpRuntime.Cache.InternalCache.Insert(assemblyCacheKey, buildResultCompiledAssemblyBase.ResultAssembly, new CacheInsertOptions
					{
						Priority = CacheItemPriority.NotRemovable
					});
				}
				CacheDependency cacheDependency2 = new CacheDependency(0, null, new string[]
				{
					assemblyCacheKey
				});
				if (cacheDependency != null)
				{
					AggregateCacheDependency aggregateCacheDependency = new AggregateCacheDependency();
					aggregateCacheDependency.Add(new CacheDependency[]
					{
						cacheDependency,
						cacheDependency2
					});
					cacheDependency = aggregateCacheDependency;
				}
				else
				{
					cacheDependency = cacheDependency2;
				}
			}
			string memoryCacheKey = MemoryBuildResultCache.GetMemoryCacheKey(cacheKey);
			CacheItemPriority priority;
			if (result.IsUnloadable)
			{
				priority = CacheItemPriority.Normal;
			}
			else
			{
				priority = CacheItemPriority.NotRemovable;
			}
			CacheItemRemovedCallback onRemovedCallback = null;
			if (result.ShutdownAppDomainOnChange || result is BuildResultCompiledAssemblyBase)
			{
				if (this._onRemoveCallback == null)
				{
					this._onRemoveCallback = new CacheItemRemovedCallback(this.OnCacheItemRemoved);
				}
				onRemovedCallback = this._onRemoveCallback;
			}
			HttpRuntime.Cache.InternalCache.Insert(memoryCacheKey, result, new CacheInsertOptions
			{
				Dependencies = cacheDependency,
				AbsoluteExpiration = result.MemoryCacheExpiration,
				SlidingExpiration = result.MemoryCacheSlidingExpiration,
				Priority = priority,
				OnRemovedCallback = onRemovedCallback
			});
		}

		// Token: 0x06006384 RID: 25476 RVA: 0x0015CA3C File Offset: 0x0015AC3C
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private void OnCacheItemRemoved(string key, object value, CacheItemRemovedReason reason)
		{
			if (reason == CacheItemRemovedReason.DependencyChanged)
			{
				if (HostingEnvironment.ShutdownInitiated)
				{
					this.RemoveAssemblyAndCleanupDependenciesShuttingDown(value as BuildResultCompiledAssembly);
					return;
				}
				this.RemoveAssemblyAndCleanupDependencies(value as BuildResultCompiledAssemblyBase);
				if (((BuildResult)value).ShutdownAppDomainOnChange)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(MemoryBuildResultCache.ShutdownCallBack), "BuildResult change, cache key=" + key);
				}
			}
		}

		// Token: 0x06006385 RID: 25477 RVA: 0x0015CA98 File Offset: 0x0015AC98
		private static void ShutdownCallBack(object state)
		{
			string text = state as string;
			if (text != null)
			{
				HttpRuntime.SetShutdownReason(ApplicationShutdownReason.BuildManagerChange, text);
			}
			HostingEnvironment.InitiateShutdownWithoutDemand();
		}

		// Token: 0x06006386 RID: 25478 RVA: 0x0015CABC File Offset: 0x0015ACBC
		internal void RemoveAssemblyAndCleanupDependenciesShuttingDown(BuildResultCompiledAssemblyBase compiledResult)
		{
			if (compiledResult == null)
			{
				return;
			}
			if (compiledResult != null && compiledResult.ResultAssembly != null && !compiledResult.UsesExistingAssembly)
			{
				string name = compiledResult.ResultAssembly.GetName().Name;
				Hashtable dependentAssemblies = this._dependentAssemblies;
				lock (dependentAssemblies)
				{
					this.RemoveAssemblyAndCleanupDependenciesNoLock(name);
				}
			}
		}

		// Token: 0x06006387 RID: 25479 RVA: 0x0015CB2C File Offset: 0x0015AD2C
		internal void RemoveAssemblyAndCleanupDependencies(BuildResultCompiledAssemblyBase compiledResult)
		{
			if (compiledResult == null)
			{
				return;
			}
			if (compiledResult != null && compiledResult.ResultAssembly != null && !compiledResult.UsesExistingAssembly)
			{
				this.RemoveAssemblyAndCleanupDependencies(compiledResult.ResultAssembly.GetName().Name);
			}
		}

		// Token: 0x06006388 RID: 25480 RVA: 0x0015CB64 File Offset: 0x0015AD64
		private void RemoveAssemblyAndCleanupDependencies(string assemblyName)
		{
			bool flag = false;
			try
			{
				CompilationLock.GetLock(ref flag);
				Hashtable dependentAssemblies = this._dependentAssemblies;
				lock (dependentAssemblies)
				{
					this.RemoveAssemblyAndCleanupDependenciesNoLock(assemblyName);
				}
			}
			finally
			{
				if (flag)
				{
					CompilationLock.ReleaseLock();
				}
				DiskBuildResultCache.ShutDownAppDomainIfRequired();
			}
		}

		// Token: 0x06006389 RID: 25481 RVA: 0x0015CBC8 File Offset: 0x0015ADC8
		private void RemoveAssemblyAndCleanupDependenciesNoLock(string assemblyName)
		{
			string assemblyCacheKeyFromName = BuildResultCache.GetAssemblyCacheKeyFromName(assemblyName);
			Assembly assembly = (Assembly)HttpRuntime.Cache.InternalCache.Get(assemblyCacheKeyFromName);
			if (assembly == null)
			{
				return;
			}
			string assemblyCodeBase = Util.GetAssemblyCodeBase(assembly);
			HttpRuntime.Cache.InternalCache.Remove(assemblyCacheKeyFromName);
			ICollection collection = this._dependentAssemblies[assemblyName] as ICollection;
			if (collection != null)
			{
				foreach (object obj in collection)
				{
					string assemblyName2 = (string)obj;
					this.RemoveAssemblyAndCleanupDependenciesNoLock(assemblyName2);
				}
				this._dependentAssemblies.Remove(assemblyCacheKeyFromName);
			}
			MemoryBuildResultCache.RemoveAssembly(assemblyCodeBase);
		}

		// Token: 0x0600638A RID: 25482 RVA: 0x0015CC8C File Offset: 0x0015AE8C
		private static void RemoveAssembly(string path)
		{
			FileInfo fileInfo = new FileInfo(path);
			DiskBuildResultCache.RemoveAssembly(fileInfo);
			string text = Path.ChangeExtension(fileInfo.FullName, ".pdb");
			if (File.Exists(text))
			{
				DiskBuildResultCache.TryDeleteFile(new FileInfo(text));
			}
		}

		// Token: 0x0600638B RID: 25483 RVA: 0x0015CCCB File Offset: 0x0015AECB
		private static string GetMemoryCacheKey(string cacheKey)
		{
			return "c" + cacheKey;
		}

		// Token: 0x0400338B RID: 13195
		private CacheItemRemovedCallback _onRemoveCallback;

		// Token: 0x0400338C RID: 13196
		private Hashtable _dependentAssemblies = new Hashtable();
	}
}
