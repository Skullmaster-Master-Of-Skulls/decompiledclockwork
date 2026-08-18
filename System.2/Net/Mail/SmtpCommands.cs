using System;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000286 RID: 646
	internal static class SmtpCommands
	{
		// Token: 0x04001829 RID: 6185
		internal static readonly byte[] Auth = Encoding.ASCII.GetBytes("AUTH ");

		// Token: 0x0400182A RID: 6186
		internal static readonly byte[] CRLF = Encoding.ASCII.GetBytes("\r\n");

		// Token: 0x0400182B RID: 6187
		internal static readonly byte[] Data = Encoding.ASCII.GetBytes("DATA\r\n");

		// Token: 0x0400182C RID: 6188
		internal static readonly byte[] DataStop = Encoding.ASCII.GetBytes("\r\n.\r\n");

		// Token: 0x0400182D RID: 6189
		internal static readonly byte[] EHello = Encoding.ASCII.GetBytes("EHLO ");

		// Token: 0x0400182E RID: 6190
		internal static readonly byte[] Expand = Encoding.ASCII.GetBytes("EXPN ");

		// Token: 0x0400182F RID: 6191
		internal static readonly byte[] Hello = Encoding.ASCII.GetBytes("HELO ");

		// Token: 0x04001830 RID: 6192
		internal static readonly byte[] Help = Encoding.ASCII.GetBytes("HELP");

		// Token: 0x04001831 RID: 6193
		internal static readonly byte[] Mail = Encoding.ASCII.GetBytes("MAIL FROM:");

		// Token: 0x04001832 RID: 6194
		internal static readonly byte[] Noop = Encoding.ASCII.GetBytes("NOOP\r\n");

		// Token: 0x04001833 RID: 6195
		internal static readonly byte[] Quit = Encoding.ASCII.GetBytes("QUIT\r\n");

		// Token: 0x04001834 RID: 6196
		internal static readonly byte[] Recipient = Encoding.ASCII.GetBytes("RCPT TO:");

		// Token: 0x04001835 RID: 6197
		internal static readonly byte[] Reset = Encoding.ASCII.GetBytes("RSET\r\n");

		// Token: 0x04001836 RID: 6198
		internal static readonly byte[] Send = Encoding.ASCII.GetBytes("SEND FROM:");

		// Token: 0x04001837 RID: 6199
		internal static readonly byte[] SendAndMail = Encoding.ASCII.GetBytes("SAML FROM:");

		// Token: 0x04001838 RID: 6200
		internal static readonly byte[] SendOrMail = Encoding.ASCII.GetBytes("SOML FROM:");

		// Token: 0x04001839 RID: 6201
		internal static readonly byte[] Turn = Encoding.ASCII.GetBytes("TURN\r\n");

		// Token: 0x0400183A RID: 6202
		internal static readonly byte[] Verify = Encoding.ASCII.GetBytes("VRFY ");

		// Token: 0x0400183B RID: 6203
		internal static readonly byte[] StartTls = Encoding.ASCII.GetBytes("STARTTLS");
	}
}
