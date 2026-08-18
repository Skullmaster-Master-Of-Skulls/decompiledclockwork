using System;
using System.Collections;

namespace a.b
{
	// Token: 0x02000243 RID: 579
	internal class bl : e
	{
		// Token: 0x0600136F RID: 4975 RVA: 0x00057B50 File Offset: 0x00056B50
		public override byte[] du()
		{
			byte[] array = new byte[this.b.Count * 16 + 8];
			for (int i = 0; i < this.a.Length; i++)
			{
				array[i] = this.a[i];
			}
			int num = 0;
			while (this.b.Count != 0)
			{
				byte[] array2 = (byte[])this.b.Pop();
				for (int j = num * 16; j < array2.Length + num * 16; j++)
				{
					array[j + this.a.Length] = array2[j - num * 16];
				}
				num++;
			}
			return array;
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00057BE8 File Offset: 0x00056BE8
		public bl()
		{
			byte[] a = new byte[8];
			this.a = a;
			this.b = new Stack();
		}
	}
}
