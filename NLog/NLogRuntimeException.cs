using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace NLog
{
	// Token: 0x02000142 RID: 322
	[Serializable]
	public class NLogRuntimeException : Exception
	{
		// Token: 0x06000B4F RID: 2895 RVA: 0x00019D36 File Offset: 0x00017F36
		public NLogRuntimeException()
		{
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00019D3E File Offset: 0x00017F3E
		public NLogRuntimeException(string message) : base(message)
		{
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00019D47 File Offset: 0x00017F47
		[StringFormatMethod("message")]
		public NLogRuntimeException(string message, params object[] messageParameters) : base(string.Format(message, messageParameters))
		{
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00019D56 File Offset: 0x00017F56
		public NLogRuntimeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00019D60 File Offset: 0x00017F60
		protected NLogRuntimeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
