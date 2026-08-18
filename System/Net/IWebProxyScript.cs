using System;

namespace System.Net
{
	// Token: 0x020004B4 RID: 1204
	public interface IWebProxyScript
	{
		// Token: 0x06002544 RID: 9540
		bool Load(Uri scriptLocation, string script, Type helperType);

		// Token: 0x06002545 RID: 9541
		string Run(string url, string host);

		// Token: 0x06002546 RID: 9542
		void Close();
	}
}
