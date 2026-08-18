using System;
using System.Drawing;
using System.IO;

namespace a.b
{
	// Token: 0x02000349 RID: 841
	internal class i0
	{
		// Token: 0x06001E69 RID: 7785 RVA: 0x00082155 File Offset: 0x00081155
		public i0() : this(new ci())
		{
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x00082162 File Offset: 0x00081162
		public i0(z A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("imageAdapter");
			}
			this.a = A_0;
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x00082186 File Offset: 0x00081186
		public z f()
		{
			return this.a;
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x0008218E File Offset: 0x0008118E
		public Color? d()
		{
			return this.b;
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x00082196 File Offset: 0x00081196
		public void a(Color? A_0)
		{
			this.b = A_0;
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x0008219F File Offset: 0x0008119F
		public string b()
		{
			return this.c;
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x000821A7 File Offset: 0x000811A7
		public void a(string A_0)
		{
			this.c = A_0;
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x000821B0 File Offset: 0x000811B0
		public bool a()
		{
			return this.d;
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x000821B8 File Offset: 0x000811B8
		public void a(bool A_0)
		{
			this.d = A_0;
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x000821C1 File Offset: 0x000811C1
		public float e()
		{
			return this.e;
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x000821C9 File Offset: 0x000811C9
		public void b(float A_0)
		{
			this.e = A_0;
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x000821D2 File Offset: 0x000811D2
		public float c()
		{
			return this.f;
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x000821DA File Offset: 0x000811DA
		public void a(float A_0)
		{
			this.f = A_0;
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x000821E4 File Offset: 0x000811E4
		public string a(int A_0, de A_1)
		{
			string text = this.a.gj(A_0, A_1);
			if (!string.IsNullOrEmpty(this.c))
			{
				text = Path.Combine(this.c, text);
			}
			return text;
		}

		// Token: 0x040013D6 RID: 5078
		private readonly z a;

		// Token: 0x040013D7 RID: 5079
		private Color? b;

		// Token: 0x040013D8 RID: 5080
		private string c;

		// Token: 0x040013D9 RID: 5081
		private bool d = true;

		// Token: 0x040013DA RID: 5082
		private float e;

		// Token: 0x040013DB RID: 5083
		private float f;
	}
}
