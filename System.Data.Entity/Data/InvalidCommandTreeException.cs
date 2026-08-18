using System;
using System.Data.Entity;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000016 RID: 22
	[Serializable]
	public sealed class InvalidCommandTreeException : DataException
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00003084 File Offset: 0x00001284
		public InvalidCommandTreeException() : base(Strings.Cqt_Exceptions_InvalidCommandTree)
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002BA3 File Offset: 0x00000DA3
		public InvalidCommandTreeException(string message) : base(message)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002BAC File Offset: 0x00000DAC
		public InvalidCommandTreeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002BB6 File Offset: 0x00000DB6
		private InvalidCommandTreeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
