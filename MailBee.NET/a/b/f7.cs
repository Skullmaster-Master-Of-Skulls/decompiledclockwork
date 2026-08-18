using System;
using System.IO;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002F9 RID: 761
	internal class f7
	{
		// Token: 0x06001AE4 RID: 6884 RVA: 0x00075DD4 File Offset: 0x00074DD4
		public f7(Stream A_0)
		{
			A_0.Position = 0L;
			this.h = this.a(A_0);
			long num = p.g(this.h, 0);
			if (num == -2226271756974174256L)
			{
				if (this.h[30] == 12)
				{
					this.a = c5.d;
				}
				else
				{
					if (this.h[30] != 9)
					{
						throw new MailBeeOutlookMsgNotFoundException("Unsupported blocksize  (2^" + this.h[30] + "). Expected 2^9 or 2^12.", 1200);
					}
					this.a = c5.b;
				}
				this.b = new id(44, this.h).a();
				this.c = new id(48, this.h).a();
				this.d = new id(60, this.h).a();
				this.e = new id(64, this.h).a();
				this.f = new id(68, this.h).a();
				this.g = new id(72, this.h).a();
				if (this.a.f() != 512)
				{
					byte[] a_ = new byte[this.a.f() - 512];
					g9.a(A_0, a_);
				}
				return;
			}
			byte[] o = c5.o;
			if (this.h[0] == o[0] && this.h[1] == o[1] && this.h[2] == o[2] && this.h[3] == o[3])
			{
				throw new MailBeeOutlookMsgNotFoundException(Resources.Instance.ErrorDesc_OleDocXmlNotOle2Format, 1200);
			}
			if ((num & -31525197391593473L) == 4503608217567241L)
			{
				throw new MailBeeOutlookMsgNotFoundException("The supplied data appears to be in BIFF2 format.  POI only supports BIFF8 format", 1200);
			}
			throw new MailBeeOutlookMsgNotFoundException(string.Format(Resources.Instance.ErrorDesc_OleDocInvalidHeaderSignatureRead0Expected1, num, -2226271756974174256L), 1200);
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x00075FD8 File Offset: 0x00074FD8
		private byte[] a(Stream A_0)
		{
			byte[] array = new byte[512];
			int num = g9.a(A_0, array);
			if (num != 512)
			{
				this.a(num, 512);
			}
			return array;
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0007600D File Offset: 0x0007500D
		private static string a(long A_0)
		{
			return new string(f5.b(A_0));
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x0007601C File Offset: 0x0007501C
		private void a(int A_0, int A_1)
		{
			if (A_0 < 0)
			{
				A_0 = 0;
			}
			" byte" + ((A_0 == 1) ? "" : "s");
			throw new MailBeeOutlookMsgNotFoundException(string.Format(Resources.Instance.ErrorDesc_OleDocUnableToReadEntireHeader0ReadExpected1, A_0, this.a), 1200);
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x00076070 File Offset: 0x00075070
		public int f()
		{
			return this.c;
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00076078 File Offset: 0x00075078
		public int d()
		{
			return this.d;
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00076080 File Offset: 0x00075080
		public int e()
		{
			return this.b;
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00076088 File Offset: 0x00075088
		public int[] c()
		{
			int[] array = new int[109];
			int num = 76;
			for (int i = 0; i < 109; i++)
			{
				array[i] = p.i(this.h, num);
				num += 4;
			}
			return array;
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x000760C1 File Offset: 0x000750C1
		public int a()
		{
			return this.g;
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x000760C9 File Offset: 0x000750C9
		public int g()
		{
			return this.f;
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x000760D1 File Offset: 0x000750D1
		public y b()
		{
			return this.a;
		}

		// Token: 0x0400130B RID: 4875
		private y a;

		// Token: 0x0400130C RID: 4876
		private int b;

		// Token: 0x0400130D RID: 4877
		private int c;

		// Token: 0x0400130E RID: 4878
		private int d;

		// Token: 0x0400130F RID: 4879
		private int e;

		// Token: 0x04001310 RID: 4880
		private int f;

		// Token: 0x04001311 RID: 4881
		private int g;

		// Token: 0x04001312 RID: 4882
		private byte[] h;
	}
}
