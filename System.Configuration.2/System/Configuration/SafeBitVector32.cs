using System;
using System.Threading;

namespace System.Configuration
{
	// Token: 0x02000086 RID: 134
	[Serializable]
	internal struct SafeBitVector32
	{
		// Token: 0x0600050E RID: 1294 RVA: 0x0001A665 File Offset: 0x00018865
		internal SafeBitVector32(int data)
		{
			this._data = data;
		}

		// Token: 0x1700017D RID: 381
		internal bool this[int bit]
		{
			get
			{
				int data = this._data;
				return (data & bit) == bit;
			}
			set
			{
				int data;
				int num;
				do
				{
					data = this._data;
					int value2;
					if (value)
					{
						value2 = (data | bit);
					}
					else
					{
						value2 = (data & ~bit);
					}
					num = Interlocked.CompareExchange(ref this._data, value2, data);
				}
				while (num != data);
			}
		}

		// Token: 0x040002E9 RID: 745
		private volatile int _data;
	}
}
