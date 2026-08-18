using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x02000014 RID: 20
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00007")]
	[Serializable]
	public class BadStateException : ZipException
	{
		// Token: 0x0600005C RID: 92 RVA: 0x000025C5 File Offset: 0x000007C5
		public BadStateException()
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000025CD File Offset: 0x000007CD
		public BadStateException(string message) : base(message)
		{
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000025D6 File Offset: 0x000007D6
		public BadStateException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000025E0 File Offset: 0x000007E0
		protected BadStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
