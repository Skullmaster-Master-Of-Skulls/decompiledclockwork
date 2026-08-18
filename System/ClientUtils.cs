using System;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x020007A0 RID: 1952
	internal static class ClientUtils
	{
		// Token: 0x06003C28 RID: 15400 RVA: 0x001012AB File Offset: 0x001002AB
		public static bool IsCriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException || ex is ExecutionEngineException || ex is IndexOutOfRangeException || ex is AccessViolationException;
		}

		// Token: 0x06003C29 RID: 15401 RVA: 0x001012E8 File Offset: 0x001002E8
		public static bool IsSecurityOrCriticalException(Exception ex)
		{
			return ex is SecurityException || ClientUtils.IsCriticalException(ex);
		}

		// Token: 0x06003C2A RID: 15402 RVA: 0x001012FC File Offset: 0x001002FC
		public static int GetBitCount(uint x)
		{
			int num = 0;
			while (x > 0U)
			{
				x &= x - 1U;
				num++;
			}
			return num;
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x00101320 File Offset: 0x00100320
		public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue)
		{
			return value >= minValue && value <= maxValue;
		}

		// Token: 0x06003C2C RID: 15404 RVA: 0x00101340 File Offset: 0x00100340
		public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue, int maxNumberOfBitsOn)
		{
			bool flag = value >= minValue && value <= maxValue;
			return flag && ClientUtils.GetBitCount((uint)value) <= maxNumberOfBitsOn;
		}

		// Token: 0x06003C2D RID: 15405 RVA: 0x00101374 File Offset: 0x00100374
		public static bool IsEnumValid_Masked(Enum enumValue, int value, uint mask)
		{
			return ((long)value & (long)((ulong)mask)) == (long)value;
		}

		// Token: 0x06003C2E RID: 15406 RVA: 0x0010138C File Offset: 0x0010038C
		public static bool IsEnumValid_NotSequential(Enum enumValue, int value, params int[] enumValues)
		{
			for (int i = 0; i < enumValues.Length; i++)
			{
				if (enumValues[i] == value)
				{
					return true;
				}
			}
			return false;
		}
	}
}
