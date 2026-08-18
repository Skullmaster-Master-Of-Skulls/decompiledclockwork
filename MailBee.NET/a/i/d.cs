using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml;

namespace a.i
{
	// Token: 0x020001E0 RID: 480
	internal static class d
	{
		// Token: 0x06000F7C RID: 3964 RVA: 0x0003BBDC File Offset: 0x0003ABDC
		public static Task a(this XmlReader A_0, string A_1)
		{
			d.c c;
			c.c = A_0;
			c.d = A_1;
			c.b = AsyncTaskMethodBuilder.Create();
			c.a = -1;
			AsyncTaskMethodBuilder b = c.b;
			b.Start<d.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0003BC2C File Offset: 0x0003AC2C
		public static Task b(this XmlReader A_0)
		{
			d.a a;
			a.c = A_0;
			a.b = AsyncTaskMethodBuilder.Create();
			a.a = -1;
			AsyncTaskMethodBuilder b = a.b;
			b.Start<d.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0003BC74 File Offset: 0x0003AC74
		public static Task<string> a(this XmlReader A_0)
		{
			d.b b;
			b.c = A_0;
			b.b = AsyncTaskMethodBuilder<string>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<string> b2 = b.b;
			b2.Start<d.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0003BCB9 File Offset: 0x0003ACB9
		private static bool a(XmlNodeType A_0)
		{
			return ((ulong)d.a & (ulong)(1L << (int)(A_0 & (XmlNodeType)31))) > 0UL;
		}

		// Token: 0x04000B57 RID: 2903
		private static uint a = 24600U;
	}
}
