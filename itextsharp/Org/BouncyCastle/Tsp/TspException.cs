using System;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x0200006E RID: 110
	public class TspException : Exception
	{
		// Token: 0x0600039B RID: 923 RVA: 0x0001387E File Offset: 0x0001287E
		public TspException()
		{
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00013886 File Offset: 0x00012886
		public TspException(string message) : base(message)
		{
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0001388F File Offset: 0x0001288F
		public TspException(string message, Exception e) : base(message, e)
		{
		}
	}
}
