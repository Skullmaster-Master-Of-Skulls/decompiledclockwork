using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x02000012 RID: 18
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00009")]
	[Serializable]
	public class BadCrcException : ZipException
	{
		// Token: 0x06000056 RID: 86 RVA: 0x0000258F File Offset: 0x0000078F
		public BadCrcException()
		{
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002597 File Offset: 0x00000797
		public BadCrcException(string message) : base(message)
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000025A0 File Offset: 0x000007A0
		protected BadCrcException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
