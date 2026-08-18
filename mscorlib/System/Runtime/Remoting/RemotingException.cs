using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	// Token: 0x02000764 RID: 1892
	[ComVisible(true)]
	[Serializable]
	public class RemotingException : SystemException
	{
		// Token: 0x06004342 RID: 17218 RVA: 0x000E5C0A File Offset: 0x000E4C0A
		public RemotingException() : base(RemotingException._nullMessage)
		{
			base.SetErrorCode(-2146233077);
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x000E5C22 File Offset: 0x000E4C22
		public RemotingException(string message) : base(message)
		{
			base.SetErrorCode(-2146233077);
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x000E5C36 File Offset: 0x000E4C36
		public RemotingException(string message, Exception InnerException) : base(message, InnerException)
		{
			base.SetErrorCode(-2146233077);
		}

		// Token: 0x06004345 RID: 17221 RVA: 0x000E5C4B File Offset: 0x000E4C4B
		protected RemotingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040021D5 RID: 8661
		private static string _nullMessage = Environment.GetResourceString("Remoting_Default");
	}
}
