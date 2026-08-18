using System;
using System.Text;

namespace a.b
{
	// Token: 0x02000378 RID: 888
	internal sealed class b6 : fe
	{
		// Token: 0x0600203B RID: 8251 RVA: 0x00086710 File Offset: 0x00085710
		public b6(string A_0, f4 A_1, i6 A_2, int A_3, int A_4, string A_5)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("id");
			}
			if (A_3 < 0)
			{
				throw new ArgumentException(fa.i(A_3));
			}
			if (A_4 < 0)
			{
				throw new ArgumentException(fa.h(A_4));
			}
			if (A_5 == null)
			{
				throw new ArgumentNullException("name");
			}
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
			this.d = A_3;
			this.e = A_4;
			this.f = A_5;
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x00086791 File Offset: 0x00085791
		public string e4()
		{
			return this.a;
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x00086799 File Offset: 0x00085799
		public f4 e5()
		{
			return this.b;
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x000867A1 File Offset: 0x000857A1
		public i6 e6()
		{
			return this.c;
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x000867A9 File Offset: 0x000857A9
		public int e7()
		{
			return this.d;
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x000867B1 File Offset: 0x000857B1
		public int e8()
		{
			if (this.e == 0)
			{
				return b3.a(this.d);
			}
			return this.e;
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x000867CD File Offset: 0x000857CD
		public Encoding b()
		{
			return Encoding.GetEncoding(this.e8());
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x000867DA File Offset: 0x000857DA
		public string e9()
		{
			return this.f;
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x000867E2 File Offset: 0x000857E2
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && !(base.GetType() != obj.GetType()) && this.a(obj));
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x00086809 File Offset: 0x00085809
		public override int GetHashCode()
		{
			return f3.a(base.GetType().GetHashCode(), this.a());
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x00086821 File Offset: 0x00085821
		public override string ToString()
		{
			return this.a + ":" + this.f;
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x0008683C File Offset: 0x0008583C
		private bool a(object A_0)
		{
			b6 b = A_0 as b6;
			return b != null && this.a.Equals(b.a) && this.b == b.b && this.c == b.c && this.d == b.d && this.e == b.e && this.f.Equals(b.f);
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x000868B4 File Offset: 0x000858B4
		private int a()
		{
			return f3.a(f3.a(f3.a(f3.a(f3.a(this.a.GetHashCode(), this.b), this.c), this.d), this.e), this.f);
		}

		// Token: 0x04001476 RID: 5238
		private readonly string a;

		// Token: 0x04001477 RID: 5239
		private readonly f4 b;

		// Token: 0x04001478 RID: 5240
		private readonly i6 c;

		// Token: 0x04001479 RID: 5241
		private readonly int d;

		// Token: 0x0400147A RID: 5242
		private readonly int e;

		// Token: 0x0400147B RID: 5243
		private readonly string f;
	}
}
