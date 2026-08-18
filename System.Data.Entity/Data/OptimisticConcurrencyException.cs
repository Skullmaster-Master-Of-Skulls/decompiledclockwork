using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public sealed class OptimisticConcurrencyException : UpdateException
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002F59 File Offset: 0x00001159
		public OptimisticConcurrencyException()
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002F61 File Offset: 0x00001161
		public OptimisticConcurrencyException(string message) : base(message)
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002F6A File Offset: 0x0000116A
		public OptimisticConcurrencyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002F74 File Offset: 0x00001174
		public OptimisticConcurrencyException(string message, Exception innerException, IEnumerable<ObjectStateEntry> stateEntries) : base(message, innerException, stateEntries)
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002F7F File Offset: 0x0000117F
		private OptimisticConcurrencyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
