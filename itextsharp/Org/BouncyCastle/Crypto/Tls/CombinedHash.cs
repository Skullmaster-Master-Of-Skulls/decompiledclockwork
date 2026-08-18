using System;
using Org.BouncyCastle.Crypto.Digests;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020005A5 RID: 1445
	public class CombinedHash : IDigest
	{
		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x00136F2C File Offset: 0x00135F2C
		public string AlgorithmName
		{
			get
			{
				return this.md5.AlgorithmName + " and " + this.sha1.AlgorithmName + " for TLS 1.0";
			}
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x00136F53 File Offset: 0x00135F53
		public int GetByteLength()
		{
			return Math.Max(this.md5.GetByteLength(), this.sha1.GetByteLength());
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x00136F70 File Offset: 0x00135F70
		public int GetDigestSize()
		{
			return this.md5.GetDigestSize() + this.sha1.GetDigestSize();
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x00136F89 File Offset: 0x00135F89
		public void Update(byte input)
		{
			this.md5.Update(input);
			this.sha1.Update(input);
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x00136FA3 File Offset: 0x00135FA3
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			this.md5.BlockUpdate(input, inOff, len);
			this.sha1.BlockUpdate(input, inOff, len);
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x00136FC4 File Offset: 0x00135FC4
		public int DoFinal(byte[] output, int outOff)
		{
			int num = this.md5.DoFinal(output, outOff);
			int num2 = this.sha1.DoFinal(output, outOff + num);
			return num + num2;
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x00136FF2 File Offset: 0x00135FF2
		public void Reset()
		{
			this.md5.Reset();
			this.sha1.Reset();
		}

		// Token: 0x04002248 RID: 8776
		private IDigest md5 = new MD5Digest();

		// Token: 0x04002249 RID: 8777
		private IDigest sha1 = new Sha1Digest();
	}
}
