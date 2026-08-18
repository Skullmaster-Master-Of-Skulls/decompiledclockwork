using System;
using System.Data;

namespace a.d
{
	// Token: 0x02000426 RID: 1062
	internal class k
	{
		// Token: 0x06002502 RID: 9474 RVA: 0x0009F1C2 File Offset: 0x0009E1C2
		public k(DataTable A_0, int A_1, IDataReader A_2, string[] A_3, object[] A_4)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
			this.d = A_3;
			this.e = A_4;
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x0009F1EF File Offset: 0x0009E1EF
		public DataTable c()
		{
			return this.a;
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x0009F1F7 File Offset: 0x0009E1F7
		public int a()
		{
			return this.b;
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x0009F1FF File Offset: 0x0009E1FF
		public IDataReader e()
		{
			return this.c;
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x0009F207 File Offset: 0x0009E207
		public string[] b()
		{
			return this.d;
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x0009F20F File Offset: 0x0009E20F
		public object[] d()
		{
			return this.e;
		}

		// Token: 0x040018A4 RID: 6308
		private DataTable a;

		// Token: 0x040018A5 RID: 6309
		private int b;

		// Token: 0x040018A6 RID: 6310
		private IDataReader c;

		// Token: 0x040018A7 RID: 6311
		private string[] d;

		// Token: 0x040018A8 RID: 6312
		private object[] e;
	}
}
