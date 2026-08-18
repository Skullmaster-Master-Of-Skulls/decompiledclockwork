using System;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000289 RID: 649
	public class FlexibleSizeLongArray
	{
		// Token: 0x0600194E RID: 6478 RVA: 0x00108AA0 File Offset: 0x00106CA0
		public FlexibleSizeLongArray(int capacity)
		{
			this.m_vArray = new long[capacity];
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00108AB4 File Offset: 0x00106CB4
		public FlexibleSizeLongArray(long[] array)
		{
			if (array == null)
			{
				this.m_vArray = new long[8];
				return;
			}
			this.m_vArray = array;
			this.m_vContentSize = array.Length;
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x00108ADC File Offset: 0x00106CDC
		public FlexibleSizeLongArray(FlexibleSizeLongArray fArray)
		{
			if (fArray == null)
			{
				this.m_vArray = new long[8];
				return;
			}
			this.m_vContentSize = fArray.m_vContentSize;
			this.m_vArray = new long[this.m_vContentSize + 8];
			long[] vArray = fArray.m_vArray;
			for (int i = 0; i < this.m_vContentSize; i++)
			{
				this.m_vArray[i] = vArray[i];
			}
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00108B44 File Offset: 0x00106D44
		public static FlexibleSizeLongArray Merge(FlexibleSizeLongArray x, FlexibleSizeLongArray y)
		{
			if (x == null)
			{
				return new FlexibleSizeLongArray(y);
			}
			if (y == null)
			{
				return x;
			}
			int vContentSize = x.m_vContentSize;
			int vContentSize2 = y.m_vContentSize;
			long[] vArray = x.m_vArray;
			long[] vArray2 = y.m_vArray;
			FlexibleSizeLongArray flexibleSizeLongArray = new FlexibleSizeLongArray(vContentSize + vContentSize2);
			int num = 0;
			int num2 = 0;
			int vContentSize3 = 0;
			while (num < vContentSize && num2 < vContentSize2)
			{
				long num3 = vArray[num];
				long num4 = vArray2[num2];
				if (num3 == num4)
				{
					flexibleSizeLongArray.m_vArray[vContentSize3++] = num3;
					num++;
					num2++;
				}
				else if (num3 < num4)
				{
					flexibleSizeLongArray.m_vArray[vContentSize3++] = num3;
					num++;
				}
				else
				{
					flexibleSizeLongArray.m_vArray[vContentSize3++] = num4;
					num2++;
				}
			}
			for (int i = num; i < vContentSize; i++)
			{
				flexibleSizeLongArray.m_vArray[vContentSize3++] = vArray[i];
			}
			for (int j = num2; j < vContentSize2; j++)
			{
				flexibleSizeLongArray.m_vArray[vContentSize3++] = vArray2[j];
			}
			flexibleSizeLongArray.m_vContentSize = vContentSize3;
			return flexibleSizeLongArray;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00108C54 File Offset: 0x00106E54
		public static FlexibleSizeLongArray Merge(FlexibleSizeLongArray x, long[] ya)
		{
			if (x == null)
			{
				return new FlexibleSizeLongArray(ya);
			}
			if (ya == null)
			{
				return x;
			}
			int vContentSize = x.m_vContentSize;
			int num = ya.Length;
			long[] vArray = x.m_vArray;
			FlexibleSizeLongArray flexibleSizeLongArray = new FlexibleSizeLongArray(vContentSize + num);
			int num2 = 0;
			int num3 = 0;
			int vContentSize2 = 0;
			while (num2 < vContentSize && num3 < num)
			{
				long num4 = vArray[num2];
				long num5 = ya[num3];
				if (num4 == num5)
				{
					flexibleSizeLongArray.m_vArray[vContentSize2++] = num4;
					num2++;
					num3++;
				}
				else if (num4 < num5)
				{
					flexibleSizeLongArray.m_vArray[vContentSize2++] = num4;
					num2++;
				}
				else
				{
					flexibleSizeLongArray.m_vArray[vContentSize2++] = num5;
					num3++;
				}
			}
			for (int i = num2; i < vContentSize; i++)
			{
				flexibleSizeLongArray.m_vArray[vContentSize2++] = vArray[i];
			}
			for (int j = num3; j < num; j++)
			{
				flexibleSizeLongArray.m_vArray[vContentSize2++] = ya[j];
			}
			flexibleSizeLongArray.m_vContentSize = vContentSize2;
			return flexibleSizeLongArray;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00108D58 File Offset: 0x00106F58
		public static FlexibleSizeLongArray Insert(FlexibleSizeLongArray x, long yv)
		{
			FlexibleSizeLongArray flexibleSizeLongArray;
			if (x == null)
			{
				flexibleSizeLongArray = new FlexibleSizeLongArray(8);
				flexibleSizeLongArray.m_vArray[0] = yv;
				flexibleSizeLongArray.m_vContentSize = 1;
				return flexibleSizeLongArray;
			}
			long[] vArray = x.m_vArray;
			int vContentSize = x.m_vContentSize;
			int num;
			if (vContentSize == 0 || yv > vArray[vContentSize - 1])
			{
				num = vContentSize;
			}
			else if (yv < vArray[0])
			{
				num = 0;
			}
			else
			{
				num = Array.BinarySearch<long>(vArray, 0, vContentSize, yv);
				if (num >= 0)
				{
					return x;
				}
				num = ~num;
			}
			int num2 = x.m_vArray.Length;
			long[] array;
			if (num2 > x.m_vContentSize)
			{
				flexibleSizeLongArray = x;
				array = vArray;
			}
			else
			{
				flexibleSizeLongArray = new FlexibleSizeLongArray(num2 + 8);
				array = flexibleSizeLongArray.m_vArray;
				for (int i = 0; i < num; i++)
				{
					array[i] = vArray[i];
				}
			}
			for (int j = x.m_vContentSize; j > num; j--)
			{
				array[j] = vArray[j - 1];
			}
			flexibleSizeLongArray.m_vArray[num] = yv;
			flexibleSizeLongArray.m_vContentSize = vContentSize + 1;
			return flexibleSizeLongArray;
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x00108E3C File Offset: 0x0010703C
		public static FlexibleSizeLongArray Append(FlexibleSizeLongArray x, long yv)
		{
			FlexibleSizeLongArray flexibleSizeLongArray;
			if (x == null)
			{
				flexibleSizeLongArray = new FlexibleSizeLongArray(8);
				flexibleSizeLongArray.m_vArray[0] = yv;
				flexibleSizeLongArray.m_vContentSize = 1;
				return flexibleSizeLongArray;
			}
			int num = x.m_vArray.Length;
			if (num > x.m_vContentSize)
			{
				flexibleSizeLongArray = x;
			}
			else
			{
				flexibleSizeLongArray = new FlexibleSizeLongArray(num + 8);
				x.m_vArray.CopyTo(flexibleSizeLongArray.m_vArray, 0);
			}
			flexibleSizeLongArray.m_vArray[x.m_vContentSize] = yv;
			flexibleSizeLongArray.m_vContentSize = x.m_vContentSize + 1;
			return flexibleSizeLongArray;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00108EB4 File Offset: 0x001070B4
		public static FlexibleSizeLongArray Append(FlexibleSizeLongArray x, long[] y, int yStart, int yEnd, long mask)
		{
			int num = yEnd - yStart;
			if (y == null || num <= 0)
			{
				return x;
			}
			FlexibleSizeLongArray flexibleSizeLongArray;
			long[] vArray;
			if (x == null)
			{
				flexibleSizeLongArray = new FlexibleSizeLongArray(num + 8);
				vArray = flexibleSizeLongArray.m_vArray;
				int i = yStart;
				int num2 = 0;
				while (i < yEnd)
				{
					vArray[num2++] = (mask | (y[i] & (long)((ulong)-1)));
					i++;
				}
				flexibleSizeLongArray.m_vContentSize = num;
				return flexibleSizeLongArray;
			}
			if (y == null)
			{
				return x;
			}
			int num3 = x.m_vArray.Length;
			int vContentSize = x.m_vContentSize;
			if (num3 > vContentSize + num)
			{
				flexibleSizeLongArray = x;
				vArray = x.m_vArray;
			}
			else
			{
				flexibleSizeLongArray = new FlexibleSizeLongArray(vContentSize + num + 8);
				vArray = flexibleSizeLongArray.m_vArray;
				long[] vArray2 = x.m_vArray;
				vArray2.CopyTo(vArray, 0);
			}
			int j = yStart;
			int num4 = vContentSize;
			while (j < yEnd)
			{
				vArray[num4++] = (mask | (y[j] & (long)((ulong)-1)));
				j++;
			}
			flexibleSizeLongArray.m_vContentSize = vContentSize + num;
			return flexibleSizeLongArray;
		}

		// Token: 0x04001B7B RID: 7035
		public const int c_vGapSize = 8;

		// Token: 0x04001B7C RID: 7036
		public long[] m_vArray;

		// Token: 0x04001B7D RID: 7037
		public int m_vContentSize;
	}
}
