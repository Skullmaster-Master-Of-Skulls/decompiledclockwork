using System;
using System.Collections.Generic;
using System.IO;

namespace a.b
{
	// Token: 0x020002FF RID: 767
	internal class d3 : b9
	{
		// Token: 0x06001B0D RID: 6925 RVA: 0x0007653C File Offset: 0x0007553C
		public d3(Stream A_0, y A_1)
		{
			List<i4> list = new List<i4>();
			i4 i;
			do
			{
				i = new i4(A_0, A_1.f());
				if (i.c())
				{
					list.Add(i);
				}
			}
			while (!i.b());
			this.a(list.ToArray());
		}
	}
}
