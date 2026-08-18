using System;

namespace System.Net.Mail
{
	// Token: 0x0200027F RID: 639
	internal static class DataCommand
	{
		// Token: 0x0600180D RID: 6157 RVA: 0x0007AB95 File Offset: 0x00078D95
		internal static IAsyncResult BeginSend(SmtpConnection conn, AsyncCallback callback, object state)
		{
			DataCommand.PrepareCommand(conn);
			return CheckCommand.BeginSend(conn, callback, state);
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x0007ABA5 File Offset: 0x00078DA5
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

		// Token: 0x0600180F RID: 6159 RVA: 0x0007ABE4 File Offset: 0x00078DE4
		internal static void EndSend(IAsyncResult result)
		{
			string serverResponse;
			SmtpStatusCode statusCode = (SmtpStatusCode)CheckCommand.EndSend(result, out serverResponse);
			DataCommand.CheckResponse(statusCode, serverResponse);
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0007AC06 File Offset: 0x00078E06
		private static void PrepareCommand(SmtpConnection conn)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(SmtpCommands.Data);
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0007AC30 File Offset: 0x00078E30
		internal static void Send(SmtpConnection conn)
		{
			DataCommand.PrepareCommand(conn);
			string serverResponse;
			SmtpStatusCode statusCode = CheckCommand.Send(conn, out serverResponse);
			DataCommand.CheckResponse(statusCode, serverResponse);
		}
	}
}
