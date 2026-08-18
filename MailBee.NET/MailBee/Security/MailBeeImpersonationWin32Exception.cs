using System;
using System.Runtime.Serialization;
using a;
using a.j;

namespace MailBee.Security
{
	// Token: 0x02000117 RID: 279
	[Serializable]
	public class MailBeeImpersonationWin32Exception : MailBeeImpersonationException, IMailBeeWin32Exception
	{
		// Token: 0x0600091E RID: 2334 RVA: 0x0002A122 File Offset: 0x00029122
		internal MailBeeImpersonationWin32Exception(int A_0) : base(MailBeeImpersonationWin32Exception.a(1121, A_0), 1101)
		{
			this.m_nativeErrorCode = A_0;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0002A141 File Offset: 0x00029141
		protected MailBeeImpersonationWin32Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0002A14B File Offset: 0x0002914B
		private static string a(int A_0, int A_1)
		{
			return a5.a(A_0) + string.Format(Resources.Instance.ErrorDescSuffix_Win32ErrorCode0Desc1, A_1, global::a.j.z.a(A_1));
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0002A173 File Offset: 0x00029173
		public int NativeErrorCode
		{
			get
			{
				return this.m_nativeErrorCode;
			}
		}

		// Token: 0x0400070E RID: 1806
		private int m_nativeErrorCode;
	}
}
