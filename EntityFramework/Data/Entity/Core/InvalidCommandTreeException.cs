using System;
using System.Data.Entity.Resources;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020003A4 RID: 932
	[Serializable]
	public sealed class InvalidCommandTreeException : DataException
	{
		// Token: 0x060021B0 RID: 8624 RVA: 0x0009DDFF File Offset: 0x0009BFFF
		public InvalidCommandTreeException() : base(Strings.Cqt_Exceptions_InvalidCommandTree)
		{
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x0009DE0C File Offset: 0x0009C00C
		public InvalidCommandTreeException(string message) : base(message)
		{
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x0009DE15 File Offset: 0x0009C015
		public InvalidCommandTreeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x0009DE1F File Offset: 0x0009C01F
		private InvalidCommandTreeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
