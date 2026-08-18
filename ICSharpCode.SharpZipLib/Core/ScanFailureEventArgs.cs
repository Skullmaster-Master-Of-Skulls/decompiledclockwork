using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x0200005F RID: 95
	public class ScanFailureEventArgs : EventArgs
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x000162CA File Offset: 0x000152CA
		public ScanFailureEventArgs(string name, Exception e)
		{
			this.name_ = name;
			this.exception_ = e;
			this.continueRunning_ = true;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x000162E7 File Offset: 0x000152E7
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x000162EF File Offset: 0x000152EF
		public Exception Exception
		{
			get
			{
				return this.exception_;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000162F7 File Offset: 0x000152F7
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x000162FF File Offset: 0x000152FF
		public bool ContinueRunning
		{
			get
			{
				return this.continueRunning_;
			}
			set
			{
				this.continueRunning_ = value;
			}
		}

		// Token: 0x040002CD RID: 717
		private string name_;

		// Token: 0x040002CE RID: 718
		private Exception exception_;

		// Token: 0x040002CF RID: 719
		private bool continueRunning_;
	}
}
