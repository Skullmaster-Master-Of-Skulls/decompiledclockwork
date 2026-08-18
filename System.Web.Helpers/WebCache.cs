using System;
using System.Globalization;
using System.Runtime.Caching;
using System.Runtime.CompilerServices;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Helpers
{
	// Token: 0x02000016 RID: 22
	public static class WebCache
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x0000545C File Offset: 0x0000365C
		public static void Set(string key, object value, int minutesToCache = 20, bool slidingExpiration = true)
		{
			if (minutesToCache <= 0)
			{
				throw new ArgumentOutOfRangeException("minutesToCache", string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThan, new object[]
				{
					0
				}));
			}
			if (slidingExpiration && minutesToCache > 525600)
			{
				throw new ArgumentOutOfRangeException("minutesToCache", string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_LessThanOrEqualTo, new object[]
				{
					525600
				}));
			}
			CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
			TimeSpan slidingExpiration2 = new TimeSpan(0, minutesToCache, 0);
			if (slidingExpiration)
			{
				cacheItemPolicy.SlidingExpiration = slidingExpiration2;
			}
			else
			{
				cacheItemPolicy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes((double)minutesToCache);
			}
			MemoryCache.Default.Set(key, value, cacheItemPolicy, null);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005512 File Offset: 0x00003712
		[return: Dynamic]
		public static dynamic Get(string key)
		{
			return MemoryCache.Default.Get(key, null);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005520 File Offset: 0x00003720
		[return: Dynamic]
		public static dynamic Remove(string key)
		{
			return MemoryCache.Default.Remove(key, null);
		}
	}
}
