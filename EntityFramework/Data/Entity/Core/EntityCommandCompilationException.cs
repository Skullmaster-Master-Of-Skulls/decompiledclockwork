using System;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x0200034A RID: 842
	[Serializable]
	public sealed class EntityCommandCompilationException : EntityException
	{
		// Token: 0x06001DFC RID: 7676 RVA: 0x000908D6 File Offset: 0x0008EAD6
		public EntityCommandCompilationException()
		{
			base.HResult = -2146232005;
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x000908E9 File Offset: 0x0008EAE9
		public EntityCommandCompilationException(string message) : base(message)
		{
			base.HResult = -2146232005;
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x000908FD File Offset: 0x0008EAFD
		public EntityCommandCompilationException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232005;
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x00090912 File Offset: 0x0008EB12
		private EntityCommandCompilationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
			base.HResult = -2146232005;
		}

		// Token: 0x04000A44 RID: 2628
		private const int HResultCommandCompilation = -2146232005;
	}
}
