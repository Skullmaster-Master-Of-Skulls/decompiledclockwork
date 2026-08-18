using System;
using System.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000033 RID: 51
	public abstract class BcpgObject
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00008D0C File Offset: 0x00007D0C
		public virtual byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
			bcpgOutputStream.WriteObject(this);
			return memoryStream.ToArray();
		}

		// Token: 0x0600015A RID: 346
		public abstract void Encode(BcpgOutputStream bcpgOut);
	}
}
