using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public sealed class ObjectNotFoundException : DataException
	{
		// Token: 0x06000065 RID: 101 RVA: 0x0000304C File Offset: 0x0000124C
		public ObjectNotFoundException()
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002BA3 File Offset: 0x00000DA3
		public ObjectNotFoundException(string message) : base(message)
		{
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002BAC File Offset: 0x00000DAC
		public ObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002BB6 File Offset: 0x00000DB6
		private ObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
