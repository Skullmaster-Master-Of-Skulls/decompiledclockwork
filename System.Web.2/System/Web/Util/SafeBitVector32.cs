using System;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x0200021C RID: 540
	[Serializable]
	internal struct SafeBitVector32
	{
		// Token: 0x06001A06 RID: 6662 RVA: 0x00051594 File Offset: 0x0004F794
		internal SafeBitVector32(int data)
		{
			this._data = data;
		}

		// Token: 0x17000770 RID: 1904
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

		// Token: 0x06001A09 RID: 6665 RVA: 0x000515F4 File Offset: 0x0004F7F4
		internal bool ChangeValue(int bit, bool value)
		{
			for (;;)
			{
				int data = this._data;
				int num;
				if (value)
				{
					num = (data | bit);
				}
				else
				{
					num = (data & ~bit);
				}
				if (data == num)
				{
					break;
				}
				int num2 = Interlocked.CompareExchange(ref this._data, num, data);
				if (num2 == data)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400180D RID: 6157
		private volatile int _data;
	}
}
