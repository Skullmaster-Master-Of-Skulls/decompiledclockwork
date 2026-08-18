using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x0200005D RID: 93
	public class ProgressEventArgs : EventArgs
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x0001622D File Offset: 0x0001522D
		public ProgressEventArgs(string name, long processed, long target)
		{
			this.name_ = name;
			this.processed_ = processed;
			this.target_ = target;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00016251 File Offset: 0x00015251
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00016259 File Offset: 0x00015259
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x00016261 File Offset: 0x00015261
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0001626C File Offset: 0x0001526C
		public float PercentComplete
		{
			get
			{
				float result;
				if (this.target_ <= 0L)
				{
					result = 0f;
				}
				else
				{
					result = (float)this.processed_ / (float)this.target_ * 100f;
				}
				return result;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000162A2 File Offset: 0x000152A2
		public long Processed
		{
			get
			{
				return this.processed_;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x000162AA File Offset: 0x000152AA
		public long Target
		{
			get
			{
				return this.target_;
			}
		}

		// Token: 0x040002C8 RID: 712
		private string name_;

		// Token: 0x040002C9 RID: 713
		private long processed_;

		// Token: 0x040002CA RID: 714
		private long target_;

		// Token: 0x040002CB RID: 715
		private bool continueRunning_ = true;
	}
}
