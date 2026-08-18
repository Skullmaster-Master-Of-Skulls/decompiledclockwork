using System;

namespace System.Configuration
{
	// Token: 0x0200008C RID: 140
	[Serializable]
	internal struct SimpleBitVector32
	{
		// Token: 0x060005C5 RID: 1477 RVA: 0x0001C517 File Offset: 0x0001A717
		internal SimpleBitVector32(int data)
		{
			this.data = data;
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0001C520 File Offset: 0x0001A720
		internal int Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x170001D8 RID: 472
		internal bool this[int bit]
		{
			get
			{
				return (this.data & bit) == bit;
			}
			set
			{
				int num = this.data;
				if (value)
				{
					this.data = (num | bit);
					return;
				}
				this.data = (num & ~bit);
			}
		}

		// Token: 0x04000343 RID: 835
		private int data;
	}
}
