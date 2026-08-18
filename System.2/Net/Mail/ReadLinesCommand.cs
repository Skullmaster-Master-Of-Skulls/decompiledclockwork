using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x0200027D RID: 637
	internal static class ReadLinesCommand
	{
		// Token: 0x060017FF RID: 6143 RVA: 0x0007A8D4 File Offset: 0x00078AD4
		internal static IAsyncResult BeginSend(SmtpConnection conn, AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(conn, callback, state);
			multiAsyncResult.Enter();
			IAsyncResult asyncResult = conn.BeginFlush(ReadLinesCommand.onWrite, multiAsyncResult);
			if (asyncResult.CompletedSynchronously)
			{
				conn.EndFlush(asyncResult);
				multiAsyncResult.Leave();
			}
			SmtpReplyReader nextReplyReader = conn.Reader.GetNextReplyReader();
			multiAsyncResult.Enter();
			IAsyncResult asyncResult2 = nextReplyReader.BeginReadLines(ReadLinesCommand.onReadLines, multiAsyncResult);
			if (asyncResult2.CompletedSynchronously)
			{
				LineInfo[] result = conn.Reader.CurrentReader.EndReadLines(asyncResult2);
				if (!(multiAsyncResult.Result is Exception))
				{
					multiAsyncResult.Result = result;
				}
				multiAsyncResult.Leave();
			}
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0007A970 File Offset: 0x00078B70
		internal static LineInfo[] EndSend(IAsyncResult result)
		{
			object obj = MultiAsyncResult.End(result);
			if (obj is Exception)
			{
				throw (Exception)obj;
			}
			return (LineInfo[])obj;
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x0007A99C File Offset: 0x00078B9C
		private static void OnReadLines(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result.AsyncState;
				try
				{
					SmtpConnection smtpConnection = (SmtpConnection)multiAsyncResult.Context;
					LineInfo[] result2 = smtpConnection.Reader.CurrentReader.EndReadLines(result);
					if (!(multiAsyncResult.Result is Exception))
					{
						multiAsyncResult.Result = result2;
					}
					multiAsyncResult.Leave();
				}
				catch (Exception result3)
				{
					multiAsyncResult.Leave(result3);
				}
			}
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0007AA14 File Offset: 0x00078C14
		private static void OnWrite(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result.AsyncState;
				try
				{
					SmtpConnection smtpConnection = (SmtpConnection)multiAsyncResult.Context;
					smtpConnection.EndFlush(result);
					multiAsyncResult.Leave();
				}
				catch (Exception result2)
				{
					multiAsyncResult.Leave(result2);
				}
			}
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x0007AA6C File Offset: 0x00078C6C
		internal static LineInfo[] Send(SmtpConnection conn)
		{
			conn.Flush();
			return conn.Reader.GetNextReplyReader().ReadLines();
		}

		// Token: 0x04001827 RID: 6183
		private static AsyncCallback onReadLines = new AsyncCallback(ReadLinesCommand.OnReadLines);

		// Token: 0x04001828 RID: 6184
		private static AsyncCallback onWrite = new AsyncCallback(ReadLinesCommand.OnWrite);
	}
}
