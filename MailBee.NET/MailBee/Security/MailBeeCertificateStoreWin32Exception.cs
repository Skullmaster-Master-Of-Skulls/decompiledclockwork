using System;
using System.Runtime.Serialization;
using a;
using a.j;

namespace MailBee.Security
{
	// Token: 0x02000112 RID: 274
	[Serializable]
	public class MailBeeCertificateStoreWin32Exception : MailBeeCertificateStoreException, IMailBeeWin32Exception
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x00029FCC File Offset: 0x00028FCC
		internal MailBeeCertificateStoreWin32Exception(int A_0) : base(MailBeeCertificateStoreWin32Exception.a(1101, A_0), 1101)
		{
			this.m_nativeErrorCode = A_0;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00029FEB File Offset: 0x00028FEB
		protected MailBeeCertificateStoreWin32Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00029FF5 File Offset: 0x00028FF5
		private static string a(int A_0, int A_1)
		{
			return a5.a(A_0) + string.Format(Resources.Instance.ErrorDescSuffix_Win32ErrorCode0Desc1, A_1, global::a.j.z.a(A_1));
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0002A01D File Offset: 0x0002901D
		public int NativeErrorCode
		{
			get
			{
				return this.m_nativeErrorCode;
			}
		}

		// Token: 0x0400070A RID: 1802
		private int m_nativeErrorCode;
	}
}
