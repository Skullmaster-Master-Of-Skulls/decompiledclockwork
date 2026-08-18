using System;
using System.Runtime.Serialization;
using a;
using a.j;

namespace MailBee
{
	// Token: 0x0200006D RID: 109
	[Serializable]
	public class MailBeeLoginWin32Exception : MailBeeLocalException, IMailBeeLoginException, IMailBeeWin32Exception
	{
		// Token: 0x060003AD RID: 941 RVA: 0x0000905B File Offset: 0x0000805B
		internal MailBeeLoginWin32Exception(int A_0) : base(MailBeeLoginWin32Exception.a(117, A_0), 117)
		{
			this.m_nativeErrorCode = A_0;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00009074 File Offset: 0x00008074
		protected MailBeeLoginWin32Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000907E File Offset: 0x0000807E
		private static string a(int A_0, int A_1)
		{
			return a5.a(A_0) + string.Format(Resources.Instance.ErrorDescSuffix_Win32ErrorCode0Desc1, A_1, global::a.j.z.a(A_1));
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x000090A6 File Offset: 0x000080A6
		public int NativeErrorCode
		{
			get
			{
				return this.m_nativeErrorCode;
			}
		}

		// Token: 0x04000170 RID: 368
		private int m_nativeErrorCode;
	}
}
