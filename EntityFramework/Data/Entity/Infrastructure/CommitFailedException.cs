using System;
using System.Data.Entity.Resources;
using System.Runtime.Serialization;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000150 RID: 336
	[Serializable]
	public class CommitFailedException : DataException
	{
		// Token: 0x06000B01 RID: 2817 RVA: 0x0003782F File Offset: 0x00035A2F
		public CommitFailedException() : base(Strings.CommitFailed)
		{
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0003783C File Offset: 0x00035A3C
		public CommitFailedException(string message) : base(message)
		{
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00037845 File Offset: 0x00035A45
		public CommitFailedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0003784F File Offset: 0x00035A4F
		protected CommitFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
