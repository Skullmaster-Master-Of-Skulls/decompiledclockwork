using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200043D RID: 1085
	public class WebBrowserNavigatingEventArgs : CancelEventArgs
	{
		// Token: 0x06004B85 RID: 19333 RVA: 0x0013A88B File Offset: 0x00138A8B
		public WebBrowserNavigatingEventArgs(Uri url, string targetFrameName)
		{
			this.url = url;
			this.targetFrameName = targetFrameName;
		}

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06004B86 RID: 19334 RVA: 0x0013A8A1 File Offset: 0x00138AA1
		public Uri Url
		{
			get
			{
				WebBrowser.EnsureUrlConnectPermission(this.url);
				return this.url;
			}
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06004B87 RID: 19335 RVA: 0x0013A8B4 File Offset: 0x00138AB4
		public string TargetFrameName
		{
			get
			{
				WebBrowser.EnsureUrlConnectPermission(this.url);
				return this.targetFrameName;
			}
		}

		// Token: 0x04002839 RID: 10297
		private Uri url;

		// Token: 0x0400283A RID: 10298
		private string targetFrameName;
	}
}
