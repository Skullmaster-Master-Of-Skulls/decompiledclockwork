using System;

namespace a.f
{
	// Token: 0x020000F2 RID: 242
	internal class j : bh
	{
		// Token: 0x06000818 RID: 2072 RVA: 0x000255E3 File Offset: 0x000245E3
		public static j a()
		{
			if (j.a == null)
			{
				j.a = new j();
			}
			return j.a;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x000255FB File Offset: 0x000245FB
		public override string jw()
		{
			return "AUTHENTICATE";
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00025602 File Offset: 0x00024602
		public override string jx()
		{
			return "LOGOUT";
		}

		// Token: 0x04000562 RID: 1378
		private static j a;
	}
}
