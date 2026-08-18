using System;
using System.Collections.Generic;

namespace a.b
{
	// Token: 0x020002EB RID: 747
	internal class cf
	{
		// Token: 0x06001A5C RID: 6748 RVA: 0x00074206 File Offset: 0x00073206
		private cf()
		{
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00074210 File Offset: 0x00073210
		public static List<ed> a(bn[] A_0)
		{
			List<ed> list = new List<ed>();
			for (int i = 0; i < A_0.Length; i++)
			{
				cf.a(A_0[i].bv(), list);
			}
			return list;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00074240 File Offset: 0x00073240
		public static void a(byte[] A_0, List<ed> A_1)
		{
			int num = A_0.Length / 128;
			int num2 = 0;
			int i = 0;
			while (i < num)
			{
				switch (A_0[num2 + 66])
				{
				case 1:
					A_1.Add(new g8(A_1.Count, A_0, num2));
					break;
				case 2:
					A_1.Add(new gg(A_1.Count, A_0, num2));
					break;
				case 3:
				case 4:
					goto IL_74;
				case 5:
					A_1.Add(new hj(A_1.Count, A_0, num2));
					break;
				default:
					goto IL_74;
				}
				IL_7B:
				num2 += 128;
				i++;
				continue;
				IL_74:
				A_1.Add(null);
				goto IL_7B;
			}
		}
	}
}
