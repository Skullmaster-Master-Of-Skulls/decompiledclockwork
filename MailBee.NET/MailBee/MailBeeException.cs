using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x02000020 RID: 32
	public abstract class MailBeeException : ApplicationException
	{
		// Token: 0x06000103 RID: 259 RVA: 0x0000771C File Offset: 0x0000671C
		internal MailBeeException(string A_0, int A_1) : base(A_0)
		{
			this.a = A_1;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000772C File Offset: 0x0000672C
		internal MailBeeException(int A_0) : this(a5.a(A_0), A_0)
		{
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000773B File Offset: 0x0000673B
		internal MailBeeException(string A_0, int A_1, Exception A_2) : base(A_0, A_2)
		{
			this.a = A_1;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000774C File Offset: 0x0000674C
		internal MailBeeException(int A_0, Exception A_1) : this(MailBeeException.a(A_0, A_1), A_0, A_1)
		{
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000775D File Offset: 0x0000675D
		protected MailBeeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00007768 File Offset: 0x00006768
		private static string a(int A_0, Exception A_1)
		{
			return a5.a(A_0) + string.Format(Resources.Instance.ErrorDescSuffix_InnerException0, A_1.Message) + ((A_1.InnerException == null) ? string.Empty : string.Format(Resources.Instance.ErrorDescSuffix_InnerException0, A_1.InnerException.Message));
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000077BE File Offset: 0x000067BE
		public int ErrorCode
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x04000132 RID: 306
		private int a;
	}
}
