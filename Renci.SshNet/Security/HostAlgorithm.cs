using System;

namespace Renci.SshNet.Security
{
	// Token: 0x0200006E RID: 110
	public abstract class HostAlgorithm
	{
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x000142A4 File Offset: 0x000124A4
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x000142AC File Offset: 0x000124AC
		public string Name { get; private set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600067A RID: 1658
		public abstract byte[] Data { get; }

		// Token: 0x0600067B RID: 1659 RVA: 0x000142B5 File Offset: 0x000124B5
		protected HostAlgorithm(string name)
		{
			this.Name = name;
		}

		// Token: 0x0600067C RID: 1660
		public abstract byte[] Sign(byte[] data);

		// Token: 0x0600067D RID: 1661
		public abstract bool VerifySignature(byte[] data, byte[] signature);
	}
}
