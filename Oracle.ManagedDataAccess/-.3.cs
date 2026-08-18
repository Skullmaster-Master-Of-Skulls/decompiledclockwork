using System;

namespace \u0002
{
	// Token: 0x0200034B RID: 843
	internal class \u0001
	{
		// Token: 0x06001DC4 RID: 7620 RVA: 0x00123C4C File Offset: 0x00121E4C
		private void \u0001()
		{
			Random random = new Random(Guid.NewGuid().GetHashCode());
			this.\u0004 = random.Next();
			this.\u0005 = true;
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x00123C88 File Offset: 0x00121E88
		internal byte \u0001()
		{
			return (byte)this.\u0001();
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x00123C94 File Offset: 0x00121E94
		internal ushort \u0001()
		{
			if (!this.\u0005)
			{
				this.\u0001();
			}
			else
			{
				this.\u0004 += 7;
				this.\u0002 += 1907;
				this.\u0003 += 73939;
				if (this.\u0004 >= 9973)
				{
					this.\u0004 -= 9871;
				}
				if (this.\u0002 >= 99991)
				{
					this.\u0002 -= 89989;
				}
				if (this.\u0003 >= 224729)
				{
					this.\u0003 -= 96233;
				}
				this.\u0004 = this.\u0004 * this.\u0001 + this.\u0002 + this.\u0003;
			}
			return (ushort)(this.\u0004 >> 16 ^ (this.\u0004 & 65535));
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x00123D7C File Offset: 0x00121F7C
		internal void \u0001(byte[] \u0002, int \u0003)
		{
			for (int i = 0; i < \u0003; i++)
			{
				\u0002[i] = this.\u0001();
			}
		}

		// Token: 0x04002012 RID: 8210
		private int \u0001 = 971;

		// Token: 0x04002013 RID: 8211
		private int \u0002 = 11113;

		// Token: 0x04002014 RID: 8212
		private int \u0003 = 104322;

		// Token: 0x04002015 RID: 8213
		private int \u0004 = 4181;

		// Token: 0x04002016 RID: 8214
		private bool \u0005;
	}
}
