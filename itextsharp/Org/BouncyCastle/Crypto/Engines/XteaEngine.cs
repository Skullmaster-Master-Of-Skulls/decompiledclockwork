using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x020004C6 RID: 1222
	public class XteaEngine : IBlockCipher
	{
		// Token: 0x060029A2 RID: 10658 RVA: 0x000FD3A7 File Offset: 0x000FC3A7
		public XteaEngine()
		{
			this._initialised = false;
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060029A3 RID: 10659 RVA: 0x000FD3DC File Offset: 0x000FC3DC
		public string AlgorithmName
		{
			get
			{
				return "XTEA";
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060029A4 RID: 10660 RVA: 0x000FD3E3 File Offset: 0x000FC3E3
		public bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x000FD3E6 File Offset: 0x000FC3E6
		public int GetBlockSize()
		{
			return 8;
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x000FD3EC File Offset: 0x000FC3EC
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

		// Token: 0x060029A7 RID: 10663 RVA: 0x000FD440 File Offset: 0x000FC440
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

		// Token: 0x060029A8 RID: 10664 RVA: 0x000FD4B1 File Offset: 0x000FC4B1
		public void Reset()
		{
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x000FD4B4 File Offset: 0x000FC4B4
		private void setKey(byte[] key)
		{
			int i;
			int num = i = 0;
			while (i < 4)
			{
				this._S[i] = Pack.BE_To_UInt32(key, num);
				i++;
				num += 4;
			}
			num = (i = 0);
			while (i < 32)
			{
				this._sum0[i] = (uint)(num + (int)this._S[num & 3]);
				num += -1640531527;
				this._sum1[i] = (uint)(num + (int)this._S[num >> 11 & 3]);
				i++;
			}
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x000FD524 File Offset: 0x000FC524
		private int encryptBlock(byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			uint num = Pack.BE_To_UInt32(inBytes, inOff);
			uint num2 = Pack.BE_To_UInt32(inBytes, inOff + 4);
			for (int i = 0; i < 32; i++)
			{
				num += ((num2 << 4 ^ num2 >> 5) + num2 ^ this._sum0[i]);
				num2 += ((num << 4 ^ num >> 5) + num ^ this._sum1[i]);
			}
			Pack.UInt32_To_BE(num, outBytes, outOff);
			Pack.UInt32_To_BE(num2, outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x000FD590 File Offset: 0x000FC590
		private int decryptBlock(byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			uint num = Pack.BE_To_UInt32(inBytes, inOff);
			uint num2 = Pack.BE_To_UInt32(inBytes, inOff + 4);
			for (int i = 31; i >= 0; i--)
			{
				num2 -= ((num << 4 ^ num >> 5) + num ^ this._sum1[i]);
				num -= ((num2 << 4 ^ num2 >> 5) + num2 ^ this._sum0[i]);
			}
			Pack.UInt32_To_BE(num, outBytes, outOff);
			Pack.UInt32_To_BE(num2, outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x04001D08 RID: 7432
		private const int rounds = 32;

		// Token: 0x04001D09 RID: 7433
		private const int block_size = 8;

		// Token: 0x04001D0A RID: 7434
		private const int delta = -1640531527;

		// Token: 0x04001D0B RID: 7435
		private uint[] _S = new uint[4];

		// Token: 0x04001D0C RID: 7436
		private uint[] _sum0 = new uint[32];

		// Token: 0x04001D0D RID: 7437
		private uint[] _sum1 = new uint[32];

		// Token: 0x04001D0E RID: 7438
		private bool _initialised;

		// Token: 0x04001D0F RID: 7439
		private bool _forEncryption;
	}
}
