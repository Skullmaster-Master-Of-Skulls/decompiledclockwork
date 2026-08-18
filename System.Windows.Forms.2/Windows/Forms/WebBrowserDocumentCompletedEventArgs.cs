using System;

namespace System.Windows.Forms
{
	// Token: 0x02000438 RID: 1080
	public class WebBrowserDocumentCompletedEventArgs : EventArgs
	{
		// Token: 0x06004B70 RID: 19312 RVA: 0x0013A621 File Offset: 0x00138821
		public WebBrowserDocumentCompletedEventArgs(Uri url)
		{
			this.url = url;
		}

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06004B71 RID: 19313 RVA: 0x0013A630 File Offset: 0x00138830
		public Uri Url
		{
			get
			{
				WebBrowser.EnsureUrlConnectPermission(this.url);
				return this.url;
			}
		}

		// Token: 0x04002825 RID: 10277
		private Uri url;
	}
}
