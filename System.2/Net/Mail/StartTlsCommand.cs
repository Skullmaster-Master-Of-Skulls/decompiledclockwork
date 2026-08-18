using System;

namespace System.Net.Mail
{
	// Token: 0x02000283 RID: 643
	internal static class StartTlsCommand
	{
		// Token: 0x0600181F RID: 6175 RVA: 0x0007AF10 File Offset: 0x00079110
		internal static IAsyncResult BeginSend(SmtpConnection conn, AsyncCallback callback, object state)
		{
			StartTlsCommand.PrepareCommand(conn);
			return CheckCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0007AF20 File Offset: 0x00079120
		private static void CheckResponse(SmtpStatusCode statusCode, string response)
		{
			if (statusCode == SmtpStatusCode.ServiceReady)
			{
				return;
			}
			if (statusCode != SmtpStatusCode.ClientNotPermitted)
			{
			}
			if (statusCode < (SmtpStatusCode)400)
			{
				throw new SmtpException(SR.GetString("net_webstatus_ServerProtocolViolation"), response);
			}
			throw new SmtpException(statusCode, response, true);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0007AF58 File Offset: 0x00079158
		internal static void EndSend(IAsyncResult result)
		{
			string response;
			SmtpStatusCode statusCode = (SmtpStatusCode)CheckCommand.EndSend(result, out response);
			StartTlsCommand.CheckResponse(statusCode, response);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0007AF7A File Offset: 0x0007917A
		private static void PrepareCommand(SmtpConnection conn)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(SmtpCommands.StartTls);
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0007AFB4 File Offset: 0x000791B4
		internal static void Send(SmtpConnection conn)
		{
			StartTlsCommand.PrepareCommand(conn);
			string response;
			SmtpStatusCode statusCode = CheckCommand.Send(conn, out response);
			StartTlsCommand.CheckResponse(statusCode, response);
		}
	}
}
