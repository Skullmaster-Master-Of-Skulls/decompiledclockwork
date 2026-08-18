using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	// Token: 0x02000766 RID: 1894
	[ComVisible(true)]
	[Serializable]
	public class RemotingTimeoutException : RemotingException
	{
		// Token: 0x0600434C RID: 17228 RVA: 0x000E5CC2 File Offset: 0x000E4CC2
		public RemotingTimeoutException() : base(RemotingTimeoutException._nullMessage)
		{
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x000E5CCF File Offset: 0x000E4CCF
		public RemotingTimeoutException(string message) : base(message)
		{
			base.SetErrorCode(-2146233077);
		}

		// Token: 0x0600434E RID: 17230 RVA: 0x000E5CE3 File Offset: 0x000E4CE3
		public RemotingTimeoutException(string message, Exception InnerException) : base(message, InnerException)
		{
			base.SetErrorCode(-2146233077);
		}

		// Token: 0x0600434F RID: 17231 RVA: 0x000E5CF8 File Offset: 0x000E4CF8
		internal RemotingTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040021D7 RID: 8663
		private static string _nullMessage = Environment.GetResourceString("Remoting_Default");
	}
}
