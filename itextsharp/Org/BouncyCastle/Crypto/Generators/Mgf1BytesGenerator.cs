using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000249 RID: 585
	public class Mgf1BytesGenerator : IDerivationFunction
	{
		// Token: 0x0600167C RID: 5756 RVA: 0x000829F4 File Offset: 0x000819F4
		public Mgf1BytesGenerator(IDigest digest)
		{
			this.digest = digest;
			this.hLen = digest.GetDigestSize();
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00082A10 File Offset: 0x00081A10
		public void Init(IDerivationParameters parameters)
		{
			if (!typeof(MgfParameters).IsInstanceOfType(parameters))
			{
				throw new ArgumentException("MGF parameters required for MGF1Generator");
			}
			MgfParameters mgfParameters = (MgfParameters)parameters;
			this.seed = mgfParameters.GetSeed();
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x00082A4D File Offset: 0x00081A4D
		public IDigest Digest
		{
			get
			{
				return this.digest;
			}
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00082A55 File Offset: 0x00081A55
		private void ItoOSP(int i, byte[] sp)
		{
			sp[0] = (byte)((uint)i >> 24);
			sp[1] = (byte)((uint)i >> 16);
			sp[2] = (byte)((uint)i >> 8);
			sp[3] = (byte)i;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00082A74 File Offset: 0x00081A74
		public int GenerateBytes(byte[] output, int outOff, int length)
		{
			if (output.Length - length < outOff)
			{
				throw new DataLengthException("output buffer too small");
			}
			byte[] array = new byte[this.hLen];
			byte[] array2 = new byte[4];
			int num = 0;
			this.digest.Reset();
			if (length > this.hLen)
			{
				do
				{
					this.ItoOSP(num, array2);
					this.digest.BlockUpdate(this.seed, 0, this.seed.Length);
					this.digest.BlockUpdate(array2, 0, array2.Length);
					this.digest.DoFinal(array, 0);
					Array.Copy(array, 0, output, outOff + num * this.hLen, this.hLen);
				}
				while (++num < length / this.hLen);
			}
			if (num * this.hLen < length)
			{
				this.ItoOSP(num, array2);
				this.digest.BlockUpdate(this.seed, 0, this.seed.Length);
				this.digest.BlockUpdate(array2, 0, array2.Length);
				this.digest.DoFinal(array, 0);
				Array.Copy(array, 0, output, outOff + num * this.hLen, length - num * this.hLen);
			}
			return length;
		}

		// Token: 0x04000F63 RID: 3939
		private IDigest digest;

		// Token: 0x04000F64 RID: 3940
		private byte[] seed;

		// Token: 0x04000F65 RID: 3941
		private int hLen;
	}
}
