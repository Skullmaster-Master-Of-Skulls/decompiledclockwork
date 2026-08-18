using System;
using System.Runtime.Serialization;

namespace MailBee.Security
{
	// Token: 0x02000118 RID: 280
	[Serializable]
	public class MailBeeDomainKeysException : MailBeeLocalException
	{
		// Token: 0x06000922 RID: 2338 RVA: 0x0002A17B File Offset: 0x0002917B
		internal MailBeeDomainKeysException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0002A184 File Offset: 0x00029184
		internal MailBeeDomainKeysException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0002A18E File Offset: 0x0002918E
		protected MailBeeDomainKeysException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
