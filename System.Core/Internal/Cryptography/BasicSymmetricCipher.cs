using System;

namespace Internal.Cryptography
{
	// Token: 0x0200000B RID: 11
	internal abstract class BasicSymmetricCipher : IDisposable
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000023B1 File Offset: 0x000005B1
		protected BasicSymmetricCipher(byte[] iv, int blockSizeInBytes)
		{
			this.IV = iv;
			this.BlockSizeInBytes = blockSizeInBytes;
		}

		// Token: 0x0600001A RID: 26
		public abstract int Transform(byte[] input, int inputOffset, int count, byte[] output, int outputOffset);

		// Token: 0x0600001B RID: 27
		public abstract byte[] TransformFinal(byte[] input, int inputOffset, int count);

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000023C7 File Offset: 0x000005C7
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000023CF File Offset: 0x000005CF
		public int BlockSizeInBytes { get; private set; }

		// Token: 0x0600001E RID: 30 RVA: 0x000023D8 File Offset: 0x000005D8
		public virtual void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023E7 File Offset: 0x000005E7
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.IV != null)
			{
				Array.Clear(this.IV, 0, this.IV.Length);
				this.IV = null;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000240F File Offset: 0x0000060F
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002417 File Offset: 0x00000617
		private protected byte[] IV { protected get; private set; }
	}
}
