using System;

namespace System.Net.Mail
{
	// Token: 0x020006BE RID: 1726
	internal static class AuthCommand
	{
		// Token: 0x06003565 RID: 13669 RVA: 0x000E3758 File Offset: 0x000E2758
		internal static IAsyncResult BeginSend(SmtpConnection conn, string type, string message, AsyncCallback callback, object state)
		{
			AuthCommand.PrepareCommand(conn, type, message);
			return ReadLinesCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x000E376B File Offset: 0x000E276B
		internal static IAsyncResult BeginSend(SmtpConnection conn, string message, AsyncCallback callback, object state)
		{
			AuthCommand.PrepareCommand(conn, message);
			return ReadLinesCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000E377C File Offset: 0x000E277C
		private static LineInfo CheckResponse(LineInfo[] lines)
		{
			if (lines == null || lines.Length == 0)
			{
				throw new SmtpException(SR.GetString("SmtpAuthResponseInvalid"));
			}
			return lines[0];
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x000E37A2 File Offset: 0x000E27A2
		internal static LineInfo EndSend(IAsyncResult result)
		{
			return AuthCommand.CheckResponse(ReadLinesCommand.EndSend(result));
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x000E37B0 File Offset: 0x000E27B0
		private static void PrepareCommand(SmtpConnection conn, string type, string message)
		{
			conn.BufferBuilder.Append(SmtpCommands.Auth);
			conn.BufferBuilder.Append(type);
			conn.BufferBuilder.Append(32);
			conn.BufferBuilder.Append(message);
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x000E3802 File Offset: 0x000E2802
		private static void PrepareCommand(SmtpConnection conn, string message)
		{
			conn.BufferBuilder.Append(message);
			conn.BufferBuilder.Append(SmtpCommands.CRLF);
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x000E3820 File Offset: 0x000E2820
		internal static LineInfo Send(SmtpConnection conn, string type, string message)
		{
			AuthCommand.PrepareCommand(conn, type, message);
			return AuthCommand.CheckResponse(ReadLinesCommand.Send(conn));
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x000E3835 File Offset: 0x000E2835
		internal static LineInfo Send(SmtpConnection conn, string message)
		{
			AuthCommand.PrepareCommand(conn, message);
			return AuthCommand.CheckResponse(ReadLinesCommand.Send(conn));
		}
	}
}
