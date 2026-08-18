using System;
using System.IO;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002CD RID: 717
	internal class cm : MemoryStream
	{
		// Token: 0x060018E0 RID: 6368 RVA: 0x0006F4D3 File Offset: 0x0006E4D3
		public cm(Stream A_0, int A_1)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = 0;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0006F4F0 File Offset: 0x0006E4F0
		public void b(int A_0)
		{
			this.a(1);
			this.a.WriteByte((byte)A_0);
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0006F506 File Offset: 0x0006E506
		public void a(byte[] A_0)
		{
			this.Write(A_0, 0, A_0.Length);
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0006F513 File Offset: 0x0006E513
		public override void Write(byte[] b, int off, int len)
		{
			this.a(len);
			this.a.Write(b, off, len);
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0006F52A File Offset: 0x0006E52A
		public override void Flush()
		{
			this.a.Flush();
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0006F537 File Offset: 0x0006E537
		public override void Close()
		{
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0006F53C File Offset: 0x0006E53C
		public void a(int A_0, byte A_1)
		{
			if (A_0 > this.c)
			{
				byte[] array = new byte[A_0 - this.c];
				d4.a(array, A_1);
				this.a.Write(array, 0, array.Length);
			}
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0006F577 File Offset: 0x0006E577
		private void a(int A_0)
		{
			if (this.c + A_0 > this.b)
			{
				throw new MailBeeOutlookMsgBuildingException(Resources.Instance.ErrorDesc_OleDocTriedToWriteTooMuchData, 1201);
			}
			this.c += A_0;
		}

		// Token: 0x04001248 RID: 4680
		private Stream a;

		// Token: 0x04001249 RID: 4681
		private int b;

		// Token: 0x0400124A RID: 4682
		private int c;
	}
}
