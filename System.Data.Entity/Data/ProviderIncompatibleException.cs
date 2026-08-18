using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	public sealed class ProviderIncompatibleException : EntityException
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002F89 File Offset: 0x00001189
		public ProviderIncompatibleException()
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002F91 File Offset: 0x00001191
		public ProviderIncompatibleException(string message) : base(message)
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F9A File Offset: 0x0000119A
		public ProviderIncompatibleException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002FA4 File Offset: 0x000011A4
		private ProviderIncompatibleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
