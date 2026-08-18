using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	public class TarException : SharpZipBaseException
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000FF81 File Offset: 0x0000EF81
		protected TarException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000FF8B File Offset: 0x0000EF8B
		public TarException()
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000FF93 File Offset: 0x0000EF93
		public TarException(string message) : base(message)
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000FF9C File Offset: 0x0000EF9C
		public TarException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
