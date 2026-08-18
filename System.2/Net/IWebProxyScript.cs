using System;

namespace System.Net
{
	// Token: 0x02000193 RID: 403
	public interface IWebProxyScript
	{
		// Token: 0x06000FA5 RID: 4005
		bool Load(Uri scriptLocation, string script, Type helperType);

		// Token: 0x06000FA6 RID: 4006
		string Run(string url, string host);

		// Token: 0x06000FA7 RID: 4007
		void Close();
	}
}
