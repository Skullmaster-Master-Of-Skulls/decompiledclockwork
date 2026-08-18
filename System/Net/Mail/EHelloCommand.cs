using System;

namespace System.Net.Mail
{
	// Token: 0x020006C1 RID: 1729
	internal static class EHelloCommand
	{
		// Token: 0x06003575 RID: 13685 RVA: 0x000E39CF File Offset: 0x000E29CF
		internal static IAsyncResult BeginSend(SmtpConnection conn, string domain, AsyncCallback callback, object state)
		{
			EHelloCommand.PrepareCommand(conn, domain);
			return ReadLinesCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x000E39E0 File Offset: 0x000E29E0
		private static string[] CheckResponse(LineInfo[] lines)
		{
			if (lines == null || lines.Length == 0)
			{
				throw new SmtpException(SR.GetString("SmtpEhloResponseInvalid"));
			}
			if (lines[0].StatusCode == SmtpStatusCode.Ok)
			{
				string[] array = new string[lines.Length - 1];
				for (int i = 1; i < lines.Length; i++)
				{
					array[i - 1] = lines[i].Line;
				}
				return array;
			}
			if (lines[0].StatusCode < (SmtpStatusCode)400)
			{
				throw new SmtpException(SR.GetString("net_webstatus_ServerProtocolViolation"), lines[0].Line);
			}
			throw new SmtpException(lines[0].StatusCode, lines[0].Line, true);
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x000E3A91 File Offset: 0x000E2A91
		internal static string[] EndSend(IAsyncResult result)
		{
			return EHelloCommand.CheckResponse(ReadLinesCommand.EndSend(result));
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x000E3AA0 File Offset: 0x000E2AA0
		private static void PrepareCommand(SmtpConnection conn, string domain)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(SmtpCommands.EHello);
			conn.BufferBuilder.Append(domain);
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x000E3AF1 File Offset: 0x000E2AF1
		internal static string[] Send(SmtpConnection conn, string domain)
		{
			EHelloCommand.PrepareCommand(conn, domain);
			return EHelloCommand.CheckResponse(ReadLinesCommand.Send(conn));
		}
	}
}
