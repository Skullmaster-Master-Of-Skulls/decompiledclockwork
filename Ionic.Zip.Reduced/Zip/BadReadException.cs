using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x02000011 RID: 17
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000A")]
	[Serializable]
	public class BadReadException : ZipException
	{
		// Token: 0x06000052 RID: 82 RVA: 0x0000256A File Offset: 0x0000076A
		public BadReadException()
		{
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002572 File Offset: 0x00000772
		public BadReadException(string message) : base(message)
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000257B File Offset: 0x0000077B
		public BadReadException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002585 File Offset: 0x00000785
		protected BadReadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
