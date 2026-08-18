using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x020001F1 RID: 497
	public class HMac : IMac
	{
		// Token: 0x06001359 RID: 4953 RVA: 0x0006EDA0 File Offset: 0x0006DDA0
		public HMac(IDigest digest)
		{
			this.digest = digest;
			this.digestSize = digest.GetDigestSize();
			this.blockLength = digest.GetByteLength();
			this.inputPad = new byte[this.blockLength];
			this.outputPad = new byte[this.blockLength];
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x0006EDF4 File Offset: 0x0006DDF4
		public string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "/HMAC";
			}
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0006EE0B File Offset: 0x0006DE0B
		public IDigest GetUnderlyingDigest()
		{
			return this.digest;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0006EE14 File Offset: 0x0006DE14
		public void Init(ICipherParameters parameters)
		{
			this.digest.Reset();
			byte[] key = ((KeyParameter)parameters).GetKey();
			int num = key.Length;
			if (num > this.blockLength)
			{
				this.digest.BlockUpdate(key, 0, key.Length);
				this.digest.DoFinal(this.inputPad, 0);
				num = this.digestSize;
			}
			else
			{
				Array.Copy(key, 0, this.inputPad, 0, num);
			}
			Array.Clear(this.inputPad, num, this.blockLength - num);
			Array.Copy(this.inputPad, 0, this.outputPad, 0, this.blockLength);
			HMac.xor(this.inputPad, 54);
			HMac.xor(this.outputPad, 92);
			this.digest.BlockUpdate(this.inputPad, 0, this.inputPad.Length);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0006EEE1 File Offset: 0x0006DEE1
		public int GetMacSize()
		{
			return this.digestSize;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0006EEE9 File Offset: 0x0006DEE9
		public void Update(byte input)
		{
			this.digest.Update(input);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0006EEF7 File Offset: 0x0006DEF7
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			this.digest.BlockUpdate(input, inOff, len);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0006EF08 File Offset: 0x0006DF08
		public int DoFinal(byte[] output, int outOff)
		{
			byte[] array = new byte[this.digestSize];
			this.digest.DoFinal(array, 0);
			this.digest.BlockUpdate(this.outputPad, 0, this.outputPad.Length);
			this.digest.BlockUpdate(array, 0, array.Length);
			int result = this.digest.DoFinal(output, outOff);
			this.digest.BlockUpdate(this.inputPad, 0, this.inputPad.Length);
			return result;
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0006EF82 File Offset: 0x0006DF82
		public void Reset()
		{
			this.digest.Reset();
			this.digest.BlockUpdate(this.inputPad, 0, this.inputPad.Length);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0006EFAC File Offset: 0x0006DFAC
		private static void xor(byte[] a, byte n)
		{
			for (int i = 0; i < a.Length; i++)
			{
				int num = i;
				a[num] ^= n;
			}
		}

		// Token: 0x04000D89 RID: 3465
		private const byte IPAD = 54;

		// Token: 0x04000D8A RID: 3466
		private const byte OPAD = 92;

		// Token: 0x04000D8B RID: 3467
		private readonly IDigest digest;

		// Token: 0x04000D8C RID: 3468
		private readonly int digestSize;

		// Token: 0x04000D8D RID: 3469
		private readonly int blockLength;

		// Token: 0x04000D8E RID: 3470
		private readonly byte[] inputPad;

		// Token: 0x04000D8F RID: 3471
		private readonly byte[] outputPad;
	}
}
