using System;
using System.Text;
using MailBee;

namespace a.i
{
	// Token: 0x020001DE RID: 478
	internal class i
	{
		// Token: 0x06000F6D RID: 3949 RVA: 0x0003BA18 File Offset: 0x0003AA18
		public i()
		{
			this.a = new byte[0];
			this.c = string.Empty;
			this.d = false;
			this.b = false;
			this.e = Global.DefaultEncoding;
			this.f = Global.DefaultEncoding;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0003BA66 File Offset: 0x0003AA66
		public i(string A_0, Encoding A_1) : this()
		{
			this.a(A_0);
			this.f = A_1;
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0003BA7C File Offset: 0x0003AA7C
		public i(byte[] A_0, Encoding A_1) : this()
		{
			this.a(A_0);
			this.e = A_1;
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0003BA92 File Offset: 0x0003AA92
		public Encoding e()
		{
			return this.e;
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0003BA9A File Offset: 0x0003AA9A
		public void a(Encoding A_0)
		{
			this.e = A_0;
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0003BAA3 File Offset: 0x0003AAA3
		public Encoding f()
		{
			return this.f;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0003BAAB File Offset: 0x0003AAAB
		public void b(Encoding A_0)
		{
			this.f = A_0;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0003BAB4 File Offset: 0x0003AAB4
		public byte[] g()
		{
			if (!this.b)
			{
				this.b();
				this.b = true;
			}
			return this.a;
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0003BAD1 File Offset: 0x0003AAD1
		public void a(byte[] A_0)
		{
			this.a = A_0;
			this.b = true;
			this.d = false;
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0003BAE8 File Offset: 0x0003AAE8
		public string c()
		{
			if (!this.d)
			{
				this.a();
				this.d = true;
			}
			return this.c;
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0003BB05 File Offset: 0x0003AB05
		public void a(string A_0)
		{
			this.c = A_0;
			this.b = false;
			this.d = true;
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0003BB1C File Offset: 0x0003AB1C
		public int d()
		{
			if (this.a != null && this.a.Length != 0)
			{
				return this.a.Length;
			}
			if (this.c != null && this.c.Length != 0)
			{
				return this.c.Length;
			}
			return 0;
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0003BB5A File Offset: 0x0003AB5A
		private void b()
		{
			if (this.c != null && this.c.Length != 0)
			{
				this.a = this.f.GetBytes(this.c);
				this.e = this.f;
			}
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0003BB94 File Offset: 0x0003AB94
		private void a()
		{
			if (this.e != null && this.a != null)
			{
				this.c = this.e.GetString(this.a, 0, this.a.Length);
				this.f = this.e;
			}
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0003BBD2 File Offset: 0x0003ABD2
		public override string ToString()
		{
			return this.c();
		}

		// Token: 0x04000B4E RID: 2894
		private byte[] a;

		// Token: 0x04000B4F RID: 2895
		private bool b;

		// Token: 0x04000B50 RID: 2896
		private string c;

		// Token: 0x04000B51 RID: 2897
		private bool d;

		// Token: 0x04000B52 RID: 2898
		private Encoding e;

		// Token: 0x04000B53 RID: 2899
		private Encoding f;
	}
}
