using System;
using System.Runtime.Serialization;
using a;
using a.j;

namespace MailBee.Security
{
	// Token: 0x0200010F RID: 271
	[Serializable]
	public class MailBeeSslWin32Exception : MailBeeSslException, IMailBeeWin32Exception, IMailBeeSocketMustCloseException
	{
		// Token: 0x06000902 RID: 2306 RVA: 0x00029F06 File Offset: 0x00028F06
		internal MailBeeSslWin32Exception(int A_0) : base(MailBeeSslWin32Exception.a(140, A_0), 140)
		{
			this.m_nativeErrorCode = A_0;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00029F25 File Offset: 0x00028F25
		protected MailBeeSslWin32Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00029F2F File Offset: 0x00028F2F
		private static string a(int A_0, int A_1)
		{
			return a5.a(A_0) + string.Format(Resources.Instance.ErrorDescSuffix_Win32ErrorCode0Desc1, A_1, global::a.j.z.a(A_1));
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00029F57 File Offset: 0x00028F57
		public int NativeErrorCode
		{
			get
			{
				return this.m_nativeErrorCode;
			}
		}

		// Token: 0x04000708 RID: 1800
		private int m_nativeErrorCode;
	}
}
