using System;

namespace a.b
{
	// Token: 0x02000280 RID: 640
	internal class a4
	{
		// Token: 0x060016CC RID: 5836 RVA: 0x000685D0 File Offset: 0x000675D0
		public a4(byte[] A_0)
		{
			this.a = new Guid(A_0);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x000685E4 File Offset: 0x000675E4
		public a4(string A_0)
		{
			this.a = new Guid(A_0);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x000685F8 File Offset: 0x000675F8
		public static a4 a(string A_0)
		{
			return new a4(A_0);
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00068600 File Offset: 0x00067600
		public override string ToString()
		{
			return this.a.ToString();
		}

		// Token: 0x040010F3 RID: 4339
		private Guid a;
	}
}
