using System;
using System.Runtime.Serialization;

namespace MailBee.Security
{
	// Token: 0x02000114 RID: 276
	[Serializable]
	public class MailBeeCertificateParsingException : MailBeeCertificateException
	{
		// Token: 0x06000914 RID: 2324 RVA: 0x0002A07E File Offset: 0x0002907E
		internal MailBeeCertificateParsingException() : base(1111)
		{
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0002A08B File Offset: 0x0002908B
		internal MailBeeCertificateParsingException(Exception A_0) : base(1111, A_0)
		{
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0002A099 File Offset: 0x00029099
		protected MailBeeCertificateParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
