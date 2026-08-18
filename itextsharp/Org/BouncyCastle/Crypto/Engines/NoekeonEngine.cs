using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000191 RID: 401
	public class NoekeonEngine : IBlockCipher
	{
		// Token: 0x06000F95 RID: 3989 RVA: 0x00059776 File Offset: 0x00058776
		public NoekeonEngine()
		{
			this._initialised = false;
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x000597A9 File Offset: 0x000587A9
		public string AlgorithmName
		{
			get
			{
				return "Noekeon";
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x000597B0 File Offset: 0x000587B0
		public bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x000597B3 File Offset: 0x000587B3
		public int GetBlockSize()
		{
			return 16;
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x000597B8 File Offset: 0x000587B8
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("Invalid parameters passed to Noekeon init - " + parameters.GetType().Name, "parameters");
			}
			this._forEncryption = forEncryption;
			this._initialised = true;
			KeyParameter keyParameter = (KeyParameter)parameters;
			this.setKey(keyParameter.GetKey());
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00059810 File Offset: 0x00058810
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this._initialised)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			if (inOff + 16 > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + 16 > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			if (!this._forEncryption)
			{
				return this.decryptBlock(input, inOff, output, outOff);
			}
			return this.encryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00059883 File Offset: 0x00058883
		public void Reset()
		{
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00059885 File Offset: 0x00058885
		private void setKey(byte[] key)
		{
			this.subKeys[0] = Pack.BE_To_UInt32(key, 0);
			this.subKeys[1] = Pack.BE_To_UInt32(key, 4);
			this.subKeys[2] = Pack.BE_To_UInt32(key, 8);
			this.subKeys[3] = Pack.BE_To_UInt32(key, 12);
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x000598C4 File Offset: 0x000588C4
		private int encryptBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			this.state[0] = Pack.BE_To_UInt32(input, inOff);
			this.state[1] = Pack.BE_To_UInt32(input, inOff + 4);
			this.state[2] = Pack.BE_To_UInt32(input, inOff + 8);
			this.state[3] = Pack.BE_To_UInt32(input, inOff + 12);
			int i;
			for (i = 0; i < 16; i++)
			{
				this.state[0] ^= NoekeonEngine.roundConstants[i];
				this.theta(this.state, this.subKeys);
				this.pi1(this.state);
				this.gamma(this.state);
				this.pi2(this.state);
			}
			this.state[0] ^= NoekeonEngine.roundConstants[i];
			this.theta(this.state, this.subKeys);
			Pack.UInt32_To_BE(this.state[0], output, outOff);
			Pack.UInt32_To_BE(this.state[1], output, outOff + 4);
			Pack.UInt32_To_BE(this.state[2], output, outOff + 8);
			Pack.UInt32_To_BE(this.state[3], output, outOff + 12);
			return 16;
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x000599F0 File Offset: 0x000589F0
		private int decryptBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			this.state[0] = Pack.BE_To_UInt32(input, inOff);
			this.state[1] = Pack.BE_To_UInt32(input, inOff + 4);
			this.state[2] = Pack.BE_To_UInt32(input, inOff + 8);
			this.state[3] = Pack.BE_To_UInt32(input, inOff + 12);
			Array.Copy(this.subKeys, 0, this.decryptKeys, 0, this.subKeys.Length);
			this.theta(this.decryptKeys, NoekeonEngine.nullVector);
			int i;
			for (i = 16; i > 0; i--)
			{
				this.theta(this.state, this.decryptKeys);
				this.state[0] ^= NoekeonEngine.roundConstants[i];
				this.pi1(this.state);
				this.gamma(this.state);
				this.pi2(this.state);
			}
			this.theta(this.state, this.decryptKeys);
			this.state[0] ^= NoekeonEngine.roundConstants[i];
			Pack.UInt32_To_BE(this.state[0], output, outOff);
			Pack.UInt32_To_BE(this.state[1], output, outOff + 4);
			Pack.UInt32_To_BE(this.state[2], output, outOff + 8);
			Pack.UInt32_To_BE(this.state[3], output, outOff + 12);
			return 16;
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00059B48 File Offset: 0x00058B48
		private void gamma(uint[] a)
		{
			a[1] ^= (~a[3] & ~a[2]);
			a[0] ^= (a[2] & a[1]);
			uint num = a[3];
			a[3] = a[0];
			a[0] = num;
			a[2] ^= (a[0] ^ a[1] ^ a[3]);
			a[1] ^= (~a[3] & ~a[2]);
			a[0] ^= (a[2] & a[1]);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00059BF0 File Offset: 0x00058BF0
		private void theta(uint[] a, uint[] k)
		{
			uint num = a[0] ^ a[2];
			num ^= (this.rotl(num, 8) ^ this.rotl(num, 24));
			a[1] ^= num;
			a[3] ^= num;
			for (int i = 0; i < 4; i++)
			{
				a[i] ^= k[i];
			}
			num = (a[1] ^ a[3]);
			num ^= (this.rotl(num, 8) ^ this.rotl(num, 24));
			a[0] ^= num;
			a[2] ^= num;
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00059CA9 File Offset: 0x00058CA9
		private void pi1(uint[] a)
		{
			a[1] = this.rotl(a[1], 1);
			a[2] = this.rotl(a[2], 5);
			a[3] = this.rotl(a[3], 2);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00059CD2 File Offset: 0x00058CD2
		private void pi2(uint[] a)
		{
			a[1] = this.rotl(a[1], 31);
			a[2] = this.rotl(a[2], 27);
			a[3] = this.rotl(a[3], 30);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00059CFE File Offset: 0x00058CFE
		private uint rotl(uint x, int y)
		{
			return x << y | x >> 32 - y;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00059D54 File Offset: 0x00058D54
		// Note: this type is marked as 'beforefieldinit'.
		static NoekeonEngine()
		{
			uint[] array = new uint[4];
			NoekeonEngine.nullVector = array;
			NoekeonEngine.roundConstants = new uint[]
			{
				128U,
				27U,
				54U,
				108U,
				216U,
				171U,
				77U,
				154U,
				47U,
				94U,
				188U,
				99U,
				198U,
				151U,
				53U,
				106U,
				212U
			};
		}

		// Token: 0x04000B41 RID: 2881
		private const int GenericSize = 16;

		// Token: 0x04000B42 RID: 2882
		private static readonly uint[] nullVector;

		// Token: 0x04000B43 RID: 2883
		private static readonly uint[] roundConstants;

		// Token: 0x04000B44 RID: 2884
		private uint[] state = new uint[4];

		// Token: 0x04000B45 RID: 2885
		private uint[] subKeys = new uint[4];

		// Token: 0x04000B46 RID: 2886
		private uint[] decryptKeys = new uint[4];

		// Token: 0x04000B47 RID: 2887
		private bool _initialised;

		// Token: 0x04000B48 RID: 2888
		private bool _forEncryption;
	}
}
