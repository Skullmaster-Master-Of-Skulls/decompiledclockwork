using System;
using System.Collections.Specialized;

namespace a.b
{
	// Token: 0x0200033A RID: 826
	internal class du
	{
		// Token: 0x06001DF7 RID: 7671 RVA: 0x00081611 File Offset: 0x00080611
		public du() : this(new ci(), global::a.b.n.g)
		{
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00081623 File Offset: 0x00080623
		public du(n A_0) : this(new ci(), A_0)
		{
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x00081631 File Offset: 0x00080631
		public du(z A_0) : this(A_0, global::a.b.n.g)
		{
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x00081640 File Offset: 0x00080640
		public du(z A_0, n A_1)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("imageAdapter");
			}
			this.d = A_0;
			this.e = A_1;
			this.k = "[a-zA-Z0-9\\-\\.]+\\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\\-\\._\\?\\,\\'/\\\\\\+&%\\$#\\=~])*";
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x00081690 File Offset: 0x00080690
		public z i()
		{
			return this.d;
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x00081698 File Offset: 0x00080698
		public n k()
		{
			return this.e;
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x000816A0 File Offset: 0x000806A0
		public void a(n A_0)
		{
			this.e = A_0;
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x000816A9 File Offset: 0x000806A9
		public bool e()
		{
			return this.f != null && this.f.Count > 0;
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x000816C3 File Offset: 0x000806C3
		public hn h()
		{
			if (this.f == null)
			{
				this.f = new hn();
			}
			return this.f;
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x000816DE File Offset: 0x000806DE
		public bool a()
		{
			return this.g != null && this.g.Count > 0;
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x000816F8 File Offset: 0x000806F8
		public StringCollection o()
		{
			if (this.g == null)
			{
				this.g = new StringCollection();
			}
			return this.g;
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x00081713 File Offset: 0x00080713
		public string m()
		{
			return this.i;
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x0008171B File Offset: 0x0008071B
		public void c(string A_0)
		{
			this.i = A_0;
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x00081724 File Offset: 0x00080724
		public string b()
		{
			return this.j;
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x0008172C File Offset: 0x0008072C
		public void a(string A_0)
		{
			this.j = A_0;
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x00081735 File Offset: 0x00080735
		public string l()
		{
			return this.m;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x0008173D File Offset: 0x0008073D
		public void d(string A_0)
		{
			this.m = A_0;
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x00081746 File Offset: 0x00080746
		public string d()
		{
			return this.k;
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x0008174E File Offset: 0x0008074E
		public void f(string A_0)
		{
			this.k = A_0;
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x00081757 File Offset: 0x00080757
		public string g()
		{
			return this.l;
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x0008175F File Offset: 0x0008075F
		public void b(string A_0)
		{
			this.l = A_0;
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x00081768 File Offset: 0x00080768
		public bool j()
		{
			return this.n;
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x00081770 File Offset: 0x00080770
		public void c(bool A_0)
		{
			this.n = A_0;
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x00081779 File Offset: 0x00080779
		public bool f()
		{
			return this.o;
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x00081781 File Offset: 0x00080781
		public void b(bool A_0)
		{
			this.o = A_0;
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x0008178A File Offset: 0x0008078A
		public bool n()
		{
			return this.p;
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x00081792 File Offset: 0x00080792
		public void a(bool A_0)
		{
			this.p = A_0;
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x0008179B File Offset: 0x0008079B
		public string c()
		{
			return this.h;
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x000817A3 File Offset: 0x000807A3
		public void e(string A_0)
		{
			this.h = A_0;
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x000817AC File Offset: 0x000807AC
		public string a(int A_0, de A_1)
		{
			return this.d.gj(A_0, A_1).Replace('\\', '/');
		}

		// Token: 0x040013A8 RID: 5032
		public const string a = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//DE\" \"http://www.w3.org/TR/html4/loose.dtd\">";

		// Token: 0x040013A9 RID: 5033
		public const string b = "UTF-8";

		// Token: 0x040013AA RID: 5034
		public const string c = "[a-zA-Z0-9\\-\\.]+\\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\\-\\._\\?\\,\\'/\\\\\\+&%\\$#\\=~])*";

		// Token: 0x040013AB RID: 5035
		private readonly z d;

		// Token: 0x040013AC RID: 5036
		private n e;

		// Token: 0x040013AD RID: 5037
		private hn f;

		// Token: 0x040013AE RID: 5038
		private StringCollection g;

		// Token: 0x040013AF RID: 5039
		private string h;

		// Token: 0x040013B0 RID: 5040
		private string i = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//DE\" \"http://www.w3.org/TR/html4/loose.dtd\">";

		// Token: 0x040013B1 RID: 5041
		private string j;

		// Token: 0x040013B2 RID: 5042
		private string k;

		// Token: 0x040013B3 RID: 5043
		private string l;

		// Token: 0x040013B4 RID: 5044
		private string m = "UTF-8";

		// Token: 0x040013B5 RID: 5045
		private bool n;

		// Token: 0x040013B6 RID: 5046
		private bool o;

		// Token: 0x040013B7 RID: 5047
		private bool p;
	}
}
