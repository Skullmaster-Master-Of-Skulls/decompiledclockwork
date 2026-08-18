using System;
using System.IO;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200026C RID: 620
	internal class di : Stream
	{
		// Token: 0x0600163E RID: 5694 RVA: 0x000647B9 File Offset: 0x000637B9
		public virtual bool b()
		{
			return this.i;
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x000647C4 File Offset: 0x000637C4
		public virtual long[] a()
		{
			if (this.c.Count == 0)
			{
				return new long[]
				{
					this.h
				};
			}
			long[] array = new long[this.c.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.c.a(i) + (long)this.d.a(i).c;
			}
			return array;
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00064830 File Offset: 0x00063830
		public virtual bs e()
		{
			return this.b;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00064838 File Offset: 0x00063838
		internal di(bs A_0, byte[] A_1) : this(A_0, A_1, true)
		{
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00064844 File Offset: 0x00063844
		internal di(bs A_0, byte[] A_1, bool A_2)
		{
			this.c = new cj();
			this.d = new i();
			base..ctor();
			this.g = A_1;
			this.h = (long)this.g.Length;
			this.i = (A_2 && A_0.a() == 1);
			this.e = 0;
			this.f = 0L;
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x000648A8 File Offset: 0x000638A8
		internal di(bs A_0, h1 A_1)
		{
			this.c = new cj();
			this.d = new i();
			base..ctor();
			this.a = A_0.g();
			this.b = A_0;
			this.i = (A_0.a() == 1);
			hp a_ = A_0.e((long)A_1.b);
			this.a(a_);
			this.e = 0;
			this.f = 0L;
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00064918 File Offset: 0x00063918
		internal di(bs A_0, hp A_1)
		{
			this.c = new cj();
			this.d = new i();
			base..ctor();
			this.a = A_0.g();
			this.b = A_0;
			this.i = (A_0.a() == 1);
			this.a(A_1);
			this.e = 0;
			this.f = 0L;
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x0006497C File Offset: 0x0006397C
		private void a(hp A_0)
		{
			bool flag = (A_0.a & 2L) != 0L;
			this.a.Seek(A_0.b, SeekOrigin.Begin);
			byte[] array = new byte[A_0.c];
			this.a.Read(array, 0, array.Length);
			if (flag)
			{
				if (A_0.c < 8)
				{
					throw new MailBeePstParsingException(Resources.Instance.ErrorDesc_OutlookPstInvalidInternalBlockSize, 1210);
				}
				if (array[0] == 1)
				{
					this.h = ii.b(array, 4, 8);
					this.a(array);
					return;
				}
			}
			if (flag)
			{
				this.i = false;
			}
			this.g = array;
			this.h = (long)this.g.Length;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00064A28 File Offset: 0x00063A28
		private void a(byte[] A_0)
		{
			if (A_0[0] != 1)
			{
				throw new MailBeePstParsingException(Resources.Instance.ErrorDesc_OutlookPstUnableToProcessXBlock, 1210);
			}
			int num = (int)ii.b(A_0, 2, 4);
			int num2 = 8;
			if (this.b.f() == 14)
			{
				num2 = 4;
			}
			if (A_0[1] == 2)
			{
				int num3 = 8;
				for (int i = 0; i < num; i++)
				{
					long num4 = ii.b(A_0, num3, num3 + num2);
					num4 &= -2L;
					hp hp = this.b.e(num4);
					this.a.Seek(hp.b, SeekOrigin.Begin);
					byte[] array = new byte[hp.c];
					this.a.Read(array, 0, array.Length);
					this.a(array);
					num3 += num2;
				}
				return;
			}
			if (A_0[1] == 1)
			{
				int num5 = 8;
				for (int j = 0; j < num; j++)
				{
					long num6 = ii.b(A_0, num5, num5 + num2);
					num6 &= -2L;
					hp hp2 = this.b.e(num6);
					this.d.a(hp2);
					this.c.a(this.f);
					this.f += (long)hp2.c;
					num5 += num2;
				}
			}
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x00064B5D File Offset: 0x00063B5D
		public override long get_Length()
		{
			return this.h;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00064B68 File Offset: 0x00063B68
		public override int ReadByte()
		{
			if (this.g != null)
			{
				if (this.f == this.h)
				{
					return -1;
				}
				int num = (int)(this.g[(int)this.f] & byte.MaxValue);
				this.f += 1L;
				if (this.i)
				{
					num = ii.aa[num];
				}
				return num;
			}
			else
			{
				hp hp = this.d.a(this.e);
				long num2 = this.c.a(this.e);
				if (this.f + 1L > num2 + (long)hp.c)
				{
					this.e++;
					if (this.e >= this.d.Count)
					{
						return -1;
					}
					hp = this.d.a(this.e);
					num2 = this.c.a(this.e);
				}
				long num3 = hp.b + (this.f - num2);
				if (this.a.Position != num3)
				{
					this.a.Seek(num3, SeekOrigin.Begin);
				}
				int num4 = this.a.ReadByte();
				if (num4 < 0)
				{
					return -1;
				}
				if (this.i)
				{
					num4 = ii.aa[num4];
				}
				this.f += 1L;
				return num4;
			}
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00064CA4 File Offset: 0x00063CA4
		public bool c()
		{
			return this.k;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00064CAC File Offset: 0x00063CAC
		public int b(byte[] A_0)
		{
			this.k = false;
			if (this.f == this.h)
			{
				return -1;
			}
			if (this.g == null)
			{
				bool flag = false;
				int num = 0;
				while (!flag)
				{
					hp hp = this.d.a(this.e);
					long num2 = this.c.a(this.e);
					int num3 = (int)(this.f - num2);
					this.a.Seek(hp.b + (long)num3, SeekOrigin.Begin);
					long num4 = num2 + (long)hp.c;
					int num5 = A_0.Length - num;
					if (num5 > (int)(this.h - this.f))
					{
						num5 = (int)(this.h - this.f);
					}
					if (num4 >= this.f + (long)num5)
					{
						byte[] array = new byte[num5];
						this.a.Read(array, 0, array.Length);
						Array.Copy(array, 0, A_0, num, num5);
						num += num5;
						flag = true;
						this.f += (long)num5;
					}
					else
					{
						int num6 = hp.c - num3;
						byte[] array2 = new byte[num6];
						this.a.Read(array2, 0, array2.Length);
						Array.Copy(array2, 0, A_0, num, num6);
						num += num6;
						this.e++;
						this.f += (long)num6;
					}
					this.j++;
				}
				if (this.i)
				{
					ii.c(A_0);
					this.k = true;
				}
				return num;
			}
			int num7 = (int)(this.h - this.f);
			if (A_0.Length >= num7)
			{
				Array.Copy(this.g, (int)this.f, A_0, 0, num7);
				if (this.i)
				{
					ii.c(A_0);
					this.k = true;
				}
				this.f += (long)num7;
				return num7;
			}
			Array.Copy(this.g, (int)this.f, A_0, 0, A_0.Length);
			if (this.i)
			{
				ii.c(A_0);
				this.k = true;
			}
			this.f += (long)A_0.Length;
			return A_0.Length;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00064EC0 File Offset: 0x00063EC0
		public override int Read(byte[] output, int offset, int length)
		{
			if (this.f == this.h)
			{
				return -1;
			}
			if (output.Length < length)
			{
				length = output.Length;
			}
			byte[] array = new byte[length];
			int num = this.b(array);
			Array.Copy(array, 0, output, offset, num);
			return num;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00064F02 File Offset: 0x00063F02
		public void f()
		{
			this.e = 0;
			this.f = 0L;
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x00064F13 File Offset: 0x00063F13
		public bool d()
		{
			return false;
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00064F18 File Offset: 0x00063F18
		public virtual void a(long A_0)
		{
			if (A_0 > this.h)
			{
				throw new MailBeePstParsingException(string.Format(Resources.Instance.ErrorDesc_OutlookPstUnableToSeekPastEndOfItemSize0SeekingTo1, this.h, A_0), 1210);
			}
			if (this.f == A_0)
			{
				return;
			}
			long num = 0L;
			this.e = 0;
			if (this.g == null)
			{
				num = this.c.a(this.e + 1);
				while (A_0 >= num)
				{
					this.e++;
					if (this.e == this.c.Count - 1)
					{
						break;
					}
					num = this.c.a(this.e + 1);
				}
			}
			this.f = A_0;
			long num2 = 0L;
			if (this.g == null)
			{
				num2 = this.d.a(this.e).b;
			}
			long offset = num2 + (A_0 - num);
			this.a.Seek(offset, SeekOrigin.Begin);
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00065004 File Offset: 0x00064004
		public virtual long a(long A_0, int A_1)
		{
			this.a(A_0);
			byte[] a_ = new byte[A_1];
			this.b(a_);
			return ii.a(a_);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0006502D File Offset: 0x0006402D
		public override void Flush()
		{
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x0006502F File Offset: 0x0006402F
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00065033 File Offset: 0x00064033
		public override void SetLength(long value)
		{
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00065035 File Offset: 0x00064035
		public override void Write(byte[] buffer, int offset, int count)
		{
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00065037 File Offset: 0x00064037
		public override bool get_CanRead()
		{
			return true;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0006503A File Offset: 0x0006403A
		public override bool get_CanSeek()
		{
			return true;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x0006503D File Offset: 0x0006403D
		public override bool get_CanWrite()
		{
			return false;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00065040 File Offset: 0x00064040
		public override long get_Position()
		{
			return this.f;
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00065048 File Offset: 0x00064048
		public override void set_Position(long value)
		{
		}

		// Token: 0x040010A7 RID: 4263
		private Stream a;

		// Token: 0x040010A8 RID: 4264
		private bs b;

		// Token: 0x040010A9 RID: 4265
		private cj c;

		// Token: 0x040010AA RID: 4266
		private i d;

		// Token: 0x040010AB RID: 4267
		private int e;

		// Token: 0x040010AC RID: 4268
		private long f;

		// Token: 0x040010AD RID: 4269
		private byte[] g;

		// Token: 0x040010AE RID: 4270
		private long h;

		// Token: 0x040010AF RID: 4271
		private bool i;

		// Token: 0x040010B0 RID: 4272
		private int j;

		// Token: 0x040010B1 RID: 4273
		private bool k;
	}
}
