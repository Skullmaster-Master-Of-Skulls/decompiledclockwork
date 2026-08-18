using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public sealed class EntityCommandCompilationException : EntityException
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public EntityCommandCompilationException()
		{
			base.HResult = -2146232005;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002B07 File Offset: 0x00000D07
		public EntityCommandCompilationException(string message) : base(message)
		{
			base.HResult = -2146232005;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B1B File Offset: 0x00000D1B
		public EntityCommandCompilationException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232005;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002B30 File Offset: 0x00000D30
		private EntityCommandCompilationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
			base.HResult = -2146232005;
		}
	}
}
