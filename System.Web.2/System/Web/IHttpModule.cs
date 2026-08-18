using System;

namespace System.Web
{
	// Token: 0x020000D1 RID: 209
	public interface IHttpModule
	{
		// Token: 0x06000DE5 RID: 3557
		void Init(HttpApplication context);

		// Token: 0x06000DE6 RID: 3558
		void Dispose();
	}
}
