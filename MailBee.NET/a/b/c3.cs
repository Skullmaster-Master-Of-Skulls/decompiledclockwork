using System;
using System.IO;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002F7 RID: 759
	internal class c3 : ae
	{
		// Token: 0x06001AC7 RID: 6855 RVA: 0x00075740 File Offset: 0x00074740
		public c3(Stream A_0)
		{
			try
			{
				A_0.Position = 0L;
				this.a(c3.a(A_0));
				if (this.b.f() != 512)
				{
					byte[] a_ = new byte[this.b.f() - 512];
					g9.a(A_0, a_);
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x000757AC File Offset: 0x000747AC
		public c3(he A_0) : this(g9.a(A_0, 512))
		{
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x000757C0 File Offset: 0x000747C0
		public c3(byte[] A_0)
		{
			try
			{
				this.a(A_0);
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x000757F0 File Offset: 0x000747F0
		public new void a(byte[] A_0)
		{
			this.i = A_0;
			long num = p.g(this.i, 0);
			if (num == -2226271756974174256L)
			{
				if (this.i[30] == 12)
				{
					this.b = c5.d;
				}
				else
				{
					if (this.i[30] != 9)
					{
						throw new MailBeeOutlookMsgNotFoundException("Unsupported blocksize  (2^" + this.i[30] + "). Expected 2^9 or 2^12.", 1200);
					}
					this.b = c5.b;
				}
				this.c = new id(44, this.i).a();
				this.d = new id(48, this.i).a();
				this.e = new id(60, this.i).a();
				this.f = new id(64, this.i).a();
				this.g = new id(68, this.i).a();
				this.h = new id(72, this.i).a();
				return;
			}
			byte[] o = c5.o;
			if (this.i[0] == o[0] && this.i[1] == o[1] && this.i[2] == o[2] && this.i[3] == o[3])
			{
				throw new OfficeXmlFileException("The supplied data appears to be in the Office 2007+ XML. You are calling the part of POI that deals with OLE2 Office Documents. You need to call a different part of POI to process this data (eg XSSF instead of HSSF)");
			}
			if ((num & -31525197391593473L) == 4503608217567241L)
			{
				throw new ArgumentException("The supplied data appears to be in BIFF2 format.  POI only supports BIFF8 format");
			}
			throw new IOException("Invalid header signature; read " + c3.a(num) + ", expected " + c3.a(-2226271756974174256L));
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0007599C File Offset: 0x0007499C
		public c3(y A_0)
		{
			this.b = A_0;
			this.i = new byte[512];
			for (int i = 0; i < this.i.Length; i++)
			{
				this.i[i] = c3.j;
			}
			new r(0, -2226271756974174256L, this.i);
			new id(8, 0, this.i);
			new id(12, 0, this.i);
			new id(16, 0, this.i);
			new id(20, 0, this.i);
			new fp(24, 59, ref this.i);
			new fp(26, 3, ref this.i);
			new fp(28, -2, ref this.i);
			new fp(30, A_0.d(), ref this.i);
			new id(32, 6, this.i);
			new id(36, 0, this.i);
			new id(40, 0, this.i);
			new id(52, 0, this.i);
			new id(56, 4096, this.i);
			this.c = 0;
			this.f = 0;
			this.h = 0;
			this.d = -2;
			this.e = -2;
			this.g = -2;
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x00075AF8 File Offset: 0x00074AF8
		private new static byte[] a(Stream A_0)
		{
			byte[] array = new byte[512];
			int num = g9.a(A_0, array);
			if (num != 512)
			{
				c3.a(num, 512);
			}
			return array;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x00075B2D File Offset: 0x00074B2D
		private new static string a(long A_0)
		{
			return new string(f5.b(A_0));
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x00075B3C File Offset: 0x00074B3C
		private new static MailBeeOutlookMsgBuildingException a(int A_0, int A_1)
		{
			if (A_0 < 0)
			{
				A_0 = 0;
			}
			string text = " byte" + ((A_0 == 1) ? "" : "s");
			return new MailBeeOutlookMsgBuildingException(string.Concat(new object[]
			{
				"Unable to Read entire header; ",
				A_0,
				text,
				" Read; expected ",
				A_1,
				" bytes"
			}), 1201);
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x00075BAE File Offset: 0x00074BAE
		public new int g()
		{
			return this.d;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x00075BB6 File Offset: 0x00074BB6
		public new void g(int A_0)
		{
			this.d = A_0;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00075BBF File Offset: 0x00074BBF
		public new int e()
		{
			return this.e;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x00075BC7 File Offset: 0x00074BC7
		public new void h(int A_0)
		{
			this.e = A_0;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00075BD0 File Offset: 0x00074BD0
		public new int c()
		{
			return this.f;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00075BD8 File Offset: 0x00074BD8
		public new void c(int A_0)
		{
			this.f = A_0;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00075BE1 File Offset: 0x00074BE1
		public new int i()
		{
			return this.f;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00075BE9 File Offset: 0x00074BE9
		public new void e(int A_0)
		{
			this.f = A_0;
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00075BF2 File Offset: 0x00074BF2
		public new int f()
		{
			return this.c;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00075BFA File Offset: 0x00074BFA
		public new void d(int A_0)
		{
			this.c = A_0;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00075C04 File Offset: 0x00074C04
		public new int[] d()
		{
			int[] array = new int[Math.Min(this.c, 109)];
			int num = 76;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = p.i(this.i, num);
				num += 4;
			}
			return array;
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00075C4C File Offset: 0x00074C4C
		public new void a(int[] A_0)
		{
			int num = Math.Min(A_0.Length, 109);
			int num2 = 109 - num;
			int num3 = 76;
			for (int i = 0; i < num; i++)
			{
				p.c(this.i, num3, A_0[i]);
				num3 += 4;
			}
			for (int j = 0; j < num2; j++)
			{
				p.c(this.i, num3, -1);
				num3 += 4;
			}
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00075CAC File Offset: 0x00074CAC
		public new int a()
		{
			return this.h;
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00075CB4 File Offset: 0x00074CB4
		public new void a(int A_0)
		{
			this.h = A_0;
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00075CBD File Offset: 0x00074CBD
		public new int h()
		{
			return this.g;
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00075CC5 File Offset: 0x00074CC5
		public new void f(int A_0)
		{
			this.h = A_0;
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00075CCE File Offset: 0x00074CCE
		public new void b(int A_0)
		{
			this.g = A_0;
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x00075CD7 File Offset: 0x00074CD7
		public new y b()
		{
			return this.b;
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00075CE0 File Offset: 0x00074CE0
		public new void b(Stream A_0)
		{
			try
			{
				new id(44, this.c, this.i);
				new id(48, this.d, this.i);
				new id(60, this.e, this.i);
				new id(64, this.f, this.i);
				new id(68, this.g, this.i);
				new id(72, this.h, this.i);
				A_0.Write(this.i, 0, 512);
				for (int i = 512; i < this.b.f(); i++)
				{
					A_0.WriteByte(0);
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}

		// Token: 0x040012F7 RID: 4855
		private new static dm a = gn.a(typeof(c3));

		// Token: 0x040012F8 RID: 4856
		private new y b;

		// Token: 0x040012F9 RID: 4857
		private new int c;

		// Token: 0x040012FA RID: 4858
		private new int d;

		// Token: 0x040012FB RID: 4859
		private new int e;

		// Token: 0x040012FC RID: 4860
		private new int f;

		// Token: 0x040012FD RID: 4861
		private new int g;

		// Token: 0x040012FE RID: 4862
		private new int h;

		// Token: 0x040012FF RID: 4863
		private new byte[] i;

		// Token: 0x04001300 RID: 4864
		private new static byte j = byte.MaxValue;
	}
}
