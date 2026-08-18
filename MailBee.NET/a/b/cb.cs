using System;
using System.IO;

namespace a.b
{
	// Token: 0x0200038C RID: 908
	internal class cb
	{
		// Token: 0x060020D2 RID: 8402 RVA: 0x00087D2E File Offset: 0x00086D2E
		public static ip b(string A_0, params g5[] A_1)
		{
			return cb.b(fy.a(A_0, new f6[0]), A_1);
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x00087D42 File Offset: 0x00086D42
		public static ip b(TextReader A_0, params g5[] A_1)
		{
			return cb.b(fy.a(A_0, new f6[0]), A_1);
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x00087D56 File Offset: 0x00086D56
		public static ip b(Stream A_0, params g5[] A_1)
		{
			return cb.b(fy.a(A_0, new f6[0]), A_1);
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x00087D6A File Offset: 0x00086D6A
		public static ip b(da A_0, params g5[] A_1)
		{
			return cb.b(fy.a(A_0, new f6[0]), A_1);
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x00087D80 File Offset: 0x00086D80
		public static ip b(f A_0, params g5[] A_1)
		{
			hi hi = new hi();
			g5[] array;
			if (A_1 == null)
			{
				array = new g5[]
				{
					hi
				};
			}
			else
			{
				array = new g5[A_1.Length + 1];
				array[0] = hi;
				A_1.CopyTo(array, 1);
			}
			cb.a(A_0, array);
			return hi.b();
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x00087DC7 File Offset: 0x00086DC7
		public static void a(string A_0, params g5[] A_1)
		{
			cb.a(fy.a(A_0, new f6[0]), A_1);
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x00087DDB File Offset: 0x00086DDB
		public static void a(TextReader A_0, params g5[] A_1)
		{
			cb.a(fy.a(A_0, new f6[0]), A_1);
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x00087DEF File Offset: 0x00086DEF
		public static void a(Stream A_0, params g5[] A_1)
		{
			cb.a(fy.a(A_0, new f6[0]), A_1);
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x00087E03 File Offset: 0x00086E03
		public static void a(da A_0, params g5[] A_1)
		{
			cb.a(fy.a(A_0, new f6[0]), A_1);
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x00087E18 File Offset: 0x00086E18
		public static void a(f A_0, params g5[] A_1)
		{
			hl hl = new hl(new g5[0]);
			if (A_1 != null)
			{
				foreach (g5 g in A_1)
				{
					if (g != null)
					{
						hl.lg(g);
					}
				}
			}
			hl.li(A_0);
		}
	}
}
