using System;

namespace System.Windows.Forms
{
	// Token: 0x0200043B RID: 1083
	public class WebBrowserNavigatedEventArgs : EventArgs
	{
		// Token: 0x06004B7F RID: 19327 RVA: 0x0013A869 File Offset: 0x00138A69
		public WebBrowserNavigatedEventArgs(Uri url)
		{
			this.url = url;
		}

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06004B80 RID: 19328 RVA: 0x0013A878 File Offset: 0x00138A78
		public Uri Url
		{
			get
			{
				WebBrowser.EnsureUrlConnectPermission(this.url);
				return this.url;
			}
		}

		// Token: 0x04002838 RID: 10296
		private Uri url;
	}
}
