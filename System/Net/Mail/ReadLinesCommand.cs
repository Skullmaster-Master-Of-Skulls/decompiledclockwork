using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x020006BD RID: 1725
	internal static class ReadLinesCommand
	{
		// Token: 0x0600355F RID: 13663 RVA: 0x000E353C File Offset: 0x000E253C
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

		// Token: 0x06003560 RID: 13664 RVA: 0x000E35D8 File Offset: 0x000E25D8
		internal static LineInfo[] EndSend(IAsyncResult result)
		{
			object obj = MultiAsyncResult.End(result);
			if (obj is Exception)
			{
				throw (Exception)obj;
			}
			return (LineInfo[])obj;
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x000E3604 File Offset: 0x000E2604
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
				catch
				{
					multiAsyncResult.Leave(new Exception(SR.GetString("net_nonClsCompliantException")));
				}
			}
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x000E36A0 File Offset: 0x000E26A0
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
				catch
				{
					multiAsyncResult.Leave(new Exception(SR.GetString("net_nonClsCompliantException")));
				}
			}
		}

		// Token: 0x06003563 RID: 13667 RVA: 0x000E371C File Offset: 0x000E271C
		internal static LineInfo[] Send(SmtpConnection conn)
		{
			conn.Flush();
			return conn.Reader.GetNextReplyReader().ReadLines();
		}

		// Token: 0x040030D9 RID: 12505
		private static AsyncCallback onReadLines = new AsyncCallback(ReadLinesCommand.OnReadLines);

		// Token: 0x040030DA RID: 12506
		private static AsyncCallback onWrite = new AsyncCallback(ReadLinesCommand.OnWrite);
	}
}
