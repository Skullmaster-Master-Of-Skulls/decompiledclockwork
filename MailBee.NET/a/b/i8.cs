using System;
using System.IO;
using System.Text;
using a.h;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000271 RID: 625
	internal class i8
	{
		// Token: 0x06001674 RID: 5748 RVA: 0x000667D8 File Offset: 0x000657D8
		public i8(di A_0, fb A_1)
		{
			this.k = A_1;
			this.j = A_0;
			this.d = A_0.a();
			A_0.a(0L);
			byte[] array = new byte[4];
			A_0.b(array);
			if (array[2] != 236)
			{
				ii.c(array);
				ii.a(array, true);
				throw new MailBeePstParsingException(Resources.Instance.ErrorDesc_OutlookPstUnableToParseTableBadType, 1210);
			}
			this.b = array[3];
			byte b = this.b;
			if (b != 124)
			{
				if (b != 188)
				{
					throw new MailBeePstParsingException(string.Format(Resources.Instance.ErrorDesc_OutlookPstUnableToParseTableBadType0, Convert.ToString(array[3], 16)), 1210);
				}
				this.a = "bc";
			}
			else
			{
				this.a = "7c";
			}
			this.c = (int)A_0.a(4L, 4);
			i8.a a = this.a(32);
			a.c.a((long)a.a);
			int num = a.c.ReadByte() & 255;
			if (num != 181)
			{
				a.c.a((long)a.a);
				num = (a.c.ReadByte() & 255);
				a.c.a((long)a.a);
				byte[] a_ = new byte[1024];
				a.c.b(a_);
				ii.a(a_, true);
				throw new MailBeePstParsingException(string.Format(Resources.Instance.ErrorDesc_OutlookPstUnableToParseTable0, num), 1210);
			}
			this.e = (a.c.ReadByte() & 255);
			this.f = (a.c.ReadByte() & 255);
			this.i = (a.c.ReadByte() & 255);
			int num2 = this.i;
			this.g = (int)a.a(4L, 4);
			this.l.Append(string.Format("Table ({0})\nhidUserRoot: {1} - 0x{2}\nSize Of Keys: {3} - 0x{4}\nSize Of Values: {5} - 0x{6}\nhidRoot: {7} - 0x{8}\n", new object[]
			{
				this.a,
				this.c,
				Convert.ToString(this.c, 16),
				this.e,
				Convert.ToString(this.e, 16),
				this.f,
				Convert.ToString(this.f, 16),
				this.g,
				Convert.ToString(this.g, 16)
			}));
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x00066A68 File Offset: 0x00065A68
		public virtual int a4()
		{
			return this.h;
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x00066A70 File Offset: 0x00065A70
		protected internal virtual void c()
		{
			this.k = null;
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x00066A7C File Offset: 0x00065A7C
		protected internal virtual i8.a a(int A_0)
		{
			if (A_0 == 0)
			{
				return new i8.a(0, 0, this.j);
			}
			if (this.k != null && this.k.a(A_0))
			{
				h1 a_ = this.k.b(A_0);
				i8.a result = null;
				try
				{
					di di = new di(this.j.e(), a_);
					result = new i8.a(0, (int)di.Length, di);
				}
				catch (IOException)
				{
					throw new MailBeePstParsingException(string.Format(Resources.Instance.ErrorDesc_OutlookPstIOExceptionReadingSubNode0, A_0), 1210);
				}
				return result;
			}
			if ((A_0 & 31) != 0)
			{
				return null;
			}
			int num = global::a.h.f.a(A_0, 16);
			if (num > this.d.Length)
			{
				return null;
			}
			int num2 = (A_0 & 65535) >> 5;
			int num3 = 0;
			if (num > 0)
			{
				num3 = (int)this.d[num - 1];
			}
			int num4 = (int)this.j.a((long)num3, 2) + num3;
			int num5 = (int)this.j.a((long)num4, 2);
			if (num2 >= num5 + 1)
			{
				return null;
			}
			num4 += 2 * num2 + 2;
			int a_2 = (int)this.j.a((long)num4, 2) + num3;
			int a_3 = (int)this.j.a((long)(num4 + 2), 2) + num3;
			return new i8.a(a_2, a_3, this.j);
		}

		// Token: 0x040010B7 RID: 4279
		protected internal string a;

		// Token: 0x040010B8 RID: 4280
		protected internal byte b;

		// Token: 0x040010B9 RID: 4281
		protected internal int c;

		// Token: 0x040010BA RID: 4282
		protected internal long[] d;

		// Token: 0x040010BB RID: 4283
		protected internal int e;

		// Token: 0x040010BC RID: 4284
		protected internal int f;

		// Token: 0x040010BD RID: 4285
		protected internal int g;

		// Token: 0x040010BE RID: 4286
		protected internal int h;

		// Token: 0x040010BF RID: 4287
		protected internal int i;

		// Token: 0x040010C0 RID: 4288
		private di j;

		// Token: 0x040010C1 RID: 4289
		private fb k;

		// Token: 0x040010C2 RID: 4290
		protected internal StringBuilder l = new StringBuilder();

		// Token: 0x02000272 RID: 626
		protected internal class a
		{
			// Token: 0x06001678 RID: 5752 RVA: 0x00066BC0 File Offset: 0x00065BC0
			internal a(int A_0, int A_1, di A_2)
			{
				this.a = A_0;
				this.b = A_1;
				this.c = A_2;
			}

			// Token: 0x06001679 RID: 5753 RVA: 0x00066BDD File Offset: 0x00065BDD
			internal virtual int a()
			{
				return this.b - this.a;
			}

			// Token: 0x0600167A RID: 5754 RVA: 0x00066BEC File Offset: 0x00065BEC
			internal virtual long a(long A_0, int A_1)
			{
				return this.c.a((long)this.a + A_0, A_1);
			}

			// Token: 0x040010C3 RID: 4291
			internal int a;

			// Token: 0x040010C4 RID: 4292
			internal int b;

			// Token: 0x040010C5 RID: 4293
			internal di c;
		}
	}
}
