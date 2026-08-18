using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.IO
{
	// Token: 0x020004B1 RID: 1201
	public class StreamOverflowException : IOException
	{
		// Token: 0x06002893 RID: 10387 RVA: 0x000F64CF File Offset: 0x000F54CF
		public StreamOverflowException()
		{
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000F64D7 File Offset: 0x000F54D7
		public StreamOverflowException(string message) : base(message)
		{
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x000F64E0 File Offset: 0x000F54E0
		public StreamOverflowException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
