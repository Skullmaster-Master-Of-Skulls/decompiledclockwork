using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200024E RID: 590
	public class RC4Engine : IStreamCipher
	{
		// Token: 0x06001690 RID: 5776 RVA: 0x00082FEC File Offset: 0x00081FEC
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is KeyParameter)
			{
				this.workingKey = ((KeyParameter)parameters).GetKey();
				this.SetKey(this.workingKey);
				return;
			}
			throw new ArgumentException("invalid parameter passed to RC4 init - " + parameters.GetType().ToString());
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x00083039 File Offset: 0x00082039
		public string AlgorithmName
		{
			get
			{
				return "RC4";
			}
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00083040 File Offset: 0x00082040
		public byte ReturnByte(byte input)
		{
			this.x = (this.x + 1 & 255);
			this.y = ((int)this.engineState[this.x] + this.y & 255);
			byte b = this.engineState[this.x];
			this.engineState[this.x] = this.engineState[this.y];
			this.engineState[this.y] = b;
			return input ^ this.engineState[(int)(this.engineState[this.x] + this.engineState[this.y] & byte.MaxValue)];
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x000830E4 File Offset: 0x000820E4
		public void ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			if (inOff + length > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + length > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			for (int i = 0; i < length; i++)
			{
				this.x = (this.x + 1 & 255);
				this.y = ((int)this.engineState[this.x] + this.y & 255);
				byte b = this.engineState[this.x];
				this.engineState[this.x] = this.engineState[this.y];
				this.engineState[this.y] = b;
				output[i + outOff] = (input[i + inOff] ^ this.engineState[(int)(this.engineState[this.x] + this.engineState[this.y] & byte.MaxValue)]);
			}
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x000831CB File Offset: 0x000821CB
		public void Reset()
		{
			this.SetKey(this.workingKey);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x000831DC File Offset: 0x000821DC
		private void SetKey(byte[] keyBytes)
		{
			this.workingKey = keyBytes;
			this.x = 0;
			this.y = 0;
			if (this.engineState == null)
			{
				this.engineState = new byte[RC4Engine.STATE_LENGTH];
			}
			for (int i = 0; i < RC4Engine.STATE_LENGTH; i++)
			{
				this.engineState[i] = (byte)i;
			}
			int num = 0;
			int num2 = 0;
			for (int j = 0; j < RC4Engine.STATE_LENGTH; j++)
			{
				num2 = ((int)((keyBytes[num] & byte.MaxValue) + this.engineState[j]) + num2 & 255);
				byte b = this.engineState[j];
				this.engineState[j] = this.engineState[num2];
				this.engineState[num2] = b;
				num = (num + 1) % keyBytes.Length;
			}
		}

		// Token: 0x04000F6D RID: 3949
		private static readonly int STATE_LENGTH = 256;

		// Token: 0x04000F6E RID: 3950
		private byte[] engineState;

		// Token: 0x04000F6F RID: 3951
		private int x;

		// Token: 0x04000F70 RID: 3952
		private int y;

		// Token: 0x04000F71 RID: 3953
		private byte[] workingKey;
	}
}
