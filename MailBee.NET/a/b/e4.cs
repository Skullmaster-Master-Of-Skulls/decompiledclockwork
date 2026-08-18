using System;

namespace a.b
{
	// Token: 0x0200024D RID: 589
	internal class e4 : ho
	{
		// Token: 0x060013B2 RID: 5042 RVA: 0x0005A60C File Offset: 0x0005960C
		public string b()
		{
			return this.d;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0005A614 File Offset: 0x00059614
		public void e(string A_0)
		{
			this.d = A_0;
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0005A61D File Offset: 0x0005961D
		public string c()
		{
			return this.a;
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0005A625 File Offset: 0x00059625
		public void d(string A_0)
		{
			this.a = A_0;
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0005A62E File Offset: 0x0005962E
		public string a()
		{
			return this.b;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0005A636 File Offset: 0x00059636
		public void c(string A_0)
		{
			this.b = A_0;
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0005A63F File Offset: 0x0005963F
		public string g()
		{
			return this.c;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0005A647 File Offset: 0x00059647
		public void b(string A_0)
		{
			this.c = A_0;
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0005A650 File Offset: 0x00059650
		public string h()
		{
			return this.e;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0005A658 File Offset: 0x00059658
		public void a(string A_0)
		{
			this.e = A_0;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0005A661 File Offset: 0x00059661
		public byte[] e()
		{
			return this.f;
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0005A669 File Offset: 0x00059669
		public void a(byte[] A_0)
		{
			this.f = A_0;
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0005A672 File Offset: 0x00059672
		public long d()
		{
			return this.g;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0005A67A File Offset: 0x0005967A
		public void a(long A_0)
		{
			this.g = A_0;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0005A683 File Offset: 0x00059683
		public bool f()
		{
			return this.h;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0005A68C File Offset: 0x0005968C
		public void a(string A_0, object A_1, h4 A_2, ref int A_3)
		{
			if (A_0 == null || A_1 == null)
			{
				return;
			}
			A_0 = string.Intern(A_0);
			uint num = global::b.a(A_0);
			if (num <= 2370703267U)
			{
				if (num <= 534276025U)
				{
					if (num != 299789532U)
					{
						if (num != 534276025U)
						{
							return;
						}
						if (!(A_0 == "3001"))
						{
							return;
						}
						if (this.c() == null)
						{
							this.d(b8.a((string)A_1));
							return;
						}
					}
					else
					{
						if (!(A_0 == "properties"))
						{
							return;
						}
						if (A_1 is byte[])
						{
							int num2 = (((byte[])A_1).Length - 8) / 16;
							byte[][] array = new byte[num2][];
							for (int i = 0; i < num2; i++)
							{
								array[i] = new byte[16];
								Buffer.BlockCopy((byte[])A_1, i * 16 + 8, array[i], 0, 16);
							}
							for (int j = 0; j < num2; j++)
							{
								int num3 = (int)BitConverter.ToInt16(array[j], 0);
								int num4 = (int)BitConverter.ToInt16(array[j], 2);
								if (num3 == 3 && num4 == 14085)
								{
									int num5 = BitConverter.ToInt32(array[j], 8);
									this.h = (num5 == 5);
								}
							}
						}
					}
					return;
				}
				if (num != 1943621528U)
				{
					if (num != 2370703267U)
					{
						return;
					}
					if (!(A_0 == "contents"))
					{
						return;
					}
					this.a((long)A_2.oy());
					this.a((byte[])A_1);
					this.e(".bmp");
					this.a("outlook_rtf_" + A_3 + this.b());
					A_3++;
					return;
				}
				else
				{
					if (!(A_0 == "3712"))
					{
						return;
					}
					this.a(b8.a((string)A_1));
					return;
				}
			}
			else if (num <= 4158414331U)
			{
				if (num != 2799427192U)
				{
					if (num != 4158414331U)
					{
						return;
					}
					if (!(A_0 == "3704"))
					{
						return;
					}
					this.d(b8.a((string)A_1));
					return;
				}
				else
				{
					if (!(A_0 == "370e"))
					{
						return;
					}
					this.b(b8.a((string)A_1));
					return;
				}
			}
			else if (num != 4175191950U)
			{
				if (num != 4208747188U)
				{
					if (num != 4242302426U)
					{
						return;
					}
					if (!(A_0 == "3703"))
					{
						return;
					}
					this.e(b8.a((string)A_1));
					return;
				}
				else
				{
					if (!(A_0 == "3701"))
					{
						return;
					}
					this.a((long)A_2.oy());
					this.a((byte[])A_1);
					return;
				}
			}
			else
			{
				if (!(A_0 == "3707"))
				{
					return;
				}
				this.c(b8.a((string)A_1));
				return;
			}
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0005A924 File Offset: 0x00059924
		public override string ToString()
		{
			if (this.b != null)
			{
				return this.b;
			}
			return this.a;
		}

		// Token: 0x04000FA8 RID: 4008
		protected internal string a;

		// Token: 0x04000FA9 RID: 4009
		protected internal string b;

		// Token: 0x04000FAA RID: 4010
		protected internal string c;

		// Token: 0x04000FAB RID: 4011
		protected internal string d;

		// Token: 0x04000FAC RID: 4012
		protected internal string e;

		// Token: 0x04000FAD RID: 4013
		protected internal byte[] f;

		// Token: 0x04000FAE RID: 4014
		protected internal long g = -1L;

		// Token: 0x04000FAF RID: 4015
		protected internal bool h;
	}
}
