using System;

namespace System.Net.Mail
{
	// Token: 0x02000280 RID: 640
	internal static class DataStopCommand
	{
		// Token: 0x06001812 RID: 6162 RVA: 0x0007AC54 File Offset: 0x00078E54
		private static void CheckResponse(SmtpStatusCode statusCode, string serverResponse)
		{
			if (statusCode <= SmtpStatusCode.InsufficientStorage)
			{
				if (statusCode == SmtpStatusCode.Ok)
				{
					return;
				}
				if (statusCode - SmtpStatusCode.LocalErrorInProcessing > 1)
				{
				}
			}
			else if (statusCode != SmtpStatusCode.ExceededStorageAllocation && statusCode != SmtpStatusCode.TransactionFailed)
			{
			}
			if (statusCode < (SmtpStatusCode)400)
			{
				throw new SmtpException(SR.GetString("net_webstatus_ServerProtocolViolation"), serverResponse);
			}
			throw new SmtpException(statusCode, serverResponse, true);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0007ACB1 File Offset: 0x00078EB1
		private static void PrepareCommand(SmtpConnection conn)
		{
			if (conn.IsStreamOpen)
			{
				throw new InvalidOperationException(SR.GetString("SmtpDataStreamOpen"));
			}
			conn.BufferBuilder.Append(SmtpCommands.DataStop);
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0007ACDC File Offset: 0x00078EDC
		internal static void Send(SmtpConnection conn)
		{
			DataStopCommand.PrepareCommand(conn);
			string serverResponse;
			SmtpStatusCode statusCode = CheckCommand.Send(conn, out serverResponse);
			DataStopCommand.CheckResponse(statusCode, serverResponse);
		}
	}
}
