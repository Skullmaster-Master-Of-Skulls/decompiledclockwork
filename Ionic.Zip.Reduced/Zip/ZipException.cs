using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x0200000F RID: 15
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00006")]
	[Serializable]
	public class ZipException : Exception
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00002520 File Offset: 0x00000720
		public ZipException()
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002528 File Offset: 0x00000728
		public ZipException(string message) : base(message)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002531 File Offset: 0x00000731
		public ZipException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000253B File Offset: 0x0000073B
		protected ZipException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
