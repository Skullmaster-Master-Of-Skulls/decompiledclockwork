using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020002FA RID: 762
	public class TeaEngine : IBlockCipher
	{
		// Token: 0x06001C00 RID: 7168 RVA: 0x000A7F6D File Offset: 0x000A6F6D
		public TeaEngine()
		{
			this._initialised = false;
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x000A7F7C File Offset: 0x000A6F7C
		public string AlgorithmName
		{
			get
			{
				return "TEA";
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x000A7F83 File Offset: 0x000A6F83
		public bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x000A7F86 File Offset: 0x000A6F86
		public int GetBlockSize()
		{
			return 8;
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x000A7F8C File Offset: 0x000A6F8C
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to TEA init - " + parameters.GetType().FullName);
			}
			this._forEncryption = forEncryption;
			this._initialised = true;
			KeyParameter keyParameter = (KeyParameter)parameters;
			this.setKey(keyParameter.GetKey());
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x000A7FE0 File Offset: 0x000A6FE0
		public int ProcessBlock(byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			if (!this._initialised)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			if (inOff + 8 > inBytes.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + 8 > outBytes.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			if (!this._forEncryption)
			{
				return this.decryptBlock(inBytes, inOff, outBytes, outOff);
			}
			return this.encryptBlock(inBytes, inOff, outBytes, outOff);
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x000A8051 File Offset: 0x000A7051
		public void Reset()
		{
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x000A8053 File Offset: 0x000A7053
		private void setKey(byte[] key)
		{
			this._a = Pack.BE_To_UInt32(key, 0);
			this._b = Pack.BE_To_UInt32(key, 4);
			this._c = Pack.BE_To_UInt32(key, 8);
			this._d = Pack.BE_To_UInt32(key, 12);
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x000A808C File Offset: 0x000A708C
		private int encryptBlock(byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			uint num = Pack.BE_To_UInt32(inBytes, inOff);
			uint num2 = Pack.BE_To_UInt32(inBytes, inOff + 4);
			uint num3 = 0U;
			for (int num4 = 0; num4 != 32; num4++)
			{
				num3 += 2654435769U;
				num += ((num2 << 4) + this._a ^ num2 + num3 ^ (num2 >> 5) + this._b);
				num2 += ((num << 4) + this._c ^ num + num3 ^ (num >> 5) + this._d);
			}
			Pack.UInt32_To_BE(num, outBytes, outOff);
			Pack.UInt32_To_BE(num2, outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x000A8110 File Offset: 0x000A7110
		private int decryptBlock(byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			uint num = Pack.BE_To_UInt32(inBytes, inOff);
			uint num2 = Pack.BE_To_UInt32(inBytes, inOff + 4);
			uint num3 = 3337565984U;
			for (int num4 = 0; num4 != 32; num4++)
			{
				num2 -= ((num << 4) + this._c ^ num + num3 ^ (num >> 5) + this._d);
				num -= ((num2 << 4) + this._a ^ num2 + num3 ^ (num2 >> 5) + this._b);
				num3 -= 2654435769U;
			}
			Pack.UInt32_To_BE(num, outBytes, outOff);
			Pack.UInt32_To_BE(num2, outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x0400133E RID: 4926
		private const int rounds = 32;

		// Token: 0x0400133F RID: 4927
		private const int block_size = 8;

		// Token: 0x04001340 RID: 4928
		private const uint delta = 2654435769U;

		// Token: 0x04001341 RID: 4929
		private const uint d_sum = 3337565984U;

		// Token: 0x04001342 RID: 4930
		private uint _a;

		// Token: 0x04001343 RID: 4931
		private uint _b;

		// Token: 0x04001344 RID: 4932
		private uint _c;

		// Token: 0x04001345 RID: 4933
		private uint _d;

		// Token: 0x04001346 RID: 4934
		private bool _initialised;

		// Token: 0x04001347 RID: 4935
		private bool _forEncryption;
	}
}
