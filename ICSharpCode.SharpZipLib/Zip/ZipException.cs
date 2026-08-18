using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	public class ZipException : SharpZipBaseException
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00009F11 File Offset: 0x00008F11
		protected ZipException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00009F1B File Offset: 0x00008F1B
		public ZipException()
		{
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00009F23 File Offset: 0x00008F23
		public ZipException(string message) : base(message)
		{
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009F2C File Offset: 0x00008F2C
		public ZipException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
