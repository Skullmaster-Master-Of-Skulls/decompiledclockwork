using System;

namespace System.Net.Mail
{
	// Token: 0x02000297 RID: 663
	internal class SmtpPooledStream : PooledStream
	{
		// Token: 0x060018A7 RID: 6311 RVA: 0x0007D367 File Offset: 0x0007B567
		internal SmtpPooledStream(ConnectionPool connectionPool, TimeSpan lifetime, bool checkLifetime) : base(connectionPool, lifetime, checkLifetime)
		{
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x0007D374 File Offset: 0x0007B574
		protected override void Dispose(bool disposing)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.Web, "SmtpPooledStream::Dispose #" + ValidationHelper.HashString(this));
			}
			if (disposing && base.NetworkStream.Connected)
			{
				this.Write(SmtpCommands.Quit, 0, SmtpCommands.Quit.Length);
				this.Flush();
				byte[] buffer = new byte[80];
				int num = this.Read(buffer, 0, 80);
			}
			base.Dispose(disposing);
			if (Logging.On)
			{
				Logging.Exit(Logging.Web, "SmtpPooledStream::Dispose #" + ValidationHelper.HashString(this));
			}
		}

		// Token: 0x04001892 RID: 6290
		internal bool previouslyUsed;

		// Token: 0x04001893 RID: 6291
		internal bool dsnEnabled;

		// Token: 0x04001894 RID: 6292
		internal bool serverSupportsEai;

		// Token: 0x04001895 RID: 6293
		internal ICredentialsByHost creds;

		// Token: 0x04001896 RID: 6294
		private const int safeBufferLength = 80;
	}
}
