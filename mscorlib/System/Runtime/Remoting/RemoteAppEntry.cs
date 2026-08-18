using System;

namespace System.Runtime.Remoting
{
	// Token: 0x02000762 RID: 1890
	internal class RemoteAppEntry
	{
		// Token: 0x06004340 RID: 17216 RVA: 0x000E5BEC File Offset: 0x000E4BEC
		internal RemoteAppEntry(string appName, string appURI)
		{
			this._remoteAppName = appName;
			this._remoteAppURI = appURI;
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x000E5C02 File Offset: 0x000E4C02
		internal string GetAppURI()
		{
			return this._remoteAppURI;
		}

		// Token: 0x040021CF RID: 8655
		private string _remoteAppName;

		// Token: 0x040021D0 RID: 8656
		private string _remoteAppURI;
	}
}
