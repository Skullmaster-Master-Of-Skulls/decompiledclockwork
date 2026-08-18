using System;
using System.Runtime.Serialization;
using a;
using a.j;

namespace MailBee.Security
{
	// Token: 0x02000111 RID: 273
	[Serializable]
	public class MailBeeCryptoProviderWin32Exception : MailBeeCryptoProviderException, IMailBeeWin32Exception
	{
		// Token: 0x06000908 RID: 2312 RVA: 0x00029F73 File Offset: 0x00028F73
		internal MailBeeCryptoProviderWin32Exception(int A_0) : base(MailBeeCryptoProviderWin32Exception.a(1100, A_0), 1100)
		{
			this.m_nativeErrorCode = A_0;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00029F92 File Offset: 0x00028F92
		protected MailBeeCryptoProviderWin32Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00029F9C File Offset: 0x00028F9C
		private static string a(int A_0, int A_1)
		{
			return a5.a(A_0) + string.Format(Resources.Instance.ErrorDescSuffix_Win32ErrorCode0Desc1, A_1, global::a.j.z.a(A_1));
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00029FC4 File Offset: 0x00028FC4
		public int NativeErrorCode
		{
			get
			{
				return this.m_nativeErrorCode;
			}
		}

		// Token: 0x04000709 RID: 1801
		private int m_nativeErrorCode;
	}
}
