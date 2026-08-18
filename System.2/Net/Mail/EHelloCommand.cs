using System;

namespace System.Net.Mail
{
	// Token: 0x02000281 RID: 641
	internal static class EHelloCommand
	{
		// Token: 0x06001815 RID: 6165 RVA: 0x0007ACFF File Offset: 0x00078EFF
		internal static IAsyncResult BeginSend(SmtpConnection conn, string domain, AsyncCallback callback, object state)
		{
			EHelloCommand.PrepareCommand(conn, domain);
			return ReadLinesCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0007AD10 File Offset: 0x00078F10
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

		// Token: 0x06001817 RID: 6167 RVA: 0x0007ADC0 File Offset: 0x00078FC0
		internal static string[] EndSend(IAsyncResult result)
		{
			return EHelloCommand.CheckResponse(ReadLinesCommand.EndSend(result));
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0007ADD0 File Offset: 0x00078FD0
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

		// Token: 0x06001819 RID: 6169 RVA: 0x0007AE21 File Offset: 0x00079021
		internal static string[] Send(SmtpConnection conn, string domain)
		{
			EHelloCommand.PrepareCommand(conn, domain);
			return EHelloCommand.CheckResponse(ReadLinesCommand.Send(conn));
		}
	}
}
