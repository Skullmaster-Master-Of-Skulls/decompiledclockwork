using System;
using System.Collections.Generic;

namespace System.Web.WebPages.Scope
{
	// Token: 0x02000076 RID: 118
	public interface IScopeStorageProvider
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600038B RID: 907
		// (set) Token: 0x0600038C RID: 908
		IDictionary<object, object> CurrentScope { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600038D RID: 909
		IDictionary<object, object> GlobalScope { get; }
	}
}
