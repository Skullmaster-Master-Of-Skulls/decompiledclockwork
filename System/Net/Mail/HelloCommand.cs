using System;

namespace System.Net.Mail
{
	// Token: 0x020006C2 RID: 1730
	internal static class HelloCommand
	{
		// Token: 0x0600357A RID: 13690 RVA: 0x000E3B05 File Offset: 0x000E2B05
		internal static IAsyncResult BeginSend(SmtpConnection conn, string domain, AsyncCallback callback, object state)
		{
			HelloCommand.PrepareCommand(conn, domain);
			return CheckCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x000E3B18 File Offset: 0x000E2B18
		private static void CheckResponse(SmtpStatusCode statusCode, string serverResponse)
		{
			if (statusCode == SmtpStatusCode.Ok)
			{
				return;
			}
			if (statusCode < (SmtpStatusCode)400)
			{
				throw new SmtpException(SR.GetString("net_webstatus_ServerProtocolViolation"), serverResponse);
			}
			throw new SmtpException(statusCode, serverResponse, true);
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x000E3B54 File Offset: 0x000E2B54
		internal static void EndSend(IAsyncResult result)
		{
			string serverResponse;
			SmtpStatusCode statusCode = (SmtpStatusCode)CheckCommand.EndSend(result, out serverResponse);
			HelloCommand.CheckResponse(statusCode, serverResponse);
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x000E3B78 File Offset: 0x000E2B78
		private static void PrepareCommand(SmtpConnection conn, string domain)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(SmtpCommands.Hello);
			conn.BufferBuilder.Append(domain);
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x000E3BCC File Offset: 0x000E2BCC
		internal static void Send(SmtpConnection conn, string domain)
		{
			HelloCommand.PrepareCommand(conn, domain);
			string serverResponse;
			SmtpStatusCode statusCode = CheckCommand.Send(conn, out serverResponse);
			HelloCommand.CheckResponse(statusCode, serverResponse);
		}
	}
}
