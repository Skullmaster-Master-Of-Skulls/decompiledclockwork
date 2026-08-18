using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x02000010 RID: 16
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000B")]
	[Serializable]
	public class BadPasswordException : ZipException
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002545 File Offset: 0x00000745
		public BadPasswordException()
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000254D File Offset: 0x0000074D
		public BadPasswordException(string message) : base(message)
		{
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002556 File Offset: 0x00000756
		public BadPasswordException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002560 File Offset: 0x00000760
		protected BadPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
