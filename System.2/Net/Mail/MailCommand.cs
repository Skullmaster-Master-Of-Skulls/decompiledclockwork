using System;

namespace System.Net.Mail
{
	// Token: 0x02000284 RID: 644
	internal static class MailCommand
	{
		// Token: 0x06001824 RID: 6180 RVA: 0x0007AFD7 File Offset: 0x000791D7
		internal static IAsyncResult BeginSend(SmtpConnection conn, byte[] command, MailAddress from, bool allowUnicode, AsyncCallback callback, object state)
		{
			MailCommand.PrepareCommand(conn, command, from, allowUnicode);
			return CheckCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0007AFEC File Offset: 0x000791EC
		private static void CheckResponse(SmtpStatusCode statusCode, string response)
		{
			if (statusCode == SmtpStatusCode.Ok)
			{
				return;
			}
			if (statusCode - SmtpStatusCode.LocalErrorInProcessing > 1 && statusCode != SmtpStatusCode.ExceededStorageAllocation)
			{
			}
			if (statusCode < (SmtpStatusCode)400)
			{
				throw new SmtpException(SR.GetString("net_webstatus_ServerProtocolViolation"), response);
			}
			throw new SmtpException(statusCode, response, true);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x0007B02C File Offset: 0x0007922C
		internal static void EndSend(IAsyncResult result)
		{
			string response;
			SmtpStatusCode statusCode = (SmtpStatusCode)CheckCommand.EndSend(result, out response);
			MailCommand.CheckResponse(statusCode, response);
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0007B050 File Offset: 0x00079250
		private static void PrepareCommand(SmtpConnection conn, byte[] command, MailAddress from, bool allowUnicode)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(command);
			string smtpAddress = from.GetSmtpAddress(allowUnicode);
			conn.BufferBuilder.Append(smtpAddress, allowUnicode);
			if (allowUnicode)
			{
				conn.BufferBuilder.Append(" BODY=8BITMIME SMTPUTF8");
			}
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0007B0BC File Offset: 0x000792BC
		internal static void Send(SmtpConnection conn, byte[] command, MailAddress from, bool allowUnicode)
		{
			MailCommand.PrepareCommand(conn, command, from, allowUnicode);
			string response;
			SmtpStatusCode statusCode = CheckCommand.Send(conn, out response);
			MailCommand.CheckResponse(statusCode, response);
		}
	}
}
