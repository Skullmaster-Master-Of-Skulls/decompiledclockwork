using System;

namespace a.h
{
	// Token: 0x02000201 RID: 513
	internal class c : n
	{
		// Token: 0x060010B7 RID: 4279 RVA: 0x00046A38 File Offset: 0x00045A38
		public c(n A_0) : base(A_0)
		{
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00046A41 File Offset: 0x00045A41
		public override long get_Position()
		{
			return this.a;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00046A49 File Offset: 0x00045A49
		public override void set_Position(long value)
		{
			this.a = value;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00046A54 File Offset: 0x00045A54
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = base.Read(buffer, offset, count);
			this.a += (long)num;
			return num;
		}

		// Token: 0x04000E42 RID: 3650
		private new long a;
	}
}
