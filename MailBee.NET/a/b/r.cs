using System;
using System.Globalization;
using System.IO;

namespace a.b
{
	// Token: 0x02000322 RID: 802
	internal class r
	{
		// Token: 0x06001CDD RID: 7389 RVA: 0x0007DB2D File Offset: 0x0007CB2D
		public r(int A_0)
		{
			if (A_0 < 0)
			{
				throw new IndexOutOfRangeException("Illegal offset: " + A_0);
			}
			this.b = A_0;
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x0007DB56 File Offset: 0x0007CB56
		public r(int A_0, long A_1) : this(A_0)
		{
			this.a(A_1);
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x0007DB66 File Offset: 0x0007CB66
		public r(int A_0, byte[] A_1) : this(A_0)
		{
			this.b(A_1);
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0007DB76 File Offset: 0x0007CB76
		public r(int A_0, long A_1, byte[] A_2) : this(A_0)
		{
			this.a(A_1, A_2);
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0007DB87 File Offset: 0x0007CB87
		public long a()
		{
			return this.a;
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0007DB8F File Offset: 0x0007CB8F
		public void a(long A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0007DB98 File Offset: 0x0007CB98
		public void a(long A_0, byte[] A_1)
		{
			this.a = A_0;
			this.a(A_1);
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0007DBA8 File Offset: 0x0007CBA8
		public void b(byte[] A_0)
		{
			this.a = p.g(A_0, this.b);
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x0007DBBC File Offset: 0x0007CBBC
		public void a(Stream A_0)
		{
			this.a = p.b(A_0);
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0007DBCA File Offset: 0x0007CBCA
		public void a(byte[] A_0)
		{
			p.a(A_0, this.b, this.a);
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0007DBDE File Offset: 0x0007CBDE
		public static void a(int A_0, long A_1, byte[] A_2)
		{
			p.a(A_2, A_0, A_1);
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0007DBE8 File Offset: 0x0007CBE8
		public override string ToString()
		{
			return Convert.ToString(this.a, CultureInfo.CurrentCulture);
		}

		// Token: 0x0400136D RID: 4973
		private long a;

		// Token: 0x0400136E RID: 4974
		private int b;
	}
}
