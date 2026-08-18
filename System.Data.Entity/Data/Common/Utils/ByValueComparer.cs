using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Common.Utils
{
	// Token: 0x0200038D RID: 909
	internal class ByValueComparer : IComparer
	{
		// Token: 0x0600327B RID: 12923 RVA: 0x000C53D4 File Offset: 0x000C35D4
		private ByValueComparer(IComparer comparer)
		{
			this.nonByValueComparer = comparer;
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x000C53E4 File Offset: 0x000C35E4
		int IComparer.Compare(object x, object y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == DBNull.Value)
			{
				x = null;
			}
			if (y == DBNull.Value)
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

		// Token: 0x04001653 RID: 5715
		internal static readonly IComparer Default = new ByValueComparer(Comparer<object>.Default);

		// Token: 0x04001654 RID: 5716
		private readonly IComparer nonByValueComparer;
	}
}
