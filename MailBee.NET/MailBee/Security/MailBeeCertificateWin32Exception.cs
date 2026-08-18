using System;
using System.Runtime.Serialization;
using a;
using a.j;

namespace MailBee.Security
{
	// Token: 0x02000113 RID: 275
	[Serializable]
	public class MailBeeCertificateWin32Exception : MailBeeCertificateException, IMailBeeWin32Exception
	{
		// Token: 0x06000910 RID: 2320 RVA: 0x0002A025 File Offset: 0x00029025
		internal MailBeeCertificateWin32Exception(int A_0) : base(MailBeeCertificateWin32Exception.a(1102, A_0), 1102)
		{
			this.m_nativeErrorCode = A_0;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0002A044 File Offset: 0x00029044
		protected MailBeeCertificateWin32Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0002A04E File Offset: 0x0002904E
		private static string a(int A_0, int A_1)
		{
			return a5.a(A_0) + string.Format(Resources.Instance.ErrorDescSuffix_Win32ErrorCode0Desc1, A_1, global::a.j.z.a(A_1));
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0002A076 File Offset: 0x00029076
		public int NativeErrorCode
		{
			get
			{
				return this.m_nativeErrorCode;
			}
		}

		// Token: 0x0400070B RID: 1803
		private int m_nativeErrorCode;
	}
}
