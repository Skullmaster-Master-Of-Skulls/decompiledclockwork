using System;
using System.Runtime.Serialization;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002EB RID: 747
	[Serializable]
	public class PingException : InvalidOperationException
	{
		// Token: 0x06001A54 RID: 6740 RVA: 0x0007FE1D File Offset: 0x0007E01D
		internal PingException()
		{
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x0007FE25 File Offset: 0x0007E025
		protected PingException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x0007FE2F File Offset: 0x0007E02F
		public PingException(string message) : base(message)
		{
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x0007FE38 File Offset: 0x0007E038
		public PingException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
