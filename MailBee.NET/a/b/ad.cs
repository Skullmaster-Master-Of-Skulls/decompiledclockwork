using System;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000273 RID: 627
	internal class ad : i8
	{
		// Token: 0x0600167B RID: 5755 RVA: 0x00066C03 File Offset: 0x00065C03
		internal ad(di A_0, fb A_1) : this(A_0, A_1, -1)
		{
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00066C10 File Offset: 0x00065C10
		internal ad(di A_0, fb A_1, int A_2) : base(A_0, A_1)
		{
			if (this.b != 124)
			{
				throw new MailBeePstParsingException(Resources.Instance.ErrorDesc_OutlookPstUnableToCreate7cTable, 1210);
			}
			i8.a a = this.a(this.c);
			int num = 0;
			this.d = (int)a.a((long)(num + 1), 1);
			a.a((long)(num + 2), 2);
			a.a((long)(num + 4), 2);
			this.g = (int)a.a((long)(num + 6), 2);
			this.e = (int)a.a((long)(num + 8), 2);
			int num2 = (int)a.a((long)(num + 10), 4);
			int num3 = (int)a.a((long)(num + 14), 4);
			num += 22;
			if (this.d != 0)
			{
				this.i = new ad.a[this.d];
				for (int i = 0; i < this.d; i++)
				{
					this.i[i] = new ad.a(this, a, num);
					if (this.i[i].c == A_2)
					{
						this.h = i;
					}
					num += 8;
				}
			}
			if (this.h > -1)
			{
				this.d = this.h + 1;
			}
			this.j = new ec();
			i8.a a2 = this.a(this.g);
			this.h = a2.a() / (this.e + this.f);
			num = 0;
			for (int j = 0; j < this.h; j++)
			{
				int a_ = (int)a2.a((long)num, this.e);
				num += this.e;
				int a_2 = (int)a2.a((long)num, this.f);
				num += this.f;
				this.j.a(a_, a_2);
			}
			this.f = this.a(num3);
			this.l.Append(string.Format("Number of keys: {0}\nNumber of columns: {1}\nRow Size: {2}\nhidRowIndex: {3}\nhnidRows: {4}\n", new object[]
			{
				this.h,
				this.d,
				this.e,
				num2,
				num3
			}));
			int num4 = this.f.a() / 8176;
			int num5 = 8176 / this.e;
			int num6 = this.e;
			this.c = num4 * num5 + this.f.a() % 8176 / this.e;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00066E7D File Offset: 0x00065E7D
		public override int a4()
		{
			return this.c;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00066E85 File Offset: 0x00065E85
		public new virtual string b()
		{
			if (this.b == null)
			{
				return string.Empty;
			}
			return this.b.ToString();
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00066EA0 File Offset: 0x00065EA0
		public new d8 a()
		{
			if (this.b == null)
			{
				this.b = this.a(-1, -1);
			}
			return this.b;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00066EC0 File Offset: 0x00065EC0
		public new d8 a(int A_0, int A_1)
		{
			d8 d = new d8();
			int num = this.f.a() / 8176;
			int num2 = 8176 / this.e;
			int num3 = 8176 - num2 * this.e;
			this.c = num * num2 + this.f.a() % 8176 / this.e;
			if (A_0 == -1)
			{
				A_1 = this.c;
				A_0 = 0;
			}
			int num4 = A_0 / num2 * 8176 + A_0 % num2 * this.e;
			if (A_1 > this.a4() - A_0)
			{
				A_1 = this.a4() - A_0;
			}
			int num5 = 0;
			int i = 0;
			while (i < A_1)
			{
				ew ew = new ew();
				if (this.f.c.e().f() == 14)
				{
					if (num4 >= 8176)
					{
						num4 += 4 * (num4 / 8176);
					}
					if ((long)(this.f.a + num4 + this.g) <= this.f.c.Length)
					{
						goto IL_135;
					}
				}
				else
				{
					if (num4 % 8176 <= 8176 - this.e)
					{
						goto IL_135;
					}
					num4 += num3;
					if (num4 + this.e >= this.f.a())
					{
						goto IL_135;
					}
				}
				IL_499:
				i++;
				continue;
				IL_135:
				byte[] array = new byte[(this.d + 7) / 8];
				this.f.c.a((long)(this.f.a + num4 + this.g));
				this.f.c.b(array);
				int num6 = (int)this.f.a((long)num4, 4);
				bh bh = new bh();
				bh.d = -1;
				bh.f = 3;
				bh.e = 26610;
				bh.g = num6;
				bh.i = true;
				ew.a(bh.e, bh);
				int j = 0;
				if (this.h > -1)
				{
					j = this.h;
				}
				while (j < this.d)
				{
					int num7 = this.i[j].f / 8;
					int num8 = this.i[j].f % 8;
					if (((int)array[num7] & 1 << num8) != 0)
					{
						bh = new bh();
						bh.d = j;
						bh.f = this.i[j].b;
						bh.e = this.i[j].c;
						bh.g = 0;
						int num9 = this.i[j].e;
						if (num9 != 1)
						{
							if (num9 != 2)
							{
								if (num9 != 8)
								{
									bh.g = (int)this.f.a((long)(num4 + this.i[j].d), 4);
									if (this.i[j].b == 3 || this.i[j].b == 4 || this.i[j].b == 10)
									{
										bh.i = true;
									}
									else if ((bh.g & 31) != 0)
									{
										bh.i = true;
									}
									else if (bh.g == 0)
									{
										bh.h = new byte[0];
									}
									else
									{
										i8.a a = this.a(bh.g);
										if (a != null)
										{
											bh.h = new byte[a.a()];
											a.c.a((long)a.a);
											a.c.b(bh.h);
										}
									}
								}
								else
								{
									bh.h = new byte[8];
									this.f.c.a((long)(this.f.a + num4 + this.i[j].d));
									this.f.c.b(bh.h);
								}
							}
							else
							{
								bh.g = ((int)this.f.a((long)(num4 + this.i[j].d), 2) & 65535);
								bh.i = true;
							}
						}
						else
						{
							bh.g = ((int)this.f.a((long)(num4 + this.i[j].d), 1) & 255);
							bh.i = true;
						}
						ew.a(bh.e, bh);
						this.l.Append(bh.ToString());
						this.l.Append("\n\n");
					}
					j++;
				}
				d.a(num5, ew);
				num5++;
				num4 += this.e;
				goto IL_499;
			}
			return d;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00067375 File Offset: 0x00066375
		public override string ToString()
		{
			return this.l.ToString();
		}

		// Token: 0x040010C6 RID: 4294
		private new const int a = 8176;

		// Token: 0x040010C7 RID: 4295
		private new d8 b;

		// Token: 0x040010C8 RID: 4296
		private new int c;

		// Token: 0x040010C9 RID: 4297
		private new int d;

		// Token: 0x040010CA RID: 4298
		private new int e;

		// Token: 0x040010CB RID: 4299
		private new i8.a f;

		// Token: 0x040010CC RID: 4300
		private new int g;

		// Token: 0x040010CD RID: 4301
		private new int h = -1;

		// Token: 0x040010CE RID: 4302
		internal new ad.a[] i;

		// Token: 0x040010CF RID: 4303
		private ec j;

		// Token: 0x02000274 RID: 628
		internal new class a
		{
			// Token: 0x06001682 RID: 5762 RVA: 0x00067382 File Offset: 0x00066382
			private void a(ad A_0)
			{
				this.a = A_0;
			}

			// Token: 0x06001683 RID: 5763 RVA: 0x0006738B File Offset: 0x0006638B
			public ad a()
			{
				return this.a;
			}

			// Token: 0x06001684 RID: 5764 RVA: 0x00067394 File Offset: 0x00066394
			internal a(ad A_0, i8.a A_1, int A_2)
			{
				this.a(A_0);
				this.b = ((int)A_1.a((long)A_2, 2) & 65535);
				this.c = (int)(A_1.a((long)(A_2 + 2), 2) & 65535L);
				this.d = (int)(A_1.a((long)(A_2 + 4), 2) & 65535L);
				this.e = (A_1.c.ReadByte() & 255);
				this.f = (A_1.c.ReadByte() & 255);
			}

			// Token: 0x040010D0 RID: 4304
			private ad a;

			// Token: 0x040010D1 RID: 4305
			internal int b;

			// Token: 0x040010D2 RID: 4306
			internal int c;

			// Token: 0x040010D3 RID: 4307
			internal int d;

			// Token: 0x040010D4 RID: 4308
			internal int e;

			// Token: 0x040010D5 RID: 4309
			internal int f;
		}
	}
}
