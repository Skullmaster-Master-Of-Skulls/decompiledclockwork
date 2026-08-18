using System;

namespace Telerik.Pdf.Security
{
	// Token: 0x02001678 RID: 5752
	internal class Arc4
	{
		// Token: 0x0600DE66 RID: 56934 RVA: 0x003094B6 File Offset: 0x003076B6
		internal Arc4()
		{
		}

		// Token: 0x0600DE67 RID: 56935 RVA: 0x003094CE File Offset: 0x003076CE
		internal Arc4(byte[] key)
		{
			this.Initialise(key);
		}

		// Token: 0x0600DE68 RID: 56936 RVA: 0x003094F0 File Offset: 0x003076F0
		internal Arc4(byte[] key, int offset, int length)
		{
			byte[] array = new byte[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = key[offset + i];
			}
			this.Initialise(array);
		}

		// Token: 0x0600DE69 RID: 56937 RVA: 0x00309538 File Offset: 0x00307738
		internal void Initialise(byte[] key)
		{
			for (int i = 0; i < 256; i++)
			{
				this.state[i] = (byte)i;
			}
			int j = 0;
			int num = 0;
			while (j < 256)
			{
				num = (num + (int)this.state[j] + (int)key[j % key.Length]) % 256;
				byte b = this.state[j];
				this.state[j] = this.state[num];
				this.state[num] = b;
				j++;
			}
			this.x = 0;
			this.y = 0;
		}

		// Token: 0x0600DE6A RID: 56938 RVA: 0x003095BC File Offset: 0x003077BC
		internal void Encrypt(byte[] dataIn, byte[] dataOut)
		{
			for (int i = 0; i < dataIn.Length; i++)
			{
				dataOut[i] = (dataIn[i] ^ this.Arc4Byte());
			}
		}

		// Token: 0x0600DE6B RID: 56939 RVA: 0x003095E8 File Offset: 0x003077E8
		private byte Arc4Byte()
		{
			this.x = (this.x + 1) % 256;
			this.y = (this.y + (int)this.state[this.x]) % 256;
			byte b = this.state[this.x];
			this.state[this.x] = this.state[this.y];
			this.state[this.y] = b;
			return this.state[(int)(this.state[this.x] + this.state[this.y]) % 256];
		}

		// Token: 0x04003FF6 RID: 16374
		private byte[] state = new byte[256];

		// Token: 0x04003FF7 RID: 16375
		private int x;

		// Token: 0x04003FF8 RID: 16376
		private int y;
	}
}
