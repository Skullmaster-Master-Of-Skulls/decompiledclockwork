using System;
using System.Runtime.Serialization;

namespace System.Configuration.Provider
{
	// Token: 0x020000C3 RID: 195
	[Serializable]
	public class ProviderException : Exception
	{
		// Token: 0x060007C1 RID: 1985 RVA: 0x000208BA File Offset: 0x0001EABA
		public ProviderException()
		{
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000208C2 File Offset: 0x0001EAC2
		public ProviderException(string message) : base(message)
		{
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000208CB File Offset: 0x0001EACB
		public ProviderException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x000208D5 File Offset: 0x0001EAD5
		protected ProviderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
