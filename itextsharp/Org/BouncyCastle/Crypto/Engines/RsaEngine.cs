using System;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200024D RID: 589
	public class RsaEngine : IAsymmetricBlockCipher
	{
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x00082F66 File Offset: 0x00081F66
		public string AlgorithmName
		{
			get
			{
				return "RSA";
			}
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00082F6D File Offset: 0x00081F6D
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (this.core == null)
			{
				this.core = new RsaCoreEngine();
			}
			this.core.Init(forEncryption, parameters);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00082F8F File Offset: 0x00081F8F
		public int GetInputBlockSize()
		{
			return this.core.GetInputBlockSize();
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00082F9C File Offset: 0x00081F9C
		public int GetOutputBlockSize()
		{
			return this.core.GetOutputBlockSize();
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00082FA9 File Offset: 0x00081FA9
		public byte[] ProcessBlock(byte[] inBuf, int inOff, int inLen)
		{
			if (this.core == null)
			{
				throw new InvalidOperationException("RSA engine not initialised");
			}
			return this.core.ConvertOutput(this.core.ProcessBlock(this.core.ConvertInput(inBuf, inOff, inLen)));
		}

		// Token: 0x04000F6C RID: 3948
		private RsaCoreEngine core;
	}
}
