using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	public sealed class EntityCommandExecutionException : EntityException
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002B45 File Offset: 0x00000D45
		public EntityCommandExecutionException()
		{
			base.HResult = -2146232004;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002B58 File Offset: 0x00000D58
		public EntityCommandExecutionException(string message) : base(message)
		{
			base.HResult = -2146232004;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002B6C File Offset: 0x00000D6C
		public EntityCommandExecutionException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232004;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002B81 File Offset: 0x00000D81
		private EntityCommandExecutionException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
			base.HResult = -2146232004;
		}
	}
}
