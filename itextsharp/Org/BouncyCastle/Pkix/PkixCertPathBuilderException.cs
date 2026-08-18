using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x02000108 RID: 264
	public class PkixCertPathBuilderException : GeneralSecurityException
	{
		// Token: 0x06000A51 RID: 2641 RVA: 0x00036BBF File Offset: 0x00035BBF
		public PkixCertPathBuilderException()
		{
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00036BC7 File Offset: 0x00035BC7
		public PkixCertPathBuilderException(string message) : base(message)
		{
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00036BD0 File Offset: 0x00035BD0
		public PkixCertPathBuilderException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
