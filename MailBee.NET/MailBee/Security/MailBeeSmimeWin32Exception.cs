using System;
using System.Runtime.Serialization;
using a;
using a.j;

namespace MailBee.Security
{
	// Token: 0x02000115 RID: 277
	[Serializable]
	public class MailBeeSmimeWin32Exception : MailBeeSmimeException, IMailBeeWin32Exception
	{
		// Token: 0x06000917 RID: 2327 RVA: 0x0002A0A3 File Offset: 0x000290A3
		internal MailBeeSmimeWin32Exception(int A_0) : base(MailBeeSmimeWin32Exception.a(1103, A_0), 1103)
		{
			this.m_nativeErrorCode = A_0;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0002A0C2 File Offset: 0x000290C2
		protected MailBeeSmimeWin32Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0002A0CC File Offset: 0x000290CC
		private static string a(int A_0, int A_1)
		{
			return a5.a(A_0) + string.Format(Resources.Instance.ErrorDescSuffix_Win32ErrorCode0Desc1, A_1, global::a.j.z.a(A_1));
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0002A0F4 File Offset: 0x000290F4
		public int NativeErrorCode
		{
			get
			{
				return this.m_nativeErrorCode;
			}
		}

		// Token: 0x0400070C RID: 1804
		private int m_nativeErrorCode;
	}
}
