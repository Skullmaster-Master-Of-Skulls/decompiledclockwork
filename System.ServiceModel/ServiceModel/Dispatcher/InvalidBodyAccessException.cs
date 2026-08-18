using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200046B RID: 1131
	[Serializable]
	public abstract class InvalidBodyAccessException : SystemException
	{
		// Token: 0x06002BEF RID: 11247 RVA: 0x000AC41C File Offset: 0x000AA61C
		protected InvalidBodyAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000AC426 File Offset: 0x000AA626
		protected InvalidBodyAccessException(string message) : this(message, null)
		{
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000AC430 File Offset: 0x000AA630
		protected InvalidBodyAccessException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
