using System;

namespace System.Net.Http.Headers
{
	// Token: 0x02000026 RID: 38
	internal class CacheControlHeaderParser : BaseHeaderParser
	{
		// Token: 0x060001AB RID: 427 RVA: 0x00007138 File Offset: 0x00005338
		private CacheControlHeaderParser() : base(true)
		{
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007144 File Offset: 0x00005344
		protected override int GetParsedValueLength(string value, int startIndex, object storeValue, out object parsedValue)
		{
			CacheControlHeaderValue cacheControlHeaderValue = storeValue as CacheControlHeaderValue;
			int cacheControlLength = CacheControlHeaderValue.GetCacheControlLength(value, startIndex, cacheControlHeaderValue, out cacheControlHeaderValue);
			parsedValue = cacheControlHeaderValue;
			return cacheControlLength;
		}

		// Token: 0x040000D8 RID: 216
		internal static readonly CacheControlHeaderParser Parser = new CacheControlHeaderParser();
	}
}
