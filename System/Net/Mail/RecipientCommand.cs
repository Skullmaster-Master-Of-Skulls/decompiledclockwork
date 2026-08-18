using System;

namespace System.Net.Mail
{
	// Token: 0x020006C5 RID: 1733
	internal static class RecipientCommand
	{
		// Token: 0x06003589 RID: 13705 RVA: 0x000E3DC9 File Offset: 0x000E2DC9
		internal static IAsyncResult BeginSend(SmtpConnection conn, string to, AsyncCallback callback, object state)
		{
			RecipientCommand.PrepareCommand(conn, to);
			return CheckCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x000E3DDC File Offset: 0x000E2DDC
		private static bool CheckResponse(SmtpStatusCode statusCode, string response)
		{
			switch (statusCode)
			{
			case SmtpStatusCode.Ok:
			case SmtpStatusCode.UserNotLocalWillForward:
				return true;
			default:
				switch (statusCode)
				{
				case SmtpStatusCode.MailboxBusy:
				case SmtpStatusCode.InsufficientStorage:
					break;
				case SmtpStatusCode.LocalErrorInProcessing:
					goto IL_50;
				default:
					switch (statusCode)
					{
					case SmtpStatusCode.MailboxUnavailable:
					case SmtpStatusCode.UserNotLocalTryAlternatePath:
					case SmtpStatusCode.ExceededStorageAllocation:
					case SmtpStatusCode.MailboxNameNotAllowed:
						break;
					default:
						goto IL_50;
					}
					break;
				}
				return false;
				IL_50:
				if (statusCode < (SmtpStatusCode)400)
				{
					throw new SmtpException(SR.GetString("net_webstatus_ServerProtocolViolation"), response);
				}
				throw new SmtpException(statusCode, response, true);
			}
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x000E3E5C File Offset: 0x000E2E5C
		internal static bool EndSend(IAsyncResult result, out string response)
		{
			SmtpStatusCode statusCode = (SmtpStatusCode)CheckCommand.EndSend(result, out response);
			return RecipientCommand.CheckResponse(statusCode, response);
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x000E3E80 File Offset: 0x000E2E80
		private static void PrepareCommand(SmtpConnection conn, string to)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(SmtpCommands.Recipient);
			conn.BufferBuilder.Append(to);
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x000E3ED4 File Offset: 0x000E2ED4
		internal static bool Send(SmtpConnection conn, string to, out string response)
		{
			RecipientCommand.PrepareCommand(conn, to);
			SmtpStatusCode statusCode = CheckCommand.Send(conn, out response);
			return RecipientCommand.CheckResponse(statusCode, response);
		}
	}
}
