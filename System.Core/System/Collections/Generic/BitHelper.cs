using System;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x02000094 RID: 148
	internal class BitHelper
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x0000A16E File Offset: 0x0000836E
		[SecurityCritical]
		internal unsafe BitHelper(int* bitArrayPtr, int length)
		{
			this.m_arrayPtr = bitArrayPtr;
			this.m_length = length;
			this.useStackAlloc = true;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000A18B File Offset: 0x0000838B
		internal BitHelper(int[] bitArray, int length)
		{
			this.m_array = bitArray;
			this.m_length = length;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000A1A4 File Offset: 0x000083A4
		[SecuritySafeCritical]
		internal unsafe void MarkBit(int bitPosition)
		{
			if (this.useStackAlloc)
			{
				int num = bitPosition / 32;
				if (num < this.m_length && num >= 0)
				{
					this.m_arrayPtr[num] |= 1 << bitPosition % 32;
					return;
				}
			}
			else
			{
				int num2 = bitPosition / 32;
				if (num2 < this.m_length && num2 >= 0)
				{
					this.m_array[num2] |= 1 << bitPosition % 32;
				}
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000A210 File Offset: 0x00008410
		[SecuritySafeCritical]
		internal unsafe bool IsMarked(int bitPosition)
		{
			if (this.useStackAlloc)
			{
				int num = bitPosition / 32;
				return num < this.m_length && num >= 0 && (this.m_arrayPtr[num] & 1 << bitPosition % 32) != 0;
			}
			int num2 = bitPosition / 32;
			return num2 < this.m_length && num2 >= 0 && (this.m_array[num2] & 1 << bitPosition % 32) != 0;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000A27C File Offset: 0x0000847C
		internal static int ToIntArrayLength(int n)
		{
			if (n <= 0)
			{
				return 0;
			}
			return (n - 1) / 32 + 1;
		}

		// Token: 0x040004C6 RID: 1222
		private const byte MarkedBitFlag = 1;

		// Token: 0x040004C7 RID: 1223
		private const byte IntSize = 32;

		// Token: 0x040004C8 RID: 1224
		private int m_length;

		// Token: 0x040004C9 RID: 1225
		[SecurityCritical]
		private unsafe int* m_arrayPtr;

		// Token: 0x040004CA RID: 1226
		private int[] m_array;

		// Token: 0x040004CB RID: 1227
		private bool useStackAlloc;
	}
}
