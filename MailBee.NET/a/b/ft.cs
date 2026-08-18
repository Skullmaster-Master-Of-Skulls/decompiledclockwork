using System;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200029F RID: 671
	internal class ft
	{
		// Token: 0x06001798 RID: 6040 RVA: 0x0006B9F0 File Offset: 0x0006A9F0
		public ft(byte[] A_0, int A_1)
		{
			int num = p.i(A_0, A_1);
			if (num == 0)
			{
				this.a = new byte[0];
				return;
			}
			this.a = p.b(A_0, A_1 + 4, num * 2);
			if (this.a[num * 2 - 1] != 0 || this.a[num * 2 - 2] != 0)
			{
				throw new IllegalPropertySetDataException("UnicodeString started at offset #" + A_1 + " is not NULL-terminated");
			}
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0006BA64 File Offset: 0x0006AA64
		public int a()
		{
			return 4 + this.a.Length;
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0006BA70 File Offset: 0x0006AA70
		public byte[] c()
		{
			return this.a;
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0006BA78 File Offset: 0x0006AA78
		public string b()
		{
			if (this.a.Length == 0)
			{
				return null;
			}
			string text = global::a.b.a.c(this.a, 0, this.a.Length >> 1);
			int num = text.IndexOf('\0');
			if (num == -1)
			{
				return text;
			}
			int num2 = text.Length - 1;
			return text.Substring(0, num);
		}

		// Token: 0x04001171 RID: 4465
		private byte[] a;
	}
}
