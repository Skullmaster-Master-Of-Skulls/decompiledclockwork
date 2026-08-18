using System;
using System.IO;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020001E3 RID: 483
	public class PasswordException : IOException
	{
		// Token: 0x06001302 RID: 4866 RVA: 0x0006D0B3 File Offset: 0x0006C0B3
		public PasswordException(string message) : base(message)
		{
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0006D0BC File Offset: 0x0006C0BC
		public PasswordException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
