using System;
using System.Data.Entity.Resources;
using System.Runtime.Serialization;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000761 RID: 1889
	[Serializable]
	public class UnintentionalCodeFirstException : InvalidOperationException
	{
		// Token: 0x06005542 RID: 21826 RVA: 0x00172DB8 File Offset: 0x00170FB8
		public UnintentionalCodeFirstException() : base(Strings.UnintentionalCodeFirstException_Message)
		{
		}

		// Token: 0x06005543 RID: 21827 RVA: 0x00172DC5 File Offset: 0x00170FC5
		protected UnintentionalCodeFirstException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06005544 RID: 21828 RVA: 0x00172DCF File Offset: 0x00170FCF
		public UnintentionalCodeFirstException(string message) : base(message)
		{
		}

		// Token: 0x06005545 RID: 21829 RVA: 0x00172DD8 File Offset: 0x00170FD8
		public UnintentionalCodeFirstException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
