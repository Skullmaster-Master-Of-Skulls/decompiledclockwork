using System;
using System.Security;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000288 RID: 648
	internal static class ClientUtils
	{
		// Token: 0x060018B7 RID: 6327 RVA: 0x0008B269 File Offset: 0x00089469
		public static bool IsCriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException || ex is ExecutionEngineException || ex is IndexOutOfRangeException || ex is AccessViolationException;
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0008B2A6 File Offset: 0x000894A6
		public static bool IsSecurityOrCriticalException(Exception ex)
		{
			return ex is SecurityException || ClientUtils.IsCriticalException(ex);
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0008B2B8 File Offset: 0x000894B8
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

		// Token: 0x060018BA RID: 6330 RVA: 0x0008B2DC File Offset: 0x000894DC
		public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue)
		{
			return value >= minValue && value <= maxValue;
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0008B2FC File Offset: 0x000894FC
		public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue, int maxNumberOfBitsOn)
		{
			bool flag = value >= minValue && value <= maxValue;
			return flag && ClientUtils.GetBitCount((uint)value) <= maxNumberOfBitsOn;
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0008B330 File Offset: 0x00089530
		public static bool IsEnumValid_Masked(Enum enumValue, int value, uint mask)
		{
			return ((long)value & (long)((ulong)mask)) == (long)value;
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0008B348 File Offset: 0x00089548
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
