using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Threading;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Management;
using System.Web.UI;

namespace System.Web.Caching
{
	// Token: 0x02000887 RID: 2183
	public static class OutputCache
	{
		// Token: 0x060066AA RID: 26282 RVA: 0x00169920 File Offset: 0x00167B20
		private static void AddCacheKeyToDependencies(ref CacheDependency dependencies, string cacheKey)
		{
			CacheDependency cacheDependency = new CacheDependency(0, null, new string[]
			{
				cacheKey
			});
			if (dependencies == null)
			{
				dependencies = cacheDependency;
				return;
			}
			AggregateCacheDependency aggregateCacheDependency = dependencies as AggregateCacheDependency;
			if (aggregateCacheDependency != null)
			{
				aggregateCacheDependency.Add(new CacheDependency[]
				{
					cacheDependency
				});
				return;
			}
			aggregateCacheDependency = new AggregateCacheDependency();
			aggregateCacheDependency.Add(new CacheDependency[]
			{
				cacheDependency,
				dependencies
			});
			dependencies = aggregateCacheDependency;
		}

		// Token: 0x060066AB RID: 26283 RVA: 0x00169984 File Offset: 0x00167B84
		private static void EnsureInitialized()
		{
			if (OutputCache.s_inited)
			{
				return;
			}
			object obj = OutputCache.s_initLock;
			lock (obj)
			{
				if (!OutputCache.s_inited)
				{
					OutputCacheSection outputCache = RuntimeConfig.GetAppConfig().OutputCache;
					OutputCache.s_providers = outputCache.CreateProviderCollection();
					OutputCache.s_defaultProvider = outputCache.GetDefaultProvider(OutputCache.s_providers);
					OutputCache.s_entryRemovedCallback = new CacheItemRemovedCallback(OutputCache.EntryRemovedCallback);
					OutputCache.s_dependencyRemovedCallback = new CacheItemRemovedCallback(OutputCache.DependencyRemovedCallback);
					OutputCache.s_dependencyRemovedCallbackForFragment = new CacheItemRemovedCallback(OutputCache.DependencyRemovedCallbackForFragment);
					OutputCache.s_inited = true;
				}
			}
		}

		// Token: 0x060066AC RID: 26284 RVA: 0x00169A2C File Offset: 0x00167C2C
		private static void DecrementCount()
		{
			if (OutputCache.Providers == null)
			{
				Interlocked.Decrement(ref OutputCache.s_cEntries);
			}
		}

		// Token: 0x060066AD RID: 26285 RVA: 0x00169A40 File Offset: 0x00167C40
		private static void IncrementCount()
		{
			if (OutputCache.Providers == null)
			{
				Interlocked.Increment(ref OutputCache.s_cEntries);
			}
		}

		// Token: 0x060066AE RID: 26286 RVA: 0x00169A54 File Offset: 0x00167C54
		private static OutputCacheProvider GetFragmentProvider(string providerName)
		{
			OutputCacheProvider outputCacheProvider;
			if (providerName == null)
			{
				outputCacheProvider = OutputCache.s_defaultProvider;
			}
			else
			{
				outputCacheProvider = OutputCache.s_providers[providerName];
				if (outputCacheProvider == null)
				{
					throw new ProviderException(SR.GetString("Provider_Not_Found", new object[]
					{
						providerName
					}));
				}
			}
			return outputCacheProvider;
		}

		// Token: 0x060066AF RID: 26287 RVA: 0x00169A98 File Offset: 0x00167C98
		private static OutputCacheProvider GetProvider(HttpContext context)
		{
			if (context == null)
			{
				return null;
			}
			HttpApplication applicationInstance = context.ApplicationInstance;
			string outputCacheProviderName = applicationInstance.GetOutputCacheProviderName(context);
			if (outputCacheProviderName == null)
			{
				throw new ProviderException(SR.GetString("GetOutputCacheProviderName_Invalid", new object[]
				{
					outputCacheProviderName
				}));
			}
			if (outputCacheProviderName == "AspNetInternalProvider")
			{
				return null;
			}
			OutputCacheProvider outputCacheProvider = (OutputCache.s_providers == null) ? null : OutputCache.s_providers[outputCacheProviderName];
			if (outputCacheProvider == null)
			{
				throw new ProviderException(SR.GetString("GetOutputCacheProviderName_Invalid", new object[]
				{
					outputCacheProviderName
				}));
			}
			return outputCacheProvider;
		}

		// Token: 0x060066B0 RID: 26288 RVA: 0x00169B1C File Offset: 0x00167D1C
		private static OutputCacheEntry Convert(CachedRawResponse cachedRawResponse, string depKey, string[] fileDependencies)
		{
			List<HeaderElement> list = null;
			ArrayList headers = cachedRawResponse._rawResponse.Headers;
			int num = (headers != null) ? headers.Count : 0;
			for (int i = 0; i < num; i++)
			{
				if (list == null)
				{
					list = new List<HeaderElement>(num);
				}
				HttpResponseHeader httpResponseHeader = (HttpResponseHeader)headers[i];
				list.Add(new HeaderElement(httpResponseHeader.Name, httpResponseHeader.Value));
			}
			List<ResponseElement> list2 = null;
			ArrayList buffers = cachedRawResponse._rawResponse.Buffers;
			num = ((buffers != null) ? buffers.Count : 0);
			for (int j = 0; j < num; j++)
			{
				if (list2 == null)
				{
					list2 = new List<ResponseElement>(num);
				}
				IHttpResponseElement httpResponseElement = buffers[j] as IHttpResponseElement;
				if (httpResponseElement is HttpFileResponseElement)
				{
					HttpFileResponseElement httpFileResponseElement = httpResponseElement as HttpFileResponseElement;
					list2.Add(new FileResponseElement(httpFileResponseElement.FileName, httpFileResponseElement.Offset, httpResponseElement.GetSize()));
				}
				else if (httpResponseElement is HttpSubstBlockResponseElement)
				{
					HttpSubstBlockResponseElement httpSubstBlockResponseElement = httpResponseElement as HttpSubstBlockResponseElement;
					list2.Add(new SubstitutionResponseElement(httpSubstBlockResponseElement.Callback));
				}
				else
				{
					byte[] bytes = httpResponseElement.GetBytes();
					long length = (long)((bytes != null) ? bytes.Length : 0);
					list2.Add(new MemoryResponseElement(bytes, length));
				}
			}
			return new OutputCacheEntry(cachedRawResponse._cachedVaryId, cachedRawResponse._settings, cachedRawResponse._kernelCacheUrl, depKey, fileDependencies, cachedRawResponse._rawResponse.StatusCode, cachedRawResponse._rawResponse.StatusDescription, list, list2);
		}

		// Token: 0x060066B1 RID: 26289 RVA: 0x00169C8C File Offset: 0x00167E8C
		private static CachedRawResponse Convert(OutputCacheEntry oce)
		{
			ArrayList arrayList = null;
			if (oce.HeaderElements != null && oce.HeaderElements.Count > 0)
			{
				arrayList = new ArrayList(oce.HeaderElements.Count);
				for (int i = 0; i < oce.HeaderElements.Count; i++)
				{
					HttpResponseHeader value = new HttpResponseHeader(oce.HeaderElements[i].Name, oce.HeaderElements[i].Value);
					arrayList.Add(value);
				}
			}
			ArrayList arrayList2;
			if (oce.ResponseElements != null && oce.ResponseElements.Count > 0)
			{
				arrayList2 = new ArrayList(oce.ResponseElements.Count);
				for (int j = 0; j < oce.ResponseElements.Count; j++)
				{
					ResponseElement responseElement = oce.ResponseElements[j];
					IHttpResponseElement value2;
					if (responseElement is FileResponseElement)
					{
						HttpContext httpContext = HttpContext.Current;
						HttpWorkerRequest httpWorkerRequest = (httpContext != null) ? httpContext.WorkerRequest : null;
						bool supportsLongTransmitFile = httpWorkerRequest != null && httpWorkerRequest.SupportsLongTransmitFile;
						bool isImpersonating = (httpContext != null && httpContext.IsClientImpersonationConfigured) || HttpRuntime.IsOnUNCShareInternal;
						FileResponseElement fileResponseElement = (FileResponseElement)responseElement;
						HttpRuntime.CheckFilePermission(fileResponseElement.Path);
						value2 = new HttpFileResponseElement(fileResponseElement.Path, fileResponseElement.Offset, fileResponseElement.Length, isImpersonating, supportsLongTransmitFile);
					}
					else if (responseElement is MemoryResponseElement)
					{
						MemoryResponseElement memoryResponseElement = (MemoryResponseElement)responseElement;
						int size = System.Convert.ToInt32(memoryResponseElement.Length);
						value2 = new HttpResponseBufferElement(memoryResponseElement.Buffer, size);
					}
					else
					{
						if (!(responseElement is SubstitutionResponseElement))
						{
							throw new NotSupportedException();
						}
						SubstitutionResponseElement substitutionResponseElement = (SubstitutionResponseElement)responseElement;
						value2 = new HttpSubstBlockResponseElement(substitutionResponseElement.Callback);
					}
					arrayList2.Add(value2);
				}
			}
			else
			{
				arrayList2 = new ArrayList();
			}
			HttpRawResponse rawResponse = new HttpRawResponse(oce.StatusCode, oce.StatusDescription, arrayList, arrayList2, false);
			return new CachedRawResponse(rawResponse, oce.Settings, oce.KernelCacheUrl, oce.CachedVaryId);
		}

		// Token: 0x060066B2 RID: 26290 RVA: 0x00169E89 File Offset: 0x00168089
		private static CachedVary UtcAdd(string key, CachedVary cachedVary)
		{
			return (CachedVary)HttpRuntime.Cache.InternalCache.Add(key, cachedVary, null);
		}

		// Token: 0x060066B3 RID: 26291 RVA: 0x00169EA2 File Offset: 0x001680A2
		private static ControlCachedVary UtcAdd(string key, ControlCachedVary cachedVary)
		{
			return (ControlCachedVary)HttpRuntime.Cache.InternalCache.Add(key, cachedVary, null);
		}

		// Token: 0x060066B4 RID: 26292 RVA: 0x00169EBC File Offset: 0x001680BC
		private static bool IsSubstBlockSerializable(HttpRawResponse rawResponse)
		{
			if (!rawResponse.HasSubstBlocks)
			{
				return true;
			}
			for (int i = 0; i < rawResponse.Buffers.Count; i++)
			{
				HttpSubstBlockResponseElement httpSubstBlockResponseElement = rawResponse.Buffers[i] as HttpSubstBlockResponseElement;
				if (httpSubstBlockResponseElement != null && !httpSubstBlockResponseElement.Callback.Method.IsStatic)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060066B5 RID: 26293 RVA: 0x00169F14 File Offset: 0x00168114
		private static void HandleErrorWithoutContext(Exception e)
		{
			HttpApplicationFactory.RaiseError(e);
			try
			{
				WebBaseEvent.RaiseRuntimeError(e, typeof(OutputCache));
			}
			catch
			{
			}
		}

		// Token: 0x060066B6 RID: 26294 RVA: 0x00169F4C File Offset: 0x0016814C
		private static void DependencyRemovedCallback(string key, object value, CacheItemRemovedReason reason)
		{
			DependencyCacheEntry dependencyCacheEntry = value as DependencyCacheEntry;
			if (dependencyCacheEntry.KernelCacheEntryKey != null)
			{
				if (HttpRuntime.UseIntegratedPipeline)
				{
					UnsafeIISMethods.MgdFlushKernelCache(dependencyCacheEntry.KernelCacheEntryKey);
				}
				else
				{
					UnsafeNativeMethods.InvalidateKernelCache(dependencyCacheEntry.KernelCacheEntryKey);
				}
			}
			if (reason == CacheItemRemovedReason.DependencyChanged && dependencyCacheEntry.OutputCacheEntryKey != null)
			{
				try
				{
					OutputCache.RemoveFromProvider(dependencyCacheEntry.OutputCacheEntryKey, dependencyCacheEntry.ProviderName);
				}
				catch (Exception e)
				{
					OutputCache.HandleErrorWithoutContext(e);
				}
			}
		}

		// Token: 0x060066B7 RID: 26295 RVA: 0x00169FC0 File Offset: 0x001681C0
		private static void DependencyRemovedCallbackForFragment(string key, object value, CacheItemRemovedReason reason)
		{
			if (reason == CacheItemRemovedReason.DependencyChanged)
			{
				DependencyCacheEntry dependencyCacheEntry = value as DependencyCacheEntry;
				if (dependencyCacheEntry.OutputCacheEntryKey != null)
				{
					try
					{
						OutputCache.RemoveFragment(dependencyCacheEntry.OutputCacheEntryKey, dependencyCacheEntry.ProviderName);
					}
					catch (Exception e)
					{
						OutputCache.HandleErrorWithoutContext(e);
					}
				}
			}
		}

		// Token: 0x060066B8 RID: 26296 RVA: 0x0016A00C File Offset: 0x0016820C
		private static void EntryRemovedCallback(string key, object value, CacheItemRemovedReason reason)
		{
			OutputCache.DecrementCount();
			PerfCounters.DecrementCounter(AppPerfCounter.OUTPUT_CACHE_ENTRIES);
			PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_TURNOVER_RATE);
			CachedRawResponse cachedRawResponse = value as CachedRawResponse;
			if (cachedRawResponse != null)
			{
				string kernelCacheUrl = cachedRawResponse._kernelCacheUrl;
				if (kernelCacheUrl != null && HttpRuntime.Cache.InternalCache.Get(key) == null)
				{
					if (HttpRuntime.UseIntegratedPipeline)
					{
						UnsafeIISMethods.MgdFlushKernelCache(kernelCacheUrl);
						return;
					}
					UnsafeNativeMethods.InvalidateKernelCache(kernelCacheUrl);
				}
			}
		}

		// Token: 0x17001CBF RID: 7359
		// (get) Token: 0x060066B9 RID: 26297 RVA: 0x0016A067 File Offset: 0x00168267
		public static string DefaultProviderName
		{
			get
			{
				OutputCache.EnsureInitialized();
				if (OutputCache.s_defaultProvider == null)
				{
					return "AspNetInternalProvider";
				}
				return OutputCache.s_defaultProvider.Name;
			}
		}

		// Token: 0x17001CC0 RID: 7360
		// (get) Token: 0x060066BA RID: 26298 RVA: 0x0016A085 File Offset: 0x00168285
		public static OutputCacheProviderCollection Providers
		{
			get
			{
				OutputCache.EnsureInitialized();
				return OutputCache.s_providers;
			}
		}

		// Token: 0x17001CC1 RID: 7361
		// (get) Token: 0x060066BB RID: 26299 RVA: 0x0016A091 File Offset: 0x00168291
		internal static bool InUse
		{
			get
			{
				return OutputCache.Providers != null || OutputCache.s_cEntries != 0;
			}
		}

		// Token: 0x060066BC RID: 26300 RVA: 0x0016A0A4 File Offset: 0x001682A4
		internal static void ThrowIfProviderNotFound(string providerName)
		{
			if (providerName == null)
			{
				return;
			}
			OutputCacheProviderCollection providers = OutputCache.Providers;
			if (providers == null || providers[providerName] == null)
			{
				throw new ProviderException(SR.GetString("Provider_Not_Found", new object[]
				{
					providerName
				}));
			}
		}

		// Token: 0x060066BD RID: 26301 RVA: 0x0016A0E4 File Offset: 0x001682E4
		internal static bool HasDependencyChanged(bool isFragment, string depKey, string[] fileDeps, string kernelKey, string oceKey, string providerName)
		{
			if (depKey == null)
			{
				return false;
			}
			if (HttpRuntime.Cache.InternalCache.Get(depKey) != null)
			{
				return false;
			}
			CacheDependency cacheDependency = new CacheDependency(0, fileDeps);
			int length = "aD".Length;
			int length2 = depKey.Length - length;
			CacheItemRemovedCallback onRemovedCallback = isFragment ? OutputCache.s_dependencyRemovedCallbackForFragment : OutputCache.s_dependencyRemovedCallback;
			if (string.Compare(cacheDependency.GetUniqueID(), 0, depKey, length, length2, StringComparison.Ordinal) == 0)
			{
				HttpRuntime.Cache.InternalCache.Insert(depKey, new DependencyCacheEntry(oceKey, kernelKey, providerName), new CacheInsertOptions
				{
					Dependencies = cacheDependency,
					OnRemovedCallback = onRemovedCallback
				});
				return false;
			}
			cacheDependency.Dispose();
			return true;
		}

		// Token: 0x060066BE RID: 26302 RVA: 0x0016A180 File Offset: 0x00168380
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public static void Serialize(Stream stream, object data)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			if (data is OutputCacheEntry || data is PartialCachingCacheEntry || data is CachedVary || data is ControlCachedVary || data is FileResponseElement || data is MemoryResponseElement || data is SubstitutionResponseElement)
			{
				binaryFormatter.Serialize(stream, data);
				return;
			}
			throw new ArgumentException(SR.GetString("OutputCacheExtensibility_CantSerializeDeserializeType"));
		}

		// Token: 0x060066BF RID: 26303 RVA: 0x0016A1E4 File Offset: 0x001683E4
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public static object Deserialize(Stream stream)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			object obj = binaryFormatter.Deserialize(stream);
			if (!(obj is OutputCacheEntry) && !(obj is PartialCachingCacheEntry) && !(obj is CachedVary) && !(obj is ControlCachedVary) && !(obj is FileResponseElement) && !(obj is MemoryResponseElement) && !(obj is SubstitutionResponseElement))
			{
				throw new ArgumentException(SR.GetString("OutputCacheExtensibility_CantSerializeDeserializeType"));
			}
			return obj;
		}

		// Token: 0x060066C0 RID: 26304 RVA: 0x0016A248 File Offset: 0x00168448
		internal static object Get(string key)
		{
			object obj = null;
			OutputCacheProvider provider = OutputCache.GetProvider(HttpContext.Current);
			if (provider != null)
			{
				obj = provider.Get(key);
				OutputCacheEntry outputCacheEntry = obj as OutputCacheEntry;
				if (outputCacheEntry != null)
				{
					if (OutputCache.HasDependencyChanged(false, outputCacheEntry.DependenciesKey, outputCacheEntry.Dependencies, outputCacheEntry.KernelCacheUrl, key, provider.Name))
					{
						OutputCache.RemoveFromProvider(key, provider.Name);
						return null;
					}
					obj = OutputCache.Convert(outputCacheEntry);
				}
			}
			if (obj == null)
			{
				obj = HttpRuntime.Cache.InternalCache.Get(key);
			}
			return obj;
		}

		// Token: 0x060066C1 RID: 26305 RVA: 0x0016A2C4 File Offset: 0x001684C4
		internal static object GetFragment(string key, string providerName)
		{
			object obj = null;
			OutputCacheProvider fragmentProvider = OutputCache.GetFragmentProvider(providerName);
			if (fragmentProvider != null)
			{
				obj = fragmentProvider.Get(key);
				PartialCachingCacheEntry partialCachingCacheEntry = obj as PartialCachingCacheEntry;
				if (partialCachingCacheEntry != null && OutputCache.HasDependencyChanged(true, partialCachingCacheEntry._dependenciesKey, partialCachingCacheEntry._dependencies, null, key, fragmentProvider.Name))
				{
					OutputCache.RemoveFragment(key, fragmentProvider.Name);
					return null;
				}
			}
			if (obj == null)
			{
				obj = HttpRuntime.Cache.InternalCache.Get(key);
			}
			return obj;
		}

		// Token: 0x060066C2 RID: 26306 RVA: 0x0016A330 File Offset: 0x00168530
		internal static void Remove(string key, HttpContext context)
		{
			HttpRuntime.Cache.InternalCache.Remove(key);
			if (context == null)
			{
				OutputCacheProviderCollection providers = OutputCache.Providers;
				if (providers == null)
				{
					return;
				}
				using (IEnumerator enumerator = providers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						OutputCacheProvider outputCacheProvider = (OutputCacheProvider)obj;
						outputCacheProvider.Remove(key);
					}
					return;
				}
			}
			OutputCacheProvider provider = OutputCache.GetProvider(context);
			if (provider != null)
			{
				provider.Remove(key);
			}
		}

		// Token: 0x060066C3 RID: 26307 RVA: 0x0016A3B8 File Offset: 0x001685B8
		internal static void RemoveFromProvider(string key, string providerName)
		{
			if (providerName == null)
			{
				throw new ArgumentNullException("providerName");
			}
			OutputCacheProviderCollection providers = OutputCache.Providers;
			OutputCacheProvider outputCacheProvider = (providers == null) ? null : providers[providerName];
			if (outputCacheProvider == null)
			{
				throw new ProviderException(SR.GetString("Provider_Not_Found", new object[]
				{
					providerName
				}));
			}
			outputCacheProvider.Remove(key);
		}

		// Token: 0x060066C4 RID: 26308 RVA: 0x0016A40C File Offset: 0x0016860C
		internal static void RemoveFragment(string key, string providerName)
		{
			OutputCacheProvider fragmentProvider = OutputCache.GetFragmentProvider(providerName);
			if (fragmentProvider != null)
			{
				fragmentProvider.Remove(key);
			}
			HttpRuntime.Cache.InternalCache.Remove(key);
		}

		// Token: 0x060066C5 RID: 26309 RVA: 0x0016A43C File Offset: 0x0016863C
		internal static void InsertFragment(string cachedVaryKey, ControlCachedVary cachedVary, string fragmentKey, PartialCachingCacheEntry fragment, CacheDependency dependencies, DateTime absExp, TimeSpan slidingExp, string providerName)
		{
			OutputCacheProvider fragmentProvider = OutputCache.GetFragmentProvider(providerName);
			bool flag = fragmentProvider != null;
			if (flag)
			{
				bool flag2 = slidingExp == Cache.NoSlidingExpiration && (dependencies == null || dependencies.IsFileDependency());
				if (flag && !flag2)
				{
					throw new ProviderException(SR.GetString("Provider_does_not_support_policy_for_fragments", new object[]
					{
						providerName
					}));
				}
			}
			if (cachedVary != null)
			{
				ControlCachedVary controlCachedVary;
				if (!flag)
				{
					controlCachedVary = OutputCache.UtcAdd(cachedVaryKey, cachedVary);
				}
				else
				{
					controlCachedVary = (ControlCachedVary)fragmentProvider.Add(cachedVaryKey, cachedVary, Cache.NoAbsoluteExpiration);
				}
				if (controlCachedVary != null)
				{
					if (!cachedVary.Equals(controlCachedVary))
					{
						if (!flag)
						{
							HttpRuntime.Cache.InternalCache.Insert(cachedVaryKey, cachedVary, null);
						}
						else
						{
							fragmentProvider.Set(cachedVaryKey, cachedVary, Cache.NoAbsoluteExpiration);
						}
					}
					else
					{
						cachedVary = controlCachedVary;
					}
				}
				if (!flag)
				{
					OutputCache.AddCacheKeyToDependencies(ref dependencies, cachedVaryKey);
				}
				fragment._cachedVaryId = cachedVary.CachedVaryId;
			}
			if (!flag)
			{
				HttpRuntime.Cache.InternalCache.Insert(fragmentKey, fragment, new CacheInsertOptions
				{
					Dependencies = dependencies,
					AbsoluteExpiration = absExp,
					SlidingExpiration = slidingExp
				});
				return;
			}
			string text = null;
			if (dependencies != null)
			{
				text = "aD" + dependencies.GetUniqueID();
				fragment._dependenciesKey = text;
				fragment._dependencies = dependencies.GetFileDependencies();
			}
			fragmentProvider.Set(fragmentKey, fragment, absExp);
			if (dependencies != null)
			{
				object obj = HttpRuntime.Cache.InternalCache.Add(text, new DependencyCacheEntry(fragmentKey, null, fragmentProvider.Name), new CacheInsertOptions
				{
					Dependencies = dependencies,
					AbsoluteExpiration = absExp,
					OnRemovedCallback = OutputCache.s_dependencyRemovedCallbackForFragment
				});
				if (obj != null)
				{
					dependencies.Dispose();
				}
			}
		}

		// Token: 0x060066C6 RID: 26310 RVA: 0x0016A5C4 File Offset: 0x001687C4
		internal static void InsertResponse(string cachedVaryKey, CachedVary cachedVary, string rawResponseKey, CachedRawResponse rawResponse, CacheDependency dependencies, DateTime absExp, TimeSpan slidingExp)
		{
			OutputCacheProvider provider = OutputCache.GetProvider(HttpContext.Current);
			bool flag = provider != null;
			if (flag)
			{
				bool flag2 = OutputCache.IsSubstBlockSerializable(rawResponse._rawResponse) && rawResponse._settings.IsValidationCallbackSerializable() && slidingExp == Cache.NoSlidingExpiration && (dependencies == null || dependencies.IsFileDependency());
				if (flag && !flag2)
				{
					throw new ProviderException(SR.GetString("Provider_does_not_support_policy_for_responses", new object[]
					{
						provider.Name
					}));
				}
			}
			if (cachedVary != null)
			{
				CachedVary cachedVary2;
				if (!flag)
				{
					cachedVary2 = OutputCache.UtcAdd(cachedVaryKey, cachedVary);
				}
				else
				{
					cachedVary2 = (CachedVary)provider.Add(cachedVaryKey, cachedVary, Cache.NoAbsoluteExpiration);
				}
				if (cachedVary2 != null)
				{
					if (!cachedVary.Equals(cachedVary2))
					{
						if (!flag)
						{
							HttpRuntime.Cache.InternalCache.Insert(cachedVaryKey, cachedVary, null);
						}
						else
						{
							provider.Set(cachedVaryKey, cachedVary, Cache.NoAbsoluteExpiration);
						}
					}
					else
					{
						cachedVary = cachedVary2;
					}
				}
				if (!flag)
				{
					OutputCache.AddCacheKeyToDependencies(ref dependencies, cachedVaryKey);
				}
				rawResponse._cachedVaryId = cachedVary.CachedVaryId;
			}
			if (!flag)
			{
				HttpRuntime.Cache.InternalCache.Insert(rawResponseKey, rawResponse, new CacheInsertOptions
				{
					Dependencies = dependencies,
					AbsoluteExpiration = absExp,
					SlidingExpiration = slidingExp,
					OnRemovedCallback = OutputCache.s_entryRemovedCallback
				});
				OutputCache.IncrementCount();
				PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_ENTRIES);
				PerfCounters.IncrementCounter(AppPerfCounter.OUTPUT_CACHE_TURNOVER_RATE);
				return;
			}
			string text = null;
			string[] fileDependencies = null;
			if (dependencies != null)
			{
				text = "aD" + dependencies.GetUniqueID();
				fileDependencies = dependencies.GetFileDependencies();
			}
			OutputCacheEntry outputCacheEntry = OutputCache.Convert(rawResponse, text, fileDependencies);
			provider.Set(rawResponseKey, outputCacheEntry, absExp);
			if (dependencies != null)
			{
				object obj = HttpRuntime.Cache.InternalCache.Add(text, new DependencyCacheEntry(rawResponseKey, outputCacheEntry.KernelCacheUrl, provider.Name), new CacheInsertOptions
				{
					Dependencies = dependencies,
					AbsoluteExpiration = absExp,
					OnRemovedCallback = OutputCache.s_dependencyRemovedCallbackForFragment
				});
				if (obj != null)
				{
					dependencies.Dispose();
				}
			}
		}

		// Token: 0x040034F2 RID: 13554
		private const string OUTPUTCACHE_KEYPREFIX_DEPENDENCIES = "aD";

		// Token: 0x040034F3 RID: 13555
		internal const string ASPNET_INTERNAL_PROVIDER_NAME = "AspNetInternalProvider";

		// Token: 0x040034F4 RID: 13556
		private static bool s_inited;

		// Token: 0x040034F5 RID: 13557
		private static object s_initLock = new object();

		// Token: 0x040034F6 RID: 13558
		private static CacheItemRemovedCallback s_entryRemovedCallback;

		// Token: 0x040034F7 RID: 13559
		private static CacheItemRemovedCallback s_dependencyRemovedCallback;

		// Token: 0x040034F8 RID: 13560
		private static CacheItemRemovedCallback s_dependencyRemovedCallbackForFragment;

		// Token: 0x040034F9 RID: 13561
		private static OutputCacheProvider s_defaultProvider;

		// Token: 0x040034FA RID: 13562
		private static OutputCacheProviderCollection s_providers;

		// Token: 0x040034FB RID: 13563
		private static int s_cEntries;
	}
}
