using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Hosting;

namespace System.Web.Caching
{
	// Token: 0x0200088C RID: 2188
	public static class OutputCacheUtility
	{
		// Token: 0x060066E3 RID: 26339 RVA: 0x0016A8DB File Offset: 0x00168ADB
		public static string SetupKernelCaching(string originalCacheUrl, HttpResponse response)
		{
			return response.SetupKernelCaching(originalCacheUrl);
		}

		// Token: 0x060066E4 RID: 26340 RVA: 0x0016A8E4 File Offset: 0x00168AE4
		public static void FlushKernelCache(string cacheKey)
		{
			UnsafeIISMethods.MgdFlushKernelCache(cacheKey);
		}

		// Token: 0x060066E5 RID: 26341 RVA: 0x0016A8ED File Offset: 0x00168AED
		public static CacheDependency CreateCacheDependency(HttpResponse response)
		{
			return response.CreateCacheDependencyForResponse();
		}

		// Token: 0x060066E6 RID: 26342 RVA: 0x0016A8F5 File Offset: 0x00168AF5
		public static ArrayList GetContentBuffers(HttpResponse response)
		{
			return response.GetSnapshot().Buffers;
		}

		// Token: 0x060066E7 RID: 26343 RVA: 0x0016A902 File Offset: 0x00168B02
		public static void SetContentBuffers(HttpResponse response, ArrayList buffers)
		{
			response.SetResponseBuffers(buffers);
		}

		// Token: 0x060066E8 RID: 26344 RVA: 0x0016A90C File Offset: 0x00168B0C
		public static IEnumerable<KeyValuePair<HttpCacheValidateHandler, object>> GetValidationCallbacks(HttpResponse response)
		{
			List<KeyValuePair<HttpCacheValidateHandler, object>> list = new List<KeyValuePair<HttpCacheValidateHandler, object>>();
			foreach (object obj in response.Cache.GetValidationCallbacks())
			{
				ValidationCallbackInfo validationCallbackInfo = (ValidationCallbackInfo)obj;
				list.Add(new KeyValuePair<HttpCacheValidateHandler, object>(validationCallbackInfo.handler, validationCallbackInfo.data));
			}
			return list;
		}
	}
}
