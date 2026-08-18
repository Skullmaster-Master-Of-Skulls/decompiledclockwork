using System;
using System.Security.Cryptography;
using \u0002;

namespace \u0008
{
	// Token: 0x02000356 RID: 854
	internal sealed class \u0002 : \u0002
	{
		// Token: 0x06001E21 RID: 7713 RVA: 0x00125D4C File Offset: 0x00123F4C
		void \u0002.\u0002(byte[] \u0002, int \u0003, int \u0004)
		{
			this.\u0003(\u0002, \u0003, \u0004);
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x00125D58 File Offset: 0x00123F58
		void \u0002.\u0002()
		{
			this.\u0003();
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x00125D60 File Offset: 0x00123F60
		int \u0002.\u0002(byte[] \u0002, int \u0003)
		{
			return this.\u0003(\u0002, \u0003);
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x00125D6C File Offset: 0x00123F6C
		void \u0002.\u0002(byte[] \u0002, int \u0003, int \u0004, byte[] \u0005, int \u0006, ref byte[] \u0007)
		{
			this.\u0003(\u0002, \u0003, \u0004, \u0005, \u0006, ref \u0007);
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x00125D80 File Offset: 0x00123F80
		internal \u0002()
		{
			this.\u0001 = new SHA384CryptoServiceProvider();
			this.\u0003();
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x00125D9C File Offset: 0x00123F9C
		public int \u0001()
		{
			return this.\u0001.HashSize / 8;
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x00125DAC File Offset: 0x00123FAC
		private void \u0001(byte \u0002)
		{
			byte[] inputBuffer = new byte[]
			{
				\u0002
			};
			this.\u0001.TransformBlock(inputBuffer, 0, 1, null, 0);
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x00125DD8 File Offset: 0x00123FD8
		internal void \u0003(byte[] \u0002, int \u0003, int \u0004)
		{
			if (this.\u0002)
			{
				this.\u0001.TransformBlock(this.\u0003.Array, this.\u0003.Offset, this.\u0003.Count, null, 0);
			}
			this.\u0003 = new ArraySegment<byte>(\u0002, \u0003, \u0004);
			this.\u0002 = true;
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x00125E34 File Offset: 0x00124034
		internal void \u0003()
		{
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x00125E38 File Offset: 0x00124038
		internal int \u0003(byte[] \u0002, int \u0003)
		{
			if (this.\u0002)
			{
				this.\u0001.TransformFinalBlock(this.\u0003.Array, this.\u0003.Offset, this.\u0003.Count);
			}
			Buffer.BlockCopy(this.\u0001.Hash, 0, \u0002, \u0003, this.\u0001());
			return this.\u0001();
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x00125E9C File Offset: 0x0012409C
		internal void \u0003(byte[] \u0002, int \u0003, int \u0004, byte[] \u0005, int \u0006, ref byte[] \u0007)
		{
			byte[] array = new byte[\u0004 + \u0006];
			Buffer.BlockCopy(\u0002, \u0003, array, 0, \u0004);
			Buffer.BlockCopy(\u0005, 0, array, \u0004, \u0006);
			\u0007 = this.\u0001.ComputeHash(array);
		}

		// Token: 0x04002058 RID: 8280
		private SHA384CryptoServiceProvider \u0001;

		// Token: 0x04002059 RID: 8281
		private bool \u0002;

		// Token: 0x0400205A RID: 8282
		private ArraySegment<byte> \u0003;
	}
}
