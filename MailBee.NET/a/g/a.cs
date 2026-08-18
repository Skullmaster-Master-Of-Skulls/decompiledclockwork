using System;
using MailBee;

namespace a.g
{
	// Token: 0x020003F3 RID: 1011
	internal class a : MailBeeException
	{
		// Token: 0x060023CA RID: 9162 RVA: 0x000965BF File Offset: 0x000955BF
		internal a(string A_0, int A_1, byte[] A_2) : base(A_0, A_1)
		{
			this.a = A_2;
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000965D0 File Offset: 0x000955D0
		internal a(int A_0, byte[] A_1) : base(A_0)
		{
			this.a = A_1;
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000965E0 File Offset: 0x000955E0
		public byte[] a()
		{
			return this.a;
		}

		// Token: 0x040017A7 RID: 6055
		private byte[] a;
	}
}
