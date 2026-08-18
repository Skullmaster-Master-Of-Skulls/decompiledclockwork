using System;
using System.Globalization;
using System.IO;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200030B RID: 779
	internal class dv : aa
	{
		// Token: 0x06001BD6 RID: 7126 RVA: 0x0007A9F3 File Offset: 0x000799F3
		public dv(int A_0) : this(A_0, 0)
		{
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x0007A9FD File Offset: 0x000799FD
		public dv(int A_0, byte A_1)
		{
			if (A_0 < 0)
			{
				throw new IndexOutOfRangeException("offset cannot be negative");
			}
			this.b = A_0;
			this.a(A_1);
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0007AA22 File Offset: 0x00079A22
		public dv(int A_0, byte[] A_1) : this(A_0)
		{
			this.@in(A_1);
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x0007AA32 File Offset: 0x00079A32
		public dv(int A_0, byte A_1, byte[] A_2) : this(A_0, A_1)
		{
			this.ip(A_2);
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x0007AA43 File Offset: 0x00079A43
		public virtual byte a()
		{
			return this.c;
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x0007AA4B File Offset: 0x00079A4B
		public virtual void a(byte A_0)
		{
			this.c = A_0;
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x0007AA54 File Offset: 0x00079A54
		public virtual void @in(byte[] A_0)
		{
			this.c = A_0[this.b];
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x0007AA64 File Offset: 0x00079A64
		public virtual void io(Stream A_0)
		{
			int num = A_0.ReadByte();
			if (num < 0)
			{
				throw new BufferUnderflowException();
			}
			this.c = (byte)num;
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x0007AA8A File Offset: 0x00079A8A
		public virtual void a(byte A_0, byte[] A_1)
		{
			this.a(A_0);
			this.ip(A_1);
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x0007AA9A File Offset: 0x00079A9A
		public override string ToString()
		{
			return Convert.ToString(this.c, CultureInfo.CurrentCulture);
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x0007AAAC File Offset: 0x00079AAC
		public virtual void ip(byte[] A_0)
		{
			A_0[this.b] = this.c;
		}

		// Token: 0x04001342 RID: 4930
		private const byte a = 0;

		// Token: 0x04001343 RID: 4931
		private int b;

		// Token: 0x04001344 RID: 4932
		private byte c;
	}
}
