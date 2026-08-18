using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000071 RID: 113
	internal class EntryPatchData
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x000170B6 File Offset: 0x000160B6
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x000170BE File Offset: 0x000160BE
		public long SizePatchOffset
		{
			get
			{
				return this.sizePatchOffset_;
			}
			set
			{
				this.sizePatchOffset_ = value;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x000170C7 File Offset: 0x000160C7
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x000170CF File Offset: 0x000160CF
		public long CrcPatchOffset
		{
			get
			{
				return this.crcPatchOffset_;
			}
			set
			{
				this.crcPatchOffset_ = value;
			}
		}

		// Token: 0x040002E7 RID: 743
		private long sizePatchOffset_;

		// Token: 0x040002E8 RID: 744
		private long crcPatchOffset_;
	}
}
