using System;
using System.IO;
using System.Text;

namespace a.b
{
	// Token: 0x0200024A RID: 586
	internal class ih : ab
	{
		// Token: 0x060013A8 RID: 5032 RVA: 0x0005A060 File Offset: 0x00059060
		public new void a(long A_0)
		{
			string text = "0FF6";
			base.b(text, "0102");
			byte[] a_ = new byte[]
			{
				0,
				0,
				0,
				(byte)A_0
			};
			base.a("__substg1.0_" + text, "0102", ab.a(a_));
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0005A0A8 File Offset: 0x000590A8
		public new void b(string A_0)
		{
			byte[] array = new byte[]
			{
				0,
				0,
				0,
				0,
				129,
				43,
				31,
				164,
				190,
				163,
				16,
				25,
				157,
				110,
				0,
				221,
				1,
				15,
				84,
				2,
				0,
				0,
				1,
				144
			};
			string text = "3001";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, this.o));
			text = "3003";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, this.o));
			text = "300B";
			base.b(text, "0102");
			base.a("__substg1.0_" + text, "0102", ab.a(Encoding.ASCII.GetBytes("SMTP:" + A_0.ToUpper() + "\0")));
			text = "5FF6";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, this.o));
			byte[] bytes = Encoding.ASCII.GetBytes(A_0 + "\0\0\0SMTP:" + A_0.ToUpper() + "\0");
			byte[] array2 = new byte[array.Length + bytes.Length * 2];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = array[i];
			}
			int num = 0;
			for (int j = array.Length; j < array2.Length; j += 2)
			{
				array2[j] = bytes[num++];
				array2[j + 1] = 0;
			}
			text = "5FF7";
			base.b(text, "0102");
			base.a("__substg1.0_" + text, "0102", ab.a(array2));
			text = "0FF9";
			base.b(text, "0102");
			base.a("__substg1.0_" + text, "0102", ab.a(array2));
			text = "0FFF";
			base.b(text, "0102");
			base.a("__substg1.0_" + text, "0102", ab.a(array2));
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0005A2BC File Offset: 0x000592BC
		public new void a(string A_0, Encoding A_1, string A_2)
		{
			byte[] array = new byte[]
			{
				0,
				0,
				0,
				0,
				129,
				43,
				31,
				164,
				190,
				163,
				16,
				25,
				157,
				110,
				0,
				221,
				1,
				15,
				84,
				2,
				0,
				0,
				1,
				144
			};
			string text = "3001";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, A_1, this.o));
			text = "3003";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_2, A_1, this.o));
			text = "300B";
			base.b(text, "0102");
			base.a("__substg1.0_" + text, "0102", ab.a(Encoding.ASCII.GetBytes("SMTP:" + A_2.ToUpper() + "\0")));
			text = "5FF6";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, A_1, this.o));
			byte[] bytes = Encoding.ASCII.GetBytes(A_0 + "\0SMTP\0" + A_2 + "\0");
			byte[] array2 = new byte[array.Length + bytes.Length * 2];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = array[i];
			}
			int num = 0;
			for (int j = array.Length; j < array2.Length; j += 2)
			{
				array2[j] = bytes[num++];
				array2[j + 1] = 0;
			}
			text = "5FF7";
			base.b(text, "0102");
			base.a("__substg1.0_" + text, "0102", ab.a(array2));
			text = "0FFF";
			base.b(text, "0102");
			base.a("__substg1.0_" + text, "0102", ab.a(array2));
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0005A4A0 File Offset: 0x000594A0
		public new void a(string A_0)
		{
			string text = "3002";
			base.b(text, base.g());
			base.a("__substg1.0_" + text, base.g(), ab.a(A_0, this.o));
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0005A4E4 File Offset: 0x000594E4
		public ih(ig A_0)
		{
			this.k = A_0;
			this.b();
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0005A4F9 File Offset: 0x000594F9
		public ih(ig A_0, bool A_1) : this(A_0)
		{
			this.o = A_1;
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0005A509 File Offset: 0x00059509
		public new void b()
		{
			this.m = new fj();
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0005A518 File Offset: 0x00059518
		public new void a()
		{
			try
			{
				byte[] array = this.m.du();
				byte[] array2 = new byte[array.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = array[i];
				}
				Stream a_ = new MemoryStream(array2);
				this.k.em("__properties_version1.0", a_);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0005A57C File Offset: 0x0005957C
		public new void a(long A_0, int A_1)
		{
			this.m.a(Convert.ToInt64("0E0F", 16), true);
			base.a(this.k, "0C15", "0003", (long)A_1);
			base.a(this.k, "3000", "0003", A_0);
			base.a(this.k, "3900", "0003", 0L);
			base.a(this.k, "5FFF", "0003", 0L);
		}
	}
}
