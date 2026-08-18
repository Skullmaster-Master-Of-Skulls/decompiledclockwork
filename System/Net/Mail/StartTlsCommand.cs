using System;

namespace System.Net.Mail
{
	// Token: 0x020006C3 RID: 1731
	internal static class StartTlsCommand
	{
		// Token: 0x0600357F RID: 13695 RVA: 0x000E3BF0 File Offset: 0x000E2BF0
		internal static IAsyncResult BeginSend(SmtpConnection conn, AsyncCallback callback, object state)
		{
			StartTlsCommand.PrepareCommand(conn);
			return CheckCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x000E3C00 File Offset: 0x000E2C00
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

		// Token: 0x06003581 RID: 13697 RVA: 0x000E3C44 File Offset: 0x000E2C44
		internal static void EndSend(IAsyncResult result)
		{
			string response;
			SmtpStatusCode statusCode = (SmtpStatusCode)CheckCommand.EndSend(result, out response);
			StartTlsCommand.CheckResponse(statusCode, response);
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000E3C66 File Offset: 0x000E2C66
		private static void PrepareCommand(SmtpConnection conn)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(SmtpCommands.StartTls);
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x000E3CA0 File Offset: 0x000E2CA0
		internal static void Send(SmtpConnection conn)
		{
			StartTlsCommand.PrepareCommand(conn);
			string response;
			SmtpStatusCode statusCode = CheckCommand.Send(conn, out response);
			StartTlsCommand.CheckResponse(statusCode, response);
		}
	}
}
