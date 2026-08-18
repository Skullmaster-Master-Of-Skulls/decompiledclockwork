using System;

namespace Renci.SshNet.Security.Cryptography.Ciphers
{
	// Token: 0x02000085 RID: 133
	public sealed class Arc4Cipher : StreamCipher
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0000CAD2 File Offset: 0x0000ACD2
		public override byte MinimumSize
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00016964 File Offset: 0x00014B64
		public Arc4Cipher(byte[] key, bool dischargeFirstBytes) : base(key)
		{
			this._workingKey = key;
			this.SetKey(this._workingKey);
			if (dischargeFirstBytes)
			{
				base.Encrypt(new byte[1536]);
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00016994 File Offset: 0x00014B94
		public override int EncryptBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			return this.ProcessBytes(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00016994 File Offset: 0x00014B94
		public override int DecryptBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			return this.ProcessBytes(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000169A4 File Offset: 0x00014BA4
		public override byte[] Encrypt(byte[] input, int offset, int length)
		{
			byte[] array = new byte[length];
			this.ProcessBytes(input, offset, length, array, 0);
			return array;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000169C8 File Offset: 0x00014BC8
		public override byte[] Decrypt(byte[] input)
		{
			byte[] array = new byte[input.Length];
			this.ProcessBytes(input, 0, input.Length, array, 0);
			return array;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x000169F0 File Offset: 0x00014BF0
		private int ProcessBytes(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (inputOffset + inputCount > inputBuffer.Length)
			{
				throw new IndexOutOfRangeException("input buffer too short");
			}
			if (outputOffset + inputCount > outputBuffer.Length)
			{
				throw new IndexOutOfRangeException("output buffer too short");
			}
			for (int i = 0; i < inputCount; i++)
			{
				this._x = (this._x + 1 & 255);
				this._y = ((int)this._engineState[this._x] + this._y & 255);
				byte b = this._engineState[this._x];
				this._engineState[this._x] = this._engineState[this._y];
				this._engineState[this._y] = b;
				outputBuffer[i + outputOffset] = (inputBuffer[i + inputOffset] ^ this._engineState[(int)(this._engineState[this._x] + this._engineState[this._y] & byte.MaxValue)]);
			}
			return inputCount;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00016AD8 File Offset: 0x00014CD8
		private void SetKey(byte[] keyBytes)
		{
			this._workingKey = keyBytes;
			this._x = 0;
			this._y = 0;
			if (this._engineState == null)
			{
				this._engineState = new byte[Arc4Cipher.STATE_LENGTH];
			}
			for (int i = 0; i < Arc4Cipher.STATE_LENGTH; i++)
			{
				this._engineState[i] = (byte)i;
			}
			int num = 0;
			int num2 = 0;
			for (int j = 0; j < Arc4Cipher.STATE_LENGTH; j++)
			{
				num2 = ((int)((keyBytes[num] & byte.MaxValue) + this._engineState[j]) + num2 & 255);
				byte b = this._engineState[j];
				this._engineState[j] = this._engineState[num2];
				this._engineState[num2] = b;
				num = (num + 1) % keyBytes.Length;
			}
		}

		// Token: 0x04000282 RID: 642
		private static readonly int STATE_LENGTH = 256;

		// Token: 0x04000283 RID: 643
		private byte[] _engineState;

		// Token: 0x04000284 RID: 644
		private int _x;

		// Token: 0x04000285 RID: 645
		private int _y;

		// Token: 0x04000286 RID: 646
		private byte[] _workingKey;
	}
}
