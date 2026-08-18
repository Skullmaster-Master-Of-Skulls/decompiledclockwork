using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.BZip2
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	public class BZip2Exception : SharpZipBaseException
	{
		// Token: 0x060003AF RID: 943 RVA: 0x00015456 File Offset: 0x00014456
		protected BZip2Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00015460 File Offset: 0x00014460
		public BZip2Exception()
		{
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00015468 File Offset: 0x00014468
		public BZip2Exception(string message) : base(message)
		{
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00015471 File Offset: 0x00014471
		public BZip2Exception(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
