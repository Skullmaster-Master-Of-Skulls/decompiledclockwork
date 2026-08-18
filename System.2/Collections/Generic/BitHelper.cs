using System;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x020003CF RID: 975
	internal class BitHelper
	{
		// Token: 0x06002553 RID: 9555 RVA: 0x000ADC26 File Offset: 0x000ABE26
		[SecurityCritical]
		internal unsafe BitHelper(int* bitArrayPtr, int length)
		{
			this.m_arrayPtr = bitArrayPtr;
			this.m_length = length;
			this.useStackAlloc = true;
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000ADC43 File Offset: 0x000ABE43
		internal BitHelper(int[] bitArray, int length)
		{
			this.m_array = bitArray;
			this.m_length = length;
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x000ADC5C File Offset: 0x000ABE5C
		[SecurityCritical]
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

		// Token: 0x06002556 RID: 9558 RVA: 0x000ADCC8 File Offset: 0x000ABEC8
		[SecurityCritical]
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

		// Token: 0x06002557 RID: 9559 RVA: 0x000ADD34 File Offset: 0x000ABF34
		internal static int ToIntArrayLength(int n)
		{
			if (n <= 0)
			{
				return 0;
			}
			return (n - 1) / 32 + 1;
		}

		// Token: 0x0400204F RID: 8271
		private const byte MarkedBitFlag = 1;

		// Token: 0x04002050 RID: 8272
		private const byte IntSize = 32;

		// Token: 0x04002051 RID: 8273
		private int m_length;

		// Token: 0x04002052 RID: 8274
		[SecurityCritical]
		private unsafe int* m_arrayPtr;

		// Token: 0x04002053 RID: 8275
		private int[] m_array;

		// Token: 0x04002054 RID: 8276
		private bool useStackAlloc;
	}
}
