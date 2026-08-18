using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	public class SharpZipBaseException : ApplicationException
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00009EEC File Offset: 0x00008EEC
		protected SharpZipBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009EF6 File Offset: 0x00008EF6
		public SharpZipBaseException()
		{
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00009EFE File Offset: 0x00008EFE
		public SharpZipBaseException(string message) : base(message)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00009F07 File Offset: 0x00008F07
		public SharpZipBaseException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
