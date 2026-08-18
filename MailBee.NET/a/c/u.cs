using System;
using System.Xml;
using iTextSharp.text;
using MailBee.Pdf;

namespace a.c
{
	// Token: 0x02000230 RID: 560
	internal class u
	{
		// Token: 0x060012AD RID: 4781 RVA: 0x0005395F File Offset: 0x0005295F
		public void c(Color A_0)
		{
			this.a = A_0;
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00053968 File Offset: 0x00052968
		public Color g()
		{
			if (!this.n)
			{
				return this.a;
			}
			return Color.WHITE;
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0005397E File Offset: 0x0005297E
		public void d(Color A_0)
		{
			this.b = A_0;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00053987 File Offset: 0x00052987
		public Color n()
		{
			if (!this.n)
			{
				return this.b;
			}
			return Color.WHITE;
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0005399D File Offset: 0x0005299D
		public void b(float A_0)
		{
			this.c = A_0;
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x000539A6 File Offset: 0x000529A6
		public float j()
		{
			if (!this.n)
			{
				return this.c;
			}
			return 1f;
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x000539BC File Offset: 0x000529BC
		public void a(Color A_0)
		{
			this.d = A_0;
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x000539C5 File Offset: 0x000529C5
		public Color m()
		{
			if (!this.n)
			{
				return this.d;
			}
			return Color.BLACK;
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x000539DB File Offset: 0x000529DB
		public void a(float A_0)
		{
			this.g = A_0;
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x000539E4 File Offset: 0x000529E4
		public float k()
		{
			return this.g;
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x000539EC File Offset: 0x000529EC
		public void d(float A_0)
		{
			this.e = A_0;
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x000539F5 File Offset: 0x000529F5
		public float d()
		{
			return this.e;
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x000539FD File Offset: 0x000529FD
		public void c(float A_0)
		{
			this.f = A_0;
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00053A06 File Offset: 0x00052A06
		public float l()
		{
			return this.f;
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00053A0E File Offset: 0x00052A0E
		public void b(Color A_0)
		{
			this.h = A_0;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00053A17 File Offset: 0x00052A17
		public Color e()
		{
			if (!this.n)
			{
				return this.h;
			}
			return Color.BLACK;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00053A2D File Offset: 0x00052A2D
		public void a(int A_0)
		{
			this.i = A_0;
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00053A36 File Offset: 0x00052A36
		public int c()
		{
			return this.i;
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00053A3E File Offset: 0x00052A3E
		public void b(int A_0)
		{
			this.j = A_0;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00053A47 File Offset: 0x00052A47
		public int q()
		{
			return this.j;
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00053A4F File Offset: 0x00052A4F
		public void a(string A_0)
		{
			this.l = A_0;
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00053A58 File Offset: 0x00052A58
		public string p()
		{
			return this.l;
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00053A60 File Offset: 0x00052A60
		public void a(Font A_0)
		{
			this.k = A_0;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00053A69 File Offset: 0x00052A69
		public Font b()
		{
			return this.k;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00053A71 File Offset: 0x00052A71
		public void a(XmlNode A_0)
		{
			this.m = A_0;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00053A7A File Offset: 0x00052A7A
		public XmlNode f()
		{
			return this.m;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00053A82 File Offset: 0x00052A82
		public void a(bool A_0)
		{
			this.n = A_0;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00053A8B File Offset: 0x00052A8B
		public bool i()
		{
			return this.n;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00053A93 File Offset: 0x00052A93
		public void a(ConvertXmlNodeToPdfDelegate A_0)
		{
			this.o = A_0;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00053A9C File Offset: 0x00052A9C
		public ConvertXmlNodeToPdfDelegate h()
		{
			return this.o;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00053AA4 File Offset: 0x00052AA4
		public void a(ProcessImagePathDelegate A_0)
		{
			this.p = A_0;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00053AAD File Offset: 0x00052AAD
		public ProcessImagePathDelegate o()
		{
			return this.p;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00053AB5 File Offset: 0x00052AB5
		public void b(bool A_0)
		{
			this.q = A_0;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00053ABE File Offset: 0x00052ABE
		public bool a()
		{
			return this.q;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00053AC8 File Offset: 0x00052AC8
		public u(XmlNode A_0)
		{
			this.m = A_0;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00053B54 File Offset: 0x00052B54
		public u(XmlNode A_0, u A_1) : this(A_0)
		{
			this.a = A_1.g();
			this.b = A_1.n();
			this.c = A_1.j();
			this.d = A_1.m();
			this.e = A_1.d();
			this.f = A_1.l();
			this.g = A_1.k();
			this.h = A_1.e();
			this.i = A_1.c();
			this.j = A_1.q();
			this.l = A_1.p();
			this.k = A_1.b();
			this.n = A_1.i();
			this.o = A_1.h();
			this.p = A_1.o();
			this.q = A_1.a();
		}

		// Token: 0x04000F48 RID: 3912
		private Color a = Color.WHITE;

		// Token: 0x04000F49 RID: 3913
		private Color b = Color.WHITE;

		// Token: 0x04000F4A RID: 3914
		private float c = 0.5f;

		// Token: 0x04000F4B RID: 3915
		private Color d = Color.BLACK;

		// Token: 0x04000F4C RID: 3916
		private float e = 3f;

		// Token: 0x04000F4D RID: 3917
		private float f;

		// Token: 0x04000F4E RID: 3918
		private float g = (float)((double)PageSize.A4.Width * 0.9);

		// Token: 0x04000F4F RID: 3919
		private Color h = Color.BLACK;

		// Token: 0x04000F50 RID: 3920
		private int i;

		// Token: 0x04000F51 RID: 3921
		private int j = 8;

		// Token: 0x04000F52 RID: 3922
		private Font k;

		// Token: 0x04000F53 RID: 3923
		private string l = "Arial";

		// Token: 0x04000F54 RID: 3924
		private XmlNode m;

		// Token: 0x04000F55 RID: 3925
		private bool n;

		// Token: 0x04000F56 RID: 3926
		private ConvertXmlNodeToPdfDelegate o;

		// Token: 0x04000F57 RID: 3927
		private ProcessImagePathDelegate p;

		// Token: 0x04000F58 RID: 3928
		private bool q;
	}
}
