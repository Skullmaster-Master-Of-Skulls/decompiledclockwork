using System;
using System.IO;

namespace Org.BouncyCastle.OpenSsl
{
	// Token: 0x02000298 RID: 664
	public class PemException : IOException
	{
		// Token: 0x06001904 RID: 6404 RVA: 0x00092FB4 File Offset: 0x00091FB4
		public PemException(string message) : base(message)
		{
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00092FBD File Offset: 0x00091FBD
		public PemException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
