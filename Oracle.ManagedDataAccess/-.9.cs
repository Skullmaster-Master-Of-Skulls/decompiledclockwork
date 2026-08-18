using System;
using System.Security.Cryptography;

namespace \u0002
{
	// Token: 0x0200035B RID: 859
	internal sealed class \u0004 : \u0002
	{
		// Token: 0x06001E3E RID: 7742 RVA: 0x00126470 File Offset: 0x00124670
		void \u0002.\u0002(byte[] \u0002, int \u0003, int \u0004)
		{
			this.\u0003(\u0002, \u0003, \u0004);
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x0012647C File Offset: 0x0012467C
		void \u0002.\u0002()
		{
			this.\u0003();
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x00126484 File Offset: 0x00124684
		int \u0002.\u0002(byte[] \u0002, int \u0003)
		{
			return this.\u0003(\u0002, \u0003);
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x00126490 File Offset: 0x00124690
		void \u0002.\u0002(byte[] \u0002, int \u0003, int \u0004, byte[] \u0005, int \u0006, ref byte[] \u0007)
		{
			this.\u0003(\u0002, \u0003, \u0004, \u0005, \u0006, ref \u0007);
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x001264A4 File Offset: 0x001246A4
		internal \u0004()
		{
			this.\u0001 = new SHA256CryptoServiceProvider();
			this.\u0003();
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x001264C0 File Offset: 0x001246C0
		public int \u0001()
		{
			return this.\u0001.HashSize / 8;
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x001264D0 File Offset: 0x001246D0
		private void \u0001(byte \u0002)
		{
			byte[] inputBuffer = new byte[]
			{
				\u0002
			};
			this.\u0001.TransformBlock(inputBuffer, 0, 1, null, 0);
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x001264FC File Offset: 0x001246FC
		internal void \u0003(byte[] \u0002, int \u0003, int \u0004)
		{
			if (this.\u0002)
			{
				this.\u0001.TransformBlock(this.\u0003.Array, this.\u0003.Offset, this.\u0003.Count, null, 0);
			}
			this.\u0003 = new ArraySegment<byte>(\u0002, \u0003, \u0004);
			this.\u0002 = true;
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x00126558 File Offset: 0x00124758
		internal void \u0003()
		{
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x0012655C File Offset: 0x0012475C
		internal int \u0003(byte[] \u0002, int \u0003)
		{
			if (this.\u0002)
			{
				this.\u0001.TransformFinalBlock(this.\u0003.Array, this.\u0003.Offset, this.\u0003.Count);
			}
			Buffer.BlockCopy(this.\u0001.Hash, 0, \u0002, \u0003, this.\u0001());
			return this.\u0001();
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x001265C0 File Offset: 0x001247C0
		internal void \u0003(byte[] \u0002, int \u0003, int \u0004, byte[] \u0005, int \u0006, ref byte[] \u0007)
		{
			byte[] array = new byte[\u0004 + \u0006];
			Buffer.BlockCopy(\u0002, \u0003, array, 0, \u0004);
			Buffer.BlockCopy(\u0005, 0, array, \u0004, \u0006);
			\u0007 = this.\u0001.ComputeHash(array);
		}

		// Token: 0x04002070 RID: 8304
		private SHA256CryptoServiceProvider \u0001;

		// Token: 0x04002071 RID: 8305
		private bool \u0002;

		// Token: 0x04002072 RID: 8306
		private ArraySegment<byte> \u0003;
	}
}
