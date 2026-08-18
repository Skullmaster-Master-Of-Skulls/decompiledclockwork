using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;

namespace a.b
{
	// Token: 0x0200037F RID: 895
	internal sealed class gt : hs, im
	{
		// Token: 0x0600208D RID: 8333 RVA: 0x00087380 File Offset: 0x00086380
		public gt(de A_0, ay A_1, int A_2, int A_3, int A_4, int A_5, int A_6, int A_7, string A_8) : base(hu.d)
		{
			if (A_2 <= 0)
			{
				throw new ArgumentException(fa.f(A_2));
			}
			if (A_3 <= 0)
			{
				throw new ArgumentException(fa.e(A_3));
			}
			if (A_4 <= 0)
			{
				throw new ArgumentException(fa.c(A_4));
			}
			if (A_5 <= 0)
			{
				throw new ArgumentException(fa.d(A_5));
			}
			if (A_6 <= 0)
			{
				throw new ArgumentException(fa.b(A_6));
			}
			if (A_7 <= 0)
			{
				throw new ArgumentException(fa.a(A_7));
			}
			if (A_8 == null)
			{
				throw new ArgumentNullException("imageDataHex");
			}
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
			this.d = A_3;
			this.e = A_4;
			this.f = A_5;
			this.g = A_6;
			this.h = A_7;
			this.i = A_8;
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x00087452 File Offset: 0x00086452
		protected override void dw(gq A_0)
		{
			A_0.it(this);
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x0008745B File Offset: 0x0008645B
		public de m8()
		{
			return this.a;
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x00087463 File Offset: 0x00086463
		public ay m9()
		{
			return this.b;
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x0008746B File Offset: 0x0008646B
		public int na()
		{
			return this.c;
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x00087473 File Offset: 0x00086473
		public int nb()
		{
			return this.d;
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x0008747B File Offset: 0x0008647B
		public int nc()
		{
			return this.e;
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x00087483 File Offset: 0x00086483
		public int nd()
		{
			return this.f;
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x0008748B File Offset: 0x0008648B
		public int ne()
		{
			return this.g;
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x00087493 File Offset: 0x00086493
		public int nf()
		{
			return this.h;
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x0008749B File Offset: 0x0008649B
		public string ng()
		{
			return this.i;
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x000874A3 File Offset: 0x000864A3
		public byte[] nh()
		{
			if (this.j == null)
			{
				this.j = gt.a(this.i);
			}
			return this.j;
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x000874C4 File Offset: 0x000864C4
		public Image ni()
		{
			switch (this.a)
			{
			case de.a:
			case de.b:
			case de.c:
			case de.d:
			case de.e:
			{
				byte[] array = this.nh();
				return Image.FromStream(new MemoryStream(array, 0, array.Length));
			}
			default:
				return null;
			}
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x0008750C File Offset: 0x0008650C
		public static byte[] a(string A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("imageDataHex");
			}
			int length = A_0.Length;
			byte[] array = new byte[length / 2];
			StringBuilder stringBuilder = new StringBuilder(2);
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				stringBuilder.Append(A_0[i]);
				if (stringBuilder.Length == 2)
				{
					array[num] = byte.Parse(stringBuilder.ToString(), NumberStyles.HexNumber);
					num++;
					stringBuilder.Remove(0, 2);
				}
			}
			return array;
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x0008758C File Offset: 0x0008658C
		protected override bool dx(object A_0)
		{
			gt gt = A_0 as gt;
			return gt != null && base.dx(gt) && this.a == gt.a && this.b == gt.b && this.c == gt.c && this.d == gt.d && this.e == gt.e && this.f == gt.f && this.g == gt.g && this.h == gt.h && this.i.Equals(gt.i);
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x00087638 File Offset: 0x00086638
		protected override int dy()
		{
			return f3.a(f3.a(f3.a(f3.a(f3.a(f3.a(f3.a(f3.a(f3.a(base.dy(), this.a), this.b), this.c), this.d), this.e), this.f), this.g), this.h), this.i);
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000876B8 File Offset: 0x000866B8
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"[",
				this.a,
				": ",
				this.b,
				", ",
				this.c,
				" x ",
				this.d,
				" (",
				this.e,
				" x ",
				this.f,
				") {",
				this.g,
				"% x ",
				this.h,
				"%} :",
				this.i.Length / 2,
				" bytes]"
			});
		}

		// Token: 0x0400148A RID: 5258
		private readonly de a;

		// Token: 0x0400148B RID: 5259
		private readonly ay b;

		// Token: 0x0400148C RID: 5260
		private readonly int c;

		// Token: 0x0400148D RID: 5261
		private readonly int d;

		// Token: 0x0400148E RID: 5262
		private readonly int e;

		// Token: 0x0400148F RID: 5263
		private readonly int f;

		// Token: 0x04001490 RID: 5264
		private readonly int g;

		// Token: 0x04001491 RID: 5265
		private readonly int h;

		// Token: 0x04001492 RID: 5266
		private readonly string i;

		// Token: 0x04001493 RID: 5267
		private byte[] j;
	}
}
