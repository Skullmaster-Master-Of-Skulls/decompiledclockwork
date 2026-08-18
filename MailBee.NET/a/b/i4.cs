using System;
using System.IO;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002FE RID: 766
	internal class i4 : bn
	{
		// Token: 0x06001B05 RID: 6917 RVA: 0x00076408 File Offset: 0x00075408
		public i4(Stream A_0) : this(A_0, 512)
		{
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x00076418 File Offset: 0x00075418
		public i4(Stream A_0, int A_1)
		{
			this.a = new byte[A_1];
			int num = g9.a(A_0, this.a);
			this.c = (num > 0);
			if (num == -1)
			{
				this.b = true;
				return;
			}
			if (num != A_1)
			{
				this.b = true;
				string text = " byte" + ((num == 1) ? "" : "s");
				i4.d.iv(7, string.Concat(new object[]
				{
					"Unable to read entire block; ",
					num,
					text,
					" read before EOF; expected ",
					A_1,
					" bytes. Your document was either written by software that ignores the spec, or has been truncated!"
				}));
				return;
			}
			this.b = false;
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x000764CC File Offset: 0x000754CC
		public bool b()
		{
			return this.b;
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x000764D4 File Offset: 0x000754D4
		public bool c()
		{
			return this.c;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x000764DC File Offset: 0x000754DC
		public byte[] bv()
		{
			if (!this.c())
			{
				throw new MailBeeOutlookMsgBuildingException(Resources.Instance.ErrorDesc_OleDocCannotReturnEmptyData, 1201);
			}
			return this.a;
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x00076501 File Offset: 0x00075501
		public override string ToString()
		{
			return "RawDataBlock of size " + this.a.Length;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0007651A File Offset: 0x0007551A
		public int a()
		{
			return this.a.Length;
		}

		// Token: 0x04001315 RID: 4885
		private byte[] a;

		// Token: 0x04001316 RID: 4886
		private bool b;

		// Token: 0x04001317 RID: 4887
		private bool c;

		// Token: 0x04001318 RID: 4888
		private static dm d = gn.a(typeof(i4));
	}
}
