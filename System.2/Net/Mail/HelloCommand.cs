using System;

namespace System.Net.Mail
{
	// Token: 0x02000282 RID: 642
	internal static class HelloCommand
	{
		// Token: 0x0600181A RID: 6170 RVA: 0x0007AE35 File Offset: 0x00079035
		internal static IAsyncResult BeginSend(SmtpConnection conn, string domain, AsyncCallback callback, object state)
		{
			HelloCommand.PrepareCommand(conn, domain);
			return CheckCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0007AE46 File Offset: 0x00079046
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

		// Token: 0x0600181C RID: 6172 RVA: 0x0007AE74 File Offset: 0x00079074
		internal static void EndSend(IAsyncResult result)
		{
			string serverResponse;
			SmtpStatusCode statusCode = (SmtpStatusCode)CheckCommand.EndSend(result, out serverResponse);
			HelloCommand.CheckResponse(statusCode, serverResponse);
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0007AE98 File Offset: 0x00079098
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

		// Token: 0x0600181E RID: 6174 RVA: 0x0007AEEC File Offset: 0x000790EC
		internal static void Send(SmtpConnection conn, string domain)
		{
			HelloCommand.PrepareCommand(conn, domain);
			string serverResponse;
			SmtpStatusCode statusCode = CheckCommand.Send(conn, out serverResponse);
			HelloCommand.CheckResponse(statusCode, serverResponse);
		}
	}
}
