using System;

namespace System.Net.Mail
{
	// Token: 0x0200027E RID: 638
	internal static class AuthCommand
	{
		// Token: 0x06001805 RID: 6149 RVA: 0x0007AAA8 File Offset: 0x00078CA8
		internal static IAsyncResult BeginSend(SmtpConnection conn, string type, string message, AsyncCallback callback, object state)
		{
			AuthCommand.PrepareCommand(conn, type, message);
			return ReadLinesCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x0007AABB File Offset: 0x00078CBB
		internal static IAsyncResult BeginSend(SmtpConnection conn, string message, AsyncCallback callback, object state)
		{
			AuthCommand.PrepareCommand(conn, message);
			return ReadLinesCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x0007AACC File Offset: 0x00078CCC
		private static LineInfo CheckResponse(LineInfo[] lines)
		{
			if (lines == null || lines.Length == 0)
			{
				throw new SmtpException(SR.GetString("SmtpAuthResponseInvalid"));
			}
			return lines[0];
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0007AAEC File Offset: 0x00078CEC
		internal static LineInfo EndSend(IAsyncResult result)
		{
			return AuthCommand.CheckResponse(ReadLinesCommand.EndSend(result));
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0007AAFC File Offset: 0x00078CFC
		private static void PrepareCommand(SmtpConnection conn, string type, string message)
		{
			conn.BufferBuilder.Append(SmtpCommands.Auth);
			conn.BufferBuilder.Append(type);
			conn.BufferBuilder.Append(32);
			conn.BufferBuilder.Append(message);
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0007AB4E File Offset: 0x00078D4E
		private static void PrepareCommand(SmtpConnection conn, string message)
		{
			conn.BufferBuilder.Append(message);
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0007AB6C File Offset: 0x00078D6C
		internal static LineInfo Send(SmtpConnection conn, string type, string message)
		{
			AuthCommand.PrepareCommand(conn, type, message);
			return AuthCommand.CheckResponse(ReadLinesCommand.Send(conn));
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0007AB81 File Offset: 0x00078D81
		internal static LineInfo Send(SmtpConnection conn, string message)
		{
			AuthCommand.PrepareCommand(conn, message);
			return AuthCommand.CheckResponse(ReadLinesCommand.Send(conn));
		}
	}
}
