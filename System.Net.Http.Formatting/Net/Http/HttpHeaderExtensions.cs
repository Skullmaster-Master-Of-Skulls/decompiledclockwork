using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace System.Net.Http
{
	// Token: 0x02000058 RID: 88
	internal static class HttpHeaderExtensions
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0000CB90 File Offset: 0x0000AD90
		public static void CopyTo(this HttpContentHeaders fromHeaders, HttpContentHeaders toHeaders)
		{
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in fromHeaders)
			{
				toHeaders.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}
}
