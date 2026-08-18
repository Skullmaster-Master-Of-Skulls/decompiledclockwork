using System;
using System.Collections;
using System.IO;
using System.Text;

namespace a.b
{
	// Token: 0x020002E4 RID: 740
	internal abstract class ed : gi, gj
	{
		// Token: 0x06001A1D RID: 6685 RVA: 0x00073768 File Offset: 0x00072768
		protected ed()
		{
			this.ak = new byte[128];
			for (int i = 0; i < this.ak.Length; i++)
			{
				this.ak[i] = 0;
			}
			this.u = new fp(64);
			this.v = new dv(66);
			this.x = new dv(67);
			this.z = new id(68, -1, this.ak);
			this.aa = new id(72, -1, this.ak);
			this.ab = new id(76, -1, this.ak);
			this.ac = new ar(this.ak, 80);
			this.ad = new id(96, 0, this.ak);
			this.ae = new id(100, 0, this.ak);
			this.af = new id(104, 0, this.ak);
			this.ag = new id(108, 0, this.ak);
			this.ah = new id(112, 0, this.ak);
			this.ai = new id(116);
			this.aj = new id(120, 0, this.ak);
			this.al = -1;
			this.a("");
			this.z(null);
			this.x(null);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x000738C4 File Offset: 0x000728C4
		protected ed(int A_0, byte[] A_1, int A_2)
		{
			this.ak = new byte[128];
			Array.Copy(A_1, A_2, this.ak, 0, 128);
			this.u = new fp(64, this.ak);
			this.v = new dv(66, this.ak);
			this.x = new dv(67, this.ak);
			this.z = new id(68, this.ak);
			this.aa = new id(72, this.ak);
			this.ab = new id(76, this.ak);
			this.ac = new ar(this.ak, 80);
			this.ad = new id(96, 0, this.ak);
			this.ae = new id(100, this.ak);
			this.af = new id(104, this.ak);
			this.ag = new id(108, this.ak);
			this.ah = new id(112, this.ak);
			this.ai = new id(116, this.ak);
			this.aj = new id(120, this.ak);
			this.al = A_0;
			int num = (int)(this.u.a() / 2 - 1);
			if (num < 1)
			{
				this.t = "";
			}
			else
			{
				char[] array = new char[num];
				int num2 = 0;
				for (int i = 0; i < num; i++)
				{
					array[i] = (char)new fp(num2, this.ak).a();
					num2 += 2;
				}
				this.t = new string(array, 0, num);
			}
			this.am = null;
			this.an = null;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x00073A77 File Offset: 0x00072A77
		public void a(Stream A_0)
		{
			A_0.Write(this.ak, 0, this.ak.Length);
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x00073A8E File Offset: 0x00072A8E
		public void c(int A_0)
		{
			this.ai.a(A_0, this.ak);
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x00073AA2 File Offset: 0x00072AA2
		public int i()
		{
			return this.ai.a();
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00073AAF File Offset: 0x00072AAF
		public bool g()
		{
			return ed.b(this.aj.a());
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00073AC1 File Offset: 0x00072AC1
		public static bool b(int A_0)
		{
			return A_0 < 4096;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00073ACB File Offset: 0x00072ACB
		public string f()
		{
			return this.t;
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00073AD4 File Offset: 0x00072AD4
		public void a(string A_0)
		{
			char[] array = A_0.ToCharArray();
			int num = Math.Min(array.Length, 31);
			this.t = new string(array, 0, num);
			short num2 = 0;
			int i;
			for (i = 0; i < num; i++)
			{
				fp.a((int)num2, (short)array[i], ref this.ak);
				num2 += 2;
			}
			while (i < 32)
			{
				fp.a((int)num2, 0, ref this.ak);
				num2 += 2;
				i++;
			}
			this.u.a((short)((num + 1) * 2), ref this.ak);
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00073B58 File Offset: 0x00072B58
		public virtual bool lj()
		{
			return false;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00073B5B File Offset: 0x00072B5B
		public ar d()
		{
			return this.ac;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00073B64 File Offset: 0x00072B64
		public void a(ar A_0)
		{
			this.ac = A_0;
			if (A_0 == null)
			{
				for (int i = 80; i < 96; i++)
				{
					this.ak[i] = 0;
				}
				return;
			}
			A_0.a(this.ak, 80);
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x00073BA1 File Offset: 0x00072BA1
		public void b(byte A_0)
		{
			this.v.a(A_0, this.ak);
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00073BB5 File Offset: 0x00072BB5
		public void a(byte A_0)
		{
			this.x.a(A_0, this.ak);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00073BC9 File Offset: 0x00072BC9
		public void e(int A_0)
		{
			this.ab.a(A_0, this.ak);
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00073BDD File Offset: 0x00072BDD
		public int j()
		{
			return this.ab.a();
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x00073BEA File Offset: 0x00072BEA
		public virtual void oo(int A_0)
		{
			this.aj.a(A_0, this.ak);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00073BFE File Offset: 0x00072BFE
		public virtual int h()
		{
			return this.aj.a();
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x00073C0B File Offset: 0x00072C0B
		public int b()
		{
			return this.al;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x00073C13 File Offset: 0x00072C13
		public void d(int A_0)
		{
			this.al = A_0;
		}

		// Token: 0x06001A31 RID: 6705
		public abstract void lk();

		// Token: 0x06001A32 RID: 6706 RVA: 0x00073C1C File Offset: 0x00072C1C
		public int e()
		{
			return this.aa.a();
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00073C29 File Offset: 0x00072C29
		public int c()
		{
			return this.z.a();
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00073C36 File Offset: 0x00072C36
		public static bool a(int A_0)
		{
			return A_0 != -1;
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00073C3F File Offset: 0x00072C3F
		public void x(gi A_0)
		{
			this.an = A_0;
			this.z.a((A_0 == null) ? -1 : ((ed)A_0).b(), this.ak);
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x00073C6A File Offset: 0x00072C6A
		public gi w()
		{
			return this.an;
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00073C72 File Offset: 0x00072C72
		public void z(gi A_0)
		{
			this.am = A_0;
			this.aa.a((A_0 == null) ? -1 : ((ed)A_0).b(), this.ak);
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00073C9D File Offset: 0x00072C9D
		public gi y()
		{
			return this.am;
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00073CA8 File Offset: 0x00072CA8
		public Array ji()
		{
			string[] array = new string[5];
			array.SetValue("Name          = \"" + this.f() + "\"", 0);
			array.SetValue("Property Type = " + this.v.a(), 1);
			array.SetValue("Node Color    = " + this.x.a(), 2);
			long num = (long)this.af.a();
			num <<= 32;
			num += ((long)this.ae.a() & 65535L);
			array.SetValue("Time 1        = " + num, 3);
			num = (long)this.ah.a();
			num <<= 32;
			num += ((long)this.ag.a() & 65535L);
			array.SetValue("Time 2        = " + num, 4);
			return array;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00073D97 File Offset: 0x00072D97
		public IEnumerator jj()
		{
			return ArrayList.ReadOnly(new ArrayList()).GetEnumerator();
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x00073DA8 File Offset: 0x00072DA8
		public bool jk()
		{
			return true;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x00073DAB File Offset: 0x00072DAB
		public string jl()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Property: \"").Append(this.f()).Append("\"");
			return stringBuilder.ToString();
		}

		// Token: 0x040012A6 RID: 4774
		private const byte a = 0;

		// Token: 0x040012A7 RID: 4775
		private const int b = 64;

		// Token: 0x040012A8 RID: 4776
		private const int c = 31;

		// Token: 0x040012A9 RID: 4777
		protected const int d = -1;

		// Token: 0x040012AA RID: 4778
		private const int e = 67;

		// Token: 0x040012AB RID: 4779
		private const int f = 68;

		// Token: 0x040012AC RID: 4780
		private const int g = 72;

		// Token: 0x040012AD RID: 4781
		private const int h = 76;

		// Token: 0x040012AE RID: 4782
		private const int i = 80;

		// Token: 0x040012AF RID: 4783
		private const int j = 96;

		// Token: 0x040012B0 RID: 4784
		private const int k = 100;

		// Token: 0x040012B1 RID: 4785
		private const int l = 104;

		// Token: 0x040012B2 RID: 4786
		private const int m = 108;

		// Token: 0x040012B3 RID: 4787
		private const int n = 112;

		// Token: 0x040012B4 RID: 4788
		private const int o = 116;

		// Token: 0x040012B5 RID: 4789
		private const int p = 120;

		// Token: 0x040012B6 RID: 4790
		protected const byte q = 1;

		// Token: 0x040012B7 RID: 4791
		protected const byte r = 0;

		// Token: 0x040012B8 RID: 4792
		private const int s = 4096;

		// Token: 0x040012B9 RID: 4793
		private string t;

		// Token: 0x040012BA RID: 4794
		private fp u;

		// Token: 0x040012BB RID: 4795
		private dv v;

		// Token: 0x040012BC RID: 4796
		private dv x;

		// Token: 0x040012BD RID: 4797
		private id z;

		// Token: 0x040012BE RID: 4798
		private id aa;

		// Token: 0x040012BF RID: 4799
		private id ab;

		// Token: 0x040012C0 RID: 4800
		private ar ac;

		// Token: 0x040012C1 RID: 4801
		private id ad;

		// Token: 0x040012C2 RID: 4802
		private id ae;

		// Token: 0x040012C3 RID: 4803
		private id af;

		// Token: 0x040012C4 RID: 4804
		private id ag;

		// Token: 0x040012C5 RID: 4805
		private id ah;

		// Token: 0x040012C6 RID: 4806
		private id ai;

		// Token: 0x040012C7 RID: 4807
		private id aj;

		// Token: 0x040012C8 RID: 4808
		private byte[] ak;

		// Token: 0x040012C9 RID: 4809
		private int al;

		// Token: 0x040012CA RID: 4810
		private gi am;

		// Token: 0x040012CB RID: 4811
		private gi an;
	}
}
