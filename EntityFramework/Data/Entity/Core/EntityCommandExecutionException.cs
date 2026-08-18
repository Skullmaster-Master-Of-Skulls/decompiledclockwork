using System;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x0200034B RID: 843
	[Serializable]
	public sealed class EntityCommandExecutionException : EntityException
	{
		// Token: 0x06001E00 RID: 7680 RVA: 0x00090927 File Offset: 0x0008EB27
		public EntityCommandExecutionException()
		{
			base.HResult = -2146232004;
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x0009093A File Offset: 0x0008EB3A
		public EntityCommandExecutionException(string message) : base(message)
		{
			base.HResult = -2146232004;
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x0009094E File Offset: 0x0008EB4E
		public EntityCommandExecutionException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232004;
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x00090963 File Offset: 0x0008EB63
		private EntityCommandExecutionException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
			base.HResult = -2146232004;
		}

		// Token: 0x04000A45 RID: 2629
		private const int HResultCommandExecution = -2146232004;
	}
}
