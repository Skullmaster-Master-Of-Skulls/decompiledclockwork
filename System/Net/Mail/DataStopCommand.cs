using System;

namespace System.Net.Mail
{
	// Token: 0x020006C0 RID: 1728
	internal static class DataStopCommand
	{
		// Token: 0x06003572 RID: 13682 RVA: 0x000E3918 File Offset: 0x000E2918
		private static void CheckResponse(SmtpStatusCode statusCode, string serverResponse)
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
				switch (statusCode)
				{
				}
				break;
			}
			if (statusCode < (SmtpStatusCode)400)
			{
				throw new SmtpException(SR.GetString("net_webstatus_ServerProtocolViolation"), serverResponse);
			}
			throw new SmtpException(statusCode, serverResponse, true);
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000E397F File Offset: 0x000E297F
		private static void PrepareCommand(SmtpConnection conn)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(SmtpCommands.DataStop);
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x000E39AC File Offset: 0x000E29AC
		internal static void Send(SmtpConnection conn)
		{
			DataStopCommand.PrepareCommand(conn);
			string serverResponse;
			SmtpStatusCode statusCode = CheckCommand.Send(conn, out serverResponse);
			DataStopCommand.CheckResponse(statusCode, serverResponse);
		}
	}
}
