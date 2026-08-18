using System;

namespace System.Web.Util
{
	// Token: 0x02000781 RID: 1921
	[Serializable]
	internal struct SimpleBitVector32
	{
		// Token: 0x06005CBB RID: 23739 RVA: 0x00173E88 File Offset: 0x00172E88
		internal SimpleBitVector32(int data)
		{
			this.data = data;
		}

		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x06005CBC RID: 23740 RVA: 0x00173E91 File Offset: 0x00172E91
		// (set) Token: 0x06005CBD RID: 23741 RVA: 0x00173E99 File Offset: 0x00172E99
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

		// Token: 0x170017CD RID: 6093
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

		// Token: 0x06005CC0 RID: 23744 RVA: 0x00173EDB File Offset: 0x00172EDB
		internal void Set(int bit)
		{
			this.data |= bit;
		}

		// Token: 0x06005CC1 RID: 23745 RVA: 0x00173EEB File Offset: 0x00172EEB
		internal void Clear(int bit)
		{
			this.data &= ~bit;
		}

		// Token: 0x0400318F RID: 12687
		private int data;
	}
}
