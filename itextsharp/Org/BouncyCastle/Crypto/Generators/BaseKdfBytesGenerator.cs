using System;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x0200001C RID: 28
	public class BaseKdfBytesGenerator : IDerivationFunction
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00005F3D File Offset: 0x00004F3D
		protected BaseKdfBytesGenerator(int counterStart, IDigest digest)
		{
			this.counterStart = counterStart;
			this.digest = digest;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005F54 File Offset: 0x00004F54
		public void Init(IDerivationParameters parameters)
		{
			if (parameters is KdfParameters)
			{
				KdfParameters kdfParameters = (KdfParameters)parameters;
				this.shared = kdfParameters.GetSharedSecret();
				this.iv = kdfParameters.GetIV();
				return;
			}
			if (parameters is Iso18033KdfParameters)
			{
				Iso18033KdfParameters iso18033KdfParameters = (Iso18033KdfParameters)parameters;
				this.shared = iso18033KdfParameters.GetSeed();
				this.iv = null;
				return;
			}
			throw new ArgumentException("KDF parameters required for KDF Generator");
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00005FB6 File Offset: 0x00004FB6
		public IDigest Digest
		{
			get
			{
				return this.digest;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005FC0 File Offset: 0x00004FC0
		public int GenerateBytes(byte[] output, int outOff, int length)
		{
			if (output.Length - length < outOff)
			{
				throw new DataLengthException("output buffer too small");
			}
			long num = (long)length;
			int digestSize = this.digest.GetDigestSize();
			if (num > 8589934591L)
			{
				throw new ArgumentException("Output length too large");
			}
			int num2 = (int)((num + (long)digestSize - 1L) / (long)digestSize);
			byte[] array = new byte[this.digest.GetDigestSize()];
			int num3 = this.counterStart;
			for (int i = 0; i < num2; i++)
			{
				this.digest.BlockUpdate(this.shared, 0, this.shared.Length);
				this.digest.Update((byte)(num3 >> 24));
				this.digest.Update((byte)(num3 >> 16));
				this.digest.Update((byte)(num3 >> 8));
				this.digest.Update((byte)num3);
				if (this.iv != null)
				{
					this.digest.BlockUpdate(this.iv, 0, this.iv.Length);
				}
				this.digest.DoFinal(array, 0);
				if (length > digestSize)
				{
					Array.Copy(array, 0, output, outOff, digestSize);
					outOff += digestSize;
					length -= digestSize;
				}
				else
				{
					Array.Copy(array, 0, output, outOff, length);
				}
				num3++;
			}
			this.digest.Reset();
			return (int)num;
		}

		// Token: 0x0400005A RID: 90
		private int counterStart;

		// Token: 0x0400005B RID: 91
		private IDigest digest;

		// Token: 0x0400005C RID: 92
		private byte[] shared;

		// Token: 0x0400005D RID: 93
		private byte[] iv;
	}
}
