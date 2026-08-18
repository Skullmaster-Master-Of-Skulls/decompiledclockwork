using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000476 RID: 1142
	public class Gost3410ValidationParameters
	{
		// Token: 0x060026DC RID: 9948 RVA: 0x000EAFFB File Offset: 0x000E9FFB
		public Gost3410ValidationParameters(int x0, int c)
		{
			this.x0 = x0;
			this.c = c;
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x000EB011 File Offset: 0x000EA011
		public Gost3410ValidationParameters(long x0L, long cL)
		{
			this.x0L = x0L;
			this.cL = cL;
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x000EB027 File Offset: 0x000EA027
		public int C
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060026DF RID: 9951 RVA: 0x000EB02F File Offset: 0x000EA02F
		public int X0
		{
			get
			{
				return this.x0;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060026E0 RID: 9952 RVA: 0x000EB037 File Offset: 0x000EA037
		public long CL
		{
			get
			{
				return this.cL;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060026E1 RID: 9953 RVA: 0x000EB03F File Offset: 0x000EA03F
		public long X0L
		{
			get
			{
				return this.x0L;
			}
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x000EB048 File Offset: 0x000EA048
		public override bool Equals(object obj)
		{
			Gost3410ValidationParameters gost3410ValidationParameters = obj as Gost3410ValidationParameters;
			return gost3410ValidationParameters != null && gost3410ValidationParameters.c == this.c && gost3410ValidationParameters.x0 == this.x0 && gost3410ValidationParameters.cL == this.cL && gost3410ValidationParameters.x0L == this.x0L;
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x000EB099 File Offset: 0x000EA099
		public override int GetHashCode()
		{
			return this.c.GetHashCode() ^ this.x0.GetHashCode() ^ this.cL.GetHashCode() ^ this.x0L.GetHashCode();
		}

		// Token: 0x04001ABC RID: 6844
		private int x0;

		// Token: 0x04001ABD RID: 6845
		private int c;

		// Token: 0x04001ABE RID: 6846
		private long x0L;

		// Token: 0x04001ABF RID: 6847
		private long cL;
	}
}
