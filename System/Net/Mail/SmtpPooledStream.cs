using System;

namespace System.Net.Mail
{
	// Token: 0x020006DB RID: 1755
	internal class SmtpPooledStream : PooledStream
	{
		// Token: 0x06003620 RID: 13856 RVA: 0x000E7188 File Offset: 0x000E6188
		internal SmtpPooledStream(ConnectionPool connectionPool, TimeSpan lifetime, bool checkLifetime) : base(connectionPool, lifetime, checkLifetime)
		{
		}

		// Token: 0x04003160 RID: 12640
		internal bool previouslyUsed;

		// Token: 0x04003161 RID: 12641
		internal bool dsnEnabled;

		// Token: 0x04003162 RID: 12642
		internal ICredentialsByHost creds;
	}
}
