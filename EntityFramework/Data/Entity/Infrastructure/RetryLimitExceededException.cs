using System;
using System.Data.Entity.Core;
using System.Runtime.Serialization;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000291 RID: 657
	[Serializable]
	public sealed class RetryLimitExceededException : EntityException
	{
		// Token: 0x06001704 RID: 5892 RVA: 0x00072C60 File Offset: 0x00070E60
		public RetryLimitExceededException()
		{
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x00072C68 File Offset: 0x00070E68
		public RetryLimitExceededException(string message) : base(message)
		{
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x00072C71 File Offset: 0x00070E71
		public RetryLimitExceededException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x00072C7B File Offset: 0x00070E7B
		private RetryLimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
