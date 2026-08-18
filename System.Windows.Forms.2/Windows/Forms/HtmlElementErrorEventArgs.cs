using System;

namespace System.Windows.Forms
{
	// Token: 0x0200027E RID: 638
	public sealed class HtmlElementErrorEventArgs : EventArgs
	{
		// Token: 0x060028F3 RID: 10483 RVA: 0x000BCAD3 File Offset: 0x000BACD3
		internal HtmlElementErrorEventArgs(string description, string urlString, int lineNumber)
		{
			this.description = description;
			this.urlString = urlString;
			this.lineNumber = lineNumber;
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060028F4 RID: 10484 RVA: 0x000BCAF0 File Offset: 0x000BACF0
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060028F5 RID: 10485 RVA: 0x000BCAF8 File Offset: 0x000BACF8
		// (set) Token: 0x060028F6 RID: 10486 RVA: 0x000BCB00 File Offset: 0x000BAD00
		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x000BCB09 File Offset: 0x000BAD09
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060028F8 RID: 10488 RVA: 0x000BCB11 File Offset: 0x000BAD11
		public Uri Url
		{
			get
			{
				if (this.url == null)
				{
					this.url = new Uri(this.urlString);
				}
				return this.url;
			}
		}

		// Token: 0x040010D2 RID: 4306
		private string description;

		// Token: 0x040010D3 RID: 4307
		private string urlString;

		// Token: 0x040010D4 RID: 4308
		private Uri url;

		// Token: 0x040010D5 RID: 4309
		private int lineNumber;

		// Token: 0x040010D6 RID: 4310
		private bool handled;
	}
}
