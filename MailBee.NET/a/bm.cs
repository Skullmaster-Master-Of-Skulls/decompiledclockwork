using System;

namespace a
{
	// Token: 0x020004A1 RID: 1185
	internal class bm
	{
		// Token: 0x06002863 RID: 10339 RVA: 0x000BC424 File Offset: 0x000BB424
		public bm(string A_0, bg A_1)
		{
			bm.a = A_0;
			bm.b = A_1;
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x000BC438 File Offset: 0x000BB438
		public string e()
		{
			return bm.a;
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x000BC43F File Offset: 0x000BB43F
		public f g()
		{
			return bm.b.a;
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x000BC44B File Offset: 0x000BB44B
		public int f()
		{
			return bm.b.c;
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x000BC457 File Offset: 0x000BB457
		public bool d()
		{
			return bm.b.a == global::a.f.c || bm.b.a == global::a.f.a;
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x000BC475 File Offset: 0x000BB475
		public bool a()
		{
			return bm.b.a == global::a.f.e;
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x000BC484 File Offset: 0x000BB484
		public bool c()
		{
			return bm.b.a == global::a.f.c;
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x000BC493 File Offset: 0x000BB493
		public int b()
		{
			if (this.c())
			{
				return bm.b.b;
			}
			return -1;
		}

		// Token: 0x04001BB2 RID: 7090
		private static string a;

		// Token: 0x04001BB3 RID: 7091
		private static bg b;
	}
}
