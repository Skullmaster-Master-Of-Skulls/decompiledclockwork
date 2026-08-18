using System;

namespace System.Net.Mail
{
	// Token: 0x02000285 RID: 645
	internal static class RecipientCommand
	{
		// Token: 0x06001829 RID: 6185 RVA: 0x0007B0E2 File Offset: 0x000792E2
		internal static IAsyncResult BeginSend(SmtpConnection conn, string to, AsyncCallback callback, object state)
		{
			RecipientCommand.PrepareCommand(conn, to);
			return CheckCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0007B0F4 File Offset: 0x000792F4
		private static bool CheckResponse(SmtpStatusCode statusCode, string response)
		{
			if (statusCode <= SmtpStatusCode.MailboxBusy)
			{
				if (statusCode - SmtpStatusCode.Ok <= 1)
				{
					return true;
				}
				if (statusCode != SmtpStatusCode.MailboxBusy)
				{
					goto IL_34;
				}
			}
			else if (statusCode != SmtpStatusCode.InsufficientStorage && statusCode - SmtpStatusCode.MailboxUnavailable > 3)
			{
				goto IL_34;
			}
			return false;
			IL_34:
			if (statusCode < (SmtpStatusCode)400)
			{
				throw new SmtpException(SR.GetString("net_webstatus_ServerProtocolViolation"), response);
			}
			throw new SmtpException(statusCode, response, true);
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0007B158 File Offset: 0x00079358
		internal static bool EndSend(IAsyncResult result, out string response)
		{
			SmtpStatusCode statusCode = (SmtpStatusCode)CheckCommand.EndSend(result, out response);
			return RecipientCommand.CheckResponse(statusCode, response);
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0007B17C File Offset: 0x0007937C
		private static void PrepareCommand(SmtpConnection conn, string to)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(SmtpCommands.Recipient);
			conn.BufferBuilder.Append(to, true);
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0007B1D0 File Offset: 0x000793D0
		internal static bool Send(SmtpConnection conn, string to, out string response)
		{
			RecipientCommand.PrepareCommand(conn, to);
			SmtpStatusCode statusCode = CheckCommand.Send(conn, out response);
			return RecipientCommand.CheckResponse(statusCode, response);
		}
	}
}
