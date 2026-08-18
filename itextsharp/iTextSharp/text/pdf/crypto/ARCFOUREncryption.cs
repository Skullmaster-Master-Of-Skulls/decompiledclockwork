using System;

namespace iTextSharp.text.pdf.crypto
{
	// Token: 0x0200032F RID: 815
	public class ARCFOUREncryption
	{
		// Token: 0x06001D7C RID: 7548 RVA: 0x000B10FB File Offset: 0x000B00FB
		public void PrepareARCFOURKey(byte[] key)
		{
			this.PrepareARCFOURKey(key, 0, key.Length);
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x000B1108 File Offset: 0x000B0108
		public void PrepareARCFOURKey(byte[] key, int off, int len)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < 256; i++)
			{
				this.state[i] = (byte)i;
			}
			this.x = 0;
			this.y = 0;
			for (int j = 0; j < 256; j++)
			{
				num2 = ((int)(key[num + off] + this.state[j]) + num2 & 255);
				byte b = this.state[j];
				this.state[j] = this.state[num2];
				this.state[num2] = b;
				num = (num + 1) % len;
			}
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x000B1198 File Offset: 0x000B0198
		public void EncryptARCFOUR(byte[] dataIn, int off, int len, byte[] dataOut, int offOut)
		{
			int num = len + off;
			for (int i = off; i < num; i++)
			{
				this.x = (this.x + 1 & 255);
				this.y = ((int)this.state[this.x] + this.y & 255);
				byte b = this.state[this.x];
				this.state[this.x] = this.state[this.y];
				this.state[this.y] = b;
				dataOut[i - off + offOut] = (dataIn[i] ^ this.state[(int)(this.state[this.x] + this.state[this.y] & byte.MaxValue)]);
			}
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x000B125B File Offset: 0x000B025B
		public void EncryptARCFOUR(byte[] data, int off, int len)
		{
			this.EncryptARCFOUR(data, off, len, data, off);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x000B1268 File Offset: 0x000B0268
		public void EncryptARCFOUR(byte[] dataIn, byte[] dataOut)
		{
			this.EncryptARCFOUR(dataIn, 0, dataIn.Length, dataOut, 0);
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x000B1277 File Offset: 0x000B0277
		public void EncryptARCFOUR(byte[] data)
		{
			this.EncryptARCFOUR(data, 0, data.Length, data, 0);
		}

		// Token: 0x04001440 RID: 5184
		private byte[] state = new byte[256];

		// Token: 0x04001441 RID: 5185
		private int x;

		// Token: 0x04001442 RID: 5186
		private int y;
	}
}
