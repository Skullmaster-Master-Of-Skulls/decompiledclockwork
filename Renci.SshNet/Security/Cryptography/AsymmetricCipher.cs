using System;

namespace Renci.SshNet.Security.Cryptography
{
	// Token: 0x0200007B RID: 123
	public abstract class AsymmetricCipher : Cipher
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0000CAD2 File Offset: 0x0000ACD2
		public override byte MinimumSize
		{
			get
			{
				return 0;
			}
		}
	}
}
