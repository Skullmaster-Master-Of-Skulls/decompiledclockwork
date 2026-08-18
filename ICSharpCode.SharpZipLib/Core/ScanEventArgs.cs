using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x0200005C RID: 92
	public class ScanEventArgs : EventArgs
	{
		// Token: 0x060003E6 RID: 998 RVA: 0x000161FE File Offset: 0x000151FE
		public ScanEventArgs(string name)
		{
			this.name_ = name;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00016214 File Offset: 0x00015214
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0001621C File Offset: 0x0001521C
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00016224 File Offset: 0x00015224
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

		// Token: 0x040002C6 RID: 710
		private string name_;

		// Token: 0x040002C7 RID: 711
		private bool continueRunning_ = true;
	}
}
