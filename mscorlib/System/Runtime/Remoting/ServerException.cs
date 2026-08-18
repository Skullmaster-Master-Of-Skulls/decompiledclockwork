using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	// Token: 0x02000765 RID: 1893
	[ComVisible(true)]
	[Serializable]
	public class ServerException : SystemException
	{
		// Token: 0x06004347 RID: 17223 RVA: 0x000E5C66 File Offset: 0x000E4C66
		public ServerException() : base(ServerException._nullMessage)
		{
			base.SetErrorCode(-2146233074);
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x000E5C7E File Offset: 0x000E4C7E
		public ServerException(string message) : base(message)
		{
			base.SetErrorCode(-2146233074);
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x000E5C92 File Offset: 0x000E4C92
		public ServerException(string message, Exception InnerException) : base(message, InnerException)
		{
			base.SetErrorCode(-2146233074);
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x000E5CA7 File Offset: 0x000E4CA7
		internal ServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040021D6 RID: 8662
		private static string _nullMessage = Environment.GetResourceString("Remoting_Default");
	}
}
