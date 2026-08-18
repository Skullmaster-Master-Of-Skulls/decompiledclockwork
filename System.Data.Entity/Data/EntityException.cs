using System;
using System.Data.Entity;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x0200000D RID: 13
	[Serializable]
	public class EntityException : DataException
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002B96 File Offset: 0x00000D96
		public EntityException() : base(Strings.EntityClient_ProviderGeneralError)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002BA3 File Offset: 0x00000DA3
		public EntityException(string message) : base(message)
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002BAC File Offset: 0x00000DAC
		public EntityException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002BB6 File Offset: 0x00000DB6
		protected EntityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
