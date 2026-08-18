using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200008F RID: 143
	public class VmpcEngine : IStreamCipher
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0001838F File Offset: 0x0001738F
		public virtual string AlgorithmName
		{
			get
			{
				return "VMPC";
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00018398 File Offset: 0x00017398
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is ParametersWithIV))
			{
				throw new ArgumentException("VMPC Init parameters must include an IV");
			}
			ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
			KeyParameter keyParameter = (KeyParameter)parametersWithIV.Parameters;
			if (!(parametersWithIV.Parameters is KeyParameter))
			{
				throw new ArgumentException("VMPC Init parameters must include a key");
			}
			this.workingIV = parametersWithIV.GetIV();
			if (this.workingIV == null || this.workingIV.Length < 1 || this.workingIV.Length > 768)
			{
				throw new ArgumentException("VMPC requires 1 to 768 bytes of IV");
			}
			this.workingKey = keyParameter.GetKey();
			this.InitKey(this.workingKey, this.workingIV);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001843C File Offset: 0x0001743C
		protected virtual void InitKey(byte[] keyBytes, byte[] ivBytes)
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

		// Token: 0x06000473 RID: 1139 RVA: 0x00018590 File Offset: 0x00017590
		public virtual void ProcessBytes(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			if (inOff + len > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + len > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			for (int i = 0; i < len; i++)
			{
				this.s = this.P[(int)(this.s + this.P[(int)(this.n & byte.MaxValue)] & byte.MaxValue)];
				byte b = this.P[(int)(this.P[(int)(this.P[(int)(this.s & byte.MaxValue)] & byte.MaxValue)] + 1 & byte.MaxValue)];
				byte b2 = this.P[(int)(this.n & byte.MaxValue)];
				this.P[(int)(this.n & byte.MaxValue)] = this.P[(int)(this.s & byte.MaxValue)];
				this.P[(int)(this.s & byte.MaxValue)] = b2;
				this.n = (this.n + 1 & byte.MaxValue);
				output[i + outOff] = (input[i + inOff] ^ b);
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000186A6 File Offset: 0x000176A6
		public virtual void Reset()
		{
			this.InitKey(this.workingKey, this.workingIV);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x000186BC File Offset: 0x000176BC
		public virtual byte ReturnByte(byte input)
		{
			this.s = this.P[(int)(this.s + this.P[(int)(this.n & byte.MaxValue)] & byte.MaxValue)];
			byte b = this.P[(int)(this.P[(int)(this.P[(int)(this.s & byte.MaxValue)] & byte.MaxValue)] + 1 & byte.MaxValue)];
			byte b2 = this.P[(int)(this.n & byte.MaxValue)];
			this.P[(int)(this.n & byte.MaxValue)] = this.P[(int)(this.s & byte.MaxValue)];
			this.P[(int)(this.s & byte.MaxValue)] = b2;
			this.n = (this.n + 1 & byte.MaxValue);
			return input ^ b;
		}

		// Token: 0x04000236 RID: 566
		protected byte n;

		// Token: 0x04000237 RID: 567
		protected byte[] P;

		// Token: 0x04000238 RID: 568
		protected byte s;

		// Token: 0x04000239 RID: 569
		protected byte[] workingIV;

		// Token: 0x0400023A RID: 570
		protected byte[] workingKey;
	}
}
