using System;

namespace System.Web
{
	// Token: 0x02000088 RID: 136
	internal sealed class ValidationCallbackInfo
	{
		// Token: 0x0600081A RID: 2074 RVA: 0x000114E0 File Offset: 0x0000F6E0
		internal ValidationCallbackInfo(HttpCacheValidateHandler handler, object data)
		{
			this.handler = handler;
			this.data = data;
		}

		// Token: 0x040002C0 RID: 704
		internal readonly HttpCacheValidateHandler handler;

		// Token: 0x040002C1 RID: 705
		internal readonly object data;
	}
}
