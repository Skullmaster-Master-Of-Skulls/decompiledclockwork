using System;

namespace System.Web.Util
{
	// Token: 0x0200021F RID: 543
	[Serializable]
	internal struct SimpleBitVector32
	{
		// Token: 0x06001A16 RID: 6678 RVA: 0x00051CA4 File Offset: 0x0004FEA4
		internal SimpleBitVector32(int data)
		{
			this.data = data;
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001A17 RID: 6679 RVA: 0x00051CAD File Offset: 0x0004FEAD
		// (set) Token: 0x06001A18 RID: 6680 RVA: 0x00051CA4 File Offset: 0x0004FEA4
		internal int IntegerValue
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x17000772 RID: 1906
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

		// Token: 0x17000773 RID: 1907
		internal int this[int mask, int offset]
		{
			get
			{
				return (this.data & mask) >> offset;
			}
			set
			{
				this.data = ((this.data & ~mask) | value << offset);
			}
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x00051D16 File Offset: 0x0004FF16
		internal void Set(int bit)
		{
			this.data |= bit;
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x00051D26 File Offset: 0x0004FF26
		internal void Clear(int bit)
		{
			this.data &= ~bit;
		}

		// Token: 0x04001815 RID: 6165
		private int data;
	}
}
