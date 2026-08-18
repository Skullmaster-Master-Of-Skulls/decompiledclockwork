using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x02000323 RID: 803
	internal class ByValueComparer : IComparer
	{
		// Token: 0x06001BB8 RID: 7096 RVA: 0x00088442 File Offset: 0x00086642
		private ByValueComparer(IComparer comparer)
		{
			this.nonByValueComparer = comparer;
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x00088454 File Offset: 0x00086654
		int IComparer.Compare(object x, object y)
		{
			if (object.ReferenceEquals(x, y))
			{
				return 0;
			}
			if (object.ReferenceEquals(x, DBNull.Value))
			{
				x = null;
			}
			if (object.ReferenceEquals(y, DBNull.Value))
			{
				y = null;
			}
			if (x != null && y != null)
			{
				byte[] array = x as byte[];
				byte[] array2 = y as byte[];
				if (array != null && array2 != null)
				{
					int num = array.Length - array2.Length;
					if (num == 0)
					{
						int num2 = 0;
						while (num == 0 && num2 < array.Length)
						{
							byte b = array[num2];
							byte b2 = array2[num2];
							if (b != b2)
							{
								num = (int)(b - b2);
							}
							num2++;
						}
					}
					return num;
				}
			}
			return this.nonByValueComparer.Compare(x, y);
		}

		// Token: 0x040009B6 RID: 2486
		internal static readonly IComparer Default = new ByValueComparer(Comparer<object>.Default);

		// Token: 0x040009B7 RID: 2487
		private readonly IComparer nonByValueComparer;
	}
}
