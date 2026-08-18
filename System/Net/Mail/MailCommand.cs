using System;

namespace System.Net.Mail
{
	// Token: 0x020006C4 RID: 1732
	internal static class MailCommand
	{
		// Token: 0x06003584 RID: 13700 RVA: 0x000E3CC3 File Offset: 0x000E2CC3
		internal static IAsyncResult BeginSend(SmtpConnection conn, byte[] command, string from, AsyncCallback callback, object state)
		{
			MailCommand.PrepareCommand(conn, command, from);
			return CheckCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x000E3CD8 File Offset: 0x000E2CD8
		private static void CheckResponse(SmtpStatusCode statusCode, string response)
		{
			if (statusCode == SmtpStatusCode.Ok)
			{
				return;
			}
			switch (statusCode)
			{
			case SmtpStatusCode.LocalErrorInProcessing:
			case SmtpStatusCode.InsufficientStorage:
				break;
			default:
				if (statusCode != SmtpStatusCode.ExceededStorageAllocation)
				{
				}
				break;
			}
			if (statusCode < (SmtpStatusCode)400)
			{
				throw new SmtpException(SR.GetString("net_webstatus_ServerProtocolViolation"), response);
			}
			throw new SmtpException(statusCode, response, true);
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x000E3D30 File Offset: 0x000E2D30
		internal static void EndSend(IAsyncResult result)
		{
			string response;
			SmtpStatusCode statusCode = (SmtpStatusCode)CheckCommand.EndSend(result, out response);
			MailCommand.CheckResponse(statusCode, response);
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x000E3D54 File Offset: 0x000E2D54
		private static void PrepareCommand(SmtpConnection conn, byte[] command, string from)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(command);
			conn.BufferBuilder.Append(from);
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x000E3DA4 File Offset: 0x000E2DA4
		internal static void Send(SmtpConnection conn, byte[] command, string from)
		{
			MailCommand.PrepareCommand(conn, command, from);
			string response;
			SmtpStatusCode statusCode = CheckCommand.Send(conn, out response);
			MailCommand.CheckResponse(statusCode, response);
		}
	}
}
