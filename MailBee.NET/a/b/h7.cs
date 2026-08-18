using System;
using System.IO;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200029E RID: 670
	internal class h7
	{
		// Token: 0x0600178E RID: 6030 RVA: 0x0006B876 File Offset: 0x0006A876
		public static int a(Stream A_0, short A_1)
		{
			p.a(A_0, A_1);
			return 2;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x0006B880 File Offset: 0x0006A880
		public static int b(Stream A_0, int A_1)
		{
			p.b(A_1, A_0);
			return 4;
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x0006B88C File Offset: 0x0006A88C
		[Obsolete]
		public static int b(Stream A_0, uint A_1)
		{
			int num = 4;
			byte[] array = new byte[num];
			p.a(array, 0, A_1);
			A_0.Write(array, 0, num);
			return num;
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x0006B8B4 File Offset: 0x0006A8B4
		public static int a(Stream A_0, long A_1)
		{
			p.a(A_1, A_0);
			return 8;
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0006B8BE File Offset: 0x0006A8BE
		public static void a(Stream A_0, int A_1)
		{
			if ((A_1 & -65536) != 0)
			{
				throw new IllegalPropertySetDataException("Value " + A_1 + " cannot be represented by 2 bytes.");
			}
			p.a(A_1, A_0);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x0006B8EC File Offset: 0x0006A8EC
		public static int a(Stream A_0, uint A_1)
		{
			ulong num = (ulong)A_1 & 18446744069414584320UL;
			if (num != 0UL && num != 18446744069414584320UL)
			{
				throw new IllegalPropertySetDataException("Value " + A_1 + " cannot be represented by 4 bytes.");
			}
			p.b((long)((ulong)A_1), A_0);
			return 4;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0006B93C File Offset: 0x0006A93C
		public static int a(Stream A_0, ar A_1)
		{
			byte[] array = new byte[16];
			A_1.a(array, 0);
			A_0.Write(array, 0, array.Length);
			return array.Length;
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0006B968 File Offset: 0x0006A968
		public static void a(Stream A_0, em[] A_1, int A_2)
		{
			if (A_1 == null)
			{
				return;
			}
			foreach (em em in A_1)
			{
				h7.a(A_0, (uint)em.e());
				h7.a(A_0, (uint)em.b());
			}
			foreach (em em2 in A_1)
			{
				long num = em2.d();
				h7.a(A_0, (uint)num);
				e3.a(A_0, (long)((int)num), em2.c(), A_2);
			}
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0006B9DC File Offset: 0x0006A9DC
		public static int a(Stream A_0, double A_1)
		{
			p.a(A_1, A_0);
			return 8;
		}
	}
}
