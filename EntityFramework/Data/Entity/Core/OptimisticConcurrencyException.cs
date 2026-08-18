using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020005C2 RID: 1474
	[Serializable]
	public sealed class OptimisticConcurrencyException : UpdateException
	{
		// Token: 0x06003B05 RID: 15109 RVA: 0x00117D76 File Offset: 0x00115F76
		public OptimisticConcurrencyException()
		{
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x00117D7E File Offset: 0x00115F7E
		public OptimisticConcurrencyException(string message) : base(message)
		{
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x00117D87 File Offset: 0x00115F87
		public OptimisticConcurrencyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x00117D91 File Offset: 0x00115F91
		public OptimisticConcurrencyException(string message, Exception innerException, IEnumerable<ObjectStateEntry> stateEntries) : base(message, innerException, stateEntries)
		{
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x00117D9C File Offset: 0x00115F9C
		private OptimisticConcurrencyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
