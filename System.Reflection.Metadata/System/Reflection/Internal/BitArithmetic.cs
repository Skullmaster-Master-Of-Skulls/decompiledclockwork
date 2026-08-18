using System;

namespace System.Reflection.Internal
{
	// Token: 0x0200015C RID: 348
	internal static class BitArithmetic
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x0001EE04 File Offset: 0x0001D004
		internal static int CountBits(int v)
		{
			return BitArithmetic.CountBits((uint)v);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0001EE0C File Offset: 0x0001D00C
		internal static int CountBits(uint v)
		{
			uint num = v;
			v = num - (num >> 1 & 1431655765U);
			v = (v & 858993459U) + (v >> 2 & 858993459U);
			uint num2 = v;
			return (int)((num2 + (num2 >> 4) & 252645135U) * 16843009U) >> 24;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0001EE44 File Offset: 0x0001D044
		internal static int CountBits(ulong v)
		{
			ulong num = v;
			v = num - (num >> 1 & 6148914691236517205UL);
			v = (v & 3689348814741910323UL) + (v >> 2 & 3689348814741910323UL);
			ulong num2 = v;
			return (int)((num2 + (num2 >> 4) & 1085102592571150095UL) * 72340172838076673UL >> 56);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0001EE9C File Offset: 0x0001D09C
		internal static uint Align(uint position, uint alignment)
		{
			uint num = position & ~(alignment - 1U);
			if (num == position)
			{
				return num;
			}
			return num + alignment;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0001EEBC File Offset: 0x0001D0BC
		internal static int Align(int position, int alignment)
		{
			int num = position & ~(alignment - 1);
			if (num == position)
			{
				return num;
			}
			return num + alignment;
		}
	}
}
