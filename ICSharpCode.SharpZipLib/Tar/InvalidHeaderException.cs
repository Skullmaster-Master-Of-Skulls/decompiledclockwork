using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	public class InvalidHeaderException : TarException
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x0000FFA6 File Offset: 0x0000EFA6
		protected InvalidHeaderException(SerializationInfo information, StreamingContext context) : base(information, context)
		{
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000FFB0 File Offset: 0x0000EFB0
		public InvalidHeaderException()
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000FFB8 File Offset: 0x0000EFB8
		public InvalidHeaderException(string message) : base(message)
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000FFC1 File Offset: 0x0000EFC1
		public InvalidHeaderException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
