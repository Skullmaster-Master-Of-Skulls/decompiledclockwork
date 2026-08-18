using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200015D RID: 349
	internal struct Buffer<TElement>
	{
		// Token: 0x06000C2B RID: 3115 RVA: 0x0002D16C File Offset: 0x0002B36C
		internal Buffer(IEnumerable<TElement> source)
		{
			TElement[] array = null;
			int num = 0;
			ICollection<TElement> collection = source as ICollection<TElement>;
			if (collection != null)
			{
				num = collection.Count;
				if (num > 0)
				{
					array = new TElement[num];
					collection.CopyTo(array, 0);
				}
			}
			else
			{
				foreach (TElement telement in source)
				{
					if (array == null)
					{
						array = new TElement[4];
					}
					else if (array.Length == num)
					{
						TElement[] array2 = new TElement[checked(num * 2)];
						Array.Copy(array, 0, array2, 0, num);
						array = array2;
					}
					array[num] = telement;
					num++;
				}
			}
			this.items = array;
			this.count = num;
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0002D220 File Offset: 0x0002B420
		internal TElement[] ToArray()
		{
			if (this.count == 0)
			{
				return new TElement[0];
			}
			if (this.items.Length == this.count)
			{
				return this.items;
			}
			TElement[] array = new TElement[this.count];
			Array.Copy(this.items, 0, array, 0, this.count);
			return array;
		}

		// Token: 0x04000798 RID: 1944
		internal TElement[] items;

		// Token: 0x04000799 RID: 1945
		internal int count;
	}
}
