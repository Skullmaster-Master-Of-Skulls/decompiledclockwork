using System;

namespace System.Net.Mail
{
	// Token: 0x020006BF RID: 1727
	internal static class DataCommand
	{
		// Token: 0x0600356D RID: 13677 RVA: 0x000E3849 File Offset: 0x000E2849
		internal static IAsyncResult BeginSend(SmtpConnection conn, AsyncCallback callback, object state)
		{
			DataCommand.PrepareCommand(conn);
			return CheckCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000E385C File Offset: 0x000E285C
		private static void CheckResponse(SmtpStatusCode statusCode, string serverResponse)
		{
			if (statusCode == SmtpStatusCode.StartMailInput)
			{
				return;
			}
			if (statusCode != SmtpStatusCode.LocalErrorInProcessing && statusCode != SmtpStatusCode.TransactionFailed)
			{
			}
			if (statusCode < (SmtpStatusCode)400)
			{
				throw new SmtpException(SR.GetString("net_webstatus_ServerProtocolViolation"), serverResponse);
			}
			throw new SmtpException(statusCode, serverResponse, true);
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x000E38A8 File Offset: 0x000E28A8
		internal static void EndSend(IAsyncResult result)
		{
			string serverResponse;
			SmtpStatusCode statusCode = (SmtpStatusCode)CheckCommand.EndSend(result, out serverResponse);
			DataCommand.CheckResponse(statusCode, serverResponse);
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x000E38CA File Offset: 0x000E28CA
		private static void PrepareCommand(SmtpConnection conn)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(SmtpCommands.Data);
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x000E38F4 File Offset: 0x000E28F4
		internal static void Send(SmtpConnection conn)
		{
			DataCommand.PrepareCommand(conn);
			string serverResponse;
			SmtpStatusCode statusCode = CheckCommand.Send(conn, out serverResponse);
			DataCommand.CheckResponse(statusCode, serverResponse);
		}
	}
}
