using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x020002E9 RID: 745
	[Serializable]
	internal class PartialCachingCacheEntry
	{
		// Token: 0x04001C54 RID: 7252
		internal Guid _cachedVaryId;

		// Token: 0x04001C55 RID: 7253
		internal string _dependenciesKey;

		// Token: 0x04001C56 RID: 7254
		internal string[] _dependencies;

		// Token: 0x04001C57 RID: 7255
		internal string OutputString;

		// Token: 0x04001C58 RID: 7256
		internal string CssStyleString;

		// Token: 0x04001C59 RID: 7257
		internal ArrayList RegisteredClientCalls;
	}
}
