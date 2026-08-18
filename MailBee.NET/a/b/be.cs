using System;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000374 RID: 884
	internal sealed class be : ip
	{
		// Token: 0x06001FED RID: 8173 RVA: 0x00086040 File Offset: 0x00085040
		public be(eq A_0, bi A_1) : this(A_0.ks(), A_0.ku(), A_0.kv(), A_0.kw(), A_0.kx(), A_0.ky(), A_0.k1(), A_0.k2(), A_1)
		{
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x00086084 File Offset: 0x00085084
		public be(int A_0, fe A_1, h6 A_2, j A_3, string A_4, gu A_5, @do A_6, ee A_7, bi A_8)
		{
			if (A_0 != 1)
			{
				throw new RtfUnsupportedStructureException(fa.k(A_0));
			}
			if (A_1 == null)
			{
				throw new ArgumentNullException("defaultFont");
			}
			if (A_2 == null)
			{
				throw new ArgumentNullException("fontTable");
			}
			if (A_3 == null)
			{
				throw new ArgumentNullException("colorTable");
			}
			if (A_5 == null)
			{
				throw new ArgumentNullException("uniqueTextFormats");
			}
			if (A_6 == null)
			{
				throw new ArgumentNullException("documentInfo");
			}
			if (A_7 == null)
			{
				throw new ArgumentNullException("userProperties");
			}
			if (A_8 == null)
			{
				throw new ArgumentNullException("visualContent");
			}
			this.a = A_0;
			this.b = A_1;
			this.c = new cu(A_1, 24);
			this.d = A_2;
			this.e = A_3;
			this.f = A_4;
			this.g = A_5;
			this.h = A_6;
			this.i = A_7;
			this.j = A_8;
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x00086161 File Offset: 0x00085161
		public int dk()
		{
			return this.a;
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x00086169 File Offset: 0x00085169
		public fe dl()
		{
			return this.b;
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x00086171 File Offset: 0x00085171
		public ej dm()
		{
			return this.c;
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x00086179 File Offset: 0x00085179
		public h6 dn()
		{
			return this.d;
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x00086181 File Offset: 0x00085181
		public j @do()
		{
			return this.e;
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x00086189 File Offset: 0x00085189
		public string dp()
		{
			return this.f;
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x00086191 File Offset: 0x00085191
		public gu dq()
		{
			return this.g;
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x00086199 File Offset: 0x00085199
		public @do dr()
		{
			return this.h;
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x000861A1 File Offset: 0x000851A1
		public ee ds()
		{
			return this.i;
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x000861A9 File Offset: 0x000851A9
		public bi dt()
		{
			return this.j;
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x000861B1 File Offset: 0x000851B1
		public override string ToString()
		{
			return "RTFv" + this.a;
		}

		// Token: 0x04001451 RID: 5201
		private readonly int a;

		// Token: 0x04001452 RID: 5202
		private readonly fe b;

		// Token: 0x04001453 RID: 5203
		private readonly ej c;

		// Token: 0x04001454 RID: 5204
		private readonly h6 d;

		// Token: 0x04001455 RID: 5205
		private readonly j e;

		// Token: 0x04001456 RID: 5206
		private readonly string f;

		// Token: 0x04001457 RID: 5207
		private readonly gu g;

		// Token: 0x04001458 RID: 5208
		private readonly @do h;

		// Token: 0x04001459 RID: 5209
		private readonly ee i;

		// Token: 0x0400145A RID: 5210
		private readonly bi j;
	}
}
