using System;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200029C RID: 668
	internal class fz
	{
		// Token: 0x0600177F RID: 6015 RVA: 0x0006B22D File Offset: 0x0006A22D
		public fz()
		{
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0006B235 File Offset: 0x0006A235
		public fz(byte[] A_0)
		{
			this.l = A_0;
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0006B244 File Offset: 0x0006A244
		public byte[] c()
		{
			return this.l;
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0006B24C File Offset: 0x0006A24C
		public void a(byte[] A_0)
		{
			this.l = A_0;
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0006B255 File Offset: 0x0006A255
		public long d()
		{
			return p.h(this.c(), 4);
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0006B263 File Offset: 0x0006A263
		public long a()
		{
			if (this.d() != -1L)
			{
				throw new HPSFException("Clipboard Format Tag of Thumbnail must be CFTAG_WINDOWS.");
			}
			return p.h(this.c(), 8);
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0006B288 File Offset: 0x0006A288
		public byte[] b()
		{
			if (this.d() != -1L)
			{
				throw new HPSFException("Clipboard Format Tag of Thumbnail must be CFTAG_WINDOWS.");
			}
			if (this.a() != 3L)
			{
				throw new HPSFException("Clipboard Format of Thumbnail must be CF_METAFILEPICT.");
			}
			byte[] array = this.c();
			int num = array.Length - 20;
			byte[] array2 = new byte[num];
			Array.Copy(array, 20, array2, 0, num);
			return array2;
		}

		// Token: 0x04001162 RID: 4450
		public const int a = 4;

		// Token: 0x04001163 RID: 4451
		public const int b = 8;

		// Token: 0x04001164 RID: 4452
		public const int c = 20;

		// Token: 0x04001165 RID: 4453
		public const int d = -1;

		// Token: 0x04001166 RID: 4454
		public const int e = -2;

		// Token: 0x04001167 RID: 4455
		public const int f = -3;

		// Token: 0x04001168 RID: 4456
		public const int g = 0;

		// Token: 0x04001169 RID: 4457
		public const int h = 3;

		// Token: 0x0400116A RID: 4458
		public const int i = 8;

		// Token: 0x0400116B RID: 4459
		public const int j = 14;

		// Token: 0x0400116C RID: 4460
		[Obsolete]
		public const int k = 2;

		// Token: 0x0400116D RID: 4461
		private byte[] l;
	}
}
