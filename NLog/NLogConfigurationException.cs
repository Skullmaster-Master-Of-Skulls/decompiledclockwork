using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace NLog
{
	// Token: 0x02000141 RID: 321
	[Serializable]
	public class NLogConfigurationException : Exception
	{
		// Token: 0x06000B4A RID: 2890 RVA: 0x00019D02 File Offset: 0x00017F02
		public NLogConfigurationException()
		{
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00019D0A File Offset: 0x00017F0A
		public NLogConfigurationException(string message) : base(message)
		{
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00019D13 File Offset: 0x00017F13
		[StringFormatMethod("message")]
		public NLogConfigurationException(string message, params object[] messageParameters) : base(string.Format(message, messageParameters))
		{
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00019D22 File Offset: 0x00017F22
		public NLogConfigurationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00019D2C File Offset: 0x00017F2C
		protected NLogConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
