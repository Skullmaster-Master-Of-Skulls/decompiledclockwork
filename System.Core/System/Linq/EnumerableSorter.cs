using System;

namespace System.Linq
{
	// Token: 0x0200015B RID: 347
	internal abstract class EnumerableSorter<TElement>
	{
		// Token: 0x06000C23 RID: 3107
		internal abstract void ComputeKeys(TElement[] elements, int count);

		// Token: 0x06000C24 RID: 3108
		internal abstract int CompareKeys(int index1, int index2);

		// Token: 0x06000C25 RID: 3109 RVA: 0x0002CFB4 File Offset: 0x0002B1B4
		internal int[] Sort(TElement[] elements, int count)
		{
			this.ComputeKeys(elements, count);
			int[] array = new int[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = i;
			}
			this.QuickSort(array, 0, count - 1);
			return array;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0002CFEC File Offset: 0x0002B1EC
		private void QuickSort(int[] map, int left, int right)
		{
			do
			{
				int num = left;
				int num2 = right;
				int index = map[num + (num2 - num >> 1)];
				do
				{
					if (num < map.Length)
					{
						if (this.CompareKeys(index, map[num]) > 0)
						{
							num++;
							continue;
						}
					}
					while (num2 >= 0 && this.CompareKeys(index, map[num2]) < 0)
					{
						num2--;
					}
					if (num > num2)
					{
						break;
					}
					if (num < num2)
					{
						int num3 = map[num];
						map[num] = map[num2];
						map[num2] = num3;
					}
					num++;
					num2--;
				}
				while (num <= num2);
				if (num2 - left <= right - num)
				{
					if (left < num2)
					{
						this.QuickSort(map, left, num2);
					}
					left = num;
				}
				else
				{
					if (num < right)
					{
						this.QuickSort(map, num, right);
					}
					right = num2;
				}
			}
			while (left < right);
		}
	}
}
