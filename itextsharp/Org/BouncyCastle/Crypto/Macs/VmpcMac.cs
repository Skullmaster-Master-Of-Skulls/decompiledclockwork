using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x02000128 RID: 296
	public class VmpcMac : IMac
	{
		// Token: 0x06000ACB RID: 2763 RVA: 0x0003876C File Offset: 0x0003776C
		public virtual int DoFinal(byte[] output, int outOff)
		{
			for (int i = 1; i < 25; i++)
			{
				this.s = this.P[(int)(this.s + this.P[(int)(this.n & byte.MaxValue)] & byte.MaxValue)];
				this.x4 = this.P[(int)(this.x4 + this.x3) + i & 255];
				this.x3 = this.P[(int)(this.x3 + this.x2) + i & 255];
				this.x2 = this.P[(int)(this.x2 + this.x1) + i & 255];
				this.x1 = this.P[(int)(this.x1 + this.s) + i & 255];
				this.T[(int)(this.g & 31)] = (this.T[(int)(this.g & 31)] ^ this.x1);
				this.T[(int)(this.g + 1 & 31)] = (this.T[(int)(this.g + 1 & 31)] ^ this.x2);
				this.T[(int)(this.g + 2 & 31)] = (this.T[(int)(this.g + 2 & 31)] ^ this.x3);
				this.T[(int)(this.g + 3 & 31)] = (this.T[(int)(this.g + 3 & 31)] ^ this.x4);
				this.g = (this.g + 4 & 31);
				byte b = this.P[(int)(this.n & byte.MaxValue)];
				this.P[(int)(this.n & byte.MaxValue)] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b;
				this.n = (this.n + 1 & byte.MaxValue);
			}
			for (int j = 0; j < 768; j++)
			{
				this.s = this.P[(int)(this.s + this.P[j & 255] + this.T[j & 31] & byte.MaxValue)];
				byte b2 = this.P[j & 255];
				this.P[j & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b2;
			}
			byte[] array = new byte[20];
			for (int k = 0; k < 20; k++)
			{
				this.s = this.P[(int)(this.s + this.P[k & 255] & byte.MaxValue)];
				array[k] = this.P[(int)(this.P[(int)(this.P[(int)(this.s & byte.MaxValue)] & byte.MaxValue)] + 1 & byte.MaxValue)];
				byte b3 = this.P[k & 255];
				this.P[k & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b3;
			}
			Array.Copy(array, 0, output, outOff, array.Length);
			this.Reset();
			return array.Length;
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00038ACA File Offset: 0x00037ACA
		public virtual string AlgorithmName
		{
			get
			{
				return "VMPC-MAC";
			}
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00038AD1 File Offset: 0x00037AD1
		public virtual int GetMacSize()
		{
			return 20;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00038AD8 File Offset: 0x00037AD8
		public virtual void Init(ICipherParameters parameters)
		{
			if (!(parameters is ParametersWithIV))
			{
				throw new ArgumentException("VMPC-MAC Init parameters must include an IV", "parameters");
			}
			ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
			KeyParameter keyParameter = (KeyParameter)parametersWithIV.Parameters;
			if (!(parametersWithIV.Parameters is KeyParameter))
			{
				throw new ArgumentException("VMPC-MAC Init parameters must include a key", "parameters");
			}
			this.workingIV = parametersWithIV.GetIV();
			if (this.workingIV == null || this.workingIV.Length < 1 || this.workingIV.Length > 768)
			{
				throw new ArgumentException("VMPC-MAC requires 1 to 768 bytes of IV", "parameters");
			}
			this.workingKey = keyParameter.GetKey();
			this.Reset();
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00038B80 File Offset: 0x00037B80
		private void initKey(byte[] keyBytes, byte[] ivBytes)
		{
			this.s = 0;
			this.P = new byte[256];
			for (int i = 0; i < 256; i++)
			{
				this.P[i] = (byte)i;
			}
			for (int j = 0; j < 768; j++)
			{
				this.s = this.P[(int)(this.s + this.P[j & 255] + keyBytes[j % keyBytes.Length] & byte.MaxValue)];
				byte b = this.P[j & 255];
				this.P[j & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b;
			}
			for (int k = 0; k < 768; k++)
			{
				this.s = this.P[(int)(this.s + this.P[k & 255] + ivBytes[k % ivBytes.Length] & byte.MaxValue)];
				byte b2 = this.P[k & 255];
				this.P[k & 255] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b2;
			}
			this.n = 0;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00038CD4 File Offset: 0x00037CD4
		public virtual void Reset()
		{
			this.initKey(this.workingKey, this.workingIV);
			this.g = (this.x1 = (this.x2 = (this.x3 = (this.x4 = (this.n = 0)))));
			this.T = new byte[32];
			for (int i = 0; i < 32; i++)
			{
				this.T[i] = 0;
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00038D50 File Offset: 0x00037D50
		public virtual void Update(byte input)
		{
			this.s = this.P[(int)(this.s + this.P[(int)(this.n & byte.MaxValue)] & byte.MaxValue)];
			byte b = input ^ this.P[(int)(this.P[(int)(this.P[(int)(this.s & byte.MaxValue)] & byte.MaxValue)] + 1 & byte.MaxValue)];
			this.x4 = this.P[(int)(this.x4 + this.x3 & byte.MaxValue)];
			this.x3 = this.P[(int)(this.x3 + this.x2 & byte.MaxValue)];
			this.x2 = this.P[(int)(this.x2 + this.x1 & byte.MaxValue)];
			this.x1 = this.P[(int)(this.x1 + this.s + b & byte.MaxValue)];
			this.T[(int)(this.g & 31)] = (this.T[(int)(this.g & 31)] ^ this.x1);
			this.T[(int)(this.g + 1 & 31)] = (this.T[(int)(this.g + 1 & 31)] ^ this.x2);
			this.T[(int)(this.g + 2 & 31)] = (this.T[(int)(this.g + 2 & 31)] ^ this.x3);
			this.T[(int)(this.g + 3 & 31)] = (this.T[(int)(this.g + 3 & 31)] ^ this.x4);
			this.g = (this.g + 4 & 31);
			byte b2 = this.P[(int)(this.n & byte.MaxValue)];
			this.P[(int)(this.n & byte.MaxValue)] = this.P[(int)(this.s & byte.MaxValue)];
			this.P[(int)(this.s & byte.MaxValue)] = b2;
			this.n = (this.n + 1 & byte.MaxValue);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00038F60 File Offset: 0x00037F60
		public virtual void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (inOff + len > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			for (int i = 0; i < len; i++)
			{
				this.Update(input[i]);
			}
		}

		// Token: 0x0400088B RID: 2187
		private byte g;

		// Token: 0x0400088C RID: 2188
		private byte n;

		// Token: 0x0400088D RID: 2189
		private byte[] P;

		// Token: 0x0400088E RID: 2190
		private byte s;

		// Token: 0x0400088F RID: 2191
		private byte[] T;

		// Token: 0x04000890 RID: 2192
		private byte[] workingIV;

		// Token: 0x04000891 RID: 2193
		private byte[] workingKey;

		// Token: 0x04000892 RID: 2194
		private byte x1;

		// Token: 0x04000893 RID: 2195
		private byte x2;

		// Token: 0x04000894 RID: 2196
		private byte x3;

		// Token: 0x04000895 RID: 2197
		private byte x4;
	}
}
