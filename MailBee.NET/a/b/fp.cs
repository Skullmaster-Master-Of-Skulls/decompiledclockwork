using System;
using System.Globalization;
using System.IO;

namespace a.b
{
	// Token: 0x0200032C RID: 812
	internal class fp
	{
		// Token: 0x06001D4C RID: 7500 RVA: 0x0007EA45 File Offset: 0x0007DA45
		public fp(int A_0)
		{
			if (A_0 < 0)
			{
				throw new IndexOutOfRangeException("Illegal offset: " + A_0);
			}
			this.b = A_0;
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x0007EA6E File Offset: 0x0007DA6E
		public fp(int A_0, short A_1) : this(A_0)
		{
			this.a = A_1;
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x0007EA7E File Offset: 0x0007DA7E
		public fp(int A_0, byte[] A_1) : this(A_0)
		{
			this.b(A_1);
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x0007EA8E File Offset: 0x0007DA8E
		public fp(int A_0, short A_1, ref byte[] A_2) : this(A_0)
		{
			this.a(A_1, ref A_2);
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x0007EA9F File Offset: 0x0007DA9F
		public short a()
		{
			return this.a;
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x0007EAA7 File Offset: 0x0007DAA7
		public void a(short A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x0007EAB0 File Offset: 0x0007DAB0
		public void a(short A_0, ref byte[] A_1)
		{
			this.a = A_0;
			this.a(A_1);
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0007EAC1 File Offset: 0x0007DAC1
		public void b(byte[] A_0)
		{
			this.a = p.k(A_0, this.b);
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x0007EAD5 File Offset: 0x0007DAD5
		public void a(Stream A_0)
		{
			this.a = p.e(A_0);
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0007EAE3 File Offset: 0x0007DAE3
		public void a(byte[] A_0)
		{
			p.a(A_0, this.b, this.a);
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x0007EAF7 File Offset: 0x0007DAF7
		public static void a(int A_0, short A_1, ref byte[] A_2)
		{
			p.a(A_2, A_0, A_1);
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0007EB02 File Offset: 0x0007DB02
		public override string ToString()
		{
			return Convert.ToString(this.a, CultureInfo.CurrentCulture);
		}

		// Token: 0x04001382 RID: 4994
		private short a;

		// Token: 0x04001383 RID: 4995
		private int b;
	}
}
