using System;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x020006C6 RID: 1734
	internal static class SmtpCommands
	{
		// Token: 0x040030DB RID: 12507
		internal static readonly byte[] Auth = Encoding.ASCII.GetBytes("AUTH ");

		// Token: 0x040030DC RID: 12508
		internal static readonly byte[] CRLF = Encoding.ASCII.GetBytes("\r\n");

		// Token: 0x040030DD RID: 12509
		internal static readonly byte[] Data = Encoding.ASCII.GetBytes("DATA\r\n");

		// Token: 0x040030DE RID: 12510
		internal static readonly byte[] DataStop = Encoding.ASCII.GetBytes("\r\n.\r\n");

		// Token: 0x040030DF RID: 12511
		internal static readonly byte[] EHello = Encoding.ASCII.GetBytes("EHLO ");

		// Token: 0x040030E0 RID: 12512
		internal static readonly byte[] Expand = Encoding.ASCII.GetBytes("EXPN ");

		// Token: 0x040030E1 RID: 12513
		internal static readonly byte[] Hello = Encoding.ASCII.GetBytes("HELO ");

		// Token: 0x040030E2 RID: 12514
		internal static readonly byte[] Help = Encoding.ASCII.GetBytes("HELP");

		// Token: 0x040030E3 RID: 12515
		internal static readonly byte[] Mail = Encoding.ASCII.GetBytes("MAIL FROM:");

		// Token: 0x040030E4 RID: 12516
		internal static readonly byte[] Noop = Encoding.ASCII.GetBytes("NOOP\r\n");

		// Token: 0x040030E5 RID: 12517
		internal static readonly byte[] Quit = Encoding.ASCII.GetBytes("QUIT\r\n");

		// Token: 0x040030E6 RID: 12518
		internal static readonly byte[] Recipient = Encoding.ASCII.GetBytes("RCPT TO:");

		// Token: 0x040030E7 RID: 12519
		internal static readonly byte[] Reset = Encoding.ASCII.GetBytes("RSET\r\n");

		// Token: 0x040030E8 RID: 12520
		internal static readonly byte[] Send = Encoding.ASCII.GetBytes("SEND FROM:");

		// Token: 0x040030E9 RID: 12521
		internal static readonly byte[] SendAndMail = Encoding.ASCII.GetBytes("SAML FROM:");

		// Token: 0x040030EA RID: 12522
		internal static readonly byte[] SendOrMail = Encoding.ASCII.GetBytes("SOML FROM:");

		// Token: 0x040030EB RID: 12523
		internal static readonly byte[] Turn = Encoding.ASCII.GetBytes("TURN\r\n");

		// Token: 0x040030EC RID: 12524
		internal static readonly byte[] Verify = Encoding.ASCII.GetBytes("VRFY ");

		// Token: 0x040030ED RID: 12525
		internal static readonly byte[] StartTls = Encoding.ASCII.GetBytes("STARTTLS");
	}
}
