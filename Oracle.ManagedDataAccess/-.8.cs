using System;
using System.Security.Cryptography;
using \u0002;

namespace \u0008
{
	// Token: 0x02000358 RID: 856
	internal sealed class \u0003 : \u0002
	{
		// Token: 0x06001E2D RID: 7725 RVA: 0x00125EEC File Offset: 0x001240EC
		void \u0002.\u0002(byte[] \u0002, int \u0003, int \u0004)
		{
			this.\u0003(\u0002, \u0003, \u0004);
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x00125EF8 File Offset: 0x001240F8
		void \u0002.\u0002()
		{
			this.\u0003();
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x00125F00 File Offset: 0x00124100
		int \u0002.\u0002(byte[] \u0002, int \u0003)
		{
			return this.\u0003(\u0002, \u0003);
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x00125F0C File Offset: 0x0012410C
		void \u0002.\u0002(byte[] \u0002, int \u0003, int \u0004, byte[] \u0005, int \u0006, ref byte[] \u0007)
		{
			this.\u0003(\u0002, \u0003, \u0004, \u0005, \u0006, ref \u0007);
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x00125F20 File Offset: 0x00124120
		internal \u0003()
		{
			this.\u0001 = new SHA512CryptoServiceProvider();
			this.\u0003();
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x00125F3C File Offset: 0x0012413C
		public int \u0001()
		{
			return this.\u0001.HashSize / 8;
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x00125F4C File Offset: 0x0012414C
		private void \u0001(byte \u0002)
		{
			byte[] inputBuffer = new byte[]
			{
				\u0002
			};
			this.\u0001.TransformBlock(inputBuffer, 0, 1, null, 0);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x00125F78 File Offset: 0x00124178
		internal void \u0003(byte[] \u0002, int \u0003, int \u0004)
		{
			if (this.\u0002)
			{
				this.\u0001.TransformBlock(this.\u0003.Array, this.\u0003.Offset, this.\u0003.Count, null, 0);
			}
			this.\u0003 = new ArraySegment<byte>(\u0002, \u0003, \u0004);
			this.\u0002 = true;
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x00125FD4 File Offset: 0x001241D4
		internal void \u0003()
		{
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x00125FD8 File Offset: 0x001241D8
		internal int \u0003(byte[] \u0002, int \u0003)
		{
			if (this.\u0002)
			{
				this.\u0001.TransformFinalBlock(this.\u0003.Array, this.\u0003.Offset, this.\u0003.Count);
			}
			Buffer.BlockCopy(this.\u0001.Hash, 0, \u0002, \u0003, this.\u0001());
			return this.\u0001();
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x0012603C File Offset: 0x0012423C
		internal void \u0003(byte[] \u0002, int \u0003, int \u0004, byte[] \u0005, int \u0006, ref byte[] \u0007)
		{
			byte[] array = new byte[\u0004 + \u0006];
			Buffer.BlockCopy(\u0002, \u0003, array, 0, \u0004);
			Buffer.BlockCopy(\u0005, 0, array, \u0004, \u0006);
			\u0007 = this.\u0001.ComputeHash(array);
		}

		// Token: 0x0400205B RID: 8283
		private SHA512CryptoServiceProvider \u0001;

		// Token: 0x0400205C RID: 8284
		private bool \u0002;

		// Token: 0x0400205D RID: 8285
		private ArraySegment<byte> \u0003;
	}
}
