using System;
using System.Collections;

namespace Telerik.Web.UI.Dictionaries
{
	// Token: 0x020011D7 RID: 4567
	internal class SorterObjectArray
	{
		// Token: 0x0600BCB0 RID: 48304 RVA: 0x0029DB85 File Offset: 0x0029BD85
		public SorterObjectArray(ArrayList items, IComparer comparer)
		{
			if (comparer == null)
			{
				comparer = Comparer.Default;
			}
			this.items = items;
			this.comparer = comparer;
		}

		// Token: 0x0600BCB1 RID: 48305 RVA: 0x0029DBA8 File Offset: 0x0029BDA8
		public virtual void QuickSort(int left, int right)
		{
			do
			{
				int num = left;
				int num2 = right;
				object obj = this.items[num + num2 >> 1];
				for (;;)
				{
					if (this.comparer.Compare(this.items[num], obj) >= 0)
					{
						while (this.comparer.Compare(obj, this.items[num2]) < 0)
						{
							num2--;
						}
						if (num > num2)
						{
							break;
						}
						if (num < num2)
						{
							object value = this.items[num];
							this.items[num] = this.items[num2];
							this.items[num2] = value;
						}
						num++;
						num2--;
						if (num > num2)
						{
							break;
						}
					}
					else
					{
						num++;
					}
				}
				if (num2 - left <= right - num)
				{
					if (left < num2)
					{
						this.QuickSort(left, num2);
					}
					left = num;
				}
				else
				{
					if (num < right)
					{
						this.QuickSort(num, right);
					}
					right = num2;
				}
			}
			while (left < right);
		}

		// Token: 0x0400318F RID: 12687
		private readonly IComparer comparer;

		// Token: 0x04003190 RID: 12688
		private readonly ArrayList items;
	}
}
