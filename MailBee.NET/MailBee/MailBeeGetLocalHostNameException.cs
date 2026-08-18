using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x02000046 RID: 70
	[Serializable]
	public class MailBeeGetLocalHostNameException : MailBeeGetHostNameException
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x0000808C File Offset: 0x0000708C
		internal MailBeeGetLocalHostNameException(int A_0, Exception A_1) : base(MailBeeGetLocalHostNameException.a(A_0, A_1), A_0, A_1)
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000809D File Offset: 0x0000709D
		protected MailBeeGetLocalHostNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000080A7 File Offset: 0x000070A7
		private static string a(int A_0, Exception A_1)
		{
			return a5.a(A_0) + string.Format(Resources.Instance.ErrorDescSuffix_InnerException0, A_1.Message);
		}
	}
}
