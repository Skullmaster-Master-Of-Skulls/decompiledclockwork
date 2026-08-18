using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000510 RID: 1296
	public abstract class OutputStreamPacket
	{
		// Token: 0x06002C50 RID: 11344 RVA: 0x0010E104 File Offset: 0x0010D104
		internal OutputStreamPacket(BcpgOutputStream bcpgOut)
		{
			if (bcpgOut == null)
			{
				throw new ArgumentNullException("bcpgOut");
			}
			this.bcpgOut = bcpgOut;
		}

		// Token: 0x06002C51 RID: 11345
		public abstract BcpgOutputStream Open();

		// Token: 0x06002C52 RID: 11346
		public abstract void Close();

		// Token: 0x04001E8B RID: 7819
		private readonly BcpgOutputStream bcpgOut;
	}
}
