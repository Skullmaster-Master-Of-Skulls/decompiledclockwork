using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.GZip
{
	// Token: 0x02000054 RID: 84
	[Serializable]
	public class GZipException : SharpZipBaseException
	{
		// Token: 0x060003AB RID: 939 RVA: 0x00015431 File Offset: 0x00014431
		protected GZipException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001543B File Offset: 0x0001443B
		public GZipException()
		{
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00015443 File Offset: 0x00014443
		public GZipException(string message) : base(message)
		{
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001544C File Offset: 0x0001444C
		public GZipException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
