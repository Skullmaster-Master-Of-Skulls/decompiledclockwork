using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace System.Web.WebPages.Scope
{
	// Token: 0x02000075 RID: 117
	internal class ApplicationScopeStorageDictionary : ScopeStorageDictionary
	{
		// Token: 0x06000388 RID: 904 RVA: 0x0000C268 File Offset: 0x0000A468
		public ApplicationScopeStorageDictionary() : this(new WebConfigScopeDictionary())
		{
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000C275 File Offset: 0x0000A475
		public ApplicationScopeStorageDictionary(WebConfigScopeDictionary webConfigState) : base(webConfigState, ApplicationScopeStorageDictionary._innerDictionary)
		{
		}

		// Token: 0x04000108 RID: 264
		private static readonly IDictionary<object, object> _innerDictionary = new ConcurrentDictionary<object, object>(ScopeStorageComparer.Instance);
	}
}
