using System;

namespace Renci.SshNet.Security
{
	// Token: 0x0200006A RID: 106
	public class CertificateHostAlgorithm : HostAlgorithm
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		public override byte[] Data
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00013D9A File Offset: 0x00011F9A
		public CertificateHostAlgorithm(string name) : base(name)
		{
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		public override byte[] Sign(byte[] data)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		public override bool VerifySignature(byte[] data, byte[] signature)
		{
			throw new NotImplementedException();
		}
	}
}
